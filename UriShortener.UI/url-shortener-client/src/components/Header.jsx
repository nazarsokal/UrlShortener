import { Link, useNavigate } from "react-router-dom";

export default function Header() {
    const token = localStorage.getItem("token");
    const username = localStorage.getItem("username");
    const navigate = useNavigate();

    const logout = () => {
        localStorage.clear();
        navigate("/login");
    };

    return (
        <div style={styles.header}>
            {/* Заголовок, клікабельний */}
            <h2
                style={styles.title}
                onClick={() => navigate("/")}
            >
                URL Shortener
            </h2>

            <div>
                {token ? (
                    <>
                        <span style={{ marginRight: "15px", fontSize: "24px" }}>{username}</span>
                        <button
                            onClick={logout}
                            style={{ ...styles.btn, ...styles.dangerBtn }}
                        >
                            Logout
                        </button>
                    </>
                ) : (
                    <>
                        <Link to="/login">
                            <button style={{ ...styles.btn, ...styles.primaryBtn }}>Login</button>
                        </Link>
                        <Link to="/register">
                            <button style={{ ...styles.btn, ...styles.secondaryBtn }}>Register</button>
                        </Link>
                    </>
                )}
            </div>
        </div>
    );
}

const styles = {
    header: {
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "15px 30px",
        background: "#1e293b",
        color: "white",
        boxShadow: "0 2px 8px rgba(0,0,0,0.15)"
    },
    title: {
        cursor: "pointer",
        margin: 0,
        fontSize: "24px",
        color: "white",
        userSelect: "none"
    },
    btn: {
        marginLeft: "10px",
        padding: "6px 14px",
        border: "none",
        borderRadius: "5px",
        cursor: "pointer",
        fontWeight: "bold",
        transition: "0.2s",
    },
    primaryBtn: {
        backgroundColor: "#4CAF50",
        color: "white",
    },
    secondaryBtn: {
        backgroundColor: "#1976d2",
        color: "white",
    },
    dangerBtn: {
        backgroundColor: "#f44336",
        color: "white",
    }
};
