# 💖 Campus Love

Campus Love es una aplicación de consola desarrollada en C# y basada en .NET Core 8.0 que simula un sistema completo de emparejamiento entre estudiantes universitarios. Este sistema permite a los usuarios registrarse, visualizar perfiles, dar like/dislike, revisar coincidencias, ver estadísticas y más.

> Proyecto desarrollado por **Luis Felipe Díaz** y **David Castillo**.

---

## 📌 Índice

- [Descripción](#descripción)
- [Características](#características)
- [Requisitos del sistema](#requisitos-del-sistema)
- [Instalación](#instalación)
- [Uso](#uso)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Diagramas](#diagramas)
- [Tecnologías y herramientas](#tecnologías-y-herramientas)
- [Autores](#autores)

---

## 🧠 Descripción

Campus Love permite simular interacciones entre usuarios a través de una interfaz de consola amigable. El objetivo es ofrecer una experiencia estructurada aplicando principios SOLID, patrones de diseño, uso de LINQ, control de datos, arquitectura limpia y mucho más.

---

## ✨ Características

- Registro de nuevos usuarios (nombre, edad, género, intereses, carrera, frase de perfil).
- Visualización de perfiles con opción de Like o Dislike.
- Sistema de coincidencias (match) basado en likes mutuos.
- Sistema de créditos diarios para limitar la cantidad de likes.
- Estadísticas del sistema con LINQ (usuarios con más likes, matches, etc.).
- Validaciones de entrada robustas (edad, género, formato, etc.).
- Cultura e internacionalización (uso de `CultureInfo`, `NumberFormatInfo`).
- Aplicación de principios SOLID y patrones como Factory y Strategy.
- Arquitectura por capas con separación clara de responsabilidades.

---

## 💻 Requisitos del sistema

- [.NET Core SDK 8.0](https://dotnet.microsoft.com/en-us/download)
- Visual Studio Code o Visual Studio 2022+
- Sistema operativo: Windows, Linux o macOS

---

## ⚙️ Instalación

```bash
git clone https://github.com/LFDIAZDEV2209/CampusLove_LuisDiaz_DavidCastillo
cd CampusLove_LuisDiaz_DavidCastillo
dotnet build
dotnet run
