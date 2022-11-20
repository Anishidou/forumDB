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

create table Denuncia (
id int not null identity primary key,
id_pergunta int foreign key references Pergunta(id),
id_resposta int foreign key references Resposta(id),
id_usuario int foreign key references Usuario(id),
texto varchar(255),
)

create table Nota (
id int not null identity primary key,
id_pergunta int foreign key references Pergunta(id),
id_resposta int foreign key references Resposta(id),
id_usuario int foreign key references Usuario(id),
valor int not null,
)

insert into Curso values ('Nenhum')
insert into Curso values ('Direito')
insert into Curso values ('Educação Física')
insert into Curso values ('Enfermagem')
insert into Curso values ('Engenharia De Produção')
insert into Curso values ('Ciências Contábeis')
insert into Curso values ('Logística')
insert into Curso values ('Ciências Biológicas')
insert into Curso values ('Letras')
insert into Curso values ('Pedagogia')
insert into Curso values ('Engenharia Civil')
insert into Curso values ('Engenharia Elétrica/Eletrônica')
insert into Curso values ('Engenharia Mecânica')
insert into Curso values ('Administração')
insert into Curso values ('Comunicação Social')
insert into Curso values ('Economia')
insert into Curso values ('Sistemas De Informação')
insert into Curso values ('Automação Industrial')
insert into Curso values ('Gestão Da Prod. Industrial')
insert into Curso values ('Gestão De RH')

insert into Usuario values ('1', 1, 'Excluido', 'nulo', '123', 'blank.png', 0)