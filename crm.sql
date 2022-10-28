
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

create table zona(
	id smallint not null unique,
	zona varchar(20),
	primary key(id)
)

create table sector(
	id smallint not null unique,
	sector varchar(20)
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
	nombre varchar(20) not null,
	descripcion varchar(50) not null,
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
	nombre_usuario varchar(20) not null,
	clave varchar(500) not null,
	rol smallint not null foreign key references rol (id),
	departamento smallint not null foreign key references departamento(id)
	primary key(cedula)
)

create table actividad(
	id smallint not null unique,
	descripcion varchar(25),
	fechaInicio date,
	fechaFin date,
	asesor varchar(10) not null foreign key references usuario(cedula)
	primary key(id)
)

create table estado(
    id smallint not null unique,
	estado varchar(20) not null
	primary key(id)
)

create table tarea
(
	id smallint not null unique,
	fechaFinalizacion date not null,
	fechaCreacion date not null,
	informacion varchar(15) not null,
	asesor varchar(10) not null foreign key references usuario(cedula),
	estado smallint not null foreign key references estado(id),

	primary key(id)
)



create table cliente(
	nombre_cuenta varchar (10) not null primary key,
	celular  varchar(8) not null,
	telefono varchar(8) not null,
	correo   varchar(50) not null,
	sitio    varchar(50) not null,
	contacto_principal varchar(20) not null,
	asesor varchar(10) not null foreign key references usuario(cedula),
	IDzona smallint not null foreign key references zona(id),
	IDsector smallint not null foreign key references sector(id),
	IDmoneda smallint not null foreign key references moneda(id),
)

create table tipoContacto(
	id smallint not null unique,
	tipo varchar(20) not null
	primary key(id)
)


create table contacto(

	idContacto smallint primary key not null,
	nombre varchar(20) not null, 
	motivo varchar(50) not null,
	telefono varchar(8) not null, 
	correo  varchar(25) not null,
	direccion varchar(50) not null,
	descripcion varchar(50) not null,
	cliente varchar(10) not null foreign key references cliente (nombre_cuenta), 
	zona smallint not null foreign key references zona(id),
	sector smallint not null foreign key references sector(id),
	asesor varchar(10) not null foreign key references usuario(cedula),
	tipoContacto smallint not null foreign key references tipoContacto(id),
	estado smallint not null foreign key references estado(id)
)

create table actividadesXcontacto(
    contacto smallint not null foreign key references contacto (idContacto), 
	actividad smallint not null foreign key references actividad(id),
	primary key(contacto,actividad)
)

