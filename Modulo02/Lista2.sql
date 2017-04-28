--1
SELECT IDEmpregado, NomeEmpregado FROM Empregado ORDER BY DataAdmissao;

--2
SELECT * FROM Empregado WHERE comissao IS NULL OR comissao = 0 ORDER BY salario;

--3
SELECT IDEmpregado AS C�digo,
NomeEmpregado AS Nome, 
(salario*13) AS [Sal�rio Anual],
(comissao * 12) AS [Comiss�o Anual],
(salario*13 + (COALESCE (comissao * 12, 0))) AS [Renda Anual]
FROM Empregado;

--4
SELECT IDEmpregado AS C�digo,
NomeEmpregado AS Nome, 
Cargo AS [Cargo Funcion�rio],
Salario AS [Sal�rio Mensal]
FROM Empregado WHERE (salario*12)<=18.500 OR Cargo = 'Atendente';


