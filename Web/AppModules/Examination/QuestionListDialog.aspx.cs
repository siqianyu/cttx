using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Examination_QuestionListDialog : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hidCourseId.Value = Request.QueryString["courseId"] == null ? "" : Request.QueryString["courseId"];
        this.hidTestSysnumber.Value = Request.QueryString["testSysnumber"] == null ? "" : Request.QueryString["testSysnumber"];
    }
}