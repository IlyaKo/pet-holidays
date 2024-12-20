import { React, useState } from "react";
import axios from "axios";
import { API_URL } from "../../../config";

export default function RegisterPage() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordConfirmation, setPasswordConfirmation] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage("");
    setSuccessMessage("");

    if (
      !username ||
      !email ||
      !password ||
      !passwordConfirmation ||
      !phoneNumber
    ) {
      setErrorMessage("All fields are required");
      return;
    }

    if (password !== passwordConfirmation) {
      setErrorMessage("Password and password confirmation do not match");
      return;
    }

    try {
      const response = await axios.post(API_URL + "users", {
        UserName: username,
        Email: email,
        PhoneNumber: phoneNumber,
        Password: password,
      });
      const token = response.data.jwt;
      console.log("Token: ", token);
      setSuccessMessage("User created");
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
          <label className="label is-normal">Username: </label>
          <div className="body">
            <input
              className="input"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              placeholder="Username"
            />
          </div>
        </div>

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
          <label className="label is-normal">Phone number: </label>
          <div className="body">
            <input
              className="input"
              type="phone"
              value={phoneNumber}
              onChange={(e) => setPhoneNumber(e.target.value)}
              placeholder="Phone number"
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

        <div className="field">
          <label className="label">Password confirmation: </label>
          <div className="body">
            <input
              className="input"
              type="password"
              value={passwordConfirmation}
              onChange={(e) => setPasswordConfirmation(e.target.value)}
              placeholder="Password confirmation"
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
          Sign Up
        </button>
      </form>
    </div>
  );
}