create table tareaXcontacto(
    contacto smallint not null foreign key references contacto (idContacto), 
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

create table cotizacionDenegada(
 id varchar(20) unique,
 razon varchar(100)
 primary key( id)
)

create table rivales(
	id varchar(20) unique,
	nombre varchar(50),
	primary key(id)
)




create table cotizaciones(

	numeroCotizacion varchar (10) primary key,
	nombreOportunidad varchar (10) not null,
	fechaCotizacion date not null,  
	fechaCierra date not nulL,   
	ordenCompra varchar (10) not null, 
	descripcion varchar (50) not null,
	factur varchar (20) not null,
		
	zona smallint not null foreign key references zona(id),
	sector smallint not null foreign key references sector(id),
	moneda smallint not null foreign key references moneda(id),
	contactoAsociado smallint not null foreign key references contacto (idContacto), 
	asesor varchar (10) not null foreign key references usuario(cedula),
	nombreCuenta varchar (10) not null foreign key references cliente(nombre_cuenta),
	etapa smallint not null foreign key references etapa(id),
	probabilidad smallint  not null foreign key references probabilidad(id),
	tipo smallint not null foreign key references tipoCotizacion(id),

	razonDenegacion varchar(20)  foreign key references cotizacionDenegada(id),
	contraQuien varchar(20)  foreign key references rivales(id),
)

create table productosXcotizacion(
 codigo_producto varchar(10) not null foreign key references producto (codigo),
 numero_cotizacion varchar(10) not null foreign key references cotizaciones (numeroCotizacion),
 cantidad smallint not null,
 precioNegociado decimal(9,2) not null,
 primary key(codigo_producto, numero_cotizacion)
)

create table tareaXcotizacion(
 numero_cotizacion varchar(10) not null foreign key references cotizaciones (numeroCotizacion),
 tarea_cotizacion smallint not null foreign key references tarea (id)
 primary key(numero_cotizacion,tarea_cotizacion )
)

create table actividadXcotizacion(
 numero_cotizacion varchar(10) not null foreign key references cotizaciones (numeroCotizacion),
 actividad_cotizacion smallint not null foreign key references actividad(id)
 primary key(numero_cotizacion,actividad_cotizacion)
)



create table ejecucion(
	IDejecucion smallint not null unique,

	propietario  varchar (10) not null,
	nombre varchar (10) not null, 
	fecha_ejecucion date not null,
	fecha_cierra date not null, 


	numero_cotizacion varchar (10) not null foreign key references cotizaciones (numeroCotizacion),
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

-------------------------------------------------------------------------------------------------------
INSERT into departamento   values (1, 'IT')
INSERT INTO ROL(id, tipoRol) VALUES (1, 'Edicion')
INSERT INTO ROL(id, tipoRol) VALUES (2, 'Visualizacion')
INSERT INTO ROL(id, tipoRol) VALUES (3, 'Reporteria')

---------------------------------------------------------------------------------------------------------


EXEC agregarUsuario '118470507','Adjany','Gard','Alpizar','adjany08','1234',1,1,'adjany'

EXEC agregarFamilia 'LA204', 'Lacteos', 'Productos lacteos'

EXEC agregarFamilia 'HO', 'Hogar', 'Productos de la hogar'
EXEC agregarFamilia 'CO', 'Cocina', 'Productos de la Cocina'
EXEC agregarFamilia 'LI', 'Limpieza', 'Productos de limpieza'
EXEC agregarFamilia 'JA', 'Jardin', 'Productos del jardin'
EXEC agregarFamilia 'BA', 'Baño', 'Productos para el baño'
EXEC agregarFamilia 'ESC','Escolares', 'Productos escolares'
EXEC agregarFamilia 'FEM','Femeninos', 'Productos para el higiene femeninos'

EXEC agregarTipoContacto 1, 'Acercamiento'
EXEC agregarTipoContacto 2, 'Prospección'
EXEC agregarTipoContacto 3, 'Oportunidad'

EXEC agregarEstado 1,'En proceso'
EXEC agregarEstado 2, 'Finalizado'

EXEC agregarTarea 1, 'Pendiente', '2022-03-25', 'Terminar crud', '2023-03-25'
EXEC agregarTarea 2, 'Pendiente', '2022-03-25', 'Terminar excel', '2023-03-25'
EXEC agregarTarea 3, 'Pendiente', '2022-03-25', 'Terminar word', '2023-03-25'
EXEC agregarTarea 4, 'Pendiente', '2022-03-25', 'Terminar power', '2023-03-25'

EXEC agregarActividad 1, 'Programar', '2023-03-25', '2023-03-26'
EXEC agregarActividad 2, 'Documentar', '2023-03-25', '2023-03-26'
EXEC agregarActividad 3, 'Hola mundo', '2023-03-25', '2023-03-26'
EXEC agregarActividad 4, 'Jugar fifa', '2023-03-25', '2023-03-26'

--agregarProducto '1', 'Esponja', 'Para el cuerpo', 4512.99, 1, 'BA'


insert into moneda values( 1, 'CRC₡')
insert into moneda values( 2, 'USD$')
insert into moneda values( 3, 'EUR€')
insert into moneda values( 4, 'JPY¥')
insert into moneda values( 5, 'GPB£')

insert into zona values(1, 'Limon')
insert into zona values(2, 'Cartago')
insert into zona values(3, 'Alajuela')
insert into zona values(4, 'Guanacaste')



insert into sector values(1, 'Norte')
insert into sector values(2, 'Sur')
insert into sector values(3, 'Este')
insert into sector values(4, 'Oeste')

insert into cliente values('Adjany', '7106204','7105874', 'adjany@gmail.com','www.aostore.com', 'Kevin', '118470507', 1,1, 4)



insert into tarea values(1, '2020-05-12','2020-05-12','LML','118470507',1)
insert into tarea values(2, '2020-05-12','2020-05-12','LML','118470507',1)

insert into etapa values (1, 'Desarrollo')
insert into etapa values (2, 'Ejecucion')
insert into etapa values (3, 'Planeamiento')
insert into etapa values (4, 'Valoracion')

insert into tipoCotizacion values(1, 'Comercial')
insert into tipoCotizacion values(2, 'Manofactura')
insert into tipoCotizacion values(3, 'Contratistas')

insert into probabilidad values (1, 10)
insert into probabilidad values (2, 20)
insert into probabilidad values (3, 30)

insert into cotizacionDenegada values(1, 'N/A')
insert into cotizacionDenegada values(2, 'Precio')
insert into cotizacionDenegada values(3, 'Calidad')

insert into rivales values(1, 'N/A')
insert into rivales values(2, 'Walmart')
insert into rivales values(3, 'Pricesmart')


insert into tareaXcontacto values(1,1)
insert into tareaXcontacto values(1,2)

select * from contacto