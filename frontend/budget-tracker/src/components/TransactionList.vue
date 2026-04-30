<script setup lang="ts">
import DateGroup, { type DateGroupTransaction } from './DateGroup.vue'

interface TransactionGroup {
  id: string
  dateLabel: string
  transactions: DateGroupTransaction[]
}

defineProps<{
  groups: TransactionGroup[]
}>()

const emit = defineEmits<{
  selectTransaction: [id: string]
}>()
</script>

<template>
  <div class="h-full overflow-y-auto px-2 pb-8 pt-2">
    <div class="flex flex-col gap-6">
      <DateGroup
        v-for="group in groups"
        :key="group.id"
        :date-label="group.dateLabel"
        :transactions="group.transactions"
        @select-transaction="emit('selectTransaction', $event)"
      />
    </div>
  </div>
</template>