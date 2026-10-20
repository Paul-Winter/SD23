const server = require("http");

server.createServer(function(request, response){
    response.setHeader("Content-Type", "text/html; charset=utf-8;");
    if(request.url === "/"){
        response.statusCode = 302;
        response.setHeader("Location", "/redirect");
        response.write("<h3>ВРЕМЕННАЯ ПЕРЕАДРЕСАЦИЯ</h3>");
    }
    if(request.url === "/redirect"){
        response.statusCode = 301;
        response.write("<h3>ПОСТОЯННАЯ ПЕРЕАДРЕСАЦИЯ</h3>");
    }
    else {
        response.statusCode = 404;
        response.write("__________NOT_FOUND__________");
    }
    response.end("__________END_RESPONSE__________");
}).listen(3000, "127.0.0.1", function(){
    console.log("__________server_listen_port_3000__________");
});