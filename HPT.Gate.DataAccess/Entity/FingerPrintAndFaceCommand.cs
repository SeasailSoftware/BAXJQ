using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public enum FingerPrintAndFaceCommand
    {
        EnrollData = 1,
        SetTime = 2,
        EmptyData = 3,
        AddDevice=4,
        UpdateDevice=5,
        DeleteDevice = 6,
    }
}
