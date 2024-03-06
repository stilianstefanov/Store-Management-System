import React, { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext();

export function useAuth() {
  return useContext(AuthContext);
}

export const AuthProvider = ({ children }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    const token = sessionStorage.getItem('token');
    const isTokenValid = token && JSON.parse(atob(token.split('.')[1])).exp * 1000 >= new Date().getTime();
    setIsLoggedIn(!!isTokenValid);
  }, []);

  const login = (token) => {
    sessionStorage.setItem('token', token);
    setIsLoggedIn(true);
  };

  const logout = () => {
    sessionStorage.removeItem('token');
    setIsLoggedIn(false);
  };

  return (
    <AuthContext.Provider value={{ isLoggedIn, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
