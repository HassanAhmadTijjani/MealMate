import React from 'react'
import FoodViewer from '../FoodViewer'
import { Link } from 'react-router-dom'

const CTA = () => {
  return (
      <section className='bg-meal-gold '>
          <div className='mx-auto max-w-6xl px-6 py-8 lg:px-8 '>
              <div className='px-4 py-4 text-meal-text font-bold'>
                  <h1 className='text-5xl'>How it works</h1>
                  <p className='text-sm'>made for your moment.</p>
              </div>
              <div className='mt-4 px-4'>
                  <h3 className='underline font-bold uppercase'>Explore our menu</h3>
                  <div className='grid lg:grid-cols-3'>
                      <div className="px-4 py-6 flex items-center justify-center flex-col">
                          <img src="/models/layer-1.svg" alt="" />
                          <h1 className="font-bold text-2xl p-2 ">Select nearest location</h1>
                          <p>Select the restaurant closest to your pick-up/delivery location.</p>
                     </div>
                      <div className="px-4 py-6 flex items-center justify-center flex-col">
                          <img src="/models/layer-2.svg" alt="" />
                          <h1 className="font-bold text-2xl p-2 ">Choose your meal</h1>
                          <p>Place your order by choosing from numerous delicacies on our menu.</p>
                     </div>
                      <div className="px-4 py-6 flex items-center justify-center flex-col">
                          <img src="/models/layer-3.svg" alt="" />
                          <h1 className="font-bold text-2xl p-2 ">Enjoy your meal</h1>
                          <p>Pick up your tasty meal in-store or have it delivered to your doorstep.</p>
                     </div>
                  </div>
                  <div className='mt-8 mx-auto w-36 border-white animate-bounce transition duration-200 rounded-md shadow-lg bg-meal-accent px-4 py-2 hover:bg-meal-accent/50 text-center cursor-pointer'>
                      <Link to="/menu" className='text-center text-white font-bolde'>Order now</Link>
                  </div>
              </div>
          </div>
    </section>
  )
}

export default CTA