<script type="ts">
	export let icon = '';
	export let href = '';
</script>

<div class="text-center">
	<a class="w-64 mx-auto mt-4 mb-4 py-2 px-4 bg-secondary text-white text-center font-semibold rounded-lg shadow-md border border-secondary hover:border-primary focus:outline-none inline-flex items-center" {href}>
		<span class="material-icons text-primary pr-2">{icon}</span>
		<slot />
	</a>
</div>
