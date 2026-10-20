console.log("подключаем модуль hello.js");
const {sum, hello} = require("./hello");

var result = sum(45, 11);
console.log(result);
hello("IT TOP College");
