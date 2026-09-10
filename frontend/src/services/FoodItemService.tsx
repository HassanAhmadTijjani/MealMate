import type { CreateFoodItem, FoodItem } from "../types/FoodItem";
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

// Get FoodItems
export async function getFoodItems(): Promise<FoodItem[]>{
    const response = await fetch(`${API_BASE_URL}/api/food`, {
        credentials: "include",
    });
    if (!response.ok) throw new Error("Failed to load menu 🖕🏼");
    return response.json();
}

// Create FoodItem
export async function createFoodItem(request: CreateFoodItem): Promise<FoodItem>{
    const csrfToken = getCsrfToken();
    if (!csrfToken) {
        throw new Error("CSRF token not found");
    }
    const response = await fetch(`${API_BASE_URL}/api/food`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify(request),
    });
    if (!response.ok) throw new Error("Failed to create Food 🖕🏼");
    return response.json();
}

// Get single foodItem
export async function getFoodItemById(id:string) {
    const response = await fetch(`${API_BASE_URL}/api/food/${id}`, {
        credentials: "include",
    });
    if (!response.ok) throw new Error("Failed to get Item 🖕🏼");
    return response.json();
}

// Edit
export async function updateFoodItem(
    id: string,
    request: {
        name: string;
        description: string | null;
        price: number;
        image: string | null;
        isAvailable: boolean;
    }
): Promise<void> {
    const csrfToken = getCsrfToken();
    if (!csrfToken) {
        throw new Error("CSRF token not found");
    }
    const response = await fetch(`${API_BASE_URL}/api/food/${id}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify(request),
    });
    if (!response.ok) throw new Error("Failed to update Item 🖕🏼");
}

// Delete
export async function deleteFoodItem(id:string): Promise<void> {
    const csrfToken = getCsrfToken();
    if (!csrfToken) {
        throw new Error("CSRF token not found");
    }
    const response = await fetch(`${API_BASE_URL}/api/food/${id}`, {
        method: "DELETE",
        headers: {
            "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
    });
    if (!response.ok) throw new Error("Failed to delete Item 🖕🏼.");

}