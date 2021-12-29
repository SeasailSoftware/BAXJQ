#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：HPT.Gate.Host.JMS

* 项目描述 ：

* 类 名 称 ：MsgCreater

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：HPT.Gate.Host.JMS

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-04 17:07:58

* 更新时间 ：2019-07-04 17:07:58

* 版 本 号 ：v1.0.0.0

*******************************************************************

* Copyright @ Administrator 2019. All rights reserved.

*******************************************************************

//----------------------------------------------------------------
*/

#endregion
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using HPT.Gate.Host.Config;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HPT.Gate.Host.JMS
{
    /// <summary>
    /// 功能描述    ：MsgCreater  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-04 17:07:58 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-04 17:07:58 
    /// </summary>
    public class JMSCreater
    {
        private int _Port;
        private bool IsRunning = false;
        private const string CARD_ADD = "add.door.card.auth";
        public JMSCreater()
        {

        }
        private IConnectionFactory factory;

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
                if (IsRunning) return;
                //初始化工厂，这里默认的URL是不需要修改的
                IsRunning = true;
                Task.Factory.StartNew(() => { AutoCreateMsg(); });
            }
            catch (Exception ex)
            {
                OnMessage($"服务启动失败:{ex.Message}");
            }
        }

        private void AutoCreateMsg()
        {
            int current = 0;
            //初始化工厂，这里默认的URL是不需要修改的
            factory = new ConnectionFactory($"tcp://{AppSettings.JMSServer}");
            using (IConnection connection = factory.CreateConnection())
            {
                //通过连接创建Session会话
                using (ISession session = connection.CreateSession())
                {
                    //通过会话创建生产者，方法里面new出来的是MQ中的Queue
                    IMessageProducer prod = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(CARD_ADD));
                    while (IsRunning)
                    {
                        try
                        {
                            //创建一个发送的消息对象
                            ITextMessage message = prod.CreateTextMessage();
                            //给这个对象赋实际的消息
                            message.Text = $"Msg{current++}:Hello";
                            //设置消息对象的属性，这个很重要哦，是Queue的过滤条件，也是P2P消息的唯一指定属性
                            message.Properties.SetString("filter", AppSettings.JMSFilter);
                            //生产者把消息发送出去，几个枚举参数MsgDeliveryMode是否长链，MsgPriority消息优先级别，发送最小单位，当然还有其他重载
                            prod.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);

                        }
                        catch (Exception ex)
                        {
                            OnMessage($"产生消息失败:{ex.Message}");
                        }
                        int index = 10;
                        while (IsRunning && index > 0)
                        {
                            Thread.Sleep(1000);
                            index--;
                        }
                    }
                }
            }
        }
    }
}
