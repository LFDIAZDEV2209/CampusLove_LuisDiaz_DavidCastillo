# Clases por Componente - CampusLove

## Resumen de Clases por Componente

| Componente | Clases | Responsabilidad Principal |
|------------|--------|--------------------------|
| **MainMenu** | 1 | Interfaz de usuario y navegación |
| **User** | 4 | Gestión de usuarios y autenticación |
| **Matches** | 3 | Gestión de coincidencias entre usuarios |
| **Shared** | 5 | Componentes compartidos y configuración |

---

## Componente MainMenu

### Clase: `MainMenu`

#### Ubicación
`src/Modules/MainMenu/MainMenu.cs`

#### Responsabilidad
Interfaz principal de usuario que coordina todas las funcionalidades de la aplicación.

#### Dependencias
- `AppDbContext`: Contexto de base de datos
- `IUserService`: Servicio de usuarios
- `IMatchService`: Servicio de matches
- `User`: Usuario autenticado actual

#### Métodos Principales
```csharp
public class MainMenu
{
    // Métodos públicos
    + Show() : Task
    
    // Métodos privados
    - ShowSignUpMenu() : Task
    - ShowLoginMenu() : Task
    - ShowMainUserMenu() : Task
    - ShowProfilesMenu() : Task
    - ShowUserProfile(User user) : Task
    - ShowLikeDislikeMenu() : Task
    - ShowMatchesMenu() : Task
    - ShowStatisticsMenu() : Task
}
```

#### Funcionalidades
- **Registro de usuarios**: Formulario completo de registro
- **Login**: Autenticación de usuarios
- **Navegación**: Menús para todas las funcionalidades
- **Gestión de estado**: Mantiene usuario autenticado

---

## Componente User

### Clase: `User`

#### Ubicación
`src/Modules/User/Domain/Entities/User.cs`

#### Responsabilidad
Entidad principal que representa un usuario del sistema.

#### Propiedades
```csharp
public class User
{
    // Propiedades básicas
    + Id : int
    + Username : string
    + Password : string
    + Email : string
    + Name : string
    + Age : int
    + Genre : string
    + Interests : string
    + Career : string
    + Phrase : string
    
    // Propiedades de interacción
    + LikesInserts : int
    + LikesAvailable : int
    + Dislikes : int
    
    // Propiedades de navegación
    + MatchesAsUser1 : ICollection<Matches>
    + MatchesAsUser2 : ICollection<Matches>
    
    // Constructor
    + User(id, username, password, email, name, age, genre, interests, career, phrase)
}
```

#### Relaciones
- **1:N** con `Matches` como `User1`
- **1:N** con `Matches` como `User2`

### Clase: `UserService`

#### Ubicación
`src/Modules/User/Application/Services/UserServie.cs`

#### Responsabilidad
Servicio de lógica de negocio para operaciones relacionadas con usuarios.

#### Dependencias
- `IUserRepository`: Repositorio de usuarios

#### Métodos
```csharp
public class UserService : IUserService
{
    // Métodos de consulta
    + GetUserByUsernameAsync(string username) : Task<User?>
    + GetAllUsersAsync() : Task<IEnumerable<User>>
    
    // Métodos de autenticación
    + RegisterUserAsync(RegisterUserDTO userDto) : Task<bool>
    + LoginAsync(string username, string password) : Task<User?>
    
    // Métodos de interacción
    + LikeUserAsync(int currentUserId, int targetUserId) : Task<bool>
    + DislikeUserAsync(int currentUserId, int targetUserId) : Task<bool>
    
    // Métodos de emparejamiento
    + GetPotentialMatchesAsync(int currentUserId, string currentUserGenre) : Task<IEnumerable<User>>
}
```

#### Funcionalidades
- **Validaciones**: Edad, género, email
- **Control de likes**: Uso de Math.Min/Max
- **Lógica de emparejamiento**: Estrategias de matching
- **Manejo de errores**: Try-catch robusto

### Clase: `UserRepository`

#### Ubicación
`src/Modules/User/Infrastructure/UserRepository.cs`

