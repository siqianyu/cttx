using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.SqlClient;
using StarTech.DBUtility;
using System.Text.RegularExpressions;
using System.Net;
using Startech.Log;
using System.Security.Cryptography;
/// <summary>
/// PubFunction 的摘要说明
/// </summary>
public class PubFunction
{
    public PubFunction()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    #region 字符串截取处理
    #region ShortWords
    /// <summary>
    /// 截取长度为colNum的字符串，剩下部分以...表示
    /// 中文算一个字符
    /// </summary>
    /// <param name="str"></param>
    /// <param name="colNum"></param>
    /// <returns></returns>
    public static string ShortWords(string str, int colNum)
    {
        if (str != null)
        {
            if (colNum < 0)
            {
                throw new Exception("The argument colNum must positive integer!");
            }

            if (str.Length > colNum)
            {
                str = String.Concat(str.Substring(0, colNum), "...");
            }
        }
        else
        {
            str = String.Empty;
        }

        return str;
    }
    #endregion

    #region CutString
    /// <summary>
    /// 截取长度为length的字符串
    /// 中文算两个字符
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string CutString(string str, int length)
    {
        if (str != null)
        {
            if (length <= 0)
            {
                throw new Exception("The argument length can't be zero or negative!");
            }

            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    //str = str.Substring(0, j) + "...";//剩下部分以...表示
                    str = str.Substring(0, j); //直接截取字符串 
                    break;
                }
                j++;
            }
        }
        else
        {
            str = String.Empty;
        }

        return str;
    }

    #endregion
    #endregion

    #region 提示框
    public string MessageBox(string strMessage)
    {
        string str = "<script type=\"text/javascript\">\r\n";
        str += "alert(\"" + strMessage + "\");\r\n";
        str += "</script>";

        return str;
    }

    public string MessageBox(string strMessage, string strUrl)
    {
        string str = "<script type=\"text/javascript\">\r\n";
        str += "alert(\"" + strMessage + "\");\r\n";
        str += "location.href=\"" + strUrl + "\";\r\n";
        str += "</script>";

        return str;
    }

    /// <summary>
    /// 特殊字符替换
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public string EnCode(string content)
    {
        string str1 = content.Replace("<", "&lt;");
        string str2 = str1.Replace(">", "&gt;");
        string str3 = str2.Replace("'", "&apos;");
        string str4 = str3.Replace(" ", "&nbsp;");
        string str5 = str4.Replace("\r\n", "<br>");
        string str6 = str5.Replace("\"", "&quot;");
        //string str7 = str6.Replace("&", "&amp;");
        return str6;

    }

    /// <summary>
    /// 关闭提示框
    /// </summary>
    /// <param name="bParent">父窗口</param>
    /// <returns>HTML脚本</returns>
    public string MessageBox_Close(bool bParent)
    {
        string str = "";
        if (bParent)
            str = "<script>window.opener.location=window.opener.location;</script>";
        str += "<script>window.close()</script>";

        return str;
    }
    #endregion

    #region 文件上传
    /// <summary>
    /// 文件上传 2007-04-05
    /// 返回：-1上传失败，0：文件重名，1：上传成功
    /// </summary>
    /// <param name="fleUp">FileUpload控件名</param>
    /// <param name="serverPath">服务器路径</param>
    public int fileUp(FileUpload fleUp, string serverPath)
    {
        //string fileContentType = fleUp.PostedFile.ContentType;
        //if (fileContentType == "application/msword")
        //{
        string clientPath = fleUp.PostedFile.FileName;                      // 客户端文件路径
        if (clientPath == "") return -1;
        FileInfo file = new FileInfo(clientPath);
        string fileName = file.Name;                                        // 文件名称

        if (!File.Exists(serverPath + fileName))
        {
            try
            {
                fleUp.SaveAs(serverPath + fileName);
                return 1;
            }
            catch
            {
                //Response.Write("<script language='javascript'>alert('提示：文件上传失败，失败原因：" + ex.Message + "');</script>");
                return -1;
            }
        }
        else
        {
            return 0;
        }
        //}
        //else
        //{
        //    Response.Write("<script language='javascript'>alert('提示：文件类型不符！请上传word文档！');window.location=window.location;</script>");
        //    Response.End();
        //}
    }

    /// <summary>
    /// 文件名另存为并上传 2007-05-20
    /// 返回：-1上传失败，0：文件重名，1：上传成功
    /// </summary>
    /// <param name="fleUp">FileUpload控件名</param>
    /// <param name="serverPath">服务器路径</param>
    /// <param name="strSaveAsName">另存为的文件名</param>
    public string fileUpSaveAs(FileUpload fleUp, string serverPath, string strSaveAsName)
    {
        string clientPath = fleUp.PostedFile.FileName;                      // 客户端文件路径
        if (clientPath == "") return "-1";
        FileInfo file = new FileInfo(clientPath);
        string fileName = file.Name;                                        // 文件名称

        if (!File.Exists(serverPath + fileName))
        {
            try
            {
                fleUp.SaveAs(serverPath + fileName);
                File.Copy(serverPath + fileName, serverPath + strSaveAsName + file.Extension);    //改名
                File.Delete(serverPath + fileName);                                 //删除原文件
                return file.Extension;
            }
            catch
            {
                return "-1";
            }
        }
        else
        {
            return "0";
        }
    }
    #endregion

    #region 公用下拉框、多选框
    /// <summary>
    /// 公用下拉框
    /// </summary>
    public void pubBindDrop(string strSql, DropDownList dlstName, string inputField)
    {
        //ZjicBase zjicDrop = new ZjicBase();
        //DataSet dsDrop = zjicDrop.getDataSet(strSql);

        //dlstName.DataSource = dsDrop;
        //dlstName.DataTextField = inputField;
        //dlstName.DataBind();
        //dlstName.Items.Insert(0, "");
        //dsDrop.Clear();
        //zjicDrop.ConnClose();
    }

    /// <summary>
    /// 公用下拉框
    /// </summary>
    public void pubBindDrop2(string strSql, DropDownList dlstName, string inputField, string valueField)
    {
        //ZjicBase zjicDrop = new ZjicBase();
        //DataSet dsDrop = zjicDrop.getDataSet(strSql);

        //dlstName.DataSource = dsDrop;
        //dlstName.DataTextField = inputField;
        //dlstName.DataValueField = valueField;
        //dlstName.DataBind();
        //dlstName.Items.Insert(0, "");
        //dsDrop.Clear();
        //zjicDrop.ConnClose();
    }

    /// <summary>
    /// 选定下拉框当前值 -- 以Value为判断条件
    /// </summary>
    /// <param name="dltName">DropDownList控件名</param>
    /// <param name="strText">原值</param>
    public void pubDrop(DropDownList dltName, string strText)
    {
        foreach (ListItem pubItem in dltName.Items)
        {
            if (pubItem.Value == strText)
            {
                pubItem.Selected = true;
            }
            else
            {
                pubItem.Selected = false;
            }
        }
    }

    /// <summary>
    /// 选定下拉框当前值 -- 以Text为判断条件
    /// </summary>
    /// <param name="dltName">DropDownList控件名</param>
    /// <param name="strText">原值</param>
    public void pubDropText(DropDownList dltName, string strText)
    {
        foreach (ListItem pubItem in dltName.Items)
        {
            if (pubItem.Text == strText)
            {
                pubItem.Selected = true;
            }
            else
            {
                pubItem.Selected = false;
            }
        }
    }

    /// <summary>
    /// 选定单选框当前值 2007-04-04
    /// </summary>
    /// <param name="dltName">RadioButtonList控件名</param>
    /// <param name="strText">原值</param>
    public void pubRdoDrop(RadioButtonList rdoName, string strText)
    {
        foreach (ListItem pubItem in rdoName.Items)
        {
            if (pubItem.Value == strText)
            {
                pubItem.Selected = true;
            }
            else
            {
                pubItem.Selected = false;
            }
        }
    }

    /// <summary>
    /// 获取多选框的值 2007-04-04
    /// </summary>
    /// <param name="chkName">CheckBoxList控件名</param>
    public string pubChkGet(CheckBoxList chkName)
    {
        string strReturn = "";
        foreach (ListItem pubItem in chkName.Items)
        {
            if (pubItem.Selected)
            {
                strReturn += pubItem.Value + "|";
            }

        }
        return strReturn;
    }

    /// <summary>
    /// 获取多选框的值（适用权限设定） 2007-9-30
    /// 选中为1，反之为0，返回数据如：10110011
    /// </summary>
    /// <param name="chkName">CheckBoxList控件名</param>
    /// <returns>返回值</returns>
    public string pubChkGetNum(CheckBoxList chkName)
    {
        string strReturn = "";
        foreach (ListItem pubItem in chkName.Items)
        {
            if (pubItem.Selected)
            {
                strReturn += 1;
            }
            else
            {
                strReturn += 0;
            }

        }
        return strReturn;
    }

    /// <summary>
    /// 选定多选框当前值（适用权限设定） 2007-9-30
    /// </summary>
    /// <param name="chkName">CheckBoxList控件名</param>
    public void pubChkSelNum(CheckBoxList chkName, string strText)
    {
        if (strText == "" || strText == null)
            return;

        int nRow = 0;
        foreach (ListItem pubItem in chkName.Items)
        {
            if (strText.Substring(nRow, 1) == "1")
            {
                pubItem.Selected = true;
            }
            else
            {
                pubItem.Selected = false;
            }
            nRow++;
        }
    }

    /// <summary>
    /// 按钮控制 2007-9-30
    /// 当前值为1：可用，0：不可用（灰掉按钮）
    /// </summary>
    ///<param name="btnName">Button控件名称</param>
    ///<param name="strPower">权限字符串</param>
    ///<param name="strCurNo">当前按钮序号</param>
    public void pubCtrlBtn(Button btnName, string strPower, int nCurNo)
    {
        if (strPower == "" || strPower == null)
            return;

        try
        {
            if (strPower.Substring(nCurNo, 1) == "1")
            {
                btnName.Enabled = true;
            }
            else
            {
                btnName.Enabled = false;
            }

        }
        catch { }
    }

    /// <summary>
    /// 按钮控制 2007-12-16
    /// 当前值为0：可用，1：不可用（灰掉文本框）
    /// </summary>
    ///<param name="btnName">Button控件名称</param>
    ///<param name="strPower">权限字符串</param>
    ///<param name="strCurNo">当前按钮序号</param>
    public void pubCtrlBtn2(HtmlInputText txtName, string strPower, int nCurNo)
    {
        if (strPower == "" || strPower == null)
            return;

        try
        {
            if (strPower.Substring(nCurNo, 1) == "0")
            {
                txtName.Disabled = true;                    //灰掉
                txtName.Attributes["class"] = "readInput"; //调用只读样式
            }
            else
            {
                txtName.Disabled = false;
            }

        }
        catch { }
    }

    /// <summary>
    /// 选定多选框当前值 2007-04-04
    /// </summary>
    /// <param name="dltName">CheckBoxList控件名</param>
    /// <param name="strText">原值</param>
    public void pubChkDrop(CheckBoxList chkName, string strText)
    {
        foreach (ListItem pubItem in chkName.Items)
        {

            if (strText.IndexOf(pubItem.Value) != -1)
            {
                pubItem.Selected = true;
            }
            else
            {
                pubItem.Selected = false;
            }
        }
    }

    /// <summary>
    /// 数值型下拉框（适合日期选择）
    /// </summary>
    /// <param name="startNum">开始值</param>
    /// <param name="endNum">结束值</param>
    /// <param name="curNum">当前选中框</param>
    public void pubNumDrop(DropDownList dlstName, int startNum, int endNum, int curNum)
    {
        for (int i = startNum; i <= endNum; i++)
        {
            dlstName.Items.Add(new ListItem(i.ToString()));
            if (i == curNum)
                dlstName.SelectedValue = curNum.ToString();
        }
    }
    #endregion

    #region 所有控件遍历
    /// <summary>
    /// 所有控件遍历为只读
    /// 2007-7-5
    /// </summary>
    public void controlsReadonly(System.Web.UI.Page pg)
    {
        foreach (System.Web.UI.Control Con in pg.Controls)
        {
            if (Con.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            {
                //遍历form内的所有控件，并初始化其值   
                foreach (System.Web.UI.Control formCon in Con.Controls)
                {
                    System.Type sType = formCon.GetType();
                    switch (sType.ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            ((System.Web.UI.WebControls.TextBox)formCon).ReadOnly = true;
                            break;
                        case "System.Web.UI.HtmlControls.HtmlInputText":
                            ((System.Web.UI.HtmlControls.HtmlInputText)formCon).Disabled = true;
                            break;
                        case "System.Web.UI.WebControls.CheckBox":
                            ((System.Web.UI.WebControls.CheckBox)formCon).Enabled = false;
                            break;
                        case "System.Web.UI.WebControls.CheckBoxList":
                            ((System.Web.UI.WebControls.CheckBoxList)formCon).Enabled = false;
                            break;
                        case "System.Web.UI.WebControls.DropDownList":
                            ((System.Web.UI.WebControls.DropDownList)formCon).Enabled = false;
                            break;
                        case "System.Web.UI.WebControls.RadioButtonList":
                            ((System.Web.UI.WebControls.RadioButtonList)formCon).Enabled = false;
                            break;
                        case "System.Web.UI.WebControls.Button":
                            ((System.Web.UI.WebControls.Button)formCon).Enabled = false;
                            break;
                        case "System.Web.UI.WebControls.FileUpload":
                            ((System.Web.UI.WebControls.FileUpload)formCon).Enabled = false;
                            break;
                    }
                }
            }
        }
    }
    #endregion

    #region 替换数字为中文
    /// <summary>
    /// 替换数字为中文
    /// </summary>
    public string repNumToChn(string strNum)
    {
        string strReturn = "";
        switch (strNum)
        {
            case "0":
                strReturn = "0";
                break;
            case "1":
                strReturn = "一";
                break;
            case "2":
                strReturn = "二";
                break;
            case "3":
                strReturn = "三";
                break;
            case "4":
                strReturn = "四";
                break;
            case "5":
                strReturn = "五";
                break;
            case "6":
                strReturn = "六";
                break;
            case "7":
                strReturn = "七";
                break;
            case "8":
                strReturn = "八";
                break;
            case "9":
                strReturn = "九";
                break;
            case "10":
                strReturn = "十";
                break;
            case "11":
                strReturn = "十一";
                break;
            case "12":
                strReturn = "十二";
                break;
        }
        return strReturn;
    }
    #endregion

    #region 字符去除
    /// <summary>
    /// 去除“'”、两边空格
    /// </summary>
    /// <param name="strChar"></param>
    /// <returns></returns>
    public string TrimChar(string strChar)
    {
        return strChar.Replace("'", "").Trim();
    }

    /// <summary>
    /// 去除或还原空格,回车符
    /// </summary>
    /// <param name="strChar"></param>
    /// <param name="bIsClear">True:去除,False:还原</param>
    /// <returns></returns>
    public string TrimParam(string strChar, Boolean bIsClear)
    {
        if (bIsClear)
            return strChar.Replace("'", "").Replace(" ", "&nbsp;").Replace("\r\n", "<br>");
        else
            return strChar.Replace("&nbsp;", " ").Replace("<br>", "\r\n");
    }

    /// <summary>
    /// 指定数量显示
    /// </summary>
    /// <param name="ob"></param>
    /// <param name="nLen"></param>
    /// <returns></returns>
    public static string Trim(object ob, int nLen)
    {
        string str = ob.ToString();
        if (str.Length > nLen)
        {
            return str.Substring(0, nLen);
        }
        else
        {
            return str;
        }
    }
    #endregion

    #region 汉字占2个字符，英文字母1个字符
    public string SubStringX(string inputString, int length)
    {
        string trimString = Regex.Replace(inputString, "<[^>]*>", "");
        trimString = trimString.Replace(" ", "");
        trimString = trimString.Replace("<hr>", "");
        trimString = trimString.Replace("<br>", "");
        trimString = trimString.Replace("<p>", "");
        trimString = trimString.Replace("&mdash;", "—");
        trimString = trimString.Replace("&gt;", ">");
        trimString = trimString.Replace("&rarr;", "→");
        trimString = trimString.Replace("&lt;", "<");

        if (Encoding.UTF8.GetByteCount(trimString) <= length * 2)
        {
            return trimString;
        }
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        string tempString = "";
        byte[] s = ascii.GetBytes(trimString);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            {
                tempLen += 2;
            }
            else
            {
                tempLen += 1;
            }
            tempString += trimString.Substring(i, 1);
            if (tempLen >= length * 2 - 3)
                break;
        }
        //如果截过则加上半个省略号
        if (System.Text.Encoding.Default.GetBytes(trimString).Length > length)
            tempString += "…";
        return tempString;
    }
    #endregion

    #region 数据集
    /// <summary>
    /// 返回数据集DataSet
    /// </summary>
    /// <param name="strSql">sql语句</param>
    /// <returns></returns>
    public static DataSet ExecuteSqlDataset(string strSql)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        return helper.ExecuteSqlDataset(strSql);
    }

    /// <summary>
    /// 返回数据集DataSet
    /// </summary>
    /// <param name="strSql">sql语句</param>
    /// <param name="sqlParams">sql参数</param>
    /// <returns></returns>
    public static DataSet ExecuteSqlDataset(string strSql, SqlParameter[] sqlParams)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        if (sqlParams == null)
        {
            return helper.ExecuteSqlDataset(strSql);
        }
        return helper.ExecuteSqlDataset(strSql, sqlParams);
    }


    /// <summary>
    /// 返回数据集DataSet
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="strWhere">条件</param>
    /// <param name="sqlParams">sql参数</param>
    /// <returns></returns>
    public static DataSet ExecuteSqlDataset(string tableName, string strWhere, SqlParameter[] sqlParams)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        string strSql = "select * from  " + tableName + " where 1=1 ";
        if (!string.IsNullOrEmpty(strWhere))
        {
            strSql += " and " + strWhere;
        }
        if (sqlParams == null)
        {
            return helper.ExecuteSqlDataset(strSql);
        }
        return helper.ExecuteSqlDataset(strSql, sqlParams);
    }

    /// <summary>
    /// 返回数据集DataTable
    /// </summary>
    /// <param name="strSql">sql语句</param>
    /// <returns></returns>
    public static DataTable ExecuteSqlDataTable(string strSql)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        return helper.ExecuteSqlDataset(strSql).Tables[0];
    }

    /// <summary>
    /// 返回数据集DataTable
    /// </summary>
    /// <param name="strSql">sql语句</param>
    /// <param name="sqlParams">sql参数</param>
    /// <returns></returns>
    public static DataTable ExecuteSqlDataTable(string strSql, SqlParameter[] sqlParams)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        if (sqlParams == null)
        {
            return helper.ExecuteSqlDataset(strSql).Tables[0];
        }
        return helper.ExecuteSqlDataset(strSql, sqlParams).Tables[0];
    }

    /// <summary>
    /// 返回数据集DataTable
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="strWhere">条件</param>
    /// <param name="sqlParams">sql参数</param>
    /// <returns></returns>
    public static DataTable ExecuteSqlDataTable(string tableName, string strWhere, SqlParameter[] sqlParams)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        string strSql = "select * from  " + tableName + " where 1=1 ";
        if (!string.IsNullOrEmpty(strWhere))
        {
            strSql += " and " + strWhere;
        }
        if (sqlParams == null)
        {
            return helper.ExecuteSqlDataset(strSql).Tables[0];
        }
        return helper.ExecuteSqlDataset(strSql, sqlParams).Tables[0];
    }

    /// <summary>
    /// 执行SQL
    /// </summary>
    /// <param name="strSql"></param>
    /// <returns></returns>
    public static int ExecuteSqlNonQuery(string strSql)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        return helper.ExecuteSqlNonQuery(strSql);
    }
    #endregion

    #region 绑定栏目
    /// <summary>
    /// 绑定栏目信息 
    /// </summary>
    /// author:郭仁欣
    /// <param name="reptname"></param>
    /// <param name="cateid">栏目编号</param>
    public void BindMenuItem(string sql, Repeater reptname, string cateid)
    {
        try
        {
            string sql1 = string.Empty;
            if (sql != null)
            {
                sql1 = sql;
            }
            else
            {
                sql1 = "select top 8 categoryid,title,newsid,releaseDate from T_news where categoryid ='" + cateid + "' and  Approved=1  order by releaseDate desc";
            }
            StringBuilder sb = new StringBuilder(sql1);
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            DataSet ds = adoHelper.ExecuteSqlDataset(sb.ToString());//得到改栏目的信息的结果集 
            reptname.DataSource = ds.Tables[0];
            reptname.DataBind();//绑定栏目详细信息
            ds.Clear();         //清空缓冲，Modify by:Tu Kequan,2008-9-3
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    #endregion

    #region 绑定栏目
    /// <summary>
    /// 绑定栏目信息 
    /// </summary>
    /// author:郭仁欣
    /// <param name="reptname"></param>
    /// <param name="cateid">栏目编号</param>
    public void BindMenuItem(string sql, DataList reptname, string cateid)
    {
        try
        {
            string sql1 = string.Empty;
            if (sql != null)
            {
                sql1 = sql;
            }
            else
            {
                sql1 = "select top 8 categoryid,title,newsid,releaseDate from T_news where categoryid ='" + cateid + "' and  Approved=1  order by releaseDate desc";
            }
            StringBuilder sb = new StringBuilder(sql1);
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            DataSet ds = adoHelper.ExecuteSqlDataset(sb.ToString());//得到改栏目的信息的结果集 
            reptname.DataSource = ds.Tables[0];
            reptname.DataBind();//绑定栏目详细信息
            ds.Clear();         //清空缓冲，Modify by:Tu Kequan,2008-9-3
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    #endregion

    #region 热点图片
    /// <summary>
    /// 显示热点图片
    /// </summary>
    /// <param name="newsid">新闻编号</param>
    /// <returns></returns>
    public bool IsVisible(string newsid)
    {
        string strSqlNews = "select * from t_news where newsid =" + newsid;
        DataSet dsNews = ExecuteSqlDataset(strSqlNews);
        if (dsNews.Tables[0].Rows.Count > 0)
        {
            string HotPic = dsNews.Tables[0].Rows[0]["HotPic"].ToString();
            string HotDays = dsNews.Tables[0].Rows[0]["HotDays"].ToString();
            string ReleaseDate = dsNews.Tables[0].Rows[0]["ReleaseDate"].ToString();
            if (HotPic == "1")
            {
                DateTime dt = Convert.ToDateTime(ReleaseDate);
                if (dt.AddDays(Convert.ToDouble(HotDays)).CompareTo(DateTime.Now) >= 0)
                {
                    return true;
                }
            }

        }
        return false;
    }
    /// <summary>
    /// 显示热点图片
    /// </summary>
    /// <param name="newsid">新闻编号</param>
    /// <returns></returns>
    public bool IsVisible1(string ReleaseDate)
    {
        DateTime dt = Convert.ToDateTime(ReleaseDate);
        if (dt.AddDays(Convert.ToDouble(3.0)).CompareTo(DateTime.Now) >= 0)
        {
            return true;
        }

        return false;
    }
    #endregion

    #region 增加日志2
    /// <summary>
    /// 添加日志
    /// </summary>
    /// <param name="applicationName">应用程序名称</param>
    /// <param name="firstItem">一级栏目名称</param>
    /// <param name="secondItem">二级栏目名称</param>
    /// <param name="actionType">操作</param>
    /// <param name="sql">获取操作记录的名称的语句</param>
    public static void InsertLog(string applicationName, string firstItem, string secondItem, string actionType, string sql, string id)
    {
        LogModel model = new LogModel();
        model.ApplicationName = applicationName;
        model.FirstItem = firstItem;
        model.SecondItem = secondItem;
        model.ActionType = actionType;

        if (sql != null && sql != "")
        {
            DataTable dt = ExecuteSqlDataTable(sql);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["title"] != null && dt.Rows[0]["title"].ToString() != "")
                {
                    model.Description = dt.Rows[0]["title"].ToString() + "&nbsp;(编号:" + id + ")";
                }
            }
        }

        new LogBLL().InsertLog(model);
    }
    #endregion

    #region 增加日志
    //public static void InsertLog(string sql, string function)
    //{
    //    DataTable dt = ExecuteSqlDataTable(sql);
    //    string operation = string.Empty;
    //    string categoryTitle = string.Empty;
    //    string title = string.Empty;
    //    if (dt.Rows.Count != 0)
    //    {
    //        if (dt.Rows[0]["cagetorytitle"] != null && dt.Rows[0]["cagetorytitle"] != "")
    //        {
    //            categoryTitle = dt.Rows[0]["cagetorytitle"].ToString();
    //        }
    //        operation += "“" + categoryTitle + "”";
    //        if (dt.Rows[0]["title"] != null && dt.Rows[0]["title"] != "")
    //        {
    //            title = dt.Rows[0]["title"].ToString();
    //            title = dt.Rows[0]["title"].ToString();
    //            operation += "“" + title + "”";
    //        }
    //        operation += function;
    //    }
    //   LogBLL _log = new  LogBLL ();
    //    _log.InsertLog(operation);
    //}
    #endregion

    #region 判断字符串是否为数字
    /// <summary>
    /// 是否为数字
    /// </summary>
    /// <param name="strNumber"></param>
    /// <returns></returns>
    public bool IsNumber(String strNumber)
    {
        if (strNumber == null || strNumber == "")
            return false;
        Regex objNotNumberPattern = new Regex("[^0-9.-]");
        Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
        String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
        String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
        Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

        return !objNotNumberPattern.IsMatch(strNumber) &&
               !objTwoDotPattern.IsMatch(strNumber) &&
               !objTwoMinusPattern.IsMatch(strNumber) &&
               objNumberPattern.IsMatch(strNumber);
    }

    public static bool IsNumeric(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
    }
    public static bool IsInt(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*$");
    }
    public static bool IsUnsign(string value)
    {
        return Regex.IsMatch(value, @"^\d*[.]?\d*$");
    }
    #endregion

    #region 全文检索
    /// <summary>
    /// 设置检索类型
    /// </summary>
    /// <returns></returns>
    public static DataTable SearchTypeTable()
    {
        DataTable dtSearchType = new DataTable();
        dtSearchType.Columns.Add("NAME", typeof(string));
        dtSearchType.Columns.Add("VALUE", typeof(int));
        dtSearchType.Rows.Add(new object[] { "----请选择栏目----", "0" });
        dtSearchType.Rows.Add(new object[] { "按标题搜索", "1" });
        dtSearchType.Rows.Add(new object[] { "按发布单位搜索", "2" });
        dtSearchType.Rows.Add(new object[] { "按关键字搜索", "3" });
        dtSearchType.Rows.Add(new object[] { "按正文内容搜索", "4" });
        return dtSearchType;
    }
    #endregion

    #region 获取节点下的所有子节点
    /// <summary>
    /// 返回节点集string
    /// </summary>
    /// <param name="CategoryId">节点</param>
    /// <returns></returns>
    public static string GetChildNodes(string CategoryId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@Nodes", SqlDbType.VarChar,1000)};
        parameters[0].Value = CategoryId;
        parameters[1].Direction = ParameterDirection.Output;
        adoHelper.ExecuteSPNonQuery("sp_GetChildNodes", parameters);
        return parameters[1].Value.ToString();
    }

    /// <summary>
    /// 返回节点集string
    /// </summary>
    /// <param name="CategoryId">节点</param>
    /// <returns></returns>
    public static string GetChildNodesByRoleId(string CategoryId, string RoleId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@RoleId", SqlDbType.Int,4),
					new SqlParameter("@Nodes", SqlDbType.VarChar,2000)};
        parameters[0].Value = CategoryId;
        parameters[1].Value = RoleId;
        parameters[2].Direction = ParameterDirection.Output;
        adoHelper.ExecuteSPNonQuery("sp_GetChildNodesByRole", parameters);
        return parameters[2].Value.ToString();
    }
    #endregion

    #region 判断上传图片格式是否争取
    public static bool IsPic(string uploadFileLastName)
    {
        string lastNameFilter = "jpeg,jpg,bmp,gif";
        string lastName = uploadFileLastName.Substring(uploadFileLastName.LastIndexOf('.') + 1);
        if (lastNameFilter.IndexOf(lastName) < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region 代码安全性字符替换
    public string ReplaceChar(string content)
    {
        content = content.Replace("<", "&lt;");
        content = content.Replace(">", "&gt;");
        content = content.Replace("'", "‘");
        content = content.Replace(",", "，");
        content = content.Replace("insert", "Ｉｎｓｅｒｔ");
        content = content.Replace("update", "Ｕｐｄａｔｅ");
        content = content.Replace("delete", "Ｄｅｌｅｔｅ");
        return content;

    }
    #endregion

    #region 验证库存量格式
    public static string getKcl(string allcount)
    {
        string temp = "";
        if (allcount == null || allcount == "")
        {
            temp = "0";
        }
        else
        {
            if (allcount.IndexOf('.') != -1)
            {
                temp = allcount.Substring(0, allcount.IndexOf('.'));
            }
        }

        return temp;
    }
    #endregion

    #region 验证销售量格式
    public static string getSalCount(string salCount)
    {
        string temp = "";
        if (salCount == null || salCount == "")
        {
            temp = "0";
        }
        else
        {
            temp = salCount;
        }
        return temp;
    }
    #endregion

    #region 过滤html代码及样式的方法
    /// 过滤html,js,css代码  
    /// <summary>
    /// 过滤html,js,css代码
    /// </summary>
    /// <param name="html">参数传入</param>
    /// <returns></returns>
    public static string CheckStr(string html)
    {
        System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        html = regex1.Replace(html, ""); //过滤<script></script>标记
        html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
        html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
        html = regex4.Replace(html, ""); //过滤iframe
        html = regex5.Replace(html, ""); //过滤frameset
        html = regex6.Replace(html, ""); //过滤frameset
        html = regex7.Replace(html, ""); //过滤frameset
        html = regex8.Replace(html, ""); //过滤frameset
        html = regex9.Replace(html, "");
        html = html.Replace("</strong>", "");
        html = html.Replace("<strong>", "");
        html = html.Replace("\r\n", "");
        html = html.Replace("\r", "");
        html = html.Replace("\n", "");
        html = html.Replace("\t", "");
        html = html.Replace(" ", "");
        return html;
    }
    #endregion

    #region 本地分页
    /// <summary>
    /// 本地分页(add by wxh on 2010.11.22)
    /// </summary>
    /// <returns></returns>
    public static DataTable LocalPaging(DataTable ds, string filter, string sort, int pageSize, int pageIndex, ref int totalCount)
    {
        DataTable dt = null;
        if (ds != null)
        {
            DataRow[] rows = ds.Select(filter, sort);
            dt = ds.Clone();

            pageIndex = (pageIndex < 1) ? 1 : (pageIndex + 1);
            int startPos = (pageIndex - 1) * pageSize;
            int endPos = startPos + pageSize - 1;
            for (int i = startPos; i <= endPos; i++)
            {
                if (i < rows.Length)
                {
                    dt.Rows.Add(rows[i].ItemArray);
                }

            }

            totalCount = rows.Length;
        }
        return dt;
    }
    #endregion

    #region  获得公开意见箱受理部门名称
    public static string GetTypeName(string strTypeid)
    {
        switch (strTypeid)
        {
            case "1":
                return "市政府办公厅";
            case "2":
                return "市财政厅";
            default: return "市政府办公厅";
        }
    }
    #endregion

    #region 获取所有会员身份列表
    /// <summary>
    /// 获取所有会员身份列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetMemberType()
    {
        //创建对象
        DataTable dtType = new DataTable();
        dtType.Columns.Add("TypeName", typeof(string));
        dtType.Columns.Add("UserType", typeof(int));

        //添加数据
        DataRow drNew = dtType.NewRow();
        drNew["TypeName"] = "社区";
        drNew["UserType"] = "0";
        dtType.Rows.Add(drNew);

        drNew = dtType.NewRow();
        drNew["TypeName"] = "工作人员";
        drNew["UserType"] = "1";
        dtType.Rows.Add(drNew);

        //返回值 
        return dtType;
    }
    #endregion

    #region 根据会员身份ID获取名称
    /// <summary>
    /// 根据会员身份ID获取名称
    /// </summary>
    /// <param name="id">会员身份ID</param>
    /// <returns></returns>
    public static string GetMemberTypeNameById(string id)
    {
        string strTypeName = string.Empty;
        switch (id)
        {
            case "0": strTypeName = "社区"; break;
            case "1": strTypeName = "工作人员"; break;
            default: strTypeName = "社区"; break;
        }
        return strTypeName;
    }
    #endregion

    #region 获取书记信箱领导列表
    /// <summary>
    /// 获取书记信箱领导列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetLeadList()
    {
        //创建对象
        DataTable dtType = new DataTable();
        dtType.Columns.Add("TypeName", typeof(string));
        dtType.Columns.Add("TypeID", typeof(int));

        //添加数据
        DataRow drNew = dtType.NewRow();
        drNew["TypeName"] = "叶榕";
        drNew["TypeID"] = "1";
        dtType.Rows.Add(drNew);

        drNew = dtType.NewRow();
        drNew["TypeName"] = "毛素云";
        drNew["TypeID"] = "2";
        dtType.Rows.Add(drNew);

        //返回值 
        return dtType;
    }
    #endregion

    #region 根据领导ID获取名称
    /// <summary>
    /// 根据领导ID获取名称
    /// </summary>
    /// <param name="id">领导ID</param>
    /// <returns></returns>
    public static string GetLeadNameById(string id)
    {
        string strTypeName = string.Empty;
        switch (id)
        {
            case "1": strTypeName = "叶榕"; break;
            case "2": strTypeName = "毛素云"; break;
            default: strTypeName = "叶榕"; break;
        }
        return strTypeName;
    }
    #endregion

    #region 书记信箱根据ID获取类别名称
    /// <summary>
    /// 书记信箱根据ID获取类别名称
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns></returns>
    public static string GetTypeNameById(string id)
    {
        string strTypeName = string.Empty;
        switch (id)
        {
            case "1": strTypeName = "咨询"; break;
            case "2": strTypeName = "投诉"; break;
            case "3": strTypeName = "建议"; break;
            case "4": strTypeName = "举报"; break;
            default: strTypeName = "咨询"; break;
        }
        return strTypeName;
    }
    #endregion

    /// <summary>
    /// 晴雨图像
    /// </summary>
    public static string ShowQYValueImg(float v)
    {
        string s1 = "";
        string s2 = "";
        if (v >= 90) { s2 = "晴：" + v + ""; s1 = "qy_26.jpg"; }
        if (v >= 80 && v < 90) { s2 = "多云：" + v + ""; s1 = "qy_27.jpg"; }
        if (v >= 70 && v < 80) { s2 = "阴：" + v + ""; s1 = "qy_28.jpg"; }
        if (v >= 60 && v < 70) { s2 = "雨：" + v + ""; s1 = "qy_29.jpg"; }
        if (v < 60) { s2 = "雨：" + v + ""; s1 = "qy_29.jpg"; }
        return "<img src='/ShineWeb/images/" + s1 + "' width='24' height='17' class='bzyq_img'/><span class='bzyq_txt'>" + s2 + "</span>";
    }

    public static string[] ShowQYValueImgArr(float v)
    {
        string s = ",";
        if (v >= 90) { s = "晴,qy_26.jpg"; }
        if (v >= 80 && v < 90) { s = "多云,qy_27.jpg"; }
        if (v >= 70 && v < 80) { s = "阴,qy_28.jpg"; }
        if (v >= 60 && v < 70) { s = "雨,qy_29.jpg"; }
        if (v < 60) { s = "雨,qy_29.jpg"; }
        return s.Split(',');
    }

    #region 天气预报采集相关方法
    /// <summary>
    /// 用WebClient进行采集(可能有乱码现象)
    /// </summary>
    public static string getCodeByUTF8(string url)
    {
        string returnStr = "";
        try
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            Byte[] pageData = wc.DownloadData(url);
            returnStr = Encoding.UTF8.GetString(pageData);
        }
        catch (Exception ee)
        {
            returnStr = ee.Message;
        }
        return returnStr;
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    public static string getInnerContent(string str, string startStr, string endStr)
    {
        try
        {
            int startPos = str.IndexOf(startStr) + startStr.Length;
            str = str.Substring(startPos, str.Length - startPos);
            int endPos = str.IndexOf(endStr);
            string returnStr = str.Substring(0, endPos);
            return returnStr;
        }
        catch { return ""; }
    }
    #endregion

    #region 根据条件获取某表或视图某些字段记录
    /// <summary>
    /// 根据条件获取某表或视图某些字段记录
    /// </summary>
    /// <param name="strWhere">条件</param>
    /// <param name="strTableName">表名</param>
    /// <param name="filedName">字段名称名</param>
    /// <returns></returns>
    public static DataSet GetRecordList(string strWhere, string strTableName, string strFiledName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        StringBuilder sbSql = new StringBuilder();
        sbSql.AppendFormat("select {0} from {1}", strFiledName, strTableName);
        if (!string.IsNullOrEmpty(strWhere))
        {
            sbSql.Append(" where " + strWhere);
        }
        return adoHelper.ExecuteSqlDataset(sbSql.ToString());
    }
    #endregion


    #region 创建复杂编号

    /// <summary>
    /// 生成复杂字符串
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    public string GetQRCodeId()
    {
        long timeTick = DateTime.Now.Ticks;
        Random r = new Random();
        int min= r.Next(0, 5);

        int num = r.Next(0, 999999);
        string numstr = num.ToString().PadLeft('0');
        string lastStr = "";
        for (int i = 0; i < 6; i++)
        {
            lastStr += numstr[i];
            if (min == i)
            {
                lastStr += timeTick.ToString();
            }
        }
        return lastStr;

    }

    #endregion
}
