using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DBHelper
{

    public enum DBType
    {
        MSSQL,
        MYSQL,
        ORACLE,
        SQLITE,
        ACCESS,

    }

    public class DBFactory
    {

        public static IDBHelper GetDB(DBType dbType, string strConn)
        {

            IDBHelper idbHelper = null;
            switch (dbType)
            {
                case DBType.ACCESS:
                    break;
                case DBType.MSSQL:
                    break;
                case DBType.MYSQL:
                    break;
                case DBType.ORACLE:
                    break;
                case DBType.SQLITE:
                    idbHelper = new DBSqLite(strConn);
                    break;

            }
            return idbHelper;
        }
    }
}
