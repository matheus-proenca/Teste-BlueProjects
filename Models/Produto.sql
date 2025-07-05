CREATE TABLE Produtos(
    ProdutoId INT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(255),
    Preco DECIMAL(10,2) NOT NULL,
    Estoque INT NOT NULL,
    DataCadastro DATETIME DEFAULT GETDATE()
);

INSERT INTO Produtos (Nome, Descricao, Preco, Estoque)
VALUES
    ('Faca','É uma faca', 59.99, 100),
    ('Colher', 'É uma colher', 9.99, 100),
    ('Garfo', 'É um garfo', 9.99, 100),
    ('Prato', 'É um prato', 79.99, 100),
    ('Copo', 'É um copo', 69.99, 100);
