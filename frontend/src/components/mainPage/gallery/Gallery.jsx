import './Gallery.css';
import { useState } from 'react';
import { GalleryCard } from './galleryCard/GalleryCard';


export function Gallery() {

    const [inputNameBoard, setInputNameBoard] = useState('Gallery');
    const [inputNewImageCard, setInputNewImageCard] = useState([]);




    const addnewGalleryCard = () => {
        setInputNewImageCard((inputNewImageCard) =>
            [...inputNewImageCard,
            <GalleryCard key={inputNewImageCard.length} staticImage={staticImage} id={`card${inputNewImageCard?.length || 0}`} />
            ]);
    }


    const staticImage = {
        imageSimple: 'img/mainPage/gallery/1729314189.jpg',
        iconPlus: 'img/mainPage/icons/iconPlus3.svg',
        iconClose: 'img/mainPage/icons/iconClose.svg'
    }

    return (
        <div className='galleryBox'>

            <div>
                <input type='text' className='inputName' value={inputNameBoard} onChange={(e) => {
                    setInputNameBoard(e.target.value);
                }
                } />

                <hr />
            </div>

            <div className="boxForGalleryImages scrollable">
                {inputNewImageCard}
                <div className='addNewImageToGallery' onClick={addnewGalleryCard}>
                    <img src={staticImage.iconPlus} alt="Plus" />
                    <p> New</p>
                </div>
            </div>


        </div>);

}



