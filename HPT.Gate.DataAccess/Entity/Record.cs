using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class Record
    {
        public int RecId { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public int Type { get; set; }

        public string TypeString
        {
            get
            {
                string type = "IC/ID卡";
                switch (Type)
                {
                    case 1:
                        type = "IC/ID卡";
                        break;
                    case 2:
                        type = "身份证序列号";
                        break;
                    case 3:
                        type = "身份证号码";
                        break;
                    case 4:
                        type = "条形码";
                        break;
                    case 5:
                        type = "指纹";
                        break;
                    case 6:
                        type = "人脸";
                        break;
                }
                return type;
            }
        }

        public string CardNo { get; set; }

        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string IOFlag { get; set; }

        public string RecDatetime { get; set; }

        public string RecordType { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public string Nation { get; set; }

        public string Address { get; set; }

        public Image Capture { get; set; }

        public int Passed { get; set; }
    }
}
