# MovieApp - Aplikacja C# + Vue (Vite)

## Opis projektu
MovieApp to aplikacja webowa umożliwiająca pobieranie filmów z bazy danych oraz zarządzanie nimi. Użytkownik może dodawać, edytować i usuwać własne filmy, które są przechowywane w lokalnej bazie danych.

Projekt składa się z backendu napisanego w C# (.NET + Entity Framework Core) oraz frontendowej aplikacji w Vue 3 (z Vite), wykorzystującej biblioteki Axios i Vuelidate.

## Technologie

### Backend (C# .NET)
- .NET 7 / .NET 8
- Entity Framework Core
- ASP.NET Web API
- SQL Server / SQLite (do wyboru)

### Frontend (Vue 3 + Vite)
- Vue 3
- Vite
- Axios (do komunikacji z API)
- Vuelidate (do walidacji formularzy)
- NPM (do zarządzania zależnościami)

## Instalacja i uruchomienie

### Backend
1. Klonowanie repozytorium
   ```sh
   git clone https://github.com/...
   cd twoje-repozytorium/backend
   ```
2. Przygotowanie bazy danych
   - Jeśli używasz SQL Server:
     - Skonfiguruj połączenie w `appsettings.json`
   - Jeśli używasz SQLite:
     - Skonfiguruj połączenie w `appsettings.json`
   - Wykonaj migracje i zainicjalizuj bazę:
     ```sh
     dotnet ef database update
     ```
3. Uruchomienie API
   ```sh
   dotnet run
   ```
   API powinno być dostępne pod adresem: `https://localhost:5001`.

### Frontend
1. Przejdź do katalogu projektu Vue
   ```sh
   cd ../frontend-movie-app
   ```
2. **Zainstaluj zależności
   ```sh
   npm install
   ```
3. **Uruchomienie aplikacji**
   ```sh
   npm run dev
   ```
   Aplikacja będzie działać pod adresem `http://localhost:5173/`.

## Funkcjonalności

### Backend:
- API REST do zarządzania filmami (CRUD: Create, Read, Update, Delete)
- Entity Framework Core do obsługi bazy danych
- Możliwość użycia SQLite lub SQL Server

### Frontend:
- Pobieranie listy filmów z backendu za pomocą Axios
- Dodawanie nowych filmów (formularz z walidacją Vuelidate)
- Edytowanie istniejących filmów
- Usuwanie filmów
- Dynamiczne odświeżanie listy filmów

## Struktura projektu

```
root/
│── backend/        # Aplikacja backendowa (C# .NET)
│── frontend/       # Aplikacja frontendowa (Vue 3 + Vite)
│── README.md       # Dokumentacja
```

## API Endpoints
| Metoda  | Endpoint          | Opis |
|---------|------------------|------|
| GET     | `/api/movies`    | Pobiera listę filmów |
| GET     | `/api/movies/{id}` | Pobiera szczegóły filmu |
| POST    | `/api/movies`    | Dodaje nowy film |
| PUT     | `/api/movies/{id}` | Edytuje istniejący film |
| DELETE  | `/api/movies/{id}` | Usuwa film |

## Autor


