using HPT.Gate.Utils.Common;
using HPT.Gate.Utils.Helper;

public class OleDbHelper : DBHelper
{

    #region ctor
    public OleDbHelper()
    {
        ConnectString = AppSettings.OLEConnectString;
    }

    public OleDbHelper(string connectString)
    {
        ConnectString = connectString;
    }
    #endregion
}


