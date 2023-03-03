import { createRouter, createWebHistory } from 'vue-router'
import Transactions from '../views/Transactions.vue'
import AddTransaction from '../views/AddTransaction.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: Transactions
  },
  {
    path: '/add',
    name: 'add',
    component: AddTransaction
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
