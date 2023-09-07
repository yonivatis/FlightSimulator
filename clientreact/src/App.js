import { useState } from 'react';
import React from 'react';
import './App.css';

import MainPage from './components/MainPage';
// as Router 
// , BrowserRouter as Router
import { Routes, Route } from 'react-router-dom';
import { AirportClientProvider } from './services/AirportService';
import AirportPage from './components/AirportPage';

function App() {

  const [date, SetDate] = useState();

  const HandleHistoryClicked = (date) => {
    SetDate(date);
  }

  return (
    <div className="App">


      <AirportClientProvider>
        {/* <Routes>
          <Route path='/' index element={<MainPage parentCallback={HandleHistoryClicked} />} />

          <Route path='/airport' element={<AirportPage />}></Route>
   

        </Routes> */}


          <Routes>
            {/* <Route path='/' index element={<MainPage parentCallback={HandleHistoryClicked} />} /> */}


            <Route path="/" element={<MainPage parentCallback={HandleHistoryClicked} />}>
              <Route path="/" index element={<MainPage parentCallback={HandleHistoryClicked} />} />
              <Route path="/airport" index element={<AirportPage />} />
              <Route element={<div className="historyStatus">Last updated: {date}</div>} />
            </Route>

            <Route path="/airport" element={<AirportPage />}>
              <Route path="/airport" index element={<AirportPage />} />
            </Route>
            <Route element={<div className="historyStatus">Last updated: {date}</div>} />

          </Routes>
          
      </AirportClientProvider>


    </div>
  );
}

export default App;
