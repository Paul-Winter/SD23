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

createRoot(document.getElementById("application")).render(
    <ul>
    {students.map((stud, index) => (
        <li key={index}>
            <p id={stud.id}>
                <h1>студент: {stud.print()}</h1>
                <h2>группа: {stud.group}</h2>
                {stud.isAccept ? <h3>допущен</h3> : <h3>не допущен</h3>}
            </p>
        </li>
    ))}
    </ul>
);
