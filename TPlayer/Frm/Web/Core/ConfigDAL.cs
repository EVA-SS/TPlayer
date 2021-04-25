using SQLite.DBUtility;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace TPlayer.Frm.Web
{
    public class ConfigDAL
    {

        public ConfigDAL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 1 from Config");
            strSql.Append(" where ");
            strSql.Append(" [key]=@Col001 ");
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@Col001", DbType.String)
            };
            parameters[0].Value = Key;
            return SQLiteDBHelperFactory.dhldesDB.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条配置
        /// </summary>
        public int AddPicCacheData(string key, string value)
        {
            string sql = $"insert into Config (id,[key],value) values(null,'{key}',@value)";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@value", DbType.String)
            };
            parameters[0].Value = value;
            return SQLiteDBHelperFactory.dhldesDB.ExecuteSql(sql, parameters);
        }

        /// <summary>
        /// 更新一条配置
        /// </summary>
        public int UpdatePicCacheData(string key, string value)
        {
            string sql = "update Config set value=@value where [key]=" + key;
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@value", DbType.String)
            };
            parameters[0].Value = value;
            return SQLiteDBHelperFactory.dhldesDB.ExecuteSql(sql, parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetPicCacheData(string key)
        {
            string sql = "select value from  Config where [key] ='" + key + "'";
            return SQLiteDBHelperFactory.dhldesDB.GetSingle(sql).ToString();
        }

        /// <summary>
        /// 删除一条配置
        /// </summary>
        public int DeletePicCacheDataPicCacheData(string key)
        {
            string sql = "delete  from Config where [key]=" + key;
            return SQLiteDBHelperFactory.dhldesDB.ExecuteSql(sql);
        }


        #endregion
    }
}
