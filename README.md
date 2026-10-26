# MovieManager - Aplikacja C# + Vue (Vite)

> **MovieManager** to aplikacja webowa umożliwiająca zarządzanie listą filmów – pobieranie z bazy danych oraz dodawanie, edytowanie i usuwanie własnych pozycji.  
> Backend oparty na **ASP.NET Core + Entity Framework Core**, a frontend na **Vue 3 (Vite) + Axios + Vuelidate + Bootstrap**.

---

## Opis projektu
Aplikacja umożliwia:
- Pobieranie listy filmów z bazy danych  
- Dodawanie, edytowanie i usuwanie własnych filmów  
- Walidację formularzy po stronie frontendu  

---

## Technologie

### Backend (C# .NET)
- .NET 8  
- Entity Framework Core  
- ASP.NET Web API  
- SQL Server / SQLite  

### Frontend (Vue 3 + Vite)
- Vue 3  
- Vite  
- Axios (komunikacja z API)  
- Vuelidate (walidacja formularzy)  
- Bootstrap (framework CSS)  
- NPM (zarządzanie zależnościami)  

---

## Instalacja i uruchomienie

### Backend
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

### Frontend
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
   
  Aplikacja frontendowa domyślnie jest dostępna pod adresem http://localhost:5173/. Jeśli aplikacja nie jest dostępna pod tym adresem, upewnij się, że odpowiednio skonfigurowałeś port w pliku vite.config.js..
  
---

## Funkcjonalności

### Backend:
- API REST do zarządzania filmami (CRUD)  
- Entity Framework Core do obsługi bazy danych  

### Frontend:
- Pobieranie listy filmów z backendu (Axios)  
- Dodawanie nowych filmów (formularz z walidacją Vuelidate)  
- Edytowanie istniejących filmów  
- Usuwanie filmów  
- Dynamiczne odświeżanie listy filmów  

---

## Struktura projektu

```
root/
│── MovieApp/                 # Aplikacja backendowa (C# .NET)
│── frontend-movie-app/       # Aplikacja frontendowa (Vue 3 + Vite)
│── README.md                 # Dokumentacja
```
---

## Zrzuty ekranu
<img width="3224" height="2153" alt="movie1" src="https://github.com/user-attachments/assets/d4ef654d-a7fb-4798-b608-134569ca6334" />
<img width="1470" height="904" alt="movie2" src="https://github.com/user-attachments/assets/0a000d46-b976-4305-894e-ce64acba741c" />
<img width="1470" height="907" alt="movie3" src="https://github.com/user-attachments/assets/1081fc51-3ae7-40d1-947e-bdf41d7afc2d" />
<img width="1470" height="879" alt="movie4" src="https://github.com/user-attachments/assets/b0670666-b0c0-4052-a61e-a5ca01702089" />
<img width="1470" height="693" alt="movie5" src="https://github.com/user-attachments/assets/b3508bc9-07f1-4a28-b7e4-6ac9b95a6f46" />

