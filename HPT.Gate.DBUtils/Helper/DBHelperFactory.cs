using HPT.Gate.Utils.Common;

namespace HPT.Gate.Utils.Helper
{
    public class DBHelperFactory
    {
        public static DBHelper CreateDBHelper()
        {
            DBHelper dbHelper = null;
            switch (AppSettings.ProviderName)
            {
                case "System.Data.SqlClient":
                    dbHelper = new MsSqlHelper();
                    break;
                case "System.Data.OleDb":
                    dbHelper = new OleDbHelper();
                    break;
            }
            return dbHelper;
        }

        public static DBHelper CreateDBHelper(string connectString)
        {
            DBHelper dbHelper = null;
            switch (AppSettings.ProviderName)
            {
                case "System.Data.SqlClient":
                    dbHelper = new MsSqlHelper(connectString);
                    break;
                case "System.Data.OleDb":
                    dbHelper = new OleDbHelper(connectString);
                    break;
            }
            return dbHelper;
        }
    }
}
