import { useRef } from 'react';
import { useDrag, useDrop } from 'react-dnd';

const ItemTypes = {
    CARD: 'card',
};
// В Card.js
export function Card({ card, index, moveCard, handleInputChange, listId, onExternalDrop }) {
    const ref = useRef(null);

    const [, drop] = useDrop({
        accept: ItemTypes.CARD,
        hover(item, monitor) {
            if (!ref.current) return;
            const hoverBoundingRect = ref.current.getBoundingClientRect();
            const hoverMiddleY = (hoverBoundingRect.bottom - hoverBoundingRect.top) / 2;
            const clientOffset = monitor.getClientOffset();
            const hoverClientY = clientOffset.y - hoverBoundingRect.top;

            if (item.listId === listId) {
                // Логика для перемещения внутри одного списка (оставляем без изменений)
                const dragIndex = item.index;
                const hoverIndex = index;
                if (dragIndex === hoverIndex) return;
                if (dragIndex < hoverIndex && hoverClientY < hoverMiddleY) return;
                if (dragIndex > hoverIndex && hoverClientY > hoverMiddleY) return;
                moveCard(dragIndex, hoverIndex);
                item.index = hoverIndex;
            } else {
                // Если карточка из другого списка, определяем место вставки:
                item.dropTargetIndex = hoverClientY < hoverMiddleY ? index : index + 1;
            }
        },
        drop(item, monitor) {
            if (item.listId !== listId && typeof item.dropTargetIndex === 'number') {
                if (onExternalDrop) {
                    onExternalDrop(item.card, item.listId, listId, item.dropTargetIndex);
                    item.listId = listId;
                }
            }
        },
    });


    const [{ isDragging }, drag] = useDrag({
        type: ItemTypes.CARD,
        item: { index, listId, card },
        collect: (monitor) => ({
            isDragging: monitor.isDragging(),
        }),
    });

    drag(drop(ref));

    return (
        <textarea
            ref={ref}
            className={`cardBox ${isDragging ? 'dragging' : ''}`}
            value={card.value}
            onChange={(e) => handleInputChange(e, index)}
            placeholder="Card_name"
        />
    );
}
