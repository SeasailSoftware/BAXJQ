using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace HPT.Gate.DataAccess.Service
{
    public class FaceDataTaskService
    {
        #region 获取未同步列表
        public static List<FaceDataTask> GetUnSynTask(int machineNumber)
        {
            List<FaceDataTask> tasks = new List<FaceDataTask>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From FaceDataTask Where UpdateFlag = 0 And DeviceId ={machineNumber} Order By EmpId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    FaceDataTask obj = new FaceDataTask();
                    obj.RecId = Convert.ToInt32(row["RecId"]);
                    obj.Type = Convert.ToInt32(row["Type"]);
                    obj.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    obj.EmpId = Convert.ToInt32(row["EmpId"]);
                    obj.UpdateFlag = Convert.ToInt32(row["UpdateFlag"]);
                    tasks.Add(obj);
                }
            }
            return tasks;
        }
        #endregion

        #region 获取未同步列表
        public static bool FinishedTask(FaceDataTask task, bool status = true)
        {
            bool success = false;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "HandleFaceTask";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@RecId", DbType.Int32, task.RecId);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, task.EmpId);
                dbHelper.AddInParameter(cmd, "@Success", DbType.Int32, status ? 1 : 0);
                /*
                sql += "Declare @Ret int";
                sql += $"{Environment.NewLine}Select @Ret = 0";
                if (status)
                    sql += $"{Environment.NewLine}Delete From FaceDataTask  Where RecId = {task.RecId}";
                else
                    sql += $"{Environment.NewLine}Update FaceDataTask Set UpdateFlag = 1 Where RecId = {task.RecId} ";
                //人员全部完成
                sql += $"{Environment.NewLine} If Not Exists(Select top 1 * From FaceDataTask Where EmpId ={task.EmpId})";
                sql += $"{Environment.NewLine} Begin";
                sql += $"{Environment.NewLine}  Update EmpInfo Set FaceStatus ='已上传' Where EmpId ={task.EmpId}";
                sql += $"{Environment.NewLine}  Select @Ret = 0";
                sql += $"{Environment.NewLine} End Else";
                //存在未同步任务
                sql += $"{Environment.NewLine}If Exists(Select top 1 * From FaceDataTask Where EmpId={task.EmpId} And UpdateFlag = 0)";
                sql += $"{Environment.NewLine}Begin";
                sql += $"{Environment.NewLine}  Update EmpInfo Set FaceStatus ='上传未完成' Where EmpId ={task.EmpId}";
                sql += $"{Environment.NewLine}  Select @Ret = 0";
                sql += $"{Environment.NewLine}End Else";
                //存在全部同步失败的任务
                sql += $"{Environment.NewLine}If Exists(Select top 1 *  From FaceDataTask Where EmpId={task.EmpId} And UpdateFlag = 1) And Not Exists(Select top 1 *  From FaceDataTask Where EmpId={task.EmpId} And UpdateFlag = 0)";
                sql += $"{Environment.NewLine}Begin";
                sql += $"{Environment.NewLine}  Delete From FaceDataTask Where EmpId={task.EmpId}";
                sql += $"{Environment.NewLine}  Update EmpInfo Set FaceStatus ='上传失败' Where EmpId={task.EmpId}";
                sql += $"{Environment.NewLine}  Select @Ret =1";
                sql += $"{Environment.NewLine}End";
                sql += $"{Environment.NewLine} Select @Ret As Result";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                */
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    success = Convert.ToInt32(row[0]) == 1;
                    break;
                }
            }
            return success;
        }

        public static void DeleteTask(int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From FaceDataTask Where DeviceId={deviceId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
