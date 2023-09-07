import React from 'react';
import Airplane from './Airplane';

function Station({ number, name, process }) {
  return (
    <div className="rectangle">
      {number} {name}
    
      {process ? (
        <Airplane
          airplane={process.airplaneId}
          flight={process.flightNum}
          isLanding={process.isLanding}
        />
      ) : null}
    </div>
  );
}

export default Station;

