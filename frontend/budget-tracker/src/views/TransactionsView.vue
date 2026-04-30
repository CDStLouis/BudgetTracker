<script setup lang="ts">
import { computed, ref } from 'vue'
import MonthSelector from '../components/MonthSelector.vue'
import TransactionList from '../components/TransactionList.vue'
import ViewToggle from '../components/ViewToggle.vue'

interface TransactionGroup {
  id: string
  dateLabel: string
  transactions: {
    id: string
    description: string
    category: string
    amount: number
    type: 'expense' | 'income'
  }[]
}

const activeView = ref<'table' | 'graph'>('table')
const displayedMonth = ref(new Date(2026, 0, 1))

const currentMonthStart = new Date()
currentMonthStart.setDate(1)
currentMonthStart.setHours(0, 0, 0, 0)

const isCurrentMonth = computed(
  () =>
    displayedMonth.value.getMonth() === currentMonthStart.getMonth() &&
    displayedMonth.value.getFullYear() === currentMonthStart.getFullYear()
)

const monthLabel = computed(() =>
  displayedMonth.value.toLocaleDateString('en-GB', { month: 'short', year: 'numeric' })
)

const spendingTotal = computed(() =>
  transactionGroups.value
    .flatMap((group) => group.transactions)
    .filter((tx) => tx.type === 'expense')
    .reduce((acc, tx) => acc + tx.amount, 0)
)

const spendingLabel = computed(() =>
  spendingTotal.value.toLocaleString('en-GB', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
)

const transactionGroups = ref<TransactionGroup[]>([
  {
    id: 'wednesday-14th',
    dateLabel: 'Wednesday 14th',
    transactions: [
      { id: 'aldi', description: 'Aldi', category: 'Groceries', amount: 40.39, type: 'expense' },
      {
        id: 'netflix',
        description: 'Netflix',
        category: 'Entertainment',
        amount: 15.99,
        type: 'expense'
      }
    ]
  },
  {
    id: 'monday-12th',
    dateLabel: 'Monday 12th',
    transactions: [
      {
        id: 'gym',
        description: 'Gym',
        category: 'Health & Fitness',
        amount: 20,
        type: 'expense'
      },
      { id: 'salary', description: 'Salary', category: 'Income', amount: 3000, type: 'income' }
    ]
  },
  {
    id: 'friday-9th',
    dateLabel: 'Friday 9th',
    transactions: [
      { id: 'nandos', description: "Nando's", category: 'Eating out', amount: 25, type: 'expense' },
      {
        id: 'trainline',
        description: 'Trainline',
        category: 'Transport',
        amount: 8.99,
        type: 'expense'
      }
    ]
  }
])

const goToPreviousMonth = () => {
  displayedMonth.value = new Date(displayedMonth.value.getFullYear(), displayedMonth.value.getMonth() - 1, 1)
}

const goToNextMonth = () => {
  if (isCurrentMonth.value) return
  displayedMonth.value = new Date(displayedMonth.value.getFullYear(), displayedMonth.value.getMonth() + 1, 1)
}

const onSelectTransaction = (id: string) => {
  // Placeholder for future Vue Router navigation to a transaction detail screen.
  console.log(`Selected transaction: ${id}`)
}

const onToggleView = (view: 'table' | 'graph') => {
  activeView.value = view
}
</script>

<template>
  <main class="mx-auto flex min-h-screen w-full max-w-md flex-col bg-[#f6f9ff]">
    <div class="h-14 w-full bg-[rgba(35,6,248,0.15)]" />
    <section class="flex flex-1 flex-col gap-2.5 p-2.5">
      <div class="flex flex-col gap-2.5">
        <MonthSelector
          :month-label="monthLabel"
          :is-current-month="isCurrentMonth"
          @prev="goToPreviousMonth"
          @next="goToNextMonth"
        />

        <ViewToggle :active-view="activeView" @change="onToggleView" />

        <div class="rounded-[20px] border border-[#b3a8a8] bg-white px-3 py-2 text-center">
          <p class="text-base font-semibold leading-none text-black">
            Month's Spending:
            <span class="text-[rgba(35,6,248,0.61)]"> £{{ spendingLabel }}</span>
          </p>
        </div>
      </div>

      <TransactionList
        v-if="activeView === 'table'"
        :groups="transactionGroups"
        @select-transaction="onSelectTransaction"
      />

      <div
        v-else
        class="mt-2 flex h-full min-h-[220px] items-center justify-center rounded-[10px] border border-[#b3a8a8] bg-white p-6 text-center text-sm text-[#706161]"
      >
        Graph view placeholder (wire this to the future graph route/page).
      </div>
    </section>
  </main>
</template>
