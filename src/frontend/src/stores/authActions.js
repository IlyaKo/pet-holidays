import axios from "axios";
import { createAsyncThunk } from "@reduxjs/toolkit";
import { API_URL } from "../config";

export const registerUser = createAsyncThunk(
  "auth/register",
  async ({ username, email, phoneNumber, password }, { rejectWithValue }) => {
    try {
      await axios.post(API_URL + "users", {
        UserName: username,
        Email: email,
        PhoneNumber: phoneNumber,
        Password: password,
      });
    } catch (error) {
      // TODO: handle error response from backend
      return rejectWithValue(error.message);
    }
  }
);

export const userLogin = createAsyncThunk(
  "auth/login",
  async ({ email, password }, { rejectWithValue }) => {
    try {
      const { data } = await axios.post(API_URL + "users/login", {
        email,
        password,
      });
      // store user's token in local storage
      localStorage.setItem("token", data.jwt);
      return data;
    } catch (error) {
      // TODO: handle error response from backend
      return rejectWithValue(error.message);
    }
  }
);
