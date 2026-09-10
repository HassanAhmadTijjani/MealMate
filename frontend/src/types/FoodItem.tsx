export type FoodItem = {
    id: string;
    name: string;
    description: string | null;
    price: string;
    image: string | null;
    isAvailable: boolean;
    createdAt: string;
    updatedAt: string | null;
}

export type CreateFoodItem = {
    name: string;
    description: string | null;
    price: number;
    image: string | null;
    isAvailable: boolean;

}

