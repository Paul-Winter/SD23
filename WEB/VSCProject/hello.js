// module.exports = {hello:this.hello};

console.log('подключаем библиотеку lodash');
const lodash = require("lodash")

const first = [1, 2, 3, 4, 5, 6];
const second = [4, 5, 6, 7, 8, 9];

console.log('объединение массивов:');
console.log(lodash.union(first, second));

console.log('пересечение массивов:');
console.log(lodash.intersection(first, second));

exports.sum = function sum(x, y)
{
    return x + y;
}
exports.hello = function hello(x)
{
    console.log('hello, ' + x + '!');
}