import { format } from 'date-fns';

export function getNoteTypeColor(type: string): string {
  switch (type) {
    case 'Regular':
      return 'primary';
    case 'Reminder':
      return 'warning';
    case 'Todo':
      return 'success';
    case 'Bookmark':
      return 'info';
    default:
      return 'secondary';
  }
}

export function formatDateTime(dateString?: string): string {
  if (!dateString) return '';
  return format(new Date(dateString), 'PPp');
}