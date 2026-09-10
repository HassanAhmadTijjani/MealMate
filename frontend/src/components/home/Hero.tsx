import HeroScene from "../../3D/scenes/HeroScene";
import FoodViewer from "../FoodViewer";

const Hero = () => {
    return (
        <section className="relative min-h-screen overflow-hidden bg-meal-background">
            <div className="mx-auto flex min-h-screen max-w-7xl items-center px-6 pt-24 lg:px-8">
                <div className="grid w-full items-center gap-12 lg:grid-cols-2">

                    {/* Hero Content */}
                    <div className="max-w-2xl">
                        <p className="mb-5 text-sm font-semibold uppercase tracking-[0.25em] text-meal-accent">
                            Welcome to MealMate
                        </p>

                        <h1 className="text-5xl font-bold leading-[1.05] tracking-tight text-meal-primary sm:text-6xl lg:text-7xl">
                            Good food.
                            <br />
                            Good moments.
                        </h1>

                        <p className="mt-6 max-w-xl text-lg leading-8 text-meal-muted">
                            Delicious meals, memorable moments, and a dining experience
                            designed around you.
                        </p>

                        <div className="mt-8 flex flex-wrap gap-4">
                            <a
                                href="/menu"
                                className="rounded-full bg-meal-primary px-7 py-3.5 font-semibold text-meal-surface transition hover:bg-meal-primary-light"
                            >
                                Explore Menu
                            </a>

                            <a
                                href="/about"
                                className="rounded-full border border-meal-primary/20 px-7 py-3.5 font-semibold text-meal-primary transition hover:border-meal-primary hover:bg-meal-primary/5"
                            >
                                Our Story
                            </a>
                        </div>
                    </div>

                    {/* 3D Scene will go here */}
                    <div className="relative h-125 lg:h-162.5">
                        <FoodViewer modelUrl="/models/Burger.glb" />
                    </div>
                </div>
            </div>
        </section>
    );
};

export default Hero;