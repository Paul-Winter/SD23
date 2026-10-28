import React from "react";
import ReactDOM from "react-dom/client";

const rootElement = document.getElementById("application");
const root = ReactDOM.createRoot(rootElement);
const element = <h1>Hello, World!</h1>
const element2 = <h2>2 + 2 = {2 + 2}</h2>
//const element = React.createElement("h1", null, "Hello, World!");

root.render(element2);
