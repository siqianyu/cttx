using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using Startech.Member;

public partial class AppModules_Sysadmin_Seller_AddSeller : System.Web.UI.Page
{
    public string id;
    BllShopUser userbll = new BllShopUser();
    ModShopUser user = new ModShopUser();
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        id = (Request["id"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByNumber(Request["id"], 50);

        if (!IsPostBack)
        {
            InitData();

        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    protected void InitData()
    {
        if (this.id != "")
        {
            user = userbll.GetModel(this.id);
            if (user.isOwnSeller == 1 && user.AccoutsState == "Normal" && user.isOpen == 1)
            {
                trOwn.Visible = true;
            }
            else
            {
                trOwn.Visible = false;
            }

            string sql2 = "select * from T_Shop_ApplyImage where shopid='" + id + "' order by addtime desc";
            DataSet ds2 = adoHelper.ExecuteSqlDataset(sql2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                //营业执照是否为空
                if (ds2.Tables[0].Rows[0]["Charter"].ToString() != "")
                {
                    SmallPic.Visible = true;
                    SmallPic.ImageUrl = ds2.Tables[0].Rows[0]["Charter"].ToString();
                    //BigPic.HRef = ds2.Tables[0].Rows[0]["Charter"].ToString();
                }
            }



            //开通帐号下显示的数据
            if (user != null)
            {
                //未开通帐号下显示的数据
                this.CompanyName.Text = user.CompanyName;
                this.ShopName.Text = user.ShopName;
                this.Contact.Text = user.LinkMan;
                this.Phone.Text = user.Phone;
                this.Mark.Text = user.Mark;
                this.address.Text = user.Address;
                rbAccountsState.SelectedValue = "Normal";
                this.area.Text = GetMarketInfo(user.MarketId);
                this.email.Text = user.Email;
                this.qq.Text = user.QQ;
                this.txtBZJ.Value = user.ShopMoney.ToString();
                this.txtOrder.Text = user.orderby != null ? user.orderby.ToString() : "0";
                DataSet ds = adoHelper.ExecuteSqlDataset("select * from T_Info_Category where categoryid='" + user.CategoryId + "' ");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    categroytype.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                }
                this.rbAccountsState.SelectedValue = (user.AccoutsState == null || user.AccoutsState.ToString() == "") ? "Unchecked" : user.AccoutsState;
                this.rbOpen.SelectedValue = user.isOpen.ToString();
                if (user.Passwrod == null && user.UserName == null)
                {

                    this.txtUserName.Text = null;
                }
                else
                {
                    this.txtUserName.Text = user.UserName;
                }
                this.categroytype.Text = GetCategoryName(id);
                ViewState["OldUserName"] = user.UserName == null ? "" : user.UserName;

            }

        }


    }




    /// <summary>
    /// 保存操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (this.id != "")
        {


            if (ViewState["OldUserName"].ToString() != this.txtUserName.Text.Trim())
            {
                if (CheckHasUserName(this.txtUserName.Text.Trim()) == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert('该帐号已经存在，请使用其他账号！');</script>");
                    return;
                }
            }


            user = userbll.GetModel(this.id);
            user.ShopMoney = DataConvert.DecimalC(txtBZJ.Value);
            user.isOpen = DataConvert.IntC(rbOpen.SelectedValue);
            user.orderby = DataConvert.IntC(txtOrder.Text);

            user.UserName = txtUserName.Text.Trim();
            user.Email = this.email.Text.Trim();
            user.QQ = this.qq.Text.Trim();

            user.AccoutsState = rbAccountsState.SelectedValue;
            if (userbll.Update(user) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert('保存成功！');layer_close_flesh();</script>");
            }

            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert('保存失败！');</script>");
            }
        }

        else
        {

            ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert('未传递参数！');</script>");
        }
    }



    protected bool CheckHasUserName(string oldusername)
    {
        object obj = adoHelper.ExecuteSqlScalar("select UserName from T_Shop_User where UserName='" + oldusername + "' ");
        if (obj == null) { return false; }
        else { return true; }
    }

    /// <summary>
    /// 获取上架卖的任务
    /// </summary>
    /// <param name="shopid"></param>
    /// <returns></returns>
    private string GetCategoryName(string shopid)
    {
        string names = string.Empty;
        try
        {
            DataTable dt = DalBase.Util_GetList(" select CategoryId from T_Shop_User where shopid='" + shopid + "'").Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows[0]["CategoryId"].ToString() != "")
            {
                DataTable dt2 = DalBase.Util_GetList(" select CategoryName from dbo.T_Goods_Category where CategoryId in('" + dt.Rows[0]["CategoryId"].ToString().Replace(",", "','") + "')").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        names += dt2.Rows[i]["CategoryName"].ToString() + ",";
                    }
                }
            }
        }
        catch
        {

        }
        if (names != "")
        {
            names.TrimEnd(',');
        }
        return names;
    }

    private string GetMarketInfo(string marketid)
    {
        try
        {
            string info = string.Empty;

            DataTable dt = DalBase.Util_GetList("select Market_name,Area_id,(select area_name from T_Base_Area where area_id=m.Area_id) as area_name from dbo.T_Base_Market m  where Market_id='" + marketid + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {

                info += dt.Rows[0]["area_name"].ToString() + "-" + dt.Rows[0]["Market_name"].ToString();
            }
            return info;

        }
        catch
        {
            return "";
        }
    }

}