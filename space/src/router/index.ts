import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import GamePage from '@/components/GamePage.vue'

const routes = [
  {
    path: '/',
    name: 'HomeView',
    component: HomeView
  },
  {
    path: '/game',
    name: 'GamePage',
    component: GamePage
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
