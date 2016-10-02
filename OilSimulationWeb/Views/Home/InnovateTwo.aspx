﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <script type="text/javascript">
        $(document).ready(function () {
            $("#WebGLLayOut").hide();
            $("#MainLayOut").show();
            $(".liNav").each(function () { $(this).removeClass("select"); });
            $(".zbar ul").each(function () { $(this).css("display", "none"); });
            $("#ModeFour").css("display", "block");
            $("#ExpTen").addClass("select");
            $("#curMode").text($("#ModeFour").prev().text());
            $("#curitem").text($("#ExpTen").children().text());
        });
    </script>
    <div id="WebGLLayOut">
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验十、气藏开发方案实践</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 气田开发是依据详探和必要的生产性开发试验，在综合研究的基础上对具有工业价值的气田，按照国家或市场对天然气的需求，从气田的实际情况和生产规律出发，制定出合理的开发方案，并对气田进行建设和投产，使气田按预定的生产能力和经济效果长期生产，并在生产过程中做必要的调整，保持合理开发，直到开发结束。<br/>&nbsp; &nbsp; &nbsp; &nbsp; 气田开发方案是指在深入认识气田地下情况的基础上，正确制订气田开发方针与原则，科学地对气藏工程、钻井工程、采气工程、地面建设工程及投资等进行设计和安排。它是指导气田开发工作的重要技术文件，气田投入开发必须有正式批准的开发方案设计报告。气藏工程设计是气田开发方案编制的核心环节，主要包括气田开发设计原则、开发层系划分与组合、开发方式的选择、开发井网的确定、开采速度的确定、开发方案设计及指标预测等。本实验结合实际气藏数据完成了开发井网部署和开发指标预测。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）气井产能评价；<br/>（2）气田开发方案的编制的步骤；<br/>（3）气藏工程设计的主要内容。</p></div>
            </div>
        </div>
        <div style="width: 400px; margin-left: -400px; position: relative;float: right;">
           <div>
                <div class="t-title f-f0">课程内容</div>
                <object id="forfun" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" width="400" height="300"
                codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0">
                <param name="movie" value="/seufld/seufld/flash/focus2.swf">
                <param name="quality" value="high">
                <param name="bgcolor" value="#F0F0F0">
                <param name="menu" value="false">
                <param name="wmode" value="opaque"><!--Window|Opaque|Transparent-->
                <param name="FlashVars" value="">
                <param name="allowScriptAccess" value="sameDomain">
                <embed id="forfunex" src="/Flash/气藏开发方案实践.swf"
                    width="400"
                    height="300"
                    align="middle"
                    quality="high"
                    bgcolor="#f0fff8"
                    menu="false"
                    play="true"
                    loop="false"
                    FlashVars=""
                    allowfullscreen="true"
                    allowScriptAccess="sameDomain"
                    type="application/x-shockwave-flash"
                    pluginspage="http://www.adobe.com/go/getflashplayer">
                </embed>
                </object>
            </div> 
            <div style="padding: 10px 20px 10px 0px;">
                <div class="t-title f-f0">实验演示</div>
                <div class="m-btnList">
                    <a href="javascript:void(0);" onclick="LoadUrl('/Innovate/InnModeThree');" ><span class="f-da">1、均质气藏开发方案设计与开发效果预测实践</span></a>
                    <a href="javascript:void(0);" onclick="LoadUrl('/Innovate/InnModeFour');" ><span class="f-da">2、实际气藏开发方案设计与开发效果预测实践</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="m-btnList">
                    <a class="f-ib u-btn u-btn-lg u-btn-orange" id="j-startLearn" href="javascript:void(0);" onclick="LoadExam(10);">
                        <span class="f-db">参加测试</span> 
                    </a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>
