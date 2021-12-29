using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class Barcode
    {
        public int RecId { get; set; }

        public string BarcodeNo { get; set; }

        public List<int> DevList { get; set; }

        public int EffectTime { get; set; }

        public int TimesOfIn { get; set; }

        public int TimesOfInLeft { get; set; }

        public int TimesOfOut { get; set; }

        public int TimesOfOutLeft { get; set; }

        public string CreateTime { get; set; }

        public string OutOfTime { get; set; }


    }
}
