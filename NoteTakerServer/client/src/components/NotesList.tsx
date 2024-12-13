'use client';

import { Card, Badge, Button, Row, Col, Form } from 'react-bootstrap';
import { format } from 'date-fns';
import { getNoteTypeColor, formatDateTime } from '@/lib/utils';
import { Note, NoteType } from '@/types/note';
import { deleteNote, toggleNoteComplete } from '@/lib/api';
import { useAuth } from '@/ContextApi/AuthContext';

type Props = {
  notes: Note[];
  onUpdate: () => void;
};

export default function NotesList({ notes, onUpdate }: Props) {
  const { accessToken, user } = useAuth();

  const handleDeleteNote = async (noteId: string) => {
    if (noteId && accessToken) {
      await deleteNote(noteId, accessToken);
      onUpdate();
    }
  };

  const handleToggleComplete = async (noteId: string) => {
    if (noteId && accessToken) {
      debugger
      await toggleNoteComplete(noteId, accessToken);
      onUpdate();
    }
  };

  const renderNoteContent = (note: Note) => {
    switch (note.type) {
      case NoteType.Bookmark:
        return (
          <a href={note.url} target="_blank" rel="noopener noreferrer" className="text-decoration-none">
            {note.url}
          </a>
        );
      case NoteType.Todo:
        return (
          <>
            <p className="mb-2">{note.text}</p>
            {note.dueDate && (
              <p className="mb-2">
                <small className="text-muted">Due: {formatDateTime(note.dueDate.toString())}</small>
              </p>
            )}
            <div className="d-flex align-items-center">
              <Form.Check
                type="checkbox"
                checked={note.isComplete}
                onChange={() => handleToggleComplete(note.noteId ?? '')}
                label="Completed"
              />
            </div>
          </>
        );
      case NoteType.Reminder:
        return (
          <>
            <p className="mb-2">{note.text}</p>
            {note.reminder && (
              <p className="mb-0">
                <small className="text-muted">Reminder: {formatDateTime(note.reminder.toString())}</small>
              </p>
            )}
          </>
        );
      default:
        return <p className="mb-0">{note.text}</p>;
    }
  };

  return (
    <Row className="g-4">
      {notes.map((note) => (
        <Col key={note.noteId} xs={12} md={6} lg={4}>
          <Card className="h-100 shadow-sm">
            <Card.Header className="d-flex justify-content-between align-items-center">
              <Badge bg={getNoteTypeColor(note.type)} className="text-capitalize">
                {note.type}
              </Badge>
              <Button
                variant="outline-danger"
                size="sm"
                onClick={() => handleDeleteNote(note.noteId ?? '')}
              >
                <i className="bi bi-trash"></i>
              </Button>
            </Card.Header>
            <Card.Body>{renderNoteContent(note)}</Card.Body>
            <Card.Footer className="text-muted">
              <small>Created: {note.createdDate ? formatDateTime(note.createdDate.toString()) : 'Date Missing'}</small>
            </Card.Footer>
          </Card>
        </Col>
      ))}
      {notes.length === 0 && (
        <Col xs={12}>
          <div className="text-center py-5">
            <p className="text-muted mb-0">No notes found</p>
          </div>
        </Col>
      )}
    </Row>
  );
}