CREATE TABLE dbo.tb_paciente (
    id_paciente     INT           IDENTITY(1,1) NOT NULL,
    dsc_nome        VARCHAR(255)  NOT NULL,
    dat_nascimento  DATETIME      NOT NULL,
    dat_criacao     DATETIME      NOT NULL,
    CONSTRAINT PK_TB_PACIENTE PRIMARY KEY (id_paciente)
);

CREATE TABLE dbo.tb_agendamento (
    id_agendamento  INT           IDENTITY(1,1) NOT NULL,
    id_paciente     INT           NOT NULL,
    id_vacina       INT           NOT NULL,
    dat_agendamento DATE          NOT NULL,
    hor_agendamento TIME          NOT NULL,
    dsc_status      VARCHAR(50)   NOT NULL,
    dsc_local       VARCHAR(255)  NULL,
    dat_criacao     DATETIME      NOT NULL,
    CONSTRAINT PK_TB_AGENDAMENTO PRIMARY KEY (id_agendamento),
    CONSTRAINT FK_AGENDAMENTO_PACIENTE FOREIGN KEY (id_paciente)
        REFERENCES dbo.tb_paciente (id_paciente)
);

INSERT INTO dbo.tb_paciente (dsc_nome, dat_nascimento, dat_criacao) VALUES
('Ana Lima',         '1990-03-15', GETDATE()),
('Bruno Santos',     '1985-07-22', GETDATE()),
('Carlos Oliveira',  '1978-11-03', GETDATE()),
('Diana Pereira',    '1995-01-08', GETDATE()),
('Eduardo Costa',    '2000-06-30', GETDATE()),
('Fernanda Silva',   '1988-09-14', GETDATE()),
('Gabriel Rocha',    '1972-04-25', GETDATE()),
('Helena Martins',   '1993-12-01', GETDATE()),
('Igor Alves',       '1980-08-17', GETDATE()),
('Julia Souza',      '1998-02-28', GETDATE()),
('Lucas Ferreira',   '1965-05-10', GETDATE()),
('Maria Gomes',      '2001-10-20', GETDATE()),
('Nicolas Carvalho', '1975-03-07', GETDATE()),
('Olivia Castro',    '1991-07-16', GETDATE()),
('Pedro Barbosa',    '1987-11-29', GETDATE());

