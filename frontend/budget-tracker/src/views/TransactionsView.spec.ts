import { mount } from '@vue/test-utils'
import { describe, expect, it } from 'vitest'
import TransactionsView from './TransactionsView.vue'

describe('TransactionsView month availability', () => {
  it('keeps month navigation disabled and locked to January in table view', async () => {
    const wrapper = mount(TransactionsView)

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
    expect(wrapper.text()).toContain('Jan 2026')

    await previousButton.trigger('click')
    await nextButton.trigger('click')

    expect(wrapper.text()).toContain('Jan 2026')
  })

  it('keeps month navigation disabled after switching to graph view', async () => {
    const wrapper = mount(TransactionsView)

    await wrapper.get('button[aria-label="Graph view"]').trigger('click')

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
    expect(wrapper.text()).toContain('Jan 2026')
  })
})
