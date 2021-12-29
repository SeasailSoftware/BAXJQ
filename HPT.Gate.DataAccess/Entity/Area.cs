using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class Area
    {
        #region ------Var

        /// <summary>
        /// 区域编号
        /// </summary>
        private int _AreaId;

        public int AreaId
        {
            get { return _AreaId; }
            set { _AreaId = value; }
        }

        /// <summary>
        /// 区域名称
        /// </summary>
        private string _AreaName;

        public string AreaName
        {
            get { return _AreaName; }
            set { _AreaName = value; }
        }

        /// <summary>
        /// 区域类型(动态1静态0)
        /// </summary>
        private int _AreaType;

        public int AreaType
        {
            get { return _AreaType; }
            set { _AreaType = value; }
        }

        /// <summary>
        /// 坐标(X)
        /// </summary>
        private int _Point_X;

        public int Point_X
        {
            get { return _Point_X; }
            set { _Point_X = value; }
        }

        /// <summary>
        /// 坐标(Y)
        /// </summary>
        private int _Point_Y;

        public int Point_Y
        {
            get { return _Point_Y; }
            set { _Point_Y = value; }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        private int _Width;

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        /// <summary>
        /// 高度
        /// </summary>
        private int _Height;

        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        /// <summary>
        /// 是否单行显示，0单行，1多行
        /// </summary>
        private int _Rows;

        public int Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }

        /// <summary>
        /// 0为手动换行，1为自动换行
        /// </summary>
        private int _ChangeRow;

        public int ChangeRow
        {
            get { return _ChangeRow; }
            set { _ChangeRow = value; }
        }

        /// <summary>
        /// 字间距
        /// </summary>
        private int _Spacing;

        public int Spacing
        {
            get { return _Spacing; }
            set { _Spacing = value; }
        }

        /// <summary>
        /// 文字内容
        /// </summary>
        private string _Content;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        /// <summary>
        /// 显示效果
        /// </summary>
        private int _DisplayEffect;

        public int DisplayEffect
        {
            get { return _DisplayEffect; }
            set { _DisplayEffect = value; }
        }

        /// <summary>
        /// 运行速度
        /// </summary>
        private int _Speed;

        public int Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        /// <summary>
        /// 停留时间
        /// </summary>
        private int _StayTime;

        public int StayTime
        {
            get { return _StayTime; }
            set { _StayTime = value; }
        }
        #endregion
    }
}
