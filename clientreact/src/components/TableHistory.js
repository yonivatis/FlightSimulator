import React from 'react';

function TableHistory({ history }) {
  return (
    <div className="tableHistoryContainer">
      <h3>HISTORY</h3>
      <div className="lineProcess"></div>
      <div className="tableHistory">
      <table >
        <tr>
          <th>Flight number</th>
          <th>Process</th>
          <th>Airplane number</th>
          <th style={{ width: '250px' }}>End time</th>
        </tr>
        {history.map((flight) => (
          <tr key={flight.flightId}>
            <td>{flight.flightId}</td>
            <td>{flight.isLanding ? 'Landing' : 'Departure'}</td>
            <td>{flight.airplaneId}</td>
            <td>{flight.timeProcessDone}</td>
          </tr>
        ))}
      </table>
      </div>
    </div>
  );
}

export default TableHistory;
