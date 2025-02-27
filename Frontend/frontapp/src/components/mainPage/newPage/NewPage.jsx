import './NewPage.css';
import { useState } from 'react';
import { Board } from '../board/Board';
import { Gallery } from '../gallery/Gallery';
import { Calendar } from '../calendar/Calendar';
import { ListComponent } from '../listComponent/ListComponent';
import { TableComponent } from '../tableComponent/TableComponent';
import { EmptyPage } from '../emptyPage/EmptyPage';
import { TemplatesComponent } from '../templates/TemplatesComponent'



export function NewPage({ setComponent }) {

  const [name, setName] = useState('Untitled')


  const path = 'img/mainPage/icons/'
  const staticImage = {
    iconEmptyPage: 'iconEmptyPage',
    iconList: 'iconList',
    iconBoard: 'iconBoard',
    iconTemplates: 'iconTemplates',
    iconTable: 'iconTable',
    iconGallery: 'iconGallery',
    iconCalendar: 'iconCalendar',
    iconClose: 'iconClose',

  }
  Object.entries(staticImage).forEach(([key, value]) => {
    staticImage[key] = path + value + '.svg'
  });


  return (
    <div className='newPageComponent'>
      <div>

        <input type='text' value={name} onChange={(e) => setName(e.target.value)} />
        <img src={staticImage.iconClose} alt="" />
      </div>
      <div>
        <ul>
          <li>
            <div onClick={(e) => setComponent(<EmptyPage setComponent={setComponent} />)}><img src={staticImage.iconEmptyPage} alt="" />Empty Page</div>
          </li>
          <li>
            <div onClick={(e) => setComponent(<TableComponent setComponent={setComponent} />)}><img src={staticImage.iconTable} alt="" />Table</div>
          </li>
          <li>
            <div onClick={(e) => setComponent(<ListComponent setComponent={setComponent} />)}><img src={staticImage.iconList} alt="" />List</div>
          </li>
          <li>
            <div onClick={(e) => setComponent(<Gallery setComponent={setComponent} />)}><img src={staticImage.iconGallery} alt="" />Gallery</div>
          </li>
          <li>
            <div onClick={(e) => setComponent(<Board setComponent={setComponent} />)}><img src={staticImage.iconBoard} alt="" />Board</div>
          </li>
          <li>
            <div onClick={(e) => setComponent(<Calendar setComponent={setComponent} />)}><img src={staticImage.iconCalendar} alt="" />Calendar</div>
          </li>
          <li>
            <div onClick={(e) => setComponent(<TemplatesComponent setComponent={setComponent} />)}><img src={staticImage.iconTemplates} alt="" />Templates</div>
          </li>

        </ul>
      </div>
      <div>
        <button >Create</button>
      </div>

    </div>
  );

}