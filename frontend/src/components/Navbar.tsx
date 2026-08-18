import { Link, NavLink } from "react-router-dom";

const Navbar = () => (
  <header className="border-b border-ink/10 bg-cream/95 backdrop-blur sticky top-0 z-20">
    <div className="mx-auto flex h-20 max-w-6xl items-center justify-between px-5 sm:px-8">
      <Link to="/" className="group flex items-center gap-3" aria-label="MealMate home">
        <span className="grid size-10 place-items-center rounded-xl bg-moss text-xl text-white shadow-sm">M</span>
        <span className="display text-lg font-extrabold tracking-tight">MealMate<span className="text-coral">.</span></span>
      </Link>
      <nav className="flex items-center gap-1 text-sm font-semibold text-muted" aria-label="Main navigation">
        <NavLink to="/restaurants" className={({isActive}) => `rounded-lg px-3 py-2 transition ${isActive ? "bg-sage text-moss-dark" : "hover:bg-black/5 hover:text-ink"}`}>Explore</NavLink>
        <NavLink to="/restaurants/new" className="ml-2 rounded-lg bg-coral px-4 py-2.5 text-white shadow-sm transition hover:bg-[#d95b42]">Add restaurant <span className="ml-1 text-base">+</span></NavLink>
      </nav>
    </div>
  </header>
);
export default Navbar;
