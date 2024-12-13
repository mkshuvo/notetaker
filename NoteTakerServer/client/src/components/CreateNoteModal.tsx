import React, { useState } from 'react';
import { Modal, Form, Button, Alert } from 'react-bootstrap';
import { useAuth } from '@/ContextApi/AuthContext';
import { createNote } from '@/lib/api';
import { Note, NoteType } from '@/types/note';

interface CreateNoteModalProps {
  show: boolean;
  onHide: () => void;
  onSave: () => void;
}

export default function CreateNoteModal({ show, onHide, onSave }: CreateNoteModalProps) {
  const { accessToken } = useAuth();
  const [noteType, setNoteType] = useState<NoteType>(NoteType.Regular);
  const [text, setText] = useState('');
  const [dueDate, setDueDate] = useState('');
  const [url, setUrl] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (text.length > 100) {
      setError('Note text cannot exceed 100 characters');
      return;
    }

    try {
      const noteData: Partial<Note> = {
        type: noteType,
        text,
        createdDate: new Date(),
      };

      if ((noteType === NoteType.Todo || noteType === NoteType.Reminder) && dueDate) {
        noteData.dueDate = new Date(dueDate);
      }
      if (noteType === NoteType.Bookmark && url) {
        noteData.url = url;
      }
      if (noteType === NoteType.Todo) {
        noteData.isComplete = false;
      }

      await createNote(noteData, accessToken);
      onSave();
      handleClose();
    } catch (err) {
      setError('An error occurred while creating the note');
      console.error(err);
    }
  };

  const handleClose = () => {
    setNoteType(NoteType.Regular);
    setText('');
    setDueDate('');
    setUrl('');
    setError('');
    onHide();
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Create New Note</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {error && <Alert variant="danger">{error}</Alert>}
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Note Type</Form.Label>
            <Form.Control as="select" value={noteType} onChange={(e) => setNoteType(e.target.value as Note['type'])}>
              <option value="Regular">Regular</option>
              <option value="Todo">Todo</option>
              <option value="Reminder">Reminder</option>
              <option value="Bookmark">Bookmark</option>
            </Form.Control>
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Label>Text</Form.Label>
            <Form.Control as="textarea" rows={3} value={text} onChange={(e) => setText(e.target.value)} required />
          </Form.Group>
          {(noteType === 'Todo' || noteType === 'Reminder') && (
            <Form.Group className="mb-3">
              <Form.Label>Due Date</Form.Label>
              <Form.Control type="datetime-local" value={dueDate} onChange={(e) => setDueDate(e.target.value)} required />
            </Form.Group>
          )}
          {noteType === 'Bookmark' && (
            <Form.Group className="mb-3">
              <Form.Label>URL</Form.Label>
              <Form.Control type="url" value={url} onChange={(e) => setUrl(e.target.value)} required />
            </Form.Group>
          )}
          <Button variant="primary" type="submit">
            Create Note
          </Button>
        </Form>
      </Modal.Body>
    </Modal>
  );
}