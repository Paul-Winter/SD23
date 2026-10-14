const fileSys = require("fs");

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

fileSys.writeFile("test.txt", data, function(error){
    if(error){
        return console.log(error);
    }
    console.log("записываем в файл асинхронно:");
});

console.log("свойства файла 'hello,txt':")
fileSys.stat("hello.txt", function(error, data){
    if(error){
        return console.log(error);
    }
    if(data.isFile()){
        console.log(data);
    }
    if(!data.isDirectory()){
        console.log("is not directory")
    }
});
// console.log("удаление файла 'test.txt'");
// fileSys.unlinkSync("test.txt");