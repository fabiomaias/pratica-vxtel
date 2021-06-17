import axios from "axios";

process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';
const api = axios
            .create({
                baseURL: "https://localhost:5001"
            });

export default api;