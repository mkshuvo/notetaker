export enum NoteType {
  Regular = 'Regular',
  Reminder = 'Reminder',
  Todo = 'Todo',
  Bookmark = 'Bookmark',
}

export interface Note {
  noteId?: string;
  noteOwner?: string;
  type: NoteType;
  text?: string;
  reminder?: Date;
  dueDate?: Date;
  isComplete?: boolean;
  url?: string;
  createdDate?: Date;
}