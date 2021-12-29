using System;
using System.Drawing;

namespace HPT.Face
{
    public interface HFace
    {
        bool CreateEmp(string ip, string password, string empCode, string empName, string endDate, Image photo, out string msg);

        bool UpdateEmp(string ip, string password, string empCode, string empName, string endDate, Image photo, out string msg);
        bool UpdateEmp(string ip, string password, int empId, string empName, string endDate, byte[] photo, out string msg);

        bool DeleteEmp(string ip, string password, string empCode, out string msg);
        bool SetTime(string iPAddress, string password, DateTime dt, out string msg);
        bool ReSet(string iPAddress, string password, bool v, out string msg);
        bool CheckFace(string iPAddress, string password, Image image, out string msg);

        bool IsOnline(string iPAddress);

        Image Capture(string iPaddress, string fileName, out string msg);
    }
}
