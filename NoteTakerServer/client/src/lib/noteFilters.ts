import { format } from 'date-fns';
import type { Note } from '@/types/note';

export function filterNotesByDate(notes: Note[], filter: string): Note[] {
  const today = new Date();
  const thisWeek = new Date(today);
  thisWeek.setDate(today.getDate() + 7);
  const thisMonth = new Date(today);
  thisMonth.setMonth(today.getMonth() + 1);

  switch (filter) {
    case 'today':
      return notes.filter((note) => {
        if (note.type === 'Reminder' || note.type === 'Todo') {
          const date = new Date(note.dueDate || '');
          return format(date, 'yyyy-MM-dd') === format(today, 'yyyy-MM-dd');
        }
        return false;
      });
    case 'week':
      return notes.filter((note) => {
        if (note.type === 'Reminder' || note.type === 'Todo') {
          const date = new Date(note.dueDate || '');
          return date <= thisWeek;
        }
        return false;
      });
    case 'month':
      return notes.filter((note) => {
        if (note.type === 'Reminder' || note.type === 'Todo') {
          const date = new Date(note.dueDate || '');
          return date <= thisMonth;
        }
        return false;
      });
    default:
      return notes;
  }
}