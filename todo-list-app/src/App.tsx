import { useState } from 'react';
import styles from './App.module.css';
import TodoConteiner from './components/todo-conteiner/TodoConteiner';
import Auth from './components/auth/Auth';

function App() {
  const [isAuthorized, setIsAuthorized] = useState(false);

  return (
    <div className={styles.app}>
      <header className={styles.header}>
        Список задач
      </header>
      {!isAuthorized && <Auth setIsAuthorized ={setIsAuthorized} />}
      {isAuthorized && <TodoConteiner setIsAuthorized ={setIsAuthorized}/>}
    </div>
  );
}

export default App;
