import './Board.css';
import { useState } from 'react';
import { ListComponent } from './listComponent/ListComponent';
import { HTML5Backend } from 'react-dnd-html5-backend';
import { DndProvider } from 'react-dnd';

export function Board() {

    // Состояние с массивом списков. Каждый список имеет id, заголовок и массив карточек.
    const [lists, setLists] = useState([
        { id: 'list_0', title: 'List 0', cards: [] }
    ]);
    const [inputNameBoard, setInputNameBoard] = useState('Board');
    // Добавление нового списка
    const addNewList = () => {
        setLists(prev => [
            ...prev,
            { id: 'list_' + prev.length, title: `List ${prev.length}`, cards: [] }
        ]);
    };

    // Обновление данных списка (например, изменение карточек)
    const updateList = (listId, newData) => {
        setLists(prev =>
            prev.map(list =>
                list.id === listId ? { ...list, ...newData } : list
            )
        );
    };

    // Перемещение карточки между списками:
    // Удаляем карточку из списка-источника и добавляем в список-соседа.
    // В Board.js
    const moveCardBetweenLists = (card, sourceListId, destListId, dropIndex) => {
        setLists(prev =>
            prev.map(list => {
                if (list.id === sourceListId) {
                    return { ...list, cards: list.cards.filter(c => c.id !== card.id) };
                } else if (list.id === destListId) {
                    const newCards = [...list.cards];
                    newCards.splice(dropIndex, 0, card);
                    return { ...list, cards: newCards };
                } else {
                    return list;
                }
            })
        );
    };


    return (
        <DndProvider backend={HTML5Backend}>
            <div className="board">
                <div>
                    <input type='text' className='inputName' value={inputNameBoard} onChange={(e) => {
                        setInputNameBoard(e.target.value);
                    }
                    } />

                    <hr />
                </div>


                <div id="boxForListComponent" className="borderBoxForList scrollable">
                    {lists.map(list => (
                        <ListComponent
                            key={list.id}
                            listId={list.id}
                            cards={list.cards}
                            updateList={updateList}
                            moveCardBetweenLists={moveCardBetweenLists}
                            addNewList={addNewList}
                        />
                    ))}

                </div>
            </div>
        </DndProvider>
    );
}