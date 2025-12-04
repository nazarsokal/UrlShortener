import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../api/axios";
import Header from "../components/Header";

export default function Details() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [link, setLink] = useState(null);

    useEffect(() => {
        loadDetails();
    }, []);

    const loadDetails = async () => {
        try {
            const res = await api.get(`/urlshortener/get/${id}`);
            setLink(res.data);
        } catch (err) {
            console.error(err);
            alert("Failed to load details");
        }
    };

    if (!link) return <p style={{ padding: "20px" }}>Loading...</p>;

    return (
        <>
            {/* ✅ ХЕДЕР */}
            <Header />

            {/* ✅ КОНТЕНТ */}
            <div style={styles.container}>
                <h2 style={styles.title}>🔗 Link Details</h2>

                <div style={styles.card}>
                    <p><b>Name:</b> {link.name || "—"}</p>

                    <p>
                        <b>Original:</b>{" "}
                        <a href={link.originalLink} target="_blank" rel="noopener noreferrer">
                            {link.originalLink}
                        </a>
                    </p>

                    <p>
                        <b>Short:</b>{" "}
                        <a href={link.shortenedLink} target="_blank" rel="noopener noreferrer">
                            {link.shortenedLink}
                        </a>
                    </p>

                    <p>
                        <b>Created:</b>{" "}
                        {new Date(link.createdAt).toLocaleString()}
                    </p>
                </div>

                <button onClick={() => navigate("/")} style={styles.backBtn}>
                    ⬅ Back
                </button>
            </div>
        </>
    );
}

/* ✅ СТИЛІ */
const styles = {
    container: {
        maxWidth: "700px",
        margin: "40px auto",
        padding: "20px",
        fontFamily: "'Segoe UI', Tahoma, sans-serif"
    },
    title: {
        textAlign: "center",
        marginBottom: "20px",
        color: "#1e293b"
    },
    card: {
        background: "#f8fafc",
        padding: "20px",
        borderRadius: "10px",
        boxShadow: "0 4px 12px rgba(0,0,0,0.15)",
        marginBottom: "20px",
        lineHeight: "1.8"
    },
    backBtn: {
        padding: "10px 18px",
        borderRadius: "6px",
        border: "none",
        background: "#1e293b",
        color: "white",
        cursor: "pointer",
        fontWeight: "bold"
    }
};
