import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import "./App.css";
import "bulma/css/bulma.min.css";
import Layout from "../layout/Layout";
import HotelsPage from "../pages/hotels-page/HotelsPage";
import HotelPage from "../pages/hotel-page/HotelPage";
import AboutPage from "../pages/about-page/AboutPage";
import NotFoundPage from "../pages/not-found-page/NotFoundPage";
import LoginPage from "../pages/login-page/LoginPage";
import RegisterPage from "../pages/register-page/RegisterPage";
import BookingsPage from "../pages/bookings-page/BookingsPage";
import NewReservationForm from "../new-reservation-form/NewReservationForm";
import PetsPage from "../pages/pets-page/PetsPage"; 

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    errorElement: <NotFoundPage />,
    children: [
      {
        path: "/",
        element: <Navigate to="/about" />,
      },
      {
        path: "/hotels",
        element: <HotelsPage />,
      },
      {
        path: "/hotels/:hotelId",
        element: <HotelPage />,
      },
      {
        path: "/bookings/new",
        element: <NewReservationForm />,
      },
      {
        path: "/bookings",
        element: <BookingsPage />,
      },
      {
        path: "/my-pets", 
        element: <PetsPage />,
      },
      {
        path: "/about",
        element: <AboutPage />,
      },
      {
        path: "/login",
        element: <LoginPage />,
      },
      {
        path: "/sign-up",
        element: <RegisterPage />,
      },
    ],
  },
]);

export default function App() {
  return (
    <div className="App">
      <RouterProvider router={router} />
    </div>
  );
}
