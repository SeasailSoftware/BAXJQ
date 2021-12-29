using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hpt.gate.CardReader
{
    public class IDCardReader
    {
        /// <summary>
        ///  身份证阅读器参数
        /// </summary>
        public static int iRetUSB = 0, iRetCOM = 0;

        /// <summary>
        /// 身份证名字
        /// </summary>
        private static string _Name;

        public static string Name
        {
            get { return IDCardReader._Name; }
            set { IDCardReader._Name = value; }
        }
        /// <summary>
        /// 身份证号码
        /// </summary>
        private static string _CardNo;

        public static string CardNo
        {
            get { return IDCardReader._CardNo; }
            set { IDCardReader._CardNo = value; }
        }

        /// <summary>
        /// 民族
        /// </summary>
        private static string _Nation;

        public static string Nation
        {
            get { return IDCardReader._Nation; }
            set { IDCardReader._Nation = value; }
        }

        /// <summary>
        /// 出生年月日
        /// </summary>
        private static string _BirthDay;

        public static string BirthDay
        {
            get { return IDCardReader._BirthDay; }
            set { IDCardReader._BirthDay = value; }
        }
        /// <summary>
        /// 住址
        /// </summary>
        private static string _Address;

        public static string Address
        {
            get { return IDCardReader._Address; }
            set { IDCardReader._Address = value; }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        private static string _BeginDate;

        public static string BeginDate
        {
            get { return IDCardReader._BeginDate; }
            set { IDCardReader._BeginDate = value; }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        private static string _EndDate;

        public static string EndDate
        {
            get { return IDCardReader._EndDate; }
            set { IDCardReader._EndDate = value; }
        }
        /// <summary>
        /// 签卡日期
        /// </summary>
        private static string _SignDate;

        public static string SignDate
        {
            get { return IDCardReader._SignDate; }
            set { IDCardReader._SignDate = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        private static string _Sex;

        public static string Sex
        {
            get { return IDCardReader._Sex; }
            set { IDCardReader._Sex = value; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        private static string _SamId;

        public static string SamId
        {
            get { return IDCardReader._SamId; }
            set { IDCardReader._SamId = value; }
        }

        /// <summary>
        /// 是否已经激活的标志
        /// </summary>
        public static bool Active { get; set; }
        /// <summary>
        /// 初始化身份证阅读器
        /// </summary>
        /// <returns></returns>
        public static bool InitIDCardReader()
        {
            int iPort;
            for (iPort = 1001; iPort <= 1016; iPort++)
            {
                iRetUSB = CVRSDK.CVR_InitComm(iPort);
                if (iRetUSB == 1)
                {
                    break;
                }
            }
            if (iRetUSB != 1)
            {
                for (iPort = 1; iPort <= 4; iPort++)
                {
                    iRetCOM = CVRSDK.CVR_InitComm(iPort);
                    if (iRetCOM == 1)
                    {
                        break;
                    }
                }
            }
            if ((iRetCOM == 1) || (iRetUSB == 1))
            {
                Active = true;
                return true;
            }
            else
            {
                Active = false;
                return false;
            }
        }
        /// <summary>
        /// 读取身份证信息
        /// </summary>
        public static bool IDReadCard()
        {
            try
            {
                if (!Active)
                {
                    if (!InitIDCardReader())
                    {
                        MessageBox.Show("身份证阅读器初始化失败!", "身份证阅读器读卡提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                if ((iRetCOM == 1) || (iRetUSB == 1))
                {

                    int authenticate = CVRSDK.CVR_Authenticate();
                    if (authenticate == 1)
                    {
                        int readContent = CVRSDK.CVR_Read_Content(4);
                        if (readContent == 1)
                        {
                            FillData();
                            return true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("未放置身份证或身份证放置不正确", "身份证阅读器读卡提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("身份证阅读器初始化失败！", "身份证阅读器初始化提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        #region 自动读卡
        public static bool AutoReadCard()
        {
            try
            {
                if (!Active)
                {
                    if (!InitIDCardReader())
                    {
                        //MessageBox.Show("身份证阅读器初始化失败!", "身份证阅读器读卡提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                if ((iRetCOM == 1) || (iRetUSB == 1))
                {

                    int authenticate = CVRSDK.CVR_Authenticate();
                    if (authenticate == 1)
                    {
                        int readContent = CVRSDK.CVR_Read_Content(4);
                        if (readContent == 1)
                        {
                            FillData();
                            return true;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("未放卡或卡片放置不正确", "身份证阅读器读卡提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("身份证阅读器初始化失败！", "身份证阅读器初始化提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.ToString());
            }
            return false;
        }
        #endregion

        public static void FillData()
        {
            try
            {
                byte[] name = new byte[30];
                int length = 30;
                CVRSDK.GetPeopleName(ref name[0], ref length);
                ///身份证姓名
                Name = Encoding.GetEncoding("GB2312").GetString(name);
                byte[] number = new byte[30];
                length = 36;
                CVRSDK.GetPeopleIDCode(ref number[0], ref length);
                CardNo = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                byte[] people = new byte[30];
                length = 3;
                CVRSDK.GetPeopleNation(ref people[0], ref length);
                Nation = System.Text.Encoding.GetEncoding("GB2312").GetString(people).Replace("\0", "").Trim();
                byte[] validtermOfStart = new byte[30];
                length = 16;
                CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);
                BeginDate = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim();

                byte[] birthday = new byte[30];
                length = 16;
                CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);
                BirthDay = System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();

                byte[] address = new byte[64];
                length = 70;
                CVRSDK.GetPeopleAddress(ref address[0], ref length);
                Address = System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();

                byte[] validtermOfEnd = new byte[30];
                length = 16;
                CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);
                EndDate = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();

                byte[] signdate = new byte[30];
                length = 30;
                CVRSDK.GetDepartment(ref signdate[0], ref length);
                SignDate = System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();

                byte[] sex = new byte[30];
                length = 3;
                CVRSDK.GetPeopleSex(ref sex[0], ref length);
                Sex = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();

                byte[] samid = new byte[32];
                CVRSDK.CVR_GetSAMID(ref samid[0]);
                SamId = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
