import { useState, useEffect, useRef } from "react";
import { NavLink, Outlet } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../stores/auth";
import { FaSun, FaMoon, FaUser } from "react-icons/fa";

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
  const [isProfileMenuOpen, setIsProfileMenuOpen] = useState(false);
  const profileMenuRef = useRef(null);

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

  const handleMouseLeave = () => {
    setIsProfileMenuOpen(false);
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
            <button className="navbar-item" onClick={onLogoutClick}>Logout</button>
          )}
        </div>
        <div className="navbar-end is-flex is-align-items-center">
          {authenticated && (
            <div
              ref={profileMenuRef}
              className={`dropdown ${isProfileMenuOpen ? "is-active" : ""} is-right`}
              onMouseLeave={handleMouseLeave}
            >
              <div className="dropdown-trigger">
                <button
                  className="button is-dark m-2"
                  onClick={() => setIsProfileMenuOpen(!isProfileMenuOpen)}
                >
                  <FaUser />
                </button>
              </div>
              <div className="dropdown-menu" role="menu">
                <div className="dropdown-content">
                  <NavLink className="dropdown-item" to="/my-pets">
                    My pets
                  </NavLink>
                </div>
              </div>
            </div>
          )}
          <button
            onClick={toggleTheme}
            className="button is-dark m-2"
          >
            {isDark ? <FaSun /> : <FaMoon />}
          </button>
        </div>
      </nav>
      <hr />
      <Outlet />
    </>
  );
}