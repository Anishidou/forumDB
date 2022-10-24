create database Forum

use Forum

create table Curso (
id int not null identity primary key,
nome char(100) not null
)

create table Usuario (
id int not null identity primary key,
matricula varchar(10),
id_curso int not null foreign key references Curso(id),
nome char(100) not null,
email varchar(150) not null,
senha varchar(255) not null,
Foto varchar(100),
administrador bit
)

create table Pergunta (
id int not null identity primary key,
id_usuario int foreign key references Usuario(id),
id_curso int foreign key references Curso(id),
texto varchar(350) not null,
titulo varchar(40) not null,
respondida bit,
horario datetime,
)

create table Resposta (
id int not null identity primary key,
id_pergunta int foreign key references Pergunta(id),
id_usuario int foreign key references Usuario(id),
texto varchar(255),
horario datetime
)

insert into Curso values ('Sistema de Informação')
insert into Curso values ('Administração')