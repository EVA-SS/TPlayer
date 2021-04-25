using SQLite.DBUtility;
using System;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace TPlayer.Frm.Web
{
    // [Synchronization]
    public class PicCacheDataDAL//: System.ContextBoundObject
    {
        private static readonly object obj = new object();//限制单线程读写
        /// <summary>
        /// 查询本地是否存在图片
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        // [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public bool ExistsPic(int picId, string picType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 1 from PicCacheData");
            strSql.Append(" where ");
            strSql.Append(" pictureID=@Col001 and picType=@picType ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Col001", DbType.Int32),
                    new SQLiteParameter("@picType", DbType.String)
            };
            parameters[0].Value = picId;
            parameters[1].Value = picType;
            return SQLiteDBHelperFactory.dhldesDB.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取图片数据
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        public DataTable GetPicUrl(string picId, string picType)
        {
            string sql = $"select * from  PicCacheData where pictureID in ({picId}) and picType='{picType}'";
            return SQLiteDBHelperFactory.dhldesDB.Query(sql).Tables[0];
        }
        /// <summary>
        /// 更新图片地址
        /// </summary>
        /// <param name="picId"></param>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        public int SetPicUrl(PicCacheData model)
        {
            string sql = "update PicCacheData set pictureImage=@fs where pictureID=" + model.pictureID + "and picType='" + model.picType + "'";
            return SQLiteDBHelperFactory.dhldesDB.ExecuteSqlInsertImg(sql, model.pictureImage);
        }
        /// <summary>
        /// 插入图片数据
        /// </summary>
        /// <param name="picId"></param>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        public int InsertPicUrl(PicCacheData model)
        {
            lock (obj)
            {
                string sql = "insert into PicCacheData(id,pictureID,pictureImage,pictureDatatime,picType) values(null," + model.pictureID + ",@fs,'" + DateTime.Now.ToString() + "','" + model.picType + "')";
                return SQLiteDBHelperFactory.dhldesDB.ExecuteSqlInsertImg(sql, model.pictureImage);
            }
        }
        /// <summary>
        /// 删除图片数据
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        public int DeletePicData(int picId)
        {
            string sql = "delete  from PicCacheData where pictureID=" + picId;
            return SQLiteDBHelperFactory.dhldesDB.ExecuteSql(sql);
        }
    }
}
