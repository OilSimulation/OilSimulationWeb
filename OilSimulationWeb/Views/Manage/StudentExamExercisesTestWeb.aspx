<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/H-ui.admin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/icheck/icheck.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/skin/default/skin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" /> 

    <title></title>
        <style type="text/css">
        table{
            text-align:center;
        }
    </style>
</head>
<body>
    <div>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
        	<tr>
        		<td  colspan="8" >什么考试</td>
        	</tr>
            <tr>
                <td>姓名</td><td><input type="text" value="张三" disabled="disabled"/></td><td>学号</td><td><input type="text" value="12312312" disabled="disabled"/></td>
                <td>总分</td><td><input type="text" value="100" disabled="disabled"/></td><td>成绩</td><td><input type="text" value="60" disabled="disabled"/></td>
            </tr>
        </table>

        <!-- 题目列表 -->
        <table id="titlelist" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="text-align:left" colspan="4">题目kkfsdklfjksdlfjksdlfjksdlfjksdlfjsdlfjsdklfjsdlfjsdklfjsdlj</td>
            </tr>
            <tr>
                <td><input type="radio" name="r1" value="0" /><span>选项1</span></td>
                <td><input type="radio" name="r1" value="0" /><span>选项2</span></td>
                <td><input type="radio" name="r1" value="0" /><span>选项3</span></td>
                <td><input type="radio" name="r1" value="0" /><span>选项4</span></td>
            </tr>
        </table>

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
        LoadExamTitle();

    });

    //加载 学生的这次做题情况
    function LoadExamTitle() {
        var jsonData={}
        var option = {
            url: '<%:Url.Action("GetStudentExam","Manage") %>',
            type: 'POST',
            async: false,
            dataType: 'html',
            contentType: 'application/json',
            success: function (result) {
                var jsonData = JSON.parse(result);
                var nodeData = '[{"id":"1","pId":"0","name":"学生列表","open":"true"}';
                for (var i = 0; i < jsonData.length; i++) {
                    nodeData += ',{"id":"1' + (i + 1) + '","pId":"1","name":"' + jsonData[i].StudentName + '","StudentExamId":"' + jsonData[i].StudentExamId + '","StudentNumber":"' + jsonData[i].StudentNumber + '"}';
                }
                nodeData += ']';
                //nodeData = '{"id":"1","pId":"0","name":"题目列表","open":"1"}';
                zNodes = JSON.parse(nodeData);

                var f = 0;
            }
        };
        $.ajax(option);

    }
</script>
</body>
</html>
