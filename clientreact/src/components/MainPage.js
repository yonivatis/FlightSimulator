import React, { useState } from 'react';
import { useNavigate  } from 'react-router-dom';

import { useAirportClient } from '../services/AirportService';
import ImageHeader from './ImageHeader/ImageHeader';
import LegendKey from '../components/LegendKey/LegendKey';
import AirportPage from '../components/AirportPage';


function MainPage(props) {
  const [status, setStatus] = useState('Not connected');
  const airportService = useAirportClient();
   
  const navigate = useNavigate();

  const getStatus = async () => {
    const status = await airportService.getStatus();
    if (status === undefined) {
      const notConnected = "server isn't connected";
      setStatus(notConnected);
      alert(notConnected);
      return;
    }
    setStatus(status);
    alert(status);
  };

  const getAirportImage = async () => {
    navigate('/');
    await airportService.startAirport();
    airportService.startConnection();
     const status = await airportService.getStatus();
    setStatus(status);
  };

  const getHistory = async () => {
    await airportService.getHistory();  
  };

  const ClickedHistory = ()=>{
    props.parentCallBack(Date());
  }

  return (
    <div>
      {/* <div>
      <ImageHeader></ImageHeader>
      </div> */}

      <div className="menu">
        <div>
          <button className="buttonStyle" onClick={getAirportImage}>
            Start
          </button>

          <button className="buttonStyle" onClick={getStatus}>
            Get status
          </button>

          {/* <button className="buttonStyle" onClick={()=>{getHistory(); ClickedHistory()}}>
            Show and update history
          </button> */}
        </div>
        <div><h4 className="status">Airport Status : {status}</h4>
        <LegendKey></LegendKey>
        </div>

        <div>
        <AirportPage></AirportPage>

        </div>
      </div>
    </div>
  );
}

export default MainPage;


