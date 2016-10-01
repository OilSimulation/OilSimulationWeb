<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

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
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>习题或考试名称：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="ExercisesName" name=""/>
			</div>
		</div>
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2"><span class="c-red">*</span>考试描述：</label>
			<div class="formControls col-xs-8 col-sm-9">
				<input type="text" class="input-text" value="" placeholder="" id="ExercisesDescribe" name=""/>

			</div>
		</div>

        <div class="row cl">
            <label class="form-label col-xs-4 col-sm-2"><span class="c-red"></span>考试类型：</label>
            <div class="formControls col-xs-8 col-sm-9">
				<span class="select-box">
                    <select name="" class="select" id="ExercisesTypeId">
                        
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
        var id = parent.$('#ExperimentTypeId').val(); //选中的实验类型ID
        var type = parent.$('#AddOrUpdate').val(); //AddOrUpdate
        LoadExercisesType();
        if (type == 2) {//2:编辑,1:增加
            LoadData(id);
        }
    });

    //加载实验类型
    function LoadExercisesType() {
        var option={
            url:'<%:Url.Action("GetExperimentType","Manage") %>',
            type:'POST',
            dataType:'html',
            async: false,
            contentType:'application/json',
            success:function (result) {
                var jsonData = JSON.parse(result);
                if (jsonData.length <= 0) {
                    return;
                }
                $("#ExercisesTypeId").append("<option value='-1'>考试</option>");

                for (var i = 0; i < jsonData.length; i++) {
                    $("#ExercisesTypeId").append("<option value='" + jsonData[i].TypeId + "'>" + jsonData[i].TypeName1 + "\\" + jsonData[i].TypeName2 + "</option>");
                }
            }
        }
        $.ajax(option)
    }


    function LoadData(id) {//GetExperimentTypeById
        var jsonData = { Id: id };
        var option = {
            url: '<%:Url.Action("GetExercisesTestId","Manage") %>',
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

                $("#ExercisesName").val(jsonData.ExercisesName);
                $("#ExercisesDescribe").val(jsonData.ExercisesDescribe);
                $("#ExercisesTypeId").val(jsonData.ExercisesTypeId);

            }
        };
        $.ajax(option);
    }

    function ParentAddData(jsonData) {
        parent.LoadData();
    }

    function ParentUpdateData(jsonData) {
        var tr = parent.$("#ExercisesTestId" + jsonData.ExercisesTestId);

        var tds = tr.children();
        tds.eq(1).text(jsonData.ExercisesName);
        tds.eq(2).text(jsonData.TypeName1 + "\\" + jsonData.TypeName2);
        tds.eq(3).text(jsonData.ExercisesDescribe);
        tds.eq(5).text(jsonData.UpdateDateTime);

        //parent.RefDataTables();
    }


    //    function IsExistData(jsonData) {
    //        var exist;
    //        var option = {
    //            url: '<%:Url.Action("IsExistData","Manage") %>',
    //            type: 'POST',
    //            data: JSON.stringify(jsonData),
    //            dataType: 'html',
    //            async: false,
    //            contentType: 'application/json',
    //            success: function (result) {
    //                if (result > 0) {
    //                    layer.msg("实验类型已经存在！");
    //                    exist = true;
    //                }
    //                else {
    //                    exist = false;
    //                }
    //            },
    //            error: function (e) {
    //                layer.msg('操作失败！');
    //                exist = true;
    //            }

    //        }
    //        $.ajax(option);

    //        return exist;

    //    }

    //删除题目(删除考试与题目对照表)
    function deltitle(obj, id) {
        layer.confirm('确认要删除吗？', function (index) {
            var table = $('#titledata').DataTable();

            var rowf = table.row($(obj).parent().parent());
            rowf.remove().draw(false);
//            var jsonData = { Id: id };
//            //删除数据库中的数据 
//            var option = {
//                url: '<%:Url.Action("DelTitleInfo","Manage") %>',
//                type: 'POST',
//                data: JSON.stringify(jsonData),
//                dataType: 'html',
//                async: false,
//                contentType: 'application/json',
//                success: function (result) {
//                    //$(obj).parents("tr").remove().draw();
//                    if (result > 0) {
//                        var table = $('#datatables').DataTable();

//                        var rowf = table.row($(obj).parent().parent());
//                        rowf.remove().draw(false);

//                        layer.msg('已删除!');

//                    }

//                }
//            };
            //$.ajax(option);
        });
    }

    function save() {
        //layer_close();
        var id = parent.$('#ExperimentTypeId').val(); //选中的 考试ID
        var type = parent.$('#AddOrUpdate').val(); //AddOrUpdate

        var a = $("#ExercisesName").val();
        var b = $("#ExercisesDescribe").val();
        var c = $("#ExercisesTypeId").val();
        var TypeName = $("#ExercisesTypeId  option:selected").text();
        var TypeNames = TypeName.split('\\');



        var varUrl;
        if (type == 1)//增加
        {
            varUrl = '<%:Url.Action("AddExercisesTest","Manage") %>';
        }
        else//修改
        {
            varUrl = '<%:Url.Action("UpdateExercisesTest","Manage") %>';
        }
        var mydate = new Date();
        var dateTime = mydate.getFullYear() + "-" + mydate.getMonth() + "-" + mydate.getDate() + " " + mydate.getHours() + ":" + mydate.getMinutes() + ":" + mydate.getSeconds();
        var jsonData = { ExercisesTestId: id, ExercisesName: a, ExercisesDescribe: b, UpdateDateTime: dateTime, ExercisesTypeId: c, TypeName1: TypeNames[0], TypeName2: TypeNames[1] };

        //        var exit = IsExistData(jsonData);
        //        if (exit) {
        //            return;
        //        }

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
