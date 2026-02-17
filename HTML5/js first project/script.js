var num;
let str;
const THIS_IS_CONSTANT_STRING_ANSWER = "dlfkjdslkfj";

function cube(x) {
    return x*x*x;
}
// Антонов  - 9
// Золин    - 5
// Красицкий- 7
// Кубанов  - 9
// Лушников - 8
// Math.round(Math.random())

function parity(x) {
    let parity;
    if(x % 2 == 0)
        parity = "чётное";
    else
        parity = "нечётное";
}
//var num;
function buttonClicked() {
    //alert("Hello, World!");
    //confirm(typeof(num));
    //if(num % 2 == 0)
    //    alert("Чётное");
    //else
    //    alert("Нечётное");
    do {
        num = prompt("Введите число: ");
        switch(num){
            case "-0": return;
            case "0": str = "Ноль"; break;
            case "13": str = "Чёртова дюжина"; break;
            case "42": str = "Ответ на вопрос о всём сущем и вообще"; break;
            default:  str = (num % 2 == 0) ? "Чётное" : "Нечётное"; break;   
        }
        alert(str);        
    } while(num != "0");
    let i=0;
    for(j=10; i!=j; j--) {
        i++;
        alert(i + " " + j);
    }
    alert(j);

    //numButtonClicks = numButtonClicks + 1;
    //document.getElementById("mainDiv").textContent =
    //    "Button Clicked times: " + numButtonClicks;
}
