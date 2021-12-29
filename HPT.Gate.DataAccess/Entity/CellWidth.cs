using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class ColumnWidth
    {
        private string _ColumnName;

        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }


        private int _Width;

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }


    }
}
