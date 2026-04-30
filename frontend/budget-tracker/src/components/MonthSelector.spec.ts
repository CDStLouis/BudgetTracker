import { mount } from '@vue/test-utils'
import { describe, expect, it } from 'vitest'
import MonthSelector from './MonthSelector.vue'

describe('MonthSelector', () => {
  it('disables both arrow buttons when props request it', () => {
    const wrapper = mount(MonthSelector, {
      props: {
        monthLabel: 'Jan 2026',
        disablePrevious: true,
        disableNext: true
      }
    })

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
  })

  it('does not emit navigation events when arrows are disabled', async () => {
    const wrapper = mount(MonthSelector, {
      props: {
        monthLabel: 'Jan 2026',
        disablePrevious: true,
        disableNext: true
      }
    })

    await wrapper.get('button[aria-label="Previous month"]').trigger('click')
    await wrapper.get('button[aria-label="Next month"]').trigger('click')

    expect(wrapper.emitted('prev')).toBeUndefined()
    expect(wrapper.emitted('next')).toBeUndefined()
  })
})
