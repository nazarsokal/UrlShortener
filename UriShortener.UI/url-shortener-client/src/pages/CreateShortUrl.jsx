import { useState } from "react";
import api from "../api/axios";
import Header from "../components/Header";

export default function CreateShortUrl() {
    const [url, setUrl] = useState("");
    const [name, setName] = useState("");
    const [shortLink, setShortLink] = useState("");

    const token = localStorage.getItem("token");

    // Генерація короткого посилання через GET
    const handleGenerate = async () => {
        if (!token) {
            alert("Please login first!");
            return;
        }

        if (!url) {
            alert("Please enter URL");
            return;
        }

        try {
            const res = await api.get("/urlshortener/generate", {
                params: { originalUrl: url },
                headers: { Authorization: `Bearer ${token}` }
            });

            setShortLink(res.data);
        } catch (err) {
            console.error(err);
            alert("Failed to generate short link");
        }
    };

    // Збереження посилання через POST
    const handleSave = async () => {
        if (!token) {
            alert("Please login first!");
            return;
        }

        if (!url) {
            alert("Please enter URL");
            return;
        }

        try {
            const payload = {
                originalLink: url,
                name: name,
                ...(shortLink && { shortenUrl: shortLink })
            };

            await api.post("/urlshortener/create", payload, {
                headers: { Authorization: `Bearer ${token}` }
            });

            alert("Link saved successfully!");
            // Очистка полів
            setUrl("");
            setName("");
            setShortLink("");
        } catch (err) {
            console.error(err);
            alert("Failed to save link");
        }
    };

    // Стилі для форми
    const styles = {
        container: { padding: "20px", fontFamily: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif" },
        input: { display: "block", marginBottom: "15px", padding: "8px", width: "100%", borderRadius: "5px", border: "1px solid #ccc" },
        button: { padding: "8px 15px", marginRight: "10px", border: "none", borderRadius: "5px", cursor: "pointer", fontWeight: "bold" },
        generateBtn: { backgroundColor: "#4CAF50", color: "white" },
        saveBtn: { backgroundColor: "#1976d2", color: "white" },
        shortLinkInput: { width: "100%", marginTop: "5px", padding: "8px", borderRadius: "5px", border: "1px solid #ccc" },
        label: { fontWeight: "bold" }
    };

    return (
        <div>
            <Header />

            <div style={styles.container}>
                <h2>Create Short URL</h2>

                <input
                    placeholder="Enter Name"
                    value={name}
                    onChange={e => setName(e.target.value)}
                    style={styles.input}
                />

                <input
                    placeholder="Enter URL"
                    value={url}
                    onChange={e => setUrl(e.target.value)}
                    style={styles.input}
                />

                <div style={{ marginBottom: "15px" }}>
                    <button onClick={handleGenerate} style={{ ...styles.button, ...styles.generateBtn }}>
                        Generate
                    </button>
                    <button onClick={handleSave} style={{ ...styles.button, ...styles.saveBtn }}>
                        Save
                    </button>
                </div>

                {shortLink && (
                    <div>
                        <label style={styles.label}>Generated Short Link:</label>
                        <input
                            type="text"
                            value={shortLink}
                            readOnly
                            style={styles.shortLinkInput}
                            onClick={(e) => e.target.select()} // щоб можна було швидко скопіювати
                        />
                    </div>
                )}
            </div>
        </div>
    );
}
