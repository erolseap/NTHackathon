# 🏨 Hotel Management & Customer Reservation System

**Project Type**: Hotel Management & Customer Reservation System  
**Platform**: ASP.NET Core Web API + ReactJS + TailwindCSS  
**Architecture**: Onion Architecture  
**Database**: SQL Server (via Entity Framework Core)  
**Authentication**: JWT Token-based  

---

## 📌 Project Overview

This system enables comprehensive hotel operations including **room reservations**, **customer management**, **room status updates**, and **service tracking**. It features an **Admin Panel** for staff management and an optional **Customer Landing Page** for online reservations and self-service functionality.

---

## 🧱 Technologies Used

### 🔹 Backend
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- AutoMapper
- FluentValidation
- JWT Authentication & Role-based Authorization
- Swagger for API Documentation
- Serilog *(optional)*
- CQRS, Repository, Unit of Work *(optional)*

### 🔹 Frontend
- ReactJS (Functional Components + Hooks)
- TailwindCSS for UI Styling
- Axios for API communication
- React Hook Form + Yup for Form Validation
- react-toastify for notifications
- Chart.js or Recharts for data visualization
- React Router for routing

---

## 🧩 Core Modules

### ✅ Admin Panel
- Dashboard (occupancy stats, revenue, reservation trends)
- User Management (Admin & Receptionist roles)
- Full CRUD operations for Rooms, Customers, Reservations, and Services

### ✅ Customer Module
- Register new customers
- View customer list and details

### ✅ Room Module
- Add / Edit / Delete Rooms
- Manage Room Status (Available / Occupied)

### ✅ Reservation Module
- Book rooms for registered customers
- Search reservations by date

### ✅ Services Module
- Add room services
- Assign services to specific reservations

### ✅ (Optional) Customer Landing Page
- Browse room details & availability
- Online registration and reservation
- Real-time room status and availability

---

## 🛠️ Getting Started

### ✅ Backend Setup

1. Navigate to backend project:
   ```bash
   cd MyApp.API
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Apply EF Core migration:
   ```bash
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

API Documentation is available at:
```
https://localhost:<port>/swagger
```

---

### ✅ Frontend Setup

1. Navigate to the frontend folder:
   ```bash
   cd client
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the React app:
   ```bash
   npm run dev
   ```

---

## 🔐 Authentication & Authorization

- User login and registration is implemented using JWT.
- Role-based access for `Admin`, `Receptionist`, and (optional) `Customer`.
- Token is stored in localStorage and automatically attached to API requests via Axios.

---

## 🗃️ Database Schema

### Customers
| Column | Type |
|--------|------|
| Id | int |
| FullName | nvarchar(100) |
| PhoneNumber | nvarchar(20) |

### Rooms
| Column | Type |
|--------|------|
| Id | int |
| Number | int |
| Type | nvarchar(50) |
| PricePerNight | decimal(10,2) |
| Status | nvarchar(20) |

### Reservations
| Column | Type |
|--------|------|
| Id | int |
| CustomerId | int (FK) |
| RoomId | int (FK) |
| CheckInDate | datetime |
| CheckOutDate | datetime |

### Services
| Column | Type |
|--------|------|
| Id | int |
| Name | nvarchar(100) |
| Price | decimal(10,2) |
| ReservationId | int (FK) |

---

## 📊 Statistics & Visualizations

- Real-time charts for occupancy rate, revenue, and daily/weekly booking stats.
- Dashboard uses `Recharts` or `Chart.js`.

---

## 🧪 Testing

- Backend API can be tested via Swagger or Postman
- Frontend testing (optional): `Jest` or `React Testing Library`

---

## 🔁 Git Workflow

- GitHub is used for version control
- Feature branches for each module (`feature/`, `fix/`, `refactor/`)
- Pull Requests (PRs) are submitted before merging to `main` or `develop`
- All PRs are subject to code review

---

## 👥 Team & Contributions

- [Name 1] – Backend Developer
- [Name 2] – Frontend Developer
- [Name 3] – UI/UX & Documentation

We welcome contributions via Pull Requests 🎉

---

## 📦 Deployment Suggestions

- **Backend**: Azure App Service, Railway, or Heroku
- **Frontend**: Vercel, Netlify, Azure Static Web Apps
- **Database**: Azure SQL, Supabase, or Amazon RDS

---

## 📝 License

MIT License — open for free use and modification.
