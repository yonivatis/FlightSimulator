import React from 'react';

function Airplane(props) {
  return (
    <React.Fragment>
      {props.isLanding ? (
        <div className="ball red-ball">
          <div>Flight: {props.flight}</div>
          <div>Airplane: {props.airplane}</div>
        </div>
      ) : (
        <div className="ball blue-ball">
          <div>Flight: {props.flight}</div>
          <div>Airplane: {props.airplane}</div>
        </div>
      )}
    </React.Fragment>
  );
}

export default Airplane;
