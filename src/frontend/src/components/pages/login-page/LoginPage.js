import { React, useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import FormInput from "../../shared/FormInput";
import ResultMessage from "../../shared/ResultMessage";
import { userLogin } from "../../../stores/authActions";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
  const formMethods = useForm();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { loading, error, authenticated } = useSelector((state) => state.auth);

  const onSubmit = (data) => {
    dispatch(userLogin({ email: data.email, password: data.password }));
  };

  useEffect(() => {
    if (authenticated) {
      navigate("/hotels");
    }
  }, [authenticated, navigate]);

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

        <ResultMessage errorMessage={error} />

        <button className="button is-link" type="submit" disabled={loading}>
          Login
        </button>
      </form>
    </FormProvider>
  );
}
