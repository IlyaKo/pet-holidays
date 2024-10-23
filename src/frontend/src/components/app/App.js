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
        path: "/about",
        element: <AboutPage />,
      },
    ],
  },
]);

function App() {
  return (
    <div className="App">
      <RouterProvider router={router} />
    </div>
  );
}

export default App;
