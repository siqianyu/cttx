using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using System.Text;
using System.Data.SqlClient;

public partial class AppModules_Sysadmin_Base_AddCoupon : StarTech.Adapter.StarTechPage
{
    protected string _pageTitle = "";
    protected string couponId = "";
    protected string isShow = "style='display:none'";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.QueryString["id"] != null)
        {
            couponId = Request.QueryString["id"];
            if (!IsPostBack)
            {
                GetData();
            }
        }
        else
        {
            if (!IsPostBack)
            {
                string cid = "";
                while (cid == "")
                {
                    cid = new PubFunction().GetQRCodeId();
                }

                lbCouponId.Text = cid;
            }
        }
    }

    /// <summary>
    /// 获取修改信息
    /// </summary>
    protected void GetData()
    {
        string strSQL = "select * from T_Base_Coupon where couponId='"+couponId+"';";
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            lbCouponId.Text = dt.Rows[0][1].ToString();
            ddlCouponType.SelectedValue = dt.Rows[0][2].ToString();
            txtValue.Text = dt.Rows[0][3].ToString();
            txtContext.Text = dt.Rows[0][4].ToString();
            this.txtDay.Text = dt.Rows[0]["CouponDay"].ToString();
            txtStartTime.Text = dt.Rows[0][5].ToString();
            txtEndTime.Text = dt.Rows[0][6].ToString();
            QRCodeUrl.ImageUrl = dt.Rows[0][7].ToString();
            QRCodeUrl.Visible = true;
            cbEffect.Checked = dt.Rows[0][8].ToString()=="1"?true:false;
            lbUse.Text = dt.Rows[0][9].ToString()=="1"?"已使用":"未使用";
            txtRemark.Text = dt.Rows[0][10].ToString();
            if (ds.Tables[0].Rows[0]["CouponType"].ToString() == "DJ")
            {
                lbMember.Text = dt.Rows[0]["memberId"].ToString();
                ddlMember.SelectedValue = ds.Tables[0].Rows[0]["postObject"].ToString();
                cbQF.Checked = ds.Tables[0].Rows[0]["isPost"].ToString() == "0" ? false : true;
                isShow = "style='dispaly:block' ";
            }
        }
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string id=lbCouponId.Text.Trim();
        string coupId = id;
        string couponType=ddlCouponType.SelectedValue;
        decimal value=0;
        decimal.TryParse(txtValue.Text,out value);
        string context=txtContext.Text;
        DateTime startTime=DateTime.Now;
        DateTime.TryParse(hfStart.Value,out startTime);
        DateTime endTime=DateTime.Now;
        DateTime.TryParse(hfEnd.Value,out endTime);
        int couponDay = int.Parse(this.txtDay.Text.Trim());
        string qrCode=QRCodeUrl.ImageUrl;
        int isPost =cbQF.Checked ? 1 : 0;
        string postObject = ddlMember.SelectedValue;
        string getPlaceInfo = this.ddlGetPlaceInfo.SelectedValue;
        decimal minPrice = decimal.Parse(this.txtMinPrice.Text.Trim());

        DataTable dt=new DataTable();
        int index = 1;
        if (couponType == "DJ")
        {
            string strMem = "select memberId,tel from T_Member_Info ";
            if (postObject == "new")
            {
                strMem += " where memberId,tel not in (select memberId from T_Base_Coupon);";
            }
            DataSet ds = adoHelper.ExecuteSqlDataset(strMem);

            if (ds != null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                dt = ds.Tables[0];
            index = ds.Tables[0].Rows.Count;

        }
        
        string cid = (lbCouponId.Text).Trim();


        int effect = cbEffect.Checked ? 1 : 0;
        int use=lbUse.Text=="已使用"?1:0;
        string remark = txtRemark.Text;
        string couponCode = id;
        StringBuilder strSql = new StringBuilder();
        if (couponId == "")
        {
            int success = 0;
            int fail = 0;
            for (int i = 0; i < index; i++)
            {
                string memberId="";
                if(dt.Rows.Count>0)
                    memberId=dt.Rows[i][0].ToString();
                coupId = id + i.ToString().PadLeft(6, '0');
                if (couponType == "CZ")
                {
                    cid = "http://" + System.Web.Configuration.WebConfigurationManager.AppSettings["coupon"].ToString() + "/Coupon.aspx?type=CZ&cid=" + coupId;
                    QRCode qrc = new QRCode(cid);
                    qrCode = qrc.CreateQRCode();
                    QRCodeUrl.ImageUrl = qrCode;
                }
                strSql.Clear();
                strSql.Append("insert into T_Base_Coupon(");
                strSql.Append("CouponId,CouponType,CouponValue,Context,StartTime,EndTime,QRCodeUrl,isEffect,isUse,Remark,isPost,postObject,CouponCode,MemberId,CouponDay,GetPlaceInfo,minPrice)");
                strSql.Append(" values (");
                strSql.Append("@CouponId,@CouponType,@CouponValue,@Context,@StartTime,@EndTime,@QRCodeUrl,@isEffect,@isUse,@Remark,@isPost,@postObject,@CouponCode,@MemberId,@CouponDay,@GetPlaceInfo,@minPrice)");
                SqlParameter[] parameters = {
					new SqlParameter("@CouponId", SqlDbType.VarChar),
					new SqlParameter("@CouponType", SqlDbType.VarChar),
					new SqlParameter("@CouponValue", SqlDbType.Decimal),
					new SqlParameter("@Context", SqlDbType.VarChar),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@QRCodeUrl", SqlDbType.VarChar),
					new SqlParameter("@isEffect", SqlDbType.Int),
					new SqlParameter("@isUse", SqlDbType.Int),
					new SqlParameter("@Remark", SqlDbType.VarChar),
					new SqlParameter("@isPost", SqlDbType.Int),
					new SqlParameter("@postObject", SqlDbType.VarChar),
					new SqlParameter("@CouponCode", SqlDbType.VarChar),
					new SqlParameter("@MemberId", SqlDbType.VarChar),
                    new SqlParameter("@CouponDay", SqlDbType.Int),
                    new SqlParameter("@GetPlaceInfo", SqlDbType.VarChar),
                    new SqlParameter("@minPrice", SqlDbType.Decimal)
                                        };
                parameters[0].Value = coupId; ;
                parameters[1].Value = couponType;
                parameters[2].Value = value;
                parameters[3].Value = context;
                parameters[4].Value = startTime;
                parameters[5].Value = endTime;
                parameters[6].Value = qrCode;
                parameters[7].Value = effect;
                parameters[8].Value = use;
                parameters[9].Value = remark;
                parameters[10].Value = isPost;
                parameters[11].Value = postObject;
                parameters[12].Value = couponCode;
                parameters[13].Value = memberId;
                parameters[14].Value = couponDay;
                parameters[15].Value = getPlaceInfo;
                parameters[16].Value = minPrice;
                
                int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    if (isPost == 1)
                        new MobileInfo().GetMess(dt.Rows[i][1].ToString(), context);
                    success++;
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('添加成功');layer_close_refresh();</script>");
                    couponId = id;
                    
                }
                else
                    fail++;
            }

            //LogAdd.CreateLog(Session["UserId"].ToString(), "添加优惠卷“" + context + "共" + success + "张”", "添加", "", "", Request.Url.ToString());
            this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('成功添加优惠卷设置');layer_close_refresh();</script>");

        }
        else
        {
            //if (couponType == "CZ")
            //    cid = "http://" + System.Web.Configuration.WebConfigurationManager.AppSettings["coupon"].ToString() + "/Coupon.aspx?type=DJ&cid=" + cid;
            //QRCode qrc = new QRCode(cid);
            //qrCode = qrc.CreateQRCode();
            //QRCodeUrl.ImageUrl = qrCode;

            //StringBuilder strSql = new StringBuilder();
            strSql.Append("update  T_Base_Coupon set ");
            strSql.Append("CouponType=@CouponType,CouponValue=@CouponValue,Context=@Context,StartTime=@StartTime,EndTime=@EndTime,QRCodeUrl=@QRCodeUrl,isEffect=@isEffect,isUse=@isUse,Remark=@Remark,isPost=@isPost,postObject=@postObject,CouponDay=@CouponDay,GetPlaceInfo=@GetPlaceInfo,minPrice=@minPrice ");
            strSql.Append(" where CouponId=@CouponId");
            strSql.Append("");
            SqlParameter[] parameters = {
					//new SqlParameter("@CouponId", SqlDbType.VarChar,50),
					new SqlParameter("@CouponType", SqlDbType.VarChar,50),
					new SqlParameter("@CouponValue", SqlDbType.Int,4),
					new SqlParameter("@Context", SqlDbType.VarChar,50),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@QRCodeUrl", SqlDbType.VarChar),
					new SqlParameter("@isEffect", SqlDbType.Int),
					new SqlParameter("@isUse", SqlDbType.Int),
					new SqlParameter("@Remark", SqlDbType.VarChar),
					new SqlParameter("@isPost", SqlDbType.Int),
					new SqlParameter("@postObject", SqlDbType.VarChar),
                    new SqlParameter("@CouponDay", SqlDbType.Int),
					new SqlParameter("@CouponId", SqlDbType.VarChar),
                    new SqlParameter("@GetPlaceInfo", SqlDbType.VarChar),
                    new SqlParameter("@minPrice", SqlDbType.Decimal)
                                        };
            parameters[0].Value = couponType;
            parameters[1].Value = value;
            parameters[2].Value = context;
            parameters[3].Value = startTime;
            parameters[4].Value = endTime;
            parameters[5].Value = qrCode;
            parameters[6].Value = effect;
            parameters[7].Value = use;
            parameters[8].Value = remark;
            parameters[9].Value = isPost;
            parameters[10].Value = postObject;
            parameters[11].Value = couponDay;
            parameters[12].Value = id;
            parameters[12].Value = getPlaceInfo;
            parameters[13].Value = minPrice;
            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                //LogAdd.CreateLog(Session["UserId"].ToString(), "修改优惠卷“" + context + "”", "修改", "", "", Request.Url.ToString());
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
                GetData();

            }
        }
    }

    ///// <summary>
    ///// 生成二维码
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btQRCode_Click(object sender, EventArgs e)
    //{
    //    string cid = lbCouponId.Text;
    //    QRCode qrc=new QRCode(cid);
    //    QRCodeUrl.ImageUrl= qrc.CreateQRCode();

    //}
}