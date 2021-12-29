using Apache.NMS;
using Apache.NMS.ActiveMQ;
using hpt.gate.DataAccess.Entity;
using hpt.gate.DataAccess.Service;
using HPT.Gate.Host.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPT.Gate.Host.JMS
{
    public class JMSReceiver
    {
        private string _ServerName;
        private string _Account;
        private string _Password;
        private string _ClientId;
        private string _Filter = "";
        private const string Card_Add = "add.door.card.auth";
        private const string Card_Delete = "delete.door.card.auth";
        private const string Timegroup_Add = "add.door.time.rule";
        private const string Timegroup_Delete = "delete.door.time.rule";
        private const string Timegroup_Reset = "full.door.time.rule";
        private const string Vacation_Add = "vacation.pass";
        private IMessageConsumer _Consumer_Card_Add;
        private IMessageConsumer _Consumer_Card_Delete;
        private IMessageConsumer _Consumer_Timegroup_Add;
        private IMessageConsumer _Consumer_Timegroup_Delete;
        private IMessageConsumer _Consumer_Timegroup_Reset;
        private IMessageConsumer _Consumer_Vacation;
        private IConnection _Connection;

        public JMSReceiver(string serverName, string userName, string password, string clientId, string filter = "")
        {
            _ServerName = serverName;
            _Account = userName;
            _Password = password;
            _Filter = filter;
            _ClientId = clientId;
        }

        #region Event
        public event Action<string> Message;

        /// <summary>
        /// 触发消息事件
        /// </summary>
        /// <param name="msg">提示消息</param>
        protected void OnMessage(string msg)
        {
            if (msg.Equals(string.Empty)) return;
            try
            {
                if (Message == null) return;
                Message($"[JMS]{msg}");
            }
            catch
            {
            }
        }
        #endregion

        public void Start()
        {
            try
            {
                //创建连接工厂
                IConnectionFactory factory = new ConnectionFactory($"tcp://{_ServerName}");
                //通过工厂构建连接
                _Connection = factory.CreateConnection(_Account, _Password);
                //这个是连接的客户端名称标识
                _Connection.ClientId = _ClientId;
                //启动连接，监听的话要主动启动连接
                _Connection.Start();
                _Connection.ExceptionListener += _Connection_ExceptionListener;
                _Connection.ConnectionInterruptedListener += _Connection_ConnectionInterruptedListener;
                //通过连接创建一个会话
                ISession session = _Connection.CreateSession();
                //通过会话创建一个消费者，这里就是Queue这种会话类型的监听参数设置

                ///订阅添加权限消息
                ///new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"), "filter='demo'"
                // _Consumer_Card_Add = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(Card_Add));
                _Consumer_Card_Add = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(Card_Add), $"filter='{_Filter}'");
                _Consumer_Card_Add.Listener += new MessageListener(Listener_Card_Add);

                ///订阅删除权限消息
                _Consumer_Card_Delete = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(Card_Delete), $"filter='{_Filter}'");
                _Consumer_Card_Delete.Listener += new MessageListener(Listener_Card_Delete);

                ///订阅添加时间组消息
                _Consumer_Timegroup_Add = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(Timegroup_Add), $"filter='{_Filter}'");
                _Consumer_Timegroup_Add.Listener += new MessageListener(Listener_Timegroup_Add);

                ///订阅删除时间组消息
                _Consumer_Timegroup_Delete = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(Timegroup_Delete), $"filter='{_Filter}'");
                _Consumer_Timegroup_Delete.Listener += new MessageListener(Listener_Timegroup_Delete);
                ///订阅删除时间组消息
                _Consumer_Timegroup_Reset = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(Timegroup_Reset), $"filter='{_Filter}'");
                _Consumer_Timegroup_Reset.Listener += new MessageListener(Listener_Timegroup_Reset);
                ///订阅请假信息消息
                _Consumer_Vacation = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(Vacation_Add), $"filter='{_Filter}'");
                _Consumer_Vacation.Listener += new MessageListener(Listener_Vacation_Add);
                OnMessage("服务已启动!");
            }
            catch (Exception ex)
            {
                OnMessage($"服务启动失败:{ex.Message}");
            }

        }

        private void _Connection_ConnectionInterruptedListener()
        {
            Restart();
        }

        private void _Connection_ExceptionListener(Exception exception)
        {
            OnMessage($"JMS异常:{exception.Message}");
        }

        private void Listener_Timegroup_Reset(IMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                StringBuilder buffer = new StringBuilder();
                ITextMessage tm = (ITextMessage)message;
                //OnMessage($"添加时间组:{tm.Text}");
                List<HTTimegroup> timegroups = new List<HTTimegroup>();
                try
                {
                    timegroups.AddRange(JsonConvert.DeserializeObject<List<HTTimegroup>>(tm.Text));
                }
                catch (Exception ex)
                {
                    buffer.AppendLine($"Json转HTTimegroup错误:{ex.Message}");
                }
                HTTimegroupService.Clear();
                foreach (HTTimegroup timegroup in timegroups)
                {
                    if (HTTimegroupService.Insert(timegroup, out string msg))
                    {
                        if (DTAPI.HandleAddTimegroup(AppSettings.ServerURL, timegroup.id, timegroup.sign, true, out msg))
                            buffer.AppendLine($"添加时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]成功!");
                        else
                            buffer.AppendLine($"添加时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]失败:{msg}");
                    }
                    else
                        buffer.AppendLine($"添加时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]失败:{msg}");
                }
                OnMessage(buffer.ToString());
            });
        }

        private void Listener_Vacation_Add(IMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                StringBuilder buffer = new StringBuilder();
                ITextMessage tm = (ITextMessage)message;
                //OnMessage($"请假信息:{tm.Text}");
                List<HTVacation> vacations = new List<HTVacation>();
                try
                {
                    vacations = JsonConvert.DeserializeObject<List<HTVacation>>(tm.Text);
                }
                catch (Exception ex)
                {
                    buffer.AppendLine($"Json转HTVacation错误:{ex.Message}");
                }
                foreach (HTVacation vacation in vacations)
                {
                    if (HTVacationService.Insert(vacation, out string msg))
                    {
                        if (DTAPI.HandleVacation(AppSettings.ServerURL, vacation.CardNo, out msg))
                            buffer.AppendLine($"添加请假信息[卡号:{vacation.CardNo},开始时间:{vacation.BeginTime},结束时间:{vacation.EndTime}]成功!");
                        else
                            buffer.AppendLine($"添加请假信息[卡号:{vacation.CardNo},开始时间:{vacation.BeginTime},结束时间:{vacation.EndTime}]失败:{msg}");
                    }
                    else
                        buffer.AppendLine($"添加请假信息[卡号:{vacation.CardNo},开始时间:{vacation.BeginTime},结束时间:{vacation.EndTime}]失败:{msg}");
                }
                OnMessage(buffer.ToString());
            });
        }

        private void Listener_Timegroup_Delete(IMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                StringBuilder buffer = new StringBuilder();
                ITextMessage tm = (ITextMessage)message;
                //OnMessage($"删除时间组:{tm.Text}");
                List<HTTimegroup> timegroups = new List<HTTimegroup>();
                try
                {
                    timegroups.AddRange(JsonConvert.DeserializeObject<List<HTTimegroup>>(tm.Text));
                }
                catch (Exception ex)
                {
                    buffer.AppendLine($"Json转HTTimegroup错误:{ex.Message}");
                }
                foreach (HTTimegroup timegroup in timegroups)
                {
                    if (HTTimegroupService.Delete(timegroup, out string msg))
                    {
                        if (DTAPI.HandleDeleteTimegroup(AppSettings.ServerURL, timegroup.id, timegroup.sign, true, out msg))
                            buffer.AppendLine($"删除时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]成功!");
                        else
                            buffer.AppendLine($"删除时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]失败:{msg}");
                    }
                    else
                        buffer.AppendLine($"删除时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]失败:{msg}");
                }
                OnMessage(buffer.ToString());
            });
        }

        private void Listener_Timegroup_Add(IMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                StringBuilder buffer = new StringBuilder();
                ITextMessage tm = (ITextMessage)message;
                //OnMessage($"添加时间组:{tm.Text}");
                List<HTTimegroup> timegroups = new List<HTTimegroup>();
                try
                {
                    timegroups.AddRange(JsonConvert.DeserializeObject<List<HTTimegroup>>(tm.Text));
                }
                catch (Exception ex)
                {
                    buffer.AppendLine($"Json转HTTimegroup错误:{ex.Message}");
                }
                foreach (HTTimegroup timegroup in timegroups)
                {
                    if (HTTimegroupService.Insert(timegroup, out string msg))
                    {
                        if (DTAPI.HandleAddTimegroup(AppSettings.ServerURL, timegroup.id, timegroup.sign, true, out msg))
                            buffer.AppendLine($"添加时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]成功!");
                        else
                            buffer.AppendLine($"添加时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]失败:{msg}");
                    }
                    else
                        buffer.AppendLine($"添加时间段[设备:{timegroup.termSn},组号:{timegroup.id},时间:[{timegroup.beginTime}-{timegroup.endTime}]]失败:{msg}");
                }
                OnMessage(buffer.ToString());
            });

        }

        private void Restart()
        {
            Stop();
            Start();
        }
        public void Stop()
        {
            try
            {
                _Consumer_Card_Add.Listener -= Listener_Card_Add;
                _Consumer_Card_Delete.Listener -= Listener_Card_Delete;
                _Consumer_Timegroup_Add.Listener -= Listener_Timegroup_Add;
                _Consumer_Timegroup_Delete.Listener -= Listener_Timegroup_Delete;
                _Consumer_Vacation.Listener -= Listener_Vacation_Add;
                _Consumer_Card_Add.Close();
                _Consumer_Card_Delete.Close();
                _Consumer_Timegroup_Add.Close();
                _Consumer_Timegroup_Delete.Close();
                _Consumer_Vacation.Close();
                _Connection.Stop();
                _Connection.Close();
                OnMessage("服务已关闭!");
            }
            catch (Exception ex)
            {
                OnMessage($"服务关闭失败:{ex.Message}");
            }
        }

        private void Listener_Card_Add(IMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                StringBuilder buffer = new StringBuilder();
                ITextMessage tm = (ITextMessage)message;
                //OnMessage($"添加权限:{tm.Text}");
                List<HTCard> cards = new List<HTCard>();
                try
                {
                    cards.AddRange(JsonConvert.DeserializeObject<List<HTCard>>(tm.Text));
                }
                catch (Exception ex)
                {
                    buffer.AppendLine($"Json转HTcards错误:{ex.Message}");
                }
                List<int> ids = new List<int>();
                string msg;
                foreach (HTCard card in cards)
                {
                    if (HTCardService.Insert(card, out msg))
                    {
                        ids.Add(card.Id);
                        buffer.AppendLine($"添加权限[设备:{card.TermSN},卡号:{card.CardNo}]成功!");
                    }
                    else
                        buffer.AppendLine($"添加权限[设备:{card.TermSN},卡号:{card.CardNo}]失败:{msg}");
                }
                if (DTAPI.HandleAddCard(AppSettings.ServerURL, ids, _Filter, out msg))
                    buffer.AppendLine($"应答添加卡权限[{JsonConvert.SerializeObject(ids)}]成功!");
                else
                    buffer.AppendLine($"应答添加卡权限[{JsonConvert.SerializeObject(ids)}]失败:{msg}!");
                OnMessage(buffer.ToString());
            });

        }


        private void Listener_Card_Delete(IMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                StringBuilder buffer = new StringBuilder();
                ITextMessage tm = (ITextMessage)message;
                //OnMessage($"删除权限:{tm.Text}");
                List<HTCard> cards = new List<HTCard>();
                try
                {
                    cards.AddRange(JsonConvert.DeserializeObject<List<HTCard>>(tm.Text));
                }
                catch (Exception ex)
                {
                    buffer.AppendLine($"Json转HTcards错误:{ex.Message}");
                }
                string msg;
                List<int> ids = new List<int>();
                foreach (HTCard card in cards)
                {
                    if (HTCardService.Delete(card, out msg))
                    {
                        ids.Add(card.Id);
                        buffer.AppendLine($"删除权限[设备:{card.TermSN},卡号:{card.CardNo}]成功!");
                    }
                    else
                        buffer.AppendLine($"删除权限[设备:{card.TermSN},卡号:{card.CardNo}]失败:{msg}");
                }
                if (DTAPI.HandleDeleteCard(AppSettings.ServerURL, ids, _Filter, out msg))
                    buffer.AppendLine($"删除权限[{JsonConvert.SerializeObject(ids)}]成功!");
                else
                    buffer.AppendLine($"删除权限[{JsonConvert.SerializeObject(ids)}]失败:{msg}");
                OnMessage(buffer.ToString());
            });
        }

    }
}
