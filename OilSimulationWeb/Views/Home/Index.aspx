<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Index" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ShowMode1() {
            $("#iframeId").attr("src","<%=Url.Content("~/Base/BaseModeOne")%>");
        }
        function ShowMode2() {
            $("#iframeId").attr("src", "<%=Url.Content("~/Base/BaseModeTwo")%>");
        }
        function ShowMode3() {
            $("#iframeId").attr("src", "<%=Url.Content("~/Base/BaseModeThree")%>");
        }
        function ShowMode4() {
            $("#iframeId").attr("src", "<%=Url.Content("~/Base/BaseModeFour")%>");
        } 
    </script>

    <div id="TopPanel">
        <div class="top-main">
            <div class="top-logo"> 
                <img alt="重庆科技学院油藏仿真" src="<%=Url.Content("~/Images/zx-logo.jpg")%>" width="235" height="85" border="0" /> 
            </div>
        </div>
    </div>
    
    <div  class="main"> 
        <div class="zt">
            <div class="zt-tu"><img alt="" src="<%=Url.Content("~/Images/zt-tu.png")%>" width="12" height="12" /></div>
            <div class="zt-zi"> 在线虚拟实验  &gt; <span id="curitem">采油模块</span>  &gt; <%= ViewData["szMac"] %>  </div>
        </div>
        <div class="greybg">
            <div class="main"> 
                <div id="LeftPanel"> 
                    <div class="zbar">
                        <div class="zbar-title">基础认知</div> 
		                <ul> 
		                    <li class="select" id = "sel1"><a href="javascript:void(0);ShowMode1();" class="oneline"><span class="lmenu-ico"></span><span class="zh">基础认知</span></a></li>
		                    <li class="" id = "sel2"><a href="javascript:void(0);ShowMode2();" class="oneline"><span class="lmenu-ico"></span><span class="zh">虚拟实验</span></a></li>
		                    <li class="" id = "sel3"><a href="javascript:void(0);ShowMode3();" class="oneline"><span class="lmenu-ico"></span><span class="zh">仿真实训</span></a></li>
		                    <li class="q-cnclast"><a href="javascript:void(0);ShowMode4();" class="oneline"><span class="lmenu-ico"></span><span class="zh">创新实践</span></a></li>
		                </ul>
                        <div class="zbar-title">虚拟实验</div> 
		                <ul> 
		                    <li class="select" id = "Li1"><a href="javascript:void(0);ShowMode1();" class="oneline"><span class="lmenu-ico"></span><span class="zh">基础认知</span></a></li>
		                    <li class="" id = "Li2"><a href="javascript:void(0);ShowMode2();" class="oneline"><span class="lmenu-ico"></span><span class="zh">虚拟实验</span></a></li>
		                    <li class="" id = "Li3"><a href="javascript:void(0);ShowMode3();" class="oneline"><span class="lmenu-ico"></span><span class="zh">仿真实训</span></a></li>
		                    <li class="q-cnclast"><a href="javascript:void(0);ShowMode4();" class="oneline"><span class="lmenu-ico"></span><span class="zh">创新实践</span></a></li>
		                </ul>
                        <div class="zbar-title">仿真实训</div> 
		                <ul> 
		                    <li class="select" id = "Li4"><a href="javascript:void(0);ShowMode1();" class="oneline"><span class="lmenu-ico"></span><span class="zh">基础认知</span></a></li>
		                    <li class="" id = "Li5"><a href="javascript:void(0);ShowMode2();" class="oneline"><span class="lmenu-ico"></span><span class="zh">虚拟实验</span></a></li>
		                    <li class="" id = "Li6"><a href="javascript:void(0);ShowMode3();" class="oneline"><span class="lmenu-ico"></span><span class="zh">仿真实训</span></a></li>
		                    <li class="q-cnclast"><a href="javascript:void(0);ShowMode4();" class="oneline"><span class="lmenu-ico"></span><span class="zh">创新实践</span></a></li>
		                </ul>
                        <div class="zbar-title">创新实践</div> 
		                <ul> 
		                    <li class="select" id = "Li7"><a href="javascript:void(0);ShowMode1();" class="oneline"><span class="lmenu-ico"></span><span class="zh">基础认知</span></a></li>
		                    <li class="" id = "Li8"><a href="javascript:void(0);ShowMode2();" class="oneline"><span class="lmenu-ico"></span><span class="zh">虚拟实验</span></a></li>
		                    <li class="" id = "Li9"><a href="javascript:void(0);ShowMode3();" class="oneline"><span class="lmenu-ico"></span><span class="zh">仿真实训</span></a></li>
		                    <li class="q-cnclast"><a href="javascript:void(0);ShowMode4();" class="oneline"><span class="lmenu-ico"></span><span class="zh">创新实践</span></a></li>
		                </ul>
	                </div> 
                </div>
                <div id="RightPanl"> 
                    <div id="divControl" class="zt">选择栏</div>
                    <div><iframe id="iframeId" name="I1" scrolling="no" style="height: 600px; width: 100%; border:0;" src="<%=Url.Content("~/Base/BaseModeTwo")%>" ></iframe></div> 
                </div>
                <div class="clear"></div>
            </div> 
        </div> 
    </div> 

    <div class="foot2">
        <div class="main">
        COPYRIGHT 2013 (C) CHONGQING UNIVERSITY OF SCIENCE AND TECHNOLOGY ALL RIGHTS RESERVED<br>
        重庆沙坪坝区大学城东路20号 邮编：401331 备案号：渝ICP备13000511号 技术支持：<a href="javascript:void(0);" target="_blank">重庆昌禀科技</a>
        </div>
    </div>
    
</asp:Content>
