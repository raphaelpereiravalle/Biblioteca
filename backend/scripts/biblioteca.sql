/* ==========================================================
   BANCO DE DADOS: BibliotecaDesafio
   OBJETIVO: recriar todas as tabelas do modelo com dados iniciais
   AUTOR: Raphael Pereira Valle
   DATA: 2025-10-17
   COMPATÍVEL: SQL Server 2022
   ========================================================== */

-- ==========================================================
-- 1️⃣ CRIAÇÃO DO BANCO (caso não exista)
-- ==========================================================
IF DB_ID('BibliotecaDesafio') IS NULL
BEGIN
    CREATE DATABASE BibliotecaDesafio;
    PRINT 'Banco de dados BibliotecaDesafio criado com sucesso!';
END
ELSE
BEGIN
    PRINT 'Banco de dados BibliotecaDesafio já existe.';
END
GO

USE BibliotecaDesafio;
GO

-- ==========================================================
-- 2️⃣ EXCLUSÃO DAS TABELAS EXISTENTES (em ordem reversa de dependência)
-- ==========================================================
IF OBJECT_ID('dbo.LivroAssunto', 'U') IS NOT NULL DROP TABLE dbo.LivroAssunto;
IF OBJECT_ID('dbo.LivroAutor', 'U') IS NOT NULL DROP TABLE dbo.LivroAutor;
IF OBJECT_ID('dbo.LivroValor', 'U') IS NOT NULL DROP TABLE dbo.LivroValor;
IF OBJECT_ID('dbo.Livro', 'U') IS NOT NULL DROP TABLE dbo.Livro;
IF OBJECT_ID('dbo.Assunto', 'U') IS NOT NULL DROP TABLE dbo.Assunto;
IF OBJECT_ID('dbo.Autor', 'U') IS NOT NULL DROP TABLE dbo.Autor;
GO

PRINT 'Tabelas anteriores removidas com sucesso!';
GO

-- ==========================================================
-- 3️⃣ CRIAÇÃO DAS TABELAS
-- ==========================================================

-- Autor
CREATE TABLE Autor (
    IdAutor INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(60) NOT NULL
);
GO

-- Assunto
CREATE TABLE Assunto (
    IdAssunto INT IDENTITY(1,1) PRIMARY KEY,
    Descricao VARCHAR(40) NOT NULL
);
GO

-- Livro
CREATE TABLE Livro (
    IdLivro INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(100) NOT NULL,
    Editora VARCHAR(60),
    Edicao INT,
    AnoPublicacao CHAR(4)
);
GO

