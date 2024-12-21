import { React, useState } from "react";
import axios from "axios";
import { API_URL } from "../../../config";
import { FormProvider, useForm } from "react-hook-form";
import FormInput from "../../shared/FormInput";

export default function LoginPage() {
  const formMethods = useForm();
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
    <FormProvider {...formMethods}>
      <form className="m-4" onSubmit={formMethods.handleSubmit(onSubmit)}>
        <FormInput
          name="email"
          label="Email"
          type="email"
          rules={{
            required: "Email is required",
            pattern: { value: /^\S+@\S+$/i, message: "Invalid email address" },
          }}
        />

        <FormInput
          name="password"
          label="Password"
          type="password"
          rules={{
            required: "Password is required",
            minLength: { value: 6, message: "Password is too short" },
          }}
        />

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
    </FormProvider>
  );
}
