import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Login from "../auth/Login";
import Register from "../auth/Register";
import CreateShortUrl from "../pages/CreateShortUrl";
import Details from "../pages/Details";
import ProtectedRoute from "../components/ProtectedRoute";

export default function AppRouter() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />

                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />

                <Route
                    path="/create"
                    element={
                        <ProtectedRoute>
                            <CreateShortUrl />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/details/:id"
                    element={
                        <ProtectedRoute>
                            <Details />
                        </ProtectedRoute>
                    }
                />
            </Routes>
        </BrowserRouter>
    );
}
