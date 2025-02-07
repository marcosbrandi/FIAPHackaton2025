# Health&Med - Sistema de Telemedicina

## Descrição do Projeto

A Health&Med é uma startup inovadora no setor de saúde, desenvolvendo um sistema proprietário para revolucionar a Telemedicina no país. O sistema visa proporcionar um serviço de maior qualidade, segurança dos dados dos pacientes e redução de custos, permitindo o gerenciamento eficiente de agendamentos e consultas online.

Este repositório contém o MVP (Minimum Viable Product) desenvolvido pelos alunos do curso 4NETT, que inclui a análise do projeto, arquitetura do software e desenvolvimento do sistema.

## Requisitos Funcionais

1. **Autenticação do Usuário (Médico)**
   - Login usando número de CRM e senha.

2. **Cadastro/Edição de Horários Disponíveis (Médico)**
   - Cadastro e edição de horários para agendamento de consultas.

3. **Aceite ou Recusa de Consultas Médicas (Médico)**
   - Aceitar ou recusar consultas agendadas.

4. **Autenticação do Usuário (Paciente)**
   - Login usando e-mail ou CPF e senha.

5. **Busca por Médicos (Paciente)**
   - Visualização da lista de médicos disponíveis com filtros por especialidade.

6. **Agendamento de Consultas (Paciente)**
   - Visualização da agenda do médico e valor da consulta, com possibilidade de agendamento e cancelamento mediante justificativa.

## Diagrama da Arquitetura

![Diagrama](https://github.com/user-attachments/assets/2d024a06-ecc1-4d8b-9b0a-056bc7e2a9f6)

## Escolhas Técnicas e Justificativas

### API Rest
Utilizamos uma API Rest para o backend devido à sua escalabilidade, facilidade de integração com diferentes frontends.

### SQL Server
O SQL Server foi escolhido como banco de dados por sua confiabilidade, recursos avançados de segurança, essenciais para um sistema de saúde.

### Docker
Docker foi adotado para conteinerização do sistema, garantindo consistência entre ambientes de desenvolvimento, teste e produção, além de facilitar a implantação e o escalonamento.

### Autenticação com Tokens (JWT)
A autenticação com tokens JWT foi escolhida por ser stateless, segura e de fácil implementação, garantindo a integridade e a confidencialidade das informações.

### C# com .NET 8
Utilizamos C# e .NET 8 para o backend devido à sua alta performance, suporte multiplataforma. O .NET 8 é uma versão LTS, com suporte de longo prazo.

### CI/CD
Implementamos um pipeline de CI/CD para automatizar testes e implantações, qualidade e agilidade no processo de entrega.

# Tecnologias Utilizadas

- **Backend**: API Rest
- **Banco de Dados**: SQL Server
- **Autenticação**: Token de Acesso
- **Infraestrutura**: Docker
- **CI/CD**: Pipeline de deploy

## Como Executar o Projeto

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/marcosbrandi/FIAPHM/tree/master

# Link do Projeto no YouTube
- 
<!--# Pessoas Contribuidoras-->

# Desenvolvedoras do Projeto

- Gustavo Amaral (gustavo-amaral@hotmail.com)
- Marcos Brandi Torres (marcosbrandi@hotmail.com)
- Jhonas Nobre (jhonas_nobre@hotmail.com)
- Júlio Valle (juliodovale2012@gmail.com)
