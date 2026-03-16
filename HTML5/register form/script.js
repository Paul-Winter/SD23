function checkForm() {
    var n1 = document.getElementById("firstName").value;
    var t1 = /^[A-Z][a-z]*$/;
    if(!t1.test(n1)) {
        alert("Неверное имя!");
        return false;
    }
    return true;
}
