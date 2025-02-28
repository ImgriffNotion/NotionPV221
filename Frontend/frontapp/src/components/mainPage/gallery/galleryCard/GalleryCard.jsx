import './GalleryCard.css';
import { useDispatch, useSelector } from 'react-redux';
import { openModal } from '../../../../reducers/modalSlice'; //импортируем OpenModal
import { useState, useEffect } from 'react';

export function GalleryCard({ id, staticImage }) {

    const dispatch = useDispatch(); // для отправки действия в редакс
    const [imageName, setImageName] = useState('Imf');
    const modalData = useSelector((state) => state.modal.modalData); // получает данные модального окна

    useEffect(() => {
        if (modalData && modalData.text && modalData.id == id) {
            console.log(id);

            setImageName(modalData.text)
        }
    }, [modalData]);


    const handleCardClick = () => {
        dispatch(openModal({ text: imageName, id: id })); //передаем данные в модалку
    }

    return (

        <>
            <div onClick={handleCardClick}>
                <img src={staticImage.imageSimple} alt="asd" />
                <p>{imageName}</p>
            </div>
        </>
    )




}