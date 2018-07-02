using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace StarTech.ELife.ShortMsg
{
    public enum MsgType
    {
        /// <summary>
        /// 注册
        /// </summary>
        reg,
        /// <summary>
        /// 登陆
        /// </summary>
        login,
        /// <summary>
        /// 找回密码
        /// </summary>
        findpwd
    }
    public static class ShortMsgModelExtensions
    {
        public static void SendSms(this ShortMsgModel S)
        {
            S.FillFeild();//从数据库填充相应字段
            if (!string.IsNullOrEmpty(S.yzm)) //验证码不能为空
            {
                if (!string.IsNullOrEmpty(S.tel))
                {
                    bool r = new ShortMsgBll().Add(S);
                    if (r)
                    {
                        //用阿里短信接口发送
                        if (S.supplier.Equals("ali", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!string.IsNullOrEmpty(S.template))
                            {
                                if (!string.IsNullOrEmpty(S.smsSign))
                                {
                                    SendWithAli(S);
                                }
                            }
                        }
                        else  //用默认的云片网短信接口发送
                        {
                            //易开工?
                            if (!string.IsNullOrEmpty(S.smsSign))
                            {
                                SendWithYunPian(S);
                            }
                        }
                    }
                }

            }

        }
        /// <summary>
        /// 补充字段
        /// </summary>
        /// <param name="SMS"></param>
        private static void FillFeild(this ShortMsgModel SMS)
        {
            ShortMsgSupBll supBll = new ShortMsgSupBll();
            ShortMsgSupModel supMod = supBll.GetDefault();
            if (supMod != null) //数据库没有数据 
            {
                List<ShortMsgTempModel> tmpList = supMod.tempList;
                SMS.smsSign = supMod.smsSign;
                SMS.supplier = supMod.flag;
                SMS.sysnumber = Guid.NewGuid().ToString();
                SMS.statu = "sending";
                SMS.accessKeyID = supMod.accessKeyID;
                SMS.accessKeySecret = supMod.accessKeySecret;

                //sms.flag  为 reg   or  login 注册登陆
                ShortMsgTempModel tmpMod = tmpList.Find(a => a.isUse == 1 && a.flag == SMS.flag);
                if (tmpMod != null)
                {
                    SMS.template = tmpMod.templateCode;
                    SMS.templateParam = tmpMod.templateParam;
                }

            }

        }
        /// <summary>
        /// 阿里 短信接口
        /// </summary>
        /// <param name="SMS"></param>
        private static void SendWithAli(this ShortMsgModel SMS)
        {
            SMS.sendText = "{\"code\":\"" + SMS.yzm.Trim() + "\"}";
            if (SMS.statu == "sending")
            {
                string text = new AliSms.AliSms(SMS.accessKeyID, SMS.accessKeySecret)
                {
                    PhoneNumbers = SMS.tel,
                    SignName = SMS.smsSign,
                    TemplateCode = SMS.template,
                    TemplateParam = SMS.sendText
                }.Send();
                if (text == "OK")
                {
                    SMS.result = "";
                    SMS.statu = "ok";
                }
                else
                {
                    SMS.result = text;
                    SMS.statu = "error";
                }
                SMS.sendTime = new System.DateTime?(System.DateTime.Now);
                new ShortMsgBll().Update(SMS);
            }
        }
        /// <summary>
        /// 云片网 短信接口
        /// </summary>
        /// <param name="SMS"></param>
        private static void SendWithYunPian(this ShortMsgModel SMS)
        {
            SMS.sendText = SMS.templateParam.Replace("code", SMS.yzm.Trim());
            if (SMS.statu == "sending")
            {
                string text = new YunPianSms.YunPianSms(SMS.accessKeySecret)
                {
                    mobile = SMS.tel,
                    text = SMS.sendText

                }.Send();
                if (text == "OK")
                {
                    SMS.result = "";
                    SMS.remark = "1";
                    SMS.statu = "ok";
                }
                else
                {
                    SMS.result = text;
                    SMS.remark = "0|" + text;
                    SMS.statu = "error";
                }
                SMS.sendTime = new System.DateTime?(System.DateTime.Now);
                new ShortMsgBll().Update(SMS);
            }
        }
    }
}
