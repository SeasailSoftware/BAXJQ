using HPT.Gate.Client.Base;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Joey.UserControls;

namespace HPT.Gate.Client.BarCode
{
    public partial class FrmBarCode : FrmBase
    {
        public FrmBarCode()
        {
            InitializeComponent();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmBarcodeTemplate template = new FrmBarcodeTemplate();
            template.ShowDialog();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            FrmBarcodeSettings settings = new FrmBarcodeSettings();
            settings.ShowDialog();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            FrmBarcodePrint print = new FrmBarcodePrint();
            print.ShowDialog();
            LoadBardCodes();
        }

        private void FrmBarCode_Load(object sender, EventArgs e)
        {
            LoadBardCodes();
        }

        #region 加载当天打印的二维码
        private void LoadBardCodes()
        {
            try
            {
                string beginTime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                string endTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                List<Barcode> barcodeList = BarcodeService.Find(beginTime, endTime);
                dgvBarcode.DataSource = null;
                dgvBarcode.Rows.Clear();
                foreach (Barcode barcode in barcodeList)
                {
                    int rowIndex = dgvBarcode.Rows.Add();
                    dgvBarcode.Rows[rowIndex].Cells[0].Value = barcode.RecId;
                    dgvBarcode.Rows[rowIndex].Cells[1].Value = barcode.BarcodeNo;
                    dgvBarcode.Rows[rowIndex].Cells[2].Value = barcode.EffectTime;
                    dgvBarcode.Rows[rowIndex].Cells[3].Value = barcode.CreateTime;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载当天条形码列表失败:{ex.Message}");
            }

        }
        #endregion

    }
}
