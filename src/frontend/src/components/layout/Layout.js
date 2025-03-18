import { useState, useEffect } from "react";
import { NavLink, Outlet } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../stores/auth";
import { FaSun, FaMoon } from "react-icons/fa";

export default function Layout() {
  const dispatch = useDispatch();
  const { authenticated } = useSelector((state) => state.auth);
  const [isDark, setIsDark] = useState(() => {
    let savedTheme = localStorage.getItem("theme");
    if (!savedTheme) {
      localStorage.setItem("theme", "light");
      savedTheme = "light";
    }
    return savedTheme === "dark";
  });

  const onLogoutClick = () => {
    dispatch(logout());
  };

  useEffect(() => {
    if (isDark) {
      document.documentElement.classList.add("dark-mode");
      localStorage.setItem("theme", "dark");
    } else {
      document.documentElement.classList.remove("dark-mode");
      localStorage.setItem("theme", "light");
    }
  }, [isDark]);

  const toggleTheme = () => {
    setIsDark((prev) => !prev);
  };

  return (
    <>
      <nav className="navbar">
        <div className="navbar-brand">
          <NavLink to="/" className="title m-2">
            Pet Holidays
          </NavLink>
        </div>
        <div className="navbar-start">
          <NavLink
            className={({ isActive }) =>
              isActive ? "navbar-item is-active" : "navbar-item"
            }
            to="/hotels"
          >
            Hotels
          </NavLink>
          {authenticated && (
            <NavLink
              className={({ isActive }) =>
                isActive ? "navbar-item is-active" : "navbar-item"
              }
              to="/my-pets"
            >
              My pets
            </NavLink>
          )}
          <NavLink
            className={({ isActive }) =>
              isActive ? "navbar-item is-active" : "navbar-item"
            }
            to="/bookings"
          >
            Bookings
          </NavLink>
          <NavLink
            className={({ isActive }) =>
              isActive ? "navbar-item is-active" : "navbar-item"
            }
            to="/about"
          >
            About
          </NavLink>
          {!authenticated ? (
            <>
              <NavLink
                className={({ isActive }) =>
                  isActive ? "navbar-item is-active" : "navbar-item"
                }
                to="/login"
              >
                Login
              </NavLink>
              <NavLink
                className={({ isActive }) =>
                  isActive ? "navbar-item is-active" : "navbar-item"
                }
                to="/sign-up"
              >
                Sign Up
              </NavLink>
            </>
          ) : (
            <button className="navbar-item" onClick={onLogoutClick}>
              Logout
            </button>
          )}
        </div>
        <div className="navbar-end is-flex is-align-items-center">
          <button onClick={toggleTheme} className="button is-dark m-2">
            {isDark ? <FaSun /> : <FaMoon />}
          </button>
        </div>
      </nav>
      <hr />
      <div className="mx-4">
        <Outlet />
      </div>
    </>
  );
}
