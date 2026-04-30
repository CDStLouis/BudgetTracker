import { mount } from '@vue/test-utils'
import { describe, expect, it } from 'vitest'
import LineGraphView from './LineGraphView.vue'

describe('LineGraphView month selector wiring', () => {
  it('passes disabled state to month navigation arrows', () => {
    const wrapper = mount(LineGraphView, {
      props: {
        monthLabel: 'Jan 2026',
        disablePreviousMonth: true,
        disableNextMonth: true,
        spendingLabel: '99.99'
      }
    })

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
  })
})
