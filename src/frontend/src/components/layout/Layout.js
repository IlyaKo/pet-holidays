import { Link, Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <>
      <nav class="navbar">
        <div class="navbar-brand ">
          <Link to="/" class="title m-2">
            Pet Holidays
          </Link>
        </div>
        <div class="navbar-start">
          <Link class="navbar-item" to="/hotels">
            Hotels
          </Link>
          <Link class="navbar-item" to="/about">
            About
          </Link>
        </div>
      </nav>
      <hr />
      <Outlet />
    </>
  );
}
