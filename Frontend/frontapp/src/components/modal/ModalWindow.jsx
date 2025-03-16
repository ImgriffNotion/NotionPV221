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
    const [currentDate, setCurrentDate] = useState(new Date());
    const [imageFile, setImageFile] = useState();
    const formattedDate = currentDate.toISOString().split('T')[0]; // Формат: год-месяц-день
    console.log(formattedDate);


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
        iconFile: 'icon_files',
        iconPlus: 'iconPlus3'

    }
    Object.entries(staticImage).forEach(([key, value]) => {
        staticImage[key] = path + value + '.svg'
    });

    /** Изменение имени файла*/
    const handleChange = (data) => {
        setInputText(data);
        dispatch(setModalText(data));//обновляем текст в глобальном состоянии
    }

    const handleSave = () => {
        // Обновляем данные в Redux
        dispatch(updateModalData({ text: inputText, id: id }));
        dispatch(closeModal()); // Закрываем модалку
    };

    /**Реакция на кнопку */
    const close = () => {

        dispatch(closeModal()); // закрываем модалку
    }



    /**Появление автоскрола в textarea  */
    const onChanges = (e) => {

        e.target.style.height = 'auto';                           // сбрасываем высоту
        const newHeight = Math.min(e.target.scrollHeight, 300);   // scrollHeight — необходимая высота для контента, ограничиваем max-height (300px)
        e.target.style.height = newHeight + 'px';
        console.log(e.target.value);
    }

    /**Trigger for simulate input:file - click */
    const onDownloadFile = () => {
        document.getElementById('fileInput').click();
    }
    const onFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            alert("yes")
        }
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
                <div className='left-modal-content-main'>
                    <ul>
                        <li>
                            <div className='gallery_card_property'>
                                <p><img src={staticImage.iconCalendar} alt="date" />Date</p><p>{formattedDate}</p>
                            </div>
                        </li>
                        <li>
                            <div className='gallery_card_property' onClick={onDownloadFile}>
                                <p><img src={staticImage.iconFile} alt="file" />Files</p><p>Empty</p>
                            </div>
                        </li>
                    </ul>

                </div>
                <div className='right-modal-content-main'>
                    <textarea className='scrolable_up' onChange={onChanges} id="#describe_text" placeholder='Add a description...' defaultValue={null} />
                </div>
            </div>
            <hr className='separator' />
            <input type="file"
                name=""
                id='fileInput'
                style={{ display: 'none' }}
                onChange={onFileChange}
                accept='image/*'
            />
        </div>
    )
}



