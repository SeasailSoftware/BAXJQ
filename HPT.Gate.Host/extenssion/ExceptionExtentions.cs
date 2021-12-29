using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPT.Gate.Host.extenssion
{
    public static class ExceptionExtentions
    {
        public static void LogExceptions(this Task task)
        {
            task.ContinueWith(t =>
           {
               var aggException = t.Exception.Flatten();
               foreach (var exception in aggException.InnerExceptions)
                   LogException(exception);
           },
            TaskContinuationOptions.OnlyOnFaulted);
        }


        private static void LogException(Exception ex)
        {
            //todo: log this
        }

    }
}
