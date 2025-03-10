import React from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { API_URL } from "../../../config";
import { useEffect, useState } from "react";

export default function HotelPage() {
  const { hotelId } = useParams();
  const [hotel, setHotel] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleMakeReservation = (roomId) => {
    navigate(`/bookings/new?hotelId=${hotelId}&roomId=${roomId}`);
  };

  useEffect(() => {
    axios
      .get(`${API_URL}hotels/${hotelId}`)
      .then((response) => {
        setHotel(response.data);
        console.log(response.data);
      })
      .catch((error) => {
        setError(error);
      });
  }, [hotelId]);

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  if (!hotel) {
    return <div>Loading...</div>;
  }

  return (
    <div className="m-2">
      <h2 className="title mt-4">Hotel "{hotel.name}"</h2>
      <hr />
      {hotel.photoUrl ? (
        <div className="columns">
          <div className="column is-one-quarter">
            <img className="image" src={hotel.photoUrl} alt="Hotel" />
          </div>
          <div className="column is-three-quarters">{hotel.description}</div>
        </div>
      ) : (
        <div>{hotel.description}</div>
      )}
      <h3 className="title mt-4">Rooms</h3>
      <div>
        {hotel.rooms.map((room) => (
          <div key={room.id} className="card m-2">
            <header className="card-header">
              <p className="card-header-title">{room.name}</p>
            </header>
            <div className="card-content">
              {room.photoUrl ? (
                <div className="columns">
                  <div className="column is-one-quarter">
                    <img className="image" src={room.photoUrl} alt="Room" />
                  </div>
                  <div className="column is-three-quarters">
                    <div className="content">{room.description}</div>
                    <div className="content">Location: {room.location}</div>
                    <div className="content">Price: ${room.price}</div>
                  </div>
                </div>
              ) : (
                <>
                  <div className="content">{room.description}</div>
                  <div className="content">Location: {room.location}</div>
                  <div className="content">Price: ${room.price}</div>{" "}
                </>
              )}
            </div>
            <footer className="card-footer">
              <button
                className="card-footer-item"
                onClick={() => handleMakeReservation(room.id)}
              >
                Make Reservation
              </button>
            </footer>
          </div>
        ))}
      </div>
    </div>
  );
}
