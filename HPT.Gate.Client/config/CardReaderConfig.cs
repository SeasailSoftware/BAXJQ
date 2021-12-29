using hpt.gate.CardReader;
using HPT.Gate.Utils.Common;
using System;
using System.IO;

namespace hpt.gate.config
{
    public class CardReaderConfig
    {

        public static string _ConfigPath = Path.Combine(Environment.CurrentDirectory, "CardReader.config");

        #region  启用IC/ID卡
        public static bool ICCardEnabled
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "ICCardEnabled").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set(_ConfigPath, "ICCardEnabled", value ? "true" : "false"); }
        }


        #region 启用IC卡发卡器
        public static bool IC_Enabled
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "IC_Enabled").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set(_ConfigPath, "IC_Enabled", value ? "true" : "false"); }
        }
        #endregion


        public static string IC_SerialPort
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "IC_SerialPort"); }
            set { AppSettingsHelper.Set(_ConfigPath, "IC_SerialPort", value); }
        }

        public static int IC_Port
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get(_ConfigPath, "IC_Port")); }
            set { AppSettingsHelper.Set(_ConfigPath, "IC_Port", value.ToString()); }
        }

        public static bool IC_IDSerialEnabled
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "IC_IDSerialEnabled").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set(_ConfigPath, "IC_IDSerialEnabled", value ? "true" : "false"); }
        }

        public static string IC_IDSerialPort
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "IC_IDSerialPort"); }
            set { AppSettingsHelper.Set(_ConfigPath, "IC_IDSerialPort", value); }
        }

        public static bool IC_USBEnabled
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "IC_USBEnabled").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set(_ConfigPath, "IC_USBEnabled", value ? "true" : "false"); }
        }

        public static int USBType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get(_ConfigPath, "USBType")); }
            set { AppSettingsHelper.Set(_ConfigPath, "USBType", value.ToString()); }
        }
        public static int ICCardReaderType
        {
            get
            {
                return IC_Enabled ? 1 : IC_IDSerialEnabled ? 2 : IC_USBEnabled ? 3 : 1;
            }
        }

        #endregion

        #region  启用IC/ID和身份证序列号卡
        public static bool ICAndIDSerialEnabled
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "ICAndIDSerialEnabled").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set(_ConfigPath, "ICAndIDSerialEnabled", value ? "true" : "false"); }
        }

        public static string IDSerialPort
        {
            get { return AppSettingsHelper.Get(_ConfigPath, "IDSerialPort"); }
            set { AppSettingsHelper.Set(_ConfigPath, "IDSerialPort", value); }
        }

        #endregion

        #region 身份证号码
        public static int IDCardType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get(_ConfigPath, "IDCardType")); }
            set { AppSettingsHelper.Set(_ConfigPath, "IDCardType", value.ToString()); }
        }
        #endregion

        #region 初始化发卡器
        public static void InitCardReader()
        {
            #region 初始化IC卡发卡器
            if (ICCardEnabled)
            {
                switch (ICCardReaderType)
                {
                    case 1:
                        if (!ICCardReader.Active)
                            ICCardReader.InitCardReader(CardReaderConfig.IC_SerialPort);
                        break;
                    case 2:
                        if (!CardReaderConfig.IC_IDSerialPort.Trim().Equals(string.Empty))
                            IDSerialReader.InitIDSerialReader(CardReaderConfig.IC_IDSerialPort);
                        break;
                    case 3:
                        break;
                }
            }
            #endregion

            #region 初始化身份证序列号发卡器
            if (CardReaderConfig.ICAndIDSerialEnabled)
            {
                if (!IDSerialPort.Trim().Equals(string.Empty))
                    IDSerialReader.InitIDSerialReader(IDSerialPort);
            }
            #endregion

            #region 初始化身份证阅读器
            IDCardReader.InitIDCardReader();
            #endregion

        }
        #endregion


    }
}
