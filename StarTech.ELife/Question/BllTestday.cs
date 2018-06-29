using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StarTech.DBUtility;

namespace StarTech.ELife.Question
{
    /// <summary>
    /// BllTestday
    /// </summary>
    public partial class BllTestday
    {
        private readonly DalTestday dal = new DalTestday();
        public BllTestday()
        { }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModelTestday model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ModelTestday model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string Sysnumber)
        {

            dal.Delete(Sysnumber);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModelTestday GetModel(string Sysnumber)
        {

            return dal.GetModel(Sysnumber);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ModelTestday> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ModelTestday> DataTableToList(DataTable dt)
        {
            List<ModelTestday> modelList = new List<ModelTestday>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ModelTestday model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ModelTestday();
                    if (dt.Rows[n]["Sysnumber"] != null && dt.Rows[n]["Sysnumber"].ToString() != "")
                    {
                        model.Sysnumber = dt.Rows[n]["Sysnumber"].ToString();
                    }
                    if (dt.Rows[n]["Title"] != null && dt.Rows[n]["Title"].ToString() != "")
                    {
                        model.Title = dt.Rows[n]["Title"].ToString();
                    }
                    if (dt.Rows[n]["Remarks"] != null && dt.Rows[n]["Remarks"].ToString() != "")
                    {
                        model.Remarks = dt.Rows[n]["Remarks"].ToString();
                    }
                    if (dt.Rows[n]["Questions"] != null && dt.Rows[n]["Questions"].ToString() != "")
                    {
                        model.Questions = dt.Rows[n]["Questions"].ToString();
                    }
                    if (dt.Rows[n]["Addtime"] != null && dt.Rows[n]["Addtime"].ToString() != "")
                    {
                        model.Addtime = DateTime.Parse(dt.Rows[n]["Addtime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method

        #region 随机组卷题目
        public string CreateRndQuestions(string testSysnumber)
        {
            string rndQuestions = "";
            string zyQuestions = "";
            string courseQuestions = "";
            AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
            //本专业
            DataSet ds = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId='Current'");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string sql = "select top " + row["num"] + " sysnumber from T_Test_Queston where " + GetPersonFlagFilter(row["categoryId"].ToString()) + " and personFlag like '%%'  and questionType='" + row["qType"] + "' and isnull(isSubQuestion,0)=0 and isnull(courseId,'')='' order by newid()";
                //Response.Write(sql+";<br>");
                DataSet dsQ = ado.ExecuteSqlDataset(sql);
                int flagI = 0;
                string ids = "";
                foreach (DataRow rowQ in dsQ.Tables[0].Rows)
                {
                    if (flagI > 100) { break; }
                    ids += rowQ["sysnumber"].ToString() + ",";
                    flagI++;
                }
                if (ids != "")
                {
                    ids = ids.TrimEnd(',');
                    zyQuestions += "," + ids;
                }
            }
            //this.hidZYQuestions.Value = zyQuestions.TrimStart(',').TrimEnd(',');

            //课后练习
            DataSet dscourse = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId='Course'");
            foreach (DataRow row in dscourse.Tables[0].Rows)
            {
                string sql = "select top " + row["num"] + " sysnumber from T_Test_Queston where " + GetPersonFlagFilter(row["categoryId"].ToString()) + " and personFlag like '%%' and questionType='" + row["qType"] + "' and isnull(isSubQuestion,0)=0 and sysnumber not in('" + zyQuestions.Replace(",", "','") + "') order by newid()";
                //Response.Write(sql + ";<br>");
                DataSet dsQ = ado.ExecuteSqlDataset(sql);
                int flagI = 0;
                string ids = "";
                foreach (DataRow rowQ in dsQ.Tables[0].Rows)
                {
                    if (flagI > 100) { break; }
                    ids += rowQ["sysnumber"].ToString() + ",";
                    flagI++;
                }
                if (ids != "")
                {
                    ids = ids.TrimEnd(',');
                    courseQuestions += "," + ids;
                }
            }
            //this.hidCourseQuestions.Value = courseQuestions.TrimStart(',').TrimEnd(',');


            //大专业、公共
            DataSet ds2 = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId in('Parent','Common')");
            foreach (DataRow row in ds2.Tables[0].Rows)
            {
                string hasExsitsQuestions = zyQuestions + courseQuestions.Replace(",,", ",");
                string sql = "select top " + row["num"] + " sysnumber from T_Test_Queston where " + GetPersonFlagFilter(row["categoryId"].ToString()) + " and personFlag like '' and questionType='" + row["qType"] + "' and isnull(isSubQuestion,0)=0 and isnull(courseId,'')='' and sysnumber not in('" + hasExsitsQuestions.Replace(",", "','") + "') order by newid()";
                //Response.Write(sql + ";<br>");
                DataSet dsQ = ado.ExecuteSqlDataset(sql);
                int flagI = 0;
                string ids = "";
                foreach (DataRow rowQ in dsQ.Tables[0].Rows)
                {
                    if (flagI > 100) { break; }
                    ids += rowQ["sysnumber"].ToString() + ",";
                    flagI++;
                }
                if (ids != "")
                {
                    ids = ids.TrimEnd(',');
                    rndQuestions += "," + ids;
                }
            }
            //this.hidOtherQuestions.Value = rndQuestions.TrimStart(',').TrimEnd(',');

            string allRndQuestions = zyQuestions + "," + courseQuestions.TrimStart(',').TrimEnd(',') + "," + rndQuestions.TrimStart(',').TrimEnd(',');
            return allRndQuestions;
        }
        public string GetPersonFlagFilter(string flag)
        {
            string filter = " 1=1 ";
            if (flag == "Current")
            {
                string categoryIds = "";
                if (categoryIds == "")
                {
                    return "1=2";
                }
                else
                {
                    string[] idArr = categoryIds.Split(',');
                    if (idArr.Length > 1)
                    {
                        string tmp = "";
                        for (int i = 0; i < idArr.Length; i++)
                        {
                            if (i == 0) { tmp += " categoryPath like '" + idArr[i] + "%' "; }
                            else { tmp += " or  categoryPath like '" + idArr[i] + "%'"; }
                        }
                        filter += "and (" + tmp + ") ";
                    }
                    else
                    {
                        filter += "and categoryPath like '" + idArr[0] + "%'";
                    }
                }

            }
            else if (flag == "Parent")
            {
                filter += "and categoryPath like ''";
            }
            else if (flag == "Common")
            {
                filter += "and categoryPath like 'Ⅴ%'";
            }
            else if (flag == "Course")
            {
                string courseIds = "";
                if (courseIds == "")
                {
                    return "1=2";
                }
                else
                {
                    filter += "and courseId in('" + courseIds.Replace(",", "','") + "')";
                }
            }
            return filter;
        }

        #endregion
    }
}
