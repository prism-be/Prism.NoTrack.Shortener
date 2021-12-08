const typography = require('@tailwindcss/typography');
const forms = require('@tailwindcss/forms');

const config = {
	mode: 'jit',
	purge: ['./src/**/*.{html,js,svelte,ts}'],

	theme: {
		extend: {
			colors: {
				transparent: 'transparent',
				current: 'current',
				white: '#fff',
				primary: '#31859c',
				secondary: '#d0dde9',
				heading: '#31859c',
				text: '#444444'
			}
		}
	},

	plugins: [forms, typography]
};

module.exports = config;
