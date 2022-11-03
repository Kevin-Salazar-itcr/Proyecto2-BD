--USE master
--drop database CRM


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

create table ValorPresenteCotizaciones(
	idCotizacion SMALLINT primary key not null,
	contactoAsociado SMALLINT not null foreign key references contacto(idContacto),
	nombreOportunidad varchar(10) not null,
	anioCotizacion smallint,
	nombreCuenta varchar(10) foreign key references cliente(nombre_cuenta),
	totalCotizacion decimal(10,2), 
	totalValorPresente decimal(10,2)
)

-------------------------------------------------------Seccion de procedimientos almacenado------------------------------------------------------------------------------

use CRM

--Query creado para los storaged procedures de las diferentes tablas 



--Procedimientos almacenados para manejar las familias de productos

go
create procedure agregarFamilia
@codigo varchar(10),
@nombre varchar (20),
@descripcion varchar (50)
as
IF EXISTS (select codigo from familia_producto where codigo = @codigo)
	BEGIN
		return -1;
	END
ELSE
BEGIN TRY
	insert into familia_producto(codigo, nombre, descripcion) 
	values(@codigo, @nombre, @descripcion)
	return 0
END TRY
BEGIN CATCH
	return -1
end catch

--Procedimientos almacenados para manejar los productos


go

create procedure agregarProducto
	@codigo varchar(10), 
	@nombre varchar(10), 
	@descripcion varchar(10), 
	@precio decimal(9,2), 
	@activo smallint, 
	@codigo_familia varchar(10)
as
IF EXISTS (select codigo from producto where codigo = @codigo)
	BEGIN
		return -1;
	END
ELSE
BEGIN
	BEGIN TRY
		INSERT INTO producto(codigo, nombre, descripcion,precio,activo,codigo_familia ) 
		VALUES(@codigo, @nombre, @descripcion,@precio,@activo,@codigo_familia)
		return 0
	END TRY
	BEGIN CATCH
		return -1
	END CATCH;
END

go

create procedure editarProducto
	@codigo varchar(10), 
	@nombre varchar(10), 
	@descripcion varchar(10),
	@precio decimal(9,2), 
	@activo smallint,
	@familiaProducto varchar(10)
as
begin
BEGIN TRY
	UPDATE producto
	SET codigo = @codigo, nombre = @nombre ,descripcion = @descripcion, precio = @precio, activo = @activo, codigo_familia = @familiaProducto
	Where @codigo = codigo;
	return 0
END TRY
BEGIN CATCH
	return -1
END CATCH;
end





--Procedimientos almacenados para el manejo de usuarios

go

create procedure agregarUsuario
@cedula varchar(10),
@nombre varchar (20),
@apellido1 varchar (20),
@apellido2 varchar (20),
@nombre_usuario varchar(20),
@clave varchar(30),
@rol smallint,
@departamento smallint,
@patron varchar(20)
as
IF EXISTS (select cedula from usuario where cedula = @cedula)
	BEGIN
		return -1;
	END
ELSE
begin
BEGIN TRY
	insert into usuario(cedula, nombre, apellido1,apellido2, nombre_usuario, clave, departamento, rol) 
	values(@cedula, @nombre, @apellido1, @apellido2, @nombre_usuario, ENCRYPTBYPASSPHRASE (@patron, @clave), @departamento, @rol)
	RETURN 0
END TRY
BEGIN CATCH 
	RETURN -1
END CATCH;
end



go

create procedure validarUsuario
@usario varchar(20),
@clave varchar(30),
@patron varchar(20)
as
BEGIN TRY
	select * from usuario where nombre_usuario = @usario and CONVERT(varchar(30), DECRYPTBYPASSPHRASE(@patron, clave))= @clave
	return 0
END TRY
BEGIN CATCH
	return -1
END CATCH;

--Procedimientos almacenados para agregar clientes

go

create procedure agregarCliente
@nombre_cuenta varchar(10),
@celular varchar (20),
@telefono varchar (20),
@correo varchar (20),
@sitio varchar(20),
@contactoP varchar(30),
@asesor varchar(10),
@zona smallint,
@sector smallint,
@moneda smallint
as
IF EXISTS (select nombre_cuenta from cliente where nombre_cuenta = @nombre_cuenta)
	BEGIN
		return -1;
	END
