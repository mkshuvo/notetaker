export async function checkAuth() {
  const response = await fetch('/api/auth/check');
  return response.ok;
}

export async function logout() {
  try {
    await fetch('/api/logout', { method: 'POST' });
    return true;
  } catch (error) {
    console.error('Error logging out:', error);
    return false;
  }
}