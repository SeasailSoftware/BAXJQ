using HPT.Gate.Host.Base;
using HPT.Gate.Host.Config;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Host.db
{
    public partial class FrmDbUpdate : WinBase
    {
        private string _FileName;
        public FrmDbUpdate(string fileName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _FileName = fileName;
        }

        private void FrmDbUpdate_Load(object sender, EventArgs e)
        {
            new Task(() => { CheckDbUpdate(); }).Start();
        }

        #region 检查数据库更新
        private void CheckDbUpdate()
        {
            /*
            ArrayList listSql = GetSqlList(_FileName);
            int i = 0;
            int count = listSql.Count;
            bool flag = true;
            using (OleDbHelper dbHelper = new OleDbHelper())
            {
                foreach (string sql in listSql)
                {
                    try
                    {
                        SystemService.ExecuteSqlFile(dbHelper, sql);
                        i++;
                        int value = i * 100 / count;
                        SetLabelValue(value);
                        SetProcessBarValue(value);
                        Thread.Sleep(300);
                    }
                    catch (Exception ex)
                    {
                        LogisTrac.WriteLog(ex);
                        MessageBox.Show("更新数据库文件失败:" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;
                */
        }

        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 获取sql 字符串长度

        private ArrayList GetSqlList(string fileName)
        {
            ///获取sql列表
            ArrayList listSql = new ArrayList();
            listSql.Add(string.Format("USE [{0}]", AppSettings.DBName));
            if (!File.Exists(fileName))
            {
                return listSql;
            }
            StreamReader rs = new StreamReader(fileName, System.Text.Encoding.Default);//注意编码
            string commandText = "";
            string varLine = "";
            while (rs.Peek() > -1)
            {
                varLine = rs.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (!varLine.ToUpper().Equals("GO"))
                {
                    commandText += varLine;
                    //commandText = commandText.Replace("@database_name=N'dbhr'", string.Format("@database_name=N'{0}'", dbname));
                    commandText += "\r\n";
                }
                else
                {
                    if (!commandText.Equals(""))
                    {
                        listSql.Add(commandText.Replace("{DBName}", AppSettings.DBName));
                        commandText = "";
                    }
                }
            }
            rs.Close();
            return listSql;
        }
        #endregion

        delegate void SetValueCallback(int value);
        private void SetLabelValue(int value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lbProgress.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetLabelValue);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                this.lbProgress.Text = value.ToString() + '%';
            }
        }
        private void SetProcessBarValue(int value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBar1.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetProcessBarValue);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                this.progressBar1.Value = value;
            }
        }
    }
}
