<script setup lang="ts">
import MonthSelector from '../components/MonthSelector.vue'
import ViewToggle from '../components/ViewToggle.vue'

defineProps<{
  monthLabel: string
  isCurrentMonth: boolean
  spendingLabel: string
}>()

const emit = defineEmits<{
  prevMonth: []
  nextMonth: []
  changeView: [view: 'table' | 'graph']
}>()
</script>

<template>
  <main class="mx-auto flex min-h-screen w-full max-w-md flex-col bg-[#f6f9ff]">
    <div class="h-14 w-full bg-[rgba(35,6,248,0.15)]" />
    <section class="flex flex-1 flex-col gap-2.5 p-2.5">
      <div class="flex flex-col gap-2.5">
        <MonthSelector
          :month-label="monthLabel"
          :is-current-month="isCurrentMonth"
          @prev="emit('prevMonth')"
          @next="emit('nextMonth')"
        />

        <ViewToggle active-view="graph" @change="emit('changeView', $event)" />

        <div class="rounded-[20px] border border-[#b3a8a8] bg-white px-3 py-2 text-center">
          <p class="text-base font-semibold leading-none text-black">
            Month's Spending:
            <span class="text-[rgba(35,6,248,0.61)]"> £{{ spendingLabel }}</span>
          </p>
        </div>
      </div>

      <div class="mt-5 rounded-[10px] border border-[#b3a8a8] bg-white p-3">
        <svg viewBox="0 0 350 300" class="h-auto w-full">
          <g stroke="#d0d4db" stroke-width="1">
            <line x1="45" y1="245" x2="330" y2="245" />
            <line x1="45" y1="205" x2="330" y2="205" />
            <line x1="45" y1="165" x2="330" y2="165" />
            <line x1="45" y1="125" x2="330" y2="125" />
            <line x1="45" y1="85" x2="330" y2="85" />
          </g>

          <line x1="45" y1="245" x2="330" y2="245" stroke="#737780" stroke-width="1.5" />
          <line x1="45" y1="245" x2="45" y2="85" stroke="#737780" stroke-width="1.5" />

          <path d="M45 213 L105 198 L160 176 L205 166 L260 142 L330 98 L330 245 L45 245 Z" fill="rgba(126,115,255,0.18)" />
          <path d="M45 213 L105 198 L160 176 L205 166 L260 142 L330 98" fill="none" stroke="#7e73ff" stroke-width="2.5" />

          <g fill="#54555a" font-size="12" font-family="Inter, system-ui, sans-serif">
            <text x="27" y="249">0</text>
            <text x="18" y="209">50</text>
            <text x="12" y="169">100</text>
            <text x="12" y="129">150</text>
            <text x="12" y="89">200</text>

            <text x="43" y="263">1</text>
            <text x="102" y="263">5</text>
            <text x="157" y="263">9</text>
            <text x="200" y="263">12</text>
            <text x="322" y="263">14</text>
          </g>

          <text x="178" y="282" text-anchor="middle" fill="#54555a" font-size="12">Days</text>
          <text x="15" y="170" fill="#54555a" font-size="12" transform="rotate(-90 15 170)">Spent</text>
        </svg>
      </div>
    </section>
  </main>
</template>
