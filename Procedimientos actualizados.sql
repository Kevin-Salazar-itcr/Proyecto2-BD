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



--Procedimientos almacenados para guardar contactos 


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



create procedure obtenerProducto
as
begin
		select p.codigo, p.nombre, p.descripcion, precio, activo  , f.nombre as nombreF
	from producto p
	join familia_producto f
	on p.codigo_familia = f.codigo
end




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


select nombre_cuenta, celular, telefono, correo, sitio, contacto_principal, usuario.nombre +' '+ usuario.apellido1 as nombre , moneda.NombreMoneda
from cliente
join usuario on cliente.asesor = usuario.cedula
join zona   on cliente.IDzona = zona.id
join sector on cliente.IDsector = sector.id
join moneda on cliente.IDmoneda = moneda.id



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