import { Nav } from './Nav.jsx';
import './Landing.css';
import { AboutUs } from './about_us/AboutUs.jsx';
import { Faq } from './faq/Faq.jsx';
import { Contacts } from './contacts/Contacts.jsx';
import { ScrollToTop } from '.././ScrollToTop/ScrollToTop.jsx';

export function Landing() {

    const objState = {
        image_header: 'img/landing/Rectangle183.svg',
        google_icon: 'img/loginpage/google_icon.svg',
        imgriff: 'img/landing/Frame13.svg',
        right_fon: 'img/landing/illustration21.svg',
        gorizontal_block_img: 'img/landing/Template1.png',
        vertical_right_block_img: 'img/landing/Template3.png',
        vertical_left_block_img: 'img/landing/Template2.png',
        foot_image_left: 'img/landing/foot-image-left.png',
        foot_image_middle: 'img/landing/foot-image-middle.png',
        foot_image_right: 'img/landing/foot-image-right.png',
    }



    return (<div className="Landing">

        <div className="header">
            <a href="#"><img src={objState.image_header} alt="" /></a>
            <Nav />
        </div>
        <hr id='hr1' />

        <div className="firstBlock">
            <div className="firstBlockLeftSide">
                <div >
                    <p>Unlock your potential
                        with
                        <img src={objState.imgriff}></img>
                    </p>
                </div>
                <div>
                    <p>
                        the ultimate tool for note management
                        helps you harness your creativity and
                        reign in your note</p>
                </div>
                <div>
                    <a>Get started</a>
                </div>
            </div>
            <div className="firstBlockRightSide">
                <img src={objState.right_fon} alt="You'r champion!" />
            </div>

        </div>

        <div className="secondBlock" id='feauters'>
            <div className="gorizontalBlock">
                <p>From ideas to plans,
                    we've got a Template for you</p>
                <span>With our templates, you can structure your thoughts, organize information quickly, and focus on what truly matters. Choose templates that fit your needs and enjoy the convenience and simplicity they offer.</span>
                <img src={objState.gorizontal_block_img} alt="" />
            </div>
            <div >
                <div className="vertikalBlockLeft">
                    <p>Choose what is convenient for you</p>
                    <span>Experience the difference our templates can make in your note-taking routine. Whether for school, work, or personal projects, our templates are here to help you stay organized, productive, and focused. </span>
                    <img src={objState.vertical_left_block_img} alt="" />
                </div>
                <div className="vertikalBlockRight">
                    <p>These templates help you:</p>
                    <ul>
                        <li>Save time on formatting</li>
                        <li>Simplify the note-taking process</li>
                        <li>Maintain order and structure in your notes</li>
                        <li>Boost productivity and focus on tasks</li>
                    </ul>
                    <img src={objState.vertical_right_block_img} alt="" />
                </div>
            </div>
        </div>
        <div className="thirdBlock">
            <div>
                <img src={objState.foot_image_left} alt="pic" />
                <img src={objState.foot_image_middle} alt="pic" />
                <img src={objState.foot_image_right} alt="pic" />
            </div>
            <div>
                <div>
                    <p>Variable notes</p>
                    <span>You can capture your ideas in various formats, whether it's tables, boards, galleries, and more, adapting to your preferred style of organization</span>
                </div>
                <div>
                    <p>Go to your goals</p>
                    <span>From structured work-related notes to creatively organizing personal thoughts, our platform empowers you to adapt notes to different contexts </span>
                </div>
                <div>
                    <p>Accessible Simplicity</p>
                    <span>Enjoy effortless navigation and a user-friendly interface, ensuring that anyone can quickly understand and utilize our platform to its fullest potential.</span>
                </div>
            </div>
        </div>
        <div id='aboutUs'><AboutUs /></div>
        <div id='faq'><Faq /></div>
        <div id='contacts'><Contacts /></div>
        <div><ScrollToTop /></div>


    </div>
    );


}


