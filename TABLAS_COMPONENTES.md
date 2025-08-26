# Tablas por Componente - CampusLove

## Resumen de Tablas

| Componente | Tabla | Descripción | Campos Principales |
|------------|-------|-------------|-------------------|
| **User** | `user` | Almacena información de usuarios registrados | id, username, password, email, name, age, genre, interests, career, phrase, likesInserts, likesAvailable, dislikes |
| **Matches** | `matches` | Almacena las coincidencias entre usuarios | user1Id, user2Id |

---

## Componente User

### Tabla: `user`

#### Propósito
Almacena toda la información relacionada con los usuarios del sistema CampusLove, incluyendo datos personales, credenciales de acceso, y métricas de interacción.

#### Estructura
```sql
CREATE TABLE user (
    id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    name VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    genre ENUM('Masculino', 'Femenino', 'Otro') NOT NULL,
    interests TEXT NOT NULL,
    career VARCHAR(100),
    phrase VARCHAR(100),
    likesAvailable INT NOT NULL DEFAULT 5,
    likesInserts INT,
    dislikes INT 
);
```

#### Campos Detallados

| Campo | Tipo | Restricciones | Descripción |
|-------|------|---------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT, NOT NULL | Identificador único del usuario |
| `username` | VARCHAR(50) | NOT NULL, UNIQUE | Nombre de usuario para login |
| `password` | VARCHAR(255) | NOT NULL | Contraseña encriptada del usuario |
| `email` | VARCHAR(100) | NOT NULL, UNIQUE | Correo electrónico del usuario |
| `name` | VARCHAR(50) | NOT NULL | Nombre completo del usuario |
| `age` | INT | NOT NULL | Edad del usuario (18-100) |
| `genre` | ENUM | NOT NULL | Género: Masculino, Femenino, Otro |
| `interests` | TEXT | NOT NULL | Intereses y hobbies del usuario |
| `career` | VARCHAR(100) | NULL | Carrera o profesión del usuario |
| `phrase` | VARCHAR(100) | NOT NULL | Frase personal o descripción |
| `likesAvailable` | INT | NOT NULL, DEFAULT 5 | Likes disponibles para enviar |
| `likesInserts` | INT | NULL | Total de likes enviados |
| `dislikes` | INT | NULL | Total de dislikes enviados |

#### Relaciones
- **Relación 1:N** con `matches` como `user1Id`
- **Relación 1:N** con `matches` como `user2Id`

#### Índices
- `PRIMARY KEY` en `id`
- `UNIQUE` en `username`
- `UNIQUE` en `email`

#### Validaciones de Negocio
- Edad mínima: 18 años
- Edad máxima: 100 años
- Género debe ser uno de los valores del ENUM
- Email debe contener '@'
- Username debe ser único en el sistema
- Email debe ser único en el sistema

---

## Componente Matches

### Tabla: `matches`

#### Propósito
Almacena las coincidencias (matches) entre usuarios del sistema, estableciendo relaciones bidireccionales entre pares de usuarios.

#### Estructura
```sql
CREATE TABLE matches (
    user1Id INT NOT NULL,
    user2Id INT NOT NULL,
    PRIMARY KEY (user1Id, user2Id),
    FOREIGN KEY (user1Id) REFERENCES user(id),
    FOREIGN KEY (user2Id) REFERENCES user(id)
);
```

#### Campos Detallados

| Campo | Tipo | Restricciones | Descripción |
|-------|------|---------------|-------------|
| `user1Id` | INT | NOT NULL, FOREIGN KEY | ID del primer usuario en el match |
| `user2Id` | INT | NOT NULL, FOREIGN KEY | ID del segundo usuario en el match |

#### Relaciones
- **Relación N:1** con `user` a través de `user1Id`
- **Relación N:1** con `user` a través de `user2Id`

#### Índices
- `PRIMARY KEY` compuesto en (`user1Id`, `user2Id`)
- `FOREIGN KEY` en `user1Id` referenciando `user.id`
- `FOREIGN KEY` en `user2Id` referenciando `user.id`

#### Características Especiales
- **Clave primaria compuesta**: Evita duplicados de matches
- **Relaciones bidireccionales**: Un match entre usuario A y B es el mismo que entre B y A
- **Integridad referencial**: Los usuarios referenciados deben existir en la tabla `user`

---

## Scripts de Creación

### Script Principal (ddl.sql)
```sql
-- Creación de tabla user
CREATE TABLE user (
    id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    name VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    genre ENUM('Masculino, Femenino, Otro') NOT NULL,
    interests TEXT NOT NULL,
    career VARCHAR(100),
    phrase VARCHAR(100),
    likesAvailable INT NOT NULL DEFAULT 5,
    likesInserts INT,
    dislikes INT 
);

-- Creación de tabla matches
CREATE TABLE matches (
    user1Id INT NOT NULL,
    user2Id INT NOT NULL,
    PRIMARY KEY (user1Id, user2Id),
    FOREIGN KEY (user1Id) REFERENCES user(id),
    FOREIGN KEY (user2Id) REFERENCES user(id)
);

-- Agregar campos adicionales a user
ALTER TABLE user ADD COLUMN password VARCHAR(255) NOT NULL;
ALTER TABLE user ADD COLUMN username VARCHAR(50) NOT NULL UNIQUE;
ALTER TABLE user ADD COLUMN email VARCHAR(100) NOT NULL UNIQUE;
```

---

## Configuración de Entity Framework

### UserConfiguration
```csharp
public class UserConfiguration
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(e => e.Id);
        // ... configuración detallada de propiedades
    }
}
```

### MatchConfiguration
```csharp
public class MatchConfiguration
{
    public void Configure(EntityTypeBuilder<Matches> builder)
    {
        builder.ToTable("matches");
        builder.HasKey(m => new { m.UserId1, m.UserId2 });
        // ... configuración de relaciones
    }
}
```

---

## Consideraciones de Diseño

### 1. Normalización
- Las tablas están en 3NF (Tercera Forma Normal)
- No hay redundancia de datos
- Las dependencias están correctamente separadas

### 2. Escalabilidad
- El diseño permite agregar más campos a `user` sin afectar `matches`
- Se pueden agregar nuevas tablas para funcionalidades futuras

### 3. Rendimiento
- Índices en claves primarias y únicas
- Uso de tipos de datos apropiados para cada campo
- Relaciones optimizadas para consultas frecuentes

### 4. Seguridad
- Contraseñas almacenadas como VARCHAR(255) para hash seguros
- Validaciones a nivel de base de datos
- Restricciones de integridad referencial
