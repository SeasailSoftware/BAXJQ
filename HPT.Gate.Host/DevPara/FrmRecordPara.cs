using HPT.Gate.Host.Base;
using HPT.Gate.Device.Dev;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmRecordPara : WinBase
    {
        private  TcpDevice _CurrentDevice =null;
        public FrmRecordPara(TcpDevice device)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _CurrentDevice = device;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                numUnCollect.Value = _CurrentDevice.GetUnCollected();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取设备未采集记录数失败:{ex.Message}","错误信息",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (_CurrentDevice.RestoreRecords((int)numRestore.Value))
                MessageBox.Show($"记录恢复成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"记录恢复失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
