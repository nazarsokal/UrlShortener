import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../api/axios";

export default function Details() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [link, setLink] = useState(null);

    useEffect(() => {
        loadDetails();
    }, []);

    const loadDetails = async () => {
        const res = await api.get(`/urlshortener/get/${id}`);
        setLink(res.data);
    };

    if (!link) return <p>Loading...</p>;

    return (
        <div style={{ padding: "20px" }}>
            <h2>Link Details</h2>

            <p><b>Original:</b> {link.originalLink}</p>
            <p><b>Short:</b> {link.shortenedLink}</p>
            <p><b>Created:</b> {new Date(link.createdAt).toLocaleString()}</p>

            <button onClick={() => navigate("/")}>Back</button>
        </div>
    );
}
