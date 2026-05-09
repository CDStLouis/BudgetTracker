import { mount } from '@vue/test-utils'
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest'
import { createMemoryHistory, createRouter } from 'vue-router'
import TransactionsView from './TransactionsView.vue'

const mountTransactionsView = async (path = '/transactions') => {
  const router = createRouter({
    history: createMemoryHistory(),
    routes: [
      { path: '/transactions', component: TransactionsView },
      { path: '/transactions/:id', component: TransactionsView }
    ]
  })

  await router.push(path)
  await router.isReady()

  return mount(TransactionsView, {
    global: {
      plugins: [router]
    }
  })
}

describe('TransactionsView month availability', () => {
  const mockApiResponse = [
    {
      id: 'tx-1',
      dateUtc: '2026-01-14T14:30:00.000Z',
      description: 'Aldi',
      category: 'Groceries',
      signedAmount: -40.39,
      absoluteAmount: 40.39,
      type: 'expense',
      accountName: 'Santander',
      monthKey: '2026-01',
      dateKey: '2026-01-14'
    },
    {
      id: 'tx-2',
      dateUtc: '2026-01-14T09:12:00.000Z',
      description: 'Netflix',
      category: 'Entertainment',
      signedAmount: -15.99,
      absoluteAmount: 15.99,
      type: 'expense',
      accountName: 'Monzo',
      monthKey: '2026-01',
      dateKey: '2026-01-14'
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
    const wrapper = await mountTransactionsView()
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
    const wrapper = await mountTransactionsView()
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
            dateUtc: '2026-05-10T12:00:00.000Z',
            description: 'Salary',
            category: 'Income',
            signedAmount: 1000,
            absoluteAmount: 1000,
            type: 'income',
            accountName: 'Santander',
            monthKey: '2026-05',
            dateKey: '2026-05-10'
          },
          {
            id: 'tx-mar',
            dateUtc: '2026-03-05T09:00:00.000Z',
            description: 'Groceries',
            category: 'Groceries',
            signedAmount: -30,
            absoluteAmount: 30,
            type: 'expense',
            accountName: 'Monzo',
            monthKey: '2026-03',
            dateKey: '2026-03-05'
          }
        ]
      })
    )
  })

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('skips empty months and navigates only through months with data', async () => {
    const wrapper = await mountTransactionsView()
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
            dateUtc: '2026-05-20T12:00:00.000Z',
            description: 'Salary',
            category: 'Income',
            signedAmount: 2500,
            absoluteAmount: 2500,
            type: 'income',
            accountName: 'Santander',
            monthKey: '2026-05',
            dateKey: '2026-05-20'
          },
          {
            id: 'tx-apr',
            dateUtc: '2026-04-12T10:00:00.000Z',
            description: 'Rent',
            category: 'Housing',
            signedAmount: -900,
            absoluteAmount: 900,
            type: 'expense',
            accountName: 'Santander',
            monthKey: '2026-04',
            dateKey: '2026-04-12'
          },
          {
            id: 'tx-mar',
            dateUtc: '2026-03-03T09:00:00.000Z',
            description: 'Groceries',
            category: 'Groceries',
            signedAmount: -42,
            absoluteAmount: 42,
            type: 'expense',
            accountName: 'Monzo',
            monthKey: '2026-03',
            dateKey: '2026-03-03'
          }
        ]
      })
    )

    const wrapper = await mountTransactionsView()
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
    const wrapper = await mountTransactionsView()
    await vi.waitFor(() => {
      expect(wrapper.text()).toContain('No transactions')
    })

    const previousButton = wrapper.get('button[aria-label="Previous month"]')
    const nextButton = wrapper.get('button[aria-label="Next month"]')

    expect((previousButton.element as HTMLButtonElement).disabled).toBe(true)
    expect((nextButton.element as HTMLButtonElement).disabled).toBe(true)
  })
})

describe('TransactionsView loading state', () => {
  const deferredPayload = [
    {
      id: 'tx-1',
      dateUtc: '2026-01-14T14:30:00.000Z',
      description: 'Aldi',
      category: 'Groceries',
      signedAmount: -40.39,
      absoluteAmount: 40.39,
      type: 'expense' as const,
      accountName: 'Santander',
      monthKey: '2026-01',
      dateKey: '2026-01-14'
    }
  ]

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('shows loading UI until fetch resolves, then shows data', async () => {
    let resolveFetch!: (value: { ok: boolean; json: () => Promise<typeof deferredPayload> }) => void
    const fetchDeferred = new Promise<{ ok: boolean; json: () => Promise<typeof deferredPayload> }>((resolve) => {
      resolveFetch = resolve
    })

    vi.stubGlobal('fetch', vi.fn().mockReturnValue(fetchDeferred))

    const wrapper = await mountTransactionsView()

    expect(wrapper.text()).toContain('Loading…')
    expect(wrapper.text()).toContain('Loading transactions…')
    expect(wrapper.text()).not.toContain('No transactions')
    expect(wrapper.find('[aria-busy="true"]').exists()).toBe(true)

    const tableToggle = wrapper.get('button[aria-label="Table view"]')
    const graphToggle = wrapper.get('button[aria-label="Graph view"]')
    expect((tableToggle.element as HTMLButtonElement).disabled).toBe(true)
    expect((graphToggle.element as HTMLButtonElement).disabled).toBe(true)

    resolveFetch({
      ok: true,
      json: async () => deferredPayload
    })

    await vi.waitFor(() => {
      expect(wrapper.text()).toContain('Jan 2026')
      expect(wrapper.text()).toContain('Aldi')
    })

    expect(wrapper.find('[aria-busy="true"]').exists()).toBe(false)
    expect(wrapper.text()).not.toContain('Loading transactions…')
    expect((tableToggle.element as HTMLButtonElement).disabled).toBe(false)
    expect((graphToggle.element as HTMLButtonElement).disabled).toBe(false)
  })
})
