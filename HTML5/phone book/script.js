let phoneCounter;

function addClick() {
    if(typeof phoneCounter == 'undefined')
        phoneCounter = 1;
    phoneCounter++;
    var f = document.forms[0];
    var b = document.createElement('br');
    f.appendChild(b);
    
    var t = document.createTextNode('Телефон:');
    f.appendChild(t);
    
    var phoneInput = document.createElement('input');
    phoneInput.type = 'text';
    phoneInput.name = 'phone_' + phoneCounter;
    phoneInput.placeholder = "Введите номер телефона";
    f.appendChild(phoneInput);
    
    var t2 = document.createTextNode(' тип: ');
    f.appendChild(t2);
    
    var selector = f.elements['type'];
    var newSelector = selector.cloneNode(true);
    console.log(newSelector);
    newSelector.name = 'type_' + phoneCounter;
    f.appendChild(newSelector);
    
    var t3 = document.createTextNode(' По умолчанию: ');
    f.appendChild(t3);
    
    var priorityRadio = document.createElement('input');
    priorityRadio.type = 'radio';
    priorityRadio.name = 'priority';
    priorityRadio.value = phoneCounter;
    f.appendChild(priorityRadio);
}

function showElements() {
    var e = document.forms[0].elements;
    p = document.getElementById("out");
    p.innerHTML = "";
    for (var i = 0; i < e.length; i++) {
        p.innerHTML += e[i].tagName + " - " + e[i].name + " - " + e[i].value + "<br>";
    }
}
