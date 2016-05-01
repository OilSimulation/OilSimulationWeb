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
                onLoad(scope.LoadMode(text));
                //parent.HideLoading();
                parent.postMessage("HideLoading()", "*");
            }
            else
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

    ChangeNetPointColor:function (text) {
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
				    colorsArray[i + 1] =  colors[1];
				    colorsArray[i + 2] =  colors[2];

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
            container.add(this.AddWell(jsonData.WellPoint[i].x - jsonData.ct[0], jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z - jsonData.ct[2], zTW / 2 + 5, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
            container.add(this.AddWellName(jsonData.WellPoint[i].x - jsonData.ct[0], jsonData.WellPoint[i].y - jsonData.ct[1], jsonData.WellPoint[i].z - jsonData.ct[2] + zTW / 2 + 10, jsonData.xyz[0][2], jsonData.WellPoint[i].name));
        }

        console.timeEnd('MyLoader');

        return container;
    }

};

//
function LoadData() {

    
    
};

 
