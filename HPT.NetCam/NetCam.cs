using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HPT.NetCam
{
    public interface NetCam
    {
        string IPAddress { get; set; }

        UInt16 Port { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        bool Login(out string msg);

        bool LogOut(out string msg);

        bool Capture(out string msg,out Image img,string fileName ="");

        bool StartPreview(IntPtr handler,out string msg);

        bool StopPreview(out string msg);
    }
}
