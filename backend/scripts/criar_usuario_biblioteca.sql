USE BibliotecaDesafio;
GO

-- Criar login no servidor para configurar na aplicação
CREATE LOGIN usuario_biblioteca
WITH PASSWORD = 'SenhaForte@2025!',
     CHECK_POLICY = ON,
     CHECK_EXPIRATION = ON;
GO

-- Criar usuário no banco de dados
CREATE USER usuario_biblioteca FOR LOGIN usuario_biblioteca;
GO

-- Conceder permissões de leitura e escrita
ALTER ROLE db_datareader ADD MEMBER usuario_biblioteca;
ALTER ROLE db_datawriter ADD MEMBER usuario_biblioteca;
GO

-- Opcional Permissões específicas
 GRANT SELECT, INSERT, UPDATE ON dbo.Livro TO usuario_biblioteca;
 GRANT SELECT ON dbo.Autor TO usuario_biblioteca;
 DENY DELETE ON dbo.Livro TO usuario_biblioteca;
 GO

PRINT 'Usuário "usuario_biblioteca" criado com sucesso no banco BibliotecaDesafio!';
