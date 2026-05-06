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
        spendingLabel: '99.99',
        dailySpending: []
      }
    })

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
  })

  it('updates the graph path when monthly data changes', async () => {
    const wrapper = mount(LineGraphView, {
      props: {
        monthLabel: 'Mar 2026',
        disablePreviousMonth: false,
        disableNextMonth: false,
        spendingLabel: '120.00',
        dailySpending: [
          { day: 2, amount: 20 },
          { day: 10, amount: 60 }
        ]
      }
    })

    const initialPath = wrapper.get('[data-testid="graph-line-path"]').attributes('d')

    await wrapper.setProps({
      monthLabel: 'Apr 2026',
      dailySpending: [
        { day: 3, amount: 80 },
        { day: 20, amount: 160 }
      ]
    })

    const updatedPath = wrapper.get('[data-testid="graph-line-path"]').attributes('d')
    expect(updatedPath).not.toBe(initialPath)
  })
})
