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

public partial class Components_SelectKM : System.Web.UI.UserControl
{
    public string url;
    public string jiaImg;
    public string jianImg;

    protected void Page_Load(object sender, EventArgs e)
    {
        url = this.ResolveUrl(DialogUrl);
        if (url.IndexOf("?") == -1) { url += "?1=1"; }
        jiaImg = this.ResolveUrl("~/Images/skin/jia.png");
        jianImg = this.ResolveUrl("~/Images/skin/jian.png");
        this.img1.Src = jiaImg;
        this.img2.Src = jianImg;
        this.img1.Attributes.Add("onclick", "" + this.txtFnumberAndName.ClientID + "_ShowUsers()");
        this.img2.Attributes.Add("onclick", "" + this.txtFnumberAndName.ClientID + "_UnShowUsers()");
        this.img3.Src = this.ResolveUrl("~/skin/jia1.png");
        this.img4.Src = this.ResolveUrl("~/skin/jian1.png");

        if (Enable)
        {
            this.disabled_img.Style.Add("display", "none");
            this.enabled_img.Style.Add("display", "");
            this.txtFnumberAndName.Attributes.Add("readonly", "true");
        }
        else
        {
            this.disabled_img.Style.Add("display", "none");
            this.enabled_img.Style.Add("display", "none");
            this.txtFnumberAndName.Attributes.Add("readonly", "true");
        }

    }

    public int Width
    {
        set { this.txtFnumberAndName.Width = value; }
    }

    public int Height
    {
        set { this.txtFnumberAndName.Height = value; }
    }

    public string TextMode
    {
        set
        {
            if (value == "MultiLine")
            {
                this.txtFnumberAndName.TextMode = TextBoxMode.MultiLine;
                this.hidTextMode.Value = "MultiLine";
            }
            else
            {
                this.txtFnumberAndName.TextMode = TextBoxMode.SingleLine;
            }
        }
    }

    public string Text
    {
        get { return this.txtFnumberAndName.Text; }
        set { this.txtFnumberAndName.Text = value; }
    }

    public string Value
    {
        get { return this.hidSysId.Value; }
        set
        {
            this.hidSysId.Value = value;
        }
    }

    public bool Enable
    {
        get { return (ViewState["Enable"] == null) ? true : bool.Parse(ViewState["Enable"].ToString()); }
        set { ViewState["Enable"] = value; }
    }

    /// <summary>
    /// 无返回值的js行数
    /// </summary>
    public string CallJsFunctionByReturnVoid
    {
        get { return (ViewState["JsFunctionByReturnVoid"] == null) ? "" : ViewState["JsFunctionByReturnVoid"].ToString(); }
        set { ViewState["JsFunctionByReturnVoid"] = value; }
    }

    public string DialogUrl
    {
        get { return (ViewState["DialogUrl"] == null) ? "" : ViewState["DialogUrl"].ToString(); }
        set { ViewState["DialogUrl"] = value; }
    }

    public int DialogWidth
    {
        get { return (ViewState["DialogWidth"] == null) ? 740 : Int32.Parse(ViewState["DialogWidth"].ToString()); }
        set { ViewState["DialogWidth"] = value; }
    }

    public int DialogHeight
    {
        get { return (ViewState["DialogHeight"] == null) ? 620 : Int32.Parse(ViewState["DialogHeight"].ToString()); }
        set { ViewState["DialogHeight"] = value; }
    }
}

