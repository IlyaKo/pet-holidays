import { React, useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
import "./HotelList.css";
import UpdateHotel from "./UpdateHotel";

function HotelList() {
  const [hotels, setHotels] = useState([]);
  const [error, setError] = useState(null);
  const [editingHotelId, setEditingHotelId] = useState(null);


  useEffect(() => {
    axios
      .get("http://localhost:5001/api/hotels/")
      .then((response) => {
        setHotels(response.data);
      })
      .catch((error) => {
        setError(error);
      });
  }, []);

  const deleteHotel = (id) => {
    axios
      .delete(`http://localhost:5001/api/hotels/${id}`)
      .then(() => {
        setHotels(hotels.filter((hotel) => hotel.id !== id));
      })
      .catch((error) => {
        setError(error);
      });
  };

  const handleUpdateHotel = (updatedHotel) => {
    setHotels(
      hotels.map((hotel) => (hotel.id === updatedHotel.id ? updatedHotel : hotel))
    );
    setEditingHotelId(null);  //Закрытие формы редактирования после обновления
  };

  const toggleEditForm = (hotelId) => {
    //Если hotelId равен текущему editingHotelId, скрываем форму, иначе отображаем
    setEditingHotelId((prevId) => (prevId === hotelId ? null : hotelId));
  };


  return (
    <>
      {error && <div>Error: {error.message}</div>}
      <div className="container is-fluid">
        {hotels.map((hotel) => (
          <div className="card" key={hotel.id}>
            <Link to={`/hotels/${hotel.id}`}>
              <header className="card-header">
                <p className="card-header-title ml-2">{hotel.name}</p>
              </header>
              <div className="card-content">{hotel.description}</div>
            </Link>
            <div className="button-container">
              {/* Открытие/закрытие формы редактирования */}
              <button onClick={() => toggleEditForm(hotel.id)} className="button is-update"> Update</button>
              <button onClick={() => deleteHotel(hotel.id)} className="button is-danger">Remove</button>
            </div>

            {editingHotelId === hotel.id && (
              <UpdateHotel hotel={hotel} onUpdate={handleUpdateHotel} />
            )}
          </div>
        ))}
      </div>
    </>
  );
}

export default HotelList;