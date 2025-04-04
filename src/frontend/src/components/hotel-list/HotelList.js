import React, { useState, useEffect } from "react";
import axios from "axios";
import "./HotelList.css";
import { API_URL } from "../../config";
import { useNavigate } from "react-router-dom";
import { FaMapMarkerAlt } from 'react-icons/fa';


const DEFAULT_HOTEL_PHOTO = '/no-photo.jpg';

function HotelList() {
  const [hotels, setHotels] = useState([]);
  const [error, setError] = useState(null);
  const [hotelPhotos, setHotelPhotos] = useState({});
  const navigate = useNavigate();
  const [hotelRatings, setHotelRatings] = useState({});

  useEffect(() => {
    axios
      .get(API_URL + "hotels")
      .then((response) => {
        setHotels(response.data);
        response.data.forEach((hotel) => {
          fetchHotelPhoto(hotel.id);
          fetchHotelRating(hotel.id); 
        });
      })
      .catch((error) => {
        setError(error);
      });
  }, []);

 
  const fetchHotelPhoto = async (hotelId) => {
    try {
      const response = await axios.get(`${API_URL}hotels/${hotelId}/photo`);
      if (response.status === 200) {
        setHotelPhotos((prev) => ({ ...prev, [hotelId]: response.data.photoUrl }));
      }
    } catch (error) {
      console.error(`Error fetching photo for hotel ${hotelId}:`, error);
    }
  };

  const fetchHotelRating = async (hotelId) => {
    try {
      const response = await axios.get(`http://localhost:5004/api/ratings/hotels/${hotelId}`);
      if (response.status === 200) {
        setHotelRatings((prev) => ({
          ...prev,
          [hotelId]: response.data,
        }));
      }
    } catch (error) {
      console.error(`Error fetching rating for hotel ${hotelId}:`, error);
    }
  };

  const handleMakeReservation = (hotelId) => {
    navigate("/bookings/new?hotelId=" + hotelId);
  };

  const getReviewWord = (count) => {
    return count === 1 ? 'review' : 'reviews';
  };
  return (
    <>
     {error && <div>Error: {error.message}</div>}

<div className="hotel-list">
  {hotels.map((hotel) => (
    <div className="hotel-card" key={hotel.id}>
      <div className="hotel-image">
        <img
          src={hotelPhotos[hotel.id] || DEFAULT_HOTEL_PHOTO}
          alt={hotel.name}
        />
      </div>

      <div className="hotel-info">
        <div className="hotel-header">
          <h2 className="hotel-name">{hotel.name}</h2>
        </div>

        <p className="hotel-location">
          <FaMapMarkerAlt className="location-icon" />
          &nbsp;City center
        </p>

        <p className="hotel-description">{hotel.description}</p>

        <div className="hotel-bottom">
          <div className="hotel-rating">
            {hotelRatings[hotel.id] ? (
              <>
                <span className="rating-label">
                  {hotelRatings[hotel.id].reviews} {getReviewWord(hotelRatings[hotel.id].reviews)}
                </span>
                <span className="rating-score">
                  {hotelRatings[hotel.id].average.toFixed(1)}
                </span>
              </>
            ) : (
              <span className="rating-label">No reviews</span>
            )}
          </div>

          <button
            className="show-prices"
            onClick={() => navigate("/hotels/" + hotel.id)}
          >
            Show prices
          </button>
        </div>
      </div>
    </div>
  ))}
</div>
    </>
  );
}

export default HotelList;