#### Responsabilidad
Acceso a datos para la entidad User.

#### Dependencias
- `AppDbContext`: Contexto de Entity Framework

#### Métodos
```csharp
public class UserRepository : IUserRepository
{
    // Métodos de consulta
    + Query() : IQueryable<User>
    + GetUserByUsernameAsync(string username) : Task<User?>
    + GetAllUsersWithoutUserLoguedAsync(string username) : Task<IEnumerable<User>>
    + GetMatchesByUsernameAsync(string username) : Task<User?>
    
    // Métodos de modificación
    + RegisterUser(User user) : void
    + UpdateUser(User user) : void
}
```

#### Funcionalidades
- **Consultas LINQ**: Optimizadas para rendimiento
- **Operaciones CRUD**: Create, Read, Update
- **Manejo de contexto**: Uso de Entity Framework

### Clase: `RegisterUserDTO`

#### Ubicación
`src/Modules/User/Application/DTOs/RegisterUserDTO.cs`

#### Responsabilidad
Objeto de transferencia de datos para el registro de usuarios.

#### Propiedades
```csharp
public class RegisterUserDTO
{
    + Username : string
    + Password : string
    + Email : string
    + Name : string
    + Age : int
    + Genre : string
    + Interests : string
    + Career : string
    + Phrase : string
}
```

#### Propósito
- **Transferencia de datos**: Entre capas de la aplicación
- **Validación**: Datos de entrada para registro
- **Desacoplamiento**: Separación de entidades y DTOs

---

## Componente Matches

### Clase: `Matches`

#### Ubicación
`src/Modules/Matches/Domain/Entities/Match.cs`

#### Responsabilidad
Entidad que representa una coincidencia entre dos usuarios.

#### Propiedades
```csharp
public class Matches
{
    // Propiedades de relación
    + UserId1 : int
    + UserId2 : int
    
    // Propiedades de navegación
    + User1 : User
    + User2 : User
}
```

#### Relaciones
- **N:1** con `User` como `User1`
- **N:1** con `User` como `User2`

### Clase: `MatchService`

#### Ubicación
`src/Modules/Matches/Application/Services/MatchService.cs`

#### Responsabilidad
Servicio de lógica de negocio para operaciones relacionadas con matches.

#### Dependencias
- `IMatchRepository`: Repositorio de matches

#### Métodos
```csharp
public class MatchService : IMatchService
{
    // Métodos de gestión
    + CreateMatch(Matches match) : void
    + GetAllMatchesAsync() : Task<IEnumerable<Matches>>
}
```

#### Funcionalidades
- **Creación de matches**: Validación de duplicados
- **Consulta de matches**: Obtención de todas las coincidencias
- **Manejo de errores**: Try-catch robusto

### Clase: `MatchRepository`

#### Ubicación
`src/Modules/Matches/Infrastructure/MatchRepository.cs`

#### Responsabilidad
Acceso a datos para la entidad Matches.

#### Dependencias
- `AppDbContext`: Contexto de Entity Framework

#### Métodos
```csharp
public class MatchRepository : IMatchRepository
{
    // Métodos de consulta
    + Query() : IQueryable<Matches>
    + GetAllMatchesAsync() : Task<IEnumerable<Matches>>
}
```

#### Funcionalidades
- **Consultas LINQ**: Optimizadas para rendimiento
- **Operaciones de lectura**: Consulta de matches existentes

---

## Componente Shared

### Clase: `AppDbContext`

#### Ubicación
`src/Shared/Context/AppDbContext.cs`

#### Responsabilidad
Contexto principal de Entity Framework para la aplicación.

#### Propiedades
```csharp
public class AppDbContext : DbContext
{
    + User : DbSet<User>
    + Matches : DbSet<Matches>
    
    // Métodos
    + OnModelCreating(ModelBuilder modelBuilder) : void
}
```

#### Funcionalidades
- **Mapeo de entidades**: Configuración de Entity Framework
- **Gestión de conexiones**: Conexión a base de datos MySQL
- **Configuraciones**: Aplicación de configuraciones de entidades

