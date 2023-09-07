import React from 'react'

function Arrow({arrow,index}) {

  const arrowStyle = index === "1" ? "arrowLoad" : "arrow";

    return (
      <div className={arrowStyle}>
        <span>{arrow}</span>
      </div>
    );
}

export default Arrow
