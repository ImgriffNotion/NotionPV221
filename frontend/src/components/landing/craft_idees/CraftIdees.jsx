function CraftIdees() {
    const setActive = ({ isActive }) => (isActive ? "active" : "");




    return (
        <div className='container'>
            <div>
                <p>Craft your ideas in your style</p>
            </div>
            <div>
                <NavLink to="/" className={setActive}>Board</NavLink>
                <NavLink to="/1" className={setActive}>List</NavLink>
                <NavLink to="/2" className={setActive}>Calendar</NavLink>
                <NavLink to="/3" className={setActive}>Table</NavLink>
                <NavLink to="/4" className={setActive}>Empty page</NavLink>

            </div>
            <div>
                <img src="/img/craftIdea.png" alt="" />
            </div>


        </div>
    )
}