### Clase: `DbContextFactory`

#### Ubicación
`src/Shared/Helpers/DbContextFactory.cs`

#### Responsabilidad
Factory para crear instancias de AppDbContext.

#### Métodos
```csharp
public class DbContextFactory
{
    // Métodos estáticos
    + Create() : AppDbContext
}
```

#### Funcionalidades
- **Creación de contextos**: Instanciación de DbContext
- **Configuración**: Lectura de configuraciones
- **Detección de versión**: MySQL version detection

### Clase: `UserConfiguration`

#### Ubicación
`src/Shared/Configuration/UserConfiguration.cs`

#### Responsabilidad
Configuración de mapeo de Entity Framework para la entidad User.

#### Métodos
```csharp
public class UserConfiguration
{
    + Configure(EntityTypeBuilder<User> builder) : void
}
```

#### Funcionalidades
- **Mapeo de tabla**: Configuración de nombre de tabla
- **Configuración de propiedades**: Tipos, restricciones, nombres de columnas
- **Configuración de claves**: Primary key y auto-increment

### Clase: `MatchConfiguration`

#### Ubicación
`src/Shared/Configuration/MatchConfiguration.cs`

#### Responsabilidad
Configuración de mapeo de Entity Framework para la entidad Matches.

#### Métodos
```csharp
public class MatchConfiguration
{
    + Configure(EntityTypeBuilder<Matches> builder) : void
}
```

#### Funcionalidades
- **Mapeo de tabla**: Configuración de nombre de tabla
- **Configuración de claves**: Primary key compuesto
- **Configuración de relaciones**: Foreign keys y navegación

### Clase: `MySqlVersionResolver`

#### Ubicación
`src/Shared/Helpers/MySqlResolver.cs`

#### Responsabilidad
Utilidad para detectar la versión de MySQL.

#### Métodos
```csharp
public class MySqlVersionResolver
{
    // Métodos estáticos
    + DetectVersion(string connectionString) : Version
}
```

#### Funcionalidades
- **Detección de versión**: Identificación de versión MySQL
- **Validación**: Verificación de compatibilidad
- **Conexión**: Establecimiento de conexión temporal

---

## Interfaces del Sistema

### `IUserService`
- **Ubicación**: `src/Modules/User/Application/Interfaces/IUserService.cs`
- **Propósito**: Contrato para servicios de usuario
- **Implementación**: `UserService`

### `IUserRepository`
- **Ubicación**: `src/Modules/User/Application/Interfaces/IUserRepository.cs`
- **Propósito**: Contrato para repositorios de usuario
- **Implementación**: `UserRepository`

### `IMatchService`
- **Ubicación**: `src/Modules/Matches/Application/Interfaces/IMatchService.cs`
- **Propósito**: Contrato para servicios de matches
- **Implementación**: `MatchService`

### `IMatchRepository`
- **Ubicación**: `src/Modules/Matches/Application/Interfaces/IMatchRepository.cs`
- **Propósito**: Contrato para repositorios de matches
- **Implementación**: `MatchRepository`

---

## Patrones de Diseño Aplicados

### 1. Repository Pattern
- **Clases**: `UserRepository`, `MatchRepository`
- **Interfaces**: `IUserRepository`, `IMatchRepository`
- **Propósito**: Abstracción del acceso a datos

### 2. Service Pattern
- **Clases**: `UserService`, `MatchService`
- **Interfaces**: `IUserService`, `IMatchService`
- **Propósito**: Lógica de negocio centralizada

### 3. Factory Pattern
- **Clase**: `DbContextFactory`
- **Propósito**: Creación de instancias de contextos

### 4. DTO Pattern
- **Clase**: `RegisterUserDTO`
- **Propósito**: Transferencia de datos entre capas

### 5. Strategy Pattern
- **Implementación**: En `UserService.GetPotentialMatchesAsync()`
- **Propósito**: Diferentes estrategias de emparejamiento

---

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

### Capa Compartida
- **Configurations**: Mapeo de entidades
- **Helpers**: Utilidades y factories
