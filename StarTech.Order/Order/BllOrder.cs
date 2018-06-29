using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace StarTech.Order.Order
{
    public class BllOrder
    {
        private DalOrder dal = new DalOrder();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModOrder model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModOrder model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModOrder GetModel(string orderId)
        {
            return dal.GetModel(orderId);
        }

        /// <summary>
        /// 支付订单
        /// </summary>
        public int PayOrder(string memberId, string orderId, string payInterfaceCode)
        {
            int r = 0;
            ModOrder mod = GetModel(orderId);
            if ((mod != null) && (mod.IsPay == 0))
            {
                r = new BllTableObject("T_Order_Info").Util_UpdateBat("IsPay=1,PayTime=getdate(),PayInterfaceCode='" + payInterfaceCode + "'", "isPay=0 and orderId='" + orderId + "' and memberId='" + memberId + "'");
                
                //进一步判断,更新销售量和库存数量
                UpdateGoodsStock(orderId);
            }
            return r;
        }


        /// <summary>
        /// 更新任务库存
        /// </summary>
        /// <returns></returns>
        public void UpdateGoodsStock(string orderId)
        {
            if (new BllTableObject("T_Order_Info").Util_CheckIsExsitData("orderId='" + orderId + "' and (IsComplete=1 or IsPay=1)") == true)
            {
                DataTable dtDetail = new BllTableObject(" T_Order_InfoDetail").Util_GetList("OrderType,GoodsId,GoodsCode,Quantity,GoodsFormate", "orderId='" + orderId + "'");
                foreach (DataRow row in dtDetail.Rows)
                {
                    int quantity = Int32.Parse(row["Quantity"].ToString());
                    if (row["OrderType"].ToString() == "PT")
                    {
                        if (row["GoodsFormate"].ToString() == "")
                        {
                            new BllTableObject("T_Goods_Info").Util_UpdateBat("TotalSaleCount=TotalSaleCount+" + quantity + ",Sotck=Sotck-" + quantity + "", "GoodsId='" + row["GoodsId"].ToString() + "'");
                        }
                        else
                        {
                            new BllTableObject("T_Goods_Info").Util_UpdateBat("TotalSaleCount=TotalSaleCount+" + quantity + "", "GoodsId='" + row["GoodsId"].ToString() + "'");
                            new BllTableObject("T_Goods_Formate").Util_UpdateBat("Stock=Stock-" + quantity + "", "GoodsId='" + row["GoodsId"].ToString() + "' and GoodsCode='" + row["GoodsCode"].ToString() + "'");
                        }
                    }
                    else if (row["OrderType"].ToString() == "TG")
                    {
                        new BllTableObject("T_Group_GoodsInfo").Util_UpdateBat("GroupNumber=GroupNumber+" + quantity + ",Stock=Stock-" + quantity + "", "GoodsId='" + row["GoodsId"].ToString() + "'");
                    }
                }
            }
        }

        /// <summary>
        /// 订单中包含的全部任务
        /// </summary>
        public string GetOrderAllGoods(string orderId)
        {
            string s = "";
            DataTable dtCate = new BllTableObject("T_Order_InfoDetail").Util_GetList("GoodsName,GoodsFormate,GoodsPic", "OrderId='" + orderId + "'");
            foreach (DataRow row in dtCate.Rows)
            {
                if (row["GoodsFormate"].ToString() != "")
                {
                    s += row["GoodsName"].ToString() + "(规格：" + row["GoodsFormate"].ToString() + "),";
                }
                else
                {
                    s += row["GoodsName"].ToString() + ",";
                }
            }
            if (s != "") { s = s.TrimEnd(','); }
            return s;
        }

        /// <summary>
        /// 显示会员的订单状态
        /// </summary>
        public string GetOrderStatusByMember(string IsPay, string IsSend, string IsGet, string IsMemberCancel, string IsComplete)
        {
            if (IsMemberCancel == "1") { return "已取消"; }
            if (IsComplete == "1") { return "已完成"; }
            if (IsPay == "0")
            {
                return "未付款";
            }
            else if (IsPay == "1")
            {
                if (IsSend == "0")
                {
                    return "已付款,未发货";
                }
                else
                {
                    if (IsGet == "0")
                    {
                        return "已发货";
                    }
                    else
                    {
                        return "已完成";
                    }
                }
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 显示后台管理的订单状态
        /// </summary>
        public string GetOrderStatusByAdmin(string IsPay, string IsSend, string IsGet, string IsMemberCancel, string IsComplete)
        {
            if (IsMemberCancel == "1") { return "已取消"; }
            if (IsComplete == "1") { return "已完成"; }
            if (IsPay == "0")
            {
                if (IsSend == "1")
                {
                    return "未付款,已发货";
                }
                else
                {
                    return "未付款";
                }
            }
            else if (IsPay == "1")
            {
                if (IsSend == "0")
                {
                    return "已付款,未发货";
                }
                else if (IsSend == "1")
                {
                    if (IsGet == "1")
                    {
                        return "已收货";
                    }
                    else
                    {
                        return "已发货";
                    }
                }
                else 
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
