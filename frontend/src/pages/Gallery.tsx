import { useState } from "react";


type GalleryItem = {
    id: number;
    src: string;
    alt: string;
    category: "Food" | "Drinks" | "Interrior";
}
const galleryItems: GalleryItem[] = [
    {id: 1, src: "/gallery/1.avif", alt: "Lorem", category: "Food"},
    { id: 2, src: "/gallery/2.avif", alt: "Lorem", category: "Drinks"},
    {id: 3, src: "/gallery/3.avif", alt: "Lorem", category: "Food"},
    { id: 4, src: "/gallery/4.avif", alt: "Lorem", category: "Drinks"},
    {id: 5, src: "/gallery/5.avif", alt: "Lorem", category: "Food"},
    { id: 6, src: "/gallery/6.avif", alt: "Lorem", category: "Interrior"},
    {id: 7, src: "/gallery/7.avif", alt: "Lorem", category: "Food"},
    { id: 8, src: "/gallery/8.avif", alt: "Lorem", category: "Interrior"},
    { id: 9, src: "/gallery/9.avif", alt: "Lorem", category: "Drinks"},
    {id: 10, src: "/gallery/10.avif", alt: "Lorem", category: "Food"},
    { id: 11, src: "/gallery/11.avif", alt: "Lorem", category: "Drinks"},
    { id: 12, src: "/gallery/12.avif", alt: "Lorem", category: "Interrior"},
    {id: 13, src: "/gallery/13.avif", alt: "Lorem", category: "Food"},
    { id: 14, src: "/gallery/14.avif", alt: "Lorem", category: "Interrior"},
    {id: 15, src: "/gallery/15.avif", alt: "Lorem", category: "Food"},
    { id: 16, src: "/gallery/16.avif", alt: "Lorem", category: "Interrior"},
    {id: 17, src: "/gallery/17.avif", alt: "Lorem", category: "Food"},
    {id: 18, src: "/gallery/18.avif", alt: "Lorem", category: "Drinks"},
    {id: 19, src: "/gallery/19.avif", alt: "Lorem", category: "Interrior"},
    {id: 20, src: "/gallery/20.avif", alt: "Lorem", category: "Food"},
]
const filters = ['All', 'Food', 'Drinks', 'Interior'] as const
const Gallery = () => {
    const [activeFilter, setActiveFilter] = useState<(typeof filters)[number]>("All");
    const [selectedImage, setSelectedImage] = useState<GalleryItem | null>(null)
    const filteredItems =
        activeFilter === 'All' ? galleryItems : galleryItems.filter((item) => item.category === activeFilter.toLowerCase());
  return (
      <section className="relative min-h-screen bg-meal-background">
          <div className="mx-auto max-w-7xl px-6 pt-24 pb-16 lg:px-8">
              <h1 className="text-center text-4xl font-bold lg:text-5xl">Gallery</h1>
              <p className="mt-3 text-center text-meal-muted">
                  A look at our food, drinks, and space
              </p>

              {/* Filters */}
              <div className="mt-8 flex items-center justify-center gap-3">
                  {filters.map((filter) => (
                      <button
                          key={filter}
                          onClick={() => setActiveFilter(filter)}
                          className={`rounded-full border px-6 py-1.5 text-sm transition duration-200 ${activeFilter === filter
                                  ? 'border-meal-primary bg-meal-primary text-white'
                                  : 'border-white/20 text-white hover:border-meal-primary hover:bg-meal-primary/20'
                              }`}
                      >
                          {filter}
                      </button>
                  ))}
              </div>

              {/* Grid */}
              <div className="mt-10 grid grid-cols-2 gap-4 lg:grid-cols-4">
                  {filteredItems.map((item) => (
                      <button
                          key={item.id}
                          onClick={() => setSelectedImage(item)}
                          className="group relative aspect-square overflow-hidden rounded-2xl"
                      >
                          <img
                              src={item.src}
                              alt={item.alt}
                              loading="lazy"
                              className="object-cover transition duration-300 group-hover:scale-105"
                              sizes="(max-width: 1024px) 50vw, 25vw"
                          />
                          <div className="absolute inset-0 flex items-end bg-linear-to-t from-black/60 via-transparent to-transparent opacity-0 transition duration-200 group-hover:opacity-100">
                              <p className="p-3 text-left text-sm text-white">{item.alt}</p>
                          </div>
                      </button>
                  ))}
              </div>
          </div>

          {/* Lightbox */}
          {selectedImage && (
              <div
                  onClick={() => setSelectedImage(null)}
                  className="fixed inset-0 z-50 flex items-center justify-center bg-black/90 p-6"
              >
                  <div className="relative h-[80vh] w-full max-w-3xl">
                      <img
                          src={selectedImage.src}
                          alt={selectedImage.alt}
                          className="object-contain"
                      />
                  </div>
              </div>
          )}
      </section>)
}

export default Gallery