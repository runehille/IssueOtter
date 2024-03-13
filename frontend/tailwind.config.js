/* eslint-disable no-undef */
/** @type {import('tailwindcss').Config} */
export default {
  daisyui: {
    themes: ["corporate", "dark"],
  },
  content: ["./index.html",
  "./src/**/*.{js,ts,jsx,tsx}",],
  plugins: [require("daisyui")],
}

