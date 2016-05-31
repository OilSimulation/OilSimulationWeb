/**
 * @author mrdoob / http://mrdoob.com/
 */

THREE.MyLoader = function ( manager ) {

	this.manager = ( manager !== undefined ) ? manager : THREE.DefaultLoadingManager;

};
var modelJsonData;
var circleGroup;
THREE.MyLoader.prototype = {

    constructor: THREE.MyLoader,

    load: function (url, pData, modelId, onLoad, onProgress, onError) {

        var scope = this;

        var loader = new THREE.XHRLoader(scope.manager);
        loader.setCrossOrigin(this.crossOrigin);
        loader.load(url, pData, function (text) {
            if (geometry == undefined) {

                if (modelId == 15) {
                    onLoad(scope.LoadBufferGeometryCircleMode(text));
                    //onLoad(scope.DrawPipe(40, 40, 100, 4));
                }
                else {
                    onLoad(scope.LoadBufferGeometryMode(text));
                    //onLoad(scope.LoadMode(text));
                    //onLoad(scope.DrawPipe(20, 40, 100, 4));
                }

                parent.postMessage("HideLoading()", "*");
            }
            else {
                if (modelId == 15) {
                    onLoad(scope.ChangeBufferGeometryCircleColor(text));
                }
                else {
                    onLoad(scope.ChangeBufferGeometryColor(text));
                    //onLoad(scope.ChangeColor(pData, text));
                }
            }
        }, function () {
            onProgress;
            //if (geometry == undefined)
            //parent.ShowLoading();
        }, onError);

    },

    //url:访问地址,pdata:json参数,scene三维场景
    LoadWell: function (url, pData, scene) {
        var scope = this;
        var loader = new THREE.XHRLoader(scope.manager);
        //loader.setCrossOrigin(this.crossOrigin);
        loader.load(url, pData, function (text) {

            var jsonData = JSON.parse(text);
            for (var i = 0; i < jsonData.length; i++) {
                scene.add(scope.AddWell(jsonData[i].x, jsonData[i].y, jsonData[i].z, 10));
            }

        });
    },



    ChangeColor: function (data, text) {
        var jsonData = JSON.parse(text);
        modelJsonData = jsonData;
        var info, color;
        var colors = [];
        for (var i = 0; i < jsonData.Data.length; i++) {
            info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[i][0], jsonData.mm[1], jsonData.mm[0]);
            if (info) {
                color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
                colors[i] = color;
            }
        }
        //修改颜色 
        setFacesVertexColors(geometry, colors);
    },

    ChangeNetPointColor: function (text) {
        var jsonData = JSON.parse(text);
        var colors = geometry.getAttribute('color');
        var colorsArray = colors.array;
        var info, color;
        var colors = [];
        for (var i = 0; i < jsonData.Data.length; i++) {
            info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[i][0], jsonData.mm[1], jsonData.mm[0]);
            if (info) {
                color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
                colors[i] = color;
            }
        }
        //var color = new THREE.Color(0xffffff);
        for (var i = 0; i < colorsArray.length; i++) {
            //color.setHSL(Math.random()*0xff, 1, 0.5);

            colorsArray[i + 0] = colors[0];
            colorsArray[i + 1] = colors[1];
            colorsArray[i + 2] = colors[2];

        }
        colors.needsUpdate = true;

    },

    //增加井xyz坐标,h 高度,返回 mesh,n:井名称,zW 模型Z坐标方向宽度/2
    AddWell: function (x, y, z, zW, h, n) {

        //
        var textGeo = new THREE.TextGeometry("O", {

            size: 10,
            height: zW,
            font: "helvetiker",
            //weight:"bold",//normal
            style: "normal"

        });
        var wellMaterial = new THREE.MeshBasicMaterial({ color: 0xAA3264, vertexColors: THREE.VertexColors });

        var wellNameMesh = new THREE.Mesh(textGeo, wellMaterial);
        wellNameMesh.position.x = x;
        wellNameMesh.position.y = y;
        wellNameMesh.position.z = z;
        return wellNameMesh;
    },

    AddWellName: function (x, y, z, h, n) {
        var textGeo = new THREE.TextGeometry(n, {

            size: 10,
            height: 1,
            font: "helvetiker",
            //weight:"bold",//normal
            style: "normal"

        });
        var wellMaterial = new THREE.MeshBasicMaterial({ color: 0xffffff, vertexColors: THREE.VertexColors }); //170 50 100

        var wellNameMesh = new THREE.Mesh(textGeo, wellMaterial);
        wellNameMesh.position.x = x;
        wellNameMesh.position.y = y;
        wellNameMesh.position.z = z;
        return wellNameMesh;
    },
    //加载模型网格点，非坐标点
    LoadModeNetPoint: function (text) {
        var jsonData = JSON.parse(text);
        geometry = new THREE.BufferGeometry();

        var positions = new Float32Array(jsonData.Data.length * 3);
        var colors = new Float32Array(jsonData.Data.length * 3);

        for (var i = 0; i < jsonData.Data.length; i++) {
            positions[i] = jsonData.Data[i][0];
            positions[i + 1] = jsonData.Data[i][1];
            positions[i + 2] = jsonData.Data[i][2];
            var info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[i][3], jsonData.mm[1], jsonData.mm[0]);
            var color;
            if (info) {
                color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
            }

            colors[i] = info["R"] << 16;
            colors[i + 1] = info["G"] << 8;
            colors[i + 2] = info["B"];

        }
        geometry.addAttribute('position', new THREE.BufferAttribute(positions, 3));
        geometry.addAttribute('color', new THREE.BufferAttribute(colors, 3));

        geometry.computeBoundingSphere();

        var material = new THREE.PointsMaterial({ size: 15, vertexColors: THREE.VertexColors });

        var particleSystem = new THREE.Points(geometry, material);

        return particleSystem;
    },

    //使用缓冲方式加载模型
    LoadBufferGeometryMode: function (text) {
        var jsonData = JSON.parse(text);
        geometry = new THREE.BufferGeometry();

        var dataLen = jsonData.Data.length; //模型立方体个数
        //右手坐标系
        //立方体的八个顶点A,B,C,D,E,F,G,H。排列方式：底面ABCD,上面EFGH，从上往下看逆时针排序。
        //*12一个立方体有六个面，每一个面分成二个三角形
        //*3 一个三角形需要三个点来描述
        //*3 一个点需要X,Y,Z三个坐标来描述
        var positions = new Float32Array(dataLen * 12 * 3 * 3);
        var colors = new Float32Array(dataLen * 12 * 3 * 3);
        var ax, ay, az, bx, by, bz, cx, cy, cz, dx, dy, dz, ex, ey, ez, fx, fy, fz, gx, gy, gz, hx, hy, hz;
        var xw, yw, zw, centerx, centery, centerz;
        var index = 0;
        var color = new THREE.Color();
        for (var i = 0; i < dataLen; i++) {
            xw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][0];
            yw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][1];
            zw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][2];
            centerx = jsonData.Data[i][0] - jsonData.ct[0];
            centery = jsonData.Data[i][1] - jsonData.ct[1];
            centerz = jsonData.Data[i][2] - jsonData.ct[2];

            ax = centerx - xw / 2;
            ay = centery - yw / 2;
            az = centerz + zw / 2;

            bx = centerx + xw / 2;
            by = centery - yw / 2;
            bz = centerz + zw / 2;

            cx = centerx + xw / 2;
            cy = centery - yw / 2;
            cz = centerz - zw / 2;

            dx = centerx - xw / 2;
            dy = centery - yw / 2;
            dz = centerz - zw / 2;

            ex = centerx - xw / 2;
            ey = centery + yw / 2;
            ez = centerz + zw / 2;

            fx = centerx + xw / 2;
            fy = centery + yw / 2;
            fz = centerz + zw / 2;

            gx = centerx + xw / 2;
            gy = centery + yw / 2;
            gz = centerz - zw / 2;

            hx = centerx - xw / 2;
            hy = centery + yw / 2;
            hz = centerz - zw / 2;

            positions[i * 108 + 0] = ex;
            positions[i * 108 + 1] = ey;
            positions[i * 108 + 2] = ez;
            positions[i * 108 + 3] = ax;
            positions[i * 108 + 4] = ay;
            positions[i * 108 + 5] = az;
            positions[i * 108 + 6] = fx;
            positions[i * 108 + 7] = fy;
            positions[i * 108 + 8] = fz;

            positions[i * 108 + 9] = ax;
            positions[i * 108 + 10] = ay;
            positions[i * 108 + 11] = az;
            positions[i * 108 + 12] = bx;
            positions[i * 108 + 13] = by;
            positions[i * 108 + 14] = bz;
            positions[i * 108 + 15] = fx;
            positions[i * 108 + 16] = fy;
            positions[i * 108 + 17] = fz;

            positions[i * 108 + 18] = fx;
            positions[i * 108 + 19] = fy;
            positions[i * 108 + 20] = fz;
            positions[i * 108 + 21] = bx;
            positions[i * 108 + 22] = by;
            positions[i * 108 + 23] = bz;
            positions[i * 108 + 24] = gx;
            positions[i * 108 + 25] = gy;
            positions[i * 108 + 26] = gz;

            positions[i * 108 + 27] = bx;
            positions[i * 108 + 28] = by;
            positions[i * 108 + 29] = bz;
            positions[i * 108 + 30] = cx;
            positions[i * 108 + 31] = cy;
            positions[i * 108 + 32] = cz;
            positions[i * 108 + 33] = gx;
            positions[i * 108 + 34] = gy;
            positions[i * 108 + 35] = gz;

            positions[i * 108 + 36] = gx;
            positions[i * 108 + 37] = gy;
            positions[i * 108 + 38] = gz;
            positions[i * 108 + 39] = cx;
            positions[i * 108 + 40] = cy;
            positions[i * 108 + 41] = cz;
            positions[i * 108 + 42] = hx;
            positions[i * 108 + 43] = hy;
            positions[i * 108 + 44] = hz;

            positions[i * 108 + 45] = cx;
            positions[i * 108 + 46] = cy;
            positions[i * 108 + 47] = cz;
            positions[i * 108 + 48] = dx;
            positions[i * 108 + 49] = dy;
            positions[i * 108 + 50] = dz;
            positions[i * 108 + 51] = hx;
            positions[i * 108 + 52] = hy;
            positions[i * 108 + 53] = hz;

            positions[i * 108 + 54] = hx;
            positions[i * 108 + 55] = hy;
            positions[i * 108 + 56] = hz;
            positions[i * 108 + 57] = dx;
            positions[i * 108 + 58] = dy;
            positions[i * 108 + 59] = dz;
            positions[i * 108 + 60] = ex;
            positions[i * 108 + 61] = ey;
            positions[i * 108 + 62] = ez;

            positions[i * 108 + 63] = dx;
            positions[i * 108 + 64] = dy;
            positions[i * 108 + 65] = dz;
            positions[i * 108 + 66] = ax;
            positions[i * 108 + 67] = ay;
            positions[i * 108 + 68] = az;
            positions[i * 108 + 69] = ex;
            positions[i * 108 + 70] = ey;
            positions[i * 108 + 71] = ez;

            positions[i * 108 + 72] = hx;
            positions[i * 108 + 73] = hy;
            positions[i * 108 + 74] = hz;
            positions[i * 108 + 75] = ex;
            positions[i * 108 + 76] = ey;
            positions[i * 108 + 77] = ez;
            positions[i * 108 + 78] = gx;
            positions[i * 108 + 79] = gy;
            positions[i * 108 + 80] = gz;

            positions[i * 108 + 81] = ex;
            positions[i * 108 + 82] = ey;
            positions[i * 108 + 83] = ez;
            positions[i * 108 + 84] = fx;
            positions[i * 108 + 85] = fy;
            positions[i * 108 + 86] = fz;
            positions[i * 108 + 87] = gx;
            positions[i * 108 + 88] = gy;
            positions[i * 108 + 89] = gz;

            positions[i * 108 + 90] = ax;
            positions[i * 108 + 91] = ay;
            positions[i * 108 + 92] = az;
            positions[i * 108 + 93] = dx;
            positions[i * 108 + 94] = dy;
            positions[i * 108 + 95] = dz;
            positions[i * 108 + 96] = bx;
            positions[i * 108 + 97] = by;
            positions[i * 108 + 98] = bz;

            positions[i * 108 + 99] = dx;
            positions[i * 108 + 100] = dy;
            positions[i * 108 + 101] = dz;
            positions[i * 108 + 102] = cx;
            positions[i * 108 + 103] = cy;
            positions[i * 108 + 104] = cz;
            positions[i * 108 + 105] = bx;
            positions[i * 108 + 106] = by;
            positions[i * 108 + 107] = bz;


            var info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[i][3], jsonData.mm[1], jsonData.mm[0]);
            var colorHex;
            if (info) {
                colorHex = (info["R"] << 16) | (info["G"] << 8) | info["B"];
            }
            color.setHex(colorHex);
            for (var j = 0; j < 108; j += 3) {
                colors[i * 108 + j] = color.r;
                colors[i * 108 + j + 1] = color.g;
                colors[i * 108 + j + 2] = color.b;
            }

        }


        geometry.addAttribute('position', new THREE.BufferAttribute(positions, 3));
        geometry.addAttribute('color', new THREE.BufferAttribute(colors, 3));

        geometry.computeBoundingSphere();
        var material = new THREE.MeshBasicMaterial({
            color: 0xffffff, vertexColors: THREE.VertexColors
        });
        var mesh = new THREE.Mesh(geometry, material);

        var group = new THREE.Group();

        var xTW = 0, yTW = 0, zTW = 0; //各个坐标方向总宽度
        for (var i = 0; i < jsonData.xyz.length; i++) {
            xTW += jsonData.xyz[i][0];
            yTW += jsonData.xyz[i][1];
            zTW += jsonData.xyz[i][2];
        }

        //增加油井
        for (var i = 0; i < jsonData.WellPoint.length; i++) {
            //container.add(this.AddWell(jsonData.WellPoint[i].x - jsonData.ct[0], -jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z, jsonData.xyz[1]));
            group.add(this.AddWell(jsonData.WellPoint[i].x - jsonData.ct[0] - jsonData.xyz[0][0] / 2, jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z - jsonData.ct[2], zTW / 2 + 50, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
            group.add(this.AddWellName(jsonData.WellPoint[i].x - jsonData.ct[0], jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z - jsonData.ct[2] + zTW / 2 + 60, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
        }
        group.add(mesh);
        return group;
    },

    ChangeBufferGeometryColor: function (text) {
        var jsonData = JSON.parse(text);
        var colors = geometry.getAttribute('color');
        var colorsArray = colors.array;
        var info, color;
        var color = new THREE.Color();
        for (var i = 0; i < jsonData.Data.length; i++) {
            info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[i][0], jsonData.mm[1], jsonData.mm[0]);
            if (info) {
                var colorHex = (info["R"] << 16) | (info["G"] << 8) | info["B"];
                color.setHex(colorHex);
            }
            else {
                var kkk = 0;
            }

            for (var j = 0; j < 108; j += 3) {
                colorsArray[i * 108 + j + 0] = color.r;
                colorsArray[i * 108 + j + 1] = color.g;
                colorsArray[i * 108 + j + 2] = color.b;

            }
        }
        colors.needsUpdate = true;

    },

    //修改球面径向流颜色
    ChangeBufferGeometryCircleColor: function (text) {
        var jsonData = JSON.parse(text);
        var colors = geometry.getAttribute('color');
        var colorsArray = colors.array;
        var info, color;
        var color = new THREE.Color();
        //颜色计算还有问题
        

        for (var i = 0; i < jsonData.Data.length; i++) {
            info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[i][0], jsonData.mm[1], jsonData.mm[0]);
            if (info) {
                var colorHex = (info["R"] << 16) | (info["G"] << 8) | info["B"];
                color.setHex(colorHex);
            }
            else {
                var kkk = 0;
            }

            for (var j = 0; j < 18; j += 3) {
                if (colorsArray.length >= i * 18 + j + 2) {
                    //防止异常
                    break;
                }
                colorsArray[i * 18 + j + 0] = color.r;
                colorsArray[i * 18 + j + 1] = color.g;
                colorsArray[i * 18 + j + 2] = color.b;

            }
        }
        colors.needsUpdate = true;

    },

    LoadMode: function (text) {

        console.time('MyLoader');

        var vertices = [];

        var jsonData = JSON.parse(text);

        var container = new THREE.Object3D();

        geometry = new THREE.Geometry();

        var THREEColor = new THREE.Color();

        var cubeMaterial = new THREE.MeshBasicMaterial({ vertexColors: THREE.VertexColors });

        for (var i = 0; i < jsonData.Data.length; i++) {

            var xw, yw, zw;
            xw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][0];
            yw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][1];
            zw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][2];
            var info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[i][3], jsonData.mm[1], jsonData.mm[0]);
            var color;
            if (info) {
                color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
            }
            var cubeMesh = addcube(jsonData.Data[i][0] - jsonData.ct[0], jsonData.Data[i][1] - jsonData.ct[1], jsonData.Data[i][2] - jsonData.ct[2], xw, yw, zw, color);
            cubeMesh.updateMatrix();
            geometry.merge(cubeMesh.geometry, cubeMesh.matrix);
        }




        //container.rotateY(-90-10);
        //修改颜色 
        //setFacesVertexColors(geometry, colors);

        function addcube(x, y, z, xw, yw, zw, color) {
            var cubeGeometry = new THREE.BoxGeometry(xw, yw, zw);

            var cube = new THREE.Mesh(cubeGeometry, new THREE.MeshBasicMaterial({ vertexColors: THREE.VertexColors }));
            cube.castShadow = true;

            // position the cube randomly in the scene
            cube.position.x = x;
            cube.position.y = y;
            cube.position.z = z;

            applyVertexColors(cubeGeometry, THREEColor.setHex(color)); //设置面颜色

            // add the cube to the scene
            return cube;
        }
        container.add(new THREE.Mesh(geometry, cubeMaterial));

        var xTW = 0, yTW = 0, zTW = 0; //各个坐标方向总宽度
        for (var i = 0; i < jsonData.xyz.length; i++) {
            xTW += jsonData.xyz[i][0];
            yTW += jsonData.xyz[i][1];
            zTW += jsonData.xyz[i][2];
        }

        //增加油井
        for (var i = 0; i < jsonData.WellPoint.length; i++) {
            //container.add(this.AddWell(jsonData.WellPoint[i].x - jsonData.ct[0], -jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z, jsonData.xyz[1]));
            container.add(this.AddWell(jsonData.WellPoint[i].x - jsonData.ct[0] - jsonData.xyz[0][0] / 2, jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z - jsonData.ct[2], zTW / 2 + 50, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
            container.add(this.AddWellName(jsonData.WellPoint[i].x - jsonData.ct[0], jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z - jsonData.ct[2] + zTW / 2 + 60, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
        }

        console.timeEnd('MyLoader');

        return container;
    },

    //画球面径向流
    LoadBufferGeometryCircleMode: function (text) {
        if (circleGroup) {

        }
        circleGroup = new THREE.Group();
        var arrayColorData; //包括坐标与颜色

        var jsonData = JSON.parse(text);
        //多少圈
        var circle = jsonData.Data[0][0]; //(如30)
        var zCount = jsonData.Data[0][2]; //Z方向个数(如50)
        var split = jsonData.Data[0][1]; //每圈分成多少份(如60)
        //var group = new THREE.Group();
        //return this.DrawPipe(100, 200, 100, 60);
        //var color = new THREE.Color();
        //color.setRGB(255, 0, 0);
        for (var j = 0; j < zCount; j++) {
            for (var i = 0; i < circle; i++) {
                var arrayColor = []; //颜色
                for (var s = 0; s < split; s++) {

                    //jsonData.Data[s * circle + i * split + i * split * circle];
                    var info = CaculateColor(255, 14, 1, 1, 14, 255, jsonData.Data[s * circle + j * split * circle + i][3], jsonData.mm[1], jsonData.mm[0]);
                    var colorHex;
                    if (info) {
                        colorHex = (info["R"] << 16) | (info["G"] << 8) | info["B"];
                    }
                    var color = new THREE.Color();
                    color.setHex(colorHex);
                    arrayColor.push(color);

                }
                this.DrawPipe(10 * (i + 1), 10 * (i + 2), 10, split, arrayColor, ((j + 1) - circle / 2) * 10);
            }

        }
        return circleGroup;
        //DrawPipe()

    },
    //绘制管道,inR:内径,outR:外径,height:管高,radialSegments:上、下面分成多少份,yOffset：坐标方向的偏移量
    //color:颜色数据 长度为radialSegments
    DrawPipe: function (inR, outR, height, radialSegments, color, yOffset) {


        geometry = new THREE.BufferGeometry();


        //true表示是否去掉圆柱上下两个面，1:表示圆柱分成多少层
        //        var inCylinder = new THREE.CylinderGeometry(inR, inR, height, radialSegments, 1, true);
        //        var outCylinder = new THREE.CylinderGeometry(outR, outR, height, radialSegments, 1, true);
        var g1 = new THREE.CylinderGeometry(inR, inR, height, radialSegments, 1, true);
        var g2 = new THREE.CylinderGeometry(outR, outR, height, radialSegments, 1, true);

        //CylinderGeometry中的顶点坐标会多有两个特殊点，一个在中间，一个在最后


        var inHalf = g1.vertices.length / 2;
        g1.vertices.splice(inHalf - 1, 1);
        g1.vertices.pop();

        var outHalf = g2.vertices.length / 2;
        g2.vertices.splice(inHalf - 1, 1);
        g2.vertices.pop();
        //2*3 每封闭一个面需要在最后的坐标点中加入起始点
        var positions = new Float32Array(g1.vertices.length * 18 * 3 + 6);
        var colors = new Float32Array(g1.vertices.length * 18 * 3 + 6);

        //上表面
        for (var i = 0; i < g1.vertices.length / 2; i++) {
            if (i == g1.vertices.length / 2 - 1) {
                //一个三角面
                positions[i * 18 + 0] = g1.vertices[i].x;
                positions[i * 18 + 1] = g1.vertices[i].y + yOffset;
                positions[i * 18 + 2] = g1.vertices[i].z;
                positions[i * 18 + 3] = g2.vertices[i].x;
                positions[i * 18 + 4] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 5] = g2.vertices[i].z;
                positions[i * 18 + 6] = g1.vertices[0].x;
                positions[i * 18 + 7] = g1.vertices[0].y + yOffset;
                positions[i * 18 + 8] = g1.vertices[0].z;
                //另一个三角面组成一个四边形的面
                positions[i * 18 + 9] = g2.vertices[i].x;
                positions[i * 18 + 10] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 11] = g2.vertices[i].z;
                positions[i * 18 + 12] = g2.vertices[0].x;
                positions[i * 18 + 13] = g2.vertices[0].y + yOffset;
                positions[i * 18 + 14] = g2.vertices[0].z;
                positions[i * 18 + 15] = g1.vertices[0].x;
                positions[i * 18 + 16] = g1.vertices[0].y + yOffset;
                positions[i * 18 + 17] = g1.vertices[0].z;


            }
            else {


                //一个三角面
                positions[i * 18 + 0] = g1.vertices[i].x;
                positions[i * 18 + 1] = g1.vertices[i].y + yOffset;
                positions[i * 18 + 2] = g1.vertices[i].z;
                positions[i * 18 + 3] = g2.vertices[i].x;
                positions[i * 18 + 4] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 5] = g2.vertices[i].z;
                positions[i * 18 + 6] = g1.vertices[i + 1].x;
                positions[i * 18 + 7] = g1.vertices[i + 1].y + yOffset;
                positions[i * 18 + 8] = g1.vertices[i + 1].z;
                //另一个三角面组成一个四边形的面
                positions[i * 18 + 9] = g2.vertices[i].x;
                positions[i * 18 + 10] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 11] = g2.vertices[i].z;
                positions[i * 18 + 12] = g2.vertices[i + 1].x;
                positions[i * 18 + 13] = g2.vertices[i + 1].y + yOffset;
                positions[i * 18 + 14] = g2.vertices[i + 1].z;
                positions[i * 18 + 15] = g1.vertices[i + 1].x;
                positions[i * 18 + 16] = g1.vertices[i + 1].y + yOffset;
                positions[i * 18 + 17] = g1.vertices[i + 1].z;

            }
            //增加颜色
            colors[i * 18 + 0] = color[i].r;
            colors[i * 18 + 1] = color[i].g;
            colors[i * 18 + 2] = color[i].b;
            colors[i * 18 + 3] = color[i].r;
            colors[i * 18 + 4] = color[i].g;
            colors[i * 18 + 5] = color[i].b;
            colors[i * 18 + 6] = color[i].r;
            colors[i * 18 + 7] = color[i].g;
            colors[i * 18 + 8] = color[i].b;
            colors[i * 18 + 9] = color[i].r;
            colors[i * 18 + 10] = color[i].g;
            colors[i * 18 + 11] = color[i].b;
            colors[i * 18 + 12] = color[i].r;
            colors[i * 18 + 13] = color[i].g;
            colors[i * 18 + 14] = color[i].b;
            colors[i * 18 + 15] = color[i].r;
            colors[i * 18 + 16] = color[i].g;
            colors[i * 18 + 17] = color[i].b;

        }


        //下表面
        for (var i = g1.vertices.length / 2; i < g1.vertices.length; i++) {
            if (i == g1.vertices.length - 1) {
                //一个三角面
                positions[i * 18 + 0] = g1.vertices[i].x;
                positions[i * 18 + 1] = g1.vertices[i].y + yOffset;
                positions[i * 18 + 2] = g1.vertices[i].z;
                positions[i * 18 + 3] = g2.vertices[i].x;
                positions[i * 18 + 4] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 5] = g2.vertices[i].z;
                positions[i * 18 + 6] = g1.vertices[g1.vertices.length / 2].x;
                positions[i * 18 + 7] = g1.vertices[g1.vertices.length / 2].y + yOffset;
                positions[i * 18 + 8] = g1.vertices[g1.vertices.length / 2].z;
                //另一个三角面组成一个四边形的面
                positions[i * 18 + 9] = g2.vertices[i].x;
                positions[i * 18 + 10] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 11] = g2.vertices[i].z;
                positions[i * 18 + 12] = g2.vertices[g1.vertices.length / 2].x;
                positions[i * 18 + 13] = g2.vertices[g1.vertices.length / 2].y + yOffset;
                positions[i * 18 + 14] = g2.vertices[g1.vertices.length / 2].z;
                positions[i * 18 + 15] = g1.vertices[g1.vertices.length / 2].x;
                positions[i * 18 + 16] = g1.vertices[g1.vertices.length / 2].y + yOffset;
                positions[i * 18 + 17] = g1.vertices[g1.vertices.length / 2].z;


            }
            else {



                //一个三角面
                positions[i * 18 + 0] = g1.vertices[i].x;
                positions[i * 18 + 1] = g1.vertices[i].y + yOffset;
                positions[i * 18 + 2] = g1.vertices[i].z;
                positions[i * 18 + 3] = g2.vertices[i].x;
                positions[i * 18 + 4] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 5] = g2.vertices[i].z;
                positions[i * 18 + 6] = g1.vertices[i + 1].x;
                positions[i * 18 + 7] = g1.vertices[i + 1].y + yOffset;
                positions[i * 18 + 8] = g1.vertices[i + 1].z;
                //另一个三角面组成一个四边形的面
                positions[i * 18 + 9] = g2.vertices[i].x;
                positions[i * 18 + 10] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 11] = g2.vertices[i].z;
                positions[i * 18 + 12] = g2.vertices[i + 1].x;
                positions[i * 18 + 13] = g2.vertices[i + 1].y + yOffset;
                positions[i * 18 + 14] = g2.vertices[i + 1].z;
                positions[i * 18 + 15] = g1.vertices[i + 1].x;
                positions[i * 18 + 16] = g1.vertices[i + 1].y + yOffset
                positions[i * 18 + 17] = g1.vertices[i + 1].z;
            }
            colors[i * 18 + 0] = color[i - g1.vertices.length / 2].r;
            colors[i * 18 + 1] = color[i - g1.vertices.length / 2].g;
            colors[i * 18 + 2] = color[i - g1.vertices.length / 2].b;
            colors[i * 18 + 3] = color[i - g1.vertices.length / 2].r;
            colors[i * 18 + 4] = color[i - g1.vertices.length / 2].g;
            colors[i * 18 + 5] = color[i - g1.vertices.length / 2].b;
            colors[i * 18 + 6] = color[i - g1.vertices.length / 2].r;
            colors[i * 18 + 7] = color[i - g1.vertices.length / 2].g;
            colors[i * 18 + 8] = color[i - g1.vertices.length / 2].b;
            colors[i * 18 + 9] = color[i - g1.vertices.length / 2].r;
            colors[i * 18 + 10] = color[i - g1.vertices.length / 2].g;
            colors[i * 18 + 11] = color[i - g1.vertices.length / 2].b;
            colors[i * 18 + 12] = color[i - g1.vertices.length / 2].r;
            colors[i * 18 + 13] = color[i - g1.vertices.length / 2].g;
            colors[i * 18 + 14] = color[i - g1.vertices.length / 2].b;
            colors[i * 18 + 15] = color[i - g1.vertices.length / 2].r;
            colors[i * 18 + 16] = color[i - g1.vertices.length / 2].g;
            colors[i * 18 + 17] = color[i - g1.vertices.length / 2].b;

        }

        //绘内圆柱面
        for (var i = 0; i < g1.vertices.length / 2; i++) {
            if (i == g1.vertices.length / 2 - 1) {
                positions[i * 18 + 0 + g1.vertices.length * 18] = g1.vertices[i].x;
                positions[i * 18 + 1 + g1.vertices.length * 18] = g1.vertices[i].y + yOffset;
                positions[i * 18 + 2 + g1.vertices.length * 18] = g1.vertices[i].z;
                positions[i * 18 + 3 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].x;
                positions[i * 18 + 4 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 5 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].z;
                positions[i * 18 + 6 + g1.vertices.length * 18] = g1.vertices[0].x;
                positions[i * 18 + 7 + g1.vertices.length * 18] = g1.vertices[0].y + yOffset;
                positions[i * 18 + 8 + g1.vertices.length * 18] = g1.vertices[0].z;



                positions[i * 18 + 9 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].x;
                positions[i * 18 + 10 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 11 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].z;
                positions[i * 18 + 12 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2].x;
                positions[i * 18 + 13 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2].y + yOffset;
                positions[i * 18 + 14 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2].z;
                positions[i * 18 + 15 + g1.vertices.length * 18] = g1.vertices[0].x;
                positions[i * 18 + 16 + g1.vertices.length * 18] = g1.vertices[0].y + yOffset;
                positions[i * 18 + 17 + g1.vertices.length * 18] = g1.vertices[0].z;

            }
            else {
                positions[i * 18 + 0 + g1.vertices.length * 18] = g1.vertices[i].x;
                positions[i * 18 + 1 + g1.vertices.length * 18] = g1.vertices[i].y + yOffset;
                positions[i * 18 + 2 + g1.vertices.length * 18] = g1.vertices[i].z;
                positions[i * 18 + 3 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].x;
                positions[i * 18 + 4 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 5 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].z;
                positions[i * 18 + 6 + g1.vertices.length * 18] = g1.vertices[i + 1].x;
                positions[i * 18 + 7 + g1.vertices.length * 18] = g1.vertices[i + 1].y + yOffset;
                positions[i * 18 + 8 + g1.vertices.length * 18] = g1.vertices[i + 1].z;



                positions[i * 18 + 9 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].x;
                positions[i * 18 + 10 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 11 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i].z;
                positions[i * 18 + 12 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i + 1].x;
                positions[i * 18 + 13 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i + 1].y + yOffset;
                positions[i * 18 + 14 + g1.vertices.length * 18] = g1.vertices[g1.vertices.length / 2 + i + 1].z;
                positions[i * 18 + 15 + g1.vertices.length * 18] = g1.vertices[i + 1].x;
                positions[i * 18 + 16 + g1.vertices.length * 18] = g1.vertices[i + 1].y + yOffset;
                positions[i * 18 + 17 + g1.vertices.length * 18] = g1.vertices[i + 1].z;
            }
            colors[i * 18 + 0 + g1.vertices.length * 18] = color[i].r;
            colors[i * 18 + 1 + g1.vertices.length * 18] = color[i].g;
            colors[i * 18 + 2 + g1.vertices.length * 18] = color[i].b;
            colors[i * 18 + 3 + g1.vertices.length * 18] = color[i].r;
            colors[i * 18 + 4 + g1.vertices.length * 18] = color[i].g;
            colors[i * 18 + 5 + g1.vertices.length * 18] = color[i].b;
            colors[i * 18 + 6 + g1.vertices.length * 18] = color[i].r;
            colors[i * 18 + 7 + g1.vertices.length * 18] = color[i].g;
            colors[i * 18 + 8 + g1.vertices.length * 18] = color[i].b;
            colors[i * 18 + 9 + g1.vertices.length * 18] = color[i].r;
            colors[i * 18 + 10 + g1.vertices.length * 18] = color[i].g;
            colors[i * 18 + 11 + g1.vertices.length * 18] = color[i].b;
            colors[i * 18 + 12 + g1.vertices.length * 18] = color[i].r;
            colors[i * 18 + 13 + g1.vertices.length * 18] = color[i].g;
            colors[i * 18 + 14 + g1.vertices.length * 18] = color[i].b;
            colors[i * 18 + 15 + g1.vertices.length * 18] = color[i].r;
            colors[i * 18 + 16 + g1.vertices.length * 18] = color[i].g;
            colors[i * 18 + 17 + g1.vertices.length * 18] = color[i].b;

        }

        //绘外圆柱面
        for (var i = 0; i < g2.vertices.length / 2; i++) {
            if (i == g2.vertices.length / 2 - 1) {
                positions[i * 18 + 0 + g2.vertices.length * 18 * 2] = g2.vertices[i].x;
                positions[i * 18 + 1 + g2.vertices.length * 18 * 2] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 2 + g2.vertices.length * 18 * 2] = g2.vertices[i].z;
                positions[i * 18 + 3 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].x;
                positions[i * 18 + 4 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 5 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].z;
                positions[i * 18 + 6 + g2.vertices.length * 18 * 2] = g2.vertices[0].x;
                positions[i * 18 + 7 + g2.vertices.length * 18 * 2] = g2.vertices[0].y + yOffset;
                positions[i * 18 + 8 + g2.vertices.length * 18 * 2] = g2.vertices[0].z;



                positions[i * 18 + 9 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].x;
                positions[i * 18 + 10 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 11 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].z;
                positions[i * 18 + 12 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2].x;
                positions[i * 18 + 13 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2].y + yOffset;
                positions[i * 18 + 14 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2].z;
                positions[i * 18 + 15 + g2.vertices.length * 18 * 2] = g2.vertices[0].x;
                positions[i * 18 + 16 + g2.vertices.length * 18 * 2] = g2.vertices[0].y + yOffset;
                positions[i * 18 + 17 + g2.vertices.length * 18 * 2] = g2.vertices[0].z;

            }
            else {
                positions[i * 18 + 0 + g2.vertices.length * 18 * 2] = g2.vertices[i].x;
                positions[i * 18 + 1 + g2.vertices.length * 18 * 2] = g2.vertices[i].y + yOffset;
                positions[i * 18 + 2 + g2.vertices.length * 18 * 2] = g2.vertices[i].z;
                positions[i * 18 + 3 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].x;
                positions[i * 18 + 4 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 5 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].z;
                positions[i * 18 + 6 + g2.vertices.length * 18 * 2] = g2.vertices[i + 1].x;
                positions[i * 18 + 7 + g2.vertices.length * 18 * 2] = g2.vertices[i + 1].y + yOffset;
                positions[i * 18 + 8 + g2.vertices.length * 18 * 2] = g2.vertices[i + 1].z;



                positions[i * 18 + 9 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].x;
                positions[i * 18 + 10 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].y + yOffset;
                positions[i * 18 + 11 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i].z;
                positions[i * 18 + 12 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i + 1].x;
                positions[i * 18 + 13 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i + 1].y + yOffset;
                positions[i * 18 + 14 + g2.vertices.length * 18 * 2] = g2.vertices[g2.vertices.length / 2 + i + 1].z;
                positions[i * 18 + 15 + g2.vertices.length * 18 * 2] = g2.vertices[i + 1].x;
                positions[i * 18 + 16 + g2.vertices.length * 18 * 2] = g2.vertices[i + 1].y + yOffset;
                positions[i * 18 + 17 + g2.vertices.length * 18 * 2] = g2.vertices[i + 1].z;
            }
            colors[i * 18 + 0 + g2.vertices.length * 18 * 2] = color[i].r;
            colors[i * 18 + 1 + g2.vertices.length * 18 * 2] = color[i].g;
            colors[i * 18 + 2 + g2.vertices.length * 18 * 2] = color[i].b;
            colors[i * 18 + 3 + g2.vertices.length * 18 * 2] = color[i].r;
            colors[i * 18 + 4 + g2.vertices.length * 18 * 2] = color[i].g;
            colors[i * 18 + 5 + g2.vertices.length * 18 * 2] = color[i].b;
            colors[i * 18 + 6 + g2.vertices.length * 18 * 2] = color[i].r;
            colors[i * 18 + 7 + g2.vertices.length * 18 * 2] = color[i].g;
            colors[i * 18 + 8 + g2.vertices.length * 18 * 2] = color[i].b;
            colors[i * 18 + 9 + g2.vertices.length * 18 * 2] = color[i].r;
            colors[i * 18 + 10 + g2.vertices.length * 18 * 2] = color[i].g;
            colors[i * 18 + 11 + g2.vertices.length * 18 * 2] = color[i].b;
            colors[i * 18 + 12 + g2.vertices.length * 18 * 2] = color[i].r;
            colors[i * 18 + 13 + g2.vertices.length * 18 * 2] = color[i].g;
            colors[i * 18 + 14 + g2.vertices.length * 18 * 2] = color[i].b;
            colors[i * 18 + 15 + g2.vertices.length * 18 * 2] = color[i].r;
            colors[i * 18 + 16 + g2.vertices.length * 18 * 2] = color[i].g;
            colors[i * 18 + 17 + g2.vertices.length * 18 * 2] = color[i].b;

        }


        geometry.addAttribute('position', new THREE.BufferAttribute(positions, 3));
        geometry.addAttribute('color', new THREE.BufferAttribute(colors, 3));

        geometry.computeBoundingSphere();

        var material = new THREE.MeshBasicMaterial({
            color: 0xffffff, vertexColors: THREE.VertexColors, side: THREE.DoubleSide
        });

        var mesh = new THREE.Mesh(geometry, material);

        circleGroup.add(mesh);

    }

};

//
function LoadData() {

    
    
};

 
