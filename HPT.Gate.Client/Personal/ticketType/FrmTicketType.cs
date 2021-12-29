using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Joey.UserControls;
using HPT.Gate.Client.cardType;

namespace HPT.Gate.Client.emp
{
    public partial class FrmTicketType : FrmBase
    {
        public FrmTicketType()
        {
            InitializeComponent();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmAddTicketType add = new FrmAddTicketType();
            DialogResult dr = add.ShowDialog();
            if (dr == DialogResult.OK)
                LoadCardTypes();
        }

        private void FrmCardType_Load(object sender, EventArgs e)
        {
            LoadCardTypes();
        }

        #region 加载卡类列表
        private void LoadCardTypes()
        {
            try
            {
                List<TicketType> ticketTypeList = TicketTypeService.ToList();
                dgvCardTypes.Rows.Clear();
                foreach (TicketType ticketType in ticketTypeList)
                {
                    int rowIndex = dgvCardTypes.Rows.Add();
                    dgvCardTypes.Rows[rowIndex].Cells[0].Value = ticketType.RecId;
                    dgvCardTypes.Rows[rowIndex].Cells[1].Value = ticketType.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error(string.Format("加载卡类列表失败:{0}", ex.Message));
                return;
            }
        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            EditCardType();
        }

        #region 编辑卡类
        private void EditCardType()
        {
            if (dgvCardTypes.SelectedRows.Count != 0)
            {
                if (dgvCardTypes.SelectedRows[0].Index != -1)
                {
                    int recId = Convert.ToInt32(dgvCardTypes.SelectedRows[0].Cells[0].Value);
                    FrmEditTicketType edit = new FrmEditTicketType(recId);
                    DialogResult dr = edit.ShowDialog();
                    if (dr == DialogResult.OK)
                        LoadCardTypes();
                }
            }

        }
        #endregion

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            DeleteTicketType();
        }

        #region 删除卡类
        private void DeleteTicketType()
        {
            if (dgvCardTypes.SelectedRows.Count != 0)
            {
                if (dgvCardTypes.SelectedRows[0].Index != -1)
                {
                    int recId = Convert.ToInt32(dgvCardTypes.SelectedRows[0].Cells[0].Value);
                    if (recId == 1)
                    {
                        MessageBoxHelper.Info($"系统默认票类不可以删除!");
                        return;
                    }
                    DialogResult dr = MessageBoxHelper.Question($"确定要删除票类[{recId}],并且将所有关联的卡的票类设置成默认的票类吗?");
                    if (dr != DialogResult.OK) return;
                    try
                    {
                        TicketTypeService.Del(recId);
                        LoadCardTypes();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error($"删除票类失败:{ex.Message}");
                    }
                }
            }
        }
        #endregion

    }
}
