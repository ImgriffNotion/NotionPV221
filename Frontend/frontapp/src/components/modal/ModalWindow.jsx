import React from 'react';
import { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { closeModal, updateModalData, setModalText } from '../../reducers/modalSlice';
import './ModalWindow.css';


export default function ModalWindow({ modalData }) {
    const dispatch = useDispatch();
    const { text, id } = modalData;
    const modalState = useSelector((state) => state.modal);
    const [inputText, setInputText] = useState(modalState.modalData?.text || '');

    const path = 'img/mainPage/icons/'
    const staticImage = {
        iconEmptyPage: 'iconEmptyPage',
        iconList: 'iconList',
        iconBoard: 'iconBoard',
        iconTemplates: 'iconTemplates',
        iconTable: 'iconTable',
        iconGallery: 'iconGallery',
        iconCalendar: 'iconCalendar',
        iconClose: 'iconClose',

    }
    Object.entries(staticImage).forEach(([key, value]) => {
        staticImage[key] = path + value + '.svg'
    });




    const handleChange = (data) => {
        setInputText(data);
        dispatch(setModalText(data));//обновляем текст в глобальном состоянии
    }

    const handleSave = () => {
        // Обновляем данные в Redux
        dispatch(updateModalData({ text: inputText, id: id }));
        dispatch(closeModal()); // Закрываем модалку
    };

    const close = () => {

        dispatch(closeModal()); // закрываем модалку
    }



    return (
        <div className="modal">
            <div className="modal-content">
                <input
                    type='text'
                    value={inputText}
                    onChange={(e) => handleChange(e.target.value)} // Обрабатываем изменения текста
                    placeholder="Card_name"
                /> <button onClick={close}><img src={staticImage.iconClose} alt="" /></button>
            </div>
            <div className='modal-content-main'>
                <div></div>
                <div></div>
            </div>
        </div>
    )
}



