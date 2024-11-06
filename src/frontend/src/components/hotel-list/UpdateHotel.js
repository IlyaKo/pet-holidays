import React, { useState } from "react";
import axios from "axios";

function UpdateHotel({ hotel, onUpdate }) {
  const [name, setName] = useState(hotel.name);
  const [description, setDescription] = useState(hotel.description);
  const [isActive, setIsActive] = useState(hotel.isActive);

  const handleSubmit = (e) => {
    e.preventDefault();

    axios
      .put(`http://localhost:5001/api/hotels/${hotel.id}`, {
        name,
        description,
        isActive,
      })
      .then((response) => {
        onUpdate(response.data);
      })
      .catch((error) => {
        console.error("Error updating hotel:", error);
      });
  };

  return (
    <form onSubmit={handleSubmit} className="box">
      <h2 className="title is-4">Update Hotel</h2>

      <div className="field">
        <label className="label"> Hotel Name </label>
        <div className="control">
          <input
            className="input"
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
            placeholder="Enter hotel name"
            autoComplete="off"
          />
        </div>
      </div>

      <div className="field">
        <label className="label"> Hotel Description </label>
        <div className="control">
          <textarea
            className="textarea"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
            placeholder="Enter hotel description"
            autoComplete="off"
          />
        </div>
      </div>

      <div className="field">
        <div className="control">
          <label className="checkbox">
            <input
              type="checkbox"
              checked={isActive}
              onChange={(e) => setIsActive(e.target.checked)}
            />
            Is Active
          </label>
        </div>
      </div>

      <div className="field">
        <div className="control">
          <button className="button is-primary">Update Hotel</button>
        </div>
      </div>
    </form>
  );
}

export default UpdateHotel;
