import { useState, type FormEvent } from "react";
import PageHeader from "../components/ui/PageHeader";

const ContactPage = () => {
    const [submitted, setSubmitted] = useState(false);

    const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setSubmitted(true);
    };

    return (
        <section className="mx-auto max-w-6xl px-5 py-14 sm:px-8 sm:py-20">
            <PageHeader
                eyebrow="We’re here to help"
                title="Let’s talk"
                description="Have a question about MealMate, a restaurant listing, or your next order? Send us a note and our team will get back to you soon."
            />

            <div className="grid gap-10 lg:grid-cols-[.75fr_1.25fr] lg:gap-16">
                <aside className="space-y-8">
                    <div>
                        <p className="text-xs font-bold uppercase tracking-[.18em] text-coral">Contact details</p>
                        <h2 className="display mt-3 text-2xl font-extrabold">Good food starts with a good conversation.</h2>
                    </div>
                    <div className="space-y-5 text-sm">
                        <div className="border-l-2 border-coral pl-4">
                            <p className="font-bold text-meal-primary">Email</p>
                            <a href="mailto:hello@mealmate.example" className="mt-1 block text-meal-muted hover:text-meal-accent">hello@mealmate.example</a>
                        </div>
                        <div className="border-l-2 border-coral pl-4">
                            <p className="font-bold text-meal-primary">Call us</p>
                            <a href="tel:+2348000000000" className="mt-1 block text-meal-muted hover:text-meal-accent">+234 800 000 0000</a>
                        </div>
                        <div className="border-l-2 border-coral pl-4">
                            <p className="font-bold text-meal-primary">Hours</p>
                            <p className="mt-1 text-meal-muted">Monday - Friday, 9:00 - 17:00</p>
                        </div>
                    </div>
                </aside>

                <form onSubmit={handleSubmit} className="rounded-2xl border border-meal-primary/10 bg-meal-surface p-5 shadow-[0_10px_35px_rgba(23,32,29,.05)] sm:p-8">
                    {submitted ? (
                        <div className="py-10 text-center" role="status">
                            <p className="text-xs font-bold uppercase tracking-[.18em] text-coral">Message received</p>
                            <h2 className="display mt-3 text-3xl font-extrabold text-meal-primary">Thanks for reaching out.</h2>
                            <p className="mx-auto mt-3 max-w-md text-meal-muted">We’ll review your message and be in touch shortly.</p>
                            <button type="button" onClick={() => setSubmitted(false)} className="mt-7 rounded-lg bg-meal-primary px-5 py-3 text-sm font-bold text-white transition hover:bg-meal-primary-light">Send another message</button>
                        </div>
                    ) : (
                        <div className="space-y-5">
                            <div className="grid gap-5 sm:grid-cols-2">
                                <label className="space-y-2 text-sm font-semibold">Name<input name="name" required placeholder="Your name" className="h-12 w-full rounded-lg border border-meal-primary/15 bg-white px-4 font-normal outline-none transition placeholder:text-meal-muted/70 focus:border-meal-accent focus:ring-3 focus:ring-meal-accent/15" /></label>
                                <label className="space-y-2 text-sm font-semibold">Email<input name="email" type="email" required placeholder="you@example.com" className="h-12 w-full rounded-lg border border-meal-primary/15 bg-white px-4 font-normal outline-none transition placeholder:text-meal-muted/70 focus:border-meal-accent focus:ring-3 focus:ring-meal-accent/15" /></label>
                            </div>
                            <label className="block space-y-2 text-sm font-semibold">Subject<input name="subject" required placeholder="How can we help?" className="h-12 w-full rounded-lg border border-meal-primary/15 bg-white px-4 font-normal outline-none transition placeholder:text-meal-muted/70 focus:border-meal-accent focus:ring-3 focus:ring-meal-accent/15" /></label>
                            <label className="block space-y-2 text-sm font-semibold">Message<textarea name="message" required rows={6} placeholder="Tell us a little more..." className="w-full resize-y rounded-lg border border-meal-primary/15 bg-white px-4 py-3 font-normal outline-none transition placeholder:text-meal-muted/70 focus:border-meal-accent focus:ring-3 focus:ring-meal-accent/15" /></label>
                            <button type="submit" className="w-full rounded-lg bg-meal-primary px-5 py-3.5 text-sm font-bold text-white transition hover:bg-meal-primary-light sm:w-auto">Send message</button>
                        </div>
                    )}
                </form>
            </div>
        </section>
    );
};

export default ContactPage;
