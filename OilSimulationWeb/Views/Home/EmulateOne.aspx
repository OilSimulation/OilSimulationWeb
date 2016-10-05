<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <script type="text/javascript">
        var sUrlPre = "/Simulation/SimMode";
        $(document).ready(function () {
            $("#WebGLLayOut").hide();
            $("#MainLayOut").show();
            $(".liNav").each(function () { $(this).removeClass("select"); });
            $(".zbar ul").each(function () { $(this).css("display", "none"); });
            $("#ModeThree").css("display", "block");
            $("#ExpSeven").addClass("select");
            $("#curMode").text($("#ModeThree").prev().text());
            $("#curitem").text($("#ExpSeven").children().text());
        });
        function ShowVirControlDiv(sId) {
            $("#Simulation311").hide();
            $("#Simulation312").hide();
            switch (sId) {
                case 311:
                    $("#Simulation311").show();
                    break;
                case 312:
                    $("#Simulation312").show();
                    break; 
            }
        }
    </script>
    <div id="WebGLLayOut">
        <div id="Simulation311" style="display:inline;">
            井距:
            <select class="DropDownList v1Mode">
                <option value="3111">100</option>
                <option value="3112">200</option>
                <option value="3113">300</option>
                <option value="3114">自定义</option>
            </select>
        </div>
        <div id="Simulation312" style="display:none;">
            井网:
            <select class="DropDownList v1Mode">
                <option value="3121">5点</option>
                <option value="3122">7点</option>
                <option value="3123">9点</option>
                <option value="3124">自定义</option>
            </select>
        </div>
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验七、井网井距方案设计与开发效果预测</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 油田的井网形式一般分为规则井网和不规则井网两种。当储层较均质时，适宜用规则井网开采；而储层非均质严重时，宜用不规则井网开采。规则井网一般指面积注水井网，包括直线形井网、交错线形井网、四点井网、五点井网、七点井网、九点井网和反九点井网等。开发井网部署是油田开发中的一个重要问题，需要选择合理的布井方式并确定合理的井网密度，它不仅关系到采油速度、稳产年限，而且关系到油田最终采收率和经济效益。本实验采用数值模拟技术，设计不同的井网井距，模拟开发过程，预测不同时间的单井日产油量、单井日产水量、年产油量、年产水量、含水率、累积产油量、累积产水量、釆出程度、单井日注水量、年注水量、累积注水量、注水压力、地层压力、油井井底流压等技术指标，预测开发效果。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）油田注水方式的确定；<br/>（2）不同注采井网的主要特征参数；<br/>（3）合理井网密度的确定方法。</p></div>
            
                <div class="top f-f0">实验仿真</div>
                <div class="imgpic">
                    <ul> 
                       <li>
                           <div class="pro-pic">
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(311);LoadUrl('/Simulation/SimMode3111');"><img src="<%=Url.Content("~/Images/井距.png")%>"></a>
                           </div>
                           <p>
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(311);LoadUrl('/Simulation/SimMode3111');">不同井距网方案设计与开发效果预测实验</a>
                           </p>
                       </li> 
                       <li>
                           <div class="pro-pic">
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(312);LoadUrl('/Simulation/SimMode3121');"><img src="<%=Url.Content("~/Images/井网.png")%>"></a>
                           </div>
                           <p>
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(312);LoadUrl('/Simulation/SimMode3121');">不同井网方案设计与开发效果预测实验</a>
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
                <embed id="forfunex" src="/Flash/井网井距设计.swf"
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
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(311);LoadUrl('/Simulation/SimMode3111');" ><span class="f-da">1、不同井网方案设计与开发效果预测实验</span></a>
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(312);LoadUrl('/Simulation/SimMode3121');" ><span class="f-da">2、不同井距网方案设计与开发效果预测实验</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="m-btnList">
                    <a class="f-ib u-btn u-btn-lg u-btn-orange" id="j-startLearn" href="javascript:void(0);" onclick="ShowVirControlDiv(000);LoadExam(7);">
                        <span class="f-db">参加测试</span> 
                    </a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>