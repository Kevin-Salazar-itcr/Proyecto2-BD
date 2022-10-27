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
@moneda smallint
as
begin
insert into cliente(nombre_cuenta, celular, telefono,correo, sitio, contacto_principal, asesor, IDzona, IDmoneda) 
values
(@nombre_cuenta, @celular, @telefono,@correo, @sitio, @contactoP, @asesor, @zona,@moneda ) 
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
@asesor varchar(10),
@tipoContacto smallint,
@estado smallint

as
begin
insert into contacto
values
(@idContacto, @nombre, @motivo, @telefono, @correo, @direccion, @descripcion,@cliente, @zona, @asesor, @tipoContacto, @estado)
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
create procedure agregarzona
@id smallint,
@zona varchar (20),
@sector varchar (20)
as
begin
insert into zonaSector
values
(@id, @zona, @sector)
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
@id smallint,
@estado varchar (20),
@fechaFinalizacion date,
@informacion varchar(15),
@fechaCreacion date
as
begin
insert into tarea
values
(@id, @estado, @fechaFinalizacion, @informacion,  @fechaCreacion)
end

go
create procedure agregarActividad
@id smallint,
@descripcion varchar (25),
@fechaIni date,
@fechaFin date
as
begin
insert into actividad
values
(@id, @descripcion, @fechaIni, @fechaFin)
end

create procedure agregarCxA
@contacto smallint,
@actividad smallint
as
begin
insert into actividadesXcontacto
values
(@contacto, @actividad)
end

create procedure agregarCxT
@contacto smallint,
@tarea smallint
as
begin
insert into tareaXcontacto
values
(@contacto, @tarea)
end

