<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjQuestionView.aspx.cs" Inherits="NGWeiXinRoot_YqxkjQuestionView" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧-主页</title>
	<link href="style/common.css?v=0410" rel="stylesheet">
	<link href="style/index.css?v=0410" rel="stylesheet">
	<link href="style/iconfont.css?v=0410" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <script language="javascript">
        function to_restart() {
            location.href = "YqxkjQuestion.aspx?memberId=" + $("#hidMemberId").val() + "&fromfree=" + $("#hidFromFree").val() + "&source=" + $("#hidSource").val() + "&courseId=" + $("#hidCourseId").val() + "&r=" + Math.random();
            return false;
        }
        function to_backcourse() {
            if ($("#hidFromFree").val() == "free") {
                location.href = "YqxkjCourseFreeTest.aspx?id=" + $("#hidCourseId").val();
            } else {
                location.href = "YqxkjMemberCoursePlay.aspx?id=" + $("#hidCourseId").val();
            }
            return false;
        }

        function to_record_panel() {
            location.href = "YqxkjQuestionRecordPanel.aspx?memberId=" + $("#hidMemberId").val() + "&fromfree=" + $("#hidFromFree").val() + "&source=" + $("#hidSource").val() + "&courseId=" + $("#hidCourseId").val() + "&r=" + Math.random();
            return false;
        }
    </script>
</head>
<body class="bottom-pd" style="padding-bottom:8em">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidMemberId" runat="server" />
    <asp:HiddenField ID="hidCourseId" runat="server" />
    <asp:HiddenField ID="hidQuestionType" runat="server" />
    <asp:HiddenField ID="hidAnswer" runat="server" />
    <asp:HiddenField ID="hidSelectAnswer" runat="server" />
    <asp:HiddenField ID="hidAllQuestions" runat="server" />
    <asp:HiddenField ID="hidCurId" runat="server" />
    <asp:HiddenField ID="hidFromFree" runat="server" />
    <asp:HiddenField ID="hidSource" runat="server" />
    <asp:HiddenField ID="hidNextId" runat="server" />
    <asp:HiddenField ID="hidSNIndex" runat="server" />
    <asp:HiddenField ID="hidTotalCount" runat="server" />
<!--课后练习开始-->
		<div  class="exercise-box left" id="div_title">
			--
		</div>
		<div  class="answer-box" id="div_answer">
            --
		</div>
        <div id="div_status" style="display:none">
			--
		</div>
        <!--我的答案-->
        <div class="explain left" runat="server" id="div_myanswer" style="color:Red"></div>
		<!--解析-->
        <div class="explain left" id="div_desc" style="color:Blue"></div>
        <div class="btn-bottom">
		<div class="answer-btn left" style="display:none">
			<button id="submit" onclick="to_tj();return false;" style="display:none">提交</button>
            <button id="btnNext" onclick="go_next_timeout();return false;" style="display:none">下一题</button>
		</div>
		<!--课后练习结束-->
		<!--练习底部按钮开始-->
		<div class="exercise-btn-bar left">
			<button onclick="to_record_next();return false;">下一题</button><button onclick="to_record_panel();return false;">错题记录</button><button onclick="to_backcourse();return false;">返回课程</button>
		</div>
        </div>
		<!--练习底部按钮开始-->

		<!--底部菜单开始-->
		<uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    //提交按钮
    function to_tj() {
        $('.right').addClass('answer').siblings('.chosen').addClass('answer');
        $('.explain').show();
        return false;
    }

    //下一题
    function to_record_next() {
        var url = "YqxkjQuestionView.aspx?memberId=" + $("#hidMemberId").val() + "&fromfree=" + $("#hidFromFree").val() + "&source=" + $("#hidSource").val() + "&courseId=" + $("#hidCourseId").val() + "&curId=" + $("#hidNextId").val();
        location.href = url;
    }

    //获取当前的题目
    function get_question(_id) {
        layer.load(2); //加载时显示加载效果
        $.ajax({
            type: "get",
            url: "YqxkjCourseInterface.ashx",
            data: { flag: "get_question", id: _id },
            dataType: "text",
            async: false,
            success: function (data) {
                layer.closeAll('loading'); //关闭所有加载效果
                var arr = data.split("$$");
                //alert(arr.toString());
                //标题
                var _title = $("#hidSNIndex").val() + "/" + $("#hidTotalCount").val() + arr[0];
                $("#div_title").html(_title);
                $("#hidAnswer").val(arr[1]);
                $("#hidSelectAnswer").val("");
                $("#div_answer").html(arr[3]);
                $("#hidQuestionType").val(arr[4]);
                //解析
                if (arr[2] != "") {
                    var _desc = arr[2].indexOf("解析") > -1 ? arr[2] : "解析：" + arr[2];
                    $("#div_desc").html(_desc);
                }
            }
        });
    }

    //加载题目
    get_question($("#hidCurId").val());


    //直接显示答案
    to_tj();        


    </script>