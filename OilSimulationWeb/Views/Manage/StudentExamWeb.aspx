<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html>
<head>
<title>学生信息</title>

    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/H-ui.admin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/icheck/icheck.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/skin/default/skin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/zTree/v3/css/zTreeStyle/zTreeStyle.css")%>" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
    .table>tbody>tr>td{
        text-align:center;
        }
    </style>


</head>
<body class="pos-r">
<%--<div class="pos-a" style="width:150px;left:0;top:0; bottom:0; height:100%; border-right:1px solid #e5e5e5; background-color:#f5f5f5">
	<ul id="treeDemo" class="ztree">
	</ul>
</div>--%>
<div ><%--style="margin-left:150px;"--%>
	<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 学生成绩管理 <span class="c-gray en">&gt;</span> 学生成绩管理 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
	<div class="page-container">
<%--		<div class="cl pd-5 bg-1 bk-gray mt-20"> <span class="l"><label id="StudentName"></label><label id="StudentNumber"></label></span>  </div>
--%>		<div class="mt-20">
			<table id="datatableStudent" class="table table-border table-bordered table-bg table-hover table-sort">
				<thead>
					<tr class="text-c">

						<th width="100">学生ID</th>
						<th width="100">学生姓名</th>
                        <th width="60">学号</th>
                        <th width="60">操作时间</th>
					</tr>
				</thead>
				<tbody>

				</tbody>
			</table>
		</div>
        <div style="height:80px;"></div>
		<div class="mt-20">
			<table id="datatable" class="table table-border table-bordered table-bg table-hover table-sort">
				<thead>
					<tr class="text-c">

						<th width="100">考试名称</th>
                        <th width="60">考试描述</th>
                        <th width="60">考试总分</th>
                        <th width="60">考试成绩</th>
                        <th width="60">操作</th>
					</tr>
				</thead>
				<tbody>

				</tbody>
			</table>
		</div>
	</div>
</div>

    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/My97DatePicker/WdatePicker.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/datatables/1.10.0/jquery.dataTables.min.js")%>"></script>
    
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/layer/2.1/layer.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui/js/H-ui.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/js/H-ui.admin.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/zTree/v3/js/jquery.ztree.all-3.5.js")%>"></script>


