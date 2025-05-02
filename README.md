# GoodHamburgerAPI

[![Build Status](https://github.com/louresb/GoodHamburgerAPI/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/louresb/GoodHamburgerAPI/actions/workflows/build-and-test.yml)
![Status](https://img.shields.io/badge/Status-Concluded-green)

This project is a backend-focused solution built with C# and .NET 8 to simulate an order management system for a hamburger restaurant, applying specific business and discount rules in a real-world scenario.

## 🎯 Project Objective

Build a Web API using ASP.NET Core 8 to manage orders for a hamburger restaurant.

### 📝 Business Rules

- Three sandwich options:
  - X Burger – $5.00
  - X Egg – $4.50
  - X Bacon – $7.00
- Extras:
  - Fries – $2.00
  - Soft Drink – $2.50

### 💡 Discount Rules
- Sandwich + Fries + Soft Drink → **20% discount**
- Sandwich + Soft Drink → **15% discount**
- Sandwich + Fries → **10% discount**

**Important constraint:**
- Each order must contain at most **one** sandwich, one fries, and one soft drink.
- If duplicate items are sent, the API returns an error.

### 🛠️ Required Endpoints
- `GET /api/products` – List all sandwiches and extras
- `GET /api/products/sandwiches` – List sandwiches only
- `GET /api/products/extras` – List extras only
- `POST /api/orders` – Create an order (with discounts applied)
- `GET /api/orders` – List all orders
- `GET /api/orders/{id}` – Get order by ID
- `PUT /api/orders/{id}` – Update an order
- `DELETE /api/orders/{id}` – Delete an order

### 📦 Constraints
- No authentication required
- All data handled in-memory
- Requests tested via Swagger 

---

## 📦 Project Structure

```text
GoodHamburgerAPI/
├── src/
│   ├── GoodHamburger.Api                  --> ASP.NET Core Web API
│   │   └── Dockerfile                     --> Docker build for API
│   ├── GoodHamburger.Web                  --> Blazor WebAssembly frontend 
│   │   └── Dockerfile                     --> Docker build for Web frontend
│   ├── GoodHamburger.Domain               --> Business models and enums
│   ├── GoodHamburger.DomainInterfaces     --> Contracts (interfaces) for services
│   └── GoodHamburger.Infra                --> In-memory implementation of services
├── tests/
│   └── GoodHamburger.Tests                --> xUnit tests for business logic
├── docker-compose.yml                     --> Runs both API and Web together
└── .github/workflows/build-and-test.yml   --> CI pipeline with GitHub Actions
```

---

## 🚀 Technologies Used

- C#
- .NET 8
- ASP.NET Core Web API
- Blazor WebAssembly (bonus frontend)
- InMemory caching (IMemoryCache)
- xUnit (tests)
- Swagger / Swashbuckle
- Docker + Docker Compose
- GitHub Actions (CI)

---

## 💻 How to Run

### Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/)

### 1. Clone the repository
```bash
git clone https://github.com/louresb/GoodHamburgerAPI.git
cd GoodHamburgerAPI
```

### 2. Run with Docker Compose
```bash
docker compose up --build
```

### 3. Access the app
- **Swagger API Docs**: http://localhost:8080/swagger
- **Frontend (Blazor)**: http://localhost:5173/

---

## 🧪 Testing

Unit tests are written using xUnit and cover key business rules such as:

- Discount calculation
- Validation of item limits
- Duplicate item rejection

To run the tests:
```bash
cd tests/GoodHamburger.Tests
dotnet test
```

---

## 🎨 Bonus: Web Frontend (Blazor)

An optional frontend was created using **Blazor WebAssembly** to enhance the presentation and usability of the solution.

### Pages Implemented

| Page               | Description                              |
|--------------------|------------------------------------------|
| `/place-order`     | Submit a new order                       |
| `/orders`          | View all orders                          |
| `/orders/edit/{id}`| Edit an existing order                   |
| `/orders/details/{id}` | View full order details             |

The frontend communicates with the API using `HttpClient`, dynamically resolving the base address (localhost or Docker alias `http://api/`).

---

## 📌 Highlights

- Clean layered architecture (API / Domain / Infra)
- Full CRUD for orders with business rules enforced
- API tested via Swagger and Postman
- Extra Blazor frontend to visualize the solution
- CI/CD configured with GitHub Actions
- Fully Dockerized for local testing and presentation

---

### ✅ Final Notes

- This solution was developed with care to fully meet the challenge requirements, while also showcasing practical development skills using modern .NET technologies.
- The extra frontend is intended to enrich the experience and demonstrate full-stack integration.
