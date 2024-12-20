import { React, useState } from "react";
import axios from "axios";
import { API_URL } from "../../../config";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage("");
    setSuccessMessage("");
    try {
      const response = await axios.post(API_URL + "users/login", {
        Email: email,
        Password: password,
      });
      const token = response.data.jwt;
      console.log("Token: ", token);
      setSuccessMessage("Token: " + token);
      // Store the token or update the UI as needed
    } catch (error) {
      setErrorMessage("Error: " + error.message);
      console.error("Error fetching token: ", error);
    }
  };

  return (
    <div className="m-4">
      <form onSubmit={handleSubmit}>
        <div className="field">
          <label className="label is-normal">Email: </label>
          <div className="body">
            <input
              className="input"
              type="text"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Email"
            />
          </div>
        </div>

        <div className="field">
          <label className="label">Password: </label>
          <div className="body">
            <input
              className="input"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Password"
            />
          </div>
        </div>

        {successMessage && (
          <div className="notification is-success">{successMessage}</div>
        )}
        {errorMessage && (
          <div className="notification is-danger">{errorMessage}</div>
        )}

        <button className="button is-link" type="submit">
          Login
        </button>
      </form>
    </div>
  );
}
