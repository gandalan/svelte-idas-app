<script>
    import { IDAS } from '@gandalan/weblibs';
    
    let redirect = '';

    $: {
        let p = new URLSearchParams(window.location.search);
        const token = p.get('t');
        redirect = p.get('r');

        if (!token || !redirect) {
            redirect = new URL(window.location.href).origin;
        }

        if (token) {
            const idas = new IDAS();
            idas.authorizeWithJwt(token);
        }

        window.location.href = redirect;
    }

</script>

<h1>AUTHENTICATING</h1>

{redirect}