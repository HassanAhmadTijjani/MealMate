import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import type { Restaurant } from "../types/Restaurant";
import { deleteRestaurant, getRestaurantById } from "../services/RestaurantService";
import FeedbackState from "../components/ui/FeedbackState";

const RestaurantDetailsPage = () => {
    const { id } = useParams<{ id: string }>();
    const [restaurant, setRestaurant] = useState<Restaurant | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const [isDeleting, setIsDeleting] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();
    // Delete restaurant
    const handleDelete = async () => {
        if (!id) return;
        const confirmed = window.confirm(`Are you sure you want to delete ${restaurant?.name}`);
        if (!confirmed) return;
        setIsDeleting(true);
        try {
            await deleteRestaurant(id);
            navigate("/restaurants");
        } catch {
            setError("Failed to delete restaurant.");
            setIsDeleting(false);
        }
    };
    useEffect(() => {
        const load = async () => {
            if (!id) {
                setError("Restaurant id is missing.");
                setIsLoading(false);
                return;
            }
            try {
                setRestaurant(await getRestaurantById(id));
            } catch {
                setError("We couldn't load this restaurant.");
            } finally {
                setIsLoading(false);
            }
        }; load();
    }, [id]);
    if (isLoading) return (
        <section className="mx-auto min-h-[65vh] max-w-6xl px-5 py-20">
            <FeedbackState loading title="Loading restaurant..." />
        </section>
    );
    if (error || !restaurant) return (
        <section className="mx-auto min-h-[65vh] max-w-6xl px-5 py-20">
        <FeedbackState tone={error ? "danger" : "neutral"} title={error ? "Something went wrong" : "Restaurant not found"} description={error || "This restaurant may no longer be available."} action={<Link to="/restaurants" className="font-bold text-moss underline underline-offset-4">Back to restaurants</Link>} />
        </section>
    );
    return (
        <div>
            <section className="bg-moss-dark text-white">
                <div className="mx-auto grid max-w-6xl gap-10 px-5 py-14 sm:px-8 lg:grid-cols-[1.1fr_.9fr] lg:py-20">
                    <div>
                        <Link to="/restaurants" className="mb-8 inline-block text-sm font-bold text-white/65 hover:text-white">← All restaurants</Link>
                        <div className="flex items-center gap-3">
                            <span className={`size-2.5 rounded-full ${restaurant.isOpen ? "bg-emerald-400" : "bg-white/35"}`} />
                            <span className="text-xs font-bold uppercase tracking-[.18em] text-white/65">{restaurant.isOpen ? "Open now" : "Currently closed"}</span>
                        </div>
                        <h1 className="display mt-4 mb-5 text-4xl font-extrabold sm:text-6xl">{restaurant.name}</h1>
                        <div className="flex flex-wrap items-center gap-3" aria-label="Restaurant actions">
                            <Link
                                to={`/restaurants/${restaurant.id}/edit`}
                                className="inline-flex min-h-11 items-center justify-center rounded-lg border border-white bg-white px-5 py-2.5 text-sm font-bold text-ink shadow-sm transition hover:-translate-y-0.5 hover:bg-white/90 hover:shadow-md focus-visible:outline-none focus-visible:ring-3 focus-visible:ring-white/60"
                            >
                                Edit restaurant
                            </Link>
                            <button
                                type="button"
                                onClick={handleDelete}
                                disabled={isDeleting}
                                aria-label={`Delete ${restaurant.name}`}
                                className="inline-flex min-h-11 items-center justify-center rounded-lg border border-red-200/60 bg-red-950/20 px-5 py-2.5 text-sm font-bold text-red-100 transition hover:-translate-y-0.5 hover:border-red-200 hover:bg-red-950/45 focus-visible:outline-none focus-visible:ring-3 focus-visible:ring-red-200/80 disabled:cursor-not-allowed disabled:opacity-60"
                            >
                                {isDeleting ? "Deleting..." : "Delete restaurant"}
                            </button>
                        </div>
                        <p className="mt-6 max-w-xl text-lg leading-relaxed text-white/70">{restaurant.description || "A local restaurant in the MealMate directory."}</p>
                    </div>
                    <div className="overflow-hidden rounded-2xl bg-white/10">{restaurant.logo ? <img src={restaurant.logo} alt={`${restaurant.name} logo`} className="aspect-[4/3] size-full object-cover" /> : <div className="grid aspect-[4/3] place-items-center"><span className="display text-8xl font-extrabold text-white/20">{restaurant.name.charAt(0)}</span>
                    </div>}
                    </div>
                </div>
            </section>
            <section className="mx-auto max-w-6xl px-5 py-14 sm:px-8">
                <div className="grid gap-8 md:grid-cols-3">
                    <div className="border-l-2 border-coral pl-5">
                        <p className="text-xs font-bold uppercase tracking-wider text-muted">Visit</p>
                        <p className="mt-2 font-semibold leading-relaxed">{restaurant.address}</p>
                    </div>
                    <div className="border-l-2 border-coral pl-5">
                        <p className="text-xs font-bold uppercase tracking-wider text-muted">Call</p>
                        <a href={`tel:${restaurant.phoneNumber}`} className="mt-2 block font-semibold hover:text-moss">{restaurant.phoneNumber}</a>
                    </div>
                    <div className="border-l-2 border-coral pl-5">
                        <p className="text-xs font-bold uppercase tracking-wider text-muted">Email</p>
                        <a href={`mailto:${restaurant.email}`} className="mt-2 block break-all font-semibold hover:text-moss">{restaurant.email}</a>
                    </div>
                </div>
            </section>
        </div>);
};
export default RestaurantDetailsPage;
