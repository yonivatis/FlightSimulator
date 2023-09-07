import '../LegendKey/LegendKey.css'

const LegendKey = () => {
    return ( 
        <div className="legendKeyContainer">
            <div className="div1">
                <h5>Departure</h5>
                <h5>Landing</h5>
            </div>
            <div className="div2">
                <div className="DepartureColorKey"></div>
                <div className="LandingColorKey"></div>
            </div>
        </div>
     );
}
 
export default LegendKey;