-- Livro_Autor (relação N:N)
CREATE TABLE LivroAutor (
    IdLivro INT NOT NULL,
    IdAutor INT NOT NULL,
    CONSTRAINT PK_Livro_Autor PRIMARY KEY (IdLivro, IdAutor),
    CONSTRAINT FK_Livro_Autor_Livro FOREIGN KEY (IdLivro)
        REFERENCES Livro (IdLivro)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_Livro_Autor_Autor FOREIGN KEY (IdAutor)
        REFERENCES Autor (IdAutor)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- Livro_Assunto (relação N:N)
CREATE TABLE LivroAssunto (
    IdLivro INT NOT NULL,
    IdAssunto INT NOT NULL,
    CONSTRAINT PK_Livro_Assunto PRIMARY KEY (IdLivro, IdAssunto),
    CONSTRAINT FK_Livro_Assunto_Livro FOREIGN KEY (IdLivro)
        REFERENCES Livro (IdLivro)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_Livro_Assunto_Assunto FOREIGN KEY (IdAssunto)
        REFERENCES Assunto (IdAssunto)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- LivroValor (preços e tipos de venda)
CREATE TABLE LivroValor (
    IdLivroValor INT IDENTITY(1,1) PRIMARY KEY,
    IdLivro INT NOT NULL,
    TipoVenda VARCHAR(50) NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_LivroValor_Livro FOREIGN KEY (IdLivro)
        REFERENCES Livro (IdLivro)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

PRINT 'Tabelas criadas com sucesso!';
GO

-- ==========================================================
-- 4️⃣ ÍNDICES
-- ==========================================================
CREATE INDEX IX_Livro_Titulo ON Livro (Titulo);
CREATE INDEX IX_Autor_Nome ON Autor (Nome);
CREATE INDEX IX_Assunto_Descricao ON Assunto (Descricao);
GO

PRINT 'Índices criados com sucesso!';
GO

-- ==========================================================
-- 5️⃣ INSERÇÃO DE DADOS INICIAIS
-- ==========================================================

-- Autores
INSERT INTO Autor (Nome) VALUES
('Luiz Flávio Gomes'),
('Maria Helena Diniz'),
('José Afonso da Silva'),
('Nelson Nery Junior'),
('Rogério Greco'),
('Fernando Capez'),
('Paulo Nader'),
('Guilherme de Souza Nucci'),
('César Roberto Bitencourt');
GO

-- Assuntos
INSERT INTO Assunto (Descricao) VALUES
('Direito Constitucional'),
('Direito Penal'),
('Direito Civil'),
('Direito Administrativo'),
('Direito do Trabalho'),
('Direito Processual Civil'),
('Direito Processual Penal'),
('Direito Tributário'),
('Direito Empresarial'),
('Direito Ambiental');
GO

-- Livros
INSERT INTO Livro (Titulo, Editora, Edicao, AnoPublicacao) VALUES
('Curso de Direito Penal - Parte Geral', 'Saraiva', 11, '2022'),
('Código Civil Comentado', 'RT', 10, '2021'),
('Direito Administrativo Moderno', 'Malheiros', 8, '2022'),
('Curso de Direito do Trabalho', 'Forense', 7, '2021'),
('Manual de Direito Processual Civil', 'Revista dos Tribunais', 9, '2023'),
('Curso de Direito Tributário', 'Forense', 12, '2020'),
('Manual de Direito Empresarial', 'Atlas', 6, '2021'),
('Curso de Direito Ambiental Brasileiro', 'Saraiva', 5, '2020'),
('Criminologia e Direito Penal', 'Juspodivm', 3, '2023');
GO

-- Relacionamento Livro x Autor
INSERT INTO LivroAutor (IdLivro, IdAutor) VALUES
(1, 5), 
(1, 6),
(2, 2), 
(2, 4),
(3, 3),
(4, 7),
(5, 2), 
(5, 4),
(6, 7),
(7, 7),
(8, 2),
(9, 9), 
(9, 5);
GO

-- Relacionamento Livro x Assunto
INSERT INTO LivroAssunto (IdLivro, IdAssunto) VALUES
(1, 2),
(2, 3),
(3, 4),
(4, 5),
(5, 6),
(6, 8),
(7, 9),
(8, 10),
(9, 2),
(9, 7);
GO

-- LivroValor (preços)
INSERT INTO LivroValor (IdLivro, TipoVenda, Valor) VALUES
(1, 'Impresso', 129.90),
(1, 'E-book', 59.90),
(2, 'Impresso', 119.00),
(3, 'Impresso', 135.50),
(4, 'E-book', 89.90),
(5, 'Impresso', 145.00),
(6, 'E-book', 75.00),
(7, 'Impresso', 155.90),
(8, 'E-book', 95.00),
(9, 'Impresso', 160.00);
GO

PRINT 'Dados inseridos com sucesso!';
GO

-- ==========================================================
-- 6️⃣ CONSULTA FINAL DE VERIFICAÇÃO
-- ==========================================================
SELECT 
    L.IdLivro,
    L.Titulo,
    A.Nome AS Autor,
    S.Descricao AS Assunto,
    V.TipoVenda,
    V.Valor
FROM Livro L
JOIN LivroAutor LA ON L.IdLivro = LA.IdLivro
JOIN Autor A ON LA.IdAutor = A.IdAutor
JOIN LivroAssunto LS ON L.IdLivro = LS.IdLivro
JOIN Assunto S ON LS.IdAssunto = S.IdAssunto
JOIN LivroValor V ON L.IdLivro = V.IdLivro
ORDER BY L.Titulo;
GO

PRINT 'Script executado com sucesso! Banco BibliotecaDesafio pronto para uso!';
GO
