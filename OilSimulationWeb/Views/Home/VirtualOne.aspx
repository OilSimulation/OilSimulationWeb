<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <script type="text/javascript">
        var sUrlPre = "/VirExperiment/VirtualMode";
        $(document).ready(function () {
            $("#WebGLLayOut").hide();
            $("#MainLayOut").show();
            $(".liNav").each(function () { $(this).removeClass("select"); });
            $(".zbar ul").each(function () { $(this).css("display", "none"); });
            $("#ModeTwo").css("display", "block");
            $("#ExpFour").addClass("select");
            $("#curMode").text($("#ModeTwo").prev().text());
            $("#curitem").text($("#ExpFour").children().text()); 
        });
        function ShowVirControlDiv(sId) {
            $("#Virtual211").hide();
            $("#Virtual212").hide();
            $("#Virtual213").hide();
            switch (sId) {
                case 211:
                    $("#Virtual211").show();
                    break;
                case 212:
                    $("#Virtual212").show();
                    break;
                case 213:
                    $("#Virtual213").show();
                    break; 
            }
        }
    </script>
    <div id="WebGLLayOut">
        <div id="Virtual211" style="display:inline;">
            毛细管压力:
            <select class="DropDownList v1Mode">
                <option value="2111">0.5</option>
                <option value="2112">1</option>
                <option value="2113">2</option>
                <option value="2114">自定义</option>
            </select>
        </div>
        <div id="Virtual212" style="display:none;">
            油水比重差:
            <select class="DropDownList v1Mode">
                <option value="2121">0.7</option>
                <option value="2122">0.8</option>
                <option value="2123">0.9</option>
                <option value="2124">自定义</option>
            </select>
        </div>
        <div id="Virtual213" style="display:none;">
            油水粘度差:
            <select class="DropDownList v1Mode">
                <option value="2131">0.5</option>
                <option value="2132">10</option>
                <option value="2133">50</option>
                <option value="2134">自定义</option>
            </select>
        </div>
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验四、非活塞式水驱油影响因素实验</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 在实际生产中，水渗入到含油区后，不能将全部原油置换出去，而是出现了一个油水混合区，这种驱油方式称为非活塞式水驱油。非活塞式水驱油影响因素主要有油层非均质性、毛管力、重率差、粘度差。首先，由于油层的非均质性，孔道大小不等，水在不同孔道中驱油的运动速度不等，油水界面不会像活塞那样整齐推进；其次，由于岩石润湿性的影响，会产生毛细管效应，形成油水共存的区域；再次，由于油水密度差，油水相遇后会形成上油下水的油水两相共存区，但只是在油水重率差较大，油层较厚情况下，这种分离才明显。此外，在外压差的作用下，由于大毛管通道横截面积大，阻力小，因而水首先渗入大毛管；又由于原油粘度大于水的粘度，大孔道阻力越来越小，因而水窜越来越快，形成严重的指进现象。为了更好的掌握非活塞式水驱油过程中的影响因素，建立一维流动模型，利用数值模拟技术研究不同影响因素下的饱和度分布特征。</p></div>
    
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
                <embed id="forfunex" src="/Flash/非活塞式水驱油影响因素.swf"
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
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(211);LoadUrl('/VirExperiment/VirtualMode2111');" ><span class="f-da">1、不同毛细管力对水驱油影响模拟</span></a>
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(212);LoadUrl('/VirExperiment/VirtualMode2121');" ><span class="f-da">2、不同油水比重差对水驱油影响模拟</span></a> 
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(213);LoadUrl('/VirExperiment/VirtualMode2131');" ><span class="f-da">3、不同油水粘度差对水驱油影响模拟</span></a> 
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