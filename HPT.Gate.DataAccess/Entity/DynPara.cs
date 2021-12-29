using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class DynPara
    {

        public DynPara()
        {

        }

        #region Properity

        public int RecId { get; set; }

        public int ParaId { get; set; }

        public string ParaName { get; set; }

        public string Server { get; set; }

        public string DBName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ParaValue { get; set; }

        public string ConnectString
        {
            get
            {
                return @"Data Source = " + Server + "; Initial Catalog = " + DBName + ";Integrated Security=false;Persist Security Info=False;User Id = " + UserName + "; Password = " + Password + ";";
            }
        }
        #endregion

    }
}
