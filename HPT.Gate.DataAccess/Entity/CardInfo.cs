using System.Data.SqlClient;
using System.Data;
namespace HPT.Gate.DataAccess.Entity
{
    public class CardInfo
    {
        #region Var

        public int TicketType { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        private int _DevId;

        public int DevId
        {
            get { return _DevId; }
            set { _DevId = value; }
        }
        /// <summary>
        /// 卡编号
        /// </summary>
        private int _CardId;

        public int CardId
        {
            get { return _CardId; }
            set { _CardId = value; }
        }

        /// <summary>
        /// 卡类型：1为I卡，2为身份证序列号，3为身份号码
        /// </summary>
        private int _Type;

        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        /// <summary>
        /// 卡号
        /// </summary>
        private string _CardNo;

        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        private int _EmpId;

        public int EmpId
        {
            get { return _EmpId; }
            set { _EmpId = value; }
        }

        /// <summary>
        /// 黑白名单
        /// </summary>
        private int _BlackName;

        public int BlackName
        {
            get { return _BlackName; }
            set { _BlackName = value; }
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        private int _CardType;

        public int CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        /// <summary>
        /// 卡编号
        /// </summary>
        private string _PhotoName;

        public string PhotoName
        {
            get { return _PhotoName; }
            set { _PhotoName = value; }
        }
        /// <summary>
        /// 进门权限
        /// </summary>
        private int _InRight;

        public int InRight
        {
            get { return _InRight; }
            set { _InRight = value; }
        }
        /// <summary>
        /// 出门权限
        /// </summary>
        private int _OutRight;

        public int OutRight
        {
            get { return _OutRight; }
            set { _OutRight = value; }
        }
        /// <summary>
        /// 语音段号码
        /// </summary>
        private int _VoiceNo;

        public int VoiceNo
        {
            get { return _VoiceNo; }
            set { _VoiceNo = value; }
        }

        /// <summary>
        /// 是否显示照片的标志
        /// </summary>
        private int _Photo;

        public int Photo
        {
            get { return _Photo; }
            set { _Photo = value; }
        }
        /// <summary>
        /// 节假日时间组号
        /// </summary>
        private int _VacationNo;

        public int VacationNo
        {
            get { return _VacationNo; }
            set { _VacationNo = value; }
        }

        /// <summary>
        /// 星期时间组进门组号
        /// </summary>
        private int _InTimeGroupNo;

        public int InTimeGroupNo
        {
            get { return _InTimeGroupNo; }
            set { _InTimeGroupNo = value; }
        }
        /// <summary>
        /// 星期时间组出门组号
        /// </summary>
        private int _OutTimeGroupNo;

        public int OutTimeGroupNo
        {
            get { return _OutTimeGroupNo; }
            set { _OutTimeGroupNo = value; }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        private string _BeginDate;

        public string BeginDate
        {
            get { return _BeginDate; }
            set { _BeginDate = value; }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        private string _EndDate;

        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        /// <summary>
        /// 显示屏显示类型(0为系统，1为自定义)
        /// </summary>
        private int _DisplayType1;

        public int DisplayType1
        {
            get { return _DisplayType1; }
            set { _DisplayType1 = value; }
        }

        /// <summary>
        /// 前缀1
        /// </summary>
        private string _Text1;

        public string Text1
        {
            get { return _Text1; }
            set { _Text1 = value; }
        }
        /// <summary>
        /// 字段1
        /// </summary>
        private int _Column1;

        public int Column1
        {
            get { return _Column1; }
            set { _Column1 = value; }
        }
        /// <summary>
        /// 内容一
        /// </summary>
        private string _Content1;

        public string Content1
        {
            get { return _Content1; }
            set { _Content1 = value; }
        }
        /// <summary>
        /// 显示屏显示类型(0为系统，1为自定义)
        /// </summary>
        private int _DisplayType2;

        public int DisplayType2
        {
            get { return _DisplayType2; }
            set { _DisplayType2 = value; }
        }
        /// <summary>
        /// 前缀2
        /// </summary>
        private string _Text2;

        public string Text2
        {
            get { return _Text2; }
            set { _Text2 = value; }
        }
        /// <summary>
        /// 字段2
        /// </summary>
        private int _Column2;

        public int Column2
        {
            get { return _Column2; }
            set { _Column2 = value; }
        }
        /// <summary>
        /// 内容二
        /// </summary>
        private string _Content2;

        public string Content2
        {
            get { return _Content2; }
            set { _Content2 = value; }
        }
        /// <summary>
        /// 显示屏显示类型(0为系统，1为自定义)
        /// </summary>
        private int _DisplayType3;

        public int DisplayType3
        {
            get { return _DisplayType3; }
            set { _DisplayType3 = value; }
        }
        /// <summary>
        /// 前缀3
        /// </summary>
        private string _Text3;

        public string Text3
        {
            get { return _Text3; }
            set { _Text3 = value; }
        }
        /// <summary>
        /// 字段3
        /// </summary>
        private int _Column3;

        public int Column3
        {
            get { return _Column3; }
            set { _Column3 = value; }
        }
        /// <summary>
        /// 内容三
        /// </summary>
        private string _Content3;

        public string Content3
        {
            get { return _Content3; }
            set { _Content3 = value; }
        }


        #endregion

    }
}
