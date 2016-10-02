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
</head>
<body>
<header class="navbar-wrapper">
	<div class="navbar navbar-fixed-top">
		<div class="container-fluid cl"> 
         <span class="logo navbar-slogan f-l mr-10 hidden-xs">考试管理系统 v1.0</span> <a aria-hidden="false" class="nav-toggle Hui-iconfont visible-xs" href="javascript:;">&#xe667;</a>

			<nav id="Hui-userbar" class="nav navbar-nav navbar-userbar hidden-xs">

			</nav>
		</div>
	</div>
</header>
<aside class="Hui-aside">
	<input runat="server" id="divScrollValue" type="hidden" value="" />
	<div class="menu_dropdown bk_2">
		<dl id="menu-article">
			<dt><i class="Hui-iconfont">&#xe616;</i>实验类型管理<i class="Hui-iconfont menu_dropdown-arrow">&#xe6d5;</i></dt>
			<dd>
				<ul>
					<li><a _href="ExperimentTypeWeb" data-title="实验类型管理" href="javascript:void(0)">实验类型管理</a></li>
				</ul>
			</dd>
		</dl>
		<dl id="menu-picture">
			<dt><i class="Hui-iconfont">&#xe613;</i> 试题管理<i class="Hui-iconfont menu_dropdown-arrow">&#xe6d5;</i></dt>
			<dd>
				<ul>
<%--					<li><a _href="picture-list.html" data-title="题目类型管理" href="javascript:void(0)">题目类型管理</a></li>--%>
					<li><a _href="TitleItemWeb" data-title="选项管理" href="javascript:void(0)">选项管理</a></li>
                    <li><a _href="TitleInfoWeb" data-title="题目管理" href="javascript:void(0)">题目管理</a></li>
					<li><a _href="TitleItemAssocWeb" data-title="题目选项配置" href="javascript:void(0)">题目选项配置</a></li>
					
					<li><a _href="ExercisesTestWeb" data-title="考试管理" href="javascript:void(0)">考试管理</a></li>
                    <li><a _href="ExercisesTitleWeb" data-title="考试管理" href="javascript:void(0)">考试题目配置</a></li>
				</ul>
			</dd>
		</dl>
		<dl id="menu-product">
			<dt><i class="Hui-iconfont">&#xe620;</i> 学生信息管理<i class="Hui-iconfont menu_dropdown-arrow">&#xe6d5;</i></dt>
			<dd>
				<ul>
					<li><a _href="StudentExamWeb" data-title="学生信息管理" href="javascript:void(0)">学生信息管理</a></li>
				</ul>
			</dd>
		</dl>
<%--		<dl id="menu-comments">
			<dt><i class="Hui-iconfont">&#xe622;</i> 试卷管理<i class="Hui-iconfont menu_dropdown-arrow">&#xe6d5;</i></dt>
			<dd>
				<ul>
					<li><a _href="http://h-ui.duoshuo.com/admin/" data-title="试卷管理" href="javascript:;">试卷管理</a></li>
				</ul>
			</dd>
		</dl>
--%>
	</div>
</aside>
<div class="dislpayArrow hidden-xs"><a class="pngfix" href="javascript:void(0);" onClick="displaynavbar(this)"></a></div>
<section class="Hui-article-box">
	<div id="Hui-tabNav" class="Hui-tabNav hidden-xs">
		<div class="Hui-tabNav-wp">
			<ul id="min_title_list" class="acrossTab cl">
				<li class="active"><span title="我的桌面" data-href="welcome">我的桌面</span><em></em></li>
			</ul>
		</div>
		<div class="Hui-tabNav-more btn-group"><a id="js-tabNav-prev" class="btn radius btn-default size-S" href="javascript:;"><i class="Hui-iconfont">&#xe6d4;</i></a><a id="js-tabNav-next" class="btn radius btn-default size-S" href="javascript:;"><i class="Hui-iconfont">&#xe6d7;</i></a></div>
	</div>
	<div id="iframe_box" class="Hui-article">
		<div class="show_iframe">
			<div style="display:none" class="loading"></div>
			<iframe scrolling="yes" frameborder="0" src="welcome"></iframe>
		</div>
	</div>
</section>

    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/layer/2.1/layer.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui/js/H-ui.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/js/H-ui.admin.js")%>"></script>

<script type="text/javascript">
    /*资讯-添加*/
    function article_add(title, url) {
        var index = layer.open({
            type: 2,
            title: title,
            content: url
        });
        layer.full(index);
    }
    /*图片-添加*/
    function picture_add(title, url) {
        var index = layer.open({
            type: 2,
            title: title,
            content: url
        });
        layer.full(index);
    }
    /*产品-添加*/
    function product_add(title, url) {
        var index = layer.open({
            type: 2,
            title: title,
            content: url
        });
        layer.full(index);
    }
    /*用户-添加*/
    function member_add(title, url, w, h) {
        layer_show(title, url, w, h);
    }
</script> 

</body>

</html>
