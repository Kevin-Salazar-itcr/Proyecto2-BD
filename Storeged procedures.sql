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
@asesor smallint,
@zona smallint,
@moneda varchar(20)
as
begin
insert into cliente(nombre_cuenta, celular, telefono,correo, sitio, contacto_principal, asesor, IDzona, IDmoneda) 
values
(@nombre_cuenta, @celular, @telefono,@correo, @sitio, @contactoP, @asesor, @zona,@moneda ) 
end