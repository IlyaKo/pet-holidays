import { useState, useEffect } from "react";
import axios from "axios";
import "./HotelList.css";

function HotelList() {
  const [hotels, setHotels] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get("http://localhost:5292/api/hotels/")
      .then((response) => {
        setHotels(response.data);
      })
      .catch((error) => {
        setError(error);
      });
  }, []);

  return (
    <div>
      <h2 className="title m-4">Hotels</h2>
      <hr />
      {error && <div> Error: {error.message} </div>}
      <div className="container is-fluid">
        {hotels.map((hotel) => (
          <div className="card" key={hotel.id}>
            <header className="card-header">
              <p class="card-header-title ml-2 ">{hotel.name}</p>
            </header>
            <div className="card-content">{hotel.description}</div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default HotelList;
