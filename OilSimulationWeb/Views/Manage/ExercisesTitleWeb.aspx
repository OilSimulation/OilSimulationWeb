<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html>
<head>
<title>考试或练习题目配置</title>

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
<div class="pos-a" style="width:150px;left:0;top:0; bottom:0; height:100%; border-right:1px solid #e5e5e5; background-color:#f5f5f5">
	<ul id="treeDemo" class="ztree">
	</ul>
</div>
<div style="margin-left:150px;">
	<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 试题管理 <span class="c-gray en">&gt;</span> 考试题目配置 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
	<div class="page-container">

		<div class="cl pd-5 bg-1 bk-gray mt-20"> <span class="l"><a href="javascript:;" onclick="datadel()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 批量删除</a> <a class="btn btn-primary radius" onclick="AddExercisesTitle('添加题目','AddExercisesTitleWeb')" href="javascript:;"><i class="Hui-iconfont">&#xe600;</i> 添加题目</a></span>  </div>
		<div class="mt-20">
			<table id="datatable" class="table table-border table-bordered table-bg table-hover table-sort">
				<thead>
					<tr class="text-c">
						<th width="40"><input name="" type="checkbox" value=""></th>
						<th width="100">题目内容</th>
                        <th width="60">正确答案</th>
                        <th width="60">该题分数</th>
                        <th width="60">题目位置</th>
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
    //当前选中的考试Id
    var CurrentExercisesTestId = -1;
    //当前选中的考试 实验类型ID(-1表示一次考试，其它表示练习)
    var CurrentExercisesTypeId = -1;
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
                    CurrentExercisesTestId = treeNode.file;
                    CurrentExercisesTypeId = treeNode.TypeId;
                    LoadExercisesTitle();
                    return true;
                }
            }
        }
    };



    $(document).ready(function () {
        LoadExercisesTest();
        var t = $("#treeDemo");
        t = $.fn.zTree.init(t, setting, zNodes);
        //demoIframe = $("#testIframe");
        //demoIframe.bind("load", loadReady);
        //var zTree = $.fn.zTree.getZTreeObj("tree");
        //zTree.selectNode(zTree.getNodeByParam("id", '1'));
    });

    //加载 考试对应的题目
    function LoadExercisesTitle() {
        var jsonDataId = { Id: CurrentExercisesTestId };

        var datatable = $("#datatable").DataTable();
        datatable.clear().draw();
        //加载 题目下的所有选项
        var option = {
            url: '<%:Url.Action("GetExercisesTitle","Manage") %>',
            type: 'POST',
            async: false,
            dataType: 'html',
            data: JSON.stringify(jsonDataId),
            contentType: 'application/json',
            success: function (result) {
                var jsonData = JSON.parse(result);
                if (jsonData.length > 0) {
                    for (var i = 0; i < jsonData.length; i++) {
                        var aDel = $("<a></a>");
                        aDel.addClass("ml-5");
                        aDel.attr("style", "text-decoration:none");
                        aDel.attr("onClick", "article_del(this,'" + jsonData[i].ExercisesTitleId + "')");
                        aDel.attr("href", "javascript:;");
                        aDel.attr("title", "删除");
                        var iDel = $("<i></i>").appendTo(aDel);
                        iDel.addClass("Hui-iconfont");
                        iDel.html("&#xe6e2;"); //&#xe6e2;

                        var ht = aDel[0].outerHTML;


                        var row = datatable.row.add(['<input type="checkbox" value="1" name="">', jsonData[i].TitleConent, jsonData[i].CorrectAnswer,jsonData[i].Score,jsonData[i].ExercisesTitleIndex, ht]).draw();
                        //row.addClass("text-c");

                    }

                }


            }
        };
        $.ajax(option);

    }


   



    $("#datatable").dataTable({
        "aaSorting": [[1, "desc"]], //默认第几个排序
        "bStateSave": true, //状态保存
        "aoColumnDefs": [
	  { "orderable": false, "aTargets": [0, 1]}// 制定列不参与排序
	]
    });

    //加载考试数据
    function LoadExercisesTest() {
        var option = {
            url: '<%:Url.Action("GetExercisesTest","Manage") %>',
            type: 'POST',
            async: false,
            dataType: 'html',
            contentType: 'application/json',
            success: function (result) {
                var jsonData = JSON.parse(result);
                var nodeData = '[{"id":"1","pId":"0","name":"考试列表","open":"true"}';
                for (var i = 0; i < jsonData.length; i++) {
                    nodeData += ',{"id":"1' + (i + 1) + '","pId":"1","name":"' + jsonData[i].ExercisesName + '","file":"' + jsonData[i].ExercisesTestId + '","TypeId":"' + jsonData[i].ExercisesTypeId + '"}';
                }
                nodeData += ']';
                //nodeData = '{"id":"1","pId":"0","name":"题目列表","open":"1"}';
                zNodes = JSON.parse(nodeData);

                var f = 0;
            }
        };
        $.ajax(option);
    }

    //增加 考试与题目对应关系
    function AddExercisesTitle(title, url) {
        if (CurrentExercisesTestId == -1) {
            layer.msg('请先选择题目！');
            return;
        }
        layer_show(title, url, '', 400);
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



    function product_del(obj, id) {
        layer.confirm('确认要删除吗？', function (index) {
            $(obj).parents("tr").remove();
            layer.msg('已删除!', { icon: 1, time: 1000 });
        });
    }
</script>
</body>
</html>