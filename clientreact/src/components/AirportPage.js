import { useState } from 'react';
import { useAirportClient } from '../services/AirportService';
import TableProcess from './TableProcess';
import Airport from './Airport';
import TableHistory from './TableHistory';

const AirportPage = (props) => {
  const [airportImage, setAirportImage] = useState();
  const [flightsHistory, setFlightsHistory] = useState();
  const airportClient = useAirportClient();

  airportClient.registerToAirportImage(setAirportImage);

  airportClient.registerToHistory(setFlightsHistory);

  return (
    <div>
      <Airport processes={airportImage?.processes || []} />

      <section className="sectionTables">
        <div className="tableContainer">
          <TableProcess
            processes={airportImage?.processes || []}
          ></TableProcess>
          {/* <TableHistory
            history={flightsHistory ? flightsHistory : []}
          ></TableHistory> */}
        </div>
      </section>
    </div>
  );
};
export default AirportPage;
