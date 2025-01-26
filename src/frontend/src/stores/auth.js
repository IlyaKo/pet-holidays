import { createSlice } from "@reduxjs/toolkit";
import { registerUser, userLogin } from "./authActions";

const loadStateFromLocalStorage = () => {
  try {
    const token = localStorage.getItem("token");
    const username = localStorage.getItem("username");
    if (token) {
      return {
        authenticated: true,
        token: token,
        name: username,
        loading: false,
        error: null,
      };
    }
  } catch (err) {
    return undefined;
  }
};

const initialState = loadStateFromLocalStorage() || {
  authenticated: false,
  token: null,
  name: null,
  loading: false,
  error: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logout(state) {
      state.authenticated = false;
      localStorage.removeItem("token");
      localStorage.removeItem("username");
      state.token = null;
      state.name = null;
      state.loading = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(registerUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(registerUser.fulfilled, (state, action) => {
        state.loading = false;
        state.authenticated = true;
        state.name = action.payload.name;
        state.token = action.payload.token;
        state.error = null;
      })
      .addCase(registerUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      })
      .addCase(userLogin.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(userLogin.fulfilled, (state, action) => {
        state.loading = false;
        state.authenticated = true;
        state.name = action.payload.name;
        state.token = action.payload.token;
        state.error = null;
      })
      .addCase(userLogin.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      });
  },
});

export const { logout } = authSlice.actions;

export default authSlice.reducer;
