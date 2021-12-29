using HPT.Gate.Host.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Host.face
{
    public partial class FrmNetPara : WinBase
    {
        public string IPAddress;
        public string Mac;
        public string NetMark;
        public string GateWay;
        public FrmNetPara(string ip,string mac,string netmark,string gateway)
        {
            InitializeComponent();
            IPAddress = ip;
            Mac = mac;
            NetMark = netmark;
            GateWay = gateway;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            IPAddress = tbIPAddress.Text;
            Mac = tbMac.Text;
            NetMark = tbNetMark.Text;
            GateWay = tbGateWay.Text;
            DialogResult = DialogResult.OK;
        }

        private void FrmNetPara_Load(object sender, EventArgs e)
        {
            tbIPAddress.Text = IPAddress;
            tbMac.Text = Mac;
            tbNetMark.Text = NetMark;
            tbGateWay.Text = GateWay;
        }
    }
}
