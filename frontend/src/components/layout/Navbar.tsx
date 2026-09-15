const Navbar = () => {
  return (
    <header className="sticky top-0 left-0 z-20 w-full bg-amber-50/55 backdrop-blur">
      <nav className="mx-auto flex max-w-7xl items-center justify-between px-6 py-5 lg:px-8 ">

        <a href="/" className="text-2xl font-bold tracking-tight text-meal-primary" > MealMate</a>

        <div className="hidden items-center gap-8 md:flex">
          <a href="/" className="text-sm font-medium text-meal-text transition hover:text-meal-accent" >  Home </a>
          <a href="/gallery" className="text-sm font-medium text-meal-muted transition hover:text-meal-accent" > Gallery</a>
          <a href="/menu" className="text-sm font-medium text-meal-muted transition hover:text-meal-accent" > Menu </a>
          <a href="/contact" className="text-sm font-medium text-meal-muted transition hover:text-meal-accent" >  Contact </a>
        </div>

        <div className="flex gap-4 items-center justify-center">
        <a href="/contact" className="md:hidden hover:bg-amber-50/90 px-2 py-1.5 rounded-lg font-bold underline  transition duration-300">Contact</a>
        <a href="/menu" className="rounded-full bg-meal-primary px-5 py-2.5 text-sm font-semibold text-meal-surface transition hover:bg-meal-primary-light" >  Order Now </a>
        </div>

      </nav>
    </header>
  );
};

export default Navbar;
