using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace HPT.Gate.Device.Data
{
    public class DataRealtimeRespon
    {
        #region 返回参数

        public int DeptId { get; set; }

        public string DeptName { get; set; }
        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string Duty { get; set; }

        public int CardType { get; set; }

        public string CardNo { get; set; }


        /// <summary>
        /// 特殊动作
        /// </summary>
        public byte SpecialAction { get; set; }
        /// <summary>
        /// 开门动作0x00不开门,0x01开门
        /// </summary>
        public byte Action { get; set; }

        public byte VoiceNo { get; set; }

        public UInt16 PhotoName { get; set; }

        public string Message_Row1 { get; set; }

        public string Message_Row2 { get; set; }

        public string Message_Row3 { get; set; }

        public byte SingleTicket { get; set; }


        public Bitmap Photo { get; set; }

        public string ErrorMessage
        {
            get
            {
                try
                {
                    RecordEvent e = (RecordEvent)Action;
                    Type t = e.GetType();
                    FieldInfo info = t.GetField(Enum.GetName(t, e));
                    DescriptionAttribute description = (DescriptionAttribute)Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute));
                    return description.Description;
                }
                catch
                {
                    return "未知卡状态";
                }
            }
        }


        #endregion

        #region 返回参数序列化

        public byte[] ToArray()
        {
            List<byte> list = new List<byte>();
            list.Add(Action);
            list.Add(VoiceNo);
            byte[] photoName = BitConverter.GetBytes(PhotoName);
            Array.Reverse(photoName);
            list.AddRange(photoName);
            list.AddRange(ArrayHelper.GB2312ToArray(Message_Row1, 16));
            list.AddRange(ArrayHelper.GB2312ToArray(Message_Row2, 16));
            list.AddRange(ArrayHelper.GB2312ToArray(Message_Row3, 16));
            list.AddRange(ArrayHelper.GB2312ToArray(ErrorMessage, 16));
            list.Add(SpecialAction);
            list.AddRange(ArrayHelper.HexToArray(EmpId.ToString("00000000"), 4));
            list.Add(SingleTicket);
            list.AddRange(new byte[26]);
            return list.ToArray();
        }
        #endregion
    }
}
