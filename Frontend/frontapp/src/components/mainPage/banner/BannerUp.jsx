import './BannerUp.css';
import { useState, useEffect } from 'react';




export function BannerUp() {

    const [bannerPath, setBannerPath] = useState('img/mainPage/bannerUp/Anime/anime_ban_1.svg');
    const mainPath = 'img/mainPage/bannerUp/';
    const staticImage = {
        iconAvatar: 'img/mainPage/icons/avatar.svg',
        iconDots: 'img/mainPAge/icons/dots.svg',
        bannerImage: [
            {
                "Anime/": [
                    "anime_ban_1.svg",
                    "anime_ban_2.png",
                    "anime_ban_3.svg"
                ]
            },
            {
                "Default/": [
                    "default_banner_1.svg",
                    "default_banner_2.svg",
                    "default_banner_3.svg",
                ]
            },
            {
                "Schmetterlinge/": [
                    "schmetterlinge_banner_1.svg",
                    "schmetterlinge_banner_2.svg",
                    "schmetterlinge_banner_3.svg"
                ]
            },
            {
                "Weltraum/": [
                    "weltraum_banner_1.svg",
                    "weltraum_banner_2.svg",
                    "weltraum_banner_3.svg"
                ]
            },
        ]
    }

    useEffect(() => {

        const bannerImage = staticImage.bannerImage;
        let currentIndex = 0;
        let categoryIndex = 0;


        const interval = setInterval(() => {

            const currentCategory = bannerImage[categoryIndex];
            const categoryKey = Object.keys(currentCategory)[0];
            const bannerSrc = currentCategory[categoryKey][currentIndex];
            const res = mainPath + categoryKey + bannerSrc;
            setBannerPath(res);

            currentIndex++;
            if (currentIndex >= currentCategory[categoryKey].length) {
                currentIndex = 0;
                categoryIndex++;
                if (categoryIndex >= bannerImage.length) {
                    categoryIndex = 0;
                }
            }
        }, 3000);
        
        return () => clearInterval(interval);
    },

        []);

    return (
        <div className='bannerUpComp'>
            <div className='bannerUserInfo'>
                <div>
                    <img src={staticImage.iconDots} alt="..." />
                    <img src={staticImage.iconAvatar} alt="" />
                </div>
            </div>
            <img src={bannerPath} alt="" />
        </div>
    )

}