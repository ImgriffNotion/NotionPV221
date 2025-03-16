import './App.css';
import { Login } from './components/loginpage/Login';
import { Landing } from './components/landing/Landing.jsx';
import { NotFound } from './components/notFound/NotFound.jsx';
import React from 'react';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import { MainPage } from './components/mainPage/MainPage.jsx';
import { useDispatch, useSelector } from 'react-redux';
import { openModal, closeModal } from './reducers/modalSlice.js';
import ModalWindow from './components/modal/ModalWindow.jsx';


function App() {

  const dispatch = useDispatch();
  const isModalOpen = useSelector((state) => state.modal.isModalOpen); // получаем состояние модалки
  const modalData = useSelector((state) => state.modal.modalData); //данные для модалки

  const openCustomModal = () => {
    dispatch(openModal({ text: "Hello from App" })); //передаем данные в модалку
  };




  return (
    <div className="App">
      <button onClick={openCustomModal}>Открыть модальное окно</button>
      <Router>
        <Routes>
          <Route path='/' element={<MainPage />} />
          <Route path='/landing' element={<Landing />} />
          <Route path='/login' element={<Login />} />
          <Route path='*' element={<NotFound />} />
        </Routes>
      </Router>
      {isModalOpen && <ModalWindow modalData={modalData} />}

    </div>
  );
}

export default App;