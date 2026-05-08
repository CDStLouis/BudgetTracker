<script setup lang="ts">
import { computed } from 'vue'
import {
  ChevronRight,
  CircleDollarSign,
  Dumbbell,
  Popcorn,
  ShoppingBasket,
  TramFront,
  Utensils
} from 'lucide-vue-next'

const props = defineProps<{
  description: string
  category: string
  amount: number
  type: 'expense' | 'income'
}>()

const emit = defineEmits<{
  select: []
}>()

const amountLabel = computed(() => {
  const sign = props.type === 'expense' ? '-' : '+'
  return `${sign}£${Math.abs(props.amount).toLocaleString('en-GB', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  })}`
})

const amountClass = computed(() => (props.type === 'expense' ? 'text-[#ef4444]' : 'text-[#45aa5b]'))

const iconWrapperClass = computed(() =>
  props.type === 'expense' ? 'bg-[rgba(35,6,248,0.15)]' : 'bg-[rgba(69,170,91,0.35)]'
)

const iconComponent = computed(() => {
  const normalized = props.category.toLowerCase()

  if (normalized.includes('grocer')) return ShoppingBasket
  if (normalized.includes('entertain')) return Popcorn
  if (normalized.includes('health') || normalized.includes('fitness')) return Dumbbell
  if (normalized.includes('income')) return CircleDollarSign
  if (normalized.includes('transport')) return TramFront
  if (normalized.includes('eat')) return Utensils
  return ShoppingBasket
})
</script>

<template>
  <button
    type="button"
    class="grid w-full grid-cols-[auto_1fr_auto_auto] items-center gap-3 bg-white px-3 py-3 text-left"
    @click="emit('select')"
  >
    <div :class="['rounded-[22px] p-2.5', iconWrapperClass]">
      <component :is="iconComponent" class="h-6 w-6 text-black" />
    </div>

    <div class="min-w-0">
      <p class="truncate text-[15px] font-medium leading-none text-black">{{ description }}</p>
      <p class="mt-1 truncate text-[13px] leading-none text-[#706161]">{{ category }}</p>
    </div>

    <p :class="['text-[15px] leading-none', amountClass]">{{ amountLabel }}</p>
    <ChevronRight class="h-4 w-4 text-[#b3a8a8]" />
  </button>
</template>
