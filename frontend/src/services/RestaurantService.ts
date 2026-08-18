import type { Restaurant, CreateRestaurantRequest } from "../types/Restaurant";
const API_URL = "http://localhost:5234/api/restaurants";
export async function getRestaurant(): Promise<Restaurant[]>{
    const response = await fetch(API_URL);
    if (!response.ok) throw new Error("Failed to fetch Restaurants");
    return response.json();
    
}

// Creating Restaurant
export async function createRestaurant(request: CreateRestaurantRequest): Promise<Restaurant>{
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(request),
    });
    if (!response.ok) throw new Error("Failed to create Restaurant.");
    return response.json();
}

export async function getRestaurantById(id: string) {
    const response = await fetch(`${API_URL}/${id}`);
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
    const response = await fetch(`${API_URL}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json", },
        body: JSON.stringify(request),
    });
    if (!response.ok) throw new Error("Failed to update restaurant.");
    // return response.json();
}

// Delete Restaurant
export async function deleteRestaurant(id: string): Promise<void> {
    const response = await fetch(`${API_URL}/${id}`, {
        method: "DELETE",
    });
    if (!response.ok) throw new Error("Failed to delete restaurant.");
}