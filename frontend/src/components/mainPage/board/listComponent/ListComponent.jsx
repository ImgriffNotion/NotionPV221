import './ListComponent.css'
import { Card } from './cardComponent/CardComponent';
import { use, useState, React, useRef } from 'react'
import { useDrag, useDrop } from 'react-dnd';

const staticImage = {
    iconAddList: 'img/mainPage/icons/iconAddList.svg',
    iconPlus: 'img/mainPage/icons/iconPlus2.svg'
}


const ItemTypes = {
    CARD: 'card',
};


export function ListComponent({ listId, cards, updateList, moveCardBetweenLists, addNewList }) {
    const [isInputName, setIsInputName] = useState(false);
    const [inputName, setInputName] = useState('');
    // add new card to list
    const addCard = () => {
        // Используем Date.now() для уникального id
        const newCard = { id: Date.now(), value: '' };
        updateList(listId, { cards: [...cards, newCard] });
    };

    // Обработка изменения текста карточки
    const handleInputChange = (e, index) => {
        const newCards = [...cards];
        newCards[index].value = e.target.value;
        updateList(listId, { cards: newCards });
    };

    // Перемещение карточки внутри одного списка (сортировка)
    const moveCard = (dragIndex, hoverIndex) => {
        const newCards = [...cards];
        const [movedCard] = newCards.splice(dragIndex, 1);
        newCards.splice(hoverIndex, 0, movedCard);
        updateList(listId, { cards: newCards });
    };

    // Настройка drop-области для приёма карточек из других списков
    const [, drop] = useDrop({
        accept: ItemTypes.CARD,
        drop(item, monitor) {
            // Если drop не произошёл на карточке, добавляем в конец
            if (item.listId !== listId) {
                moveCardBetweenLists(item.card, item.listId, listId, cards.length);
                item.listId = listId;
            }
        }
    });

    const clickHandle = () => {
        addNewList();
        setIsInputName(true);
    }

    return (
        <div ref={drop} className="listComponentBox">
            {isInputName === true ? (
                <><input type="text" value={inputName} onChange={(e) => setInputName(e.target.value)} className='nameListComponentCreated' />

                    {cards.map((card, index) => (
                        <Card
                            key={card.id}
                            index={index}
                            card={card}
                            moveCard={moveCard}
                            handleInputChange={handleInputChange}
                            listId={listId}
                            onExternalDrop={(card, sourceListId, destListId, dropIndex) => {
                                moveCardBetweenLists(card, sourceListId, destListId, dropIndex);
                            }}
                        />
                    ))}
                    <button onClick={addCard}>Add Card</button>
                </>
            ) : (
                <>
                    <input
                        value={inputName}
                        type="text"
                        placeholder="Enter a list title..."
                        onChange={(e) => setInputName(e.target.value)}
                        className="nameListComponent"
                    />
                    <button onClick={clickHandle}>
                        <img src={staticImage.iconAddList} alt="" />
                        Add List
                    </button>
                </>
            )}
        </div>
    );
}
