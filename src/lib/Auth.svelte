<script>
    import { IDAS } from '@gandalan/weblibs';
    import { location, querystring } from 'svelte-spa-router';

    let redirect = '';

    $: {
        let p = new URLSearchParams($querystring);
        const token = p.get('t');
        redirect = p.get('r');

        if (!token || !redirect) {
            redirect = new URL(window.location.href).origin;
        }

        if (token) {
            const idas = new IDAS();
            idas.authorizeWithJwt(token);
        }

        window.location = redirect;
    }

</script>

<h1>AUTHENTICATING</h1>

{redirect}