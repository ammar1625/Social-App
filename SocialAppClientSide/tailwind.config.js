// tailwind.config.js
module.exports = {
    content: [
      "./src/**/*.{js,jsx,ts,tsx}", // Ensure Tailwind scans all your JSX files
      "./public/index.html",
    ],
    theme: {
      extend: {
        fontFamily: {
          roboto: ['Roboto', 'sans-serif'], // Correct way to add Roboto
        },
      
      },
    },
    plugins: [
      function ({ addUtilities }) {
        addUtilities({
          '.text-shadow-sm': { 'text-shadow': '1px 1px 2px rgba(0, 0, 0, 0.5)' },
          '.text-shadow-md': { 'text-shadow': '4px 4px 6px rgba(0, 0, 0, 0.5)' },
          '.text-shadow-lg': { 'text-shadow': '3px 3px 6px rgba(0, 0, 0, 0.7)' },
        });
      },
    ],
  }
  