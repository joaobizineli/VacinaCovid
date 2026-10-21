# 💉 Vacina Covid

Sistema de agendamento de vacinação contra a COVID-19. Permite cadastrar pacientes, agendar doses respeitando regras de capacidade por dia e por horário, e acompanhar o status de cada agendamento (agendado, realizado ou cancelado).

O projeto é dividido em duas partes:

| Parte | Stack |
|---|---|
| **Backend** | .NET 8 · ASP.NET Core Web API · Entity Framework Core · SQL Server · FluentValidation · log4net · Swagger |
| **Frontend** | React 19 · TypeScript · Vite · TanStack Router · Zustand · Tailwind CSS 4 · shadcn/ui |

---

## ✨ Funcionalidades

- **Pacientes:** cadastro, listagem e remoção.
- **Agendamentos:** criação de agendamento com paciente, vacina, data, horário e local, com painel de estatísticas (total, agendados, realizados, cancelados).
- **Consulta:** listagem de todos os agendamentos com filtros por paciente, vacina, local, data e status, e ações para marcar como realizado ou cancelar.

### Regras de negócio

**Pacientes**
- Nome e data de nascimento são obrigatórios.
- A data de nascimento não pode estar no futuro.

**Agendamentos**
- O paciente precisa existir.
- Não é possível agendar em datas passadas.
- Máximo de **20 agendamentos por dia**.
- Máximo de **2 agendamentos no mesmo horário**.
- Agendamentos em horários diferentes no mesmo dia precisam ter **pelo menos 1 hora de intervalo**.
- O local de vacinação é obrigatório e deve ter entre 3 e 100 caracteres.
- Só agendamentos com status `Agendado` podem ser marcados como realizados ou cancelados.
- Agendamentos já realizados ou cancelados não podem ser removidos.

---

## 📁 Estrutura do projeto

```
VacinaCovid/
├── .sql                         # Script de criação das tabelas + dados de exemplo
├── backend/VacinaCovid/
│   ├── VacinaCovid/                         # Web API (controllers, middleware, configuração)
│   ├── VacinaCovid.Entidade/                # Entidades, DTOs, models e enums
│   ├── VacinaCovid.Negocios/                # Regras de negócio
│   ├── VacinaCovid.Negocios.Interface/      # Contratos da camada de negócio
│   ├── VacinaCovid.Repositorio/             # DbContext, mapeamentos EF e repositórios
│   ├── VacinaCovid.Repositorio.Interface/   # Contratos dos repositórios
│   ├── VacinaCovid.Validador/               # Validações (FluentValidation)
│   ├── VacinaCovid.Utilitarios/             # Exceptions, mensagens (.resx) e resposta padrão
│   ├── VacinaCovid.TestesUnitario/          # Testes (NUnit + Moq + EF InMemory)
│   └── VacinaCovid.slnx
└── frontend/
    └── src/
        ├── pages/        # Agendamentos, Pacientes, Consulta
        ├── components/   # Componentes por domínio + ui (shadcn)
        ├── hooks/        # Lógica das telas
        ├── store/        # Estado global (Zustand)
        ├── services/     # Chamadas à API
        ├── routes/       # Rotas (TanStack Router)
        └── types/
```

O backend segue uma arquitetura em camadas: **Controller → Negócio → Repositório → EF Core**. Um middleware (`ApiMiddleware`) abre uma transação por requisição, faz commit em caso de sucesso ou rollback em caso de erro, registra o tempo de execução no log e converte exceções em uma resposta JSON padronizada:

```json
{ "HttpStatus": 400, "Messages": ["Limite de 20 agendamentos por dia já atingido."], "Data": null }
```

Erros de negócio retornam `400`; erros inesperados retornam `500`.

---

## 🚀 Como rodar

### Pré-requisitos

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) 20+ (ou a versão exigida pelo Vite 8)
- SQL Server (o padrão é o **LocalDB** `(localdb)\MSSQLLocalDB`)

### 1. Banco de dados

1. Crie um banco chamado `VacinaCovid`.
2. Execute o script [`.sql`](./.sql) nele. Ele cria as tabelas `tb_paciente` e `tb_agendamento` e insere 15 pacientes e agendamentos de exemplo para maio de 2026.

