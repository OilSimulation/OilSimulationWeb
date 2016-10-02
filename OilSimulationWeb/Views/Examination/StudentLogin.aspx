<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title><style type="text/css">
          table
      {
        width:800px;
        text-align:center;
        margin: auto;
        color:White;
        }</style>
            <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>

</head>
<body>
    <div>
        <table id="table1" border="0" cellspacing="0" cellpadding="0" >

    	<tr>
    		<td >姓名</td><td ><input id="studentname" type="text" /></td><td>学号</td><td><input id="studentnumber" type="text" /></td>
    	</tr>
        <tr>
    		<td colspan="4"><input type="button" onclick="start()" value="开始测试"/></td>
    	</tr>

    	<tr>
    		<td colspan="4" style="color:Red">注意：学号请务必填写正确！学号填写错误可能会没有成绩！</td>
    	</tr>
   </table>
    </div>
    <script type="text/javascript">
//         var ExperimentTypeId;
//         var studentName=-1;
//         var studentNumber="-1";
//         var StudentExamId = -1;
        $(document).ready(function () {
              parent.postMessage("HideLoading()", "*");
        });

        function start() {
             parent.StudentName = $.trim($("#studentname").val());
             parent.StudentNumber = $.trim($("#studentnumber").val());
             if (parent.StudentNumber == "") {
                 alert("请输入学号！");
                 return;
             }
             var jsonData = { StudentName: parent.StudentName, StudentNumber: parent.StudentNumber };
             var option = {
                 url: '<%:Url.Action("OptStudentNumber","Examination") %>',
                 data: JSON.stringify(jsonData),
                 dataType: 'html',
                 type: 'POST',
                 async: false,
                 contentType: 'application/json',
                 success: function (result) {
                     if (result == "") {
                         return;
                     }
                     var resultData = JSON.parse(result);
                     if (resultData != null) {
                         parent.StudentExamId = resultData.StudentExamId;
                     }

                     parent.LoadUrl("/Examination/ExamPaper");

                 }
             }
            $.ajax(option);
            window.location.href = 'ExamPaper?';
        }
    </script>
</body>
</html>