INSERT INTO dbo.tb_agendamento (id_paciente, id_vacina, dat_agendamento, hor_agendamento, dsc_status, dsc_local, dat_criacao) VALUES
-- 01/05
( 1, 1, '2026-05-01', '08:00:00', 'Agendado',  'UBS Centro', GETDATE()),
( 2, 2, '2026-05-01', '08:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 3, 3, '2026-05-01', '10:00:00', 'Realizado', 'UBS Sul',    GETDATE()),
( 4, 1, '2026-05-01', '10:00:00', 'Realizado', 'UBS Centro', GETDATE()),
( 5, 2, '2026-05-01', '14:00:00', 'Agendado',  'UBS Leste',  GETDATE()),
( 6, 3, '2026-05-01', '14:00:00', 'Cancelado', 'UBS Norte',  GETDATE()),
( 7, 1, '2026-05-01', '16:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
-- 02/05
( 8, 1, '2026-05-02', '08:00:00', 'Realizado', 'UBS Centro', GETDATE()),
( 9, 2, '2026-05-02', '09:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
(10, 3, '2026-05-02', '09:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
(11, 1, '2026-05-02', '11:00:00', 'Realizado', 'UBS Leste',  GETDATE()),
(12, 2, '2026-05-02', '11:00:00', 'Agendado',  'UBS Centro', GETDATE()),
(13, 3, '2026-05-02', '15:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
(14, 1, '2026-05-02', '15:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
-- 03/05
(15, 2, '2026-05-03', '08:00:00', 'Agendado',  'UBS Centro', GETDATE()),
( 1, 3, '2026-05-03', '08:00:00', 'Cancelado', 'UBS Norte',  GETDATE()),
( 2, 1, '2026-05-03', '10:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
( 3, 2, '2026-05-03', '13:00:00', 'Agendado',  'UBS Leste',  GETDATE()),
( 4, 3, '2026-05-03', '13:00:00', 'Realizado', 'UBS Centro', GETDATE()),
( 5, 1, '2026-05-03', '15:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 6, 2, '2026-05-03', '15:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
-- 04/05
( 7, 3, '2026-05-04', '09:00:00', 'Agendado',  'UBS Centro', GETDATE()),
( 8, 1, '2026-05-04', '09:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 9, 2, '2026-05-04', '11:00:00', 'Realizado', 'UBS Sul',    GETDATE()),
(10, 3, '2026-05-04', '11:00:00', 'Realizado', 'UBS Leste',  GETDATE()),
(11, 1, '2026-05-04', '14:00:00', 'Agendado',  'UBS Centro', GETDATE()),
(12, 2, '2026-05-04', '14:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
(13, 3, '2026-05-04', '16:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
(14, 1, '2026-05-04', '16:00:00', 'Cancelado', 'UBS Centro', GETDATE()),
-- 05/05
(15, 2, '2026-05-05', '08:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 1, 3, '2026-05-05', '08:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
( 2, 1, '2026-05-05', '10:00:00', 'Realizado', 'UBS Leste',  GETDATE()),
( 3, 2, '2026-05-05', '10:00:00', 'Realizado', 'UBS Centro', GETDATE()),
( 4, 3, '2026-05-05', '13:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 5, 1, '2026-05-05', '13:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
( 6, 2, '2026-05-05', '15:00:00', 'Agendado',  'UBS Centro', GETDATE()),
-- 06/05
( 7, 3, '2026-05-06', '09:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 8, 1, '2026-05-06', '11:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
( 9, 2, '2026-05-06', '11:00:00', 'Realizado', 'UBS Leste',  GETDATE()),
(10, 3, '2026-05-06', '14:00:00', 'Agendado',  'UBS Centro', GETDATE()),
(11, 1, '2026-05-06', '14:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
(12, 2, '2026-05-06', '16:00:00', 'Cancelado', 'UBS Sul',    GETDATE()),
(13, 3, '2026-05-06', '16:00:00', 'Agendado',  'UBS Centro', GETDATE()),
-- 07/05
(14, 1, '2026-05-07', '08:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
(15, 2, '2026-05-07', '08:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
( 1, 3, '2026-05-07', '10:00:00', 'Agendado',  'UBS Leste',  GETDATE()),
( 2, 1, '2026-05-07', '14:00:00', 'Agendado',  'UBS Centro', GETDATE()),
( 3, 2, '2026-05-07', '14:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 4, 3, '2026-05-07', '16:00:00', 'Agendado',  'UBS Sul',    GETDATE());


-- 28/05 — 7 agendamentos
INSERT INTO dbo.tb_agendamento (id_paciente, id_vacina, dat_agendamento, hor_agendamento, dsc_status, dsc_local, dat_criacao) VALUES
( 5, 1, '2026-04-28', '08:00:00', 'Agendado',  'UBS Centro', GETDATE()),
( 6, 2, '2026-04-28', '08:00:00', 'Realizado', 'UBS Norte',  GETDATE()),
( 7, 3, '2026-04-28', '10:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
( 8, 1, '2026-04-28', '10:00:00', 'Agendado',  'UBS Leste',  GETDATE()),
( 9, 2, '2026-04-28', '13:00:00', 'Cancelado', 'UBS Centro', GETDATE()),
(10, 3, '2026-04-28', '14:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
(11, 1, '2026-04-28', '14:00:00', 'Agendado',  'UBS Sul',    GETDATE()),

-- 29/05 — 7 agendamentos
(12, 2, '2026-04-29', '09:00:00', 'Agendado',  'UBS Centro', GETDATE()),
(13, 3, '2026-04-29', '09:00:00', 'Realizado', 'UBS Norte',  GETDATE()),
(14, 1, '2026-04-29', '11:00:00', 'Agendado',  'UBS Sul',    GETDATE()),
(15, 2, '2026-04-29', '11:00:00', 'Agendado',  'UBS Leste',  GETDATE()),
( 1, 3, '2026-04-29', '14:00:00', 'Cancelado', 'UBS Centro', GETDATE()),
( 2, 1, '2026-04-29', '15:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 3, 2, '2026-04-29', '15:00:00', 'Agendado',  'UBS Sul',    GETDATE()),

-- 30/05 — 7 agendamentos
( 4, 3, '2026-04-30', '08:00:00', 'Agendado',  'UBS Centro', GETDATE()),
( 5, 1, '2026-04-30', '08:00:00', 'Agendado',  'UBS Norte',  GETDATE()),
( 6, 2, '2026-04-30', '10:00:00', 'Realizado', 'UBS Sul',    GETDATE()),
( 7, 3, '2026-04-30', '13:00:00', 'Agendado',  'UBS Leste',  GETDATE()),
( 8, 1, '2026-04-30', '13:00:00', 'Agendado',  'UBS Centro', GETDATE()),
( 9, 2, '2026-04-30', '15:00:00', 'Cancelado', 'UBS Norte',  GETDATE()),
(10, 3, '2026-04-30', '16:00:00', 'Agendado',  'UBS Sul',    GETDATE());
