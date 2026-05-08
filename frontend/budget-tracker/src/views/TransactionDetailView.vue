<script setup lang="ts">
import { computed } from 'vue'
import {
  BadgePoundSterling,
  Calendar,
  ChevronLeft,
  CircleDollarSign,
  Clock3,
  CreditCard,
  Dumbbell,
  Popcorn,
  ShoppingBasket,
  Tag,
  TramFront,
  Utensils
} from 'lucide-vue-next'

interface DetailTransaction {
  description: string
  category: string
  amount: number
  type: 'expense' | 'income'
  fullDate: string
  time: string
  account: string
}

const props = defineProps<{
  transaction: DetailTransaction
}>()

const emit = defineEmits<{
  back: []
}>()

const amountLabel = computed(() => {
  const sign = props.transaction.type === 'expense' ? '-' : '+'
  return `${sign}£${Math.abs(props.transaction.amount).toLocaleString('en-GB', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  })}`
})

const amountClass = computed(() =>
  props.transaction.type === 'expense' ? 'text-[#ef4444]' : 'text-[#45aa5b]'
)

const iconBgClass = computed(() =>
  props.transaction.type === 'expense' ? 'bg-[rgba(35,6,248,0.15)]' : 'bg-[rgba(69,170,91,0.35)]'
)

const categoryIcon = computed(() => {
  const normalized = props.transaction.category.toLowerCase()
  if (normalized.includes('grocer')) return ShoppingBasket
  if (normalized.includes('entertain')) return Popcorn
  if (normalized.includes('health') || normalized.includes('fitness')) return Dumbbell
  if (normalized.includes('income')) return CircleDollarSign
  if (normalized.includes('transport')) return TramFront
  if (normalized.includes('eat')) return Utensils
  return Tag
})
</script>

<template>
  <main class="mx-auto flex min-h-screen w-full max-w-md flex-col bg-[#f6f9ff]">
    <div class="h-14 w-full bg-[rgba(35,6,248,0.15)]" />

    <section class="flex flex-1 flex-col gap-2.5 p-2.5">
      <header class="grid grid-cols-[auto_1fr_auto] items-center px-2.5">
        <button
          type="button"
          class="flex h-12 w-12 items-center justify-center rounded-full border border-[#b3a8a8] bg-white text-black"
          aria-label="Back to transactions"
          @click="emit('back')"
        >
          <ChevronLeft class="h-5 w-5" />
        </button>
        <h1 class="text-center text-[20px] font-medium text-black">Transaction Details</h1>
        <div class="h-12 w-12" />
      </header>

      <div class="overflow-hidden rounded-[10px] border border-[#b3a8a8] bg-white">
        <div class="flex flex-col items-center gap-2.5 px-2.5 py-4">
          <div :class="['rounded-[22px] p-2.5', iconBgClass]">
            <component :is="categoryIcon" class="h-6 w-6 text-black" />
          </div>
          <p class="text-[15px] font-medium leading-none text-black">{{ transaction.description }}</p>
        </div>

        <div
          class="grid grid-cols-[1fr_auto] items-center border-t border-[#b3a8a8] px-[15px] py-3"
        >
          <div class="flex items-center gap-2.5">
            <div class="rounded-[22px] bg-[#dbdee4] p-2.5">
              <BadgePoundSterling class="h-6 w-6 text-black" />
            </div>
            <p class="text-[15px] font-medium text-[#808080]">Amount</p>
          </div>
          <p :class="['text-[15px]', amountClass]">{{ amountLabel }}</p>
        </div>

        <div class="grid grid-cols-[1fr_auto] items-center border-t border-[#b3a8a8] px-[15px] py-3">
          <div class="flex items-center gap-2.5">
            <div class="rounded-[22px] bg-[#dbdee4] p-2.5">
              <Calendar class="h-6 w-6 text-black" />
            </div>
            <p class="text-[15px] font-medium text-[#808080]">Date</p>
          </div>
          <p class="text-[15px] text-black">{{ transaction.fullDate }}</p>
        </div>

        <div class="grid grid-cols-[1fr_auto] items-center border-t border-[#b3a8a8] px-[15px] py-3">
          <div class="flex items-center gap-2.5">
            <div class="rounded-[22px] bg-[#dbdee4] p-2.5">
              <Clock3 class="h-6 w-6 text-black" />
            </div>
            <p class="text-[15px] font-medium text-[#808080]">Time</p>
          </div>
          <p class="text-[15px] text-black">{{ transaction.time }}</p>
        </div>

        <div class="grid grid-cols-[1fr_auto] items-center border-t border-[#b3a8a8] px-[15px] py-3">
          <div class="flex items-center gap-2.5">
            <div class="rounded-[22px] bg-[#dbdee4] p-2.5">
              <Tag class="h-6 w-6 text-black" />
            </div>
            <p class="text-[15px] font-medium text-[#808080]">Category</p>
          </div>
          <p class="text-[15px] text-black">{{ transaction.category }}</p>
        </div>

        <div class="grid grid-cols-[1fr_auto] items-center border-t border-[#b3a8a8] px-[15px] py-3">
          <div class="flex items-center gap-2.5">
            <div class="rounded-[22px] bg-[#dbdee4] p-2.5">
              <CreditCard class="h-6 w-6 text-black" />
            </div>
            <p class="text-[15px] font-medium text-[#808080]">Account</p>
          </div>
          <p class="text-[15px] text-black">{{ transaction.account }}</p>
        </div>
      </div>
    </section>
  </main>
</template>
