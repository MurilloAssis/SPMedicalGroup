USE SP_MEDICAL_GROUP;
GO


--Listagem simples de todos os paciente do sistema
SELECT nomeUsuario 'Nome do Paciente', emailUsuario 'Email do paciente', titulotipoUsuario 'tipo de usuário', RG
FROM usuario u
INNER JOIN paciente p
ON u.idUsuario = p.idUsuario
INNER JOIN tipoUsuario tu
ON tu.idTipoUsuario = u.idTipoUsuario

--Listagem simples de todos os médicos cadastrados
SELECT *
FROM usuario u
INNER JOIN medico m
ON u.idUsuario = m.idUsuario

--Listagem dos pacientes cadastrados no sistema
SELECT nomeUsuario, emailUsuario, RG, ISNULL (Telefone, 'Não cadastrado') Telefone, enderecoPaciente
FROM usuario u
INNER JOIN paciente
ON u.idUsuario = paciente.idUsuario

--Listagem das consultas do sistema
SELECT UP.nomeUsuario Paciente, 
	   UM.nomeUsuario Medico,
	   E.tipoEspecializacao Especialização,
	   CONVERT(VARCHAR(25), C.dataConsulta, 113) 'Data da Consulta',
	   S.descricao Situação,
	   C.descricaoConsulta 'Descricao da consulta' 
  FROM CONSULTA C
 INNER JOIN SITUACAO S
    ON C.idSituacao = S.idSituacao
 INNER JOIN PACIENTE P
    ON C.idPaciente = P.idPaciente
 INNER JOIN MEDICO M
    ON C.idMedico = M.idMedico 
 INNER JOIN ESPECIALIZACAO E
    ON M.idEspecializacao = E.idEspecializacao
 INNER JOIN USUARIO UP
    ON P.idUsuario = UP.idUsuario
 INNER JOIN USUARIO UM
    ON M.idUsuario = UM.idUsuario;
GO

--Mostar a quantidade de usuarios
SELECT COUNT(idUsuario) 'Quantidade de Usuarios' FROM usuario;
GO

--Função para retornar a quantodade de médicos de um tipo de especialidade em especifico
CREATE FUNCTION MED_ESPECIALIZACAO(@tipoEspec VARCHAR(90))
RETURNS TABLE
     AS
 RETURN (
          SELECT @tipoEspec AS especializacao, COUNT(idEspecializacao) 'Código Médico'
		    FROM ESPECIALIZACAO
		   WHERE tipoEspecializacao LIKE '%' + @tipoEspec + '%'
        )
GO
SELECT * FROM MED_ESPECIALIZACAO('Psiquiatria');
GO

--Stored Procedurew para retornar a idade do paciente
CREATE PROCEDURE  IdadePaciente
@idade VARCHAR(30)
    AS
 BEGIN
SELECT nomeUsuario, DATEDIFF(YEAR, dataNasc,GETDATE())
    AS idade
  FROM USUARIO U
 INNER JOIN PACIENTE P
    ON U.idUsuario = P.idUsuario
 WHERE nomeUsuario = @idade
   END;
GO

EXEC IdadePaciente 'Mariana';
GO


