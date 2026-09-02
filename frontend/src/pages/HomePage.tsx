import { Link } from "react-router-dom";
import hero from "../assets/hero.png";

const HomePage = () => (
  <div>
    <section className="grain relative overflow-hidden bg-moss-dark text-white">
      <div className="mx-auto grid min-h-140 max-w-6xl items-center gap-12 px-5 py-20 sm:px-8 lg:grid-cols-[1.05fr_.95fr] lg:py-24">
        <div className="relative z-10">
          <p className="mb-6 text-sm font-bold uppercase tracking-[.2em] text-[#a9d1bd]">Good food, close to home</p>
          <h1 className="display max-w-xl text-5xl font-extrabold leading-[1.02] sm:text-7xl">Your table is waiting.</h1>
          <p className="mt-7 max-w-md text-lg leading-relaxed text-white/70">Discover independent restaurants, see what`s open, and make tonight delicious.</p>
          <div className="mt-9 flex flex-wrap gap-3">
            <Link to="/restaurants" className="rounded-lg bg-coral px-5 py-3.5 font-bold text-white transition hover:bg-[#d95b42]">Explore restaurants <span className="ml-2">→</span></Link>
            <Link to="/restaurants/new" className="rounded-lg border border-white/25 px-5 py-3.5 font-bold text-white transition hover:bg-white/10">List your restaurant</Link>
          </div>
        </div>
        <div className="relative z-10 overflow-hidden rounded-[2rem] border border-white/15 shadow-2xl">
          <img src={hero} alt="A beautifully plated restaurant meal" className="aspect-[4/3] w-full object-cover" />
          <div className="absolute bottom-4 left-4 rounded-xl bg-white/90 px-4 py-3 text-ink shadow-lg">
            <p className="text-xs font-bold uppercase tracking-wider text-muted">Tonight’s pick</p>
            <p className="display font-bold">Good food. Great company.</p>
          </div>
        </div>
      </div>
    </section>
    <section className="mx-auto max-w-6xl px-5 py-20 sm:px-8"><div className="grid gap-8 sm:grid-cols-3">
      <div>
        <p className="display text-4xl font-extrabold text-coral">01</p>
        <h2 className="mt-3 display text-xl font-bold">Find your flavour</h2>
        <p className="mt-2 text-sm leading-relaxed text-muted">Browse a considered collection of local places worth your time.</p>
      </div>
      <div>
        <p className="display text-4xl font-extrabold text-coral">02</p>
        <h2 className="mt-3 display text-xl font-bold">Know before you go</h2>
        <p className="mt-2 text-sm leading-relaxed text-muted">See opening status, contact details, and location at a glance.</p>
      </div>
      <div>
        <p className="display text-4xl font-extrabold text-coral">03</p>
        <h2 className="mt-3 display text-xl font-bold">Make it yours</h2>
        <p className="mt-2 text-sm leading-relaxed text-muted">Restaurant owners can add their place in under a minute.</p>
      </div>
    </div>
    </section>
  </div>
);
export default HomePage;
