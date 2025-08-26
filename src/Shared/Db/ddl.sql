CREATE TABLE user (
    id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    name VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    genre ENUM('Masculino','Femenino','Otro') NOT NULL,
    interests TEXT NOT NULL,
    career VARCHAR(100),
    phrase VARCHAR(100),
    likesAvailable INT NOT NULL DEFAULT 5,
    likesInserts INT,
    dislikes INT 
);

CREATE TABLE matches (
    user1Id INT NOT NULL,
    user2Id INT NOT NULL,
    PRIMARY KEY (user1Id, user2Id),
    FOREIGN KEY (user1Id) REFERENCES user(id),
    FOREIGN KEY (user2Id) REFERENCES user(id)
);


ALTER TABLE user ADD COLUMN password VARCHAR(255) NOT NULL;
ALTER TABLE user ADD COLUMN username VARCHAR(50) NOT NULL UNIQUE;
ALTER TABLE user ADD COLUMN email VARCHAR(100) NOT NULL UNIQUE;