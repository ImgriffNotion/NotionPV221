import logo from './logo.png';
import './App.css';

function App() {
  return (
    <div className="App">

      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Links <code>to my Instagram</code> and like my Photos
        </p>
        <a
          className="App-link"
          href="https://www.instagram.com/le_al_dente/"
          target="_blank"
          rel="noopener noreferrer"
        >
          @le_al_dente
        </a>
      </header>
    </div>
  );
}

export default App;
