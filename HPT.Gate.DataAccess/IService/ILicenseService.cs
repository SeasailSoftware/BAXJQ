using hpt.gate.DataAccess.Entity;
using System;

namespace HPT.Face.Data.Service
{
    public interface ILicenseService : IDisposable
    {
        bool Set(License license, out string msg);

        License Get();
        bool SetRegistCode(string registCode, out string msg);
    }
}
