import { createApp } from 'vue'
import App from './App.vue'
import BootstrapVue3 from 'bootstrap-vue-3'

// Optional, since every component import their Bootstrap functionality
// the following line is not necessary

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css'
import moment from 'moment'
const app = createApp(App)
app.use(BootstrapVue3)
app.config.globalProperties.$moment =moment
app.mount('#app')
