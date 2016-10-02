<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


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
            $("#ModeOne").css("display", "block");
            $("#ExpTwo").addClass("select");
            $("#curMode").text($("#ModeOne").prev().text());
            $("#curitem").text($("#ExpTwo").children().text());
        });
    </script>
    <div id="WebGLLayOut">
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验二、渗流方式模拟</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 渗流方式即为流体在多孔介质中流动的方式。在实际生产中，油藏形状和布井方式都比较复杂。为研究这些复杂情况下的流体渗流规律，需要概括其共有的渗流特征。根据渗流特点的不同，可以把渗流方式分为单向流、平面径向流、球面向心流。单向流主要存在于岩芯流动及排状井网中，其流线为彼此平行的直线；如果该渗流是稳定流，则在垂直于流动方向的每个截面上，各点的渗流速度相等。平面径向流主要存在于井眼附近，其流线为直线且以二维向中心井汇集，越接近中心，渗流面积越小，但各个平面的渗流状况相同。球面向心流的情况是在开采底水油藏时，因为油藏厚度往往比较大，且为防止底水过快锥进到井底，通常只钻开油层顶部，则在井点附近地区，流线成直线向井点汇集，其渗流面积呈半球面。为了更好地理解不同渗流方式的特征及规律，针对单向流、平面径向流、球面向心流，分别建立一维直角坐标模型、二维径向模型、三维球面模型，利用数值模拟方法研究不同渗流方式的流动过程。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）单向流的概念、渗流规律及其特征、水动力学场图；<br/>（2）平面径向流的概念、渗流规律及其特征、水动力学场图；<br/>（3）球面向心流的概念、渗流规律及其特征。</p></div>
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
                <embed id="forfunex" src="/Flash/渗流方式模拟.swf"
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
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeThree');" ><span class="f-da">1、单向渗流模拟</span></a>
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeFour');" ><span class="f-da">2、平面径向渗流模拟</span></a> 
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeFive');" ><span class="f-da">2、球面向心流模拟</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="m-btnList">
                    <a class="f-ib u-btn u-btn-lg u-btn-orange" id="j-startLearn" href="javascript:void(0);" onclick="LoadExam(2);">
                        <span class="f-db">参加测试</span> 
                    </a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>
