import { React, useState, useEffect } from "react";
import axios from "axios";
import "./HotelList.css";
import { API_URL } from "../../config";
import { useNavigate } from "react-router-dom";

function HotelList() {
  const [hotels, setHotels] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get(API_URL + "hotels")
      .then((response) => {
        setHotels(response.data);
      })
      .catch((error) => {
        setError(error);
      });
  }, []);

  const handleMakeReservation = (hotelId) => {
    navigate("/bookings/new?hotelId=" + hotelId);
  };

  return (
    <>
      {error && <div>Error: {error.message}</div>}
      {hotels.map((hotel) => (
        <div className="card" key={hotel.id}>
          <header className="card-header">
            <p className="card-header-title ml-2">{hotel.name}</p>
          </header>
          <div className="card-content">{hotel.description}</div>
          <footer className="card-footer">
            <button
              className="card-footer-item"
              onClick={() => navigate("/hotels/" + hotel.id)}
            >
              Details
            </button>
            <button
              className="card-footer-item"
              onClick={() => handleMakeReservation(hotel.id)}
            >
              Make a reservation
            </button>
          </footer>
        </div>
      ))}
    </>
  );
}

export default HotelList;
