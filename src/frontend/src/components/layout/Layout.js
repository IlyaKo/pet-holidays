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
    if (!savedTheme) 
    {
      localStorage.setItem("theme", "light");
      savedTheme = "light";
    }
    return savedTheme === "dark";
  });

  const onLogoutClick = () => {
    dispatch(logout());
  };

  useEffect(() => {
    if (isDark) 
    {
      document.documentElement.classList.add("dark-mode");
      localStorage.setItem("theme", "dark");
    } 
    else 
    {
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
            <NavLink
              className={({ isActive }) =>
                isActive ? "navbar-item is-active" : "navbar-item"
              }
              onClick={onLogoutClick}
              to="/"
            >
              Logout
            </NavLink>
          )}
        </div>
        <div className="navbar-end">
          <button
            onClick={toggleTheme}
            className={`button m-2 is-flex is-align-items-center ${isDark ? "is-light" : "is-dark"}`}style={{ gap: "0.5rem" }}>
            {isDark ? <FaSun /> : <FaMoon />}
          </button>
        </div>
      </nav>
      <hr />
      <Outlet />
    </>
  );
}