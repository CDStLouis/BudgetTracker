<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import MonthSelector from '../components/MonthSelector.vue'
import TransactionList from '../components/TransactionList.vue'
import ViewToggle from '../components/ViewToggle.vue'
import LineGraphView from './LineGraphView.vue'
import TransactionDetailView from './TransactionDetailView.vue'

interface TransactionGroup {
  id: string
  dateLabel: string
  transactions: {
    id: string
    description: string
    category: string
    amount: number
    type: 'expense' | 'income'
    fullDate: string
    time: string
    account: string
    sortDate: string
  }[]
}

interface ApiTransaction {
  id: string
  dateUtc: string
  description: string
  signedAmount: number
  absoluteAmount: number
  type: 'expense' | 'income'
  category: string
  accountName: string
  monthKey: string
  dateKey: string
}

const configuredApiUrl = (import.meta.env.VITE_API_URL as string | undefined)?.replace(/\/+$/, '') ?? ''
const apiBaseUrl = import.meta.env.DEV ? '' : configuredApiUrl
const transactionsEndpoint = `${apiBaseUrl}/api/transactions`

const activeView = ref<'table' | 'graph'>('table')
const activeScreen = ref<'transactions' | 'detail'>('transactions')
const selectedTransactionId = ref<string | null>(null)
const allTransactionGroups = ref<TransactionGroup[]>([])
const availableMonths = ref<Date[]>([])
const activeMonthIndex = ref(0)
const displayedMonth = computed(() => availableMonths.value[activeMonthIndex.value] ?? null)
const canGoToPreviousMonth = computed(() => activeMonthIndex.value < availableMonths.value.length - 1)
const canGoToNextMonth = computed(() => activeMonthIndex.value > 0)

const monthLabel = computed(() => {
  if (!displayedMonth.value) return 'No transactions'
  return displayedMonth.value.toLocaleDateString('en-GB', { month: 'short', year: 'numeric' })
})

const transactionGroups = computed(() => {
  if (!displayedMonth.value) return []

  const month = displayedMonth.value.getMonth()
  const year = displayedMonth.value.getFullYear()

  return allTransactionGroups.value.filter((group) => {
    const groupDate = new Date(group.id)
    return !Number.isNaN(groupDate.getTime()) && groupDate.getMonth() === month && groupDate.getFullYear() === year
  })
})

const spendingTotal = computed(() =>
  transactionGroups.value
    .flatMap((group) => group.transactions)
    .filter((tx) => tx.type === 'expense')
    .reduce((acc, tx) => acc + tx.amount, 0)
)

const spendingLabel = computed(() =>
  spendingTotal.value.toLocaleString('en-GB', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
)

const dailySpending = computed(() =>
  transactionGroups.value
    .map((group) => {
      const date = new Date(group.id)
      if (Number.isNaN(date.getTime())) return null

      const amount = group.transactions
        .filter((tx) => tx.type === 'expense')
        .reduce((total, tx) => total + tx.amount, 0)

      return {
        day: date.getDate(),
        amount
      }
    })
    .filter((point): point is { day: number; amount: number } => point !== null)
    .sort((a, b) => a.day - b.day)
)

const selectedTransaction = computed(() =>
  allTransactionGroups.value.flatMap((group) => group.transactions).find((tx) => tx.id === selectedTransactionId.value)
)

const toOrdinal = (day: number) => {
  if (day >= 11 && day <= 13) return `${day}th`
  const lastDigit = day % 10
  if (lastDigit === 1) return `${day}st`
  if (lastDigit === 2) return `${day}nd`
  if (lastDigit === 3) return `${day}rd`
  return `${day}th`
}

const formatDateLabel = (date: Date) => {
  const weekday = date.toLocaleDateString('en-GB', { weekday: 'long' })
  return `${weekday} ${toOrdinal(date.getDate())}`
}

const formatFullDate = (date: Date) =>
  `${formatDateLabel(date)} ${date.toLocaleDateString('en-GB', { month: 'short', year: 'numeric' })}`

const loadTransactions = async () => {
  try {
    const response = await fetch(transactionsEndpoint)
    if (!response.ok) throw new Error(`Failed to fetch transactions: ${response.status}`)

    const apiTransactions = (await response.json()) as ApiTransaction[]
    const byDate = new Map<string, TransactionGroup>()
    const months = new Set<string>()

    for (const tx of apiTransactions) {
      const date = new Date(tx.dateUtc)
      if (Number.isNaN(date.getTime())) continue

      months.add(tx.monthKey)

      const normalized = {
        id: tx.id,
        description: tx.description,
        category: tx.category,
        amount: tx.absoluteAmount,
        type: tx.type,
        fullDate: formatFullDate(date),
        time: date.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit', hour12: false }),
        account: tx.accountName,
        sortDate: tx.dateUtc
      }

      const existing = byDate.get(tx.dateKey)
      if (existing) {
        existing.transactions.push(normalized)
      } else {
        byDate.set(tx.dateKey, {
          id: tx.dateKey,
          dateLabel: formatDateLabel(date),
          transactions: [normalized]
        })
      }
    }

    const sortedGroups = Array.from(byDate.entries())
      .sort(([a], [b]) => b.localeCompare(a))
      .map(([, group]) => ({
        ...group,
        transactions: group.transactions.sort((a, b) => b.sortDate.localeCompare(a.sortDate))
      }))

    allTransactionGroups.value = sortedGroups
    availableMonths.value = Array.from(months)
      .map((monthKey) => {
        const [year, month] = monthKey.split('-').map(Number)
        return new Date(year, month - 1, 1)
      })
      .sort((a, b) => b.getTime() - a.getTime())

    activeMonthIndex.value = 0
  } catch (error) {
    allTransactionGroups.value = []
    availableMonths.value = []
    activeMonthIndex.value = 0
    console.error(error)
  }
}

onMounted(loadTransactions)

const goToPreviousMonth = () => {
  if (!canGoToPreviousMonth.value) return
  activeMonthIndex.value += 1
}

const goToNextMonth = () => {
  if (!canGoToNextMonth.value) return
  activeMonthIndex.value -= 1
}

const onSelectTransaction = (id: string) => {
  selectedTransactionId.value = id
  activeScreen.value = 'detail'
}

const onToggleView = (view: 'table' | 'graph') => {
  activeView.value = view
  activeScreen.value = 'transactions'
}

const onBackFromDetail = () => {
  activeScreen.value = 'transactions'
}
</script>

<template>
  <TransactionDetailView
    v-if="activeScreen === 'detail' && selectedTransaction"
    :transaction="selectedTransaction"
    @back="onBackFromDetail"
  />

  <LineGraphView
    v-else-if="activeView === 'graph'"
    :month-label="monthLabel"
    :disable-previous-month="!canGoToPreviousMonth"
    :disable-next-month="!canGoToNextMonth"
    :spending-label="spendingLabel"
    :daily-spending="dailySpending"
    @prev-month="goToPreviousMonth"
    @next-month="goToNextMonth"
    @change-view="onToggleView"
  />

  <main v-else class="mx-auto flex min-h-screen w-full max-w-md flex-col bg-[#f6f9ff]">
    <div class="h-14 w-full bg-[rgba(35,6,248,0.15)]" />
    <section class="flex flex-1 flex-col gap-2.5 p-2.5">
      <div class="flex flex-col gap-2.5">
        <MonthSelector
          :month-label="monthLabel"
          :disable-previous="!canGoToPreviousMonth"
          :disable-next="!canGoToNextMonth"
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
        :groups="transactionGroups"
        @select-transaction="onSelectTransaction"
      />
    </section>
  </main>
</template>
