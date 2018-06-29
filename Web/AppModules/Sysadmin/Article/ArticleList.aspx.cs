using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.ELife.Base;
using Startech.Category;


public partial class AppModules_Sysadmin_Article_ArticleList : StarTech.Adapter.StarTechPage
{
    AreaBll bll = new AreaBll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindArticleCateoryDrp();
        }
    }

    #region 绑定文章类别下拉框列表
    /// <summary>
    /// 绑定文章类别下拉框列表
    /// </summary>
    private void BindArticleCateoryDrp()
    {
        CategoryBLL bll = new CategoryBLL();

        ArrayList items = bll.GetSortedArticleCategoryItems(4, "1");
        IEnumerator e = items.GetEnumerator();
        while (e.MoveNext())
        {
            CategoryEntity item = (CategoryEntity)e.Current;
            this.ddlCategory.Items.Add(new ListItem(item.Name, item.Id));
        }
    }
    #endregion

}