import { Link, Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <>
      <nav className="navbar">
        <div className="navbar-brand ">
          <Link to="/" className="title m-2">
            Pet Holidays
          </Link>
        </div>
        <div className="navbar-start">
          <Link className="navbar-item" to="/hotels">
            Hotels
          </Link>
          <Link className="navbar-item" to="/about">
            About
          </Link>
        </div>
      </nav>
      <hr />
      <Outlet />
    </>
  );
}
