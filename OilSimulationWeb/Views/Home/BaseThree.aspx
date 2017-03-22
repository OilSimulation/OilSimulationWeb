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
            $("#ExpThree").addClass("select");
            $("#curMode").text($("#ModeOne").prev().text());
            $("#curitem").text($("#ExpThree").children().text());
        });
    </script>
    <div id="WebGLLayOut">
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验三、弹性不稳定渗流模拟</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 在渗流过程中，若各运动要素（压力及流速等）要随时间变化，则称为不稳定流动。在井的生产过程中，压力波的传播分为两个阶段，压力波传到边界之前为压力波传播的第一阶段，传到边界之后为压力波传播的第二阶段。在第一阶段，压降漏斗不断扩大，除井点外各点均加深；压降曲线传到边界以后开始压力波传播的第二阶段：对于定压边界，在相当长时间后，渗流过程就趋于稳定，压力分布曲线和稳定渗流时的对数曲线一致；对于封闭边界，边界处的压力不断下降，随时间增加，从井壁到边界各点压降幅度逐渐趋于一致，这种状态称为“拟稳定状态”，在该状态下，地层中任意一点压降速度为常数，直到地层内各点压力低于饱和压力时，弹性开采阶段结束。通过建立二维平面流动模型，输入油水PVT属性、油水相对渗透率曲线和毛管压力曲线，模拟计算不稳定渗流过程中的压力分布。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）弹性不稳定渗流的概念；<br/>（2）定压边界压力波传播变化规律；<br/>（3）封闭边界压力波传播变化规律。</p></div>
                
                <div class="top f-f0">实验仿真</div>
                <div class="imgpic">
                    <ul> 
                       <li>
                           <div class="pro-pic">
                                <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeSix');"><img src="<%=Url.Content("~/Images/稳定水压弹性驱动.png")%>"></a>
                           </div>
                           <p>
                                <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeSix');">稳定水压弹性驱动模拟</a>
                           </p>
                       </li> 
                       <li>
                           <div class="pro-pic">
                                <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeSeven');"><img src="<%=Url.Content("~/Images/封闭边界.png")%>"></a>
                           </div>
                           <p>
                                <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeSeven');">封闭弹性驱动模拟</a>
                           </p>
                       </li> 
                    </ul>
                </div>
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
                <embed id="forfunex" src="../Flash/弹性不稳定渗流模拟.swf"
                    width="400"
                    height="331"
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
            <div style="padding: 10px 20px 10px 0px; display:none;">
                <div class="t-title f-f0">实验演示</div>
                <div class="m-btnList">
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeSix');" ><span class="f-da">1、稳定水压弹性驱动模拟</span></a>
                    <a href="javascript:void(0);" onclick="LoadUrl('/Base/BaseModeSeven');" ><span class="f-da">2、封闭弹性驱动模拟</span></a>  
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;position: relative;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="g-sd2">
                    <div id="j-courseImg" class="m-recimg canlick">
                        <img class="img" id="" src="../Images/exam.png" alt="图片" width="350" height="240" />
                        <a  onclick="LoadExam(3);" class="f-db clickBtn"></a>
                    </div>
                </div> 
            </div>
            <div style="padding: 10px 20px 20px 0px;"> 
                <div class="t-title f-f0">规格型号：CB-SIM03</div> 
            </div>
        </div>
    </div>  
</asp:Content>