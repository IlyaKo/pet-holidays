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

  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      <h2>Hotels</h2>
      <hr />
      <ul>
        {hotels.map((hotel) => (
          <li key={hotel.id}>
            <b>{hotel.name}</b>
            <br />
            <span> {hotel.description} </span>
            <br />
            <br />
          </li>
        ))}
      </ul>
    </div>
  );
}

export default HotelList;
