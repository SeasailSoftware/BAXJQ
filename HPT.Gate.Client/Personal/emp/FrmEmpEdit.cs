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
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HPT.Gate.Client
{
    public partial class FrmEmpEdit : JForm
    {

        private Image CurrentPhoto = null;
        private EmpInfo _CurrentEmp = null;
        private int _CurrentEmpId;
        public int _CurrentDeptId;
        private byte[] FingerPrintData;
        /// <summary>
        /// 修改员工的构造函数
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="empCode"></param>
        public FrmEmpEdit(int deptId, int empId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _CurrentEmpId = empId;
            InitParas();
            cbbDept.SelectedValue = deptId;
        }

        #region 初始化参数
        private void InitParas()
        {
            #region 初始化部门下拉列表
            try
            {
                ComboBoxHelper.FillDeptComboBox(cbbDept);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载部门列表失败:{ex.Message}");
                return;
            }
            #endregion

            #region 初始化票类
            try
            {
                ComboBoxHelper.FillTicketTypeCombobox(cbbTicketType);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载票类失败:{ex.Message}");
                return;
            }
            #endregion

        }

        #endregion



        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 编辑人员与卡信息

        private void EditEmpAndCard()
        {

            #region 人员信息

            _CurrentDeptId = Convert.ToInt32(cbbDept.SelectedValue);
            _CurrentEmp.DeptId = Convert.ToInt32(cbbDept.SelectedValue);
            string empCode = tbEmpCode.Text.Trim();
            if (string.IsNullOrEmpty(empCode))
            {
                MessageBoxHelper.Info("员工姓名不能为空!");
                return;
            }
            if (!empCode.Equals(_CurrentEmp.EmpCode))
            {
                if (EmpInfoService.CheckExists(empCode))
                {
                    MessageBoxHelper.Info($"人员编号[{empCode}]已存在!");
                    return;
                }
            }
            _CurrentEmp.EmpCode = empCode;
            _CurrentEmp.EmpName = tbEmpName.Text.Trim();
            if (_CurrentEmp.EmpName.Equals(string.Empty))
            {
                MessageBoxHelper.Info("员工姓名不能为空!");
                return;
            }
            _CurrentEmp.JoinDate = dtpBirthday.Text.Trim();
            _CurrentEmp.BirthDay = dtpBirthday.Text.Trim();
            _CurrentEmp.IdentityCard = tbIDCard.Text.Trim();
            _CurrentEmp.EnglishName = string.Empty;
            _CurrentEmp.Telephone = textBoxX4.Text.Trim();
            _CurrentEmp.Nation = tbNation.Text.Trim();
            _CurrentEmp.BornEarth = tbAddress.Text.Trim(); ;
            _CurrentEmp.Marrige = string.Empty;
            _CurrentEmp.Sex = cbbSex.Text.Trim();
            _CurrentEmp.JoinDate = dtpJoinDate.Text;
            _CurrentEmp.Duty = tbDuty.Text;
            _CurrentEmp.Rehire = cbbRehire.SelectedIndex;
            _CurrentEmp.HireTimes = (int)numHireTimes.Value;
            _CurrentEmp.Status = cbbEmpStatus.SelectedIndex;
            if (_CurrentEmp.Status == 0)
                _CurrentEmp.LeaveDate = string.Empty;
            else
                _CurrentEmp.LeaveDate = dtpLeaveDate.Text;
            _CurrentEmp.Photo = CurrentPhoto == null ? null : (Bitmap)CurrentPhoto;
            _CurrentEmp.FingerData1 = FingerPrintData;
            #endregion

            #region 公共属性
            if (cbbTicketType.SelectedIndex == -1)
            {
                MessageBoxHelper.Info("请先添加票类!");
                return;
            }
            _CurrentEmp.TicketType = (int)cbbTicketType.SelectedValue;
            _CurrentEmp.BeginDate = dtpBegin.Text;
            _CurrentEmp.EndDate = dtpEnd.Text;

            #endregion

            #region IC卡

            string icCardNo = tbICCardNo.Text.Trim();
            if (!string.IsNullOrEmpty(icCardNo))
            {
                if (!icCardNo.Equals(_CurrentEmp.ICCardNo))
                {
                    if (!StringValidate.IsICIDCardNo(icCardNo))
                    {
                        MessageBoxHelper.Info($"IC/ID卡格式非法:必须为16进制字符串且长度等于8");
                        return;
                    }
                    if (EmpInfoService.CheckICCardExists(icCardNo))
                    {
                        MessageBoxHelper.Info($"IC/ID卡号[{icCardNo}]已经存在!");
                        return;
                    }
                }
            }
            _CurrentEmp.ICCardNo = icCardNo;

            #endregion

            #region 身份证序列号

            string idSerial = tbIDSerial.Text.Trim();
            if (!string.IsNullOrEmpty(idSerial))
            {
                if (!idSerial.Equals(_CurrentEmp.IDSerial))
                {
                    if (!StringValidate.IsIDSerial(idSerial))
                    {
                        MessageBoxHelper.Info($"身份证序列号格式非法:必须为16进制字符串且长度等于16");
                        return;
                    }
                    if (EmpInfoService.CheckIDSerialExists(idSerial))
                    {
                        MessageBoxHelper.Info($"身份证序列号[{idSerial}]已经存在!");
                        return;
                    }
                }
            }
            _CurrentEmp.IDSerial = idSerial;

            #endregion

            #region 身份证号码

            string idCardNo = tbIDCardNo.Text.Trim();
            if (!string.IsNullOrEmpty(idCardNo))
            {
                if (!idCardNo.Equals(_CurrentEmp.IDCardNo))
                {
                    if (!StringValidate.IsIDCardNo(idCardNo))
                    {
                        MessageBoxHelper.Info($"身份证号码格式非法:不是有效的身份证号码!");
                        return;
                    }
                    if (EmpInfoService.CheckIDCardNoExists(idCardNo))
                    {
                        MessageBoxHelper.Info($"身份证号码[{idCardNo}]已经存在!");
                        return;
                    }
                }
            }
            _CurrentEmp.IDCardNo = idCardNo;
            #endregion

            #region 保存人员信息
            try
            {
                EmpInfoService.Update(_CurrentEmp);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"保存人员信息失败:{ex.Message}");
                return;
            }
            #endregion

        }
        #endregion



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
            FrmCaptureFace capture = new FrmCaptureFace();
            DialogResult dr = capture.ShowDialog();
            if (dr != DialogResult.OK) return;
            CurrentPhoto = capture.CurrentPhoto;
            picPhoto.Image = ImageHelper.KiResizeImage((Bitmap)CurrentPhoto, picPhoto.Width, picPhoto.Height);
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {


        }

        private void buttonX6_Click(object sender, EventArgs e)
        {

        }

        private void EmpForm_Load(object sender, EventArgs e)
        {
            ///加载发卡器
            new Thread(() => CardReaderConfig.InitCardReader()) { IsBackground = true }.Start();
            LoadEmpInfo();
        }

        #region 加载人员信息
        private void LoadEmpInfo()
        {
            try
            {
                _CurrentEmp = EmpInfoService.GetByEmpId(_CurrentEmpId);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载人员信息失败:{ex.Message}");
                return;
            }
            if (_CurrentEmp != null)
            {
                tbEmpCode.Text = _CurrentEmp.EmpCode;
                tbEmpName.Text = _CurrentEmp.EmpName;
                //textBoxX5.Text = emp.EnglishName;
                cbbSex.Text = _CurrentEmp.Sex;
                tbNation.Text = _CurrentEmp.Nation;
                tbAddress.Text = _CurrentEmp.BornEarth;
                //textBoxX8.Text = emp.Marrige;
                dtpBirthday.Text = _CurrentEmp.BirthDay;
                //dateTimePicker2.Text = emp.JoinDate;
                tbIDCard.Text = _CurrentEmp.IdentityCard;
                textBoxX4.Text = _CurrentEmp.Telephone;
                CurrentPhoto = _CurrentEmp.Photo;
                if (_CurrentEmp.Photo != null)
                {
                    Bitmap bmp = new Bitmap(_CurrentEmp.Photo);
                    bmp = ImageHelper.KiResizeImage(bmp, picPhoto.Width, picPhoto.Height);
                    picPhoto.Image = bmp;
                }
                else
                {
                    picPhoto.Image = null;
                }

                cbbTicketType.SelectedValue = _CurrentEmp.TicketType;
                dtpBegin.Text = _CurrentEmp.BeginDate;
                dtpEnd.Text = _CurrentEmp.EndDate;
                //IC卡
                tbICCardNo.Text = _CurrentEmp.ICCardNo;
                btCancelICCard.Enabled = !tbICCardNo.Text.Equals(string.Empty);
                btCancelICCard.Enabled = !string.IsNullOrWhiteSpace(_CurrentEmp.ICCardNo);
                //身份证序列号
                tbIDSerial.Text = _CurrentEmp.IDSerial;
                btCancelIDSerial.Enabled = !string.IsNullOrWhiteSpace(_CurrentEmp.IDSerial);
                //身份证号码
                tbIDCardNo.Text = _CurrentEmp.IDCardNo;
                btCancelIDCardNo.Enabled = !string.IsNullOrWhiteSpace(_CurrentEmp.IDCardNo);

                dtpJoinDate.Text = _CurrentEmp.JoinDate;
                tbDuty.Text = _CurrentEmp.Duty;
                cbbRehire.SelectedIndex = _CurrentEmp.Rehire;
                numHireTimes.Value = _CurrentEmp.HireTimes;
                cbbEmpStatus.SelectedIndex = _CurrentEmp.Status;
                if (_CurrentEmp.Status == 0)
                {
                    lbLeaveDate.Enabled = false;
                    dtpLeaveDate.Text = "1900-01-01";
                    dtpLeaveDate.Enabled = false;
                }
                else
                {
                    btRehire.Visible = true;
                    cbbEmpStatus.Enabled = false;
                    lbLeaveDate.Enabled = true;
                    dtpLeaveDate.Enabled = true;
                    dtpLeaveDate.Text = _CurrentEmp.LeaveDate;
                }
                label_faceStatus.Text = _CurrentEmp.FaceStatus;
                FingerPrintData = _CurrentEmp.FingerData1;
                tbFPData.Text = _CurrentEmp.FingerData1 == null ? "" : ArrayHelper.ToHexString(_CurrentEmp.FingerData1);
            }

        }
        #endregion


        #region 展示人员信息
        private void ShowEmpInfo()
        {
            if (_CurrentEmp != null)
            {
                tbEmpCode.Text = _CurrentEmp.EmpCode;
                tbEmpCode.ReadOnly = true;
                tbEmpName.Text = _CurrentEmp.EmpName;
                //textBoxX5.Text = emp.EnglishName;
                cbbSex.Text = _CurrentEmp.Sex;
                tbNation.Text = _CurrentEmp.Nation;
                tbAddress.Text = _CurrentEmp.BornEarth;
                //textBoxX8.Text = emp.Marrige;
                dtpBirthday.Text = _CurrentEmp.BirthDay;
                //dateTimePicker2.Text = emp.JoinDate;
                tbIDCard.Text = _CurrentEmp.IdentityCard;
                textBoxX4.Text = _CurrentEmp.Telephone;
                if (_CurrentEmp.Photo != null)
                {
                    Bitmap bmp = new Bitmap(_CurrentEmp.Photo);
                    bmp = ImageHelper.KiResizeImage(bmp, picPhoto.Width, picPhoto.Height);
                    picPhoto.Image = bmp;
                }
                else
                {
                    picPhoto.Image = null;
                }
            }
        }
        #endregion

        #region 展示IC卡信息
        private void ShowICCardInfo()
        {

        }

        #endregion

        #region 展示身份证序列号信息
        private void ShowIDSerialInfo()
        {

        }

        #endregion


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
                    //Bitmap bmp = new Bitmap("zp.bmp");
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

        private void btCancelIDSerial_Click(object sender, EventArgs e)
        {
            tbIDSerial.Text = string.Empty;
            //CancelIDSerial();
        }

        private void btCancelIDCard_Click(object sender, EventArgs e)
        {
            tbIDCardNo.Text = string.Empty;
            //CancelIDCardNo();
        }

        private void buttonX10_Click_1(object sender, EventArgs e)
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

        private void buttonX9_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonX11_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonX12_Click_1(object sender, EventArgs e)
        {
            FrmIDSerialReader idSerialReader = new FrmIDSerialReader();
            idSerialReader.ShowDialog();
            new Thread(() => CardReaderConfig.InitCardReader()) { IsBackground = true }.Start();
        }

        private void buttonX13_Click_1(object sender, EventArgs e)
        {

        }







        #region 获取指纹编号
        private int GetFingerId(List<DataAccess.Entity.FingerPrint> fingerList)
        {
            int fingerId = 0;
            if (fingerList.Count == 0)
                return 1;
            for (int i = 1; i <= fingerList.Max(c => c.FingerId); i++)
            {
                if (fingerList.All(c => c.FingerId != i))
                {
                    fingerId = i;
                    break;
                }
            }
            if (fingerId == 0)
                fingerId = fingerList.Max(s => s.FingerId) + 1;
            return fingerId;
        }
        #endregion


        private void cbbEmpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbEmpStatus.SelectedIndex == 1)
            {
                lbLeaveDate.Enabled = true;
                dtpLeaveDate.Enabled = true;
            }
            else
            {
                dtpLeaveDate.Enabled = false;
                lbLeaveDate.Enabled = false;
            }

        }

        private void btRehire_Click(object sender, EventArgs e)
        {
            btRehire.Enabled = false;
            cbbRehire.SelectedIndex = 1;
            numHireTimes.Value += 1;
            cbbEmpStatus.Enabled = true;
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

        private void btCancelICCard_Click(object sender, EventArgs e)
        {
            tbICCardNo.Text = string.Empty;
            //CancelICCard();
        }

        #region 注销IC/ID卡
        private void CancelICCard()
        {
            try
            {
                EmpInfoService.CancelICCard(_CurrentEmp.EmpId);
                tbICCardNo.Text = string.Empty;
                _CurrentEmp.ICCardNo = string.Empty;
                MessageBoxHelper.Info("IC/ID卡注销成功!");
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"注销IC/ID卡失败:{ex.Message}");
            }
        }
        #endregion

        #region 注销I身份证序列号
        private void CancelIDSerial()
        {
            try
            {
                EmpInfoService.CancelIDSerial(_CurrentEmp.EmpId);
                tbIDSerial.Text = string.Empty;
                _CurrentEmp.IDSerial = string.Empty;
                MessageBoxHelper.Info("身份证序列号注销成功!");
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"注销身份证序列号失败:{ex.Message}");
            }
        }
        #endregion

        #region 注销I身份证序列号
        private void CancelIDCardNo()
        {
            try
            {
                EmpInfoService.CancelIDCardNo(_CurrentEmp.EmpId);
                tbIDCardNo.Text = string.Empty;
                _CurrentEmp.IDCardNo = string.Empty;
                MessageBoxHelper.Info("身份证号码注销成功!");
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"注销身份证号码失败:{ex.Message}");
            }
        }
        #endregion

        private void buttonX7_Click_1(object sender, EventArgs e)
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

        private void buttonX11_Click(object sender, EventArgs e)
        {

        }

        private void buttonX13_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EditEmpAndCard();
        }

        private void FrmEmpEdit_Shown(object sender, EventArgs e)
        {
            picPhoto.BringToFront();
        }

        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            tbFPData.Clear();
            FingerPrintData = null;
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
                fingerList.Remove(fingerList.FirstOrDefault(p => p.EmpId == _CurrentEmp.EmpId));
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
    }
}
