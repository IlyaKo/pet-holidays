import React, { useState } from "react";
import axios from "axios";

function UpdateHotel({ hotel, onUpdate }) {
  const [name, setName] = useState(hotel.name);
  const [description, setDescription] = useState(hotel.description);
  const [isActive, setIsActive] = useState(hotel.isActive);

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
    <form onSubmit={handleSubmit} class="box">
      <h2 class="title is-4">Update Hotel</h2>

      <div class="field">
        <label class="label"> Hotel Name </label>
        <div class="control">
          <input class="input" type="text" value={name} onChange={(e) => setName(e.target.value)} required placeholder="Enter hotel name" autoComplete="off" />
        </div>
      </div>

      <div class="field">
        <label class="label"> Hotel Description </label>
        <div class="control">
          <textarea class="textarea" value={description} onChange={(e) => setDescription(e.target.value)} required placeholder="Enter hotel description" autoComplete="off" />
        </div>
      </div>

      <div class="field">
        <div class="control">
          <label class="checkbox">
            <input type="checkbox" checked={isActive} onChange={(e) => setIsActive(e.target.checked)} />
            Is Active
          </label>
        </div>
      </div>

      <div class="field">
        <div class="control">
          <button class="button is-primary">Update Hotel</button>
        </div>
      </div>
    </form>
  );
}

export default UpdateHotel;