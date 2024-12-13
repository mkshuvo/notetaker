'use client';

import { Row, Col, Button } from 'react-bootstrap';
import { useRouter } from 'next/navigation';
import { useAuth } from '@/ContextApi/AuthContext';

export default function Header() {
  const router = useRouter();
  const { logout } = useAuth();
  const handleLogout = async () => {
    logout();
    router.push('/login');
  };

  return (
    <Row className="mb-4 py-3 bg-primary text-white">
      <Col>
        <h1 className="h4 mb-0">Notes Dashboard</h1>
      </Col>
      <Col xs="auto">
        <Button variant="light" onClick={handleLogout}>
          Logout
        </Button>
      </Col>
    </Row>
  );
}