using System;
using System.Collections.Generic;
using System.Text;

namespace HPT.Gate.DataAccess.Entity //修改名字空间
{
    public class AreaInfo
    {
        private int recId;
        public int RecId
        {
            get { return recId; }
            set { recId = value; }
        }

        private int areaId;
        public int AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }

        private int lID;
        public int LID
        {
            get { return lID; }
            set { lID = value; }
        }

        private int point_X;
        public int Point_X
        {
            get { return point_X; }
            set { point_X = value; }
        }

        private int point_Y;
        public int Point_Y
        {
            get { return point_Y; }
            set { point_Y = value; }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int bordreType;
        public int BordreType
        {
            get { return bordreType; }
            set { bordreType = value; }
        }

        private int borderNo;
        public int BorderNo
        {
            get { return borderNo; }
            set { borderNo = value; }
        }

        private int borderLength;
        public int BorderLength
        {
            get { return borderLength; }
            set { borderLength = value; }
        }

        private int borderSpeed;
        public int BorderSpeed
        {
            get { return borderSpeed; }
            set { borderSpeed = value; }
        }

        private int borderEffect;
        public int BorderEffect
        {
            get { return borderEffect; }
            set { borderEffect = value; }
        }

        private string textFont;
        public string TextFont
        {
            get { return textFont; }
            set { textFont = value; }
        }

        private int textFontSize;
        public int TextFontSize
        {
            get { return textFontSize; }
            set { textFontSize = value; }
        }

        private int textBold;
        public int TextBold
        {
            get { return textBold; }
            set { textBold = value; }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private int singleLine;
        public int SingleLine
        {
            get { return singleLine; }
            set { singleLine = value; }
        }

        private int displayEffect;
        public int DisplayEffect
        {
            get { return displayEffect; }
            set { displayEffect = value; }
        }

        private int speed;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private int stay;
        public int Stay
        {
            get { return stay; }
            set { stay = value; }
        }

        /// <summary>
        /// 刷新频率
        /// </summary>
        public int Interval { get; set; }

        public string Content { get; set; }
    }
}