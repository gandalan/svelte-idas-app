import App from './App.svelte';
import './index.css';
import { initIDAS } from './auth';

const settings = await initIDAS('<INSERT YOUR AUTH TOKEN HERE>');

const app = new App({
  target: document.getElementById('app'),
  props : {
    settings
  }
})

export default app
