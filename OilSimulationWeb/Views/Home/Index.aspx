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
            <div class="zt-zi"> 在线虚拟实验  &gt; <span id="curMode">基础认知</span>  &gt; <span id="curitem">水驱油模拟</span>  </div>
        </div>
        <div class="greybg">
            <div class="main"> 
                <div id="LeftPanel"> 
                    <div class="zbar">
                        <div class="zbar-title">基础认知</div> 
		                <ul style="display: block;"> 
		                    <li class="liNav select"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">水驱油模拟</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">渗流方式模拟</span></a></li> 
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">弹性不稳定渗流</span></a></li>
		                </ul>
                        <div class="zbar-title">虚拟实验</div> 
		                <ul style="display: none"> 
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">非活塞式水驱油影响因素</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">水驱油效率实验</span></a></li> 
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">采收率实验</span></a></li>
		                </ul>
                        <div class="zbar-title">仿真实训</div> 
		                <ul style="display: none"> 
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">井网井距方案开发设计</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">注采系统方案开发设计</span></a></li> 
		                </ul>
                        <div class="zbar-title">创新实践</div> 
		                <ul style="display: none"> 
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">油藏开发方案实践</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">气藏开发方案实践</span></a></li> 
		                </ul>
	                </div> 
                </div>
                <div id="RightPanl"> 
                    <div id="divControl" class="zt">选择栏</div>
                    <div><iframe id="iframeId" name="I1" scrolling="no" style="height: 600px; width: 100%; border:0;" src="" ></iframe></div> 
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
