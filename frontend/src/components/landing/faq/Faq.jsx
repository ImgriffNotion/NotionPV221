import './Faq.css';
import React, { useState } from 'react';

export function Faq(props) {

    const [state, setState] = useState(props);


    const staticImg = {
        question_img: 'img/landing/question_img.svg',
    }

    return (
        <div className='container2'>
            <div>
                <p>FAQ</p>
                <ul>
                    <li><hr />What can your site be used for?</li>
                    <li><hr />Can I use your site for work?</li>
                    <li><hr />Is there a limit to the number of notes I can create?</li>
                    <li><hr />Can I add images and attachments to my notes?</li>
                    <li><hr />How secure is my data?</li>
                    <li><hr /></li>
                </ul>

            </div>
            <div>
                <img src={staticImg.question_img} alt="" />
            </div>

        </div>
    )
}