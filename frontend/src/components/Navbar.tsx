import { useEffect, useState } from "react";
import { Link, NavLink, useNavigate } from "react-router-dom";
import { AUTH_STATE_CHANGED_EVENT, isAuthenticated, logout } from "../services/AuthService";

const Navbar = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(isAuthenticated);
  const [isLoggingOut, setIsLoggingOut] = useState(false);
  const [logoutError, setLogoutError] = useState<string | null>(null);
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const syncAuthState = () => setIsLoggedIn(isAuthenticated());
    window.addEventListener(AUTH_STATE_CHANGED_EVENT, syncAuthState);
    window.addEventListener("storage", syncAuthState);

    return () => {
      window.removeEventListener(AUTH_STATE_CHANGED_EVENT, syncAuthState);
      window.removeEventListener("storage", syncAuthState);
    };
  }, []);

  const handleLogout = async () => {
    setLogoutError(null);
    setIsLoggingOut(true);

    try {
      await logout();
      setIsMenuOpen(false);
      navigate("/auth/login");
    } catch (error) {
      setLogoutError(error instanceof Error ? error.message : "Failed to log out.");
    } finally {
      setIsLoggingOut(false);
    }
  };

  return <>
  <header className="sticky top-0 z-20 border-b border-ink/10 bg-cream/95 backdrop-blur">
    <div className="mx-auto max-w-6xl px-5 sm:px-8">
      <div className="flex h-20 items-center justify-between">
      <Link to="/" className="group flex items-center gap-3" aria-label="MealMate home">
        <span className="grid size-10 place-items-center rounded-xl bg-moss text-xl text-white shadow-sm">M</span>
        <span className="display text-lg font-extrabold tracking-tight">MealMate<span className="text-coral">.</span></span>
      </Link>
      <button type="button" className="grid size-10 place-items-center rounded-lg border border-ink/15 text-ink transition hover:bg-black/5 md:hidden" onClick={() => setIsMenuOpen((open) => !open)} aria-expanded={isMenuOpen} aria-controls="main-navigation" aria-label={isMenuOpen ? "Close navigation menu" : "Open navigation menu"}>
        <span className="text-2xl leading-none" aria-hidden="true">{isMenuOpen ? "×" : "☰"}</span>
      </button>
      </div>
      <nav id="main-navigation" className={`${isMenuOpen ? "flex" : "hidden"} flex-col gap-1 border-t border-ink/10 pb-4 pt-3 text-sm font-semibold text-muted md:flex md:flex-row md:items-center md:border-0 md:py-0`} aria-label="Main navigation">
        <NavLink onClick={() => setIsMenuOpen(false)} to="/restaurants" className={({isActive}) => `rounded-lg px-3 py-2 transition ${isActive ? "bg-sage text-moss-dark" : "hover:bg-black/5 hover:text-ink"}`}>Explore</NavLink>
        <NavLink onClick={() => setIsMenuOpen(false)} to="/restaurants/new" className="rounded-lg bg-coral px-4 py-2.5 text-white shadow-sm transition hover:bg-[#d95b42] md:ml-2">Add restaurant <span className="ml-1 text-base">+</span></NavLink>
        {isLoggedIn ? (
          <button type="button" onClick={handleLogout} disabled={isLoggingOut} className="rounded-lg border border-ink/15 bg-white px-3 py-2 text-left font-bold text-ink transition hover:border-coral hover:text-coral disabled:cursor-not-allowed disabled:opacity-60 md:ml-2 md:text-center">
            {isLoggingOut ? "Logging out..." : "Logout"}
          </button>
        ) : (
          <Link onClick={() => setIsMenuOpen(false)} to="/auth/login" className="rounded-lg px-3 py-2 font-bold text-[#2563EB] transition hover:bg-blue-50 hover:text-[#1D4ED8]">Sign in</Link>
        )}
      </nav>
    </div>
  </header>
  {logoutError && <div className="border-b border-red-200 bg-red-50 px-5 py-2 text-center text-sm text-red-800" role="alert">{logoutError}</div>}
  </>;
};
export default Navbar;
