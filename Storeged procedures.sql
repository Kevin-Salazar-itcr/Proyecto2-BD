use CRM

--Query creado para los storaged procedures de las diferentes tablas 



--Procedimientos almacenados para manejar las familias de productos

go
create procedure agregarFamilia
@codigo varchar(10),
@nombre varchar (20),
@descripcion varchar (50)
as
begin
insert into familia_producto(codigo, nombre, descripcion) 
values
(@codigo, @nombre, @descripcion)
end

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
begin
insert into producto(codigo, nombre, descripcion,precio,activo,codigo_familia ) 
values
(@codigo, @nombre, @descripcion,@precio,@activo,@codigo_familia)
end




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
UPDATE producto
SET codigo = @codigo, nombre = @nombre ,descripcion = @descripcion, precio = @precio, activo = @activo, codigo_familia = @familiaProducto
	Where @codigo = codigo;
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
begin
insert into usuario(cedula, nombre, apellido1,apellido2, nombre_usuario, clave, departamento, rol) 
values
(@cedula, @nombre, @apellido1, @apellido2, @nombre_usuario, ENCRYPTBYPASSPHRASE (@patron, @clave), @departamento, @rol)
end

go
create procedure validarUsuario
@usario varchar(20),
@clave varchar(30),
@patron varchar(20)
as
begin
select * from usuario where nombre_usuario = @usario and CONVERT(varchar(30), DECRYPTBYPASSPHRASE(@patron, clave))= @clave
end

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
begin
insert into cliente(nombre_cuenta, celular, telefono,correo, sitio, contacto_principal, asesor, IDzona,IDsector, IDmoneda) 
values
(@nombre_cuenta, @celular, @telefono,@correo, @sitio, @contactoP, @asesor, @zona,@sector,@moneda ) 
end



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
begin
insert into contacto
values
(@idContacto, @nombre, @motivo, @telefono, @correo, @direccion, @descripcion,@cliente, @zona,@sector, @asesor, @tipoContacto, @estado)
end


--Storaged procedures para catalogos 

go
create procedure agregarTipoContacto
@id smallint,
@tipo varchar (20)
as
begin
insert into tipoContacto
values
(@id, @tipo)
end





go
create procedure agregarEstado
@id smallint,
@estado varchar (20)
as
begin
insert into estado
values
(@id, @estado)
end


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
begin
insert into tarea
values 
(@id, @fechaFinalizacion, @fechaCreacion, @informacion,@asesor, @estado)
INSERT INTO tareaXcontacto VALUES (@idContacto, @id)
end



go
create procedure agregarActividad

@idContacto smallint,
@id smallint,
@descripcion varchar (25),
@fechaIni date,
@fechaFin date,
@asesor varchar(10)
as
begin
insert into actividad
values
(@id, @descripcion, @fechaIni, @fechaFin,@asesor)
INSERT INTO actividadesXcontacto VALUES (@idContacto, @id)
end

go
create procedure agregarCxA
@contacto smallint,
@actividad smallint
as
begin
insert into actividadesXcontacto
values
(@contacto, @actividad)
end


go
create procedure agregarCxT
@contacto smallint,
@tarea smallint
as
begin
insert into tareaXcontacto
values
(@contacto, @tarea)
end



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
begin
insert into cotizaciones
values
(@numeroCot, @nombreOpor, @fechaCot, @fechaCierre, @ordenCompra, @descripcion, @factura, @zona, @sector, 
@moneda, @contactoAsociado, @asesor,  @nombreCuenta, @etapa, @probabilidad, @tipo, @razon, @contraQuien)
end


go
create procedure validarContacto
@contacto smallint
as
begin
select * from contacto where idContacto = @contacto
end



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
begin
UPDATE cotizaciones
SET nombreOportunidad = @nombreOportunidad, fechaCotizacion = @fechaCotizacion, fechaCierra = @fechaCierre,
	ordenCompra = @ordenCompra, factur = @factura, moneda = @moneda, etapa = @etapa, probabilidad = @probabilidad,
	tipo = @tipo, razonDenegacion = @razon, contraQuien = @contraQuien, descripcion = @descripcion

	Where @numeroCot = numeroCotizacion;
end



select * from contacto



create procedure agregarProductos
@codigo varchar(10),
@numeroCot varchar(10),
@cantidad smallint,
@precioNegociado decimal(9,2)
as
begin

insert into productosXcotizacion
values(@codigo, @numeroCot, @cantidad, @precioNegociado)
end



go
create procedure editarTarea
	@id  smallint, 
	@fechafin date, 
	@asesor varchar(10),
	@estado smallint
as
begin
UPDATE tarea
SET fechaFinalizacion =  @fechafin, asesor = @asesor, estado = @estado
	Where @id = id;
end


go
create procedure editarActividad
	@id  smallint, 
	@fechafin date, 
	@asesor varchar(10)
as
begin
UPDATE actividad
SET fechaFin =  @fechafin, asesor = @asesor
	Where @id = id;
end