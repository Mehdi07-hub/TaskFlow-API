## Auteur
| Mehdi07-hub | Fartoune Mehdi |

# TaskFlow API

API REST de gestion de projets et taches — ASP.NET Core 8 + Entity Framework + SQL Server.

## Prerequis
- .NET 8 SDK
- SQL Server Express

## Installation

1. Cloner le projet
2. Modifier la connection string dans appsettings.json :
   Server=.\SQLEXPRESS;Database=TaskFlowDb;Trusted_Connection=True;TrustServerCertificate=True;
3. Lancer l'API : dotnet run
4. La base de donnees est creee automatiquement au premier lancement
5. Swagger disponible sur http://localhost:5043

## Endpoints

### Sans token
| Methode | Route | Description |
|---|---|---|
| POST | /api/users/register | Creer un compte |
| POST | /api/users/login | Se connecter |

### Avec token JWT (Bearer)
| Methode | Route | Description |
|---|---|---|
| GET | /api/projects | Mes projets |
| POST | /api/projects | Creer un projet |
| GET | /api/projects/{id} | Detail projet |
| PUT | /api/projects/{id} | Modifier projet |
| DELETE | /api/projects/{id} | Supprimer projet |
| GET | /api/tasks | Mes taches |
| POST | /api/tasks | Creer une tache |
| GET | /api/tasks/{id} | Detail tache |
| PUT | /api/tasks/{id} | Modifier tache |
| DELETE | /api/tasks/{id} | Supprimer tache |

## Comment utiliser Swagger
1. Faire POST /api/users/login
2. Copier le token
3. Cliquer sur Authorize en haut de Swagger
4. Ecrire : Bearer {votre_token}
5. Tester les endpoints

## Base de donnees
Generee automatiquement par Entity Framework Code First.
Pour regenerer manuellement :
dotnet ef migrations add Init
dotnet ef database update

## Architecture
- Models/ — entites User, Project, TaskItem
- Data/ — AppDbContext EF Core
- DTOs/ — objets de transfert
- Services/ — logique metier injection de dependances
- Controllers/ — endpoints REST
- Middleware/ — gestion des erreurs