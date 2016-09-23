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
    $(document).ready(function () {
        var id = parent.$('#ExperimentTypeId').val(); //选中的实验类型ID
        var type = parent.$('#AddOrUpdate').val(); //AddOrUpdate
        if (type == 2) {//2:编辑,1:增加
            LoadData(id);
        }
        
    });
    function LoadData(id) {//GetExperimentTypeById
        var option = {
            url: '<%:Url.Action("GetExperimentTypeById","Manage") %>',
            type: 'POST',
            dataType: 'html',
            async: false,
            data: id,
            contentType: 'application/json',
            success: function (result) {
                var d = eval(result);
                if (d.length <= 0) {
                    return;
                }
                var jsonData = JSON.parse(d);
                $("#TypeName1").val(jsonData.TypeName1);
                $("#TypeName2").val(jsonData.TypeName2);
                $("#TypeDescribe").val(jsonData.TypeDescribe);
            }
        };
        $.ajax(option);
    }

    function ParentAddData(jsonData) {
        parent.LoadData();

        return;
        var tbodyData = parent.$("#data"); //datatables
        var datatables = parent.$("#datatables").DataTable();
        var checkbox = '<input type="checkbox" value="" name="">'
        var tr = datatables.row.add([checkbox, jsonData.TypeName1, jsonData.TypeName2, jsonData.TypeDescribe, jsonData.UpdateDataTime]).draw(false);

        return;
        var tr = $("<tr></tr>").appendTo(tbodyData);
        tr.addClass("text-c");
        //Check
        var tdCheck = $("<td></td>").appendTo(tr);
        var input = $("<input></input>").appendTo(tdCheck);
        input.attr("type", "checkbox");
        input.attr("value", "");
        input.attr("name", "");
        //大类实验类型名称
        var tdType1 = $("<td></td>").appendTo(tr);
        tdType1.val(jsonData.TypeName1);
        //小类实验类型名称
        var tdType2 = $("<td></td>").appendTo(tr);
        tdType2.val(jsonData.TypeName2);

        var tdDescribe = $("<td></td>").appendTo(tr);
        tdDescribe.val(jsonData.TypeDescribe);

        var tdUpdateDateTime = $("<td></td>").appendTo(tr);
        tdUpdateDateTime.val(jsonData.UpdateDateTime);

        //修改与删除按钮
        var tdManage = $("<td></td>").appendTo(tr);
        tdManage.addClass("f-14 td-manage");
        var aEdit = $("<a></a>").appendTo(tdManage);
        aEdit.addClass("ml-5");
        aEdit.attr("style", "text-decoration:none");
        aEdit.attr("onClick", "article_edit('实验类型编辑','AddExperimentTypeWeb','" + jsonData.TypeId + "')");
        aEdit.attr("href", "javascript:;");
        aEdit.attr("title", "编辑");

        var aDel = $("<a></a>").appendTo(tdManage);
        aDel.addClass("ml-5");
        aDel.attr("style", "text-decoration:none");
        aDel.attr("onClick", "article_del('this','" + jsonData.TypeId + "')");
        aDel.attr("href", "javascript:;");
        aDel.attr("title", "删除");

        parent.LoadData();

    }

    function ParentUpdateData(jsonData) {
        var tr = parent.$("#TypeId" + jsonData.TypeId);
        var tds = tr.children();
        tds[1].html(jsonData.TypeName1);
        tds[2].html(jsonData.TypeName2);
        tds[3].html(jsonData.TypeDescribe);
        tds[4].html(jsonData.UpdateDateTime);

        //parent.RefDataTables();
    }


    function IsExistData(jsonData) {
        var exist;
        var option = {
            url: '<%:Url.Action("IsExistData","Manage") %>',
            type: 'POST',
            data: JSON.stringify(jsonData),
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {
                if (result > 0) {
                    layer.msg("实验类型已经存在！");
                    exist = true;
                }
                else {
                    exist = false;
                }


            },
            error: function (e) {
                layer.msg('操作失败！');
                exist = true;
            }

        }
        $.ajax(option);

        return exist;

    }

    function save() {
        //layer_close();
        var id = parent.$('#ExperimentTypeId').val(); //选中的实验类型ID
        var type = parent.$('#AddOrUpdate').val(); //AddOrUpdate

        var a = $("#TypeName1").val();
        var b = $("#TypeName2").val();
        var c = $("#TypeDescribe").val();
        var varUrl;
        if (type==1)//增加
        {
            varUrl = '<%:Url.Action("AddExperimentType","Manage") %>';
        }
        else//修改
        {
            varUrl = '<%:Url.Action("UpdateExperimentType","Manage") %>';
        }
        var mydate = new Date();
        var dateTime = mydate.getFullYear() + "-" + mydate.getMonth() + "-" + mydate.getDate() + " " + mydate.getHours() + ":" + mydate.getMinutes() + ":" + mydate.getSeconds();
        var jsonData = { TypeId: id, TypeName1: a, TypeName2: b, TypeDescribe: c, UpdateDateTime: dateTime };

        var exit = IsExistData(jsonData);
        if (exit) {
            return;
        }

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

                //                 $(obj).parents("tr").remove();
                //                 layer.msg('已删除!', 1);
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
