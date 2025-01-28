import { NavLink, Outlet } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../stores/auth";

export default function Layout() {
  const dispatch = useDispatch();
  const { authenticated } = useSelector((state) => state.auth);

  const onLogoutClick = () => {
    dispatch(logout());
  };

  return (
    <>
      <nav className="navbar">
        <div className="navbar-brand ">
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
      </nav>
      <hr />
      <Outlet />
    </>
  );
}
