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
            $("#wizard").hide();
            $("#explainPage").show();   
            $(".liNav").each(function () { $(this).removeClass("select"); });
            $(".zbar ul").each(function () { $(this).css("display", "none"); });
            $("#ModeTwo").css("display", "block");
            $("#ExpFive").addClass("select");
            $("#curMode").text($("#ModeTwo").prev().text());
            $("#curitem").text($("#ExpFive").children().text()); 
        });
        function ShowVirControlDiv(sId) {
            $("#Virtual221").hide();
            $("#Virtual222").hide();
            $("#Virtual223").hide();
            switch (sId) {
                case 221:
                    $("#Virtual221").show();
                    break;
                case 222:
                    $("#Virtual222").show();
                    break;
                case 223:
                    $("#Virtual223").show();
                    break;
            }
        };
        function ShowWizard()
        {
            $("#wizard").show();
            $("#explainPage").hide();   
        }
        $(function () {
            $("#wizard").scrollable();
        }); 
    </script>
    <div id="WebGLLayOut">
        <div id="Virtual221" style="display:inline;">
            毛细管压力:
            <select class="DropDownList v1Mode">
                <option value="2211">无毛管</option>
                <option value="2212">低毛管</option>
                <option value="2213">高毛管</option>
            </select>
        </div>
        <div id="Virtual222" style="display:none;">
            原油粘度级别:
            <select class="DropDownList v1Mode">
                <option value="2221">低粘度0.5</option>
                <option value="2222">中粘度5</option>
                <option value="2223">高粘度50</option>
            </select>
        </div>
        <div id="Virtual223" style="display:none;">
            原油密度级别:
            <select class="DropDownList v1Mode">
                <option value="2231">低密度0.7</option>
                <option value="2232">中密度0.9</option>
                <option value="2233">高密度1.15</option>
            </select>
        </div>
        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
    </div>
    <div id="MainLayOut" style="width:100%;">
        <div style="width:100%; float:left;">
            <div id="wizard" style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1); display:none;">  
                <h2>实验流程</h2>
                <div class="items">  
                    <div class="page">  
                       <div class="top f-f0">（一）配制实验用油和水</div>
                       <div class="bottom f-richEditorText"><p>将脱气原油脱水、过滤，按一定比列加入煤油，充分搅拌后，测量模拟油粘度。一般模拟油粘度与地层油粘度一致。<br/>采用按地层水离子组成与矿化度配置，或用标准盐水。标准盐水配方：Nacl:Cacl2:Mgcl2&bull;6H2O=7:0.6:0.4(质量比)。</p></div>
                       <div class="btn_nav"> 
                          <input type="button" class="next f-ib u-btn u-btn-orange" value="下一步»" /> 
                       </div> 
                    </div> 
                    <div class="page"> 
                       <div class="top f-f0">（二）抽真空饱和盐水（地层水）</div>
                       <div class="bottom f-richEditorText"><p>将处理好的岩样放入真空干燥器中，真空度达到133.3Pa时，抽空2~8个小时。</p></div>
                       <object id="Object1" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" 
                            codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0">
                            <param name="movie" value="/seufld/seufld/flash/focus2.swf">
                            <param name="quality" value="high">
                            <param name="bgcolor" value="#F0F0F0">
                            <param name="menu" value="false"> 
                            <param name="wmode" value="Opaque"><!--Window|Opaque|Transparent-->
                            <param name="FlashVars" value="">
                            <param name="allowScriptAccess" value="sameDomain">
                            <embed id="forfunex" src="/Flash/演示-饱.swf"
                                width="750"
                                height="550"
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
                       <div class="btn_nav"> 
                           <input type="button" class="prev f-ib u-btn u-btn-orange" style="float:left" value="«上一步" /> 
                           <input type="button" class="next f-ib u-btn u-btn-orange" value="下一步»" /> 
                       </div> 
                    </div> 
                    <div class="page"> 
                       <div class="top f-f0">（三）将实验用油（模拟油）放入相应容器中</div> 
                       <div class="bottom f-richEditorText"></div>
                       <object id="Object3" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
                            codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0">
                            <param name="movie" value="/seufld/seufld/flash/focus2.swf">
                            <param name="quality" value="high">
                            <param name="bgcolor" value="#F0F0F0">
                            <param name="menu" value="false">
                            <param name="wmode" value="opaque"><!--Window|Opaque|Transparent-->
                            <param name="FlashVars" value="">
                            <param name="allowScriptAccess" value="sameDomain">
                            <embed id="forfunex" src="/Flash/演示-抽油.swf"
                                width="750"
                                height="550"
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
                       <div class="btn_nav"> 
                           <input type="button" class="prev f-ib u-btn u-btn-orange" style="float:left" value="«上一步" /> 
                           <input type="button" class="next f-ib u-btn u-btn-orange" value="下一步»" /> 
                       </div> 
                    </div>
                    <div class="page"> 
                       <div class="top f-f0">（四）将实验用水放入相应容器中</div> 
                       <div class="bottom f-richEditorText"></div>
                       <object id="Object2" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" width="400" height="300"
                            codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0">
                            <param name="movie" value="/seufld/seufld/flash/focus2.swf">
                            <param name="quality" value="high">
                            <param name="bgcolor" value="#F0F0F0">
                            <param name="menu" value="false">
                            <param name="wmode" value="opaque"><!--Window|Opaque|Transparent-->
                            <param name="FlashVars" value="">
                            <param name="allowScriptAccess" value="sameDomain">
                            <embed id="forfunex" src="/Flash/演示-抽水.swf"
                                width="750"
                                height="550"
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
                       <div class="btn_nav"> 
                           <input type="button" class="prev f-ib u-btn u-btn-orange" style="float:left" value="«上一步" /> 
                           <input type="button" class="next f-ib u-btn u-btn-orange" value="下一步»" /> 
                       </div> 
                    </div>
                    <div class="page"> 
                       <div class="top f-f0">（五）流程接入</div> 
                       <div class="bottom f-richEditorText"></div>
                       <object id="Object4" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" width="400" height="300"
                            codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0">
                            <param name="movie" value="/seufld/seufld/flash/focus2.swf">
                            <param name="quality" value="high">
                            <param name="bgcolor" value="#F0F0F0">
                            <param name="menu" value="false">
                            <param name="wmode" value="opaque"><!--Window|Opaque|Transparent-->
                            <param name="FlashVars" value="">
                            <param name="allowScriptAccess" value="sameDomain">
                            <embed id="forfunex" src="/Flash/演示-水驱油.swf"
                                width="750"
                                height="550"
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
                       <div class="btn_nav"> 
                           <input type="button" class="prev f-ib u-btn u-btn-orange" style="float:left" value="«上一步" /> 
                           <input type="button" class="next f-ib u-btn u-btn-orange" value="下一步»" /> 
                       </div> 
                    </div>
                    <div class="page"> 
                       <div class="top f-f0">（六）数据处理</div> 
                       <div class="btn_nav"> 
                           <input type="button" class="prev f-ib u-btn u-btn-orange" style="float:left" value="«上一步" /> 
                           <input type="button" class="next f-ib u-btn u-btn-orange" id="sub" value="确定" /> 
                       </div> 
                    </div> 
                </div>          
            </div>
            <div id="explainPage" style="margin-right: 415px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
                <h2>实验五、水驱油效率实验</h2>
                <div class="top f-f0">实验简介</div>
                <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 在驱油剂波及范围内，所驱替出的原油体积与总含油体积的比值称为驱油效率。驱油效率与原始含油分布状态、岩石孔隙度结构、岩石表面性质、驱油剂性质等有关。油水流动被复杂的毛细管现象所左右，由毛管力和润湿性作用所产生的阻力而圈闭的油膜、油滴、油块等，在通常情况下成为所谓“死油”。为了使“死油”投入流动，必须克服由此而产生的阻力。因此，微观状态下的界面现象，包括油水间的界面张力、润湿接触角、孔隙大小（分布）和孔喉比等是决定驱油效率的关键因素，所以降低毛管力，使岩石表面由亲油转变为亲水是增加驱油效率最关键的措施之一。本实验是以一维水驱油理论为基础，利用外部注水进行驱油，模拟实际非活塞式驱替过程。水驱油采用恒速法，在岩样出口端记录每种流体的产量、岩样两端的压差，计算水驱油效率。</p></div>
    
                <div class="top f-f0">预备知识</div>
                <div class="bottom f-richEditorText"><p>（1）驱油效率概念，水驱油效率的影响因素；<br/>（2）水驱残余油微观分布特征；<br/>（3）水驱油物理实验流程。</p></div>
            
                <div class="top f-f0">实验仿真</div>
                <div class="imgpic">
                    <ul> 
                       <li>
                           <div class="pro-pic">
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(221);LoadUrl('/VirExperiment/VirtualMode2211');"><img src="<%=Url.Content("~/Images/驱油效率毛管力.png")%>"></a>
                           </div>
                           <p>
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(221);LoadUrl('/VirExperiment/VirtualMode2211');">三种类型毛管压力下的水驱油效率实验</a>
                           </p>
                       </li> 
                       <li>
                           <div class="pro-pic">
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(222);LoadUrl('/VirExperiment/VirtualMode2221');"><img src="<%=Url.Content("~/Images/驱油效率原油粘度.png")%>"></a>
                           </div>
                           <p>
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(222);LoadUrl('/VirExperiment/VirtualMode2221');">三种类型原油粘度级别下的水驱油效率实验</a>
                           </p>
                       </li> 
                       <li>
                           <div class="pro-pic">
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(223);LoadUrl('/VirExperiment/VirtualMode2231');"><img src="<%=Url.Content("~/Images/驱油效率原油密度.png")%>"></a>
                           </div>
                           <p>
                                <a href="javascript:void(0);" onclick="ShowVirControlDiv(223);LoadUrl('/VirExperiment/VirtualMode2231');">三种类型原油密度级别下的水驱油效率实验</a>
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
                <embed id="forfunex" src="/Flash/水驱油效率实验.swf"
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
            <div style="padding: 10px 20px 10px 0px; display:none;" >
                <div class="t-title f-f0">实验演示</div>
                <div class="m-btnList">
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(221);LoadUrl('/VirExperiment/VirtualMode2211');" ><span class="f-da">1、三种类型毛管压力下的水驱油效率实验</span></a>
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(222);LoadUrl('/VirExperiment/VirtualMode2221');" ><span class="f-da">2、三种类型原油粘度级别下的水驱油效率实验</span></a> 
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(223);LoadUrl('/VirExperiment/VirtualMode2231');" ><span class="f-da">2、三种类型原油密度级别下的水驱油效率实验</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 10px 0px; " >
                <div class="t-title f-f0">实验流程</div>
                <div class="m-btnList"> 
                    <a href="javascript:void(0);" onclick="ShowVirControlDiv(000);ShowWizard();" ><span class="f-da">观看实验流程</span></a> 
                </div>
            </div>
            <div style="padding: 10px 20px 20px 0px;">
                <div class="t-title f-f0">随堂测试</div>
                <div class="m-btnList">
                    <a class="f-ib u-btn u-btn-lg u-btn-orange" id="j-startLearn" href="javascript:void(0);" onclick="ShowVirControlDiv(000);LoadExam(5);">
                        <span class="f-db">参加测试</span> 
                    </a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>