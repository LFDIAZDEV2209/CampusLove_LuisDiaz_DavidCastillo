# Diagrama de Clases - CampusLove

## Vista General de la Arquitectura

```mermaid
classDiagram
    class MainMenu {
        -AppDbContext _context
        -IUserService _userService
        -IMatchService _matchService
        -User _currentUser
        +Show()
        -ShowSignUpMenu()
        -ShowLoginMenu()
        -ShowMainUserMenu()
        -ShowProfilesMenu()
        -ShowUserProfile()
        -ShowLikeDislikeMenu()
        -ShowMatchesMenu()
        -ShowStatisticsMenu()
    }

    class IUserService {
        <<interface>>
        +GetUserByUsernameAsync()
        +GetAllUsersAsync()
        +RegisterUserAsync()
        +LoginAsync()
        +LikeUserAsync()
        +DislikeUserAsync()
        +GetPotentialMatchesAsync()
    }

    class UserService {
        -IUserRepository _userRepository
        +GetUserByUsernameAsync()
        +GetAllUsersAsync()
        +RegisterUserAsync()
        +LoginAsync()
        +LikeUserAsync()
        +DislikeUserAsync()
        +GetPotentialMatchesAsync()
    }

    class IUserRepository {
        <<interface>>
        +RegisterUser()
        +UpdateUser()
        +GetAllUsersWithoutUserLoguedAsync()
        +Query()
        +GetMatchesByUsernameAsync()
        +GetUserByUsernameAsync()
    }

    class UserRepository {
        -AppDbContext _context
        +RegisterUser()
        +UpdateUser()
        +GetAllUsersWithoutUserLoguedAsync()
        +Query()
        +GetMatchesByUsernameAsync()
        +GetUserByUsernameAsync()
    }

    class User {
        +Id
        +Username
        +Password
        +Email
        +Name
        +Age
        +Genre
        +Interests
        +Career
        +Phrase
        +LikesInserts
        +LikesAvailable
        +Dislikes
        +MatchesAsUser1
        +MatchesAsUser2
        +User()
    }

    class RegisterUserDTO {
        +Username
        +Password
        +Email
        +Name
        +Age
        +Genre
        +Interests
        +Career
        +Phrase
    }

    class IMatchService {
        <<interface>>
        +CreateMatch()
        +GetAllMatchesAsync()
    }

    class MatchService {
        -IMatchRepository _matchRepository
        +CreateMatch()
        +GetAllMatchesAsync()
    }

    class IMatchRepository {
        <<interface>>
        +Query()
        +GetAllMatchesAsync()
    }

    class MatchRepository {
        -AppDbContext _context
        +Query()
        +GetAllMatchesAsync()
    }

    class Matches {
        +UserId1
        +UserId2
        +User1
        +User2
    }

    class AppDbContext {
        +User DbSet
        +Matches DbSet
        +OnModelCreating()
    }

    class DbContextFactory {
        <<static>>
        +Create()
    }

    class UserConfiguration {
        +Configure()
    }

    class MatchConfiguration {
        +Configure()
    }

    class MySqlVersionResolver {
        <<static>>
        +DetectVersion()
    }

    %% Relaciones
    MainMenu --> IUserService
    MainMenu --> IMatchService
    MainMenu --> User

    IUserService <|.. UserService
    UserService --> IUserRepository
    UserService --> RegisterUserDTO

    IUserRepository <|.. UserRepository
    UserRepository --> AppDbContext

    User --> Matches
    User --> Matches

    IMatchService <|.. MatchService
    MatchService --> IMatchRepository

    IMatchRepository <|.. MatchRepository
    MatchRepository --> AppDbContext

    AppDbContext --> User
    AppDbContext --> Matches

    DbContextFactory --> AppDbContext
    UserConfiguration --> User
    MatchConfiguration --> Matches
```

## Detalle de Relaciones

### Relaciones de Herencia
- `UserService` implementa `IUserService`
- `UserRepository` implementa `IUserRepository`
- `MatchService` implementa `IMatchService`
- `MatchRepository` implementa `IMatchRepository`

### Relaciones de Composición
- `MainMenu` contiene `UserService` y `MatchService`
- `UserService` contiene `UserRepository`
- `MatchService` contiene `MatchRepository`
- `AppDbContext` contiene `DbSet<User>` y `DbSet<Matches>`

### Relaciones de Asociación
- `User` tiene muchos `Matches` (como User1)
- `User` tiene muchos `Matches` (como User2)
- `Matches` tiene dos `User` (User1 y User2)

## Patrones de Diseño Aplicados

### 1. Repository Pattern
- `IUserRepository` y `UserRepository`
- `IMatchRepository` y `MatchRepository`
- Abstracción del acceso a datos

### 2. Service Pattern
- `IUserService` y `UserService`
- `IMatchService` y `MatchService`
- Lógica de negocio centralizada

### 3. Factory Pattern
- `DbContextFactory`
- Creación de instancias de `AppDbContext`

### 4. Strategy Pattern
- Diferentes estrategias de emparejamiento en `UserService.GetPotentialMatchesAsync()`

### 5. DTO Pattern
- `RegisterUserDTO` para transferencia de datos de registro

## Responsabilidades por Capa

### Capa de Presentación
- **MainMenu**: Interfaz de usuario y navegación

### Capa de Aplicación
- **UserService**: Lógica de negocio de usuarios
- **MatchService**: Lógica de negocio de matches
- **DTOs**: Transferencia de datos

### Capa de Dominio
- **User**: Entidad principal del usuario
- **Matches**: Entidad de coincidencias

### Capa de Infraestructura
- **UserRepository**: Acceso a datos de usuarios
- **MatchRepository**: Acceso a datos de matches
- **AppDbContext**: Contexto de Entity Framework
- **Configurations**: Mapeo de entidades

### Capa Compartida
- **DbContextFactory**: Factory para contextos
- **MySqlVersionResolver**: Utilidades de base de datos
