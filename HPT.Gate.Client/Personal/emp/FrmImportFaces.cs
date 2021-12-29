using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Joey.Lib.Controls;
using HPT.Joey.Lib.Utils;
using Joey.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Client.Personal.emp
{
    public partial class FrmImportFaces : JWindow
    {
        private string CurrentPath;
        public FrmImportFaces()
        {
            InitializeComponent();
        }

        private void FrmImportFaces_Load(object sender, EventArgs e)
        {

        }








        private void UpdatePhotos()
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    btnOpen.Enabled = false;
                }));
                int index = 0;
                int count = dgv.Rows.Count;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    this.Invoke(new Action(() => { dgv.CurrentCell = row.Cells[0]; }));
                    string content = $"正在处理文件[{row.Cells[1].Value.ToString()}]";
                    index = index + 1;
                    int value = index * 100 / count;
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        string empcode = row.Cells[1].Value.ToString();
                        FaceToEmps face = new FaceToEmps();
                        face.EmpCode = empcode;
                        face.Photo = Image.FromFile(empcode);
                        if (face == null) continue;

                        try
                        {
                            string msg;
                            EmpInfoService.InsertPhoto(face, out msg);
                            this.Invoke(new Action(() => { row.Cells[2].Value = msg; }));
                            /*
                            HPTFace service = new HPTFaceSDK();
                            if (service.CheckFace(device.IPAddress, device.Password, face.Photo, out msg))
                            {
                                EmpInfoService.InsertPhoto(face, out msg);
                                this.Invoke(new Action(() => { row.Cells[2].Value = msg; }));
                            }
                            else
                                this.Invoke(new Action(() => { row.Cells[2].Value = "图片不符合规则!"; }));
                                */
                        }
                        catch (Exception ex)
                        {
                            this.Invoke(new Action(() => { row.Cells[2].Value = $"导入失败:{ex.Message}"; }));
                        }
                    }
                }
                this.Invoke(new Action(() =>
                {
                    btnOpen.Enabled = true;
                }));
            });
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new FolderBrowserDialog();
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr != DialogResult.OK) return;
            CurrentPath = openFileDialog.SelectedPath;
            string path = CurrentPath;
            if (dgv.Rows.Count > 0)
                dgv.Rows.Clear();
            JProgressHelper process = new JProgressHelper();
            process.MessageInfo = "正在处理中,请稍后...";
            process.BackgroundWork = ImportPhoto;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();

            /*
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    btnOpen.Enabled = false;
                    btnOK.Enabled = false;
                    btnCancel.Enabled = false;
                    loading.ShowLoad();
                }));
                DirectoryInfo folder = new DirectoryInfo(path);
                int index = 0;
                int count = folder.GetFiles().Length;
                DataTable dt = new DataTable("Images");
                DataColumn dc = null;
                dc = dt.Columns.Add("Column1", Type.GetType("System.String"));
                dc = dt.Columns.Add("Column2", Type.GetType("System.String"));
                dc = dt.Columns.Add("Column3", Type.GetType("System.String"));
                foreach (FileInfo file in folder.GetFiles())
                {
                    index++;
                    string fileName = $@"{folder.FullName}\{file.Name}";
                    if (!ImageHelper.IsImage(fileName)) continue;
                    DataRow newRow = dt.NewRow();
                    newRow["Column1"] = false;
                    newRow["Column2"] = fileName;
                    newRow["Column3"] = "";
                    dt.Rows.Add(newRow);
                }
                this.Invoke(new Action(() =>
                {
                    dgv.DataSource = dt;
                }));
                if (dt.Rows.Count == 0)
                {
                    MessageBoxHelper.Info("所选文件夹里没有可用的人脸图片!");
                }
                else
                {

                }
                this.Invoke(new Action(() =>
                {
                    btnOpen.Enabled = true;
                    btnOK.Enabled = true;
                    btnCancel.Enabled = true;
                    loading.HideLoad();
                }));
            });
            */
        }

        private void ImportPhoto(Action<int> progress, Action<string> showMsg)
        {
            DataTable dt = new DataTable("Images");
            DataColumn dc = null;
            dc = dt.Columns.Add("Column1", Type.GetType("System.String"));
            dc = dt.Columns.Add("Column2", Type.GetType("System.String"));
            dc = dt.Columns.Add("Column3", Type.GetType("System.String"));
            DirectoryInfo folder = new DirectoryInfo(CurrentPath);
            int index = 0;
            int count = folder.GetFiles().Length;
            foreach (FileInfo file in folder.GetFiles())
            {
                index++;
                string msg;
                DataRow newRow = dt.NewRow();
                newRow["Column1"] = false;
                string fileName = $@"{folder.FullName}\{file.Name}";
                newRow["Column2"] = fileName;
                showMsg($@"正在处理[{file.Name}],{index}/{count}");
                progress(index * 100 / count);
                if (!ImageHelper.IsImage(fileName))
                {
                    msg = "不是有效的图片!";
                    dt.Rows.Add(newRow);
                    continue;
                }
                string empcode = fileName;
                using (Image img = Image.FromFile(empcode))
                {
                    FaceToEmps face = new FaceToEmps();
                    face.EmpCode = empcode;
                    face.Photo = img;
                    if (face == null)
                    {
                        msg = "不是有效的图片!";
                        dt.Rows.Add(newRow);
                        continue;
                    }
                    try
                    {
                        EmpInfoService.InsertPhoto(face, out msg);
                        newRow["Column3"] = msg;
                        dt.Rows.Add(newRow);
                    }
                    catch (Exception ex)
                    {
                        newRow["Column3"] = ex.Message;
                        dt.Rows.Add(newRow);
                    }
                }
            }
            this.Invoke(new Action(() => { dgv.DataSource = dt; }));
        }

        private void process_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            MessageBoxHelper.Info($"处理完毕!");
        }

        private void FrmImportFaces_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnOpen.Enabled)
            {
                e.Cancel = true;
                return;
            }
        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
