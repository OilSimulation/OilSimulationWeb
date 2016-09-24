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
            $("#ModeTwo").css("display", "block");
            $("#ExpSix").addClass("select");
            $("#curMode").text($("#ModeTwo").prev().text());
            $("#curitem").text($("#ExpSix").children().text());
        });
    </script>
    <div id="WebGLLayOut">
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验六、采收率实验</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 采收率是油（气）田废弃时，累积釆油（气）量占其原始石油地质储量的百分比。水驱油采收率是注入水的宏观波及系数与微观驱油效率的乘积。控制驱油剂的宏观波及系数的主要因素有油层渗透率的差异、油层内流体的流度差异、驱替剂推进的速度高低，此外，油藏构造、裂缝、和断层分布发育状况等油藏性质以及采取的油藏工程方法（如井网注采关系等）也都会影响波及系数。驱油效率与原始含油分布状态、岩石孔隙结构、岩石表面性质、驱油剂性质等有关。微观状态下的界面现象，包括油水间的界面张力、润湿接触角、孔隙大小（分布）和孔喉比等是决定驱油效率的关键因素，所以降低毛管力，使岩石表面由亲油转变成亲水是增加驱油效率最关键的措施之一。利用数值模拟技术，建立数值模拟模型，研究不同井网类型、不同流体性质、不同相渗曲线、不同毛管压力等因素对水驱油采收率的影响。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）宏观波及系数、微观驱油效率、采收率、采收率等基本概念；<br/>（2）影响宏观波及系数的因素；<br/>（2）影响微观驱油效率的因素。</p></div>
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
                <embed id="forfunex" src="/Flash/采收率实验.swf"
                    width="400"
                    height="300"
                    align="middle"
                    quality="high"
                    bgcolor="#f0fff8"
                    menu="false"
                    play="true"
                    loop="false"
                    FlashVars=""
                    allowScriptAccess="sameDomain"
                    type="application/x-shockwave-flash"
                    pluginspage="http://www.adobe.com/go/getflashplayer">
                </embed>
                </object>
            </div> 
            <div style="padding: 10px 20px 10px 0px;">
                <div class="t-title f-f0">实验演示</div>
                <div class="m-btnList">
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeOne');" ><span class="f-da">1、三种类型束缚水饱和度下的采收率实验</span></a>
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeTwo');" ><span class="f-da">2、三种类型残余油饱和度下的采收率实验</span></a> 
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeTwo');" ><span class="f-da">3、三种类型油水相渗曲线下的采收率实验</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="m-btnList">
                    <a class="f-ib u-btn u-btn-lg u-btn-orange" id="j-startLearn">
                        <span class="f-db">参加测试</span> 
                    </a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>