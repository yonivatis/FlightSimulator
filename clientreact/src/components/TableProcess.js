import React from 'react';

function TableProcess({ processes }) {
  return (
    <div className="tableProcessContainer">
      <h3>PROCESSES</h3>
      <div className="lineProcess"></div>
      <div className="tableProcess">
      <table>
        <thead>
          <tr>
            <th>Arrivals</th>
            <th>Departures</th>
          </tr>
        </thead>
        <tbody>
          {processes.map((process) => (
            <tr key={process.flightNum}>
              {process.isLanding ? (
                <td className="arrivalsTd">
                  Flight: {process.flightNum} Station:
                  {process.stationNum}
                </td>
              ) : (
                <React.Fragment>
                  <td></td>
                  <td className="departuresTd">
                    Flight: {process.flightNum} Station: {process.stationNum}
                  </td>
                </React.Fragment>
              )}
            </tr>
          ))}
        </tbody>
      </table>
      </div>
    </div>
  );
}

export default TableProcess;