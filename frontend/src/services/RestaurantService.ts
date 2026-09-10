import type { Restaurant, CreateRestaurantRequest } from "../types/Restaurant";
import { API_BASE_URL } from "../config/api";

function getCsrfToken(): string | null {
    const cookies = document.cookie.split(";");

    for (const cookie of cookies) {
        const [name, value] = cookie.trim().split("=");

        if (name === "csrfToken") {
            return decodeURIComponent(value);
        }
    }

    return null;
}

// const API_BASE_URL = "http://localhost:5234/api/restaurants";
export async function getRestaurant(): Promise<Restaurant[]>{
    // console.log('Base URL:', API_BASE_URL);
    const response = await fetch(`${API_BASE_URL}/api/restaurants`, {
        
        credentials: "include",
    });
    if (!response.ok) throw new Error("Failed to fetch Restaurants");
    return response.json();
}

// Creating Restaurant
export async function createRestaurant(
    request: CreateRestaurantRequest
): Promise<Restaurant> {
    const csrfToken = getCsrfToken();
    if (!csrfToken) {
        throw new Error("CSRF token not found");
    }
    const response = await fetch(`${API_BASE_URL}/api/restaurants`, {
        method: "POST",

        headers: {
            "Content-Type": "application/json",
            "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify(request),
    });
    if (!response.ok) {
        throw new Error("Failed to create Restaurant.");
    }

    return response.json();
}

// Get a single restaurant by ID
export async function getRestaurantById(id: string) {
    const response = await fetch(`${API_BASE_URL}/api/restaurants/${id}`, {
        credentials: "include",
    });
    if (!response.ok) throw new Error("Failed to get restaurant");
    return response.json();
}

// Edit Restaurant
export async function updateRestaurant(
    id: string,
    request: {
        name: string;
        address: string;
        phoneNumber: string;
        email: string;
        description: string;
        isOpen: boolean;
        logo: string;
    }
): Promise<void> {
    const csrfToken = getCsrfToken();
    if (!csrfToken) {
        throw new Error("CSRF token not found");
    }
    const response = await fetch(`${API_BASE_URL}/api/restaurants/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "X-CSRF-TOKEN": csrfToken,
         },
        credentials: "include",
        body: JSON.stringify(request),
    });
    if (!response.ok) throw new Error("Failed to update restaurant.");
    // return response.json();
}

// Delete Restaurant
export async function deleteRestaurant(id: string): Promise<void> {
    const csrfToken = getCsrfToken();
    if (!csrfToken) {
        throw new Error("CSRF token not found");
    }
    const response = await fetch(`${API_BASE_URL}/api/restaurants/${id}`, {
        method: "DELETE",
        headers: {
            "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
    });
    if (!response.ok) throw new Error("Failed to delete restaurant.");
}