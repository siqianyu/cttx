<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="NGWeiXinRoot_News_NewsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,viewport-fit=cover">
    <title>一起学会计吧</title>
    <link href="../WeUICss/style/weui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery.min.js" type="text/javascript"></script>
    <script src="../Js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
    body,html{height:100%;-webkit-tap-highlight-color:transparent}
    body{font-family:-apple-system-font,Helvetica Neue,Helvetica,sans-serif}
    ul{list-style:none}
    .page,body{background-color:#f8f8f8}
    .link{color:#1aad19}
    .page__hd{padding:20px}
    .page__bd_spacing{padding:0 15px}
    .page__ft{padding-top:40px;padding-bottom:10px;text-align:center}
    .page__ft img{height:50px}
    .page__ft.j_bottom{position:absolute;bottom:0;left:0;right:0}
    .page__title{text-align:left;font-size:20px;font-weight:400}
    .page__desc{margin-top:5px;color:#888;text-align:left;font-size:14px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidNewsId" runat="server" />
    <asp:HiddenField ID="hidFirstDirId" runat="server" />
<div class="page">
    <div class="page__hd">
        <h1 class="page__title"><%=this.title%></h1>
    </div>
    <div class="page__bd">
          <article class="weui-article">
            <h1><%=this.time%>&nbsp;&nbsp;一起学会计吧</h1>
            <section id="content_section">
                <section>
                    <p><%=this.content%>
                    </p>
                </section>
            </section>
        </article>

        <!--list_start-->
        <div class="weui-panel weui-panel_access">
            <div class="weui-panel__hd">相关信息</div>
            <div class="weui-panel__bd" id="list_news_div">
            </div>
            <div class="weui-panel__ft">
                <a href="NewsList.aspx" class="weui-cell weui-cell_access weui-cell_link">
                    <div class="weui-cell__bd">查看更多</div>
                    <span class="weui-cell__ft"></span>
                </a>    
            </div>
        </div>
        <!--list_end-->
    </div>
    <div class="page__ft">
        <a href="javascript:home()"><img src="../Images/yqxkj.png" /></a>
    </div>
</div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $('.weui-navbar__item').on('click', function () {
            $(this).addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
        });
    });
</script>
<script type="text/javascript">
    $(function () {
        var $searchBar = $('#searchBar'),
            $searchResult = $('#searchResult'),
            $searchText = $('#searchText'),
            $searchInput = $('#searchInput'),
            $searchClear = $('#searchClear'),
            $searchCancel = $('#searchCancel');

        function hideSearchResult() {
            $searchResult.hide();
            $searchInput.val('');
        }
        function cancelSearch() {
            hideSearchResult();
            $searchBar.removeClass('weui-search-bar_focusing');
            $searchText.show();
        }

        $searchText.on('click', function () {
            $searchBar.addClass('weui-search-bar_focusing');
            $searchInput.focus();
        });
        $searchInput
            .on('blur', function () {
                if (!this.value.length) cancelSearch();
            })
            .on('input', function () {
                if (this.value.length) {
                    $searchResult.show();
                } else {
                    $searchResult.hide();
                }
            })
        ;
        $searchClear.on('click', function () {
            hideSearchResult();
            $searchInput.focus();
        });
        $searchCancel.on('click', function () {
            cancelSearch();
            $searchInput.blur();
        });
    });
</script>
<script language="javascript" type="text/javascript">
    function list_news(category_id) {
        layer.load(2); //加载时显示加载效果
        $.ajax({
            type: "get",
            url: "NewsInterface.ashx",
            data: { flag: "list_news", categoryId: category_id, nonewsid: $("#hidNewsId").val(),topnum:10 },
            dataType: "text",
            async: true,
            success: function (data) {
                layer.closeAll('loading'); //关闭所有加载效果
                $("#list_news_div").html(data);
            }
        });
    }
    list_news($("#hidFirstDirId").val());

    $("#content_section").css("word-break", "break-all");
    $("#content_section p").css("word-break", "break-all");
    </script>