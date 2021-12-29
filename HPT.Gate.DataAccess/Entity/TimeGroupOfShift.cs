using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class TimeGroupOfShift
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string BeginTime1 { get; set; }

        public string Time1 { get; set; }

        public string EndTime1 { get; set; }

        public string BeginTime2 { get; set; }

        public string Time2 { get; set; }

        public string EndTime2 { get; set; }

        public int LateMinute { get; set; }
        public int EarlyMinute { get; set; }

        public double Day { get; set; }

        public int Minute { get; set; }

        public int MustSignIn { get; set; }

        public int MustSignOut { get; set; }

        public int OTBeforeSignIn { get; set; }

        public int OTAfterSignOut { get; set; }

        public int Across
        {
            get
            {
                int temp = 0;
                DateTime signInBegin = Convert.ToDateTime(BeginTime1);
                DateTime signIn = Convert.ToDateTime(Time1);
                DateTime signInEnd = Convert.ToDateTime(EndTime1);
                DateTime signOutBegin = Convert.ToDateTime(BeginTime2);
                DateTime signOut = Convert.ToDateTime(Time2);
                DateTime signOutEnd = Convert.ToDateTime(EndTime2);
                if (signInBegin > signIn)
                {
                    temp = 1;
                    return temp;
                }
                if (signIn > signInEnd)
                {
                    temp = 2;
                    return temp;
                }
                if (signInEnd > signOutBegin)
                {
                    temp = 3;
                    return temp;
                }
                if (signOutBegin > signOut)
                {
                    temp = 4;
                    return temp;
                }
                if (signOut > signOutEnd)
                {
                    temp = 5;
                    return temp;
                }
                return temp;
            }
        }

    }
}
