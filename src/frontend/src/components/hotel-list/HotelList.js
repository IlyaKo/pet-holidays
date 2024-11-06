import { React, useState, useEffect } from "react";
import axios from "axios";
import "./HotelList.css";
import UpdateHotel from "./UpdateHotel";

function HotelList() {
  const [hotels, setHotels] = useState([]);
  const [error, setError] = useState(null);
  const [editingHotelId, setEditingHotelId] = useState(null);
  const [newHotelName, setNewHotelName] = useState(""); // Для нового отеля
  const [newHotelDescription, setNewHotelDescription] = useState(""); // Для описания нового отеля
  const [isFormVisible, setIsFormVisible] = useState(false); // Для отображения/скрытия формы добавления отеля

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
      hotels.map((hotel) =>
        hotel.id === updatedHotel.id ? updatedHotel : hotel
      )
    );
    setEditingHotelId(null); // Закрытие формы редактирования после обновления
  };

  const toggleEditForm = (hotelId) => {
    // Если hotelId равен текущему editingHotelId, скрываем форму, иначе отображаем
    setEditingHotelId((prevId) => (prevId === hotelId ? null : hotelId));
  };

  const handleAddHotel = (e) => {
    e.preventDefault();
    // Отправляем данные нового отеля на сервер
    axios
      .post("http://localhost:5001/api/hotels/", {
        name: newHotelName,
        description: newHotelDescription,
        isActive: true,
      })
      .then((response) => {
        setHotels([...hotels, response.data]);

        // Очищаем поля формы
        setNewHotelName("");
        setNewHotelDescription("");
        // Скрываем форму
        setIsFormVisible(false);
      })
      .catch((error) => {
        console.error("Error adding hotel:", error);
      });
  };

  return (
    <>
      {error && <div>Error: {error.message}</div>}
      <div className="container is-fluid">
        {/* Кнопка для отображения формы добавления отеля */}
        <button
          onClick={() => setIsFormVisible(!isFormVisible)}
          className="button is-primary is-fullwidth"
        >
          {" "}
          {isFormVisible ? "Cancel" : "Add Hotel"}{" "}
        </button>

        {/* Форма для добавления нового отеля */}
        {isFormVisible && (
          <form onSubmit={handleAddHotel} className="box">
            <h2 className="title is-4">Add New Hotel</h2>

            <div className="field">
              <label className="label"> Hotel Name </label>
              <div className="control">
                <input
                  className="input"
                  value={newHotelName}
                  onChange={(e) => setNewHotelName(e.target.value)}
                  required
                  placeholder="Enter hotel name"
                />
              </div>
            </div>

            <div className="field">
              <label className="label"> Hotel Description </label>
              <div className="control">
                <textarea
                  className="textarea"
                  value={newHotelDescription}
                  onChange={(e) => setNewHotelDescription(e.target.value)}
                  required
                  placeholder="Enter hotel description"
                  autoComplete="off"
                />
              </div>
            </div>

            <div className="field">
              <div className="control">
                <button className="button is-success">Add Hotel</button>
              </div>
            </div>
          </form>
        )}

        {hotels.map((hotel) => (
          <div className="card" key={hotel.id}>
            <header className="card-header">
              <p className="card-header-title ml-2">{hotel.name}</p>
            </header>
            <div className="card-content">{hotel.description}</div>
            <div className="buttons">
              <button
                onClick={() => toggleEditForm(hotel.id)}
                className="button is-warning"
              >
                {" "}
                Update
              </button>
              <button
                onClick={() => deleteHotel(hotel.id)}
                className="button is-danger"
              >
                Remove
              </button>
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
