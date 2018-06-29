<%@ WebHandler Language="C#" Class="AddMenu" %>

using System;
using System.Web;

public class AddMenu : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }


}