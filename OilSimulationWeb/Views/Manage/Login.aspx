<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta charset="utf-8">
<meta name="renderer" content="webkit|ie-comp|ie-stand">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta http-equiv="Cache-Control" content="no-siteapp" />


<link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" />
<link href="<%=Url.Content("~/Scripts/Exam/static//h-ui.admin/css/H-ui.login.css")%>" rel="stylesheet" type="text/css" />
<link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" />
<link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" />


<title>试题管理 后台登录</title>
</head>
<body>
<input type="hidden" id="TenantId" name="TenantId" value="" />
<div class="header"></div>
<div class="loginWraper">
  <div id="loginform" class="loginBox">
    <form class="form form-horizontal" action="ExamManage" method="post" onsubmit="return checkData()">
      <div class="row cl">
        <label class="form-label col-xs-3"><i class="Hui-iconfont">&#xe60d;</i></label>
        <div class="formControls col-xs-8">
          <input id="username" name="" type="text" placeholder="账户" class="input-text size-L">
        </div>
      </div>
      <div class="row cl">
        <label class="form-label col-xs-3"><i class="Hui-iconfont">&#xe60e;</i></label>
        <div class="formControls col-xs-8">
          <input id="password" name="" type="password" placeholder="密码" class="input-text size-L">
        </div>
      </div>
<%--      <div class="row cl">
        <div class="formControls col-xs-8 col-xs-offset-3">
          <input class="input-text size-L" type="text" placeholder="验证码" onblur="if(this.value==''){this.value='验证码:'}" onclick="if(this.value=='验证码:'){this.value='';}" value="验证码:" style="width:150px;">
          <img src="images/VerifyCode.aspx.png"> <a id="kanbuq" href="javascript:;">看不清，换一张</a> </div>
      </div>--%>
      <div class="row cl">
        <div class="formControls col-xs-8 col-xs-offset-3">
          <label for="online">
            <input type="checkbox" name="online" id="online" value="">
            使我保持登录状态</label>
        </div>
      </div>
      <div class="row cl">
        <div class="formControls col-xs-8 col-xs-offset-3">
          <input name="" type="submit" class="btn btn-success radius size-L" value="&nbsp;登&nbsp;&nbsp;&nbsp;&nbsp;录&nbsp;">
          <input name="" type="reset" class="btn btn-default radius size-L" value="&nbsp;取&nbsp;&nbsp;&nbsp;&nbsp;消&nbsp;">
        </div>
      </div>
    </form>
  </div>
</div>
<div class="footer">Copyright 重庆科技学院油藏仿真实验室 试题管理系统</div>
<!-- <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>  -->
<script type="text/javascript" src="<%=Url.Content("~/Scripts/jquery-1.4.1.min.js")%>"></script>

<script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui/js/H-ui.js")%>"></script> 
<script type="text/javascript">
    function checkData() {
        return true;
        var name = document.getElementById("username").value;
        var password = document.getElementById("password").value;
       if(name ==  null || name == ''||password==null||password==''){
            alert("用户名和密码不能为空！");
            return false;
        }
        else {
            if (name == "admin" && password == "110120119") {
                return true;
            }
            else {
                return false;
            }
        }

       return true;
    }
    
</script>

<!-- <script> -->
<!-- var _hmt = _hmt || []; -->
<!-- (function() { -->
<!--   var hm = document.createElement("script"); -->
<!--   hm.src = "//hm.baidu.com/hm.js?080836300300be57b7f34f4b3e97d911"; -->
<!--   var s = document.getElementsByTagName("script")[0];  -->
<!--   s.parentNode.insertBefore(hm, s); -->
<!-- })(); -->
<!-- var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://"); -->
<!-- document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F080836300300be57b7f34f4b3e97d911' type='text/javascript'%3E%3C/script%3E")); -->
<!-- </script> -->
</body>

</html>
