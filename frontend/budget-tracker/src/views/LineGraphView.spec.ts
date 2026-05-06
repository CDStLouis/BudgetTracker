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

  it('anchors area and line at chart origin before first data point', () => {
    const wrapper = mount(LineGraphView, {
      props: {
        monthLabel: 'Apr 2026',
        disablePreviousMonth: false,
        disableNextMonth: false,
        spendingLabel: '200.00',
        dailySpending: [
          { day: 14, amount: 25 },
          { day: 24, amount: 40 },
          { day: 27, amount: 0 }
        ]
      }
    })

    const linePath = wrapper.get('[data-testid="graph-line-path"]').attributes('d') ?? ''
    const areaPath = wrapper.get('[data-testid="graph-area-path"]').attributes('d') ?? ''

    const firstPointMatch = linePath.match(/^M([0-9.]+) ([0-9.]+)/)
    const lastPointMatch = linePath.match(/L([0-9.]+) ([0-9.]+)$/)

    expect(firstPointMatch).not.toBeNull()
    expect(lastPointMatch).not.toBeNull()

    const firstX = firstPointMatch?.[1]
    const firstY = firstPointMatch?.[2]
    const lastX = lastPointMatch?.[1]
    expect(firstX).toBe('45.00')
    expect(firstY).toBe('245.00')
    expect(areaPath).toContain(`L${lastX} 245`)
    expect(areaPath).toContain(`L45 245`)
  })
})
