﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.YF.Request
{
    public class YFRequestSetPassword
    {

        public string OldPass { get; set; }

        public string NewPass { get; set; }

        public string Serialize()
        {
            return $"oldPass={OldPass}&newPass={NewPass}";
        }
    }
}
