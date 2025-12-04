import { useState } from "react";
import api from "../api/axios";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [userName, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleLogin = async () => {
        try {
            const res = await api.post("/auth/login", {
                userName,
                email,
                password
            });

            localStorage.setItem("token", res.data.token);
            localStorage.setItem("username", res.data.returnUserDto.userName);

            navigate("/");
        } catch (error) {
            console.error(error);
            alert("Login failed!");
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
            backgroundColor: "#1976d2",
            color: "white",
            fontWeight: "bold",
            cursor: "pointer",
            transition: "0.2s"
        },
        buttonHover: {
            backgroundColor: "#155fa0"
        }
    };

    return (
        <div style={styles.container}>
            <h2 style={styles.title}>Login</h2>

            <input
                placeholder="Username"
                value={userName}
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
                onClick={handleLogin}
                onMouseOver={e => e.currentTarget.style.backgroundColor = "#155fa0"}
                onMouseOut={e => e.currentTarget.style.backgroundColor = "#1976d2"}
            >
                Login
            </button>
        </div>
    );
}
