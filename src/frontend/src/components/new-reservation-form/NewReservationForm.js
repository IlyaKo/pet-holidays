import React from "react";
import { useLocation } from "react-router-dom";

export default function NewReservationForm() {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const hotelId = queryParams.get("hotelId");
  const roomId = queryParams.get("roomId");

  return (
    <>
      New reservation works!
      <div>Hotel id: {hotelId} </div>
      {roomId && <div>Room id: {roomId}</div>}
    </>
  );
}
