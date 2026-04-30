<script setup lang="ts">
import { ChartLine, Table2 } from 'lucide-vue-next'

const props = defineProps<{
  activeView: 'table' | 'graph'
}>()

const emit = defineEmits<{
  change: [view: 'table' | 'graph']
}>()

const baseButtonClass =
  'flex h-full w-1/2 items-center justify-center border border-[#b3a8a8] text-black transition'
</script>

<template>
  <div class="mx-auto flex h-11 w-fit rounded-lg border border-[#b3a8a8] bg-[#d2d2d2] p-0.5">
    <button
      type="button"
      aria-label="Table view"
      :class="[
        baseButtonClass,
        props.activeView === 'table'
          ? 'rounded-l-md bg-[rgba(179,168,168,0.2)] shadow-[inset_0_3px_4px_rgba(0,0,0,0.15)]'
          : 'rounded-l-md bg-[#d9d9d9]'
      ]"
      @click="emit('change', 'table')"
    >
      <Table2 class="h-5 w-5" />
    </button>

    <button
      type="button"
      aria-label="Graph view"
      :class="[
        baseButtonClass,
        props.activeView === 'graph'
          ? 'rounded-r-md bg-[rgba(179,168,168,0.2)] shadow-[inset_0_3px_4px_rgba(0,0,0,0.15)]'
          : 'rounded-r-md bg-[#d9d9d9]'
      ]"
      @click="emit('change', 'graph')"
    >
      <ChartLine class="h-5 w-5" />
    </button>
  </div>
</template>