ELSE
begin try
	insert into cliente(nombre_cuenta, celular, telefono,correo, sitio, contacto_principal, asesor, IDzona,IDsector, IDmoneda) 
	values(@nombre_cuenta, @celular, @telefono,@correo, @sitio, @contactoP, @asesor, @zona,@sector,@moneda ) 
	return 0 
end try
begin catch
	return -1
end catch



--Procedimientos almacenados para  contactos 


go

create procedure agregarContacto
@idContacto smallint,
@nombre varchar (20),
@motivo varchar (50),
@telefono varchar (8),
@correo varchar(25),
@direccion varchar(50),
@descripcion varchar(50),
@cliente varchar(10),
@zona smallint,
@sector smallint,
@asesor varchar(10),
@tipoContacto smallint,
@estado smallint

as
IF EXISTS (select nombre from contacto where nombre = @nombre)
	BEGIN
		return -1;
	END
ELSE
begin try
insert into contacto
	values	(@idContacto, @nombre, @motivo, @telefono, @correo, @direccion, @descripcion,@cliente, @zona,@sector, @asesor, @tipoContacto, @estado)
	return 0
end try
begin catch 
	return -1
end catch


GO
create procedure eliminarCliente
@Id varchar(10)
as
BEGIN TRY
	delete from cliente where nombre_cuenta = @Id
	return 0
END TRY
BEGIN CATCH
	return -1
END CATCH;


--Storaged procedures para catalogos 

go

create procedure agregarTipoContacto
@id smallint,
@tipo varchar (20)
as
IF EXISTS (select id from tipoContacto where id = @id)
	BEGIN
		return -1;
	END
ELSE
begin try
	insert into tipoContacto
	values(@id, @tipo)
	return 0
end try
begin catch
	return -1
end catch;



go

create procedure agregarEstado
@id smallint,
@estado varchar (20)
as
IF EXISTS (select id from estado where id = @id)
	BEGIN
		return -1;
	END
ELSE
begin try
	insert into estado
	values(@id, @estado)
	return 0
end try
begin catch
	return -1
end catch



go

create procedure agregarTarea

@idContacto smallint,
@id smallint,
@estado varchar (20),
@fechaFinalizacion date,
@informacion varchar(15),
@fechaCreacion date,
@asesor varchar(10)
as
IF EXISTS (select id from tarea where id = @idContacto)
	BEGIN
		return -1;
	END
ELSE
begin try
insert into tarea
	values (@id, @fechaFinalizacion, @fechaCreacion, @informacion,@asesor, @estado)
	INSERT INTO tareaXcontacto VALUES (@idContacto, @id)
	return 0
end try
begin catch
	return -1
end catch



go

create procedure agregarActividad
@idContacto smallint,
@id smallint,
@descripcion varchar (25),
@fechaIni date,
@fechaFin date,
@asesor varchar(10)
as
IF EXISTS (select id from actividad where id = @id)
	BEGIN
		return -1;
	END
ELSE
begin try
	insert into actividad
	values (@id, @descripcion, @fechaIni, @fechaFin,@asesor)
	
    INSERT INTO actividadesXcontacto VALUES (@idContacto, @id)
    return 0
end try
begin catch
	return -1
end catch;


go

create procedure agregarCxA
@contacto smallint,
@actividad smallint
as
begin try
	insert into actividadesXcontacto
	values(@contacto, @actividad)
	return 0;
end try
begin catch
	return -1
end catch;


go

create procedure agregarCxT
@contacto smallint,
@tarea smallint
as
begin try
insert into tareaXcontacto
values
(@contacto, @tarea)
return 0
end try
begin CATCH
return -1
end CATCH



--Stored Procedures para cotizaciones

go

create procedure agregarCotizacion
@numeroCot varchar(10),
@nombreOpor varchar (10),
@fechaCot date,
@fechaCierre date,
@ordenCompra varchar(10),
@descripcion varchar(50),
@factura varchar(20),

@zona smallint,
@sector smallint,
@moneda smallint,
@contactoAsociado smallint,
@asesor varchar(10),
@nombreCuenta varchar(10),
@etapa smallint,
@probabilidad smallint,
@tipo smallint,
@razon varchar(20),
@contraQuien varchar(20)

as
begin try
	insert into cotizaciones
	values
	(@numeroCot, @nombreOpor, @fechaCot, @fechaCierre, @ordenCompra, @descripcion, @factura, @zona, @sector, 
	@moneda, @contactoAsociado, @asesor,  @nombreCuenta, @etapa, @probabilidad, @tipo, @razon, @contraQuien)
	return 0
