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
            $("#ExpFive").addClass("select");
            $("#curMode").text($("#ModeTwo").prev().text());
            $("#curitem").text($("#ExpFive").children().text());
        });
    </script>
    <div id="WebGLLayOut">
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验五、水驱油效率实验</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 在驱油剂波及范围内，所驱替出的原油体积与总含油体积的比值称为驱油效率。驱油效率与原始含油分布状态、岩石孔隙度结构、岩石表面性质、驱油剂性质等有关。油水流动被复杂的毛细管现象所左右，由毛管力和润湿性作用所产生的阻力而圈闭的油膜、油滴、油块等，在通常情况下成为所谓“死油”。为了使“死油”投入流动，必须克服由此而产生的阻力。因此，微观状态下的界面现象，包括油水间的界面张力、润湿接触角、孔隙大小（分布）和孔喉比等是决定驱油效率的关键因素，所以降低毛管力，使岩石表面由亲油转变为亲水是增加驱油效率最关键的措施之一。本实验是以一维水驱油理论为基础，利用外部注水进行驱油，模拟实际非活塞式驱替过程。水驱油采用恒速法，在岩样出口端记录每种流体的产量、岩样两端的压差，计算水驱油效率。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）驱油效率概念，水驱油效率的影响因素；<br/>（2）水驱残余油微观分布特征；<br/>（3）水驱油物理实验流程。</p></div>
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
                <embed id="forfunex" src="/Flash/水驱油效率实验.swf"
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
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeOne');" ><span class="f-da">1、三种类型毛管压力下的水驱油效率实验</span></a>
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeTwo');" ><span class="f-da">2、三种类型原油粘度级别下的水驱油效率实验</span></a> 
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeTwo');" ><span class="f-da">2、三种类型原油密度级别下的水驱油效率实验</span></a> 
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