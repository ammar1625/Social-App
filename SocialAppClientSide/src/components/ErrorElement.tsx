import React from 'react';
import { Link } from 'react-router-dom';

 function ErrorElement() {
  return (
    <div className="flex min-h-screen flex-col items-center justify-center bg-gray-50 px-4 text-center">
      {/* Icon or Emoji */}
      <div className="mb-4 text-6xl" role="img" aria-label="Warning">
        ⚠️
      </div>

      {/* Error Message */}
      <h1 className="mb-2 text-2xl font-bold text-gray-800">Oops! Page Not Found</h1>
      <p className="mb-6 max-w-sm text-gray-600">
        The page you're looking for doesn't exist or has been moved.
      </p>

      {/* Action Button */}
      <Link
        to="/"
        className="rounded-lg bg-blue-600 px-5 py-2.5 text-sm font-medium text-white shadow-md transition hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
      >
        Go Home
      </Link>
    </div>
  );
};

export default ErrorElement;