import { Link } from 'react-router-dom'

const storyHighlights = [
  { value: '01', title: 'Made for real life', description: 'From a quick lunch to a long-overdue dinner, MealMate helps you find food that fits the moment.' },
  { value: '02', title: 'Local by design', description: 'We bring nearby dishes into one simple place so every order supports the people behind the food.' },
  { value: '03', title: 'Simple from start to finish', description: 'Choose a location, pick your meal, and enjoy a smoother way to make your next meal happen.' },
]

const Story = () => (
  <section id='#about' className="bg-meal-surface px-6 py-20 sm:py-24 lg:px-8" aria-labelledby="story-heading">
    <div className="mx-auto max-w-6xl">
      <div className="grid gap-12 lg:grid-cols-[0.85fr_1.15fr] lg:items-start lg:gap-20">
        <div>
          <p className="text-sm font-semibold uppercase tracking-[0.24em] text-meal-accent">Why MealMate</p>
          <h2 id="story-heading" className="mt-4 max-w-md text-4xl font-bold leading-tight text-meal-primary sm:text-5xl">Good food should feel easy to find.</h2>
          <p className="mt-6 max-w-lg text-base leading-8 text-meal-muted">MealMate started with a simple idea: discovering a great meal should be as enjoyable as eating it. We are building a warmer, more thoughtful way to connect people with the better taste around them.</p>
          <Link to="/menu" className="mt-8 inline-flex items-center rounded-full bg-meal-primary px-6 py-3 text-sm font-semibold text-meal-surface transition hover:bg-meal-primary-light">Meet our meals <span className="ml-2" aria-hidden="true">→</span></Link>
        </div>
        <div className="divide-y divide-meal-primary/10 border-y border-meal-primary/10">
          {storyHighlights.map((highlight) => (
            <article key={highlight.value} className="grid gap-4 py-7 sm:grid-cols-[5rem_1fr] sm:gap-7">
              <p className="text-3xl font-bold text-meal-gold" aria-hidden="true">{highlight.value}</p>
              <div><h3 className="text-xl font-bold text-meal-primary">{highlight.title}</h3><p className="mt-2 max-w-xl leading-7 text-meal-muted">{highlight.description}</p></div>
            </article>
          ))}
        </div>
      </div>
      <div className="mt-16 grid gap-6 border-t border-meal-primary/10 pt-8 sm:grid-cols-3">
        <div><p className="text-3xl font-bold text-meal-primary">50+</p><p className="mt-1 text-sm text-meal-muted">local food spots to discover</p></div>
        <div><p className="text-3xl font-bold text-meal-primary">3 steps</p><p className="mt-1 text-sm text-meal-muted">from craving to table</p></div>
        <div><p className="text-3xl font-bold text-meal-primary">1 place</p><p className="mt-1 text-sm text-meal-muted">for your next good moment</p></div>
      </div>
    </div>
  </section>
)

export default Story
