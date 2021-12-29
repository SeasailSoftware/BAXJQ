using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using hpt.gate.config;
using System.IO;
using System.Diagnostics;
using HPT.Gate.Client.Base;
using hpt.gate.CardReader;
using Joey.UserControls;

namespace HPT.Gate.Client
{
    public partial class SerialSettingForm : JForm
    {
        public SerialSettingForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            //动态读取串口号存放到combobox
            foreach (string s in SerialPort.GetPortNames())
            {
                cbbICSerialPort.Items.Add(s);
                cbbICIDSerialPort.Items.Add(s);
                cbbICAndIDSerial.Items.Add(s);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        #region 保存配置文件
        private void SaveConfig()
        {
            CardReaderConfig.ICCardEnabled = rbICCardEnabled.Checked;
            CardReaderConfig.IC_Enabled = rbICEnable.Checked;
            CardReaderConfig.IC_SerialPort = cbbICSerialPort.Text;
            CardReaderConfig.IC_Port = (int)numICPort.Value;

            CardReaderConfig.IC_IDSerialEnabled = rbIC_IDSerialEnabled.Checked;
            CardReaderConfig.IC_IDSerialPort = cbbICIDSerialPort.Text;
            CardReaderConfig.IC_USBEnabled = rbUSB.Checked;
            CardReaderConfig.USBType = cbbWgType.SelectedIndex;

            CardReaderConfig.ICAndIDSerialEnabled = rbICAndIDSerial.Checked;
            CardReaderConfig.IDSerialPort = cbbICAndIDSerial.Text;

            CardReaderConfig.IDCardType = cbbIDCardType.SelectedIndex;
        }
        #endregion

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cbbICSerialPort.Enabled = rbICEnable.Checked;
            numICPort.Enabled = rbICEnable.Checked;
            buttonX1.Enabled = rbICEnable.Checked;
            buttonX5.Enabled = rbICEnable.Checked;
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cbbICIDSerialPort.Enabled = rbIC_IDSerialEnabled.Checked;
            buttonX2.Enabled = rbIC_IDSerialEnabled.Checked;
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            cbbICSerialPort.Text = ICCardReader.GetCurrentPort();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            cbbICIDSerialPort.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                cbbICIDSerialPort.Items.Add(s);
            }
            if (cbbICIDSerialPort.Items.Count > 0)
                cbbICIDSerialPort.SelectedIndex = 0;
        }

        private void SerialSettingForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #region 加载配置文件
        private void LoadConfig()
        {
            rbICCardEnabled.Checked = CardReaderConfig.ICCardEnabled;
            rbICEnable.Checked = CardReaderConfig.IC_Enabled;
            cbbICSerialPort.Text = CardReaderConfig.IC_SerialPort;
            numICPort.Value = CardReaderConfig.IC_Port;
            rbIC_IDSerialEnabled.Checked = CardReaderConfig.IC_IDSerialEnabled;
            cbbICIDSerialPort.Text = CardReaderConfig.IC_IDSerialPort;
            rbUSB.Checked = CardReaderConfig.IC_USBEnabled;
            cbbWgType.SelectedIndex = CardReaderConfig.USBType;
            rbICAndIDSerial.Checked = CardReaderConfig.ICAndIDSerialEnabled;
            cbbICAndIDSerial.Text = CardReaderConfig.IDSerialPort;

            cbbIDCardType.SelectedIndex = CardReaderConfig.IDCardType;
            cbbFingerPrint.SelectedIndex = 0;
            //gbICEnable.Enabled = rbICEnable.Checked;
            //gbIDSerial.Enabled = rbIC_IDSerialEnabled.Checked;
            //cbbWgType.Enabled = rbUSB.Checked;
        }

        #endregion

        private void rbUSB_CheckedChanged(object sender, EventArgs e)
        {
            cbbWgType.Enabled = rbUSB.Checked;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {

        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            InstallICDriver();
        }

        #region 安装IC发卡器驱动
        private void InstallICDriver()
        {
            string driverPath = $@"{Application.StartupPath}\发卡器驱动\IC发卡器驱动.EXE";
            if (!File.Exists(driverPath))
            {
                MessageBoxHelper.Info($"找不到驱动文件[{driverPath}]");
                return;
            }
            try
            {
                Process.Start(driverPath);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"安装IC发卡器驱动失败:{ex.Message}");
                return;
            }
        }
        #endregion

        private void buttonX6_Click(object sender, EventArgs e)
        {
            InstallIDCardDriver();
        }

        #region 安装身份证阅读器驱动
        private void InstallIDCardDriver()
        {
            string driverPath = $@"{Application.StartupPath}\发卡器驱动\华视身份证阅读器驱动.EXE";
            if (!File.Exists(driverPath))
            {
                MessageBoxHelper.Info($"找不到驱动文件[{driverPath}]");
                return;
            }
            try
            {
                Process.Start(driverPath);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"安装华视身份证阅读器驱动失败:{ex.Message}");
                return;
            }
        }
        #endregion

        private void rbICCard_CheckedChanged(object sender, EventArgs e)
        {
            gbICCard.Enabled = rbICCardEnabled.Checked;
        }

        private void rbICAndIDSerial_CheckedChanged(object sender, EventArgs e)
        {
            gbICAndIDSerial.Enabled = rbICAndIDSerial.Checked;
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            cbbICAndIDSerial.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                cbbICAndIDSerial.Items.Add(s);
            }
            if (cbbICAndIDSerial.Items.Count > 0)
                cbbICAndIDSerial.SelectedIndex = 0;
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            InstallFringPrintDriver();
        }

        private void InstallFringPrintDriver()
        {
            string driverPath = $@"{Application.StartupPath}\发卡器驱动\中控指纹仪驱动.EXE";
            if (!File.Exists(driverPath))
            {
                MessageBoxHelper.Info($"找不到驱动文件[{driverPath}]");
                return;
            }
            try
            {
                Process.Start(driverPath);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"安装华视身份证阅读器驱动失败:{ex.Message}");
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
            DialogResult = DialogResult.OK;
        }
    }
}
