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
            $("#ModeOne").css("display","block");
            $("#ExpOne").addClass("select");
            $("#curMode").text($("#ModeOne").prev().text());
            $("#curitem").text($("#ExpOne").children().text());
        });
    </script>
    <div id="WebGLLayOut">
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验一、水驱油模拟</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 对于天然水驱或人工注水驱动油藏，油水两相渗流普遍存在。在水驱油过程中，油水饱和度都是随时间改变的，而油水粘度差、油水重率差以及毛管力必然影响两相共渗混合区的范围及其阻力的变化规律。因此，进一步深入分析油水两相渗流问题，对于正确了解水驱油藏的渗流规律，采取有效措施控制含水率的变化，从而保证注入水的均匀推进，延长高产稳产时间，提高采收率都具有极其重要的意义。<br>&nbsp; &nbsp; &nbsp; &nbsp; 为了掌握注水开发的基本原理和方法，了解注水驱油实验的基本原理和实验步骤，增强学生对石油工业的兴趣，培养学生求实严谨的科学态度，开展本实验研究。该实验以一维水驱油理论为基础，利用数值模拟技术模拟水驱油过程。实验中，依次建立一维油藏模型，输入油水PVT属性、油水相对渗透率曲线和毛管压力曲线、初始化计算参数，即可进行活塞式和非活塞式水驱油模拟。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）了解活塞式水驱油、非活塞式水驱油概念；<br/>（2）弄清影响水驱油过程非活塞性的因素。</p></div>
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
                <embed id="forfunex" src="/Flash/水驱油模拟.swf"
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
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeOne');" ><span class="f-da">1、活塞式水驱油模拟</span></a>
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeTwo');" ><span class="f-da">2、非活塞式水驱油模拟</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="m-btnList">
                    <a class="f-ib u-btn u-btn-lg u-btn-orange" id="j-startLearn" href="javascript:void(0);" onclick="LoadExam(1);">
                        <span class="f-db">参加测试</span>

                    </a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>
