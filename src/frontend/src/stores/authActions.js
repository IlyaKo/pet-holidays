import axios from "axios";
import { createAsyncThunk } from "@reduxjs/toolkit";
import { AUTH_API_URL } from "../config";

export const registerUser = createAsyncThunk(
  "auth/register",
  async ({ username, email, phoneNumber, password }, { rejectWithValue }) => {
    try {
      const { data } = await axios.post(AUTH_API_URL + "auth/register", {
        UserName: username,
        Email: email,
        PhoneNumber: phoneNumber,
        Password: password,
      });
      localStorage.setItem("token", data.token);
      localStorage.setItem("username", data.username);
      return data;
    } catch (error) {
      if (error.response?.data?.detail) {
        return rejectWithValue(error.response.data.detail);
      } else {
        return rejectWithValue(error.message);
      }
    }
  }
);

export const userLogin = createAsyncThunk(
  "auth/login",
  async ({ email, password }, { rejectWithValue }) => {
    try {
      const { data } = await axios.post(AUTH_API_URL + "auth/login", {
        email,
        password,
      });
      localStorage.setItem("token", data.token);
      localStorage.setItem("username", data.username);
      return data;
    } catch (error) {
      // TODO: handle error response from backend
      return rejectWithValue(error.message);
    }
  }
);
