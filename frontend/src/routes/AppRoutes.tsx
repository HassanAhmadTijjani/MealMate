import { Routes, Route } from "react-router-dom";
import HomePage from "../pages/HomePage";
import MenuPage from "../pages/MenuPage";
// import CreateRestaurantPage from "../pages/CreateRestaurantPage";
import MainLayouts from "../layouts/MainLayouts";
// import RestaurantDetailsPage from "../pages/RestaurantDetailsPage";
// import EditRestaurantPage from "../pages/EditRestaurantPage";
import LoginPage from "../pages/LoginPage";
import SecurityLabPage from "../pages/SecurityLabPage";
import RegisterPage from "../pages/RegisterPage";
import ContactPage from "../pages/ContactPage";

const AppRoutes = () => {
    return (
        <Routes>
            <Route element={<MainLayouts />} >
            <Route path="/" element={<HomePage />} />
            <Route path="/menu"  element={<MenuPage />} />
            {/* <Route path="/restaurants/new" element={<CreateRestaurantPage />} /> */}
            {/* <Route path="/restaurants/:id" element={<RestaurantDetailsPage />} /> */}
            {/* <Route path="/restaurants/:id/edit" element={<EditRestaurantPage />} /> */}
            <Route path="/auth/login" element={<LoginPage />} />
            <Route path="/auth/register" element={<RegisterPage />} />
            <Route path="/lab" element={<SecurityLabPage />} />
            <Route path="/contact" element={<ContactPage />} />

            </Route>
        </Routes>
    );
};

export default AppRoutes;
