const server = require("http");
const fs = require("fs");
const { forEach } = require("lodash");

server.createServer(function(request, response){
    console.log("URL: ", request.url);
    console.log("METHOD: ", request.method);
    // console.log(request.headers);
    const data = request.headers;
    for(var header in data) {
        fs.appendFileSync("request_info.txt", header);
        fs.appendFileSync("request_info.txt", " - ");
        fs.appendFileSync("request_info.txt", data[header]);
        fs.appendFileSync("request_info.txt", "\n");
    };
    console.log("__________записываем_в_файл_'request_info.txt'__________");
    response.end();
}).listen(3000, "127.0.0.1", function(){
    console.log("__________server_listen_port_3000___________");
});