import { useState } from 'react';
import heroImg from './assets/hero.png';
import reactLogo from './assets/react.svg';
import viteLogo from './assets/vite.svg';
import './App.css';

function App() {
  const [name, setName] = useState('');

  const handleGo = () => {
    window.location.href = 'https://topitcollege.ru/';
  };

  return (
    <div>
      <div className="button">
        <button id="goBtn" onClick={handleGo}>
          Кнопка
        </button>
      </div>

      <input
        type="text"
        placeholder="Введите текст"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <h1>Hello, {name}</h1>
    </div>
  );
}

export default App;
