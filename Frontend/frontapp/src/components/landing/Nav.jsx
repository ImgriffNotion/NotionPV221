import { NavLink } from 'react-router-dom';


export function Nav() {
    return (<ul className="menu">
        <li><NavLink to='/'>MainPage</NavLink></li>
        <li><a href="#feauters" >Features</a></li>
        <li><a href="#faq">FAQ</a></li>
        <li><a href="#aboutUs" >About Us</a></li >
        <li><a href="#contacts" >Contacts</a></li >
        <li><NavLink to="/login" >Log in</NavLink></li >
    </ul>);
}

const setActive = ({ isActive }) => (isActive ? "active" : "");

export default Nav;
