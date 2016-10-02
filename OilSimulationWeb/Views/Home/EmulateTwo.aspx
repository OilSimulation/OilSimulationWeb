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
            $("#ExpEight").addClass("select");
            $("#curMode").text($("#ModeThree").prev().text());
            $("#curitem").text($("#ExpEight").children().text());
        });
        function ShowVirControlDiv(sId) {
            $("#Simulation321").hide();
            $("#Simulation322").hide();
            $("#Simulation323").hide();
            $("#Simulation324").hide();
            switch (sId) {
                case 321:
                    $("#Simulation321").show();
                    break;
                case 322:
                    $("#Simulation322").show();
                    break;
                case 323:
                    $("#Simulation323").show();
                    break;
                case 324:
                    $("#Simulation324").show();
                    break;
            }
        }
    </script>
    <div id="WebGLLayOut">
        <div id="Simulation321" style="display:inline;">
            不同注水时机:
            <select class="DropDownList v1Mode">
                <option value="3211">早期（同采同注）</option>
                <option value="3212">中期开发后半年</option>
                <option value="3213">晚期开发后两年</option>
                <option value="3214">自定义</option>
            </select>
        </div>
        <div id="Simulation322" style="display:none;">
            不同注采比:
            <select class="DropDownList v1Mode">
                <option value="3221">注采比0.8</option>
                <option value="3222">注采比1</option>
                <option value="3223">注采比1.2</option>
                <option value="3224">自定义</option>
            </select>
        </div>
        <div id="Simulation323" style="display:none;">
            不同最大井底注入压力:
            <select class="DropDownList v1Mode">
                <option value="3231">35MPA</option>
                <option value="3232">40MPA</option>
                <option value="3233">50MPA</option>
                <option value="3234">自定义</option>
            </select>
        </div>
        <div id="Simulation324" style="display:none;">
            不同最小井底流压:
            <select class="DropDownList v1Mode">
                <option value="3241">1MPA</option>
                <option value="3242">5MPA</option>
                <option value="3243">10MPA</option>
                <option value="3244">自定义</option>
            </select>
        </div>
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验八、注采系统方案设计与开发效果预测</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 注采系统设计包括地层压力、流动压力、注水压力、注水时机、注釆比等参数的优化设计。保持一定的地层压力水平，是保证油井具有旺盛的生产能力，实现油田较长时间稳产的重要条件；为了既保持一定的油井产量，又避免引起油井脱气半径扩大而使液体在油层和井筒中流动条件变差，生产井流动压力应控制在正常合理范围内；合理的注水压力既要考虑注水井的合理配注量，又要使注水压力不超过油层破裂压力；注水时机可以分为超前注水、早期注水、和晚期注水，需要根据油田天然能量的大小、油田的大小和对油田产量的要求以及油田的开采特点和采油方式来考虑合理注水时机；注釆比即地层条件下注水量与产液量之比，在注水开发过程中，一方面要充分提高注水井吸水能力和采油能力，另一方面要防止压破地层产生裂缝，防止注入水沿裂缝窜进，避免油井暴性水淹。因此，如何建立油藏合理的注釆压力系统，是油藏注水开发中的重要研究课题，本实验通过相关计算公式和数值模拟方法对注采系统进行优化设计。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）注水时机、井底流压、注入压力、注釆比、破裂压力。</p></div>
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
                <embed id="forfunex" src="/Flash/注采系统优化.swf"
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
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(321);LoadUrl('/Simulation/SimMode3211');" ><span class="f-da">1、不同注水时机方案设计与开发效果预测实验</span></a>
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(322);LoadUrl('/Simulation/SimMode3221');" ><span class="f-da">2、不同注采比方案设计与开发效果预测实验</span></a> 
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(323);LoadUrl('/Simulation/SimMode3231');" ><span class="f-da">3、不同最大井底注入压力方案设计与开发效果预测实验</span></a> 
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(324);LoadUrl('/Simulation/SimMode3241');" ><span class="f-da">4、不同最低井底流压方案设计与开发效果预测实验</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="m-btnList">
                    <a class="f-ib u-btn u-btn-lg u-btn-orange" id="j-startLearn" href="javascript:void(0);" onclick="LoadExam(8);">
                        <span class="f-db">参加测试</span> 
                    </a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>