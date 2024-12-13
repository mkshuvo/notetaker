import type { Note } from '@/types/note';

const API_BASE_URL = 'https://localhost:7124/api';

async function authFetch(url: string, token: string | null, options: RequestInit = {}): Promise<Response> {
  const headers = {
    'Content-Type': 'application/json',
    ...(token ? { 'Authorization': `Bearer ${token}` } : {}),
    ...options.headers,
  };
  
  const response = await fetch(url, { ...options, headers });
  if (!response.ok) {
    const error = await response.json().catch(() => ({}));
    throw new Error(error.message || `HTTP error! status: ${response.status}`);
  }
  return response;
}

export async function fetchNotes(userId: string, token: string | null,): Promise<Note[]> {
  try {
    const response = await authFetch(`${API_BASE_URL}/notes/user/${userId}`, token);
    return response.json();
  } catch (error) {
    console.error('Error fetching notes:', error);
    return [];
  }
}

export async function createNote(noteData: Partial<Note>, token: string | null): Promise<Note> {
  const response = await authFetch(`${API_BASE_URL}/notes`, token, {
    method: 'POST',
    body: JSON.stringify(noteData),
  });
  return response.json();
}

export async function deleteNote(id: string, token: string | null): Promise<void> {
  await authFetch(`${API_BASE_URL}/notes/${id}`, token, {
    method: 'DELETE',
  });
}

export async function toggleNoteComplete(id: string, token: string | null): Promise<void> {
  await authFetch(`${API_BASE_URL}/notes/${id}/complete`, token, {
    method: 'PUT',
  });
}