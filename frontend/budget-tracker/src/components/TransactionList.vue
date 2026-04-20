<script setup lang="ts">
import { ref, onMounted } from 'vue'

interface Transaction {
  id: string
  date: string
  description: string
  amount: number
  category: string
  accountName: string
}

const transactions = ref<Transaction[]>([])
const loading = ref(true)
const error = ref<string | null>(null)

onMounted(async () => {
  try {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/api/transactions`)
    if (!response.ok) throw new Error('Failed to fetch transactions')
    transactions.value = await response.json()
  } catch (e) {
    error.value = 'Could not load transactions'
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div>
    <h1>Transactions</h1>
    <p v-if="loading">Loading...</p>
    <p v-else-if="error">{{ error }}</p>
    <ul v-else>
      <li v-for="transaction in transactions" :key="transaction.id">
        {{ transaction.date }} - {{ transaction.description }} - {{ transaction.amount }}
      </li>
    </ul>
  </div>
</template>