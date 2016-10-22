<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Index" ContentPlaceHolderID="MainContent" runat="server">  
    <script type="text/javascript">
        $(document).ready(function () {
            $("#cmode").hide();
            $("#citem").hide();
            $("#passwordtable").css("display","block");
            $("#logintable").css("display", "none");
        });
        function updatepassword() {
            var oldpassword = $("oldpassword").val();
            var newpassword = $("newpassword").val();
            var okpassword = $("okpassword").val();
            if (oldpassword.length <= 0 || newpassword <= 0 || okpassword <= 0) {
                alert("请输入密码！");
                return;
            }
        }
         function login() {
             var type=1;
             var vteacher = $("#teacher").val();
             var vstudent = $("#student").val();
             var vuserid = $("#userid").val();
             var vpassword = $("#password").val();
             if (vuserid.length <= 0 || vpassword.length <= 0) {
                 alert("请输入用户名和密码！");
                 return;
             }
             if (vteacher=="yes") {
                type = 2;
             }
             var jsonData = {UserName:vuserid,Password:vpassword,Type:type};
             var option = {
                 url: '<%:Url.Action("Login","Examination") %>',
                 data: JSON.stringify(jsonData),
                 dataType: 'html',
                 type: 'POST',
                 async: false,
                 contentType: 'application/json',
                 success: function (result) {
                     if (result != null) {
                         var resultData  = JSON.parse(result);
                     }
                     if (resultData.IsFirstLogin > 0) {
                         $("#passwordtable").css("display", "none");
                         $("#logintable").css("display", "block");
                     }

                 }
             }
             $.ajax(option);

         }
    </script>
    <div id="MainLayOut" style="width:100%">
        <div style="float:left;margin-right:515px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
            <h2>油藏工程虚拟仿真实验教学中心简介</h2>
            <div class="bottom f-richEditorIndex"><p>&nbsp; &nbsp; &nbsp; &nbsp; 油藏工程是石油工程专业的一门主要专业课，它是研究油气藏动态特征及规律和油气田开发方法及决策的工程学科，以油气藏（田）为研究对象，以地球物理、油藏地质、油层物理、渗流力学等学科为理论基础，以数学、计算机科学、经济学等学科为研究工具，以高效开发油气资源为目的的一门综合性学科。<br />&nbsp; &nbsp; &nbsp; &nbsp; 油藏是一个深埋地下而无法进行直接观察和描述的地质实体，是根据各种间接资料所描绘出来的概念模型。为了正确评价和合理开发油气藏,利用油藏工程虚拟仿真实验软件开展基础认知、虚拟实验、仿真实训和创新实践四个层次的培训。培养学生的基本技能、专业技能、工程应用能力和创新能力；同时依托开放课题、教师科研课题、石油工程设计大赛等项目，强化学生的工程应用能力，并培养学生的创新实践能力。</p>
            </div>
        </div>
        <div style="width: 500px; margin-left: -500px; position: relative;float: right;">
            <div style="margin:5px auto;"><img alt="" width="490px" src="../../Images/img2.jpg" /></div>
            <div class="login">
            <table id="logintable"  border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr><td>用户名：</td><td><input id="userid" type="text" /></td></tr>
                <tr><td>密&nbsp;&nbsp;&nbsp;码：</td><td><input id="password" type="text" /></td></tr>
                <tr><td>角&nbsp;&nbsp;&nbsp;色：</td><td>学生<input type="radio" name="1" id="student" />教师<input type="radio" name="1" id="teacher"/></td></tr>
                <tr><td colspan="2"><input class="loginButton" type="button" value="登录" onclick="login()" /></td></tr>
            </table>
            <table id="passwordtable"  border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr><td colspan="2">请修改密码</td></tr>
                <tr><td>&nbsp;&nbsp;原密码：</td><td><input id="olduserid" type="text" /></td></tr>
                <tr><td>&nbsp;&nbsp;新密码：</td><td><input id="newuserid" type="text" /></td></tr>
                <tr><td>确认密码：</td><td><input id="okuserid" type="text" /></td></tr>
                <tr><td colspan="2"><input class="loginButton" type="button" value="确定" onclick="updatepassword()" /></td></tr>
            </table>
            </div>
        </div>
    </div>
</asp:Content>
