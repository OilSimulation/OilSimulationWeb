/**
 * @author mrdoob / http://mrdoob.com/
 */

THREE.MyLoader = function ( manager ) {

	this.manager = ( manager !== undefined ) ? manager : THREE.DefaultLoadingManager;

};
var modelJsonData;
THREE.MyLoader.prototype = {

    constructor: THREE.MyLoader,

    load: function (url, pData, onLoad, onProgress, onError) {

        var scope = this;

        var loader = new THREE.XHRLoader(scope.manager);
        loader.setCrossOrigin(this.crossOrigin);
        loader.load(url, pData, function (text) {
            if (geometry == undefined) {
                //onLoad(scope.LoadBufferGeometryMode(text)); 
                onLoad(scope.LoadMode(text));
                //parent.HideLoading();
                parent.postMessage("HideLoading()", "*");
            }
            else
                //onLoad(scope.ChangeBufferGeometryColor(text));
                onLoad(scope.ChangeColor(text));
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



    ChangeColor: function (text) {
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
            if (i == 29999) {
                var xx = 0;
            }
            index++;
            xw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][0];
            yw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][1];
            zw = jsonData.xyz[parseInt(i / (jsonData.Data.length / jsonData.lev))][2];
            centerx = jsonData.Data[i][0] - jsonData.ct[0];
            centery = jsonData.Data[i][1] - jsonData.ct[1];
            centerz = jsonData.Data[i][2] - jsonData.ct[2];

            ax = centerx - xw;
            ay = centery - yw;
            az = centerz + zw;

            bx = centerx + xw;
            by = centery - yw;
            bz = centerz + zw;

            cx = centerx + xw;
            cy = centery - yw;
            cz = centerz - zw;

            dx = centerx - xw;
            dy = centery - yw;
            dz = centerz - zw;

            ex = centerx - xw;
            ey = centery + yw;
            ez = centerz + zw;

            fx = centerx + xw;
            fy = centery + yw;
            fz = centerz + zw;

            gx = centerx + xw;
            gy = centery + yw;
            gz = centerz - zw;

            hx = centerx - xw;
            hy = centery + yw;
            hz = centerz - zw;

            positions[i * 108 + 0] = ax;
            positions[i * 108 + 1] = ay;
            positions[i * 108 + 2] = az;
            positions[i * 108 + 3] = ex;
            positions[i * 108 + 4] = ey;
            positions[i * 108 + 5] = ez;
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

            positions[i * 108 + 18] = ax;
            positions[i * 108 + 19] = ay;
            positions[i * 108 + 20] = az;
            positions[i * 108 + 21] = bx;
            positions[i * 108 + 22] = by;
            positions[i * 108 + 23] = bz;
            positions[i * 108 + 24] = cx;
            positions[i * 108 + 25] = cy;
            positions[i * 108 + 26] = cz;

            positions[i * 108 + 27] = ax;
            positions[i * 108 + 28] = ay;
            positions[i * 108 + 29] = az;
            positions[i * 108 + 30] = dx;
            positions[i * 108 + 31] = dy;
            positions[i * 108 + 32] = dz;
            positions[i * 108 + 33] = cx;
            positions[i * 108 + 34] = cy;
            positions[i * 108 + 35] = cz;

            positions[i * 108 + 36] = ax;
            positions[i * 108 + 37] = ay;
            positions[i * 108 + 38] = az;
            positions[i * 108 + 39] = dx;
            positions[i * 108 + 40] = dy;
            positions[i * 108 + 41] = dz;
            positions[i * 108 + 42] = hx;
            positions[i * 108 + 43] = hy;
            positions[i * 108 + 44] = hz;

            positions[i * 108 + 45] = ax;
            positions[i * 108 + 46] = ay;
            positions[i * 108 + 47] = az;
            positions[i * 108 + 48] = ex;
            positions[i * 108 + 49] = ey;
            positions[i * 108 + 50] = ez;
            positions[i * 108 + 51] = hx;
            positions[i * 108 + 52] = hy;
            positions[i * 108 + 53] = hz;

            positions[i * 108 + 54] = gx;
            positions[i * 108 + 55] = gy;
            positions[i * 108 + 56] = gz;
            positions[i * 108 + 57] = cx;
            positions[i * 108 + 58] = cy;
            positions[i * 108 + 59] = cz;
            positions[i * 108 + 60] = bx;
            positions[i * 108 + 61] = by;
            positions[i * 108 + 62] = bz;

            positions[i * 108 + 63] = gx;
            positions[i * 108 + 64] = gy;
            positions[i * 108 + 65] = gz;
            positions[i * 108 + 66] = fx;
            positions[i * 108 + 67] = fy;
            positions[i * 108 + 68] = fz;
            positions[i * 108 + 69] = bx;
            positions[i * 108 + 70] = by;
            positions[i * 108 + 71] = bz;

            positions[i * 108 + 72] = gx;
            positions[i * 108 + 73] = gy;
            positions[i * 108 + 74] = gz;
            positions[i * 108 + 75] = cx;
            positions[i * 108 + 76] = cy;
            positions[i * 108 + 77] = cz;
            positions[i * 108 + 78] = dx;
            positions[i * 108 + 79] = dy;
            positions[i * 108 + 80] = dz;

            positions[i * 108 + 81] = gx;
            positions[i * 108 + 82] = gy;
            positions[i * 108 + 83] = gz;
            positions[i * 108 + 84] = hx;
            positions[i * 108 + 85] = hy;
            positions[i * 108 + 86] = hz;
            positions[i * 108 + 87] = dx;
            positions[i * 108 + 88] = dy;
            positions[i * 108 + 89] = dz;

            positions[i * 108 + 90] = gx;
            positions[i * 108 + 91] = gy;
            positions[i * 108 + 92] = gz;
            positions[i * 108 + 93] = hx;
            positions[i * 108 + 94] = hy;
            positions[i * 108 + 95] = hz;
            positions[i * 108 + 96] = ex;
            positions[i * 108 + 97] = ey;
            positions[i * 108 + 98] = ez;

            positions[i * 108 + 99] = gx;
            positions[i * 108 + 100] = gy;
            positions[i * 108 + 101] = gz;
            positions[i * 108 + 102] = fx;
            positions[i * 108 + 103] = fy;
            positions[i * 108 + 104] = fz;
            positions[i * 108 + 105] = ex;
            positions[i * 108 + 106] = ey;
            positions[i * 108 + 107] = ez;


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
            color: 0xffff00, vertexColors: THREE.VertexColors
        });
        var mesh = new THREE.Mesh(geometry, material);

        return mesh;
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
            for (var j = 0; j < 108; j += 3) {
                colorsArray[i * 108 + j + 0] = color.r;
                colorsArray[i * 108 + j + 1] = color.g;
                colorsArray[i * 108 + j + 2] = color.b;

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
            container.add(this.AddWell(jsonData.WellPoint[i].x - jsonData.ct[0] - jsonData.xyz[0][0] / 2, jsonData.WellPoint[i].y - jsonData.ct[1] - jsonData.xyz[0][1] / 2, jsonData.WellPoint[i].z - jsonData.ct[2], zTW / 2 + 5, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
            container.add(this.AddWellName(jsonData.WellPoint[i].x - jsonData.ct[0], jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z - jsonData.ct[2] + zTW / 2 + 10, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
        }

        console.timeEnd('MyLoader');

        return container;
    }

};

//
function LoadData() {

    
    
};

 
