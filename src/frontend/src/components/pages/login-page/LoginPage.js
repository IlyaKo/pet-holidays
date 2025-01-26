import { React, useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import FormInput from "../../shared/FormInput";
import ResultMessage from "../../shared/ResultMessage";
import { userLogin } from "../../../stores/authActions";
import { useDispatch, useSelector } from "react-redux";

export default function LoginPage() {
  const formMethods = useForm();
  const dispatch = useDispatch();
  const { token, loading, error, authenticated } = useSelector(
    (state) => state.auth
  );

  const onSubmit = (data) => {
    dispatch(userLogin({ email: data.email, password: data.password }));
  };

  useEffect(() => {
    if (authenticated) {
      // Redirect or perform any other action after successful login
      console.log("User authenticated");
    }
  }, [authenticated]);

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

        <ResultMessage errorMessage={error} successMessage={token} />

        <button className="button is-link" type="submit">
          Login
        </button>
      </form>
    </FormProvider>
  );
}
