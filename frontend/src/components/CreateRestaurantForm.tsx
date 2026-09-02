import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { createRestaurant } from "../services/RestaurantService";
import FormField from "./ui/FormField";

const CreateRestaurantForm = () => {
  const navigate = useNavigate();
    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");
    const [description, setDescription] = useState("");
    const [isOpen, setIsOpen] = useState(true);
    const [logo, setLogo] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setError(null);
        setIsSubmitting(true);
        try {
            await createRestaurant({
                name, address, phoneNumber, email, description: description || null, isOpen, logo: logo || null
            });
            navigate("/restaurants");
        } catch {
            setError("We couldn`t create this restaurant. Check the details and try again.");
        } finally {
            setIsSubmitting(false);
        }
    };
    return (
        <form onSubmit={handleSubmit} className="space-y-8">
            {error && <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-800" role="alert">
                <p className="font-bold">Restaurant wasn`t created</p>
                <p className="mt-1">{error}</p>
            </div>}
            <fieldset className="space-y-5">
                <legend className="display mb-5 text-lg font-bold">Restaurant details</legend>
                <FormField id="name" label="Restaurant name" placeholder="e.g. The Green Table" value={name} onChange={e => setName(e.target.value)} required />
                <FormField id="description" label="Description" hint="A short introduction shown on the restaurant card." placeholder="What makes this place special?" value={description} onChange={e => setDescription(e.target.value)} multiline />
                <FormField id="address" label="Address" placeholder="12 Market Street, Lagos" value={address} onChange={e => setAddress(e.target.value)} required />
                </fieldset>
    <hr className="border-ink/10" />
            <fieldset className="space-y-5">
                <legend className="display mb-5 text-lg font-bold">Contact & identity</legend>
                <div className="grid gap-5 sm:grid-cols-2">
                    <FormField id="phoneNumber" label="Phone number" type="tel" placeholder="+234 800 000 0000" value={phoneNumber} onChange={e => setPhoneNumber(e.target.value)} required />
                    <FormField id="email" label="Email address" type="email" placeholder="hello@restaurant.com" value={email} onChange={e => setEmail(e.target.value)} required /></div>
                <FormField id="logo" label="Logo or image URL" type="url" hint="Optional. Use a direct, publicly available image URL." placeholder="https://example.com/image.jpg" value={logo} onChange={e => setLogo(e.target.value)} />
            </fieldset>
            <div className="flex items-center justify-between rounded-lg border border-ink/10 bg-sage/60 p-4">
                <div>
                    <label htmlFor="isOpen" className="font-bold">Open for business</label>
                    <p className="mt-1 text-xs text-muted">Customers will see this status in the directory.</p>
                </div>
                <button type="button" role="switch" aria-checked={isOpen} onClick={() => setIsOpen(value => !value)} className={`relative h-7 w-12 rounded-full transition ${isOpen ? "bg-moss" : "bg-ink/20"}`}>
                    <span className={`absolute top-1 size-5 rounded-full bg-white shadow transition ${isOpen ? "left-6" : "left-1"}`} /><span className="sr-only">Toggle restaurant open status</span>
                </button>
            </div>
            <div className="flex flex-col-reverse gap-3 border-t border-ink/10 pt-6 sm:flex-row sm:justify-end">
                <Link to="/restaurants" className="rounded-lg px-5 py-3 text-center text-sm font-bold text-muted hover:bg-black/5">Cancel</Link>
                <button type="submit" disabled={isSubmitting} className="rounded-lg bg-coral px-5 py-3 text-sm font-bold text-white shadow-sm transition hover:bg-[#d95b42] disabled:cursor-not-allowed disabled:opacity-60">{isSubmitting ? "Creating..." : "Create restaurant"}</button>
            </div>
  </form>);
};
export default CreateRestaurantForm;
