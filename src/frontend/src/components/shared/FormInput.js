import React from "react";
import { useFormContext, Controller } from "react-hook-form";

export default function FormInput({ name, label, rules, ...rest }) {
  const {
    control,
    formState: { errors },
  } = useFormContext();

  return (
    <div className="field">
      <label className="label is-normal">{label}</label>
      <div className="body">
        <Controller
          name={name}
          control={control}
          rules={rules}
          defaultValue=""
          render={({ field }) => (
            <input
              className="input"
              placeholder={rest.placeholder ? rest.placeholder : label}
              {...field}
              {...rest}
            />
          )}
        />
        {errors[name] && (
          <p className="help is-danger">{errors[name]?.message}</p>
        )}
      </div>
    </div>
  );
}
