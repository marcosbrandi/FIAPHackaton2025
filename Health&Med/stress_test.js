import http from 'k6/http';
import { sleep } from 'k6';

// Configuração do teste
export const options = {
    stages: [
        { duration: '1m', target: 333 },  // Aumenta para 20 usuários em 1 minuto
        // Adicione mais estágios conforme necessário
    ],
};

// Função que simula as ações dos usuários
export default function () {
    const url = 'https://172.30.43.225:63238/api/Agenda/ListarAgendas'; // Endpoint da API
    const headers = {
        'accept': '*/*',
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IjMyMTMyMSIsImp0aSI6Ik1lZGljbyIsInByb2ZpbGUiOiJNZWRpY28iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNZWRpY28iLCJyb2xlcyI6Ik1lZGljbyIsInJvbGUiOiJNZWRpY28iLCJleHAiOjE3MzkwNTU5NzMsImlzcyI6IkZJQVBITUFQSSIsImF1ZCI6IkZJQVBITUFQSSJ9.R7NOL2KZmlJdeH0onhU5B4z-FXK6hPA5O6kOvN6OQZk'
    };

    const response = http.get(url, {
        headers: headers,
        insecureSkipTLSVerify: true // Ignora a verificação do certificado SSL
    });

    console.log(response.body); // Exibe o corpo da resposta no console
    sleep(1); // Simula um tempo de espera entre as requisições (1 segundo)
}
