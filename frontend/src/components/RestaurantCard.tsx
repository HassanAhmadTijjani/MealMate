import { Link } from "react-router-dom";
import type { Restaurant } from "../types/Restaurant";
type RestaurantCardProps = { restaurant: Restaurant };

const RestaurantCard = ({ restaurant }: RestaurantCardProps) => (
  <article className="group flex h-full flex-col overflow-hidden rounded-2xl border border-ink/10 bg-white shadow-[0_8px_30px_rgba(23,32,29,.04)] transition hover:-translate-y-1 hover:shadow-[0_14px_35px_rgba(23,32,29,.1)]">
    <div className="relative flex h-40 items-end overflow-hidden bg-moss p-5">
      {restaurant.logo ? <img src={restaurant.logo} alt="" className="absolute inset-0 size-full object-cover opacity-75 transition duration-500 group-hover:scale-105" /> : <div className="absolute -right-5 -top-12 size-44 rounded-full border-[22px] border-white/10" />}
      <div className="relative flex w-full items-center justify-between">
        <span className="rounded-full bg-white/15 px-2.5 py-1 text-xs font-bold uppercase tracking-wider text-white backdrop-blur">{restaurant.isOpen ? "Open now" : "Closed"}</span>
        <span className="text-3xl text-white/80">{restaurant.name.charAt(0)}</span>
      </div>
    </div>
    <div className="flex flex-1 flex-col p-5">
      <div className="mb-2 flex items-start justify-between gap-3">
        <h2 className="display text-lg font-bold leading-tight">{restaurant.name}</h2>
        <span className={`mt-1 size-2 shrink-0 rounded-full ${restaurant.isOpen ? "bg-emerald-500" : "bg-slate-300"}`} />
      </div>
      <p className="mb-3 line-clamp-2 text-sm leading-relaxed text-muted">{restaurant.description || "A welcoming local spot waiting to be discovered."}</p>
      <p className="mt-auto truncate text-xs font-semibold uppercase tracking-wide text-muted">{restaurant.address}</p>
      <Link to={`/restaurants/${restaurant.id}`} className="mt-5 flex items-center justify-between border-t border-ink/10 pt-4 text-sm font-bold text-moss transition group-hover:text-coral">View restaurant <span className="text-lg">→</span>
      </Link>
      
    </div>
  </article>
);
export default RestaurantCard;
