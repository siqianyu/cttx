<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjQuestionMN.aspx.cs" Inherits="NGWeiXinRoot_YqxkjQuestionMN" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧-主页</title>
	<link href="style/common.css?v=0310" rel="stylesheet">
	<link href="style/index.css?v=0310" rel="stylesheet">
	<link href="style/iconfont.css?v=0310" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <script language="javascript">
        function to_restart() {
            location.href = location.href + Math.random();
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
    <!--课后练习开始-->
      
            <div class="exercise-box left" id="div_title">
                --
            </div>
            <div class="answer-box" id="div_answer">
                --
            </div>
            <div id="div_status" style="display: none">
                --
            </div>
            <div class="explain left" id="div_desc" style="color: Red; display: none;">
                --
            </div>
      
         <div class="btn-bottom">
		<div class="answer-btn left">
			<button id="submit" onclick="to_tj();return false;" style="display:none">提交</button>
            <button id="btnNext" onclick="go_next_timeout();return false;">下一题</button>
		</div>
		<!--课后练习结束-->
		<!--练习底部按钮开始-->
		<div class="exercise-btn-bar left">
			<button onclick="to_restart();return false;">重新开始</button><button onclick="to_record_panel();return false;">错题记录</button><button onclick="to_backcourse();return false;">返回</button>
		</div>
		<!--练习底部按钮开始-->
        </div>
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
        //$('.explain').show();
        var select_answer = order_str($("#hidSelectAnswer").val());
        if (select_answer == $("#hidAnswer").val()) {
            //$("#div_status").show().html("回答正确").css("color", "green");
            //$("#div_desc").css("color", "green");
            record_error("0", select_answer);
        } else {
            //$("#div_status").show().html("回答错误").css("color", "red");
            //$("#div_desc").css("color", "red");
            record_error("1", select_answer);
        }
        return false;
    }

    //重新排序下答案
    function order_str(str2) {
        var str = "";
        if (str2.indexOf("A") > -1) { str += "A"; }
        if (str2.indexOf("B") > -1) { str += "B"; }
        if (str2.indexOf("C") > -1) { str += "C"; }
        if (str2.indexOf("D") > -1) { str += "D"; }
        if (str2.indexOf("E") > -1) { str += "E"; }
        return str;
    }

    //下一题按钮
    function get_nextid() {
        var arr = $("#hidAllQuestions").val().split(',');
        if ($("#hidCurId").val() == "") {
            $("#hidCurId").val(arr[0]);
            return arr[0];
         }
        else {
            for (var i = 0; i < arr.length; i++) {
                if ($("#hidCurId").val() == arr[i]) {
                    if (i < arr.length - 1) { $("#hidCurId").val(arr[i + 1]); return arr[i + 1]; }
                    else {return "end"; }
                }
            }
        }
    }

    function go_next_timeout() {
        if (document.getElementById("div_desc").style.display == "none") {
            to_tj();
            $("#btnNext").hide();
            $("#submit").hide();
            get_question_next();
        } else {
            var next_id = get_nextid();
            get_question(next_id);
        }
    }

    function get_question_next() {
        var next_id = get_nextid();
        get_question(next_id);
    }

    function get_question(_id) {
        //alert(_id);
        if (_id == "end") {
            var computer_str = computer_record();
            var tmp = "答题结束，本次答题的正确率为【" + computer_str.split('|')[2] + "%】";
            layer.confirm('' + tmp + '', {
                btn: ['查看成绩', '返回'] //按钮
            }, function () {
                to_record_panel();
            }, function () {
                to_backcourse();
            });
            return false;
        }
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
                $("#btnNext").show();
                //$("#submit").show();
                $("#div_title").html(arr[0]);
                $("#hidAnswer").val(arr[1]);
                $("#hidSelectAnswer").val("");
                $("#div_desc").html(arr[2]);
                $("#div_answer").html(arr[3]);
                $("#hidQuestionType").val(arr[4]);
                $("#div_desc").hide();
                $("#div_status").hide();
            }
        });
    }
    get_question($("#hidCurId").val());


    function select_answer(id) {
        if ($("#hidQuestionType").val() == "判断题" || $("#hidQuestionType").val() == "单选题") {
            //取消之前选择的项目
            var arrAll = "A,B,C,D,E".split(',');
            for (var j = 0; j < arrAll.length; j++) { $("#" + arrAll[j]).removeClass('chosen'); }
            //点击答案选项加蓝色背景
            $("#" + id).addClass('chosen');
            $("#hidSelectAnswer").val(id);
        } else {
            //点击答案选项加蓝色背景
            var css = $("#" + id).attr("class");
            //alert(css);
            if (css.indexOf("chosen") == -1) {
                $("#" + id).addClass('chosen');
                var tmp = $("#hidSelectAnswer").val();
                if (tmp.indexOf(id) == -1) {
                    $("#hidSelectAnswer").val(tmp + id);
                }
            } else {
                $("#" + id).removeClass('chosen');
                var tmp = $("#hidSelectAnswer").val();
                $("#hidSelectAnswer").val(tmp.replace(id, ""));
            }
            //alert($("#hidSelectAnswer").val());
        }
    }

  
    //记录错误题目
    function record_error(ifadd, _useranswer) {
        $.ajax({
            type: "get",
            url: "YqxkjCourseInterface.ashx",
            data: { flag: "record_error", courseId: $("#hidCourseId").val(), questionid: $("#hidCurId").val(), mid: $("#hidMemberId").val(), code: ifadd, useranswer: _useranswer },
            dataType: "text",
            async: false,
            success: function (data) {
               
            }
        });
    }

    //错题练习
    function list_error_question() {
        $.ajax({
            type: "get",
            url: "YqxkjCourseInterface.ashx",
            data: { flag: "list_error_question", courseId: $("#hidCourseId").val(), mid: $("#hidMemberId").val(), r: Math.random() },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data != "") {
                    var len = data.split(',').length;
                    layer.msg("总共记录了 " + len + " 条错误答题。");
                    $("#hidAllQuestions").val(data);
                    $("#hidCurId").val("");
                    get_question();
                } else {
                    layer.msg("暂无错误答题记录。");
                }
            }
        });
    }

    //得分
    function computer_record() {
        htmlobj = $.ajax({ url: "YqxkjCourseInterface.ashx?flag=computer_record&courseId=" + $("#hidCourseId").val() + "&mid=" + $("#hidMemberId").val() + "&mytype=" + escape("模拟试题") + "", async: false });
        var str = htmlobj.responseText;
        return str;
    }
    </script>