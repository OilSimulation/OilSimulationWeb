<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl" %>
    <style type="text/css">
			div#canvas-frame {
				border: none;
				cursor: pointer;
				width: 100%;
				height: 400px;
				background-color: #EEEEEE;
			}

		</style>
        		<script>
        		    var renderer, stats, frame;
        		    function initThree() {
        		        frame = document.getElementById('canvas-frame');
        		        width = document.getElementById('canvas-frame').clientWidth;
        		        height = document.getElementById('canvas-frame').clientHeight;
//        		        renderer = new THREE.WebGLRenderer({
//        		            antialias: true
//        		        }); //显卡渲染

        		                		        renderer = new THREE.CanvasRenderer({
        		                		            antialias: true
        		                		        });//软件渲染
        		        renderer.setSize(width, height);
        		        document.getElementById('canvas-frame').appendChild(renderer.domElement);
        		        //renderer.setClearColorHex(0xFFFFFF, 1.0);


        		        window.addEventListener('resize', onWindowResize, false);
        		    }

        		    var camera;
        		    function initCamera() {

        		        camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 10000); //透视
        		        camera.position.z = 1000;

        		        //        		        camera = new THREE.OrthographicCamera(0, 600, 3000, 0, 1, 5000);//正交投影，
        		        //        		        camera.position.set(500, 3000, 4000);
        		        //        		        

        		        camera.lookAt(new THREE.Vector3(0, 0, 0)); //视野中心坐标
        		    }

        		    var scene;
        		    function initScene() {
        		        scene = new THREE.Scene();
        		    }

        		    var light;
        		    function initLight() {
        		        light = new THREE.PointLight(0x00FF00);
        		    }

        		    var cube;
        		    var mesh;
        		    var varCubeGeometry;
        		    var container = new THREE.Object3D();
        		    var geometry = new THREE.Geometry();
        		    //加载模型
        		    function initObject(x, y, z, color) {
                        
                        
        		        return;
        		        varCubeGeometry = new THREE.CubeGeometry(90, 90, 90);
        		        cube = new THREE.Mesh(varCubeGeometry,
                                                    new THREE.MeshBasicMaterial({
                                                        color: color

                                                    }));
        		        cube.position.x = x;
        		        cube.position.z = y;
        		        cube.position.y = z;
        		        cube.updateMatrix();
        		        geometry.merge(cube.geometry, cube.matrix);
        		        //THREE.GeometryUtils.merge(geometry, cube);

        		        //scene.add(cube);

        		    }


        		    //计算当前方格颜色.
        		    //selMaxColor用户选的颜色最大值,
        		    function CaculateColor(selMaxColorR, selMaxColorG, selMaxColorB, selMinColorR, selMinColorG, selMinColorB, curColor, maxColor, minColor) {
        		        var h, s, l, H, S, L;
        		        var minInfo = RGB2HSL(selMinColorR, selMinColorG, selMinColorB);
        		        var maxInfo = RGB2HSL(selMaxColorR, selMaxColorG, selMaxColorB);
        		        h = minInfo["H"];
        		        s = minInfo["S"];
        		        l = minInfo["L"];

        		        H = maxInfo["H"];
        		        S = maxInfo["S"];
        		        L = maxInfo["L"];

        		        var info;

        		        if (0.0 == maxColor || curColor == maxColor) {
        		            info = { "R": selMaxColorR, "G": selMaxColorG, "B": selMaxColorB };
        		            return info;
        		        }
        		        else {
        		            if (minColor == curColor) {
        		                info = { "R": selMinColorR, "G": selMinColorG, "B": selMinColorB };
        		                return info;
        		            }
        		            var fStep = (curColor - minColor) / (maxColor - minColor) * (H - h) + h;

        		            if (fStep < 0.0) {
        		                fStep += 1;
        		            }
        		            return HSL2RGB(fStep, s, l);

        		        }
        		    }

        		    //返回 R:VALUE,G:VALUE,B:VALUE
        		    function HSL2RGB(h, sl, l) {
        		        var v;
        		        var r, g, b;
        		        r = l;   // default to gray
        		        g = l;
        		        b = l;
        		        v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
        		        if (v > 0) {
        		            var m;
        		            var sv;
        		            var sextant;
        		            var fract, vsf, mid1, mid2;
        		            m = l + l - v;
        		            sv = (v - m) / v;
        		            h *= 6.0;
        		            sextant = parseInt(h);
        		            fract = h - sextant;
        		            vsf = v * sv * fract;
        		            mid1 = m + vsf;
        		            mid2 = v - vsf;
        		            switch (sextant) {

        		                case 0:
        		                    r = v;
        		                    g = mid1;
        		                    b = m;
        		                    break;
        		                case 1:
        		                    r = mid2;
        		                    g = v;
        		                    b = m;
        		                    break;
        		                case 2:
        		                    r = m;
        		                    g = v;
        		                    b = mid1;
        		                    break;
        		                case 3:
        		                    r = m;
        		                    g = mid2;
        		                    b = v;
        		                    break;
        		                case 4:
        		                    r = mid1;
        		                    g = m;
        		                    b = v;
        		                    break;
        		                case 5:
        		                    r = v;
        		                    g = m;
        		                    b = mid2;
        		                    break;
        		            }
        		        }
        		        var info = { "R": parseInt(r * 255), "G": parseInt(g * 255), "B": parseInt(b * 255) };
        		        return info;
        		    }

        		    //返回H:VALUE,S:VALUE,L:VALUE
        		    function RGB2HSL(ColorR, ColorG, ColorB) {
        		        var r = ColorR / 255.0;
        		        var g = ColorG / 255.0;
        		        var b = ColorB / 255.0;
        		        var v;
        		        var m;
        		        var vm;
        		        var r2, g2, b2;
        		        var h = 0; // default to black
        		        var s = 0;
        		        var l = 0;
        		        v = r > g ? r : g;
        		        v = v > b ? v : b;
        		        m = r > b ? b : r;
        		        m = m > b ? b : m;
        		        l = (m + v) / 2.0;
        		        if (l <= 0.0) {
        		            return;
        		        }
        		        vm = v - m;
        		        s = vm;
        		        if (s > 0.0) {
        		            s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
        		        }
        		        else {
        		            return;
        		        }
        		        r2 = (v - r) / vm;
        		        g2 = (v - g) / vm;
        		        b2 = (v - b) / vm;
        		        if (r == v) {
        		            h = (g == m ? 5.0 + b2 : 1.0 - g2);
        		        }
        		        else if (g == v) {
        		            h = (b == m ? 1.0 + r2 : 3.0 - b2);
        		        }
        		        else {
        		            h = (r == m ? 3.0 + g2 : 5.0 - r2);
        		        }
        		        h /= 6.0;
        		        var info = { "H": h, "S": s, "L": l };
        		        return info;
        		    }



        		    //帧情况
        		    function InitStats() {
        		        stats = new Stats();
        		        stats.domElement.style.position = 'absolute';
        		        stats.domElement.style.top = '0px';
        		        frame.appendChild(stats.domElement);

        		    }

        		    var controls;
        		    function InitControl() {
        		        controls = new THREE.OrbitControls(camera, renderer.domElement);
        		    }

        		    var triangles = 160000;

        		    var geometry = new THREE.BufferGeometry();

        		    var positions = new Float32Array(triangles * 3 * 3);
        		    var normals = new Float32Array(triangles * 3 * 3);
        		    var colors = new Float32Array(triangles * 3 * 3);

        		    var color = new THREE.Color();


        		    var boxX = 50, boxY = 50;boxZ = 25;

        		    function funGetModelData() {
        		        $.ajax({
        		            type: "post",
        		            url: "/Business/GetModelData",
        		            dataType: "json",
        		            success: function (data) {
        		                var list = data.list; //模型列表数据
        		                var info, color;
        		                //alert(list.length);
        		                for (var i = 0; i < list.length; i++) {
        		                    info = CaculateColor(255, 0, 0, 0, 0, 255, list[i].Color, list[i].MaxColor, list[i].MinColor);
        		                    if (info) {
        		                        color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
        		                    }
        		                    var ax = list[i].X - boxX;
        		                    var ay = list[i].Y - boxY;
        		                    var az = list[i].Z + boxZ;

        		                    var bx = list[i].X + boxX;
        		                    var by = list[i].Y - boxY;
        		                    var bz = list[i].Z + boxZ;

        		                    var cx = list[i].X + boxX;
        		                    var cy = list[i].Y - boxY;
        		                    var cz = list[i].Z - boxZ;

        		                    var dx = list[i].X - boxX;
        		                    var dy = list[i].Y - boxY;
        		                    var dz = list[i].Z - boxZ;

        		                    var ex = list[i].X - boxX;
        		                    var ey = list[i].Y + boxY;
        		                    var ez = list[i].Z + boxZ;

        		                    var fx = list[i].X + boxX;
        		                    var fy = list[i].Y + boxY;
        		                    var fz = list[i].Z + boxZ;

        		                    var gx = list[i].X + boxX;
        		                    var gy = list[i].Y + boxY;
        		                    var gz = list[i].Z - boxZ;

        		                    var hx = list[i].X - boxX;
        		                    var hy = list[i].Y + boxY;
        		                    var hz = list[i].Z - boxZ;

        		                    positions[i] = ax;
        		                    positions[i + 1] = ay;
        		                    positions[i + 2] = az;
        		                    positions[i + 3] = ex;
        		                    positions[i + 4] = ey;
        		                    positions[i + 5] = ez;
        		                    positions[i + 6] = fx;
        		                    positions[i + 7] = fy;
        		                    positions[i + 8] = fz;


        		                    positions[i + 9] = ax;
        		                    positions[i + 10] = ay;
        		                    positions[i + 11] = az;
        		                    positions[i + 12] = bx;
        		                    positions[i + 13] = by;
        		                    positions[i + 14] = bz;
        		                    positions[i + 15] = fx;
        		                    positions[i + 16] = fy;
        		                    positions[i + 17] = fz;


        		                    positions[i + 18] = ax;
        		                    positions[i + 19] = ay;
        		                    positions[i + 20] = az;
        		                    positions[i + 21] = bx;
        		                    positions[i + 22] = by;
        		                    positions[i + 23] = bz;
        		                    positions[i + 24] = cx;
        		                    positions[i + 25] = cy;
        		                    positions[i + 26] = cz;


        		                    positions[i + 27] = ax;
        		                    positions[i + 28] = ay;
        		                    positions[i + 29] = az;
        		                    positions[i + 30] = dx;
        		                    positions[i + 31] = dy;
        		                    positions[i + 32] = dz;
        		                    positions[i + 33] = cx;
        		                    positions[i + 34] = cy;
        		                    positions[i + 35] = cz;


        		                    positions[i + 36] = ax;
        		                    positions[i + 37] = ay;
        		                    positions[i + 38] = az;
        		                    positions[i + 39] = dx;
        		                    positions[i + 40] = dy;
        		                    positions[i + 41] = dz;
        		                    positions[i + 42] = hx;
        		                    positions[i + 43] = hy;
        		                    positions[i + 44] = hz;


        		                    positions[i + 45] = ax;
        		                    positions[i + 46] = ay;
        		                    positions[i + 47] = az;
        		                    positions[i + 48] = ex;
        		                    positions[i + 49] = ey;
        		                    positions[i + 50] = ez;
        		                    positions[i + 51] = hx;
        		                    positions[i + 52] = hy;
        		                    positions[i + 53] = hz;


        		                    positions[i + 54] = gx;
        		                    positions[i + 55] = gy;
        		                    positions[i + 56] = gz;
        		                    positions[i + 57] = fx;
        		                    positions[i + 58] = fy;
        		                    positions[i + 59] = fz;
        		                    positions[i + 60] = ex;
        		                    positions[i + 61] = ey;
        		                    positions[i + 62] = ez;


        		                    positions[i + 63] = gx;
        		                    positions[i + 64] = gy;
        		                    positions[i + 65] = gz;
        		                    positions[i + 66] = hx;
        		                    positions[i + 67] = hy;
        		                    positions[i + 68] = hz;
        		                    positions[i + 69] = ex;
        		                    positions[i + 70] = ey;
        		                    positions[i + 71] = ez;


        		                    positions[i + 72] = gx;
        		                    positions[i + 73] = gy;
        		                    positions[i + 74] = gz;
        		                    positions[i + 75] = fx;
        		                    positions[i + 76] = fy;
        		                    positions[i + 77] = fz;
        		                    positions[i + 78] = bx;
        		                    positions[i + 79] = by;
        		                    positions[i + 80] = bz;


        		                    positions[i + 81] = gx;
        		                    positions[i + 82] = gy;
        		                    positions[i + 83] = gz;
        		                    positions[i + 84] = cx;
        		                    positions[i + 85] = cy;
        		                    positions[i + 86] = cz;
        		                    positions[i + 87] = bx;
        		                    positions[i + 88] = by;
        		                    positions[i + 89] = bz;


        		                    positions[i + 90] = gx;
        		                    positions[i + 91] = gy;
        		                    positions[i + 92] = gz;
        		                    positions[i + 93] = hx;
        		                    positions[i + 94] = hy;
        		                    positions[i + 95] = hz;
        		                    positions[i + 96] = dx;
        		                    positions[i + 97] = dy;
        		                    positions[i + 98] = dz;


        		                    positions[i + 99] = gx;
        		                    positions[i + 100] = gy;
        		                    positions[i + 101] = gz;
        		                    positions[i + 102] = cx;
        		                    positions[i + 103] = cy;
        		                    positions[i + 104] = cz;
        		                    positions[i + 105] = dx;
        		                    positions[i + 106] = dy;
        		                    positions[i + 107] = dz;

        		                    //color.setRGB(info["R"], info["G"], info["B"]);
        		                    for (var j = 0; j < 108; j += 3) {
        		                        colors[i * 108 + j] = color.r;
        		                        colors[i * 108 + j + 1] = color.g;
        		                        colors[i * 108 + j + 2] = color.b;
        		                    }



        		                    //initObject(list[i].X, list[i].Y, list[i].Z, color);
        		                }
        		                //container.add(new THREE.Mesh(geometry, cubeMaterial));
        		                geometry.addAttribute('position', new THREE.BufferAttribute(positions, 3));
        		                geometry.addAttribute('normal', new THREE.BufferAttribute(normals, 3));
        		                geometry.addAttribute('color', new THREE.BufferAttribute(colors, 3));

        		                geometry.computeBoundingSphere();

        		                var material = new THREE.MeshPhongMaterial({
        		                    color: 0xaaaaaa, specular: 0xffffff, shininess: 250,
        		                    side: THREE.DoubleSide, vertexColors: THREE.VertexColors
        		                });

        		                var meshss = new THREE.Mesh(geometry, material);
        		                scene.add(meshss);

        		            }


        		        });
        		    }




        		    function threeStart() {

        		        initThree();
        		        initCamera();
        		        initScene();
        		        //initLight();
        		        //initObject();
        		        funGetModelData();
        		        //scene.add(container);
        		        InitStats();
        		        //InitControl();



        		        animation();
        		        //TrackballControls,OrbitControls
        		        controls = new THREE.OrbitControls(camera, renderer.domElement);
        		        //controls = new THREE.TrackballControls(camera);
        		        controls.target.set(0, 0, 0);

        		        controls.update();
        		        //camera.toTopView();

        		    }
        		    function onWindowResize() {
        		        camera.aspect = window.innerWidth / window.innerHeight;
        		        camera.updateProjectionMatrix();
        		        renderer.setSize(window.innerWidth, window.innerHeight);
        		    }

        		    function animation() {

        		        //                cube.rotation.y += 0.01;
        		        //                if (cube.rotation.y > Math.PI * 2) {
        		        //                    cube.rotation.y -= Math.PI * 2;
        		        //                }
        		        renderer.render(scene, camera);
        		        requestAnimationFrame(animation);
        		        stats.update(); //帧情况


        		        //controls.update();
        		    }



                </script>
                <div><button onclick="funGetModelData">sdf</button></div>
                <div id="canvas-frame"></div>
