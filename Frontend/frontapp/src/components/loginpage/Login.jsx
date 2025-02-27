import { useState } from "react"
import './Login.css';

export function Login(props) {
    /**прокинуть пропсами ссылки на картинки  */
    const [state, setState] = useState(props);

    const objState = {
        obj_state_fon: 'img/loginpage/Login_main.png',
        google_icon: 'img/loginpage/google_icon.svg',

    }

    return (<div className="containerLogin" >
        <div><img src={objState.obj_state_fon} alt="" /></div>
        <div>
            <div className="RegistrationBox">
                <div>
                    <p>Log in</p>
                    <a href=""><img src={objState.google_icon} alt="" />Continue with Google</a>
                    <hr />
                </div>
                <div>
                    <p>Email</p>
                    <input type="text" placeholder="Enter your email" />
                    <a href="">Check your inbox</a>
                </div>
                <div>
                    <p>Login code</p>
                    <input type="text" placeholder="Enter your login code" />
                    <button>Continue with login code</button>
                    <a href="">Didn't receive the code?</a>
                </div>

            </div>
        </div>
    </div >


    )

}