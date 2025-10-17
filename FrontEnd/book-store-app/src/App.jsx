import { useEffect, useState } from 'react'
import './App.css'
import api from './api'

function emptyBook() {
  return { title: '', description: '', price: '', id: null }
}

function App() {
  const [books, setBooks] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
  const [form, setForm] = useState(emptyBook())
  const [editing, setEditing] = useState(false)

  useEffect(() => {
    load()
  }, [])

  async function load() {
    setLoading(true)
    setError('')
    try {
      const data = await api.getBooks()
      setBooks(Array.isArray(data) ? data : [])
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  function startEdit(book) {
    setForm({ ...book })
    setEditing(true)
  }

  function resetForm() {
    setForm(emptyBook())
    setEditing(false)
  }

  async function submit(e) {
    e.preventDefault()
    setError('')
    try {
      if (editing && form.id) {
        await api.updateBook(form.id, { title: form.title, description: form.description, price: form.price })
      } else {
        await api.createBook({ title: form.title, description: form.description, price: form.price })
      }
      await load()
      resetForm()
    } catch (err) {
      setError(err.message)
    }
  }

  async function remove(id) {
    if (!confirm('Delete this book?')) return
    setError('')
    try {
      await api.deleteBook(id)
      setBooks((b) => b.filter((x) => x.id !== id))
    } catch (err) {
      setError(err.message)
    }
  }

  return (
    <div className="app">
      <header>
        <h1>BookStore — API demo</h1>
        <p className="muted">A small single-page app to verify your BookStore API (GET/POST/PUT/DELETE)</p>
      </header>

      <main>
        <section className="list">
          <div className="list-header">
            <h2>Books</h2>
            <div>
              <button onClick={load} disabled={loading}>Refresh</button>
            </div>
          </div>

          {loading ? (
            <p>Loading…</p>
          ) : error ? (
            <p className="error">{error}</p>
          ) : books.length === 0 ? (
            <p>No books found.</p>
          ) : (
            <table>
              <thead>
                <tr>
                  <th>Title</th>
                  <th>Description</th>
                  <th>Price (UAH)</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {books.map((b) => (
                  <tr key={b.id}>
                    <td>{b.title}</td>
                    <td>{b.description}</td>
                    <td>{b.price}</td>
                    <td className="actions">
                      <button onClick={() => startEdit(b)}>Edit</button>
                      <button onClick={() => remove(b.id)} className="danger">Delete</button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </section>

        <aside className="form-area">
          <h2>{editing ? 'Edit Book' : 'Add Book'}</h2>
          <form onSubmit={submit} className="book-form">
            <label>
              Title
              <input value={form.title} onChange={(e) => setForm({ ...form, title: e.target.value })} required />
            </label>
            <label>
              Description
              <input value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} required />
            </label>
            <label>
              Price
              <input value={form.price} onChange={(e) => setForm({ ...form, price: e.target.value })} />
            </label>

            <div className="form-actions">
              <button type="submit">{editing ? 'Update' : 'Create'}</button>
              <button type="button" onClick={resetForm}>Clear</button>
            </div>
          </form>
        </aside>
      </main>

      <footer>
        <p className="muted">API base: {api && api.setBaseUrl ? 'configurable via VITE_API_BASE' : ''}</p>
      </footer>
    </div>
  )
}

export default App
