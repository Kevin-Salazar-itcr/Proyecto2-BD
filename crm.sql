USE master

DROP DATABASE CRM

create database CRM
go


use CRM
go



--**********************************
--creacion de las tablas


create table departamento(
	id smallint not null unique,
	nombre varchar(20),
	primary key(id)
)

create table moneda(
	id smallint not null unique,
	NombreMoneda varchar(10)
	primary key(id)
)

create table zonaSector(
	id smallint not null unique,
	zona varchar(20),
	sector varchar(20)
	primary key(id)
)



create table actividad(
	id smallint not null unique,
	descripcion varchar(25),
	fechaInicio date,
	fechaFin date
	primary key(id)
)

create table familia_producto(
	codigo varchar(10) not null unique,
	nombre varchar(15) not null,
	descripcion varchar(50) not null
	primary key(codigo)
)

create table producto(
	codigo varchar(10) unique not null,
	nombre varchar(10) not null,
	descripcion varchar(10) not null,
	precio decimal(9,2) not null, 
	activo smallint not null check (activo between 0 and 1 ),
	codigo_familia varchar(10) not null,
	primary key(codigo),
	foreign key (codigo_familia) references familia_producto (codigo)
)

create table rol(
	id smallint not null unique,
	tipoRol varchar(15) not null
	primary key(id)
)

create table usuario(
	cedula varchar(10) not null unique,
	nombre varchar (20)  not null,
 	apellido1 varchar (20) not null,
	apellido2 varchar (20) not null,
	departamento varchar (20) not null,
	nombre_usuario varchar(10) not null,
	clave varchar(10) not null,
	rol smallint not null foreign key references rol (id),
	primary key(cedula)
)


create table modulo(
	id smallint not null primary key,
	nombre varchar(25)
)

create table operaciones(
	
	id smallint not null primary key,
	nombre varchar(25),
	idModulo smallint not null foreign key references modulo(id)
)

create table rolxoperaciones(

	id smallint not null primary key,
	idRol smallint not null foreign key references rol(id),
	idModulo smallint not null foreign key references operaciones(id)

)

create table tarea(
	id smallint not null unique,
	estado varchar(10) not null,
	fechaFinalizacion date not null,
	informacion varchar(15) not null,
	primary key(id)
)

create table cliente(
	nombre_cuenta varchar (10) not null primary key,
	celular  varchar(8) not null,
	telefono varchar(8) not null,
	correo   varchar(25) not null,
	sitio    varchar(25) not null,
	contacto_principal varchar(20) not null,
	asesor varchar(10) not null foreign key references usuario(cedula),
	IDzona smallint not null foreign key references zonaSector(id),
	IDmoneda smallint not null foreign key references moneda(id),
)

create table tipoContacto(
	id smallint not null unique,
	tipo varchar(20) not null
	primary key(id)
)

create table estado(
    id smallint not null unique,
	estado varchar(20) not null
	primary key(id)
)


create table contacto(
	nombre varchar(20) primary key not null, 
	motivo varchar(50) not null,
	telefono varchar(8) not null, 
	correo  varchar(25) not null,
	direccion varchar(50) not null,
	descripcion varchar(50) not null,
	cliente varchar(10) not null foreign key references cliente (nombre_cuenta), 
	zona smallint not null foreign key references zonaSector(id),
	asesor varchar(10) not null foreign key references usuario(cedula),
	tipoContacto smallint not null foreign key references tipoContacto(id),
	estado smallint not null foreign key references estado(id)
)

create table actividadesXcontacto(
    contacto varchar(20) not null foreign key references contacto (nombre), 
	actividad smallint not null foreign key references actividad(id),
	primary key(contacto,actividad)
)

create table tareaXcontacto(
    contacto varchar(20) not null foreign key references contacto (nombre), 
	tarea smallint not null foreign key references tarea(id),
	primary key(contacto,tarea)
)

create table etapa(
	id smallint not null unique,
	etapa varchar(20) not null,
	primary key (id)
)

create table probabilidad(
	id smallint not null unique,
	etapa float not null,
	primary key (id)
)


create table inflacion(
	id smallint not null unique,
	porcentaje float not null,
	primary key (id)
)

create table tipoCotizacion(
	id smallint not null unique,
	tipo varchar(25) not null,
	primary key (id)
)



create table cotizaciones(
	numero_cotizacion varchar (10) primary key,
	nombre_oportunidad varchar (10) not null,
	fecha_cotizacion date not null,  
	fecha_cierra date not nulL,   
	orden_de_compra varchar (10) not null, 
	descripcion varchar (50) not null,  
		
	zona smallint not null foreign key references zonaSector(id),
	moneda smallint not null foreign key references moneda(id),
	contacto_asociado varchar (20) not null foreign key references contacto (nombre), 
	asesor varchar (10) not null foreign key references usuario(cedula),
	nombre_cuenta varchar (10) not null foreign key references cliente(nombre_cuenta),
	etapa smallint not null foreign key references etapa(id),
	probabilidad smallint  not null foreign key references probabilidad(id),
	tipo smallint not null foreign key references tipoCotizacion(id),
	inflacion smallint not null foreign key references inflacion(id),
)

