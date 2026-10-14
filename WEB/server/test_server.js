const server = require("http");

server.createServer(function(request, response){
    response.end("Hello, World!");
}).listen(3000, "127.0.0.1", function(){
    console.log("server listen port 3000:");
});