﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>题目增加选项</title>
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/H-ui.admin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/icheck/icheck.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/skin/default/skin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" /> 
</head>
<body>
<div class="page-container">
	<form action="" method="post" class="form form-horizontal" id="form-article-add">
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>选项：</label>
			<div class="formControls col-xs-8 col-sm-9">
                    <select name="" class="select" id="TitleItemId">

				    </select>
			</div>
		</div>
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>选项位置：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="TitleItemIndex" name=""/>

			</div>
		</div>

        <div class="row cl">
			<div class="col-xs-8 col-sm-9 col-xs-offset-4 col-sm-offset-2">
				<button onClick="save();" class="btn btn-secondary radius" type="button"><i class="Hui-iconfont">&#xe632;</i> 保存草稿</button>
				<button onClick="layer_close();" class="btn btn-default radius" type="button">&nbsp;&nbsp;取消&nbsp;&nbsp;</button>
			</div>
		</div>


	</form>
</div>

    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script> 
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/layer/2.1/layer.js")%>"></script> 
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/icheck/jquery.icheck.min.js")%>"></script> 
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery.validation/1.14.0/jquery.validate.min.js")%>"></script> 
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery.validation/1.14.0/validate-methods.js")%>"></script> 
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery.validation/1.14.0/messages_zh.min.js")%>"></script> 
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui/js/H-ui.js")%>"></script> 
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/js/H-ui.admin.js")%>"></script> 

<script type="text/javascript">
    var index = 1;
    $(document).ready(function () {
        //         var id = parent.$('#ExperimentTypeId').val(); //选中的实验类型ID
        //         var type = parent.$('#AddOrUpdate').val(); //AddOrUpdate

        //只有增加没有修改
        LoadAllTitleItem();
        LoadTitleItemMaxIndex();


    });


    //加载位置索引
    function LoadTitleItemMaxIndex() {
        var jsonData = { Id: parent.CurrentTitleInfoId };
        var option = {
            url: '<%:Url.Action("GetTitleItemAssocIndex","Manage") %>',
            type: 'POST',
            dataType: 'html',
            asyc: false,
            data: JSON.stringify(jsonData),
            contentType: 'application/json',
            success: function (result) {
                index = result;
                $("#TitleItemIndex").val(index);
            }
        }
        $.ajax(option)

    }

    //加载所有题目选项
    function LoadAllTitleItem() {
        var option = {
            url: '<%:Url.Action("GetTitleItem","Manage") %>',
            type: 'POST',
            dataType: 'html',
            asyc: false,
            contentType: 'application/json',
            success: function (result) {
                var jsonData = JSON.parse(result);
                if (jsonData.length <= 0) {
                    return;
                }
                for (var i = 0; i < jsonData.length; i++) {
                    $("#TitleItemId").append("<option value='" + jsonData[i].TitleItemId + "'>" + jsonData[i].TitleItemContent + "</option>");
                }
            }
        }
        $.ajax(option)
    }


    function IsExistTitleItem(titleinfoid, titleitemid) {
        var exist;
        var jsonData = { TitleInfoId: titleinfoid, TitleItemId: titleitemid };
        var option = {
            url: '<%:Url.Action("IsExistTitleItemAssoc","Manage") %>',
            type: 'POST',
            dataType: 'html',
            async: false,
            data: JSON.stringify(jsonData),
            contentType: 'application/json',
            success: function (result) {
                if (result > 0) {
                    exist = true;
                }
                else {
                    exit = false;
                }
            },
            error: function (e) {
                exist = true;
            }
        }
        $.ajax(option)
        return exist;
    }


    function save() {


        //选项ID
        var a = $("#TitleItemId").val();
        //选项位置
        var b = $("#TitleItemIndex").val();
        if (IsExistTitleItem(parent.CurrentTitleInfoId, a)) {
            layer.msg('选项已经存在！');
            return;
        }

        var mydate = new Date();
        var dateTime = mydate.getFullYear() + "-" + mydate.getMonth() + "-" + mydate.getDate() + " " + mydate.getHours() + ":" + mydate.getMinutes() + ":" + mydate.getSeconds();
        var jsonData = { TitleItemId: a, TitleItemIndex: b, TitleInfoId: parent.CurrentTitleInfoId, UpdateDateTime: dateTime };  //TitleItemAssocId



        var option = {
            url: '<%:Url.Action("AddTitleItemAssoc","Manage") %>',
            type: 'POST',
            data: JSON.stringify(jsonData),
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {
                if (result > 0) {

                    layer.msg('增加成功！');
                    index++;
                    $("#TitleItemIndex").val(index);
                    //重新加载父界面数据
                    parent.LoadTitleItem();


                }
                else {

                    layer.msg('增加失败！');

                }

            },
            error: function (e) {
                layer.msg('操作失败！');
            }

        }
        $.ajax(option);
    }

</script>
</body>
</html>
