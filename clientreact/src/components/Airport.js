import React from 'react';
import Station from './Station';
import Arrow from './Arrow';
function Airport({ processes }) {

  return (
    <React.Fragment>
      <div className="allStations">
        <div id="stationsBox">
          {/* <Station
            number={9}
            name={'Runway'}
            process={processes && processes.find(process => process.stationNum === 9)}
          /> */}
          {/* <Arrow arrow="&#8592;" index="0" /> */}

          <Arrow arrow="&#8595;" index="0" />
          <Station
            number={4}
            name={'Runway'}
            process={processes && processes.find(process => process.stationNum === 4)}
          />
          <Arrow arrow="&#8592;" index="0" />
          <Station
            number={3}
            name={'Approach'}
            process={processes && processes.find(process => process.stationNum === 3)}
          />
          <Arrow arrow="&#8592;" index="0" />
          <Station
            number={2}
            name={'Landing preparation'}
            process={processes && processes.find(process => process.stationNum === 2)}
          />
          <Arrow arrow="&#8592;" index="0" />
          <Station
            number={1}
            name={'Landing request'}
            process={processes && processes.find(process => process.stationNum === 1)}
          />
        </div>


        <div id="transportation">
          <Arrow arrow="&#8595;" index="0" />
          <Station
            number={5}
            name={'Transportation'}
            process={processes && processes.find(process => process.stationNum === 5)}
          />
          
          <Arrow class="long" arrow="&#8600;" index="0" />
          <Arrow class="long" arrow="&#8598;" index="0" />
          <Station
            number={8}
            name={'Transportation'}
            process={processes && processes.find(process => process.stationNum === 8)}
          />
        </div>
        <Arrow class="long" arrow="&#8599;" index="1" />
        <div id="load">
          <Station
            number={6}
            name={'Load/Unload'}
            process={processes && processes.find(process => process.stationNum === 6)}
          />
          <Station
            number={7}
            name={'Load/Unload'}
            process={processes && processes.find(process => process.stationNum === 7)}
          />
          <Arrow arrow="&#8593;" index="0" />
        </div>
      </div>
    </React.Fragment>
  );
}

export default Airport;
