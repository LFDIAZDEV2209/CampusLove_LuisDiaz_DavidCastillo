-- DML ampliado para pruebas

-- Asegúrate de ejecutar primero ddl.sql y tener la DB vacía o coherente.

-- Usuarios de ejemplo (IDs pueden variar según autoincremento)
INSERT INTO user (name, age, genre, interests, career, phrase, likesAvailable, likesInserts, dislikes, password, username, email)
VALUES 
  ('Alice', 22, 'Femenino', 'Música, Lectura', 'Ingeniería', 'Carpe Diem', 5, 3, 1, 'pass1', 'alice', 'alice@example.com'),
  ('Bob', 24, 'Masculino', 'Deportes, Cine', 'Diseño', 'Just do it', 5, 1, 2, 'pass2', 'bob', 'bob@example.com'),
  ('Carol', 21, 'Femenino', 'Arte, Viajes', 'Arquitectura', 'Less is more', 5, 4, 0, 'pass3', 'carol', 'carol@example.com'),
  ('Dave', 23, 'Masculino', 'Programación, Café', 'Sistemas', 'Hello World', 5, 2, 3, 'pass4', 'dave', 'dave@example.com'),
  ('Eve', 20, 'Femenino', 'IA, Lectura', 'Informática', 'Think different', 5, 5, 1, 'pass5', 'eve', 'eve@example.com'),
  ('Frank', 25, 'Masculino', 'Música, Senderismo', 'Industrial', 'Keep moving', 5, 2, 0, 'pass6', 'frank', 'frank@example.com'),
  ('Gina', 22, 'Femenino', 'Fotografía, Cine', 'Diseño', 'Capture the moment', 5, 1, 4, 'pass7', 'gina', 'gina@example.com'),
  ('Hank', 24, 'Masculino', 'Gaming, Deportes', 'Electrónica', 'No pain no gain', 5, 3, 3, 'pass8', 'hank', 'hank@example.com'),
  ('Iris', 23, 'Femenino', 'Ciencia, Danza', 'Biomédica', 'To the stars', 5, 4, 2, 'pass9', 'iris', 'iris@example.com'),
  ('Jack', 26, 'Masculino', 'Lectura, Viajes', 'Administración', 'Veni Vidi Vici', 5, 0, 1, 'pass10', 'jack', 'jack@example.com');

-- Coincidencias de ejemplo (ajusta IDs si tu tabla ya tenía usuarios previos)
INSERT INTO matches (user1Id, user2Id)
VALUES 
  (1, 2),
  (1, 3),
  (2, 3),
  (3, 4),
  (4, 5),
  (5, 6),
  (2, 5),
  (7, 8),
  (8, 9),
  (9, 10),
  (1, 5);
