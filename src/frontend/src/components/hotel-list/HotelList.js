import { useState, useEffect } from "react";
import axios from "axios";

function HotelList() {
  const [hotels, setHotels] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get("http://localhost:5292/api/hotels/")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Backend is not responding");
        }
        return response.json();
      })
      .then((data) => {
        setHotels(data);
      })
      .catch((error) => {
        setError(error);
      });
  }, []);

  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      <h1>Hotel List</h1>
      <ul>
        {hotels.map((hotel) => (
          <li key={hotel.id}>{hotel.name}</li>
        ))}
      </ul>
    </div>
  );
}

export default HotelList;
