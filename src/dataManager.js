import { getTokenFromUser } from "./utils/getUserFromCookies";
import { useNavigate } from "react-router-dom";
const pagestypes = ['empty', 'board', 'list', 'calendur', 'table', 'galllery', 'library'];
//const localHost = 'https://localhost:7114';
//const localHost = 'http://26.211.160.167:7115';
//const localHost = 'https://26.211.160.167:7114';
//const localHost = 'https://10.0.2.4';
const localHost = `https://notion-back.azurewebsites.net`;
const pointUploadFile = `/imgriff/files/user-files`;


const methodGetAll = '/imgriff/pages/get-all';
const pathMainController = '/imgriff/pages'; //гетСлаг 
const pathMethodPut = '/imgriff/pages'; //изменения
const pathMethodPost = '/imgriff/pages'; //изменения
const pathAuthbyEmail = '/imgriff/auth/user-by-email';
const pathAuthLogin = '/imgriff/auth/login';
const pathGetOtp = '/imgriff/auth/get-otp'
const pathSendPostEmailAndCode = '/imgriff/auth/';








/**добавить тьокен? юзера */
export const getAllPages = async () => {
    try {
        const token = getTokenFromUser();
        const response = await fetch(localHost + methodGetAll,
            {
                method: "GET",
                headers: {
                    "Authorization": `Bearer ${token}`,
                },
                credentials: 'include'

            }
        );
        const data = await response.json();
        console.log("GET ALL PAGES", data);
        return data;

    } catch (error) {
        console.log(`error`);
        return null;
    }
}


export const getPageBySlug = async (slug) => {

    const token = getTokenFromUser();

    try {

        const response = await fetch(`${localHost}${pathMainController}?slug=${slug}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
            },
            credentials: 'include'
        });
        const data = await response.json();

        return data;

    } catch {
        console.log(`error`);
        return null;
    }
}






export const putChangesOfPage = async (page) => {
    try {
        const token = getTokenFromUser();
        const body = {
            "title": page.title,
            "banner": page.banner,
            "icon": page.icon,
            "type": page.type,
            "content":
                page.type === "Empty" ? {
                    "text": page.content?.text || null,
                } :
                    {
                        "title": page.title,
                        "internalContent": page.content,
                    },
            "slug": page.slug,
        };
        console.log("BOOOOODY POSITIV", body);

        const response = await fetch(localHost + pathMethodPut,
            {
                method: "PUT",
                headers: {
                    "Authorization": `Bearer ${token}`,
                    "Content-type": "application/json ; charset=UTF-8"
                },
                credentials: 'include',
                body: JSON.stringify(body)
            }
        );
        const data = await response.json();
        console.log("Response", data);
        return data;

    } catch (error) {
        console.log(`error`);
        return null;
    }
}
export const sendFileToServer = async (file) => {
    try {
        const token = getTokenFromUser();
        const formData = new FormData();

        if (!(file instanceof File)) {
            console.log("Incorrect file");
        }
        formData.append("uploadedFile", file);

        const response = await fetch(localHost + pointUploadFile, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
            },
            credentials: 'include',
            body: formData
        });

        const data = await response.json();
        console.log("Response", data);
        return data;
    } catch (error) {
        console.log("error", error);
        return null;
    }
}


export const createNewPage = async (namePage, type, bannerURL, iconURL, content) => {
    console.log("Зашли в createNewPage");
    const token = getTokenFromUser();
    const body = {
        "title": namePage,
        "banner": bannerURL,
        "icon": iconURL,
        "type": type,
        "content": type === "Empty" ? null : {
            "title": namePage
        }
    }
    const response = await fetch(localHost + pathMethodPost, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${token}`,
        },
        credentials: 'include',
        body: JSON.stringify(body)
    });
    const data = await response.json();
    console.log("Вышли в createNewPage");
    return data;


}

export async function AuthByEmail(email) {
    console.log("Зашли в authByEmail");

    const response = await fetch(localHost + pathAuthbyEmail + `?email=${email}`, {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',

        },
        credentials: 'include',
    });


    return await response.json();


}

export async function sendEmailAndCode(email, code) {
    console.log("Зашли в sendEmailAndCode");
    const requestData = {
        "email": email,
        "passcode": code,
    };
    const response = await fetch(localHost + pathSendPostEmailAndCode, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        credentials: 'include',
        body: JSON.stringify(requestData),
    });
    const data = await response.json();

    return data;

}


export async function continueWithGoogle() {
    console.log("Зашли в Continue with Google");

    return await (window.location.href = localHost + pathAuthLogin);
}


