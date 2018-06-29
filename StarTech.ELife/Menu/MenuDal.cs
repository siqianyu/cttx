using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StarTech.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace StarTech.ELife.Menu
{
    public class MenuDal
    {


        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MenuModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Menu_Info(");
            strSql.Append("menuId,menuName,Technology,Flavor,CookingTime,Calorie,CookingSkill,UserId,AddTime,isShow,imgSrc,smallImgSrc,categoryId,signId,isTop)");
            strSql.Append(" values (");
            strSql.Append("@menuId,@menuName,@Technology,@Flavor,@CookingTime,@Calorie,@CookingSkill,@UserId,@AddTime,@isShow,@imgSrc,@smallImgSrc,@categoryId,@signId,@isTop)");
            SqlParameter[] parameters = {
					new SqlParameter("@menuId", SqlDbType.VarChar,50),
					new SqlParameter("@menuName", SqlDbType.VarChar,50),
					new SqlParameter("@Technology", SqlDbType.VarChar,50),
					new SqlParameter("@Flavor", SqlDbType.VarChar,50),
					new SqlParameter("@CookingTime", SqlDbType.VarChar,50),
					new SqlParameter("@Calorie", SqlDbType.Float,8),
					new SqlParameter("@CookingSkill", SqlDbType.VarChar,5000),
					new SqlParameter("@UserId", SqlDbType.VarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@isShow", SqlDbType.Int,4),
					new SqlParameter("@imgSrc", SqlDbType.VarChar,500),
					new SqlParameter("@smallImgSrc", SqlDbType.VarChar,500),
                    new SqlParameter("@categoryId", SqlDbType.VarChar,500),
                    new SqlParameter("@signId", SqlDbType.VarChar),
                    new SqlParameter("@isTop",SqlDbType.VarChar)
                                        };
            parameters[0].Value = model.menuId;
            parameters[1].Value = model.menuName;
            parameters[2].Value = model.Technology;
            parameters[3].Value = model.Flavor;
            parameters[4].Value = model.CookingTime;
            parameters[5].Value = model.Calorie;
            parameters[6].Value = model.CookingSkill;
            parameters[7].Value = model.UserId;
            parameters[8].Value = model.AddTime;
            parameters[9].Value = model.isShow;
            parameters[10].Value = model.imgSrc;
            parameters[11].Value = model.smallImgSrc;
            parameters[12].Value = model.categoryId;
            parameters[13].Value = model.signId;
            parameters[14].Value = model.isTop;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MenuModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Menu_Info set ");
            strSql.Append("menuName=@menuName,");
            strSql.Append("Technology=@Technology,");
            strSql.Append("Flavor=@Flavor,");
            strSql.Append("CookingTime=@CookingTime,");
            strSql.Append("Calorie=@Calorie,");
            strSql.Append("CookingSkill=@CookingSkill,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("isShow=@isShow,");
            strSql.Append("imgSrc=@imgSrc,");
            strSql.Append("smallImgSrc=@smallImgSrc,");
            strSql.Append("categoryId=@categoryId,");
            strSql.Append("signId=@signId ,");
            strSql.Append("isTop=@isTop ");
            strSql.Append(" where menuId=@menuId ");
            SqlParameter[] parameters = {
					new SqlParameter("@menuName", SqlDbType.VarChar,50),
					new SqlParameter("@Technology", SqlDbType.VarChar,50),
					new SqlParameter("@Flavor", SqlDbType.VarChar,50),
					new SqlParameter("@CookingTime", SqlDbType.VarChar,50),
					new SqlParameter("@Calorie", SqlDbType.Float,8),
					new SqlParameter("@CookingSkill", SqlDbType.VarChar,5000),
					new SqlParameter("@UserId", SqlDbType.VarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@isShow", SqlDbType.Int,4),
					new SqlParameter("@imgSrc", SqlDbType.VarChar,500),
					new SqlParameter("@smallImgSrc", SqlDbType.VarChar,500),
                    new SqlParameter("@categoryId", SqlDbType.VarChar,50),
                    new SqlParameter("@signId", SqlDbType.VarChar),
                    new SqlParameter("@isTop", SqlDbType.VarChar),
					new SqlParameter("@menuId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.menuName;
            parameters[1].Value = model.Technology;
            parameters[2].Value = model.Flavor;
            parameters[3].Value = model.CookingTime;
            parameters[4].Value = model.Calorie;
            parameters[5].Value = model.CookingSkill;
            parameters[6].Value = model.UserId;
            parameters[7].Value = model.AddTime;
            parameters[8].Value = model.isShow;
            parameters[9].Value = model.imgSrc;
            parameters[10].Value = model.smallImgSrc;
            parameters[11].Value = model.categoryId;
            parameters[12].Value = model.signId;
            parameters[13].Value = model.isTop;
            parameters[14].Value = model.menuId;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string menuId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Menu_Info ");
            strSql.Append(" where menuId=@menuId");
            SqlParameter[] parameters = {
					new SqlParameter("@menuId", SqlDbType.VarChar,50)};
            parameters[0].Value = menuId;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MenuModel GetModel(string menuId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 isTop,signId,menuId,menuName,Technology,Flavor,CookingTime,Calorie,CookingSkill,UserId,AddTime,isShow,imgSrc,smallImgSrc,categoryId from T_Menu_Info ");
            strSql.Append(" where menuId=@menuId");
            SqlParameter[] parameters = {
					new SqlParameter("@menuId", SqlDbType.VarChar,50)};
            parameters[0].Value = menuId;

            MenuModel model = new MenuModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["menuId"] != null && ds.Tables[0].Rows[0]["menuId"].ToString() != "")
                {
                    model.menuId = ds.Tables[0].Rows[0]["menuId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["menuName"] != null && ds.Tables[0].Rows[0]["menuName"].ToString() != "")
                {
                    model.menuName = ds.Tables[0].Rows[0]["menuName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Technology"] != null && ds.Tables[0].Rows[0]["Technology"].ToString() != "")
                {
                    model.Technology = ds.Tables[0].Rows[0]["Technology"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Flavor"] != null && ds.Tables[0].Rows[0]["Flavor"].ToString() != "")
                {
                    model.Flavor = ds.Tables[0].Rows[0]["Flavor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CookingTime"] != null && ds.Tables[0].Rows[0]["CookingTime"].ToString() != "")
                {
                    model.CookingTime = ds.Tables[0].Rows[0]["CookingTime"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Calorie"] != null && ds.Tables[0].Rows[0]["Calorie"].ToString() != "")
                {
                    model.Calorie = decimal.Parse(ds.Tables[0].Rows[0]["Calorie"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CookingSkill"] != null && ds.Tables[0].Rows[0]["CookingSkill"].ToString() != "")
                {
                    model.CookingSkill = ds.Tables[0].Rows[0]["CookingSkill"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserId"] != null && ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AddTime"] != null && ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isShow"] != null && ds.Tables[0].Rows[0]["isShow"].ToString() != "")
                {
                    model.isShow = int.Parse(ds.Tables[0].Rows[0]["isShow"].ToString());
                }
                if (ds.Tables[0].Rows[0]["imgSrc"] != null && ds.Tables[0].Rows[0]["imgSrc"].ToString() != "")
                {
                    model.imgSrc = ds.Tables[0].Rows[0]["imgSrc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["smallImgSrc"] != null && ds.Tables[0].Rows[0]["smallImgSrc"].ToString() != "")
                {
                    model.smallImgSrc = ds.Tables[0].Rows[0]["smallImgSrc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["categoryId"] != null && ds.Tables[0].Rows[0]["categoryId"].ToString() != "")
                {
                    model.categoryId = ds.Tables[0].Rows[0]["categoryId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["signId"] != null && ds.Tables[0].Rows[0]["signId"].ToString() != "")
                {
                    model.signId = ds.Tables[0].Rows[0]["signId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isTop"] != null && ds.Tables[0].Rows[0]["isTop"].ToString() != "")
                {
                    int isTop = 0;
                    int.TryParse(ds.Tables[0].Rows[0]["isTop"].ToString(),out isTop);
                    model.isTop = isTop;
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select istop,menuId,menuName,Technology,Flavor,CookingTime,Calorie,CookingSkill,UserId,AddTime,isShow,imgSrc,smallImgSrc,categoryId ");
            strSql.Append(" FROM T_Menu_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

    }
}
