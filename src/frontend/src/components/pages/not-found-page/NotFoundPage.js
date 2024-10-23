import React from "react";
import { Link } from "react-router-dom";

export default function NotFoundPage() {
  return (
    <>
      <h2 className="title m-4">Not found</h2>
      <hr />
      <Link to="/">Home page</Link>
    </>
  );
}
