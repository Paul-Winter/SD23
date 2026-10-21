class Student{
    name : string;
    group: string;
    constructor(_name:string, _group:string){
        this.name = _name;
        this.group = _group;
    }
}
const header = this.document.getElementById("header");
const hello_world : Student = new Student("Hello", "World");
if (header !== null) {
    header.innerHTML = hello_world.name + ", " + hello_world.group + "!";
}