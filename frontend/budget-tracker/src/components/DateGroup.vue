<script setup lang="ts">
import TransactionRow from './TransactionRow.vue'

export interface DateGroupTransaction {
  id: string
  description: string
  category: string
  amount: number
  type: 'expense' | 'income'
}

defineProps<{
  dateLabel: string
  transactions: DateGroupTransaction[]
}>()

const emit = defineEmits<{
  selectTransaction: [id: string]
}>()
</script>

<template>
  <section class="w-full">
    <h2 class="mb-1 text-[20px] font-semibold leading-none text-black">{{ dateLabel }}</h2>
    <div class="overflow-hidden rounded-[10px] border border-[#b3a8a8] bg-white">
      <TransactionRow
        v-for="(transaction, index) in transactions"
        :key="transaction.id"
        :description="transaction.description"
        :category="transaction.category"
        :amount="transaction.amount"
        :type="transaction.type"
        :class="{ 'border-t border-[#b3a8a8]': index > 0 }"
        @select="emit('selectTransaction', transaction.id)"
      />
    </div>
  </section>
</template>
