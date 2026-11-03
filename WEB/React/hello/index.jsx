import React from "react";
import ReactDOM, { createRoot } from "react-dom/client";

const student1 = {
    id: 12345,
    group: "SoftDev",
    firstName: "Ivan",
    secondName: "Ivanov",
    isAccept: true,
    print: function() {
        return `${this.id}\t${this.firstName} ${this.secondName}`;
    }
};
const students = [student1];

const head_elem = createRoot(document.getElementById("header"));

function timer() {
    head_elem.render(
        <h1>{new Date().toLocaleTimeString()}</h1>
    )
}
setInterval(timer, 1000);

createRoot(document.getElementById("application")).render(
    <div>
        <h1>{new Date().toLocaleTimeString()}</h1>
    </div>
);