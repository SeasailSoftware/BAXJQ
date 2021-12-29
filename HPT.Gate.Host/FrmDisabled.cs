using HPT.Gate.Host.Base;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host
{
    public partial class FrmDisabled : WinBase
    {
        public string _MachineCode;
        public FrmDisabled()
        {
            InitializeComponent();

        }

        #region Instance

        private static FrmDisabled instance;
        private static readonly object lockHelper = new object();
        public static FrmDisabled Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new FrmDisabled();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        private void FrmDisabled_Load(object sender, EventArgs e)
        {
            tbMachineCode.Text = _MachineCode;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }
    }
}
