import { React, useState } from "react";
import axios from "axios";
import { API_URL } from "../../../config";
import { FormProvider, useForm } from "react-hook-form";
import FormInput from "../../shared/FormInput";

export default function RegisterPage() {
  const formMethods = useForm();
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
    <FormProvider {...formMethods}>
      <form className="m-4" onSubmit={formMethods.handleSubmit(onSubmit)}>
        <FormInput
          name="username"
          label="Username"
          rules={{
            required: "Username is required",
            minLength: { value: 2, message: "Username is too short" },
            maxLength: { value: 200, message: "Username is too long" },
          }}
        />

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
          name="phoneNumber"
          label="Phone number"
          type="tel"
          rules={{
            required: "Phone number is required",
            maxLength: { value: 12, message: "Username is too long" },
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

        <FormInput
          name="passwordConfirmation"
          label="Password confirmation"
          type="password"
          rules={{ required: "Password confirmation is required" }}
        />

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
    </FormProvider>
  );
}
