const server = require("http");
const fs = require("fs");

server.createServer(function(request, response){
    response.setHeader("user_header", "__________USER_HEADER__________");
    console.log("URL: ", request.url);
    console.log("METHOD: ", request.method);

    const data = request.headers;
    
    response.write("\nURL: ");
    response.write(request.url);
    response.write("\nMETHOD: ");
    response.write(request.method);
    response.write("\nHEADERS:\n");
    for(var header in data) {
        response.write(header);
        response.write(" - ");
        response.write(data[header]);
        response.write("\n");
    };
    response.end("__________END_RESPONSE__________");
}).listen(3000, "127.0.0.1", function(){
    console.log("__________server_listen_port_3000__________");
});