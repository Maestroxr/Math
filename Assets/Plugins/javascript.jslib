mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(Pointer_stringify(str));
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },
  
  GetParameters: function () {
	  alert('kaka1');
	  var raw = window.location.search.substring(1).split("&");
	  var result = [];
	  for (var i in raw) {
		  var pair = raw[i].split("=");
		  result[pair[0]] = pair[1]];
	  }
	  alert('kaka2');
	  alert(result);
	  window.parent.document.getElementById(result[inputId]).value("fuck me this works."); 
	  return result;
  }
  
  

});