<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Index" ContentPlaceHolderID="MainContent" runat="server">  
    <script type="text/javascript">
        $(document).ready(function () {
            $("#cmode").hide();
            $("#citem").hide();
            $("#passwordtable").css("display", "none");
            $("#logintable").css("display", "block");
        });
        function updatepassword() {

            var oldpassword = $("#oldpassword").val();
            var newpassword = $("#newpassword").val();
            var okpassword = $("#okpassword").val();
            if (oldpassword.length <= 0 || newpassword <= 0 || okpassword <= 0) {
                alert("请输入密码！");
                return;
            }
            if (newpassword != okpassword) {
                alert("确认密码错误！");
                return;
            }
            var type = $('input:radio[name="role"]:checked').val();
            var vuserid = $("#userid").val();

            var jsonData = { UserId: vuserid, Password: oldpassword, NewPassword: newpassword, Type: type };
            var option = {
                url: '<%:Url.Action("UpdatePassword","Examination") %>',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                type: 'POST',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    if (result.length>0) {
                        if (result > 0) {
                            //密码修改成功,显示用户信息
                            $("#userinfo").css("display", "block");
                            $("#passwordtable").css("display", "none");
                            //$("userinfo").css("display", "block");
                        }
                        else if (result == -100) {
                            alert("原密码错误！");
                        }
                        else if (result == -101) {
                            alert("密码修改失败！");
                        }

                    }

                }
            }
            $.ajax(option);


        }
         function login() {
             var type = $('input:radio[name="role"]:checked').val();
             var vuserid = $("#userid").val();
             var vpassword = $("#password").val();
             if (vuserid.length <= 0 || vpassword.length <= 0) {
                 alert("请输入用户名和密码！");
                 return;
             }
             var jsonData = {UserId:vuserid,Password:vpassword,Type:type};
             var option = {
                 url: '<%:Url.Action("Login","Examination") %>',
                 data: JSON.stringify(jsonData),
                 dataType: 'html',
                 type: 'POST',
                 async: false,
                 contentType: 'application/json',
                 success: function (result) {
                     if (result.length>0) {
                         var resultData = JSON.parse(result);
                         //第一次登录需要修改密码
                         if (resultData.IsFirstLogin == 1) {
                             $("#passwordtable").css("display", "block");
                             $("#logintable").css("display", "none");
                         }
                         else {
                             //不需要修改密码，显示用户信息
                             $("#passwordtable").css("display", "none");
                             $("#userinfo").css("display", "block");
                             $("#logintable").css("display", "none");
                         }


                         $("#userinfoid").text(resultData.UserID);
                         $("#username").text(resultData.UserName);
                         $("#logindatetime").text(resultData.LoginDateTime);

                     }
                     else {
                        alert("登录失败！");
                     }


                 }
             }
             $.ajax(option);

         }

         function showpassword() {
             //显示密码修改框
             $("#passwordtable").css("display", "block");
             $("#userinfo").css("display", "none");



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
            <!-- 登录-->
            <table id="logintable"  border="0" cellspacing="0" cellpadding="0" width="100%" style="display:block;">
                <tr><td class="tableleft">用户名：</td><td><input id="userid" type="text" /></td></tr>
                <tr><td class="tableleft">密码：</td><td><input id="password" type="text" /></td></tr>
                <tr><td class="tableleft">角色：</td><td>学生<input type="radio" name="role" id="student" value="1" />教师<input type="radio" name="role" id="teacher" value="2"/></td></tr>
                <tr><td colspan="2"><input class="loginButton" type="button" value="登录" onclick="login()" /></td></tr>
            </table>
            <!-- 第一次登录需要修改密码-->
            <table id="passwordtable"  border="0" cellspacing="0" cellpadding="0" width="100%" style="display:none;">
                <tr><td colspan="2">请修改密码</td></tr>
                <tr><td class="tableleft">原密码：</td><td><input id="oldpassword" type="text" /></td></tr>
                <tr><td class="tableleft">新密码：</td><td><input id="newpassword" type="text" /></td></tr>
                <tr><td class="tableleft">确认密码：</td><td><input id="okpassword" type="text" /></td></tr>
                <tr><td colspan="2"><input class="loginButton" type="button" value="确定" onclick="updatepassword()" /></td></tr>
            </table>
            <!-- 登录成功后显示的用户信息-->
            <table id="userinfo"  border="0" cellspacing="0" cellpadding="0" width="100%" style="display:none;">
                <tr><td colspan="2">用户信息</td></tr>
                <tr><td class="tableleft">用户名：</td><td><label id="userinfoid"></label></td></tr>
                <tr><td class="tableleft">姓名：</td><td><label id="username"></label></td></tr>
                <tr><td class="tableleft">登录时间：</td><td><label id="logindatetime"></label></td></tr>
                <tr><td colspan="2"><a style="color:Blue" href="#" onclick="showpassword()">密码修改</a></td></tr>
            </table>
            </div>
        </div>
    </div>
</asp:Content>
