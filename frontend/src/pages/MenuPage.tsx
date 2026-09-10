import { useCallback, useEffect, useState } from "react";
import FoodItemCard from "../components/FoodItemCard";
import FeedbackState from "../components/ui/FeedbackState";
import PageHeader from "../components/ui/PageHeader";
import type { FoodItem } from "../types/FoodItem";
import { getFoodItems } from "../services/FoodItemService";

const MenuPage = () => {
  const [foodItems, setFoodItems] = useState<FoodItem[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

    const loadFoodItems = useCallback(async () => {
        setIsLoading(true);
        setError(null);

        try {
            setFoodItems(await getFoodItems());
        } catch {
            setError("We couldn't load the menu.");
        } finally {
            setIsLoading(false);
        }
    }, []);

    useEffect(() => {
        void loadFoodItems();
    }, [loadFoodItems]);

    return (
        <section className="mx-auto min-h-[65vh] max-w-6xl px-5 py-14 sm:px-8 sm:py-20">
            <PageHeader eyebrow="Our menu" title="Good food, made for your moment" description="Explore our dishes and choose something delicious to enjoy." />
            {isLoading && <FeedbackState loading title="Loading menu..." description="Getting today's dishes ready." />}
            {!isLoading && error && <FeedbackState tone="danger" title="Couldn't load menu" description={error} action={<button onClick={() => void loadFoodItems()} className="rounded-lg bg-red-700 px-4 py-2.5 text-sm font-bold text-white">Try again</button>} />}
            {!isLoading && !error && foodItems.length === 0 && <FeedbackState title="No menu items available" description="Please check back soon." />}
            {!isLoading && !error && foodItems.length > 0 && <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">{foodItems.map(foodItem => <FoodItemCard key={foodItem.id} foodItem={foodItem} />)}</div>}
        </section>
    );
};
export default MenuPage;
