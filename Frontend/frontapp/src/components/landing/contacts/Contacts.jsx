import './Contacts.css';


export function Contacts() {


    const staticImg = {
        icon: 'img/landing/icon.svg',
        insta_icon: 'img/landing/instaIcon.svg',
        twitter_icon: 'img/landing/twitter.svg',
        fb_icon: 'img/landing/fbIcon.svg',
        youtube_icon: 'img/landing/youtubeIcon.svg',
        tiktok_icon: 'img/landing/tiktokIcon.svg',

    }




    return (
        <div className="containerContacts">
            <div>
                <img src={staticImg.icon} alt="" />
            </div>
            <div>
                <ul>
                    <li><a href="">IMGRIFF</a></li>
                    <li><div></div></li>
                    <li><a href="">Features</a></li>
                    <li><a href="">FAQ</a></li>
                    <li><a href="">About us</a></li>
                </ul>
            </div>
            <div>
                <div><a href="">CONTACTS</a></div>
                <div><p>support.imgriff@gmail.com</p></div>
                <div>
                    <img src={staticImg.insta_icon} alt="" />
                    <img src={staticImg.twitter_icon} alt="" />
                    <img src={staticImg.fb_icon} alt="" />
                    <img src={staticImg.youtube_icon} alt="" />
                    <img src={staticImg.tiktok_icon} alt="" />
                </div>
            </div>
        </div>
    )
}