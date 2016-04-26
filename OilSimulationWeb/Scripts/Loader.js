/**
 * @author mrdoob / http://mrdoob.com/
 */

THREE.MyLoader = function ( manager ) {

	this.manager = ( manager !== undefined ) ? manager : THREE.DefaultLoadingManager;

};

THREE.MyLoader.prototype = {

	constructor: THREE.MyLoader,

	load: function ( url, onLoad, onProgress, onError ) {

		var scope = this;

		var loader = new THREE.XHRLoader( scope.manager );
		loader.setCrossOrigin( this.crossOrigin );
		loader.load( url, function ( text ) {

			onLoad( scope.parse( text ) );

		}, onProgress, onError );

	},

	parse: function ( text ) {

		console.time( 'MyLoader' );

		var vertices = [];
		
		JSON.parse( text );

		var lines = text.split( '\n' );
		var line = lines[0].trim();
		var re = /([\d]+)(\,)([\d]+)(\,)([\d]+)(\,)([\d|\.|\+|\-|e|E]+)/g;
		var knode = 0;
		while ( tempR = re.exec(line))
		{
			knode ++;
			//console.log(knode, "x:", tempR[1], ",y:",tempR[3], ",z:", tempR[5]);
			vertices.push(
			              parseInt( tempR[1] ),
			              parseInt( tempR[3] ),
			              parseInt( tempR[5] )
			             );
		}
		console.log("knode:", knode, ", first point:", vertices[0],vertices[1],vertices[2]);
		
		var container = new THREE.Object3D();
 
    var geometry = new THREE.Geometry();
    var cubeMaterial = new THREE.MeshNormalMaterial({color: 0x0f00f1, transparent: true, opacity: 0.5});
    var iLeive = 3;
    alert(vertices.length);
    
		for ( var i = 0, l=vertices.length/3; i<l; i++ ){
        var cubeMesh = addcube(vertices[i*3+0]-400, vertices[i*3+1]-500, vertices[i*3+2]-3010,10);
        cubeMesh.updateMatrix();
        geometry.merge(cubeMesh.geometry, cubeMesh.matrix);
		}
    function addcube(x, y, z,zw) {
      var cubeSize = 10.0;
      var cubeGeometry = new THREE.BoxGeometry(cubeSize, cubeSize, zw);

      var cube = new THREE.Mesh(cubeGeometry, cubeMaterial);
      cube.castShadow = true;

      // position the cube randomly in the scene
      cube.position.x = x;
      cube.position.y = y;
      cube.position.z = z;

      // add the cube to the scene
      return cube;
    }
    container.add(new THREE.Mesh(geometry, cubeMaterial));

		console.timeEnd( 'MyLoader' );

		return container;
	}

};
