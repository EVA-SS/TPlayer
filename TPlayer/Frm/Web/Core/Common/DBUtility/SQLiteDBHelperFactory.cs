using System;
using System.Data.SQLite;
using System.IO;

namespace SQLite.DBUtility
{
    /// <summary>
    /// 提供同时多个数据库连接
    /// </summary>
    public class SQLiteDBHelperFactory
    {
        static SQLiteDBHelperFactory()
        {
            string dbName = "videotemp.db";
            //string dbFolder = Path.GetTempPath();
            string dbFolder = AppDomain.CurrentDomain.BaseDirectory;
            dbFile = Path.Combine(dbFolder, dbName);
            // FileInfo fileInfo = new FileInfo(dbFilePath);
            if (!File.Exists(dbFile))
            {
                //File.WriteAllBytes(dbFile, GatherVideoController.Properties.Resources.H_Gather);
                SQLiteConnection.CreateFile(dbFile);
            }
            LoadingSqlite();
        }
        /// <summary>
        ///  初始化数据表
        /// </summary>
        public static void LoadingSqlite()
        {
            var isPicTable = Convert.ToInt32(dhldesDB.GetSingle("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='PicCacheData';"));
            var isConfigTable = Convert.ToInt32(dhldesDB.GetSingle("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Config';"));
            if (isPicTable != 1)
            {   //创建图片表
                dhldesDB.ExecuteSql("CREATE TABLE PicCacheData (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, pictureID INTEGER NOT NULL, pictureImage BLOB," +
                    " pictureDatatime TEXT" +
                    ",picType TEXT); ");
            }
            if (isConfigTable != 1)
            {   //创建配置表
                dhldesDB.ExecuteSql("CREATE TABLE Config ( id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [key] NOT NULL, value TEXT ); ");
            }
        }
        private static string dbFile;
        private static SQLiteDBHelper _dhldesDBHelper;
        private static readonly object obj = new object();
        /// <summary>
        /// dhldes数据库连接
        /// </summary>
        public static SQLiteDBHelper dhldesDB
        {
            get
            {
                lock (obj)
                {
                    if (_dhldesDBHelper == null)
                        _dhldesDBHelper = new SQLiteDBHelper();
                    try
                    {
                        _dhldesDBHelper.connectionString = string.Format(@"Version = 3;Data Source={0}", dbFile);
                    }
                    catch { }
                    return _dhldesDBHelper;
                };
            }
        }
    }
}