Se for usar outro servidor, ajuste a connection string em `backend/VacinaCovid/VacinaCovid/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=VacinaCovid;Trusted_Connection=true;MultipleActiveResultSets=true;"
}
```

### 2. Backend

```bash
cd backend/VacinaCovid
dotnet restore
dotnet run --project VacinaCovid --launch-profile https
```

A API sobe em `https://localhost:7262` (e `http://localhost:5059`). O **Swagger** fica na raiz: <https://localhost:7262>.

> Se o navegador reclamar do certificado, rode `dotnet dev-certs https --trust` uma vez.

### 3. Frontend

```bash
cd frontend
npm install
npm run dev
```

Acesse <http://localhost:5173>. A URL da API está definida em `frontend/src/services/api/api.ts`, e o backend só libera CORS para `http://localhost:5173`. Se mudar alguma das portas, ajuste os dois lugares (`api.ts` e a policy `AllowFrontend` em `Startup.cs`).

### Scripts do frontend

| Comando | O que faz |
|---|---|
| `npm run dev` | Servidor de desenvolvimento |
| `npm run build` | Checagem de tipos + build de produção |
| `npm run preview` | Serve o build localmente |
| `npm run lint` | ESLint |

---

## 🧪 Testes

```bash
cd backend/VacinaCovid
dotnet test
```

Há dois conjuntos de testes da camada de negócio:

- **`Mock/`**: repositórios mockados com Moq.
- **`BancoEmMemoria/`**: repositórios reais sobre o provider InMemory do EF Core.

---

## 📡 Endpoints

Base: `https://localhost:7262/api`

### Pacientes — `/Pacientes`

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/InserirPaciente` | Cadastra um paciente |
| `GET` | `/ListarPacientes` | Lista todos os pacientes |
| `GET` | `/ObterPaciente?id={id}` | Busca um paciente |
| `PUT` | `/AlterarPaciente?id={id}` | Atualiza um paciente |
| `DELETE` | `/RemoverPaciente?id={id}` | Remove um paciente |

```json
// POST /InserirPaciente
{ "nome": "Ana Lima", "dataNascimento": "1990-03-15T00:00:00" }
```

### Agendamentos — `/VacinaPacientes`

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/AgendarVacina` | Cria um agendamento |
| `GET` | `/ListarAgendamentos` | Lista todos (ordenados por data e hora) |
| `GET` | `/ListarAgendamentosPaciente?idPaciente={id}` | Agendamentos de um paciente |
| `GET` | `/ListarAgendamentosPorData?data={yyyy-MM-dd}` | Agendamentos de um dia |
| `GET` | `/ObterAgendamento?id={id}` | Busca um agendamento |
| `PUT` | `/MarcarComoRealizado?id={id}` | Muda o status para `Realizado` |
| `PUT` | `/CancelarAgendamento?id={id}` | Muda o status para `Cancelado` |
| `DELETE` | `/RemoverAgendamento?id={id}` | Remove um agendamento ainda `Agendado` |

```json
// POST /AgendarVacina
{
  "idPaciente": 1,
  "idVacina": 1,
  "dataVacinacao": "2026-11-10T00:00:00",
  "horaVacinacao": "09:00:00",
  "localVacinacao": "UBS Centro"
}
```

Vacinas disponíveis (IDs usados pelo frontend):

| ID | Vacina |
|---|---|
| 1 | Pfizer |
| 2 | AstraZeneca |
| 3 | Coronavac |

---

## ⚙️ Observações

- **Logs:** o log4net grava em um caminho absoluto definido em `backend/VacinaCovid/VacinaCovid/log4net.config` (`<file value="..."/>`). Troque para um caminho válido na sua máquina, por exemplo `logs\WebApi.log`.
- **Tabela de vacinas:** existe a entidade `Vacina` mapeada para `tb_vacina`, mas o script `.sql` não cria essa tabela e as vacinas estão fixas no frontend. A API funciona normalmente sem ela, já que nenhum endpoint consulta vacinas.
