import './ScrollToTop.css';

export function ScrollToTop() {

    function scrollToTop() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    }
    return <div className="scroll-to-top" onClick={scrollToTop}>
        TOP
    </div>

}