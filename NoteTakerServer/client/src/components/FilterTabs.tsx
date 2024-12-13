'use client';

import { Nav } from 'react-bootstrap';

type Props = {
  activeFilter: string;
  onFilterChange: (filter: string) => void;
};

export default function FilterTabs({ activeFilter, onFilterChange }: Props) {
  return (
    <Nav variant="pills" className="mb-3">
      <Nav.Item>
        <Nav.Link
          active={activeFilter === 'all'}
          onClick={() => onFilterChange('all')}
        >
          All Notes
        </Nav.Link>
      </Nav.Item>
      <Nav.Item>
        <Nav.Link
          active={activeFilter === 'today'}
          onClick={() => onFilterChange('today')}
        >
          Today
        </Nav.Link>
      </Nav.Item>
      <Nav.Item>
        <Nav.Link
          active={activeFilter === 'week'}
          onClick={() => onFilterChange('week')}
        >
          This Week
        </Nav.Link>
      </Nav.Item>
      <Nav.Item>
        <Nav.Link
          active={activeFilter === 'month'}
          onClick={() => onFilterChange('month')}
        >
          This Month
        </Nav.Link>
      </Nav.Item>
    </Nav>
  );
}