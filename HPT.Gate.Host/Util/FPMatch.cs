using HPT.Gate.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Host
{
    public class FPMatch
    {
        static FPMatch()
        {
            SetThreshold(35, 35);
        }

        [DllImport(@"matchdll.dll")]
        public static extern bool process(string regTemp, string verTemp);

        [DllImport(@"matchdll.dll")]
        public static extern void SetThreshold(int regTemp, int verTemp);

        private static List<FingerPrint> FingerPrints = new List<FingerPrint>();
        private static object FingerPrintsLocker = new object();

        #region 清空指纹列表
        public static void Clear()
        {
            lock (FingerPrintsLocker)
            {
                FingerPrints.Clear();
            }
        }
        #endregion


        #region 更新指纹
        public static void AddFingerPrint(FingerPrint fingerPrint)
        {
            if (fingerPrint == null)
                return;
            if (fingerPrint.FingerData.Length < 300)
                return;
            lock (FingerPrintsLocker)
            {
                if (FingerPrints.Exists(p => p.FingerId == fingerPrint.FingerId))
                    FingerPrints.Remove(fingerPrint);
                FingerPrints.Add(fingerPrint);
                FingerPrints = FingerPrints.OrderBy(p => p.FingerId).ToList();
            }

        }
        #endregion

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public static int Match(byte[] verTemp)
        {
            List<FingerPrint> list = new List<FingerPrint>();
            lock (FingerPrintsLocker)
            {
                FingerPrints.ForEach(p => list.Add(p));
            }
            int fingerId = 0;
            string ver = Convert.ToBase64String(verTemp);
            if (string.IsNullOrWhiteSpace(ver)) return fingerId;
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                foreach (FingerPrint fp in FingerPrints.AsParallel())
                {
                    if (fp.FingerData.Length < 300)
                        continue;
                    string reg = Convert.ToBase64String(fp.FingerData);
                    if (string.IsNullOrWhiteSpace(reg)) continue;
                    if (process(reg, ver))
                    {
                        fingerId = fp.FingerId;
                        break;
                    }
                    if (watch.ElapsedMilliseconds >= 2500) break;
                }
            }
            catch (System.AccessViolationException e) //捕获cse类型的异常
            {

                LocalCache.Restart = true;
                Application.Restart();
            }
            catch (AggregateException ex)
            {

                LocalCache.Restart = true;
                Application.Restart();
            }
            catch (Exception ex)
            {
                LocalCache.Restart = true;
                Application.Restart();
            }
            finally
            {
                watch.Stop();
            }
            return fingerId;
        }


        private static string ArrayToHexString(byte[] bytes)
        {
            var hexString = string.Empty;
            if (bytes != null)
            {
                var strB = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }
    }
}
