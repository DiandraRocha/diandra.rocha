--1
SELECT IDEmpregado AS [C�digo Empregado], NomeEmpregado AS Nome, 
DATEDIFF(YEAR, DataNascimento, DataAdmissao) AS [Idade na Data de Admiss�o]
FROM Empregado WHERE YEAR(DataAdmissao) = 1980;

--2
SELECT TOP(1) WITH TIES Cargo, COUNT(1) AS Contagem FROM Empregado GROUP BY Cargo ORDER BY Contagem DESC; 

--3
SELECT UF AS Cidade, COUNT(Nome) AS [Quantidade de Cidades]
FROM Cidade GROUP BY UF ORDER BY [Quantidade de Cidades] DESC;

--4
INSERT INTO Departamento(IDDepartamento, NomeDepartamento, Localizacao) VALUES (70, 'Inova��o', 'S�o Leopoldo');
UPDATE Departamento SET localizacao = 'S�O LEOPOLDO' WHERE IDDepartamento = 70;
UPDATE Empregado SET IDDepartamento = 70 WHERE MONTH(DataAdmissao) = 12 AND Cargo != 'Atendente';