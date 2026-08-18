import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import RestaurantCard from "../components/RestaurantCard";
import FeedbackState from "../components/ui/FeedbackState";
import PageHeader from "../components/ui/PageHeader";
import type { Restaurant } from "../types/Restaurant";
import { getRestaurant } from "../services/RestaurantService";

const RestaurantsPage = () => {
  const [restaurants, setRestaurants] = useState<Restaurant[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
    async function loadRestaurants() {
        setIsLoading(true);
        setError(null);
        try {
            setRestaurants(await getRestaurant());
        } catch {
            setError("We couldn’t reach the restaurant directory.");
        } finally {
            setIsLoading(false);
        }
    }
    useEffect(() => {
        let cancelled = false;
        getRestaurant().then(data => {
            if (!cancelled) {
                setRestaurants(data);
                setIsLoading(false);
            }
        }).catch(() => {
            if (!cancelled) {
                setError("We couldn`t reach the restaurant directory.");
                setIsLoading(false);
            }
        });
        return () => {
            cancelled = true;
        };
    }, []);

    return (
        <section className="mx-auto min-h-[65vh] max-w-6xl px-5 py-14 sm:px-8 sm:py-20">
        <PageHeader eyebrow="The directory" title="Places worth a visit" description="Discover local kitchens, neighbourhood favourites, and your next go-to table." action={<Link to="/restaurants/new" className="inline-block rounded-lg bg-moss px-4 py-3 text-sm font-bold text-white hover:bg-moss-dark">Add restaurant +</Link>} />
    {isLoading && <FeedbackState loading title="Setting the table..." description="Loading restaurants near you." />}
    {error && <FeedbackState tone="danger" title="Couldn’t load restaurants" description={error} action={<button onClick={loadRestaurants} className="rounded-lg bg-red-700 px-4 py-2.5 text-sm font-bold text-white">Try again</button>} />}
    {!isLoading && !error && restaurants.length === 0 && <FeedbackState title="No restaurants yet" description="Be the first to add a restaurant to the MealMate directory." action={<Link to="/restaurants/new" className="font-bold text-moss underline underline-offset-4">Add the first restaurant</Link>} />}
    {!isLoading && !error && restaurants.length > 0 && <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">{restaurants.map(restaurant => <RestaurantCard key={restaurant.id} restaurant={restaurant} />)}</div>}
  </section>
  );
};
export default RestaurantsPage;
