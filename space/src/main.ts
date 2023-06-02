import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import SpaceBackground from './components/SpaceBackground.vue';

const app = createApp(App)
app.component('SpaceBackground', SpaceBackground);
app.use(router)

app.mount('#app')
