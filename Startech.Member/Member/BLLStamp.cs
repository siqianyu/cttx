using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Startech.Member.Member
{
    public class BLLStamp
    {
        private DALStamp dal = new DALStamp();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModStamp model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModStamp model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {
            return dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModStamp GetModel(string stampNo)
        {
            return dal.GetModel(stampNo);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 卡号验证（1：通过验证，-1：卡号错误，-2：密码错误）
        /// </summary>
        public int CheckStamp(string stampNum, string stampPwd)
        {
            return dal.CheckStamp(stampNum, stampPwd);
        }

        /// <summary>
        /// 更新卡号为已使用
        /// </summary>
        public int UpdateToUse(string stampNum)
        {
            return dal.UpdateToUse(stampNum);
        }

        /// <summary>
        /// 批量制卡
        /// </summary>
        public int BatCreateCard(int stampMoney, int stampType, DateTime stampOutTime, int batCerateNum)
        {
            return dal.BatCreateCard(stampMoney, stampType, stampOutTime, batCerateNum);
        }

        /// <summary>
        /// 使用消费券充值（1：充值成功，小于0充值失败）
        /// </summary>
        public int RegStamp(string stampNum, string userID)
        {
            return dal.RegStamp(stampNum, userID);
        }

        /// <summary>
        /// 验证卡号是否为已使用(true:已使用，false:未使用)
        /// </summary>
        public bool CheckIsUse(string stampNum)
        {
            return dal.CheckIsUse(stampNum);
        }

        /// <summary>
        /// 统计用户使用消费卡的次数
        /// </summary>
        public int CountStampUseNumber(string userID)
        {
            return dal.CountStampUseNumber(userID);
        }
    }
}
