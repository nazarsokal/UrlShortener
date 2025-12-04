import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Header from "../components/Header";
import api from "../api/axios";
import jwtDecode from "jwt-decode";

export default function Home() {
    const [links, setLinks] = useState([]);
    const [user, setUser] = useState(null);
    const navigate = useNavigate();

    const token = localStorage.getItem("token");

    useEffect(() => {
        if (token) {
            try {
                const decoded = jwtDecode(token);
                setUser({
                    id: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
                    roles: [decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]]
                });
            } catch (err) {
                console.error("Invalid token", err);
            }
        }

        loadLinks();
    }, []);

    const loadLinks = async () => {
        try {
            const res = await api.get("/urlshortener/getall");
            setLinks(res.data);
        } catch (err) {
            console.log("API error", err);
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm("Are you sure?")) return;

        try {
            await api.delete(`/urlshortener/delete/${id}`, {
                headers: { Authorization: `Bearer ${token}` }
            });
            loadLinks();
        } catch (err) {
            alert("You are not authorized to delete this link!");
        }
    };

    const canDelete = (link) => {
        if (!user) return false;
        return user.roles.includes("ADMIN") || user.id === link.userIdCreatedBy;
    };

    // Стилі
    const styles = {
        container: { padding: "20px", fontFamily: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif" },
        button: {
            padding: "8px 15px",
            border: "none",
            borderRadius: "5px",
            cursor: "pointer",
            transition: "0.2s",
        },
        buttonPrimary: {
            backgroundColor: "#4CAF50",
            color: "white",
            marginRight: "10px",
        },
        buttonDanger: {
            backgroundColor: "#f44336",
            color: "white",
        },
        buttonDisabled: {
            backgroundColor: "#ccc",
            color: "#666",
            cursor: "not-allowed",
        },
        table: {
            width: "100%",
            borderCollapse: "collapse",
            boxShadow: "0 2px 10px rgba(0,0,0,0.1)",
        },
        th: {
            backgroundColor: "#1976d2",
            color: "white",
            padding: "12px",
            textAlign: "left",
        },
        td: {
            padding: "10px",
            borderBottom: "1px solid #ddd",
        },
        rowEven: {
            backgroundColor: "#f9f9f9",
        },
        rowOdd: {
            backgroundColor: "#fff",
        },
        link: { color: "#1976d2", textDecoration: "none" },
    };

    return (
        <div>
            <Header />
            <div style={styles.container}>
                <div style={{ marginBottom: "20px" }}>
                    <button
                        style={{ ...styles.button, ...styles.buttonPrimary }}
                        onClick={() => navigate("/create")}
                        disabled={!token}
                    >
                        Create Short URL
                    </button>

                    {!token && (
                        <span style={{ marginLeft: "10px", color: "gray" }}>
                            (Login to create links)
                        </span>
                    )}
                </div>

                <table style={styles.table}>
                    <thead>
                        <tr>
                            <th style={styles.th}>Name</th>
                            <th style={styles.th}>Original</th>
                            <th style={styles.th}>Short</th>
                            <th style={styles.th}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {links.map((link, index) => (
                            <tr
                                key={link.id}
                                style={index % 2 === 0 ? styles.rowEven : styles.rowOdd}
                            >
                                <td style={styles.td}>{link.name}</td>
                                <td style={styles.td}>
                                    <a
                                        href={link.originalLink}
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        style={styles.link}
                                    >
                                        {link.originalLink}
                                    </a>
                                </td>
                                <td style={styles.td}>
                                    <a
                                        href={link.shortenedLink}
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        style={styles.link}
                                    >
                                        {link.shortenedLink}
                                    </a>
                                </td>
                                <td style={styles.td}>
                                    <button
                                        style={{ ...styles.button, ...styles.buttonPrimary }}
                                        onClick={() => navigate(`/details/${link.id}`)}
                                        disabled={!token}
                                    >
                                        Details
                                    </button>

                                    <button
                                        style={{
                                            ...styles.button,
                                            ...styles.buttonDanger,
                                            ...(canDelete(link) ? {} : styles.buttonDisabled),
                                            marginLeft: "10px",
                                        }}
                                        onClick={() => handleDelete(link.id)}
                                        disabled={!canDelete(link)}
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
