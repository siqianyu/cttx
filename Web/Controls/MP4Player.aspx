<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MP4Player.aspx.cs" Inherits="Controls_MP4Player" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../js/video.js" type="text/javascript"></script>
    <link href="../js/video-js.css" rel="stylesheet" type="text/css" />
    <script>
        videojs.options.flash.swf = "video-js.swf";
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hidVideoPath" />
    <div>
        <!--视频播放start-->
        <video id="index_video" class="video-js vjs-default-skin vjs-big-play-centered" controls
            preload="auto" width="600" height="450" poster="index.png" data-setup="{}">
                        <source src="<%=this.path %>" type='video/mp4' />
                        <source src="<%=this.path %>" type='video/webm' />
                        <!--<source src="index_video.ogv" type='video/ogg' />-->
                    </video>
        <script type="text/javascript">
            var myPlayer = videojs('index_video');
            videojs("index_video").ready(function () {
                var myPlayer = this;
                myPlayer.play();
            });
        </script>
        <!--视频播放end-->
    </div>
    </form>
</body>
</html>
