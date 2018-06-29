/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.language = 'zh-cn';
    config.filebrowserUploadUrl = "actions/ckeditorUpload";
    var pathName = window.document.location.pathname;
    //皮肤

    //config.skin = 'v2';

    //默认的宽度
    //config.width = 1000;

    //默认的宽度
    //config.height = 300;

    //表情显示每行个数

    //config.smiley_columns = 16;

    //去掉左下角的body和p标签

    //config.removePlugins = 'elementspath';

    //表情自定义
    //下载你想要的表情包，一般是gif格式的图片，将这些这些图片放在…../ckeditor/plugins/smiley/images中
    //当你的图标过多时，由于显示不开会导致一部分图标无法显示，而且表情图标对话框没有滚动条，为了避免这种问题，我们可以修改一下css文件。找到ckeditor/skins/v2/dialog.css（假定你使用了默认的皮肤v2），在最后一行添加如下代码.cke_dialog_ui_html{height:350px;overflow:auto;}
    //config.smiley_images = ['e100.gif', 'e101.gif', 'e102.gif', 'e103.gif', 'e104.gif', 'e105.gif'];

    //加入中文

    config.font_names = '宋体/宋体;黑体/黑体;仿宋/仿宋_GB2312;楷体/楷体_GB2312;隶书/隶书;幼圆/幼圆;微软雅黑/微软雅黑;' + config.font_names; //设置编辑器里字体下拉列表里的字体

    //所有的功能（备份）
    //        config.toolbar =
    //    [
    //    ['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
    //    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
    //    ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
    //    ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button',
    //    'ImageButton', 'HiddenField'],
    //    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    //    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
    //    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
    //    ['Link', 'Unlink', 'Anchor'],
    //    ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar',
    //    'PageBreak'],
    //    ['Styles', 'Format', 'Font', 'FontSize'],
    //    ['TextColor', 'BGColor'],
    //    ['Maximize', 'ShowBlocks', '-', 'About']
    //    ];


    config.toolbar =
[
['Source', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
['Checkbox', 'Radio', 'Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
['Outdent', 'Indent', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
['Link', 'Unlink', 'Anchor'],
['Maximize', 'ShowBlocks'],
['Styles', 'Format', 'Font', 'FontSize'],
['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar'],
['TextColor', 'BGColor']
];

    config.filebrowserBrowseUrl = '../../../ckfinder/ckfinder.html';

    config.filebrowserImageBrowseUrl = '../../../ckfinder/ckfinder.html?Type=Images';

    config.filebrowserFlashBrowseUrl = '../../../ckfinder/ckfinder.html?Type=Flash';

    config.filebrowserUploadUrl = '../../../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';

    config.filebrowserImageUploadUrl = '../../../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';

    config.filebrowserFlashUploadUrl = '../../../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
