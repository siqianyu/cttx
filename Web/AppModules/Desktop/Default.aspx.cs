using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Desktop_Default :StarTech.Adapter.StarTechPage
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected string strOrderTable = "";
    protected string strMemberTable = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        GetNewsData();
    }


    public string GetLogData()
    {
        string log = "";// <li><a  class='Comm'>系统培训会议的通知<span>（01月07日）</span></a></li><li><a  class='All'>查看全部</a></li>";
        DataSet ds = adoHelper.ExecuteSqlDataset("select top 6 * from IACenter_UserActionLog where actionType='登录' and userName='" + this.UserName + "' order by addTime desc");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            log += "<li><a  class='Comm'>" + row["menuNameLevel1"] + "<span>（" + row["addTime"] + "）</span></a></li>";
        }
        return log;
    }

    /// <summary>
    ///获取站内最新数据
    /// </summary>
    protected void GetNewsData()
    {
        string strSQL = "";
        //获取最新订单
        strSQL += "select top 5 * from T_Order_Info order by orderTime desc;";
        //获取最新注册会员
        strSQL += "select top 5 * from T_Member_Info order by RegisterTiem desc;";

        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 2)
            return;
        DataTable dtOrder = ds.Tables[0];
        DataTable dtMember = ds.Tables[1];
        dtOrder.Columns.Add("goodsList");
        string goodsSql = "";
        for (int i = 0; i < dtOrder.Rows.Count; i++)
        {
            goodsSql += "select goodsName from T_order_Infodetail where orderid='"+dtOrder.Rows[i]["orderid"]+"';";
        }
        DataSet dsGoods = new DataSet();
        if (goodsSql != "")
            dsGoods = adoHelper.ExecuteSqlDataset(goodsSql);
        for (int i = 0; i < dsGoods.Tables.Count; i++)
        {
            string glist="";
            for (int j = 0; j < dsGoods.Tables[i].Rows.Count; j++)
            {
                if(j!=0)
                    glist+=",";
                 glist+= dsGoods.Tables[i].Rows[j][0];
            }
            dtOrder.Rows[i]["goodsList"]=glist;
        }

        for (int i = 0; i < dtOrder.Rows.Count; i++)
        {
            strOrderTable += "<tr role=\"row\" id=\"1\" tabindex=\"-1\" class=\"ui-widget-content jqgrow ui-row-ltr ui-state-hover\" style='font-size:12px;'>";
            strOrderTable += "<td role=\"gridcell\" style=\"text-align:center;\" title=\""+dtOrder.Rows[i]["orderId"]+"\" aria-describedby=\"startech_table_jqgrid_orderId\">"+dtOrder.Rows[i]["orderId"]+"</td>";
            strOrderTable += "<td role=\"gridcell\" style=\"text-align:center;\" class=\"goodsimgstyle\" title=\"" + dtOrder.Rows[i]["memberName"] + "\" aria-describedby=\"startech_table_jqgrid_memberName\">" + dtOrder.Rows[i]["memberName"] + "</td>";
            strOrderTable += "<td role='gridcell' style='text-align:center;' title='" + dtOrder.Rows[i]["orderTime"] + "' aria-describedby='startech_table_jqgrid_orderTime'>" + dtOrder.Rows[i]["orderTime"] + "</td>";
            strOrderTable += "<td role='gridcell' style='text-align:center;' title='" + dtOrder.Rows[i]["OrderAllMoney"] + "' aria-describedby='startech_table_jqgrid_orderAllMoney'>" + dtOrder.Rows[i]["OrderAllMoney"] + "</td>";
            strOrderTable += "<td role='gridcell' style='text-align:left;' title='" + dtOrder.Rows[i]["goodsList"] + "' aria-describedby='startech_table_jqgrid_goodsList'>" + dtOrder.Rows[i]["goodsList"] + "</td>";
            strOrderTable += "<td role='gridcell' style='text-align:center;' title='' aria-describedby='startech_table_jqgrid_cmd_col'><input type='button' class='CommonButon' value='详情' onclick='button_actions(\"detail\",\"" + dtOrder.Rows[i]["orderId"] + "\")'></td>";
            strOrderTable += "</tr>";
            //<td role='gridcell' style='text-align:center;' title='' aria-describedby='startech_table_jqgrid_cmd_col'><input type='button' class='CommonButon' value='详情' onclick='button_actions('detail','201504150915030100175')'></td></tr>';
        }


        for (int i = 0; i < dtMember.Rows.Count; i++)
        {
            strMemberTable += "<tr role=\"row\" id=\"1\" tabindex=\"-1\" class=\"ui-widget-content jqgrow ui-row-ltr ui-state-hover\" style='font-size:12px;'>";
            strMemberTable += "<td role=\"gridcell\" style=\"text-align:center;\" title=\"" + dtMember.Rows[i]["memberId"] + "\" aria-describedby=\"startech_table_jqgrid_orderId\">" + dtMember.Rows[i]["memberId"] + "</td>";
            strMemberTable += "<td role=\"gridcell\" style=\"text-align:center;\" class=\"goodsimgstyle\" title=\"" + dtMember.Rows[i]["memberName"] + "\" aria-describedby=\"startech_table_jqgrid_memberName\">" + dtMember.Rows[i]["memberName"] + "</td>";
            strMemberTable += "<td role='gridcell' style='text-align:center;' title='" + dtMember.Rows[i]["mobile"] + "' aria-describedby='startech_table_jqgrid_orderTime'>" + dtMember.Rows[i]["mobile"] + "</td>";
            strMemberTable += "<td role='gridcell' style='text-align:center;' title='" + dtMember.Rows[i]["truename"] + "' aria-describedby='startech_table_jqgrid_orderAllMoney'>" + dtMember.Rows[i]["trueName"] + "</td>";
            strMemberTable += "<td role='gridcell' style='text-align:center;' title='" + dtMember.Rows[i]["RegisterTiem"] + "' aria-describedby='startech_table_jqgrid_goodsList'>" + dtMember.Rows[i]["RegisterTiem"] + "</td>";
            strMemberTable += "<td role='gridcell' style='text-align:center;' title='' aria-describedby='startech_table_jqgrid_cmd_col'><input type='button' class='CommonButon' value='详情' onclick='button_actions(\"detail2\",\"" + dtMember.Rows[i]["memberId"] + "\")'></td>";
            strMemberTable += "</tr>";
            //<td role='gridcell' style='text-align:center;' title='' aria-describedby='startech_table_jqgrid_cmd_col'><input type='button' class='CommonButon' value='详情' onclick='button_actions('detail','201504150915030100175')'></td></tr>';
        }
    }
}