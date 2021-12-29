using HPT.Gate.Host.Properties;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host.Base
{
    public partial class FrmBase : JForm
    {
        public FrmBase()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.hpt;
        }
    }
}
