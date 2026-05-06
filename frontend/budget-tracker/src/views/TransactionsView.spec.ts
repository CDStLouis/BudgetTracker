import { mount } from '@vue/test-utils'
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest'
import TransactionsView from './TransactionsView.vue'

describe('TransactionsView month availability', () => {
  const mockApiResponse = [
    {
      id: 'tx-1',
      date: '2026-01-14T14:30:00.000Z',
      description: 'Aldi',
      category: 'Groceries',
      amount: -40.39,
      accountName: 'Santander'
    },
    {
      id: 'tx-2',
      date: '2026-01-14T09:12:00.000Z',
      description: 'Netflix',
      category: 'Entertainment',
      amount: -15.99,
      accountName: 'Monzo'
    }
  ]

  beforeEach(() => {
    vi.stubGlobal(
      'fetch',
      vi.fn().mockResolvedValue({
        ok: true,
        json: async () => mockApiResponse
      })
    )
  })

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('keeps month navigation disabled and locked to January in table view', async () => {
    const wrapper = mount(TransactionsView)
    await vi.waitFor(() => {
      expect(wrapper.text()).toContain('Jan 2026')
    })

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
    await vi.waitFor(() => {
      expect(wrapper.text()).toContain('Jan 2026')
    })

    await wrapper.get('button[aria-label="Graph view"]').trigger('click')

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
    expect(wrapper.text()).toContain('Jan 2026')
  })
})

describe('TransactionsView month navigation', () => {
  beforeEach(() => {
    vi.stubGlobal(
      'fetch',
      vi.fn().mockResolvedValue({
        ok: true,
        json: async () => [
          {
            id: 'tx-may',
            date: '2026-05-10T12:00:00.000Z',
            description: 'Salary',
            category: 'Income',
            amount: 1000,
            accountName: 'Santander'
          },
          {
            id: 'tx-mar',
            date: '2026-03-05T09:00:00.000Z',
            description: 'Groceries',
            category: 'Groceries',
            amount: -30,
            accountName: 'Monzo'
          }
        ]
      })
    )
  })

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('skips empty months and navigates only through months with data', async () => {
    const wrapper = mount(TransactionsView)
    await vi.waitFor(() => {
      expect(wrapper.text()).toContain('May 2026')
      expect(wrapper.text()).toContain('Salary')
    })

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(false)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)

    await previousButton.trigger('click')
    expect(wrapper.text()).toContain('Mar 2026')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(false)
  })

  it('moves backward with previous and forward with next', async () => {
    vi.stubGlobal(
      'fetch',
      vi.fn().mockResolvedValue({
        ok: true,
        json: async () => [
          {
            id: 'tx-may',
            date: '2026-05-20T12:00:00.000Z',
            description: 'Salary',
            category: 'Income',
            amount: 2500,
            accountName: 'Santander'
          },
          {
            id: 'tx-apr',
            date: '2026-04-12T10:00:00.000Z',
            description: 'Rent',
            category: 'Housing',
            amount: -900,
            accountName: 'Santander'
          },
          {
            id: 'tx-mar',
            date: '2026-03-03T09:00:00.000Z',
            description: 'Groceries',
            category: 'Groceries',
            amount: -42,
            accountName: 'Monzo'
          }
        ]
      })
    )

    const wrapper = mount(TransactionsView)
    await vi.waitFor(() => {
      expect(wrapper.text()).toContain('May 2026')
    })

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    await previousButton.trigger('click')
    expect(wrapper.text()).toContain('Apr 2026')

    await previousButton.trigger('click')
    expect(wrapper.text()).toContain('Mar 2026')

    await nextButton.trigger('click')
    expect(wrapper.text()).toContain('Apr 2026')
  })
})

describe('TransactionsView no-data state', () => {
  beforeEach(() => {
    vi.stubGlobal(
      'fetch',
      vi.fn().mockResolvedValue({
        ok: true,
        json: async () => []
      })
    )
  })

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('does not fall back to current month when there are no transactions', async () => {
    const wrapper = mount(TransactionsView)
    await vi.waitFor(() => {
      expect(wrapper.text()).toContain('No transactions')
    })

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
  })
})
