import { API_BASE_URL } from "../config/api";
import type { LoginRequest, RegisterRequest } from "../types/AuthRequest";

const AUTHENTICATED_KEY = "mealmate.authenticated";
export const AUTH_STATE_CHANGED_EVENT = "mealmate:auth-state-changed";

type ErrorResponse = {
    error?: string | string[];
    errors?: string[];
};

async function getErrorMessage(response: Response, fallback: string): Promise<string> {
    try {
        const data = await response.json() as ErrorResponse;
        const errors = data.errors ?? (Array.isArray(data.error) ? data.error : undefined);

        if (errors?.length) return errors.join(" ");
        if (typeof data.error === "string") return data.error;
    } catch {
        // Use the fallback when the API does not return JSON.
    }

    return fallback;
}

function setAuthenticated(value: boolean) {
    if (value) {
        localStorage.setItem(AUTHENTICATED_KEY, "true");
    } else {
        localStorage.removeItem(AUTHENTICATED_KEY);
    }

    window.dispatchEvent(new Event(AUTH_STATE_CHANGED_EVENT));
}

export function isAuthenticated(): boolean {
    return localStorage.getItem(AUTHENTICATED_KEY) === "true";
}

export async function login(request: LoginRequest): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/api/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(request),
    });
    if (!response.ok) {
        throw new Error(await getErrorMessage(response, "Login failed. Please check your details."));
    }

    setAuthenticated(true);
}

export async function register(request: RegisterRequest): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/api/auth/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(request),
    });

    if (!response.ok) {
        throw new Error(await getErrorMessage(response, "Registration failed. Please try again."));
    }
}

export async function logout(): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/api/auth/logout`, {
        method: "POST",
        credentials: "include",
    });
    if (!response.ok) throw new Error(await getErrorMessage(response, "Failed to log out."));

    setAuthenticated(false);
}

export async function getCsrfToken(): Promise<string> {
    const response = await fetch(
        "http://localhost:5234/api/auth/csrf-token",
        {
            credentials: "include",
        }
    );

    if (!response.ok) {
        throw new Error("Failed to get CSRF token");
    }
   
    const data = await response.json();

    return data.token;
}