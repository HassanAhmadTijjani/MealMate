export type Restaurant = {
    id: string;
    name: string;
    address: string;
    phoneNumber: string;
    email: string;
    description: string | null;
    isOpen: boolean;
    createdAt: string;
    updatedAt: string | null;
    logo: string | null;
};

export type CreateRestaurantRequest = {
    name: string;
    address: string;
    phoneNumber: string;
    email: string;
    description: string | null;
    isOpen: boolean;
    logo: string | null;
}