import React, { useState, useEffect } from "react";
import axios from "axios";

function UpdateHotel({ hotel, onUpdate }) {
  const [name, setName] = useState(hotel.name);
  const [description, setDescription] = useState(hotel.description);
  const [isActive, setIsActive] = useState(hotel.isActive); 

  useEffect(() => {
    setName(hotel.name);
    setDescription(hotel.description);
    setIsActive(hotel.isActive); 
  }, [hotel]);

  const handleSubmit = (e) => {
    e.preventDefault();

    axios
      .put(`http://localhost:5001/api/hotels/${hotel.id}`, { name, description, isActive }) 
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
        <label className="label" htmlFor="name">Hotel Name</label>
        <div className="control">
          <input
            className="input"
            type="text"
            id="name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
            placeholder="Enter hotel name"
          />
        </div>
      </div>
      <div className="field">
        <label className="label" htmlFor="description">Hotel Description</label>
        <div className="control">
          <textarea
            className="textarea"
            id="description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
            placeholder="Enter hotel description"
          />
        </div>
      </div>
      <div className="field">
        <label className="label" htmlFor="isActive">Is Active</label>
        <div className="control">
          <input
            type="checkbox"
            id="isActive"
            checked={isActive}
            onChange={(e) => setIsActive(e.target.checked)}
          />
        </div>
      </div>
      <div className="field">
        <div className="control">
          <button type="submit" className="button is-primary">Update Hotel</button>
        </div>
      </div>
    </form>
  );
}

export default UpdateHotel;