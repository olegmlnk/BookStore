const DEFAULT_BASE = 'http://localhost:5198'

let base = DEFAULT_BASE.replace(/\/+$/, '')

export function setBaseUrl(url) {
  if (typeof url !== 'string') {
    console.error('setBaseUrl: invalid URL', url)
    return
  }
  base = url.replace(/\/+$/, '')
}

async function request(path, opts = {}) {
  const url = `${base}${path.startsWith('/') ? path : `/${path}`}`
  let res
  try {
    res = await fetch(url, {
      headers: { 'Content-Type': 'application/json' },
      ...opts,
    })
  } catch (err) {
    console.error('Fetch error for', url, opts, err)
    throw err
  }
  if (!res.ok) {
    const text = await res.text()
    throw new Error(`${res.status} ${res.statusText}: ${text}`)
  }
  if (res.status === 204) return null
  return res.json()
}

export function getBooks() {
  return request('/api/get-books')
}

export function createBook(book) {
  return request('/api/create-book', { method: 'POST', body: JSON.stringify(book) })
}

export function updateBook(id, book) {
  return request(`/api/update-book/${id}`, { method: 'PUT', body: JSON.stringify(book) })
}

export function deleteBook(id) {
  return request(`/api/delete-book/${id}`, { method: 'DELETE' })
}

export default { setBaseUrl, getBooks, createBook, updateBook, deleteBook }
