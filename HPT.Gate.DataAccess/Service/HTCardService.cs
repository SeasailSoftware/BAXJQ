#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：hpt.gate.DataAccess.Service

* 项目描述 ：

* 类 名 称 ：HPCardService

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：hpt.gate.DataAccess.Service

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-08 09:01:36

* 更新时间 ：2019-07-08 09:01:36

* 版 本 号 ：v1.0.0.0

*******************************************************************

* Copyright @ Administrator 2019. All rights reserved.

*******************************************************************

//----------------------------------------------------------------
*/

#endregion

using hpt.gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace hpt.gate.DataAccess.Service
{
    /// <summary>
    /// 功能描述    ：HPCardService  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-08 09:01:36 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-08 09:01:36 
    /// </summary>
    public class HTCardService
    {
        static HTCardService()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If Not exists(select * from dbo.sysObjects where id = Object_Id(N'HTCard') and ObjectProperty(id, N'IsUserTable') = 1) ");
                    buffer.AppendLine($"CREATE TABLE [dbo].[HTCard](");
                    buffer.AppendLine($"[Id] [int] IDENTITY(1,1) NOT NULL,");
                    buffer.AppendLine($"[Type] [int] NOT NULL,");
                    buffer.AppendLine($"[GradeId] [int] NOT NULL,");
                    buffer.AppendLine($"[Flag] [int] NOT NULL,");
                    buffer.AppendLine($"[CardNo] [varchar](20) NOT NULL,");
                    buffer.AppendLine($"[TermSN] [varchar](20) NOT NULL");
                    buffer.AppendLine($") ON [PRIMARY]");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }
        }

        public static bool Insert(HTCard card, out string msg)
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If Not Exists(Select top 1 * From HTCard Where Flag={card.flag}  And CardNo ='{card.CardNo}' And TermSN='{card.TermSN}') ");
                    buffer.AppendLine($"    Insert Into HTCard(Flag,CardNo,TermSN,GradeId,Type) Values({card.flag},'{card.CardNo}','{card.TermSN}',{card.gradeId},{card.type})");
                    buffer.AppendLine($"Else");
                    buffer.AppendLine($"Update HTCard Set GradeId={card.gradeId},Type={card.type} Where Flag={card.flag}  And CardNo ='{card.CardNo}' And TermSN='{card.TermSN}'");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
                msg = "Success!";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public static bool Delete(HTCard card, out string msg)
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($" Delete From HTCard Where  Flag={card.flag}  And CardNo ='{card.CardNo}' And TermSN='{card.TermSN}'");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
                msg = "Success!";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public static HTCard GetByMacAndCardNo(string mac, string sCardNo)
        {
            HTCard card = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($" Select top 1 * From HTCard Where TermSN='{mac}' And CardNo='{sCardNo}'");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    card = new HTCard();
                    card.CardNo = sCardNo;
                    card.TermSN = mac;
                    card.flag = Convert.ToInt32(row["Flag"]);
                    card.type = Convert.ToInt32(row["Type"]);
                    card.gradeId = Convert.ToInt32(row["GradeId"]);
                    card.Id = Convert.ToInt32(row["Id"]);
                    break;
                }
            }
            return card;
        }
    }
}
