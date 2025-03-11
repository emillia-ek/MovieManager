# FilmManager - Aplikacja C# + Vue (Vite)

## Opis projektu
Aplikacja umożliwia pobieranie listy filmów z bazy danych oraz dodawanie, edytowanie i usuwanie własnych filmów. Backend jest oparty na ASP.NET Core z Entity Framework Core, natomiast frontend został zbudowany przy użyciu Vue 3 (Vite), Axios, Vuelidate i Bootstrap.

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
   git clone https://github.com/emillia-ek/FilmManager.git
   cd FilmManager/MovieApp
   ```
2. Przygotowanie bazy danych
   - Jeśli używasz SQL Server:
     - Skonfiguruj połączenie w `appsettings.json`
   - Jeśli używasz SQLite:
     - Skonfiguruj połączenie w `appsettings.json`
   - Wykonaj migracje i zainicjalizuj bazę:
     ```sh
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```
3. Uruchomienie API
   ```sh
   dotnet run
   ```
   API powinno być dostępne pod adresem: `http://localhost:5004`.

### Frontend
1. Przejdź do katalogu projektu Vue
   ```sh
   cd ../frontend-movie-app
   ```
2. **Zainstaluj zależności
   ```sh
   npm install
   npm install axios vuelidate bootstrap
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

### Frontend:
- Pobieranie listy filmów z backendu za pomocą Axios
- Dodawanie nowych filmów (formularz z walidacją Vuelidate)
- Edytowanie istniejących filmów
- Usuwanie filmów
- Dynamiczne odświeżanie listy filmów

## Struktura projektu

```
root/
│── MovieApp/        # Aplikacja backendowa (C# .NET)
│── frontend-movie-app/       # Aplikacja frontendowa (Vue 3 + Vite)
│── README.md       # Dokumentacja
```

## API Endpoints
...



