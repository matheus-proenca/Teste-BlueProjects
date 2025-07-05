CREATE DATABASE TechStoreDB;

CREATE TABLE Produtos(
    ProdutoId INT IDENTITY(1, 1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(255),
    Preco DECIMAL(10,2) NOT NULL,
    Estoque INT NOT NULL,
    DataCadastro DATETIME DEFAULT GETDATE()
);

INSERT INTO Produtos (Nome, Descricao, Preco, Estoque)
VALUES
    ('Mouse','E um Mouse', 59.99, 100),
    ('Pendrive', 'E um Pendrive', 9.99, 100),
    ('Teclado', 'E um Teclado', 99.99, 100),
    ('Cabo-USB', 'E um Cabo-USB', 79.99, 100),
    ('MousePad', 'E um MousePad', 69.99, 100);

GO
    CREATE PROCEDURE FiltroProdutos(
        @Nome VARCHAR(100) = null,
        @PrecoMax DECIMAL(10,2) = null,
        @PrecoMim DECIMAL(10,2) = null,
        @Pag INT,
        @PagTamanho INT
    ) 
    AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @Inicio int;
        SET @Inicio = (@Pag * @PagTamanho) + 1;

        SELECT * FROM(
            SELECT ProdutoId,Nome,Descricao,Preco,Estoque,DataCadastro,
                ROW_NUMBER() OVER(ORDER BY Nome ASC) AS NumeroLinhas
            FROM Produtos
            WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%') AND 
                  (@PrecoMax IS NULL OR Preco <= @PrecoMax) AND
                  (@PrecoMim IS NULL OR Preco >= @PrecoMim)
        )AS ResultadoProduto
        WHERE NumeroLinhas BETWEEN @Inicio AND (@Inicio + @PagTamanho) - 1
    END