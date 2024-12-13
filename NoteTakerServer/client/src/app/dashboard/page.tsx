'use client';

import { useState, useEffect, useCallback } from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import CreateNoteModal from '@/components/CreateNoteModal';
import NotesList from '@/components/NotesList';
import Header from '@/components/Header';
import FilterTabs from '@/components/FilterTabs';
import { filterNotesByDate } from '@/lib/noteFilters';
import { Note } from '@/types/note';
import { useAuth } from '@/ContextApi/AuthContext';
import { useRouter } from 'next/navigation';
import { fetchNotes } from '@/lib/api';

export default function Dashboard() {
  const router = useRouter();
  const { accessToken, user } = useAuth();
  const [isLoading, setIsLoading] = useState(true);
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [notes, setNotes] = useState<Note[]>([]);
  const [filter, setFilter] = useState('all');

  const fetchAndSetNotes = async () => {
    if (!user?.userId || !accessToken) {
      console.error('User not authenticated');
      return;
    }

    try {
      const fetchedNotes = await fetchNotes(user.userId, accessToken);
      setNotes(fetchedNotes);
    } catch (error) {
      console.error('Error fetching notes:', error);
    }
  };

  useEffect(() => {
    if (!accessToken) {
      router.push('/login');
      return;
    }

    setIsLoading(true);
    fetchAndSetNotes().finally(() => setIsLoading(false));
  }, [accessToken, user?.userId, router]);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  const filteredNotes = filterNotesByDate(notes, filter);

  return (
    <Container fluid>
      <Header />

      <Row className="mb-4">
        <Col>
          <FilterTabs activeFilter={filter} onFilterChange={setFilter} />
          <Button onClick={() => setShowCreateModal(true)}>Create New Note</Button>
        </Col>
      </Row>

      <NotesList
        notes={filteredNotes}
        onUpdate={fetchAndSetNotes}
      />

      <CreateNoteModal
        show={showCreateModal}
        onHide={() => setShowCreateModal(false)}
        onSave={fetchAndSetNotes}
      />
    </Container>
  );
}
