import { createRouter, createWebHistory } from 'vue-router'
import TransactionsView from './views/TransactionsView.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      redirect: '/transactions'
    },
    {
      path: '/transactions',
      name: 'transactions',
      component: TransactionsView
    },
    {
      path: '/transactions/:id',
      name: 'transaction-detail',
      component: TransactionsView
    }
  ]
})

export default router
