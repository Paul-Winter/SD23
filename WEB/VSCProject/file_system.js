const fileSys = require("fs");
const { functionsIn } = require("lodash");

fileSys.readFile("hello.txt", function(error, data){
    if(error) {
        return console.log(error);
    }    
    console.log(data.toString());
    console.log("async");
});
console.log("читаем из файла асинхронно:");

const data = fileSys.readFileSync("hello.txt");
console.log("читаем из файла синхронно:");
console.log(data.toString());
