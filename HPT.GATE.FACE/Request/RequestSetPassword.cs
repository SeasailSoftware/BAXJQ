﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.Request
{
    public class RequestSetPassword
    {

        public string OldPass { get; set; }

        public string NewPass { get; set; }

        public string Serialize()
        {
            return $"oldPass={OldPass}&newPass={NewPass}";
        }
    }
}
