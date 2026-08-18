import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import type { Restaurant } from "../types/Restaurant";
import {
    getRestaurantById,
    updateRestaurant,
} from "../services/RestaurantService";
import FeedbackState from "../components/ui/FeedbackState";
import PageHeader from "../components/ui/PageHeader";
import FormField from "../components/ui/FormField";

const EditRestaurantPage = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    const [restaurant, setRestaurant] = useState<Restaurant | null>(null);

    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");
    const [description, setDescription] = useState("");
    const [isOpen, setIsOpen] = useState(false);
    const [logo, setLogo] = useState("");

    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [submitError, setSubmitError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    useEffect(() => {
        async function loadRestaurant() {
            if (!id) {
                setError("Restaurant ID is missing.");
                setIsLoading(false);
                return;
            }

            try {
                const data = await getRestaurantById(id);

                setRestaurant(data);

                setName(data.name);
                setAddress(data.address);
                setPhoneNumber(data.phoneNumber);
                setEmail(data.email);
                setDescription(data.description ?? "");
                setIsOpen(data.isOpen);
                setLogo(data.logo ?? "");
            } catch {
                setError("Failed to load restaurant.");
            } finally {
                setIsLoading(false);
            }
        }

        loadRestaurant();
    }, [id]);

    const handleSubmit = async (
        event: React.FormEvent<HTMLFormElement>
    ) => {
        event.preventDefault();

        if (!id) {
            return;
        }
        setSubmitError(null);
        setIsSubmitting(true);

        const request = {
            name,
            address,
            phoneNumber,
            email,
            description,
            isOpen,
            logo,
        };

        try {
            await updateRestaurant(id, request);
            navigate(`/restaurants/${id}`);
        } catch {
            setSubmitError("Failed to update restaurant. Check the details and try again.")
        } finally {
            setIsSubmitting(false);
        }
    };

    if (isLoading) {
        return (
            <section className="mx-auto min-h-[65vh] max-w-3xl px-5 py-20">
                <FeedbackState loading title="Loading restaurant..." description="Loading the restaurant details please wait." />
            </section>
        );
    }

    if (error || !restaurant) {
        return (
            <section className="mx-auto min-h-[65vh] max-w-6xl px-5 py-20">
                <FeedbackState tone={error ? "danger" : "neutral"} title={error ? "Something went wrong." : "Restaurant not found."} description={error || "Restaurant may not be available."} action={<Link to="/restaurants" className="font-bold text-moss underline underline-offset-4">Back to restaurants</Link>} />
            </section>
        );
    }
    return (
        <section className="mx-auto max-w-3xl px-5 py-14 sm:px-8 sm:py-20">
            <PageHeader eyebrow="Join the directory" title="Update restaurant" action={<Link to={`/restaurants/${restaurant.id}`} className="mb-8 inline-block text-sm font-bold text-black hover:border rounded-md px-3 py-2 " >← Back to restaurant</Link>} />
            <form onSubmit={handleSubmit} className="space-y-8">
                {/* {error && <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-red-800 " role="alert">
                    <p className="font-bold">Restaurant wasn`t updated</p>
                    <p className="mt-1">{error}</p>
                </div>} */}
                <fieldset className="space-y-5">
                    <legend className="display mb-5 text-lg font-bold">Restaurant details</legend>
                    <FormField id="name" label="Restaurant name" placeholder="Restaurant name" value={name} onChange={event => setName(event.target.value)} required />
                    <FormField id="address" label="Restaurant address" placeholder="12 Market street Kano" value={address} onChange={event => setAddress(event.target.value)} required />
                    <div className="grid gap-5 sm:grid-cols-2">
                        <FormField id="phoneNumber" label="Phone number" placeholder="+2348143128855" type="tel"  value={phoneNumber} onChange={event => setPhoneNumber(event.target.value)} required />
                        <FormField id="email" label="Email Address" placeholder="example@gmail.com" type="email"  value={email} onChange={event => setEmail(event.target.value)} required />
                    </div>
                    <FormField id="description" label="Description" hint="A short description displayed on the restaurant page." value={description} onChange={event => setDescription(event.target.value)} multiline />
                    <FormField id="logo"    label="Logo or image URL"   type="url" hint="Optional. Enter a publicly available image URL." value={logo}  onChange={event => setLogo(event.target.value)}/>
                    <div className="flex items-center gap-3">
                        <input
                            id="isOpen"
                            type="checkbox"
                            checked={isOpen}
                            onChange={event => setIsOpen(event.target.checked)}
                        />

                        <label htmlFor="isOpen" className="font-bold">
                            Restaurant is open
                        </label>
                    </div>
                </fieldset>
                {submitError && (
                    <div
                        className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-800"
                        role="alert"
                    >
                        {submitError}
                    </div>
                )}

                <button type="submit" disabled={isSubmitting} className="rounded-lg bg-coral px-5 py-3 text-sm font-bold text-white transition hover:bg-[#d95b42] disabled:cursor-not-allowed disabled:opacity-60">
                    {isSubmitting ? "Updating..." : "Update restaurant"}
                </button>
            </form>
        </section>
    );
};

export default EditRestaurantPage;