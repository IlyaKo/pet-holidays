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

  useEffect(() => {
    hotels.forEach((hotel) => fetchHotelPhoto(hotel.id));
  }, [hotels]);

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

  const handleMakeReservation = (hotelId) => {
    navigate("/bookings/new?hotelId=" + hotelId);
  };

  const getReviewWord = (count) => {
    if (count % 10 === 1 && count % 100 !== 11) return 'review';
    if ([2, 3, 4].includes(count % 10) && ![12, 13, 14].includes(count % 100)) return 'reviews';
    return 'reviews';
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
                <span className="rating-label">
                     {hotel.reviewsCount
                ? `${hotel.reviewsCount} ${getReviewWord(hotel.reviewsCount)}`
        : 'No reviews'}
    </span>
    {hotel.rating && (
      <span className="rating-score">
        {hotel.rating.toFixed(1)}
      </span>
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
