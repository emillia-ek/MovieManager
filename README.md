# 🎬 MovieManager - Aplikacja C# + Vue (Vite)

> **MovieManager** to aplikacja webowa umożliwiająca zarządzanie listą filmów – pobieranie z bazy danych oraz dodawanie, edytowanie i usuwanie własnych pozycji.  
> Backend oparty na **ASP.NET Core + Entity Framework Core**, a frontend na **Vue 3 (Vite) + Axios + Vuelidate + Bootstrap**.

---

## 📑 Spis treści
- [🚀 Opis projektu](#-opis-projektu)  
- [🛠 Technologie](#-technologie)  
  - [Backend (C# .NET)](#-backend-c-net)  
  - [Frontend (Vue 3 + Vite)](#-frontend-vue-3--vite)  
- [⚡ Instalacja i uruchomienie](#-instalacja-i-uruchomienie)  
  - [Backend](#-backend)  
  - [Frontend](#-frontend)  
- [✨ Funkcjonalności](#-funkcjonalności)  
- [📂 Struktura projektu](#-struktura-projektu)  

---

## 🚀 Opis projektu
Aplikacja umożliwia:
- Pobieranie listy filmów z bazy danych  
- Dodawanie, edytowanie i usuwanie własnych filmów  
- Walidację formularzy po stronie frontendu  

---

## 🛠 Technologie

### 🔹 Backend (C# .NET)
- .NET 8  
- Entity Framework Core  
- ASP.NET Web API  
- SQL Server / SQLite  

### 🔹 Frontend (Vue 3 + Vite)
- Vue 3  
- Vite  
- Axios (komunikacja z API)  
- Vuelidate (walidacja formularzy)  
- Bootstrap (framework CSS)  
- NPM (zarządzanie zależnościami)  

---

## ⚡ Instalacja i uruchomienie

### 🖥 Backend
1. Klonowanie repozytorium
   ```sh
   git clone https://github.com/emillia-ek/MovieManager.git  
   cd MovieManager/MovieApp
   ```

2. Przygotowanie bazy danych  
   - Skonfiguruj połączenie w `appsettings.json` (SQL Server).  
   - Wykonaj migracje:
     ```sh
     dotnet ef migrations add InitialCreate  
     dotnet ef database update
     ```

3. Uruchomienie API
   ```sh
   dotnet run
   ```
   Domyślnie: https://localhost:7108  

---

### 🌐 Frontend
1. Przejdź do katalogu projektu Vue
   ```sh 
   cd ../frontend-movie-app
   ```

2. Instalacja zależności
   ```sh
   npm install  
   npm install axios vuelidate bootstrap
   ```

3. Uruchomienie aplikacji  
   ```sh
   npm run dev  
   Domyślnie: http://localhost:5173/
   ```
Aplikacja frontendowa domyślnie jest dostępna pod adresem http://localhost:5173/. Jeśli aplikacja nie jest dostępna pod tym adresem, upewnij się, że odpowiednio skonfigurowałeś port w pliku `vite.config.js`.
---

## ✨ Funkcjonalności

### 🔹 Backend:
- API REST do zarządzania filmami (CRUD)  
- Entity Framework Core do obsługi bazy danych  

### 🔹 Frontend:
- Pobieranie listy filmów z backendu (Axios)  
- Dodawanie nowych filmów (formularz z walidacją Vuelidate)  
- Edytowanie istniejących filmów  
- Usuwanie filmów  
- Dynamiczne odświeżanie listy filmów  

---

## 📂 Struktura projektu

```
root/
│── MovieApp/                 # Aplikacja backendowa (C# .NET)
│── frontend-movie-app/       # Aplikacja frontendowa (Vue 3 + Vite)
│── README.md                 # Dokumentacja
```
---

⬆️ [Powrót do góry](#-moviemanager---aplikacja-c--vue-vite)
