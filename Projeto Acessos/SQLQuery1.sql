create database ControleDeAcessos
GO
use ControleDeAcessos
GO
create table Usuarios
(
	 UsuarioId int primary key,
	 UsuarioNome varchar(100)
)
GO
create table Ambientes
(
	AmbienteId int primary key,
	AmbienteNome varchar(100)
)
GO
create table Permissoes
(
	UsuarioId int,
	AmbienteId int,
	foreign key (UsuarioId) references Usuarios(UsuarioId),
	foreign key (AmbienteId) references Ambientes(AmbienteId),
	primary key (UsuarioId, AmbienteId)
)
GO
create table LogsDeAmbientes
(
	AmbienteId int,
	LogDtAcesso DateTime,
	UsuarioId int,
	LogTipoAcesso bit,
	foreign key (UsuarioId) references Usuarios(UsuarioId),
	foreign key (AmbienteId) references Ambientes(AmbienteId)
)