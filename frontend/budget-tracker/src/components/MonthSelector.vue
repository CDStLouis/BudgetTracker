<script setup lang="ts">
import { ChevronLeft, ChevronRight } from 'lucide-vue-next'

defineProps<{
  monthLabel: string
  isCurrentMonth?: boolean
}>()

const emit = defineEmits<{
  prev: []
  next: []
}>()
</script>

<template>
  <div class="grid grid-cols-[1fr_auto_1fr] items-center rounded-[23px] p-1">
    <button
      type="button"
      class="flex h-12 w-12 items-center justify-center justify-self-start rounded-full border border-[#b3a8a8] bg-white text-black transition disabled:cursor-not-allowed disabled:bg-[#d9d9d9] disabled:text-[#808080]"
      aria-label="Previous month"
      @click="emit('prev')"
    >
      <ChevronLeft class="h-5 w-5" />
    </button>

    <p class="text-center text-[20px] font-medium leading-none text-black">{{ monthLabel }}</p>

    <button
      type="button"
      class="flex h-12 w-12 items-center justify-center justify-self-end rounded-full border border-[#b3a8a8] bg-white text-black transition disabled:cursor-not-allowed disabled:bg-[#d9d9d9] disabled:text-[#808080]"
      aria-label="Next month"
      :disabled="isCurrentMonth"
      @click="emit('next')"
    >
      <ChevronRight class="h-5 w-5" />
    </button>
  </div>
</template>
