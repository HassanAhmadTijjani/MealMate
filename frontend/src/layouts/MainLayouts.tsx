import { Outlet } from "react-router-dom";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";

const MainLayouts = () => (
  <div className="min-h-screen bg-cream text-ink">
    <Navbar />
    <main><Outlet /></main>
    <Footer />
  </div>
);

export default MainLayouts;
