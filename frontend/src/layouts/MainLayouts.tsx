import { Outlet } from "react-router-dom";
import Footer from "../components/layout/Footer";
import Navbar from "../components/layout/Navbar";

const MainLayouts = () => (
  <div className="min-h-screen bg-meal-background text-meal-text">
    <Navbar />

    <main>
      <Outlet />
    </main>

    <Footer />
  </div>
);

export default MainLayouts;