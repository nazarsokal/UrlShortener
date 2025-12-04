import { useState } from "react";
import api from "../api/axios";
import { useNavigate } from "react-router-dom";

export default function Register() {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleRegister = async () => {
        try {
            await api.post("/auth/register", {
                username,
                email,
                password
            });

            navigate("/login");
        } catch (error) {
            console.error(error);
            alert("Registration failed!");
        }
    };

    const styles = {
        container: {
            maxWidth: "400px",
            margin: "50px auto",
            padding: "30px",
            borderRadius: "10px",
            boxShadow: "0 4px 15px rgba(0,0,0,0.2)",
            backgroundColor: "#f9f9f9",
            fontFamily: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
        },
        title: {
            textAlign: "center",
            marginBottom: "20px",
            color: "#1e293b"
        },
        input: {
            width: "100%",
            padding: "10px",
            marginBottom: "15px",
            borderRadius: "5px",
            border: "1px solid #ccc",
            fontSize: "14px"
        },
        button: {
            width: "100%",
            padding: "10px",
            borderRadius: "5px",
            border: "none",
            backgroundColor: "#4CAF50",
            color: "white",
            fontWeight: "bold",
            cursor: "pointer",
            transition: "0.2s"
        },
        buttonHover: {
            backgroundColor: "#3e8e41"
        }
    };

    return (
        <div style={styles.container}>
            <h2 style={styles.title}>Register</h2>

            <input
                placeholder="Username"
                value={username}
                onChange={e => setUsername(e.target.value)}
                style={styles.input}
            />
            <input
                placeholder="Email"
                value={email}
                onChange={e => setEmail(e.target.value)}
                style={styles.input}
            />
            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={e => setPassword(e.target.value)}
                style={styles.input}
            />

            <button
                style={styles.button}
                onClick={handleRegister}
                onMouseOver={e => e.currentTarget.style.backgroundColor = "#3e8e41"}
                onMouseOut={e => e.currentTarget.style.backgroundColor = "#4CAF50"}
            >
                Register
            </button>
        </div>
    );
}
