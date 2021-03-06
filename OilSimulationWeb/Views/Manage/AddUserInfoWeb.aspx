﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
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
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>学号或工号：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="StudentNumber" name=""/>
			</div>
		</div>
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>姓名：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="StudentName" name=""/>
			</div>
		</div>		
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>密码：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="Password" name=""/>
			</div>
		</div>

		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>角色类型：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<span class="select-box">
                    <select name="" class="select" id="Role">
                        <option value="1">学生</option>
                        <option value="2">教师</option>
				    </select>
				</span>
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
    $(document).ready(function () {
        var id = parent.$('#StudentExamId').val(); 
        var type = parent.$('#AddOrUpdate').val(); //AddOrUpdate
        if (type == 2) {//2:编辑,1:增加
            LoadData(id);
        }

    });

    function LoadData(StudentExamId) {
        var jsonData = { Id: StudentExamId };
        var option = {
            url: '<%:Url.Action("GetUserInfoId","Examination") %>',
            type: 'POST',
            dataType: 'html',
            async: false,
            data: JSON.stringify(jsonData),
            contentType: 'application/json',
            success: function (result) {
                var jsonData = JSON.parse(result);
                if (jsonData.length <= 0) {
                    return;
                }

                $("#StudentNumber").val(jsonData.StudentNumber);
                $("#StudentName").val(jsonData.StudentName);
                $("#Password").val(jsonData.Password);
                $("#Role").val(jsonData.Type);
            }
        };
        $.ajax(option);

    }
    function ParentAddData(jsonData) {
        parent.LoadUser();
    }
    function save() {
        //layer_close();
        var id = parent.$('#StudentExamId').val(); 
        var type = parent.$('#AddOrUpdate').val(); 

        var a = $("#StudentNumber").val();
        var b = $("#StudentName").val();
        //var TitleTypeName = $("#Role  option:selected").text();
        var c = $("#Password").val();

        var d = $("#Role").val();
        //var e = $("#Score").val();

        var varUrl;
        if (type == 1)//增加
        {
            varUrl = '<%:Url.Action("AddUserInfo","Examination") %>';
        }
        else//修改
        {
            varUrl = '<%:Url.Action("UpdateUserInfo","Examination") %>';
        }
        var mydate = new Date();
        var dateTime = mydate.getFullYear() + "-" + mydate.getMonth() + "-" + mydate.getDate() + " " + mydate.getHours() + ":" + mydate.getMinutes() + ":" + mydate.getSeconds();
        var jsonData = { StudentExamId: id, StudentNumber: a, StudentName: b, Password: c, Type: d };
    

        var option = {
            url: varUrl,
            type: 'POST',
            data: JSON.stringify(jsonData),
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {
                if (result > 0) {
                    if (type == 1) {//增加
                        layer.msg('增加成功！');
                        ParentAddData(jsonData);
                    }
                    else {
                        layer.msg('编辑成功！');
                        ParentUpdateData(jsonData);
                    }

                }
                else {
                    if (type == 1) {
                        layer.msg('增加失败！');
                    }
                    else {
                        layer.msg('编辑失败！');
                    }
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
