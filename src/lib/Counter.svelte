<script>
    const awaiter = fetch('/api/counter')
        .then(res => res.json())
        .then(data => {
        console.log(data)
        return data
        })
        .catch(err => {
        console.log(err)
        return err
        });
</script>

{#await awaiter}
    <progress />
{:then a} 
    <p class="mb-12">Sub-API says: {JSON.stringify(a)}</p>
{/await}

