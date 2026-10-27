import React from "react";
import ReactDOM from "react-dom/client";

function MyButton() {
    return (
        <button>I'm a button</button>
    );
}
export default function MyApp() {
  return (
    <div>
      <h1>Welcome to my app</h1>
      <MyButton />
    </div>
  );
}

const rootElement = document.getElementById("application");
const root = ReactDOM.createRoot(rootElement);
const element = React.createElement(MyApp);

root.render(element);