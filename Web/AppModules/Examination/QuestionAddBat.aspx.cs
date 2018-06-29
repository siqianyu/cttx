using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using StarTech.DBUtility;

using StarTech.ELife.Question;

public partial class MemberCenter_QuestionAddBat : System.Web.UI.Page
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    public string courseId;
    public string sjid;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sjid = Request["sjid"] == null ? "" : Request["sjid"];
        this.courseId = Request["courseId"] == null ? "" : Request["courseId"];
        if (this.courseId != "")
        {
            this.txtCourseId.Value = this.courseId;
            this.txtCourseId.Disabled = true;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        //Excel导入
        string path = UploadFile(this.FileUpload1, "~/upload/excel/");
        if (path == "") { ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请选择要导入的Excel文件!');</script>"); return; }

        //Excel读取
        string error = "";
        DataSet dsExcel = GetExcelData(Server.MapPath(path), ref error);
        if (dsExcel == null) { ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + error.Replace("'", "’") + "');</script>"); return; }

        //Excel格式判读
        if (dsExcel.Tables[0].Columns.Count < 5) { ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Excel格式错误!');</script>"); return; }


        //开始导入
        int totalNum = 0;
        int errorNum = 0;
        int exsitNum = 0;
        BllTestQueston bll = new BllTestQueston();
        int sn = 1;
        foreach (DataRow row in dsExcel.Tables[0].Rows)
        {
            if (row[0].ToString().Trim() == "" || row[2].ToString().Trim() == "") { continue; }
            if (row[0].ToString().Trim() == "题型名称" || row[2].ToString().Trim() == "题目内容") { continue; }

            ModelTestQueston model = new ModelTestQueston();
            model.sysnumber = Guid.NewGuid().ToString();
            model.questionTitle = row[2].ToString().Trim().Replace("'", "’").Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("/", "∕").Replace("ˆ", "^").Trim();
            model.questionType = row[0].ToString().Trim().Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
            try
            {
                model.LevelPoint = decimal.Parse(row[1].ToString().Trim().Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim());
            }
            catch { model.LevelPoint = 0; }
            model.Orner = "0";  //1属于每日一练表 0属于普通题目表；
            model.personFlag = "";
            model.categoryFlag = this.courseId;
            model.categorypath = getCategoryPath(this.courseId);
            model.createTime = DateTime.Now;
            model.createPerson = "excel";
            model.courseId = this.txtCourseId.Value.Trim();
            model.Description = row[6].ToString().Trim().Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("【解析】", "").Trim();
            #region answer
            model.questionAnswer = row[5].ToString().Trim().ToUpper().Replace("##", ",").Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
            if (model.questionType == "判断题")
            {
                model.questionAnswer = (model.questionAnswer == "T") ? "A" : "B";
            }
            model.questionAnswer = model.questionAnswer.Replace("A", "A,").Replace("B", "B,").Replace("C", "C,").Replace("D", "D,").Replace("E", "E,").Replace(",,", ",");
            #endregion
            model.orderBy = sn;
            model.shFlag = int.Parse(this.ddlSH.SelectedValue);

            //重复判断
            if (this.txtCourseId.Value.Trim() != "")
            {
                if (bll.GetList("questionTitle='" + model.questionTitle + "' and courseId='" + this.txtCourseId.Value.Trim() + "'").Tables[0].Rows.Count > 0)
                {
                    exsitNum++;
                    continue;
                }
            }
            else
            {
                if (bll.GetList("questionTitle='" + model.questionTitle + "' and questionType='" + model.questionType + "' and categorypath='" + model.categorypath + "'").Tables[0].Rows.Count > 0)
                {
                    exsitNum++;
                    continue;
                }
            }


            if (bll.Add(model) > 0)
            {
                sn++;
                if (model.questionType == "判断题")
                {
                    AddQuestionAnswer(model.sysnumber, "A", "对", 1);
                    AddQuestionAnswer(model.sysnumber, "B", "错", 2);
                }
                else
                {
                    string[] answerArr = row[4].ToString().Trim().Split(new string[] { "##" }, StringSplitOptions.None);
                    string strA = answerArr.Length >= 1 ? answerArr[0].Trim() : "";
                    string strB = answerArr.Length >= 2 ? answerArr[1].Trim() : "";
                    string strC = answerArr.Length >= 3 ? answerArr[2].Trim() : "";
                    string strD = answerArr.Length >= 4 ? answerArr[3].Trim() : "";
                    string strE = answerArr.Length >= 5 ? answerArr[4].Trim() : "";


                    if (strA != "") { AddQuestionAnswer(model.sysnumber, "A", strA, 1); }
                    if (strB != "") { AddQuestionAnswer(model.sysnumber, "B", strB, 2); }
                    if (strC != "") { AddQuestionAnswer(model.sysnumber, "C", strC, 3); }
                    if (strD != "") { AddQuestionAnswer(model.sysnumber, "D", strD, 4); }
                    if (strE != "") { AddQuestionAnswer(model.sysnumber, "E", strE, 5); }
                }
                totalNum++;
                if (this.courseId != "" && this.sjid != "") { AddIntoSJ(this.sjid, model.sysnumber); }//课后练习
            }
            else
            {
                errorNum++;
            }
        }



        Response.Write("<script>alert('成功导入" + totalNum + "条数据[失败" + errorNum + "条][重复" + exsitNum + "条]，重复信息不导入!');window.close();</script>");
    }

    /// <summary>
    /// 目录链
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string getCategoryPath(string id)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DBInstance");
        DataTable dtGoods = ado.ExecuteSqlDataset("select * from t_goods_info where goodsid='" + id + "'").Tables[0];
        if (dtGoods.Rows.Count > 0)
        {
            if (dtGoods.Rows[0]["JobType"].ToString() == "SubGoods")
            {
                DataTable dtGoodsTop = ado.ExecuteSqlDataset("select * from t_goods_info where goodsid='" + dtGoods.Rows[0]["CategoryId"].ToString() + "'").Tables[0];
                if (dtGoodsTop.Rows.Count > 0)
                {
                    return "," + dtGoodsTop.Rows[0]["CategoryId"].ToString() + "," + dtGoodsTop.Rows[0]["goodsid"].ToString() + "," + id;
                }
            }
            else
            {
                return "," + dtGoods.Rows[0]["CategoryId"].ToString() + "," + id;
            }
        }
        return "," + id;
    }


    private int AddQuestionAnswer(string questionSys, string key, string keyText, int orderby)
    {
        ModelTestQuestonAnswer modelTestA = new ModelTestQuestonAnswer();
        modelTestA.sysnumber = Guid.NewGuid().ToString();
        modelTestA.questionSysnumber = questionSys;
        modelTestA.AnswerKey = key.Trim().Replace("'", "’").Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
        modelTestA.AnswerValue = keyText.Trim().Replace("'", "’").Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
        modelTestA.OrderBy = orderby;
        return new DalTestQuestonAnswer().Add(modelTestA);
    }

    #region Excle导入
    protected DataSet GetExcelData(string phyPath,ref string error)
    {
        if (System.IO.File.Exists(phyPath) == false) { return null; }

        try
        {
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + phyPath + ";Extended Properties='Excel 8.0;IMEX=1'";
            System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(strCon);
            string strCom = "SELECT * FROM [Sheet1$]";
            Conn.Open();
            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, Conn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "[Sheet1$]");
            Conn.Close();
            return ds;
        }
        catch(Exception ee)
        {
            error = ee.Message;
            return null;
        }
    }

    protected string UploadFile(FileUpload fileUpload, string uploadWebDir)
    {
        if (fileUpload.FileName.Length > 4 && fileUpload.HasFile)
        {
            string oldFileName = System.IO.Path.GetFileName(fileUpload.FileName);
            string fileExtName = System.IO.Path.GetExtension(fileUpload.FileName);

            if (fileExtName.ToLower() != ".xls") { return ""; }
            string phyDir = Server.MapPath(uploadWebDir) + DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "/") + "/";
            string webDir = this.ResolveUrl(uploadWebDir) + DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "/") + "/";
            string fileName = Guid.NewGuid().ToString() + fileExtName;
            int fileSize = (int)fileUpload.FileContent.Length / 1024;
            if (fileSize == 0) { fileSize = 1; }
            if (!System.IO.Directory.Exists(phyDir)) { System.IO.Directory.CreateDirectory(phyDir); }
            fileUpload.SaveAs(phyDir + fileName);
            return webDir + fileName;
        }
        else { return ""; }
    }
    #endregion

    protected void AddIntoSJ(string sjid, string tiid)
    {
        DataTable dt = new NGShop.Bll.TableObject("T_Test_day").Util_GetList("*", "sysnumber='" + sjid + "'");
        if (dt.Rows.Count > 0)
        {
            string questions = dt.Rows[0]["questions"].ToString();
            if (questions.IndexOf(tiid) == -1)
            {
                questions += tiid + ",";
            }
            new NGShop.Bll.TableObject("T_Test_day").Util_UpdateBat("questions='" + questions + "'", "sysnumber='" + sjid + "'");
        }
    }
}
