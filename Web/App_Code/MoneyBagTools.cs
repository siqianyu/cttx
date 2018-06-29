using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarTech.DBUtility;

/// <summary>
///电子钱包通用类库
/// </summary>
public class MoneyBagTools
{
	public MoneyBagTools()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 创建用户提现、收入明细数据（提现：xf，收入：cz）,fromsourceid数据写入源编号
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="moeny"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int CreateMoneyBagDetail(string memberId, decimal moeny, string type, string fromsourceid)
    {
        AdoHelper ado = AdoHelper.CreateHelper(Startech.Utils.AppConfig.DBInstance);

        object obj = ado.ExecuteSqlScalar("select account_id from T_Moneybag_AccountInfo where member_id='" + memberId + "'");
        string account_id = obj == null ? "" : obj.ToString();
        if (account_id == "")
        {
            //create
            account_id = Guid.NewGuid().ToString();
            string sqla = "INSERT INTO [T_Moneybag_AccountInfo]([account_id],[account_money],[account_state],[member_id],[createtime]) ";
            sqla += " VALUES('" + account_id + "',0,'Normal','" + memberId + "',getdate())";
            ado.ExecuteSqlNonQuery(sqla);
        }
        string sql = "INSERT INTO [T_Moneybag_AccountDetail]([detail_id],[account_id],[detail_type],[money],[from_source_id],[createtime])";
        sql += " VALUES('" + Guid.NewGuid().ToString() + "','" + account_id + "','" + type + "'," + moeny + ",'" + fromsourceid + "',getdate())";


        if (ado.ExecuteSqlNonQuery(sql) > 0)
        {
            //update total
            ado.ExecuteSqlNonQuery("update T_Moneybag_AccountInfo set account_money=(select isnull(sum(money),0) from T_Moneybag_AccountDetail where account_id='" + account_id + "')");
        }
        return 1;
    }


    /// <summary>
    /// 回滚提现操作
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="fromsourceid"></param>
    /// <returns></returns>
    public static int DelMoneyBagDetail(string memberId, string fromsourceid)
    {
        AdoHelper ado = AdoHelper.CreateHelper(Startech.Utils.AppConfig.DBInstance);

        object obj = ado.ExecuteSqlScalar("select account_id from T_Moneybag_AccountInfo where member_id='" + memberId + "'");
        string account_id = obj == null ? "" : obj.ToString();
        if (account_id != "")
        {
            string sql = "delete from T_Moneybag_AccountDetail where from_source_id='" + fromsourceid + "'";
            if (ado.ExecuteSqlNonQuery(sql) > 0)
            {
                //update total
                ado.ExecuteSqlNonQuery("update T_Moneybag_AccountInfo set account_money=(select isnull(sum(money),0) from T_Moneybag_AccountDetail where account_id='" + account_id + "')");
            }
            return 1;
        }
        return 0;
    }
}