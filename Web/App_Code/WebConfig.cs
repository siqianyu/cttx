using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarTech.DBUtility;

/// <summary>
///WebConfig 的摘要说明
/// </summary>
public class WebConfig
{
	public WebConfig()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}


    /// <summary>
    /// 获取提现扣除的手续费（百分比）
    /// </summary>
    /// <returns></returns>
    public static decimal GetKouMoneySet()
    {
        AdoHelper ado = AdoHelper.CreateHelper(Startech.Utils.AppConfig.DBInstance);
        string sql = "select KouMoney from T_Web_Config";
        object obj = ado.ExecuteSqlScalar(sql);
        string m = obj == null ? "0" : obj.ToString();
        if (m == "") { m = "0"; }
        return decimal.Parse(m);
    }
}