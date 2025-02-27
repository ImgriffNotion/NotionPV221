import { createSlice } from "@reduxjs/toolkit";



//slice for modal window

const modalSlice = createSlice({
    name: 'modal',  //Имя слайса. Оно будет использоваться для генерации имени редьюсера
    initialState: {
        isModalOpen: false,  //initialState: Начальное состояние слайса.
        modalData: null
    },
    reducers: { // Здесь мы описываем функции, которые изменяют состояние
        openModal(state, action) {
            state.isModalOpen = true;
            state.modalData = action.payload; // данные передаваемые при открытии
        },
        closeModal(state) {
            state.isModalOpen = false;
            state.modalData = null; //очизщаем данные при закрытии
        },
        updateModalData(state, action) {
            state.modalData = action.payload;  // обновление данных 
        },
        setModalText(state, action) {
            if (state.modalData) {
                state.modalData.text = action.payload;
            }
        }
    },
});

//Экспрт actions и редьюсер
export const { openModal, closeModal, updateModalData, setModalText } = modalSlice.actions;
export default modalSlice.reducer;