import React, { useState } from 'react';
import './AboutUs.css';

export function AboutUs() {
    const imageLink = {
        leftimg: 'img/landing/left.svg',
        rightLink: 'img/landing/right.svg'
    }

    const propObj = [
        {
            name: "Anna",
            surname: "Chernova",
            role: "UX/UI Designer",
            photo: "img/landing/photoAboutUs.png",
            about: `I was the art leader in the Imgriff team. I was involved 
            in the creation of screens, and I also made all the illustrations
            that are in the project, and my task was to create the entire
            branding part of the project, choosing the font, colors, and so on.`,
        },
        {
            name: "Sergio",
            surname: "Azeka",
            role: "Backend Developer ",
            photo: "img/landing/about_us_foto2.png",
            about: `the ultimate tool for note management
helps you harness your creativity and 
reign in your note, you harness your creativity and reign in your notethe ultimate tool for note management
helps you harness your creativity and 
reign in your note, you harness your creativity and reign in your note`,
        }
    ];

    const [count, setCount] = useState(0);
    const [person, setPerson] = useState(propObj[count]);

    const nextClick = () => {
        const newCount = (count + 1) % propObj.length;
        setCount(newCount);
        setPerson(propObj[newCount]);
        console.log(newCount);

    };

    const prevClick = () => {
        {
            const prevCount = (count - 1 + propObj.length) % propObj.length;
            setCount(prevCount);
            setPerson(propObj[prevCount]);
            console.log(prevCount);
        }
    };

    return (
        <div className="container">
            <div>
                <p>About us</p>
            </div>
            <div>
                <dv>
                    <div>
                        <div>
                            <ul>
                                <li>Name: {person.name} {person.surname}</li>
                                <li>Role: {person.role}</li>
                            </ul>
                        </div>
                        <div>
                            <span>
                                <img src={imageLink.rightLink} onClick={prevClick} alt="Left!" />
                                <img src={imageLink.leftimg} onClick={nextClick} alt="Right!" />
                            </span>
                        </div>
                    </div>
                </dv>
                <hr id='vector7' />
                <div>
                    <div><img src={person.photo} alt="Photo" /></div>
                    <div><p>{person.about}</p></div>

                </div>
            </div>
        </div>
    );
}