export async function sendEmail(email) {
    console.log("Зашли в sendEmail", email);
    if (!email) {
        throw new Error("sendEmail 102 dataManager Error");
    }
    try {
        console.log("adress", localHost + pathGetOtp + `?email=${email}`);

        const response = await fetch(localHost + pathGetOtp + `?email=${email}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
        });

        console.log("RESPONSE DATA MANAGER ", response);

        if (!response.ok) {
            console.log("ERROR (% STR ", response);
            throw new Error('response not OK');
        }
        const res = await response.json();
        console.log("RES", res);

        return res;
    } catch (error) {
        console.log("ERROR", error);

    }
}



const list = {
    id: 'айди листа',
    parent_page: 'id',
    board_id: 'id',
    title: 'Название списка',
    internal_content: [{
        id: 'айди карт',
        list_id: 'id_page',
        title: 'имя карты',
        description: 'описание карты',
        number: 'номер ',
        color: 'color',
        index: ' положение в строке',
        date: 'date',
        file: {
            id: 'id',
            name: 'file_name',
            url: 'url_file',
        },

    }],
    createdAt: 'date',
    deleteDt: 'date'

};

const table = {
    id: 'id',
    parent_page_id: 'idParent',
    title: 'table name',
    rows: 'int',
    columns: 'int',
    cards: [
        [
            {
                id: 'id',
                table_id: 'id',
                data: 'data',
                background: 'color',
                foreground: 'color',
                row: 'int',
                column: 'int'
            },
        ]
    ],
    createdAt: 'date',
    deleteAt: 'date',
};

const board = {
    id: 'id',
    title: 'title',
    parent_page: 'id',
    created_at: 'date',
    delete_at: 'date',
    internal_content:
        [
            {
                id: 'id',
                parent_page: 'id',
                board_id: 'id',
                title: 'Название списка',
                InternalContent: {
                    id: 'айди карт',
                    listId: 'id_page',
                    title: 'имя карты',
                    description: 'описание карты',
                    number: 'номер ',
                    color: 'color',
                    index: ' положение в строке',
                    date: 'date',
                    file: {
                        id: 'id',
                        name: 'file_name',
                        url: 'url_file',
                    },

                },
                createdAt: 'date',
                deleteDt: 'date'
            }
        ]
};
const calendar = {
    id: 'id',
    title: 'name',
    parent_page: 'id',
    created_at: 'date',
    deleted_at: 'date',
    internal_content: [
        {
            id: 'id',
            title: 'name',
            description: 'description',
            planed_date: 'date',
            number: 'number',
            color: 'color',
            calendar_id: 'date',
            file: {
                id: 'id',
                name: 'file_name',
                url: 'url_file',
            },
        }
    ]
};

const gallery = {
    id: 'id',
    title: 'name',
    parent_page: 'id',
    created_at: 'date',
    delete_at: 'date',
    internal_content: [
        {
            id: 'id',
            title: 'name',
            url: 'url_file',
            description: 'name',
            color: 'color',
            galery_id: 'id',
            date: 'date',
            number: 'number'
        }
    ]
};
const emptypage = [
    {
        id: 'id_emptypage_1',
        title: 'name1',
        text: 'description1',
        parent_page: 'id1'
    },
    {
        id: 'id_emptypage_2',
        title: 'name2',
        text: 'description2',
        parent_page: 'id2'
    }
];


const pages = [{
    id: 'id_page1',
    owner_id: 'id',
    title: 'EmptyPagenumber1',
    banner: 'banner.img',
    icon: 'icon.ico',
    slug: 'slug.page',
    type: 'empty',
    delete_date: 'date',
    content: 'ссылка на эту страницу'
},
{
    id: 'id_page2',
    owner_id: 'id',
    title: 'BoardNumber1',
    banner: 'banner.img',
    icon: 'icon.ico',
    slug: 'slug.page',
    type: 'board',
    delete_date: 'date',
    content: 'ссылка на эту страницу'
},
{
    id: 'id_page3',
    owner_id: 'id',
    title: 'Календарь 2025',
    banner: 'banner.img',
    icon: 'icon.ico',
    slug: 'calendar_2025',
    type: 'board',
    delete_date: 'date',
    content: 'ссылка на эту страницу'
}, {
    id: 'id_page4',
    owner_id: 'id',
    title: 'BoardNumber1',
    banner: 'banner.img',
    icon: 'icon.ico',
    slug: 'slug.page',
    type: 'board',
    delete_date: 'date',
    content: 'Приходит страница через новый запрос по слагу ?? null'
},
]
