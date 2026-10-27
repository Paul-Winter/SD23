import React from "react";
import ReactDOM from "react-dom/client";
import { useState } from 'react';
//1
// function MyButton() {
//     return (
//         <button>I'm a button</button>
//     );
// }
// export default function MyApp() {
//   return (
//     <div>
//       <h1>Welcome to my app</h1>
//       <MyButton />
//     </div>
//   );
// }

//2

// const user = {
//   name: 'Hedy Lamarr',
//   imageUrl: 'https://react.dev/images/docs/scientists/yXOvdOSs.jpg',
//   imageSize: 90,
// };

// function AboutPage() {
//   return (
//     <div>
//       <h1>About</h1>
//       <p>Hello there.<br />How do you do?</p>
//       {user.name}
//       <img className="avatar" src={user.imageUrl} />
//     </div>
//   );
// }

// export default function Profile() {
//   return (
//     <>
//       <h1>{user.name}</h1>
//       <img
//         className="avatar"
//         src={user.imageUrl}
//         alt={'Photo of ' + user.name}
//         style={{
//           width: user.imageSize,
//           height: user.imageSize
//         }}
//       />
//     </>
//   );
// }

// function Content() {
//     let content;
//     if (isLoggedIn) {
//     content = <AdminPanel />;
//     } else {
//     content = <LoginForm />;
//     }
//     return (
//     <div>
//         {content}
//     </div>
//     );
// }

// const products = [
//   { title: 'Cabbage', isFruit: false, id: 1 },
//   { title: 'Garlic', isFruit: false, id: 2 },
//   { title: 'Apple', isFruit: true, id: 3 },
// ];

// export default function ShoppingList() {
//   const listItems = products.map(product =>
//     <li
//       key={product.id}
//       style={{
//         color: product.isFruit ? 'magenta' : 'darkgreen'
//       }}
//     >
//       {product.title}
//     </li>
//   );

//   return (
//     <ul>{listItems}</ul>
//   );
// }

// export default function MyButton() {
//     function handleClick() {
//         alert('You clicked me!');
//     }

//   return (
//     <button onClick={handleClick}>
//       Click me
//     </button>
//   );
// }

export default function MyApp() {
  const [count, setCount] = useState(0);

  function handleClick() {
    setCount(count + 1);
  }

  return (
    <div>
      <h1>Counters that update together</h1>
      <MyButton count={count} onClick={handleClick} />
      {/* <MyButton count={count} onClick={handleClick} /> */}
    </div>
  );
}

function MyButton({ count, onClick }) {
  return (
    <button onClick={onClick}>
      Clicked {count} times
    </button>
  );
}


const rootElement = document.getElementById("application");
const root = ReactDOM.createRoot(rootElement);
const element = React.createElement(MyApp);

root.render(element);