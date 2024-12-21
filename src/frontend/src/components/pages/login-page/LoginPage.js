import { React, useState } from "react";
import axios from "axios";
import { API_URL } from "../../../config";
import { useForm } from "react-hook-form";

export default function LoginPage() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const onSubmit = async (data) => {
    setErrorMessage("");
    setSuccessMessage("");
    try {
      const response = await axios.post(API_URL + "users/login", {
        Email: data.email,
        Password: data.password,
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
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="field">
          <label className="label is-normal">Email: </label>
          <div className="body">
            <input
              className="input"
              type="text"
              {...register("email", { required: true })}
              placeholder="Email"
            />
            {errors.email && (
              <p className="help is-danger">Field is required</p>
            )}
          </div>
        </div>

        <div className="field">
          <label className="label">Password: </label>
          <div className="body">
            <input
              className="input"
              type="password"
              {...register("password", { required: true })}
              placeholder="Password"
            />
            {errors.password && (
              <p className="help is-danger">Field is required</p>
            )}
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
