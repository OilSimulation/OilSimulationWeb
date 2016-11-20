<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebReport.aspx.cs" Inherits="OilSimulationWeb.Views.WebReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="Index">
        <div class="loading-overlay">
	        <p class="loading-spinner">
		        <span class="loading-icon"></span>
		        <span class="loading-text">loading</span>
	        </p>
	    </div> 

        <div id="TopPanel">
            <div class="top-main">
                <div class="top-logo"> 
                    <img alt="重庆科技学院油藏仿真" src="<%=Url.Content("~/Images/zx-logo.jpg")%>" width="382" height="85" border="0" /> 
                </div>
            </div>
        </div>
    
        <div class="main"> 
            <div class="zt">
                <div class="zt-tu"><img alt="" src="<%=Url.Content("~/Images/zt-tu.png")%>" width="12" height="12" /></div>
                <div class="zt-zi"> 在线虚拟实验 <span id="cmode"> &gt; <span id="curMode">基础认知</span> </span><span id="citem"> &gt; <span id="curitem">水驱油模拟</span></span>  </div>
            </div>
            <div class="greybg">
                <div class="main">   
                    <div class="clear"></div>
                </div> 
            </div> 
        </div> 

        <div class="foot2">
            <div class="main">
            COPYRIGHT 2013 (C) CHONGQING UNIVERSITY OF SCIENCE AND TECHNOLOGY ALL RIGHTS RESERVED<br/>
            重庆沙坪坝区大学城东路20号 邮编：401331 备案号：渝ICP备13000511号 技术支持：<a href="javascript:void(0);" target="_blank">重庆昌禀科技</a>
            </div>
        </div>
    </div> 
</body>
</html>
