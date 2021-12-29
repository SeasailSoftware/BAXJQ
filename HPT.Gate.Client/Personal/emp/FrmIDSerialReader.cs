using HPT.Gate.Client.Base;
using hpt.gate.config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Client.emp
{
    public partial class FrmIDSerialReader : FrmBase
    {
        public FrmIDSerialReader()
        {
            InitializeComponent();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmIDSerialReader_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #region 加载配置文件
        private void LoadConfig()
        {
            cbbIDSerialPort.Text = CardReaderConfig.IDSerialPort;
        }

        #endregion

        private void buttonX2_Click(object sender, EventArgs e)
        {
            cbbIDSerialPort.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                cbbIDSerialPort.Items.Add(s);
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        #region 保存配置
        private void SaveConfig()
        {
            CardReaderConfig.IDSerialPort = cbbIDSerialPort.Text;
            DialogResult = DialogResult.OK;
        }

        #endregion

    }
}
