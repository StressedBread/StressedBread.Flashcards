# Flashcards
 
A C# console application for creating and studying flashcard stacks, with session tracking and monthly performance reports.
 
 
## Features
 
- **Stack Management** — Create, browse, and delete named flashcard stacks
- **Card Management** — Add, edit, and delete flashcards within any stack, or manage all cards across stacks from a single view
- **Study Mode** — Practice with randomised flashcards and get instant right/wrong feedback
- **Study History** — View past sessions filtered by stack or across all stacks
- **Monthly Reports** — Reports showing sessions count or average score per month per stack
 
## Getting Started
 
### Prerequisites
 
- .NET 8 SDK
- SQL Server (LocalDB is sufficient)
### Installation
 
1. **Clone the repository**
   ```bash
   git clone <your-repository-url>
   cd StressedBread.Flashcards
   ```
 
2. **Restore dependencies**
   ```bash
   dotnet restore
   ```
 
3. **Run the application**
   ```bash
   dotnet run
   ```
 
   On first launch the app will automatically create the `FlashcardsStressedBread` database and all required tables. Subsequent launches safely skip this step.
  
## How to Use
 
### 1. Creating Your First Stack
 
- Select **Manage Stacks** from the main menu
- Type the name of a new stack and press Enter
### 2. Adding Flashcards
 
- After selecting a stack, select **Add Flashcard**
- Enter a question and an answer
- Repeat to fill your stack
### 3. Studying
 
- Select **Study** from the main menu
- Enter the name of the stack you want to practise
- Type your answer for each card shown — the app tells you instantly if you are right and reveals the correct answer if not
- Cards are presented in random order; each card appears once per session
- Enter `0` at any point to end the session early
- Your score is saved automatically when the session ends but not if exited early
### 4. Tracking Progress
 
- Select **View Study Sessions** from the main menu
- Enter `1` to see all sessions across every stack, or type a stack name to filter
### 5. Monthly Reports
 
- Select **View Reports** from the main menu
- Choose a report type:
  - **Sessions Per Month** — how many sessions you completed each month
  - **Average Score Per Month** — your average score across sessions each month
- Enter a stack name and a year to generate the report
 
## Technologies Used
 
- **Framework**: .NET 8
- **Language**: C# 12
- **Database**: SQL Server with Dapper
- **UI**: Spectre.Console
 
## Configuration
 
The app requires two connection strings in `appsettings.json`:
 
| Key | Purpose |
|---|---|
| `DefaultConnection` | Used on startup to create the flashcards database (point at `master` or any existing DB) |
| `StressedBreadFlashcards` | Used for all application data (points at `FlashcardsStressedBread`) |
 
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;",
    "StressedBreadFlashcards": "Server=localhost;Database=FlashcardsStressedBread;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
