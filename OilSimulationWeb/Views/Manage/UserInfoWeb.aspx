<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>学生教师管理</title>



    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/H-ui.admin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/icheck/icheck.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/skin/default/skin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" /> 

</head>
<body>
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 学生教师管理 <span class="c-gray en">&gt;</span> 学生教师管理 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
<div class="page-container">

	<div class="cl pd-5 bg-1 bk-gray mt-20"> 
        <span class="l">
            <a href="javascript:;" onclick="datadel()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 批量删除</a> 
            <a class="btn btn-primary radius" onclick="adddata('添加用户','AddUserInfoWeb')" href="javascript:;"><i class="Hui-iconfont">&#xe600;</i> 添加数据</a>
        </span>  
    </div>
	<div class="mt-20">
		<table id="datatables" class="table table-border table-bordered table-bg table-hover table-sort">
			<thead>
				<tr class="text-c">
					<th width="25"><input type="checkbox" name="" value=""></th>
					<th width="100">学号或工号</th>
					<th width="100">姓名</th>
					<th width="100">密码</th>
                    <th width="100">角色</th>
                    <th width="120">最后登录时间</th>
                    <th width="120">操作</th>
				</tr>
			</thead>
			<tbody id="data" type="text">

			</tbody>
		</table>
        <input id="StudentExamId"  style="display:none"/>
        <input id="AddOrUpdate"  style="display:none"/>

	</div>
</div>





    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/My97DatePicker/WdatePicker.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/datatables/1.10.0/jquery.dataTables.min.js")%>"></script>
    
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/layer/2.1/layer.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui/js/H-ui.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/js/H-ui.admin.js")%>"></script>

<script type="text/javascript">

    $('.table-sort').DataTable({
        "aaSorting": [[1, "desc"]], //默认第几个排序
        "bStateSave": true, //状态保存
        "aoColumnDefs": [
	  {"orderable": false, "aTargets": [0, 3]}// 不参与排序的列
	]
    });

    $(document).ready(function () {
        
        LoadUser();
        //RefDataTables();
    })

    function RefDataTables() {



    }
    function LoadUser() {
        var option = {
            url: '<%:Url.Action("GetUserInfos","Examination") %>',
            type: 'POST',
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {

                ShowData(result);
            },
            error: function (e) {

            }

        };
        $.ajax(option);

    }

    function ShowData(dataValue) {

        var datatable = $("#datatables").DataTable();
        datatable.clear().draw(); //清空列表
        if (dataValue.length <= 0) {
            return;
        }
        var jsonData = JSON.parse(dataValue);

        for (var i = 0; i < jsonData.length; i++) {
            var aDel = $("<a></a>");
            aDel.addClass("ml-5");
            aDel.attr("style", "text-decoration:none");
            aDel.attr("onClick", "deldata(this,'" + jsonData[i].StudentExamId + "')");
            aDel.attr("href", "javascript:;");
            aDel.attr("title", "删除");
            var iDel = $("<i></i>").appendTo(aDel);
            iDel.addClass("Hui-iconfont");
            iDel.html("&#xe6e2;"); 

            var aEdit = $("<a></a>");
            aEdit.addClass("ml-5");
            aEdit.attr("style", "text-decoration:none");
            aEdit.attr("onClick", "updatedata('修改','AddUserInfoWeb','" + jsonData[i].StudentExamId + "')");
            aEdit.attr("href", "javascript:;");
            aEdit.attr("title", "编辑");
            var iEdit = $("<i></i>").appendTo(aEdit);
            iEdit.addClass("Hui-iconfont");
            iEdit.html("&#xe6df;");

            var ht = aDel[0].outerHTML;
            ht += aEdit[0].outerHTML;


            var row = datatable.row.add(['<input type="checkbox" value="1" name="">', jsonData[i].StudentNumber, jsonData[i].StudentName, jsonData[i].Password, jsonData[i].Type == 1 ? "学生" : "教师", jsonData[i].LoginDateTime, ht]).draw();
        }

    }

    function deldata(obj,StudentExamId) {
        layer.confirm('确认要删除吗？', function (index) {
            var jsonData = { Id: StudentExamId };
            //删除数据库中的数据 
            var option = {
                url: '<%:Url.Action("DelUserInfo","Examination") %>',
                type: 'POST',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    //$(obj).parents("tr").remove().draw();
                    if (result > 0) {
                        var table = $('#datatables').DataTable();

                        var rowf = table.row($(obj).parent().parent());
                        rowf.remove().draw(false);

                        layer.msg('已删除!');

                    }
                    else {
                        layer.msg("删除失败！");
                    }
                }
            };
            $.ajax(option);


        });

    }

    function updatedata(title, url, StudentExamId) {
        $("#StudentExamId").val(StudentExamId);
        $("#AddOrUpdate").val("2");
        layer_show(title, url, '', 400);
    }

    function adddata(title, url) {
        $("#AddOrUpdate").val("1");
        layer_show(title, url, '', 400);
    }


    </script>