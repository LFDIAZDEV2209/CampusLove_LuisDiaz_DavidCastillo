# CampusLove - Sistema de Emparejamiento Universitario

## Descripción
CampusLove es una aplicación de consola desarrollada en C# que implementa un sistema de emparejamiento para estudiantes universitarios. La aplicación permite a los usuarios registrarse, ver perfiles de otros usuarios, dar likes/dislikes, y ver estadísticas de uso.

## Arquitectura del Proyecto

### Patrones de Diseño Implementados
- **Clean Architecture**: Separación clara entre capas (Domain, Application, Infrastructure)
- **Repository Pattern**: Para acceso a datos
- **Service Pattern**: Para lógica de negocio
- **Factory Pattern**: Para creación de contextos de base de datos
- **Strategy Pattern**: Para reglas de emparejamiento

### Estructura de Carpetas
```
src/
├── Modules/
│   ├── MainMenu/           # Interfaz de usuario principal
│   ├── User/               # Módulo de usuarios
│   │   ├── Application/    # DTOs, Interfaces y Servicios
│   │   ├── Domain/         # Entidades del dominio
│   │   └── Infrastructure/ # Repositorios
│   └── Matches/            # Módulo de coincidencias
│       ├── Application/    # Interfaces y Servicios
│       ├── Domain/         # Entidades del dominio
│       └── Infrastructure/ # Repositorios
└── Shared/                 # Componentes compartidos
    ├── Configuration/      # Configuraciones de Entity Framework
    ├── Context/           # Contexto de base de datos
    ├── Db/                # Scripts SQL
    └── Helpers/           # Utilidades y factories
```

## Base de Datos

### Diagrama ER
```
┌─────────────┐         ┌─────────────┐
│    user     │         │   matches   │
├─────────────┤         ├─────────────┤
│ id (PK)     │◄────────┤ user1Id (FK)│
│ username    │         │ user2Id (FK)│
│ password    │         └─────────────┘
│ email       │
│ name        │
│ age         │
│ genre       │
│ interests   │
│ career      │
│ phrase      │
│ likesInserts│
│ likesAvailable│
│ dislikes    │
└─────────────┘
```

### Tablas por Componente

#### Componente User
- **user**: Almacena información de usuarios registrados

#### Componente Matches  
- **matches**: Almacena las coincidencias entre usuarios

### Scripts SQL
El archivo `src/Shared/Db/ddl.sql` contiene:
- Creación de tablas
- Definición de claves primarias y foráneas
- Restricciones de integridad

## Clases por Componente

### Componente User
- **User**: Entidad principal del usuario
- **UserService**: Servicio de lógica de negocio
- **UserRepository**: Acceso a datos
- **RegisterUserDTO**: DTO para registro de usuarios

### Componente Matches
- **Matches**: Entidad de coincidencias
- **MatchService**: Servicio de lógica de negocio
- **MatchRepository**: Acceso a datos

### Componente MainMenu
- **MainMenu**: Interfaz de usuario principal

### Componente Shared
- **AppDbContext**: Contexto de Entity Framework
- **UserConfiguration**: Configuración de mapeo de User
- **MatchConfiguration**: Configuración de mapeo de Matches
- **DbContextFactory**: Factory para creación de contextos
- **MySqlVersionResolver**: Resolución de versiones MySQL

## Funcionalidades Implementadas

### ✅ Completadas
- **Registro de usuarios**: Formulario completo con validaciones
- **Login funcional**: Autenticación de usuarios
- **Visualización de perfiles**: Vista detallada de usuarios
- **Sistema Like/Dislike**: Gestión de preferencias
- **Ver coincidencias**: Lista de matches del usuario
- **Estadísticas con LINQ**: Análisis de datos del usuario
- **Validaciones de entrada**: Edad, género, email
- **CultureInfo y NumberFormat**: Formateo de números
- **Conversiones con TryParse**: Validación de tipos
- **Manejo de errores robusto**: Try-catch en todos los servicios
- **Uso de Math.Min/Max**: Control de likes diarios

### 🔧 Características Técnicas
- **Entity Framework Core 9.0**: ORM moderno
- **MySQL**: Base de datos robusta
- **Spectre.Console**: Interfaz de consola atractiva
- **Async/Await**: Programación asíncrona
- **LINQ**: Consultas eficientes a la base de datos
- **Dependency Injection**: Arquitectura desacoplada

## Instalación y Configuración

### Prerrequisitos
- .NET 9.0 SDK
- MySQL 8.0 o superior
- Visual Studio 2022 o VS Code

### Configuración de Base de Datos
1. Crear base de datos MySQL llamada `examendb`
2. Ejecutar el script `src/Shared/Db/ddl.sql`
3. Configurar la cadena de conexión en `appsettings.json`

### Variables de Entorno
```bash
MYSQL_CONNECTION=server=localhost;database=examendb;user=campus2023;password=campus2023;
```

### Ejecución
```bash
dotnet restore
dotnet build
dotnet run
```

## Uso de la Aplicación

### Flujo Principal
1. **Registro**: Crear cuenta con datos personales
2. **Login**: Iniciar sesión con credenciales
3. **Explorar**: Ver perfiles de otros usuarios
4. **Interactuar**: Dar likes/dislikes
5. **Estadísticas**: Ver métricas personales y generales

### Menús Disponibles
- **Menú Principal**: Navegación general
- **Perfiles**: Explorar usuarios
- **Likes/Dislikes**: Gestión de preferencias
- **Coincidencias**: Ver matches
- **Estadísticas**: Análisis de datos

## Validaciones Implementadas

### Usuario
- Edad entre 18 y 100 años
- Género válido (Masculino, Femenino, Otro)
- Email con formato válido
- Username único
- Password requerido

### Likes
- Máximo 5 likes disponibles por día
- Control de límites con Math.Min/Max
- Validación de disponibilidad

## Estadísticas y LINQ

### Métricas Calculadas
- Edad promedio de usuarios
- Promedio de likes enviados
- Promedio de dislikes
- Porcentaje de popularidad personal

### Consultas LINQ Implementadas
```csharp
// Usuarios potenciales por género
var potentialMatches = await _userRepository.Query()
    .Where(u => u.Id != currentUserId)
    .Where(u => u.Genre != currentUserGenre)
    .OrderByDescending(u => u.LikesInserts)
    .Take(10)
    .ToListAsync();

// Estadísticas generales
var avgAge = allUsers.Average(u => u.Age);
var avgLikes = allUsers.Average(u => u.LikesInserts);
```

## Manejo de Errores

### Estrategias Implementadas
- **Try-Catch**: En todos los servicios
- **Logging**: Mensajes informativos en consola
- **Validaciones**: Previas a operaciones críticas
- **Fallbacks**: Valores por defecto en caso de error

### Tipos de Errores Manejados
- Errores de base de datos
- Validaciones de entrada
- Errores de autenticación
- Errores de lógica de negocio

## Contribución

### Estándares de Código
- Nomenclatura en inglés
- Comentarios en español
- Arquitectura limpia
- Principios SOLID

### Flujo de Desarrollo
1. Crear rama feature
2. Implementar funcionalidad
3. Agregar tests si es necesario
4. Crear pull request

## Licencia
Este proyecto es parte del examen de C# del curso PIPEJ2.

## Autores
- Luis Diaz
- David Castillo

## Fecha
2024
