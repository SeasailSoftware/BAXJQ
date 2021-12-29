using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hpt.gate.DataAccess.Entity
{
    public class License
    {
        public long Id { get; set; }
        public System.DateTime CreateTime { get; set; }
        public bool Expired { get; set; }
        public string ClientCode { get; set; }
        public string RegistCode { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
    }
}
