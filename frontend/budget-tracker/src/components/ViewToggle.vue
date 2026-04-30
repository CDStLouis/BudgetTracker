<script setup lang="ts">
import { ChartLine, Table2 } from 'lucide-vue-next'

const props = defineProps<{
  activeView: 'table' | 'graph'
}>()

const emit = defineEmits<{
  change: [view: 'table' | 'graph']
}>()

const baseButtonClass = 'flex h-full w-1/2 items-center justify-center transition'
</script>

<template>
  <div class="mx-auto flex h-11 w-fit overflow-hidden rounded-lg border border-[#b3a8a8] bg-[#d2d2d2]">
    <button
      type="button"
      aria-label="Table view"
      :class="[
        baseButtonClass,
        'border-r border-[#b3a8a8]',
        props.activeView === 'table'
          ? 'bg-[#eeeeee] text-black shadow-[inset_0_3px_4px_rgba(0,0,0,0.15)]'
          : 'bg-[#d2d2d2] text-[#808080]'
      ]"
      @click="emit('change', 'table')"
    >
      <Table2 class="h-6 w-10" />
    </button>

    <button
      type="button"
      aria-label="Graph view"
      :class="[
        baseButtonClass,
        props.activeView === 'graph'
          ? 'bg-[#eeeeee] text-black shadow-[inset_0_3px_4px_rgba(0,0,0,0.15)]'
          : 'bg-[#d2d2d2] text-[#808080]'
      ]"
      @click="emit('change', 'graph')"
    >
      <ChartLine class="h-6 w-10" />
    </button>
  </div>
</template>
