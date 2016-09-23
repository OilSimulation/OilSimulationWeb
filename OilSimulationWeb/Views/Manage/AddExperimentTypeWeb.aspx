<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/H-ui.admin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/icheck/icheck.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/skin/default/skin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" /> 
<!--     <link href="<%=Url.Content("~/Scripts/Exam/lib/webuploader/0.1.5/webuploader.css")%>" rel="stylesheet" type="text/css" />  -->
</head>
<body>
<div class="page-container">
	<form action="" method="post" class="form form-horizontal" id="form-article-add">
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>大类实验类型名称：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="TypeName1" name=""/>
			</div>
		</div>
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>小类实验类型名称：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="TypeName2" name=""/>
			</div>
		</div>

		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">描述：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="0" placeholder="" id="TypeDescribe" name=""/>
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
<!--     <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/js/comment.js")%>"></script> -->

<!--     <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/webuploader/0.1.5/webuploader.min.js")%>"></script> -->
    <!--     <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/ueditor/1.4.3/ueditor.config.js")%>"></script> -->
    <!--     <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/ueditor/1.4.3/ueditor.all.min.js")%>"> </script> -->
    <!--     <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/ueditor/1.4.3/lang/zh-cn/zh-cn.js")%>"></script>



<!--_footer 作为公共模版分离出去-->
<!-- <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>  -->
<!-- <script type="text/javascript" src="lib/layer/2.1/layer.js"></script>  -->
<!-- <script type="text/javascript" src="lib/icheck/jquery.icheck.min.js"></script>  -->
<!-- <script type="text/javascript" src="lib/jquery.validation/1.14.0/jquery.validate.min.js"></script>  -->
<!-- <script type="text/javascript" src="lib/jquery.validation/1.14.0/validate-methods.js"></script>  -->
<!-- <script type="text/javascript" src="lib/jquery.validation/1.14.0/messages_zh.min.js"></script>  -->
<!-- <script type="text/javascript" src="static/h-ui/js/H-ui.js"></script>  -->
<!-- <script type="text/javascript" src="static/h-ui.admin/js/H-ui.admin.js"></script>  -->
<!-- <script type="text/javascript" src="static/h-ui.admin/js/comment.js"></script>
<!--/_footer /作为公共模版分离出去-->

<!--请在下方写此页面业务相关的脚本-->
<!-- <script type="text/javascript" src="lib/webuploader/0.1.5/webuploader.min.js"></script>  -->
<!-- <script type="text/javascript" src="lib/ueditor/1.4.3/ueditor.config.js"></script> -->
<!-- <script type="text/javascript" src="lib/ueditor/1.4.3/ueditor.all.min.js"> </script> -->
<!-- <script type="text/javascript" src="lib/ueditor/1.4.3/lang/zh-cn/zh-cn.js"></script> -->
<script type="text/javascript">
    function save() {
        //layer_close();
        var a = $("#TypeName1").val();
        var b = $("#TypeName2").val();
        var c = $("#TypeDescribe").val();
        var varUrl;
        if (true)//增加
        {
            varUrl = '<%:Url.Action("AddExperimentType","Manage") %>';
        }
        else//修改
        {
            varUrl = '<%:Url.Action("UpdateExperimentType","Manage") %>';
        }
        var jsonData = { TypeId: 0, TypeName1: a, TypeName2: b, TypeDescribe: c, UpdateDateTime: 0 };
        var option = {
            url: varUrl,
            type: 'POST',
            data: JSON.stringify(jsonData),
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {
                $(obj).parents("tr").remove();
                layer.msg('已删除!', 1);
            }

        }
    }
//     function layer_close() {
// 
//     }
</script>
</body>
</html>
