import { React, useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import FormInput from "../../shared/FormInput";
import ResultMessage from "../../shared/ResultMessage";
import { useDispatch, useSelector } from "react-redux";
import { registerUser } from "../../../stores/authActions";
import { useNavigate } from "react-router-dom";

export default function RegisterPage() {
  const formMethods = useForm();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { authenticated, loading, error } = useSelector((state) => state.auth);

  useEffect(() => {
    if (authenticated) {
      navigate("/hotels");
    }
  }, [authenticated, navigate]);

  const onSubmit = async (data) => {
    if (data.password !== data.passwordConfirmation) {
      formMethods.setError("passwordConfirmation", {
        type: "manual",
        message: "Password and password confirmation do not match",
      });
      return;
    }
    dispatch(
      registerUser({
        username: data.username,
        email: data.email,
        phoneNumber: data.phoneNumber,
        password: data.password,
      })
    );
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
            maxLength: { value: 12, message: "Number is too long" },
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

        <ResultMessage errorMessage={error} />

        <button className="button is-link" type="submit" disabled={loading}>
          Sign Up
        </button>
      </form>
    </FormProvider>
  );
}
