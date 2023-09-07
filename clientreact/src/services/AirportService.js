import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import axios from 'axios';
import { createContext, useContext, useState } from 'react';

const airportClientContext = createContext();
export const useAirportClient = () => useContext(airportClientContext);

export const AirportClientProvider = ({ children }) => {
  const baseUrl = 'http://localhost:5055';
  const airportUrl = `${baseUrl}/airport`;
  const [connection] = useState(
    new HubConnectionBuilder()
      .withUrl(`${baseUrl}/notifications`)
      .configureLogging(LogLevel.Information)
      .build()
  );

  const registerToAirportImage = (handler) => {
    connection.on('ReceiveAirportImage', handler); 
  };

  const registerToHistory = (handler) => {
    connection.on('ShowHistory', handler);
  };

  const startConnection = () => {
    if (connection.state === 'Disconnected') connection.start();
    else alert("Airport is already activated");
  };

  const startAirport = async () => {
    try {
      return (await axios.get(`${airportUrl}/start`));
    } catch (error) {
      console.log(error);
    }
  };

  const getStatus = async () => {
    try {
      return (await axios.get(`${airportUrl}/getstatus`)).data;
    } catch (error) {
      console.log(error);
    }
  };

  const getProcesses = async () => {
    try {
      return (await axios.get(`${airportUrl}/getprocesses`)).data;
    } catch (error) {
      console.log(error);
    }
  };

  const getHistory = async () => {
    try {
      return (await axios.get(`${airportUrl}/gethistorydata`));
    } catch (error) {
      console.log(error);
    }
  };

  const AirportService = {
    connection,
    startConnection,
    startAirport,
    registerToAirportImage,
    getStatus,
    getProcesses,
    getHistory,
    registerToHistory
  };
  return (
    <airportClientContext.Provider value={AirportService}>
      {children}
    </airportClientContext.Provider>
  );
};