create table cotizacionDenegada(
 numero_cotizacion varchar(10) not null foreign key references cotizaciones (numero_cotizacion),
 razon varchar(100)
 primary key( numero_cotizacion)
)

create table productosXcotizacion(
 codigo_producto varchar(10) not null foreign key references producto (codigo),
 numero_cotizacion varchar(10) not null foreign key references cotizaciones (numero_cotizacion),
 cantidad smallint not null,
 precioNegociado decimal(9,2) not null,
 primary key(codigo_producto, numero_cotizacion)
)

create table tareaXcotizacion(
 numero_cotizacion varchar(10) not null foreign key references cotizaciones (numero_cotizacion),
 tarea_cotizacion smallint not null foreign key references tarea (id)
 primary key(numero_cotizacion,tarea_cotizacion )
)

create table actividadXcotizacion(
 numero_cotizacion varchar(10) not null foreign key references cotizaciones (numero_cotizacion),
 actividad_cotizacion smallint not null foreign key references actividad(id)
 primary key(numero_cotizacion,actividad_cotizacion)
)



create table ejecucion(
	IDejecucion smallint not null unique,

	propietario  varchar (10) not null,
	nombre varchar (10) not null, 
	fecha_ejecucion date not null,
	fecha_cierra date not null, 


	numero_cotizacion varchar (10) not null foreign key references cotizaciones (numero_cotizacion),
	asesor varchar (10) not null foreign key references usuario (cedula),
	nombre_cuenta varchar (10) not null foreign key references cliente(nombre_cuenta),
	departamento smallint not null foreign key references departamento(id),

	primary key(IDejecucion)

)

create table actividadesXejecucion(
    ejecucion smallint not null foreign key references ejecucion (IDejecucion), 
	actividad smallint not null foreign key references actividad(id),
	primary key(ejecucion,actividad)
)

create table tareaXejecucion(
    ejecucion smallint not null foreign key references ejecucion (IDejecucion), 
	tarea smallint not null foreign key references tarea(id),
	primary key(ejecucion,tarea)
)


insert into modulo(id, nombre) values (1 , 'usuarios')
insert into modulo(id, nombre) values (2 , 'Roles')
insert into modulo(id, nombre) values (3 , 'Productos')
insert into modulo(id, nombre) values (4 , 'Ordenes')
insert into modulo(id, nombre) values (5 , 'Configuracion')

insert into operaciones(id, nombre, idModulo) VALUES (1, 'agregar', 1)
insert into operaciones(id, nombre, idModulo) VALUES (2, 'editar', 1)
insert into operaciones(id, nombre, idModulo) VALUES (3, 'eliminar', 1)
insert into operaciones(id, nombre, idModulo) VALUES (4, 'ver', 1)


insert into operaciones(id, nombre, idModulo) VALUES (6, 'agregar', 2)
insert into operaciones(id, nombre, idModulo) VALUES (7, 'editar', 2)
insert into operaciones(id, nombre, idModulo) VALUES (8, 'eliminar', 2)
insert into operaciones(id, nombre, idModulo) VALUES (9, 'ver', 2)

insert into operaciones(id, nombre, idModulo) VALUES (11, 'agregar', 1)
insert into operaciones(id, nombre, idModulo) VALUES (12, 'editar', 1)
insert into operaciones(id, nombre, idModulo) VALUES (13, 'eliminar', 1)
insert into operaciones(id, nombre, idModulo) VALUES (14, 'editar', 1)

INSERT INTO ROL(id, tipoRol) VALUES (1, 'Admi')
INSERT INTO ROL(id, tipoRol) VALUES (2, 'Usuario')

INSERT INTO usuario(cedula, nombre, apellido1,apellido2, nombre_usuario, clave, departamento, rol) 
VALUES (118470507, 'Adjany', 'Gard', 'Alpizar', 'adjany08', '1234', 1, 1)

INSERT INTO usuario(cedula, nombre, apellido1,apellido2, nombre_usuario, clave, departamento, rol) 
VALUES (118470508, 'Kevin', 'Gard', 'Alpizar', 'kevin', '1234', 1, 2)

select * FROM usuario
left join rol on rol.id = usuario.rol


insert INTO rolxoperaciones(id, idRol, idModulo)
VALUES(1, 1, 1)

insert INTO rolxoperaciones(id, idRol, idModulo)
VALUES(2, 1, 2)

insert INTO rolxoperaciones(id, idRol, idModulo)
VALUES(3, 1, 3)

insert INTO rolxoperaciones(id, idRol, idModulo)
VALUES(4, 1, 4)

insert INTO rolxoperaciones(id, idRol, idModulo)
VALUES(5, 2, 2)
