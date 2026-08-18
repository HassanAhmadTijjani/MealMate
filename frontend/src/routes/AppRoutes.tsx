import { Routes, Route } from "react-router-dom";
import HomePage from "../pages/HomePage";
import RestaurantsPage from "../pages/RestaurantsPage";
import CreateRestaurantPage from "../pages/CreateRestaurantPage";
import MainLayouts from "../layouts/MainLayouts";
import RestaurantDetailsPage from "../pages/RestaurantDetailsPage";
import EditRestaurantPage from "../pages/EditRestaurantPage";

const AppRoutes = () => {
    return (
        <Routes>
            <Route element={<MainLayouts />} >
            <Route path="/" element={<HomePage />} />
            <Route path="/restaurants"  element={<RestaurantsPage />} />
            <Route path="/restaurants/new" element={<CreateRestaurantPage />} />
            <Route path="/restaurants/:id" element={<RestaurantDetailsPage />} />
            <Route path="/restaurants/:id/edit" element={<EditRestaurantPage />} />
            </Route>
        </Routes>
    );
};

export default AppRoutes;