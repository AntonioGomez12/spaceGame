import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import GamePage from '@/components/GamePage.vue'

const routes: Array<RouteRecordRaw> = [
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
