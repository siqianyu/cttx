using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarTech.DBUtility;
using System.Data;

/// <summary>
///QuestionHelper 的摘要说明
/// </summary>
public class QuestionHelper
{

    /// <summary>
    /// 创建我的题库记录
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="courseId"></param>
    /// <param name="ids"></param>
    public static void CreateMyQuestions(string memberId, string courseId, string ids)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");

        //读取我的原有题库
        DataTable dtOld = ado.ExecuteSqlDataset("select QuestionId,IfPass,getScore,userAnswer from T_Test_ErrorRecord where MemberId='" + memberId + "' and CourseId='" + courseId + "'").Tables[0];

        //读取题库信息
        DataTable dtAll = ado.ExecuteSqlDataset("select sysnumber,questionType,questionAnswer from T_Test_Queston where categoryFlag='" + courseId + "'").Tables[0];

        //删除题目
        ado.ExecuteSqlNonQuery("delete T_Test_ErrorRecord where MemberId='" + memberId + "' and CourseId='" + courseId + "'");

        int index = 1;
        foreach (string id in ids.Split(','))
        {
            if (id != "")
            {
                string questionType="";
                string questionAnswer = "";
                DataRow[] rowQ = dtAll.Select("sysnumber='" + id + "'");
                if (rowQ.Length > 0)
                {
                    questionType = rowQ[0]["questionType"].ToString();
                    questionAnswer = rowQ[0]["questionAnswer"].ToString().Replace(",","").TrimEnd(',');
                }

                string IfPass = "-1";
                string getScore = "0";
                string userAnswer = "";
                DataRow[] rowMy = dtOld.Select("QuestionId='" + id + "'");
                if (rowMy.Length > 0)
                {
                    IfPass = rowMy[0]["IfPass"].ToString();
                    getScore = rowMy[0]["getScore"].ToString();
                    userAnswer = rowMy[0]["userAnswer"].ToString();
                }

                string sql = "INSERT INTO [T_Test_ErrorRecord]([MemberId],[CourseId],[QuestionId],[CreateTime],[IfPass],[showIndex],[questionType],[questionAnswer],[userAnswer],[getScore])";
                sql += "VALUES('" + memberId + "','" + courseId + "','" + id + "',getdate()," + IfPass + "," + index + ",'" + questionType + "','" + questionAnswer + "','" + userAnswer + "'," + getScore + ")";
                if (ado.ExecuteSqlNonQuery(sql) > 0)
                {
                    index++;
                }
            }
        }
    }


    /// <summary>
    /// 得分计算
    /// </summary>
    public static string ComputerQuestionRecord(string hidMorePropertys, string courseId, string memberId)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        int total_questions = 0; //总题目数量
        int record_questions = 0; //记录数量
        int record_percent = 0;//百分比

        //总题目数量
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Test_ErrorRecord where  MemberId='" + memberId + "' and CourseId='" + courseId + "'").Tables[0];
        total_questions = dt.Rows.Count;
        if (total_questions == 0)
        {
            //还未创建我的题库
            total_questions = CountTotalQuestions(courseId);
            return total_questions + "|" + record_questions + "|" + record_percent;
        }

        //已回答正确
        record_questions = dt.Select("IfPass=1").Length;

        //百分比
        if (total_questions > 0)
        {
            record_percent = record_questions * 100 / total_questions;
        }
        return total_questions + "|" + record_questions + "|" + record_percent;
    }

    //统计习题数量
    public static int CountTotalQuestions(string courseId)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        //模拟练习
        DataTable dt = ado.ExecuteSqlDataset("select Questions from T_Test_day where courseType = '" + courseId + "' and shFlag=1").Tables[0];
        if (dt.Rows.Count > 0)
        {
            string sysnumbers = dt.Rows[0]["Questions"].ToString().TrimEnd(',').Replace(",", "','");
            DataTable dt2 = ado.ExecuteSqlDataset("select sysnumber from T_Test_Queston where sysnumber in('" + sysnumbers + "') and shFlag=1 and isnull(isAL,0)=0 ").Tables[0];
            return dt2.Rows.Count;
        }
        else
        {
            //课后练习
            DataTable dtHKLX = ado.ExecuteSqlDataset("select sysnumber from T_Test_Queston where courseId = '" + courseId + "' and shFlag=1 and isnull(isAL,0)=0").Tables[0];
            return dtHKLX.Rows.Count;
        }
    }
}