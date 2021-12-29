using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPT.Gate.Utils.Common;
using hpt.gate.config;

namespace HPT.Gate.Client.emp
{
    public partial class USBCardReader : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 读头维根格式
        /// </summary>
        public int _WGType = 2;
        public string _CardNo = string.Empty;
        public USBCardReader(int wgType)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _WGType = wgType;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string _OriginalCardNo = tbOriCardNo.Text.Trim();
            int length = _OriginalCardNo.Length;
            if (length == 8 || length == 10)
            {
                switch (this._WGType)
                {
                    case 0:
                        if (_OriginalCardNo.Length == 8)
                        {
                            try
                            {
                                string cardNo = ArrayHelper.ToWG26CardNo(_OriginalCardNo);
                                tbCardNo.Text = cardNo;
                                _CardNo = cardNo;
                            }
                            catch
                            {

                            }
                        }
                        break;
                    case 1:

                        if (_OriginalCardNo.Length == 8)
                        {
                            tbCardNo.Text = _OriginalCardNo;
                            _CardNo = _OriginalCardNo;
                        }
                        ///维根34
                        if (_OriginalCardNo.Length == 10)
                        {
                            try
                            {
                                UInt64 num = Convert.ToUInt64(_OriginalCardNo);
                                byte[] arr = ArrayHelper.IntToBytes(num);
                                tbCardNo.Text = ArrayHelper.ArrayToHex(arr);
                                _CardNo = ArrayHelper.ArrayToHex(arr);
                            }
                            catch
                            {

                            }
                        }

                        break;
                    case 2:
                        if (_OriginalCardNo.Length == 8)
                        {
                            tbCardNo.Text = _OriginalCardNo;
                            _CardNo = _OriginalCardNo;
                        }
                        break;
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }
    }
}