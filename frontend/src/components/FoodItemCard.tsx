import type { FoodItem } from "../types/FoodItem";

type FoodItemCardProps = {
    foodItem: FoodItem;
};

const FoodItemCard = ({ foodItem }: FoodItemCardProps) => (
    <article className="group flex h-full flex-col overflow-hidden rounded-2xl border border-meal-primary/10 bg-meal-surface shadow-[0_8px_30px_rgba(23,32,29,.04)] transition hover:-translate-y-1 hover:shadow-[0_14px_35px_rgba(23,32,29,.1)]">
        <div className="relative h-48 overflow-hidden bg-meal-primary/10">
            {foodItem.image ? (
                <img
                    src={foodItem.image}
                    alt={foodItem.name}
                    className="size-full object-cover transition duration-500 group-hover:scale-105"
                />
            ) : (
                <div className="flex size-full items-center justify-center text-5xl font-bold text-meal-primary/30" aria-hidden="true">
                    {foodItem.name.charAt(0).toUpperCase()}
                </div>
            )}
            {!foodItem.isAvailable && (
                <span className="absolute left-4 top-4 rounded-lg bg-meal-text/85 px-2.5 py-1 text-xs font-bold uppercase tracking-wide text-white">
                    Unavailable
                </span>
            )}
        </div>

        <div className="flex flex-1 flex-col p-5">
            <div className="flex items-start justify-between gap-4">
                <h2 className="text-lg font-bold leading-tight text-meal-primary">{foodItem.name}</h2>
                <p className="shrink-0 font-bold text-meal-accent">{foodItem.price}</p>
            </div>
            <p className="mt-3 flex-1 text-sm leading-relaxed text-meal-muted">
                {foodItem.description || "A delicious choice from our kitchen."}
            </p>
            <button
                type="button"
                disabled={!foodItem.isAvailable}
                className="mt-5 w-full rounded-lg bg-meal-primary px-4 py-3 text-sm font-bold text-white transition hover:bg-meal-primary-light disabled:cursor-not-allowed disabled:opacity-50"
            >
                {foodItem.isAvailable ? "Add to order" : "Currently unavailable"}
            </button>
        </div>
    </article>
);

export default FoodItemCard;