end try
begin catch
	return -1;
end catch;


go

create procedure validarContacto
@contacto smallint

as
begin try
	select * from contacto where idContacto = @contacto
end try
begin catch
	return -1
end catch;



go

create procedure editarCotizacion
    @numeroCot varchar(10),
	@nombreOportunidad varchar(10), 
	@fechaCotizacion date,
	@fechaCierre date, 
	@ordenCompra varchar(10),
	@factura varchar(20),
	@descripcion varchar(50),
	@moneda smallint,
	@etapa smallint,
	@probabilidad smallint,
	@tipo smallint,
	@razon varchar(20),
	@contraQuien varchar(20)


as
begin try
	UPDATE cotizaciones
	SET nombreOportunidad = @nombreOportunidad, fechaCotizacion = @fechaCotizacion, fechaCierra = @fechaCierre,
		ordenCompra = @ordenCompra, factur = @factura, moneda = @moneda, etapa = @etapa, probabilidad = @probabilidad,
		tipo = @tipo, razonDenegacion = @razon, contraQuien = @contraQuien, descripcion = @descripcion

	Where @numeroCot = numeroCotizacion;
	return 0;
end try
begin catch
	return -1;
end catch


select * from contacto

go

create procedure agregarProductos
@codigo varchar(10),
@numeroCot varchar(10),
@cantidad smallint,
@precioNegociado decimal(9,2)

as
begin try
	insert into productosXcotizacion
	values(@codigo, @numeroCot, @cantidad, @precioNegociado)
    return 0
end try
begin catch
	return -1
end catch;

GO

create procedure editarTarea
	@id  smallint, 
	@fechafin date, 
	@asesor varchar(10),
	@estado smallint
as
begin TRY
UPDATE tarea
SET fechaFinalizacion =  @fechafin, asesor = @asesor, estado = @estado
	Where @id = id;
    return 0
end TRY
begin CATCH
return -1
end CATCH


go
create procedure editarActividad
	@id  smallint, 
	@fechafin date, 
	@asesor varchar(10)
as
begin try
	UPDATE actividad
    SET fechaFin =  @fechafin, asesor = @asesor
	Where @id = id;
    return 0
end try
begin catch
	return -1
end catch;


go
create procedure obtenerProducto
as
begin
		select p.codigo, p.nombre, p.descripcion, precio, activo  , f.nombre as nombreF
	from producto p
	join familia_producto f
	on p.codigo_familia = f.codigo
end



go
create procedure obtenerClientes
as
begin
select nombre_cuenta as nombre, celular, telefono, correo, sitio, contacto_principal,
usuario.nombre +' '+ usuario.apellido1 as asesor , moneda.NombreMoneda, zona, sector
from cliente
join usuario on cliente.asesor = usuario.cedula
join zona   on cliente.IDzona = zona.id
join sector on cliente.IDsector = sector.id
join moneda on cliente.IDmoneda = moneda.id
end




go
create procedure obtenerCot
@id varchar(10)
as
begin
SELECT numeroCotizacion, nombreOportunidad, fechaCotizacion, fechaCierra, ordenCompra, co.descripcion, factur, Z.zona,s.sector, M.NombreMoneda,contacto.nombre, usuario.nombre + ' '+usuario.apellido1 
asesor, cliente.nombre_cuenta cliente, E.etapa, probabilidad.etapa proba, T.tipo, cotizacionDenegada.razon, rivales.nombre  as rival
FROM cotizaciones co
JOIN zona Z on co.zona = Z.id
JOIN moneda M ON co.moneda = M.id
JOIN etapa E on co.etapa = E.id
JOIN tipoCotizacion T on co.tipo = T.id
join sector s ON co.sector = s.id
JOIN contacto on co.contactoAsociado = contacto.idContacto
join usuario on co.asesor = usuario.cedula
join cliente on co.nombreCuenta = cliente.nombre_cuenta
joiN probabilidad on co.probabilidad = probabilidad.id
JOiN cotizacionDenegada on co.razonDenegacion = cotizacionDenegada.id
JOin rivales on co.contraQuien = rivales.id
where numeroCotizacion = @id;
END 







