<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Index" ContentPlaceHolderID="MainContent" runat="server">
    

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
                        <div class="zbar-title">采油模块</div> 
		                     <ul> 
		                     <li class="select" id = "sel1"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">基础认知</span></a></li>
		                     <li class="" id = "sel2"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">虚拟实验</span></a></li>
		                     <li class="" id = "sel3"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">仿真实训</span></a></li>
		                     <li class="q-cnclast"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">创新实践</span></a></li>
		                     </ul>
	                    </div> 
                    </div>
                <div id="RightPanl" >
                    <iframe id="iframe1Id" name="I1" scrolling="no" style="height: 400px; width: 100%; border:0;" src="<%=Url.Content("/Home/ViewBaseKnow1")%>" allowfullscreen="true"></iframe>  
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
