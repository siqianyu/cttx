<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DivDialog.ascx.cs" Inherits="Controls_DivDialog" %>
<div id="overlay_<%=this.DialogId %>" style="position:absolute;top:0;left:0;width:100%;height:100%;opacity:0.5;display:none;"></div>
<div id="<%=this.DialogId %>" style="position:absolute;top:100px;left:100px;width:<%=this.DialogWidth%>px;height:<%=this.DialogHeight%>px;background:#fff;border:1px solid #eaeaea;display:none;">
<h2 style="font-size:12px;height:18px;text-align:right;background:#eaeaea;border-bottom:1px solid #eaeaea;padding:5px;cursor:move; margin:-1px 0 0 0">
<p style="float:left; margin:-1px 0 0 0; padding:2px 0 0 0" ><%=this.DialogTitle%></p>
<span id="<%=this.DialogId %>_Close" style="color:#000;cursor:pointer;background:#aaa;border:1px solid #eaeaea;padding:0 2px;">×</span></h2>
<p style="text-align:center">
<iframe src="<%=this.DialogIFrameSrc %>"></iframe>
</p>
<p style="position: absolute;height:30px; width:100%;background:#eaeaea; bottom:-16px;*bottom:0; text-align:right; border-top:1px solid #eaeaea;"><input style=" margin:3px 5px 0 0" value="确定"  type="button" id="<%=this.DialogId %>_OK" > <input  style=" margin:3px 5px 0 0" value="关闭"  type="button"  onclick="Controls_DivDialog_Hide()" ></p>
</div>
<script language="javascript">
    function Controls_DivDialog_Show() {
        var o = document.getElementById("<%=this.DialogId %>");
        o.style.display = "";
    }
    function Controls_DivDialog_Hide() {
        var o = document.getElementById("<%=this.DialogId %>");
        o.style.display = "none";
    }
</script>
<script language="javascript">
    window.onload = function () {
        var oWin = document.getElementById("<%=this.DialogId %>");
        var oLay = document.getElementById("overlay_<%=this.DialogId %>");
        var oBtn = document.getElementById("<%=this.DialogId %>_OK");
        var oClose = document.getElementById("<%=this.DialogId %>_Close");
        var oH2 = oWin.getElementsByTagName("h2")[0];
        var bDrag = false;
        var disX = disY = 0;
        oBtn.onclick = function () {
            oLay.style.display = "";
            oWin.style.display = "block"
        };
        oClose.onclick = function () {
            oLay.style.display = "none";
            oWin.style.display = "none"

        };
        oClose.onmousedown = function (event) {
            (event || window.event).cancelBubble = true;
        };
        oH2.onmousedown = function (event) {
            var event = event || window.event;
            bDrag = true;
            disX = event.clientX - oWin.offsetLeft;
            disY = event.clientY - oWin.offsetTop;
            this.setCapture && this.setCapture();
            return false
        };
        document.onmousemove = function (event) {
            if (!bDrag) return;
            var event = event || window.event;
            var iL = event.clientX - disX;
            var iT = event.clientY - disY;
            var maxL = document.documentElement.clientWidth - oWin.offsetWidth;
            var maxT = document.documentElement.clientHeight - oWin.offsetHeight;
            iL = iL < 0 ? 0 : iL;
            iL = iL > maxL ? maxL : iL;
            iT = iT < 0 ? 0 : iT;
            iT = iT > maxT ? maxT : iT;

            oWin.style.marginTop = oWin.style.marginLeft = 0;
            oWin.style.left = iL + "px";
            oWin.style.top = iT + "px";
            return false
        };
        document.onmouseup = window.onblur = oH2.onlosecapture = function () {
            bDrag = false;
            oH2.releaseCapture && oH2.releaseCapture();
        };
    };
</script>