go
create procedure obtenerContacto
@id smallint
as
begin
select  c.idContacto, c.nombre, c.motivo, c.telefono, c.correo, direccion , descripcion, ce.nombre_cuenta,z.zona, s.sector, u.nombre + ' '+u.apellido1 as asesor, t.tipo, e.estado
from contacto c
join cliente ce on c.cliente = ce.nombre_cuenta
join zona z on c.zona = z.id
join sector s on c.zona = s.id
jOiN usuario u on c.asesor = u.cedula
JOIN tipoContacto t on c.tipoContacto = T.id
join estado e on c.estado = e.id
where idContacto = @id;
end
go

create procedure obtenerContactoCliente
@id varchar(10)
as
begin
select  c.idContacto, c.nombre, c.motivo, c.telefono, c.correo, direccion , descripcion, ce.nombre_cuenta,z.zona, s.sector, u.nombre + ' '+u.apellido1 as asesor, t.tipo, e.estado
from contacto c
join cliente ce on c.cliente = ce.nombre_cuenta
join zona z on c.zona = z.id
join sector s on c.zona = s.id
jOiN usuario u on c.asesor = u.cedula
JOIN tipoContacto t on c.tipoContacto = T.id
join estado e on c.estado = e.id
where cliente = @id;
end
go
create procedure obtenerContactos

as
begin
select  c.idContacto, c.nombre, c.motivo, c.telefono, c.correo, direccion , descripcion, ce.nombre_cuenta,z.zona, s.sector, u.nombre + ' '+u.apellido1 as asesor, t.tipo, e.estado
from contacto c
join cliente ce on c.cliente = ce.nombre_cuenta
join zona z on c.zona = z.id
join sector s on c.zona = s.id
jOiN usuario u on c.asesor = u.cedula
JOIN tipoContacto t on c.tipoContacto = T.id
join estado e on c.estado = e.id

end


go

create procedure calcularTVP
	
	@idCotizacion SMALLINT,
	@contacto SMALLINT,
	@oportunidad varchar(15),
	@fechaCotizacion smallint,
	@cuenta varchar(20),
	@totalCotizacion decimal(10,2), 
	@tvp decimal(10,2)
	
AS
begin TRY
	
	SET @totalCotizacion = (SELECT SUM( precioNegociado * cantidad ) FROM productosXcotizacion
							WHERE numero_cotizacion = @idCotizacion)
	SET @tvp = @totalCotizacion -- en un principio son lo mismo
	-- tvp inicial es el total de cotizaciones
	-- los calculos se hacen para obtener la inflacion del siguiente año

	DECLARE @valorInflacion AS FLOAT, @fecha AS SMALLINT

	DECLARE inflacionCursor CURSOR FOR SELECT id, porcentaje FROM inflacion
	OPEN inflacionCursor
	FETCH NEXT FROM inflacionCursor INTO @fecha, @valorInflacion
	WHILE @@fetch_status = 0
	BEGIN

		if(@fechaCotizacion >= @fecha) 
			FETCH NEXT FROM inflacionCursor INTO @valorInflacion
		else
			begin
			-- total cotizacion = suma de productos (precio negociado)
			SET @totalCotizacion = (SELECT sum( precioNegociado * cantidad ) 
			from productosXcotizacion
			where numero_cotizacion = @idCotizacion)

			set @tvp = (@tvp + (@tvp*@valorInflacion/100)) -- calculo de la inflacion al siguiente año
		
			FETCH NEXT FROM inflacionCursor INTO @valorInflacion
			end
	END
	CLOSE inflacionCursor
	DEALLOCATE inflacionCursor

	INSERT INTO ValorPresenteCotizaciones 
	VALUES(@idCotizacion, @contacto, @oportunidad, @fechaCotizacion, @cuenta, @totalCotizacion, @tvp)
	
	return 0
END TRY
BEGIN CATCH
	return -1
END CATCH

---------------------------------------------------------Seccion de inserts--------------------------------------------------------------------------------------------
INSERT into departamento   values (1, 'IT')
INSERT INTO ROL(id, tipoRol) VALUES (1, 'Edicion')
INSERT INTO ROL(id, tipoRol) VALUES (2, 'Visualizacion')
INSERT INTO ROL(id, tipoRol) VALUES (3, 'Reporteria')


EXEC agregarUsuario '118470507','Adjany','Gard','Alpizar','adjany08','1234',1,1,'adjany'

EXEC agregarUsuario '118470508','Kevin','Salazar','Valle','kevin','1234',2,1,'adjany'

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


sELECt * FROM cotizaciones

insert into inflacion values (2020, 4)
insert into inflacion values (2021, 3)
insert into inflacion values (2022, 12)