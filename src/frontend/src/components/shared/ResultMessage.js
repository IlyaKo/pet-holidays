import React from "react";

export default function ResultMessage({ successMessage, errorMessage }) {
  return (
    <>
      {successMessage && (
        <div className="notification is-success">{successMessage}</div>
      )}
      {errorMessage && (
        <div className="notification is-danger">{errorMessage}</div>
      )}
    </>
  );
}
