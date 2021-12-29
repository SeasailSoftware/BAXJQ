using HPT.Gate.Utils.Common;

namespace HPT.Gate.Utils.Helper
{
    public class MsSqlHelper : DBHelper
    {
        #region ctor
        public MsSqlHelper()
        {
            ConnectString = AppSettings.MssqlConnectString;

        }

        public MsSqlHelper(string connectString)
        {
            ConnectString = connectString;
        }
        #endregion
    }
}
