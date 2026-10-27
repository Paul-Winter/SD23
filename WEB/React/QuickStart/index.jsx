import React from "react";
import ReactDOM from "react-dom/client";

//1
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

//2

const user = {
  name: 'Hedy Lamarr',
  imageUrl: 'https://react.dev/images/docs/scientists/yXOvdOSs.jpg',
  imageSize: 90,
};

function AboutPage() {
  return (
    <div>
      <h1>About</h1>
      <p>Hello there.<br />How do you do?</p>
      {user.name}
      <img className="avatar" src={user.imageUrl} />
    </div>
  );
}

const rootElement = document.getElementById("application");
const root = ReactDOM.createRoot(rootElement);
const element = React.createElement(AboutPage);

root.render(element);