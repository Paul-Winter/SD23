"use strict";
class Student {
    name;
    group;
    constructor(_name, _group) {
        this.name = _name;
        this.group = _group;
    }
}
const header = this.document.getElementById("header");
const hello_world = new Student("Hello", "World");
if (header !== null) {
    header.innerHTML = hello_world.name + ", " + hello_world.group + "!";
}
