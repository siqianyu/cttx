using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Text;

namespace Startech.Utils
{
    /// <summary>
    /// 提供向页面输出客户端代码实现特殊功能的方法
    /// </summary>
    /// <remarks>
    /// </remarks>

    public class JSUtility
    {


        /// <summary>
        /// 跳出提示框并跳转到其它页面
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="toURL">跳转地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }



        /// <summary>
        /// 向客户端发送函数KendoPostBack(eventTarget, eventArgument)
        /// 服务器端可接收__EVENTTARGET,__EVENTARGUMENT的值
        /// </summary>
        /// <param name="page">System.Web.UI.Page 一般为this</param>
        public static void JscriptSender(System.Web.UI.Page page)
        {

            page.RegisterHiddenField("__EVENTTARGET", "");
            page.RegisterHiddenField("__EVENTARGUMENT", "");
            string s = @"		
			<script language=Javascript>
				function KendoPostBack(eventTarget, eventArgument) 
				{
							var theform = document.forms[0];
							theform.__EVENTTARGET.value = eventTarget;
							theform.__EVENTARGUMENT.value = eventArgument;
							theform.submit();
						}
			</script>";

            page.RegisterStartupScript("sds", s);
        }

        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message)
        {
            Alert(message, false);
        }

        public static void Alert(object message)
        {
            Alert(message.ToString(), false);
        }

        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="message">窗口信息</param>
        /// <param name="IsClose">是否关闭本窗口</param>
        public static void Alert(string message, bool IsClose)
        {
            StringBuilder js = new StringBuilder();
            string colsejs=IsClose ? "window.close();" : string.Empty;
            js.AppendFormat("<Script language='JavaScript'>alert('{0}');{1}</script>", message, colsejs);
            HttpContext.Current.Response.Write(js.ToString());
        }
        /// <summary>
        /// 对话框模式
        /// </summary>
        /// <param name="message"></param>
        /// <param name="strWinCtrl"></param>
        public static void RtnRltMsgbox(object message, string strWinCtrl)
        {
            string js = @"<Script language='JavaScript'>
					 strWinCtrl = true;
                     strWinCtrl = if(!confirm('" + message + "'))return false;</Script>";
            HttpContext.Current.Response.Write(string.Format(js, message.ToString()));
        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent()
        {
            string js = @"<Script language='JavaScript'>					
                    parent.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 格式化为JS可解释的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string JSStringFormat(string s)
        {
            return s.Replace("\r", "\\r").Replace("\n", "\\n").Replace("'", "\\'").Replace("\"", "\\\"");
        }

        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshUrl()
        {
            string js = @"<Script language='JavaScript'>
					opener.document.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="url"></param>
        public static void OpenWebForm(string url)
        {

            /*功能:	新开页面去掉ie的菜单。。。						*/
            /*注释内容:								*/
            /*开始*/
            string js = @"<Script language='JavaScript'>
			//window.open('" + url + @"');
			window.open('" + url + @"','','height=100%,width=100%,top=0,left=0,location=no,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,directories=no');
			</Script>";
            /*结束*/
            /*…………………………………………………………………………………………*/


            HttpContext.Current.Response.Write(js);
        }

        public static void OpenWebForm(string url, string name, string future)
        {
            string js = @"<Script language='JavaScript'>
                     window.open('" + url + @"','" + name + @"','" + future + @"')
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void OpenWebForm(string url, string formName)
        {
            /*功能:	新开页面去掉ie的菜单。。。						*/
            /*注释内容:								*/
            /*开始*/
            string js = @"<Script language='JavaScript'>
			//window.open('" + url + @"','" + formName + @"');
			window.open('" + url + @"','" + formName + @"','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=yes,directories=no');
			</Script>";
            /*结束*/
            /*…………………………………………………………………………………………*/

            HttpContext.Current.Response.Write(js);
        }

        public static void OpenWebForm(string url,string formName, bool isFullScreen)
        {
            string js = @"<Script language='JavaScript'>";
            if (isFullScreen)
            {//全屏
                js += "var iWidth = 0;";
                js += "var iHeight = 0;";
                //js += "iWidth=window.screen.availWidth-10;";
                //js += "iHeight=window.screen.availHeight-50;";
                js += "iWidth=window.screen.availWidth;";
                js += "iHeight=window.screen.availHeight;";
                js += "var szFeatures ='width=' + iWidth + ',height=' + iHeight + ',top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=no,status=no,titlebar=no,toolbar=no,directories=no';";
                js += "window.open('" + url + @"','"+formName+"',szFeatures);";
            }
            else
            {//不全屏
                js += "window.open('" + url + @"','" + formName + "','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');";
            }
            js += "</Script>";
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>		
        /// 函数名:OpenWebForm	
        /// 功能描述:打开WEB窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="url">WEB窗口</param>
        /// <param name="isFullScreen">是否全屏幕</param>
        public static void OpenWebForm(string url, bool isFullScreen)
        {

            OpenWebForm(url, "", isFullScreen);
            //string js = @"<Script language='JavaScript'>";
            //if (isFullScreen)
            //{//全屏
            //    js += "var iWidth = 0;";
            //    js += "var iHeight = 0;";
            //    js += "iWidth=window.screen.availWidth-10;";
            //    js += "iHeight=window.screen.availHeight-50;";
            //    js += "var szFeatures ='width=' + iWidth + ',height=' + iHeight + ',top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=no,status=no,titlebar=no,toolbar=no,directories=no';";
            //    js += "window.open('" + url + @"','',szFeatures);";
            //}
            //else
            //{//不全屏
            //    js += "window.open('" + url + @"','','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');";
            //}
            //js += "</Script>";
            //HttpContext.Current.Response.Write(js);
        }

        public static string GoHistoryBack()
        {
            string js = @"<Script language='JavaScript'>
                    history.back(-1);  
                  </Script>";
            return js;
        }

        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url"></param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 指定的框架页面转换
        /// </summary>
        /// <param name="FrameName"></param>
        /// <param name="url"></param>
        public static void JavaScriptFrameHref(string FrameName, string url)
        {
            string js = @"<Script language='JavaScript'>
					
                    @obj.location.replace(""{0}"");
                  </Script>";
            js = js.Replace("@obj", FrameName);
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        ///重置页面
        /// </summary>
        public static void JavaScriptResetPage(string strRows)
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.CenterFrame.rows='" + strRows + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 函数名:JavaScriptSetCookie
        /// 功能描述:客户端方法设置Cookie
        /// 版本：1.0
        /// </summary>
        /// <param name="strName">Cookie名</param>
        /// <param name="strValue">Cookie值</param>
        public static void JavaScriptSetCookie(string strName, string strValue)
        {
            string js = @"<script language=Javascript>
			var the_cookie = '" + strName + "=" + strValue + @"'
			var dateexpire = 'Tuesday, 01-Dec-2020 12:00:00 GMT';
			//document.cookie = the_cookie;//写入Cookie<BR>} <BR>
			document.cookie = the_cookie + '; expires='+dateexpire;			
			</script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>		
        /// 函数名:GotoParentWindow	
        /// 功能描述:返回父窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="parentWindowUrl">父窗口</param>		
        public static void GotoParentWindow(string parentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    this.parent.location.replace('" + parentWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>		
        /// 函数名:ReplaceParentWindow	
        /// 功能描述:替换父窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="parentWindowUrl">父窗口</param>
        /// <param name="caption">窗口提示</param>
        /// <param name="future">窗口特征参数</param>
        public static void ReplaceParentWindow(string parentWindowUrl, string caption, string future)
        {
            string js = "";
            if (future != null && future.Trim() != "")
            {
                js = @"<script language=javascript>this.parent.location.replace('" + parentWindowUrl + "','" + caption + "','" + future + "');</script>";
            }
            else
            {
                js = @"<script language=javascript>var iWidth = 0 ;var iHeight = 0 ;iWidth=window.screen.availWidth-10;iHeight=window.screen.availHeight-50;
							var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';this.parent.location.replace('" + parentWindowUrl + "','" + caption + "',szFeatures);</script>";
            }

            HttpContext.Current.Response.Write(js);
        }


        /// <summary>		
        /// 函数名:ReplaceOpenerWindow	
        /// 功能描述:替换当前窗体的打开窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="openerWindowUrl">当前窗体的打开窗口</param>		
        public static void ReplaceOpenerWindow(string openerWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.location.replace('" + openerWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>		
        /// 函数名:ReplaceOpenerParentWindow	
        /// 功能描述:替换当前窗体的打开窗口的父窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="openerWindowUrl">当前窗体的打开窗口的父窗口</param>		
        public static void ReplaceOpenerParentFrame(string frameName, string frameWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent." + frameName + ".location.replace('" + frameWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>		
        /// 函数名:ReplaceOpenerParentWindow	
        /// 功能描述:替换当前窗体的打开窗口的父窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="openerWindowUrl">当前窗体的打开窗口的父窗口</param>		
        public static void ReplaceOpenerParentWindow(string openerParentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent.location.replace('" + openerParentWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>		
        /// 函数名:CloseParentWindow	
        /// 功能描述:关闭窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        public static void CloseParentWindow()
        {
            string js = @"<Script language='JavaScript'>
					window.parent.opener=null;
                    window.parent.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void CloseOpenerWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void CloseSelfWindow()
        {
            string js = @"<Script language='JavaScript'>
					window.opener = null;
                    window.close();					 
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 函数名:ShowModalDialogJavascript	
        /// 功能描述:返回打开模式窗口的脚本	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <returns></returns>
        public static string ShowModalDialogJavascript(string webFormUrl)
        {
            string js = @"<script language=javascript>
							var iWidth = 0 ;
							var iHeight = 0 ;
							iWidth=window.screen.availWidth-10;
							iHeight=window.screen.availHeight-50;
							var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';
							showModalDialog('" + webFormUrl + "','_Self',szFeatures);</script>";
            return js;
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','_self','" + features + "');</script>";
            return js;
        }

        /// <summary>
        /// 函数名:ShowModalDialogWindow	
        /// 功能描述:打开模式窗口	
        /// 处理流程:
        /// 算法描述:
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <returns></returns>
        public static void ShowModalDialogWindow(string webFormUrl)
        {
            string js = ShowModalDialogJavascript(webFormUrl);
            HttpContext.Current.Response.Write(js);
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }

        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=no";
            ShowModalDialogWindow(webFormUrl, features);
        }

        //		public static void ShowModalDialogWindow(string webFormUrl,int width,int height)
        //		{
        //			string features = "edge:raised;scroll:0;status:0;help:0;resizable:1;dialogwidth:"+width.ToString()+"px" 
        //								+";dialoHeight:"+height.ToString()+"px";
        //			string js = ShowModalDialogJavascript(webFormUrl,features);
        //			HttpContext.Current.Response.Write(js);
        //		}

        public static void SetHtmlElementValue(string formName, string elementName, string elementValue)
        {
            string js = @"<Script language='JavaScript'>if(document." + formName + "." + elementName + "!=null){document." + formName + "." + elementName + ".value =" + elementValue + ";}</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static string GetArrayString(string[] stringArray)
        {
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
        }

        /// <summary>
        ///	获取某一字符串在字符串数组中出现的次数
        /// </summary>
        /// <param name="stringArray" type="string[]"></param>
        /// <param name="findString" type="string"></param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetStringCount(string[] stringArray, string findString)
        {
            int count = -1;
            string totalString = GetArrayString(stringArray);
            string subString = totalString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = totalString.Substring(subString.IndexOf(findString));
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 去掉最后一个逗号
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static string DelLastComma(string origin)
        {
            if (origin.IndexOf(",") == -1)
            {
                return origin;
            }
            return origin.Substring(0, origin.LastIndexOf(","));
        }
    }

}
