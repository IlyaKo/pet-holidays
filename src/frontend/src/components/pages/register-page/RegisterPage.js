import { React, useState } from "react";
import axios from "axios";
import { API_URL } from "../../../config";
import { useForm } from "react-hook-form";

export default function RegisterPage() {
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

    if (data.password !== data.passwordConfirmation) {
      setErrorMessage("Password and password confirmation do not match");
      return;
    }

    try {
      const response = await axios.post(API_URL + "users", {
        UserName: data.username,
        Email: data.email,
        PhoneNumber: data.phoneNumber,
        Password: data.password,
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
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="field">
          <label className="label is-normal">Username: </label>
          <div className="body">
            <input
              className="input"
              type="text"
              {...register("username", { required: true })}
              placeholder="Username"
            />
            {errors.username && (
              <p className="help is-danger">Field is required</p>
            )}
          </div>
        </div>

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
          <label className="label is-normal">Phone number: </label>
          <div className="body">
            <input
              className="input"
              type="phone"
              {...register("phoneNumber", { required: true })}
              placeholder="Phone number"
            />
            {errors.phoneNumber && (
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

        <div className="field">
          <label className="label">Password confirmation: </label>
          <div className="body">
            <input
              className="input"
              type="password"
              {...register("passwordConfirmation", { required: true })}
              placeholder="Password confirmation"
            />
            {errors.passwordConfirmation && (
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
          Sign Up
        </button>
      </form>
    </div>
  );
}
