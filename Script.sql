create table tb_empleado(
	id int identity(1,1) ,
	documentoId varchar(15) primary key not null,
	nombres varchar(100) not null,
	apellidos varchar(100) not null,
	edad int not null,
	fechaNacimiento datetime not null,
	salario decimal(18,2) not null,
	fechaRegistro datetime 
)

insert into tb_empleado (documentoId,nombres,apellidos,edad,fechaNacimiento,salario,fechaRegistro)
values ('70116576','Demo','Pruebas QA',15,getdate()-800,1500,getdate())


create table tb_usuario(
	id int identity(1,1)  primary key not null,
	usuario varchar(20) not null,
	password varchar(20) not null,
)

insert into tb_usuario (usuario,password)
values ('admin','admin')