import React from "react";
import ReactDOM from "react-dom/client";

const rootElement = document.getElementById("application");
const root = ReactDOM.createRoot(rootElement);
const element = React.createElement("h1", null, "Hello, World!");

root.render(element);
