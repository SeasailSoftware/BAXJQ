using hpt.gate.CardReader;
using hpt.gate.config;
using HPT.Face.Client.Personal.emp;
using HPT.Gate.Client.emp;
using HPT.Gate.Client.Personal.emp;
using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.ZKFP;
using HPT.Joey.Lib.Utils;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HPT.Gate.Client
{
    public partial class FrmEmpAdd : JForm
    {
        private Image CurrentPhoto = null;
        private byte[] FingerPrintData;
        #region 截图
        private bool isDrag = false;                                                    //是否可以剪切图片
        private Rectangle theRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));                //实例化Rectangle类
        private Point startPoint, oldPoint;                                             //定义Point类
        private Graphics ig;
        #endregion

        private int _CurrentEmpId;
        private string imagePath = string.Empty;

        public FrmEmpAdd(int deptId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ComboBoxHelper.FillDeptComboBox(cbbDept);
            cbbDept.SelectedValue = deptId;
            tbEmpCode.Text = EmpInfoService.GetUseAbleEmpCode();
            cbbSex.SelectedIndex = 0;
            _CurrentEmpId = EmpInfoService.GetNextEmpId();
            cbbEmpStatus.SelectedIndex = 0;
            cbbRehire.SelectedIndex = 0;
            dtpJoinDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }


        /// <summary>
        /// 初始化时间组
        /// </summary>
        private void InitTimeGroup()
        {


        }

        /// <summary>
        /// 初始化发卡器
        /// </summary>


        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }


        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 添加人员信息
        private void AddEmp()
        {
            EmpInfo emp = new EmpInfo();
            List<HPT.Gate.DataAccess.Entity.FingerPrint> fingerList = new List<HPT.Gate.DataAccess.Entity.FingerPrint>();
            #region 人员基本信息

            emp.DeptId = Convert.ToInt32(cbbDept.SelectedValue);
            emp.EmpCode = tbEmpCode.Text.Trim();

            if (!StringValidate.IsEmpCode(emp.EmpCode))
            {
                MessageBoxHelper.Info("人员编号格式有误:必须为16进制字符串且长度不能超过8!");
                return;
            }
            if (EmpInfoService.CheckExists(emp.EmpCode))
            {
                MessageBoxHelper.Info("人员编号已存在!");
                return;
            }
            emp.EmpName = tbEmpName.Text.Trim();
            if (emp.EmpName.Equals(string.Empty))
            {
                MessageBoxHelper.Info("员工姓名不能为空!");
                return;
            }
            emp.JoinDate = dtpBirthday.Text.Trim();
            emp.BirthDay = dtpBirthday.Text.Trim();
            emp.IdentityCard = tbIDCard.Text.Trim();
            emp.EnglishName = string.Empty;
            emp.Telephone = textBoxX4.Text.Trim();
            emp.Nation = tbNation.Text.Trim();
            emp.BornEarth = tbAddress.Text.Trim(); ;
            emp.Marrige = string.Empty;
            emp.Sex = cbbSex.Text.Trim();
            emp.JoinDate = dtpJoinDate.Text;
            emp.Duty = tbDuty.Text;
            emp.Rehire = 0;
            emp.HireTimes = 0;
            emp.Status = 0;
            emp.LeaveDate = string.Empty;
            emp.Photo = CurrentPhoto == null ? null : (Bitmap)CurrentPhoto;

            #endregion

            #region 公共属性
            if (cbbTicketType.SelectedIndex == -1)
            {
                MessageBoxHelper.Info("请先添加票类!");
                return;
            }
            emp.TicketType = (int)cbbTicketType.SelectedValue;
            emp.BeginDate = dtpBegin.Text;
            emp.EndDate = dtpEnd.Text;

            #endregion

            emp.ICCardNo = tbICCardNo.Text.Trim();
            emp.IDSerial = tbIDSerial.Text.Trim();
            emp.IDCardNo = tbIDCardNo.Text.Trim();
            if (!emp.ICCardNo.Equals(string.Empty))
            {
                if (!StringValidate.IsICIDCardNo(emp.ICCardNo))
                {
                    MessageBoxHelper.Info($"IC/ID卡号格式非法:只能为16进制字符串且长度为8位");
                    return;
                }
                if (EmpInfoService.CheckICCardExists(emp.ICCardNo))
                {
                    MessageBoxHelper.Info($"IC/ID卡号[{emp.ICCardNo}]已经存在!");
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(emp.IDSerial))
            {
                if (!StringValidate.IsIDSerial(emp.IDSerial))
                {
                    MessageBoxHelper.Info($"身份证序列号格式非法:只能为16进制字符串且长度为16位");
                    return;
                }
                if (EmpInfoService.CheckIDSerialExists(emp.IDSerial))
                {
                    MessageBoxHelper.Info($"身份证序列号[{emp.IDSerial}]已经存在!");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(emp.IDCardNo))
            {
                if (!StringValidate.IsIDCardNo(emp.IDCardNo))
                {
                    MessageBoxHelper.Info($"身份证号码格式非法!");
                    return;
                }
                if (EmpInfoService.CheckIDCardNoExists(emp.IDCardNo))
                {
                    MessageBoxHelper.Info($"身份证号码[{emp.IDCardNo}]已经存在!");
                    return;
                }
            }

            //指纹一数据
            emp.FingerData1 = FingerPrintData;
            //指纹二数据
            emp.FingerData2 = null;

            //人脸数据
            emp.FaceData = null;

            #region 保存人员信息
            try
            {
                EmpInfoService.Insert(emp);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"保存人员信息失败:{ex.Message}");
                return;
            }
            #endregion

            DialogResult dr = MessageBoxHelper.Question("添加人员信息成功!是否继续添加？");
            if (dr != DialogResult.OK)
                DialogResult = DialogResult.OK;
            ClearDesplay();
        }
        #endregion



        /// <summary>
        /// 清除显示
        /// </summary>
        private void ClearDesplay()
        {
            _CurrentEmpId = EmpInfoService.GetNextEmpId();
            tbEmpCode.Text = EmpInfoService.GetUseAbleEmpCode();
            tbEmpName.Text = "";
            //textBoxX5.Text = "";
            cbbSex.SelectedIndex = 0;
            tbAddress.Text = "";
            tbNation.Text = "";
            //textBoxX8.Text = "";
            tbIDCard.Text = "";
            textBoxX4.Text = "";
            //dtpBirthday.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tbICCardNo.Text = string.Empty;
            picPhoto.Image = null;
        }




        /// <summary>
        /// 判断一个文件是否图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool IsPicture(string filePath)//filePath是文件的完整路径   
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                byte[] b = new byte[2];
                buffer = reader.ReadByte();
                b[0] = buffer;
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                b[1] = buffer;
                fileClass += buffer.ToString();


                reader.Close();
                fs.Close();
                if (fileClass == "255216" || fileClass == "13780" || fileClass == "6677")//255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar   
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void labelX15_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            FrmCapturePictureFace capture = new FrmCapturePictureFace();
            DialogResult dr = capture.ShowDialog();
            if (dr != DialogResult.OK) return;
            CurrentPhoto = capture.FaceImage;
            picPhoto.Image = ImageHelper.KiResizeImage((Bitmap)CurrentPhoto, picPhoto.Width, picPhoto.Height);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {


        }

        private void buttonX6_Click(object sender, EventArgs e)
        {

        }

        private void EmpForm_Load(object sender, EventArgs e)
        {
            dtpEnd.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
            ///加载发卡器
            new Thread(() => CardReaderConfig.InitCardReader()) { IsBackground = true }.Start();
            LoadCardType();
        }

        #region 加载卡类型
        private void LoadCardType()
        {
            try
            {
                ComboBoxHelper.FillTicketTypeCombobox(cbbTicketType);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载卡类失败:{ex.Message}");
                return;
            }
        }
        #endregion



        private void buttonX7_Click(object sender, EventArgs e)
        {
            tbICCardNo.Text = string.Empty;
            if (CardReaderConfig.ICCardEnabled)
            {
                switch (CardReaderConfig.ICCardReaderType)
                {
                    case 1:
                        tbICCardNo.Text = ICCardReader.ReadCardNo();
                        break;
                    case 2:
                        if (CardReaderConfig.IC_IDSerialPort.Trim().Equals(string.Empty))
                        {
                            MessageBoxHelper.Info("身份证序列号发卡器串口有误!");
                            return;
                        }
                        string cardNo = IDSerialReader.ReadIDSerialNo();
                        if (cardNo.Equals(string.Empty))
                        {
                            MessageBoxHelper.Info("没有检测到IC卡!");
                            return;
                        }
                        if (cardNo.Length == 16)
                        {
                            tbICCardNo.Text = cardNo.Substring(6, 8);
                            return;
                        }
                        if (cardNo.Length == 10)
                        {
                            tbICCardNo.Text = cardNo.Substring(0, 8);
                            return;
                        }
                        MessageBoxHelper.Info("卡号长度不对!请确认刷的是否为IC卡!");
                        break;
                    case 3:
                        USBCardReader usbCardReader = new USBCardReader(CardReaderConfig.USBType);
                        DialogResult dr = usbCardReader.ShowDialog();
                        if (dr != DialogResult.OK) return;
                        tbICCardNo.Text = usbCardReader._CardNo;
                        break;
                }
            }
            if (CardReaderConfig.ICAndIDSerialEnabled)
            {
                string cardNo = IDSerialReader.ReadIDSerialNo();
                if (cardNo.Equals(string.Empty))
                {
                    MessageBoxHelper.Info("没有检测到IC卡!");
                    return;
                }
                if (cardNo.Length == 16)
                {
                    tbICCardNo.Text = cardNo.Substring(6, 8);
                    return;
                }
                if (cardNo.Length == 10)
                {
                    tbICCardNo.Text = cardNo.Substring(0, 8);
                    return;
                }
                MessageBoxHelper.Info("卡号长度不对!请确认刷的是否为IC卡!");
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        #region 读取身份证信息
        private void ReadIDCardInfo()
        {
            try
            {
                if (IDCardReader.IDReadCard())
                {
                    tbEmpName.Text = IDCardReader.Name;
                    tbIDCard.Text = IDCardReader.CardNo;
                    tbAddress.Text = IDCardReader.Address;
                    cbbSex.Text = IDCardReader.Sex;
                    tbNation.Text = IDCardReader.Nation;
                    dtpBirthday.Text = IDCardReader.BirthDay;
                    picPhoto.ImageLocation = Application.StartupPath + "\\zp.bmp";

                }
                else
                {
                    tbEmpName.Text = string.Empty;
                    tbIDCard.Text = string.Empty;
                    tbAddress.Text = string.Empty;
                    cbbSex.Text = IDCardReader.Sex;
                    tbNation.Text = string.Empty;
                    dtpBirthday.Text = string.Empty;
                    picPhoto.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("读取身份证号码失败:" + ex.Message);
                return;
            }
        }
        #endregion

        private void cbbDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmEmpAdd_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void buttonX9_Click_1(object sender, EventArgs e)
        {
        }

        private void buttonX12_Click_1(object sender, EventArgs e)
        {
            FrmIDSerialReader idSerialReader = new FrmIDSerialReader();
            idSerialReader.ShowDialog();
            new Thread(() => CardReaderConfig.InitCardReader()) { IsBackground = true }.Start();
        }


        private void btAddFP1_Click(object sender, EventArgs e)
        {

        }


        private void btDelFP1_Click(object sender, EventArgs e)
        {

        }

        private void btDelFP2_Click(object sender, EventArgs e)
        {

        }

        private void btAddFP2_Click(object sender, EventArgs e)
        {

        }

        private void cbbEmpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbbEmpStatus.SelectedIndex;
            if (index == 1)
            {
                lbLeaveDate.Enabled = true;
                lbLeaveDate.ForeColor = Color.Red;
                dtpLeaveDate.Enabled = true;
            }
            else
            {
                lbLeaveDate.Enabled = false;
                lbLeaveDate.ForeColor = Color.Black;
                dtpLeaveDate.Enabled = false;
            }
        }

        private void buttonX8_Click_1(object sender, EventArgs e)
        {
            CurrentPhoto = null;
            picPhoto.Image = null;
        }

        private void buttonX9_Click_2(object sender, EventArgs e)
        {
            if (!CardReaderConfig.ICAndIDSerialEnabled)
            {
                MessageBoxHelper.Info("发卡器配置有误,请重新配置!");
                return;
            }
            tbIDSerial.Text = string.Empty;
            string cardNo = IDSerialReader.ReadIDSerialNo();
            if (cardNo.Equals(string.Empty))
            {
                MessageBoxHelper.Info("没有检测到身份证!");
                return;
            }
            if (cardNo.Length == 16)
            {
                tbIDSerial.Text = cardNo;
                return;
            }
            MessageBoxHelper.Info("卡号长度不对!请确认刷的是否为身份证序列号!");
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            tbIDCard.Text = string.Empty;
            if (IDCardReader.IDReadCard())
                tbIDCard.Text = IDCardReader.CardNo;
        }

        private void buttonX11_Click_1(object sender, EventArgs e)
        {
            if (IDCardReader.IDReadCard())
            {
                tbEmpName.Text = IDCardReader.Name;
                tbIDCard.Text = IDCardReader.CardNo;
                tbIDCardNo.Text = IDCardReader.CardNo;
                tbAddress.Text = IDCardReader.Address;
                cbbSex.Text = IDCardReader.Sex;
                tbNation.Text = IDCardReader.Nation;
                dtpBirthday.Text = IDCardReader.BirthDay;
                picPhoto.ImageLocation = $@"{Application.StartupPath}\zp.bmp";
            }
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {

        }



        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AddEmp();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();                                     //为当前窗体创建Graphics类
            if (isDrag)                                                     //如果鼠示已按下
            {
                //绘制一个矩形框
                g.DrawRectangle(new Pen(Color.Black, 1), startPoint.X, startPoint.Y, e.X - startPoint.X, e.Y - startPoint.Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            ig = picPhoto.CreateGraphics();                              //创建pictureBox1控件的Graphics类
            //绘制矩形框
            ig.DrawRectangle(new Pen(Color.Black, 1), startPoint.X, startPoint.Y, e.X - startPoint.X, e.Y - startPoint.Y);
            theRectangle = new Rectangle(startPoint.X, startPoint.Y, e.X - startPoint.X, e.Y - startPoint.Y);   //获得矩形框的区域

        }

        private void FrmEmpAdd_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Graphics graphics = this.CreateGraphics();                      //为当前窗体创建Graphics类
                Bitmap bitmap = new Bitmap(picPhoto.Image);                  //实例化Bitmap类
                Bitmap cloneBitmap = bitmap.Clone(theRectangle, PixelFormat.DontCare);//通过剪切图片的大小实例化Bitmap类
                graphics.DrawImage(cloneBitmap, e.X, e.Y);                      //绘制剪切的图片
                Graphics g = picPhoto.CreateGraphics();
                SolidBrush myBrush = new SolidBrush(Color.White);               //定义画刷
                g.FillRectangle(myBrush, theRectangle);                         //绘制剪切后的图片
            }
            catch { }
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            FrmCaptureFace capture = new FrmCaptureFace();
            DialogResult dr = capture.ShowDialog();
            if (dr != DialogResult.OK) return;
            CurrentPhoto = capture.CurrentPhoto;
            picPhoto.Image = ImageHelper.KiResizeImage((Bitmap)CurrentPhoto, picPhoto.Width, picPhoto.Height);
        }

        private void tbEmpName_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFPData.Text))
            {
                MessageBoxHelper.Info("请先删除指纹!");
                return;
            }
            try
            {
                List<DataAccess.Entity.FingerPrint> fingerList = EmpInfoService.GetAllFingerPrints();
                if (fingerList.Count >= 2000)
                {
                    MessageBoxHelper.Info("指纹个数已经超过2000!");
                    return;
                }
                List<HPT.Gate.ZKFP.FingerPrint> fps = new List<ZKFP.FingerPrint>();
                fingerList.ForEach(p => fps.Add(new ZKFP.FingerPrint() { FPid = p.FingerId, FPData = p.FingerData }));
                if (!ZKFPHelper.Instance.Init(fps, out string msg, true))
                {
                    MessageBox.Show($"初始化指纹采集器失败:{msg}", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int fingerId = fingerList.Any() ? fingerList.Max(p => p.FingerId) + 1 : 1;
                FrmAddFingerPrint addFingerPrint = new FrmAddFingerPrint(fingerId);
                DialogResult dr = addFingerPrint.ShowDialog();
                if (dr != DialogResult.OK) return;
                FingerPrintData = addFingerPrint._FingerData;
                tbFPData.Text = ArrayHelper.ToHexString(addFingerPrint._FingerData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化指纹仪失败:{ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            tbFPData.Clear();
            FingerPrintData = null;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //如果开始绘制，则开始记录鼠标位置
                if ((isDrag = !isDrag) == true)
                {
                    startPoint = new Point(e.X, e.Y);                               //记录鼠标的当前位置
                    oldPoint = new Point(e.X, e.Y);
                }
            }
        }
    }
}