<script type="text/javascript">
    //当前选中的学生ID
    var CurrentStudentExamId = -1;
    //当前选中的考试ID
    //var CurrentExercisesTestId = -1;
    var CurrentExercisesTestId = -1;
    var CurrentTotleScore = 0;
    var CurrentStudentScore = 0;
    var zNodes;
    var setting = {
        view: {
            dblClickExpand: false,
            showLine: false,
            selectedMulti: false
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "pId",
                rootPId: ""
            }
        },
        callback: {
            beforeClick: function (treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj("tree");
                if (treeNode.isParent) {
                    zTree.expandNode(treeNode);
                    return false;
                } else {
                    CurrentStudentExamId = treeNode.StudentExamId;
//                    $("#StudentName").val(treeNode.name);
//                    $("#StudentNumber").val(treeNode.StudentNumber);
                    LoadExercisesTest();
                    return true;
                }
            }
        }
    };

    $("#datatable").dataTable({
        "aaSorting": [[1, "desc"]], //默认第几个排序
        "bStateSave": true, //状态保存
        "aoColumnDefs": [
	  { "orderable": false, "aTargets": [0, 1]}// 制定列不参与排序
	]
    }); 

    $("#datatableStudent").dataTable({
        "aaSorting": [[1, "desc"]], //默认第几个排序
        "bStateSave": true, //状态保存
        "aoColumnDefs": [
	  { "orderable": false, "aTargets": [0, 1]},// 制定列不参与排序
      { "targets": [ 0 ],"visible": false,"searchable": false}
	]
    });

    $(document).ready(function () {


        LoadStudentExam();
        var t = $("#treeDemo");
        t = $.fn.zTree.init(t, setting, zNodes);


        var table = $('#datatableStudent').DataTable();

        $('#datatableStudent tbody').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
            //var ff = $(this).data;
            var rowData = table.row(this).data();
            CurrentStudentExamId = rowData[0];
//            $("#StudentName").val(rowData[1]);
//            $("#StudentNumber").val(rowData[2]);
            LoadExercisesTest();

        });
        //demoIframe = $("#testIframe");
        //demoIframe.bind("load", loadReady);
        //var zTree = $.fn.zTree.getZTreeObj("tree");
        //zTree.selectNode(zTree.getNodeByParam("id", '1'));
    });

    //加载学生信息
    function LoadStudentExam() {
        var datatable = $("#datatableStudent").DataTable();
        datatable.clear().draw();

        var option = {
            url: '<%:Url.Action("GetStudentExam","Manage") %>',
            type: 'POST',
            async: false,
            dataType: 'html',
            contentType: 'application/json',
            success: function (result) {
               
//                 var jsonData = JSON.parse(result);
//                 var nodeData = '[{"id":"1","pId":"0","name":"学生列表","open":"true"}';
//                 for (var i = 0; i < jsonData.length; i++) {
//                     nodeData += ',{"id":"1' + (i + 1) + '","pId":"1","name":"' + jsonData[i].StudentName + '","StudentExamId":"' + jsonData[i].StudentExamId + '","StudentNumber":"' + jsonData[i].StudentNumber + '"}';
//                 }
//                 nodeData += ']';
//                 //nodeData = '{"id":"1","pId":"0","name":"题目列表","open":"1"}';
//                 zNodes = JSON.parse(nodeData);

                var jsonData = JSON.parse(result);
                if (jsonData.length > 0) {
                    for (var i = 0; i < jsonData.length; i++) {
                        var row = datatable.row.add([jsonData[i].StudentExamId,jsonData[i].StudentName, jsonData[i].StudentNumber, ""]).draw();
                        //row.addClass("text-c");

                    }

                }


            }
        };
        $.ajax(option);
    }
    //加载 学生参加过的所有考试
    function LoadExercisesTest() {
        var jsonDataId = { Id: CurrentStudentExamId };

        var datatable = $("#datatable").DataTable();
        datatable.clear().draw();
        var option = {
            url: '<%:Url.Action("GetExercisesTestStudent","Manage") %>',
            type: 'POST',
            async: false,
            dataType: 'html',
            data:JSON.stringify(jsonDataId),
            contentType: 'application/json',
            success: function (result) {
                var jsonData = JSON.parse(result);
                if (jsonData.length > 0) {
                    for (var i = 0; i < jsonData.length; i++) {
                        var aDel = $("<a></a>");
                        aDel.addClass("ml-5");
                        aDel.attr("style", "text-decoration:none");
                        aDel.attr("onClick", "viewdetails(this,'" + jsonData[i].ExercisesTestId + "','" + jsonData[i].TotleScore + "','" + jsonData[i].StudentScore + "')");
                        aDel.attr("href", "javascript:;");
                        aDel.attr("title", "查看详情");
                        var iDel = $("<i></i>").appendTo(aDel);
                        iDel.addClass("Hui-iconfont");
                        iDel.html("&#xe667;"); //&#xe6e2;

                        var ht = aDel[0].outerHTML;


                        var row = datatable.row.add([jsonData[i].ExercisesName, jsonData[i].ExercisesDescribe, jsonData[i].TotleScore, jsonData[i].StudentScore, ht]).draw();
                        //row.addClass("text-c");

                    }

                }


            }
        };
        $.ajax(option);

    }









    function viewdetails(obj, ExercisesTestId, TotleScore, StudentScore) {

        CurrentExercisesTestId = ExercisesTestId;
        CurrentStudentScore = StudentScore;
        CurrentTotleScore = TotleScore;
        //layer.show("学生答题情况", "StudentExamExercisesTestWeb",'',)
        var index = layer.open({
            type: 2,
            title: "学生答题情况",
            content: "StudentExamExercisesTestWeb"
        });
        layer.full(index);
    }


    function article_del(obj, id) {
        layer.confirm('确认要删除吗？', function (index) {
            var jsonData = { Id: id };
            //删除数据库中的数据 
            var option = {
                url: '<%:Url.Action("DelExercisesTitle","Manage") %>',
                type: 'POST',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    //$(obj).parents("tr").remove().draw();
                    if (result > 0) {
                        var table = $('#datatable').DataTable();
                        table.row($(obj).parents('tr')).remove().draw();
                        layer.msg('已删除!');

                    }
                    else {

                        layer.msg('删除失败！');
                    }

                }
            };
            $.ajax(option);


        });
    }



    </script>
</body>
</html>