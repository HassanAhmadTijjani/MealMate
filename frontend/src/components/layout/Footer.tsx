const Footer = () => (
  <footer className="border-t border-meal-primary/10 bg-meal-primary text-white">
    <div className="mx-auto max-w-7xl px-6 py-12 lg:px-8 lg:py-14">
      <div className="grid gap-10 sm:grid-cols-2 lg:grid-cols-[1.5fr_1fr_1fr_1.2fr]">
        <div>
          <a href="/" className="text-2xl font-bold tracking-tight text-white">MealMate</a>
          <p className="mt-4 max-w-xs text-sm leading-6 text-white/65">
            Discover local favourites, share great places, and make every meal worth remembering.
          </p>
          <a href="mailto:hello@mealmate.example" className="mt-5 inline-block text-sm font-semibold text-meal-gold transition hover:text-white">
            hello@mealmate.example
          </a>
        </div>

        <div>
          <h2 className="text-sm font-bold text-white">Explore</h2>
          <nav className="mt-4 flex flex-col items-start gap-3 text-sm text-white/65" aria-label="Explore">
            <a href="/" className="transition hover:text-white">Home</a>
            <a href="/menu" className="transition hover:text-white">Browse restaurants</a>
            <a href="/restaurants/new" className="transition hover:text-white">Add a restaurant</a>
          </nav>
        </div>

        <div>
          <h2 className="text-sm font-bold text-white">Company</h2>
          <nav className="mt-4 flex flex-col items-start gap-3 text-sm text-white/65" aria-label="Company">
            <a href="/contact" className="transition hover:text-white">Contact us</a>
            <a href="/auth/login" className="transition hover:text-white">Sign in</a>
            <a href="/auth/register" className="transition hover:text-white">Create an account</a>
          </nav>
        </div>

        <div>
          <h2 className="text-sm font-bold text-white">Stay in the loop</h2>
          <p className="mt-4 text-sm leading-6 text-white/65">Questions or feedback? We would love to hear from you.</p>
          <a href="/contact" className="mt-4 inline-flex rounded-lg bg-meal-accent px-4 py-2.5 text-sm font-bold text-white transition hover:bg-[#c65d3f]">Get in touch</a>
        </div>
      </div>

      <div className="mt-12 flex flex-col gap-3 border-t border-white/15 pt-6 text-xs text-white/50 sm:flex-row sm:items-center sm:justify-between">
        <p>© 2026 MealMate. All rights reserved.</p>
        <div className="flex gap-5">
          <a href="/" className="transition hover:text-white">Privacy</a>
          <a href="/" className="transition hover:text-white">Terms</a>
        </div>
      </div>
    </div>
  </footer>
);

export default Footer;
