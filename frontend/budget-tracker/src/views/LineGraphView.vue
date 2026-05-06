<script setup lang="ts">
import { computed } from 'vue'
import MonthSelector from '../components/MonthSelector.vue'
import ViewToggle from '../components/ViewToggle.vue'

interface DailySpendingPoint {
  day: number
  amount: number
}

const props = defineProps<{
  monthLabel: string
  disablePreviousMonth: boolean
  disableNextMonth: boolean
  spendingLabel: string
  dailySpending: DailySpendingPoint[]
}>()

const emit = defineEmits<{
  prevMonth: []
  nextMonth: []
  changeView: [view: 'table' | 'graph']
}>()

const chart = {
  left: 45,
  right: 330,
  top: 85,
  bottom: 245
}

const maxDay = computed(() => {
  if (props.dailySpending.length === 0) return 1
  return Math.max(...props.dailySpending.map((point) => point.day), 1)
})

const maxAmount = computed(() => {
  if (props.dailySpending.length === 0) return 200
  const observedMax = Math.max(...props.dailySpending.map((point) => point.amount), 0)
  return Math.max(Math.ceil(observedMax / 50) * 50, 50)
})

const yTicks = computed(() => {
  const step = maxAmount.value / 4
  return [0, 1, 2, 3, 4].map((idx) => ({
    value: Math.round(step * idx),
    y: chart.bottom - ((chart.bottom - chart.top) / 4) * idx
  }))
})

const xTicks = computed(() => {
  const mid = Math.max(Math.round(maxDay.value / 2), 1)
  return [1, mid, maxDay.value].map((day) => {
    const ratio = maxDay.value === 1 ? 0 : (day - 1) / (maxDay.value - 1)
    return {
      day,
      x: chart.left + (chart.right - chart.left) * ratio
    }
  })
})

const linePath = computed(() => {
  if (props.dailySpending.length === 0) return ''

  const sorted = [...props.dailySpending].sort((a, b) => a.day - b.day)
  return sorted
    .map((point, index) => {
      const xRatio = maxDay.value === 1 ? 0 : (point.day - 1) / (maxDay.value - 1)
      const yRatio = maxAmount.value === 0 ? 0 : point.amount / maxAmount.value
      const x = chart.left + (chart.right - chart.left) * xRatio
      const y = chart.bottom - (chart.bottom - chart.top) * yRatio
      return `${index === 0 ? 'M' : 'L'}${x.toFixed(2)} ${y.toFixed(2)}`
    })
    .join(' ')
})

const areaPath = computed(() => {
  if (!linePath.value) return ''
  return `${linePath.value} L${chart.right} ${chart.bottom} L${chart.left} ${chart.bottom} Z`
})
</script>

<template>
  <main class="mx-auto flex min-h-screen w-full max-w-md flex-col bg-[#f6f9ff]">
    <div class="h-14 w-full bg-[rgba(35,6,248,0.15)]" />
    <section class="flex flex-1 flex-col gap-2.5 p-2.5">
      <div class="flex flex-col gap-2.5">
        <MonthSelector
          :month-label="monthLabel"
          :disable-previous="disablePreviousMonth"
          :disable-next="disableNextMonth"
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
        <svg viewBox="0 0 350 300" class="h-auto w-full overflow-visible">
          <g stroke="#d0d4db" stroke-width="1">
            <line v-for="tick in yTicks" :key="`grid-${tick.value}`" x1="45" :y1="tick.y" x2="330" :y2="tick.y" />
          </g>

          <line x1="45" y1="245" x2="330" y2="245" stroke="#737780" stroke-width="1.5" />
          <line x1="45" y1="245" x2="45" y2="85" stroke="#737780" stroke-width="1.5" />

          <path
            v-if="areaPath"
            data-testid="graph-area-path"
            :d="areaPath"
            fill="rgba(126,115,255,0.18)"
          />
          <path
            v-if="linePath"
            data-testid="graph-line-path"
            :d="linePath"
            fill="none"
            stroke="#7e73ff"
            stroke-width="2.5"
          />

          <g fill="#54555a" font-size="12" font-family="Inter, system-ui, sans-serif">
            <text
              v-for="tick in yTicks"
              :key="`y-label-${tick.value}`"
              x="12"
              :y="tick.y + 4"
            >
              {{ tick.value }}
            </text>

            <text
              v-for="tick in xTicks"
              :key="`x-label-${tick.day}`"
              :x="tick.x - 4"
              y="263"
            >
              {{ tick.day }}
            </text>
          </g>

          <text x="178" y="282" text-anchor="middle" fill="#54555a" font-size="12">Days</text>
          <text x="6" y="165" text-anchor="middle" fill="#54555a" font-size="12" transform="rotate(-90 6 165)">
            Spent
          </text>
        </svg>
      </div>
    </section>
  </main>
</template>
