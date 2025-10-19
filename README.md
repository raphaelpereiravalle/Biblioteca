# 📚 Biblioteca TJRJ

Sistema completo para gerenciamento de biblioteca, com backend em .NET 8, frontend em Angular 17 e banco de dados SQL Server.  
O projeto foi desenvolvido com foco em arquitetura limpa, boas práticas e integração entre camadas (API RESTful + Frontend SPA).

---

## 🏗️ Estrutura do Projeto

```
Biblioteca-TJRJ/
│
├── backend/                  # API em .NET 8
│   ├── Biblioteca.API/       # Camada de apresentação (controllers, DTOs, configuração)
│   ├── Biblioteca.Domain/    # Entidades e interfaces de domínio
│   ├── Biblioteca.Infra/     # Persistência e acesso ao banco (Entity Framework Core)
│   └── Biblioteca.Tests/     # Testes unitários
│
├── frontend/                 # Aplicação Angular
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/   # Componentes (autor, assunto, livro, etc.)
│   │   │   ├── services/     # Serviços para consumir a API
│   │   │   └── models/       # Interfaces TypeScript
│   └── angular.json
│
└── database/
    ├── scripts/
    │   └── 01_create_tables.sql
    └── seed/
        └── 02_seed_data.sql
```

---

## ⚙️ Tecnologias Utilizadas

### 🔹 Backend (.NET 8 / C#)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger / OpenAPI
- CORS habilitado para integração com o Angular

### 🔹 Frontend (Angular 17)
- Angular CLI
- Bootstrap 5
- TypeScript / RxJS
- Componentização e modularização
- Integração com a API via `HttpClient`

### 🔹 Banco de Dados
- Microsoft SQL Server 2022
- Scripts de criação e carga inicial localizados em `/database/scripts`

---

## 🚀 Como Instalar e Executar o Projeto

### ✅ Pré-requisitos

| Tecnologia | Versão recomendada |
|-------------|-------------------|
| .NET SDK | 8.0+ |
| Node.js | 20.x |
| Angular CLI | 17.x |
| SQL Server | 2022 |
| Git | Última versão |

---

## 🧩 1️⃣ Clonar o Repositório

```bash
git clone https://github.com/seu-usuario/Biblioteca-TJRJ.git
cd Biblioteca-TJRJ
```

---

## 🗄️ 2️⃣ Configurar o Banco de Dados

1. Crie um banco de dados no SQL Server:
   ```sql
   CREATE DATABASE BibliotecaDesafio;
   ```

2. Execute os scripts:
   ```bash
   database/scripts/01_create_tables.sql
   database/seed/02_seed_data.sql
   ```

3. Ajuste a **connection string** no arquivo:

   ```
   backend/Biblioteca.API/appsettings.json
   ```

   Exemplo:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=BibliotecaDesafio;User Id=usuario_biblioteca;Password=SenhaForte@2025!;Encrypt=False;"
   }
   ```

---

## 🧠 3️⃣ Rodar o Backend (.NET API)

```bash
cd backend/Biblioteca.API
dotnet restore
dotnet build
dotnet run
```

A API estará disponível em:  
👉 **https://localhost:5001/swagger**

---

## 💻 4️⃣ Rodar o Frontend (Angular)

```bash
cd frontend
npm install
ng serve
```

A aplicação estará disponível em:  
👉 **http://localhost:4200**

> ⚠️ Certifique-se de que o backend está rodando antes de iniciar o frontend.  
> Caso ocorra erro de **CORS**, confirme que o `AllowAnyOrigin()` está habilitado no `Program.cs`.

---

## 🧩 5️⃣ Estrutura das APIs Principais

| Recurso | Método | Endpoint | Descrição |
|----------|---------|-----------|------------|
| Autor | GET | `/api/autor/listar-autores` | Lista todos os autores |
| Autor | POST | `/api/autor/criar` | Cria um novo autor |
| Assunto | GET | `/api/assunto/listar-assuntos` | Lista todos os assuntos |
| Livro | GET | `/api/livro/listar-livros` | Lista todos os livros |
| Livro | POST | `/api/livro/criar` | Cadastra um novo livro |

---

## 🧰 6️⃣ Comandos Úteis

### Backend
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet test
```

### Frontend
```bash
npm run build
ng generate component nome-componente
```

---

## 🧪 Testes

- Backend: `xUnit`
- Frontend: `Karma + Jasmine`
- Testes automatizados podem ser executados via:
  ```bash
  dotnet test
  ng test
  ```

---

## 🧱 7️⃣ Docker (Opcional)

Caso deseje subir via Docker:

```bash
docker-compose up --build
```

O `docker-compose.yml` sobe:
- API em .NET
- Banco SQL Server
- Aplicação Angular

---

## 📄 Licença

Este projeto está sob a licença **MIT**.  
Sinta-se livre para usar e modificar conforme necessário.

---

## 👨‍💻 Autor

**Raphael Pereira Valle**  
📧 seuemail@exemplo.com  
🔗 https://github.com/seu-usuario
