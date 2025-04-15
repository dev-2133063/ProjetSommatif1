DROP TABLE IF EXISTS projet_login_membre;
CREATE TABLE projet_login_membre (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    membreid INT NOT NULL
);

INSERT INTO projet_login_membre (username, password, membreid)
VALUES ('admin', 'admin', 1),
('user', 'user', 2),
('editor', 'editor', 3);