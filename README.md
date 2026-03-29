# ✈️ TravelEase — Smart Travel Management System

### *Your All-in-One Desktop Travel Ecosystem*

A powerful **Windows Forms (.NET Framework 4.7.2)** application backed by **SQL Server**, designed to simulate real-world travel operations—from **trip discovery to booking, payments, analytics, and management dashboards**.

> 💡 Think of TravelEase as a mini *Expedia + Admin Panel + Analytics Suite* — all inside a desktop app.

---

## 🌍 Why TravelEase?

TravelEase isn’t just a university project — it’s a **complete travel workflow simulation** that demonstrates how modern travel platforms operate behind the scenes.

It brings together:

✔️ User experience (travellers)
✔️ Business logic (operators/providers)
✔️ Admin control systems
✔️ Database-driven architecture
✔️ Real-time UI interaction (WinForms)

---

## 🚀 Core Features

### 👤 Traveller Experience

Explore the world with ease:

* 🔐 **Secure Sign-Up & Login**
* 🔎 **Smart Trip Discovery**

  * Tile-based interactive UI
* 📅 **Booking & Reservation System**
* 💳 **Payment Processing Flow**
* ⭐ **Reviews & Ratings System**
* 📌 **Upcoming Trips Tracking**

---

### 🏢 Provider / Operator / Admin Panel

Powerful backend controls:

* 🧾 **Provider & Operator Onboarding**
* 📊 **Admin Dashboard**
* 🧭 **Service Listing & Integration**
* 📦 **Booking Management System**
* 📈 **Performance Analytics**
* ⚙️ **Full System Control**

---

## 🧠 Tech Stack

| Layer           | Technology               |
| --------------- | ------------------------ |
| 💻 Frontend     | C# WinForms              |
| ⚙️ Framework    | .NET Framework 4.7.2     |
| 🗄️ Database    | Microsoft SQL Server     |
| 🔌 Connectivity | Microsoft.Data.SqlClient |

---

## 📂 Project Structure (Simplified View)

```
projfinal.sln
│
├── WindowsFormsApp1/
│   ├── Program.cs              → Entry point
│   ├── App.config             → DB connection
│   ├── Forms & UI files       → Core interface
│   ├── traveleaseDataSet.xsd  → Data schema
│
└── packages/                  → Dependencies
```

🔗 **Explore full project:**
[https://github.com/Hadi-Wasim/TravelEase-DBfinalProject/tree/main/WindowsFormsApp1](https://github.com/Hadi-Wasim/TravelEase-DBfinalProject/tree/main/WindowsFormsApp1)

---

## ⚡ Getting Started

### 🔧 Prerequisites

Make sure you have:

* Windows OS
* Visual Studio (2019 or 2022 recommended)
* .NET Framework 4.7.2 Developer Pack
* SQL Server (Express works fine)
* SSMS (optional but recommended)

---

### 📥 Setup Guide

#### 1. Clone & Open

```bash
git clone https://github.com/Hadi-Wasim/TravelEase-DBfinalProject
```

Open `projfinal.sln` in Visual Studio.

---

#### 2. Database Setup

Create a database named:

```
travelease
```

✔️ If you have a `.sql` script → run it
✔️ Otherwise → manually create tables based on dataset/schema

---

#### 3. Configure Connection

Edit `App.config`:

```xml
<connectionStrings>
  <add
    name="WindowsFormsApp1.Properties.Settings.traveleaseConnectionString"
    connectionString="Data Source=YOUR_SERVER;Initial Catalog=travelease;Integrated Security=True;TrustServerCertificate=True"
    providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

📌 Example Data Sources:

* `.\SQLEXPRESS`
* `(localdb)\MSSQLLocalDB`

---

#### 4. Run the Project

* Restore NuGet packages
* Build solution
* Press **F5 🚀**

---

## 🖥️ Screens & Modules (System Overview)

### 🔐 Authentication

* Login
* Traveller / Provider / Admin Sign-Up

---

### 📊 Dashboards

* Traveller Dashboard
* Provider Dashboard
* Admin Control Panel

---

### 🌍 Trip Management

* Trip Search
* Trip Tiles (interactive cards)
* Detailed Trip Views
* Upcoming Trips

---

### 💳 Booking & Payments

* Reservation System
* Booking Details
* Payment Flow

---

### ⭐ Reviews System

* Review Submission
* Review Tiles UI

---

### 📈 Analytics & Insights

* Performance Dashboard
* Booking Trends
* Analytics Views

---

## 🛠️ Troubleshooting

### ❌ Database Connection Errors

* Check SQL Server is running
* Verify instance name
* Ensure database exists
* Check Windows Authentication access

---

### 📦 Missing Packages

* Right-click solution → Restore NuGet Packages

---

### ⚠️ .NET Issues

* Install .NET Framework 4.7.2 Developer Pack
* Avoid changing target framework unless necessary

---

## 🧩 Design Highlights

✨ Clean modular WinForms architecture
✨ Database-first approach
✨ Real-world system simulation
✨ Multi-role user flows
✨ Scalable structure for future upgrades

---

## 📌 Important Notes

* 🖥️ Desktop application (not web-based)
* 🔐 Connection strings stored in `App.config`
* ⚠️ Avoid committing personal machine names

---

## 📜 License

Currently for **educational use**.
You can add an **MIT License** if open-sourcing publicly.

---

## 👨‍💻 Author

**Hadi Wasim**
💡 Passionate about building real-world systems & scalable applications

---

## ⭐ Final Thought

TravelEase is more than a project — it’s a **foundation for building full-scale travel platforms**. With further upgrades (API integration, cloud DB, modern UI), it can evolve into a production-ready system.

---
