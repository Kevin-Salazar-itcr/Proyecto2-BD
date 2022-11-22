use CRM
go

--Todo lo necesario para la consulta #1
--------------------------------------------------------------------------
--Funcion para retornar la venta total por cada familia de productos 


create function VentaFamilia(@codigo VARCHAR(20), @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join producto on productosXcotizacion.codigo_producto = producto.codigo
	join familia_producto on producto.codigo_familia = familia_producto.codigo
	where @codigo = familia_producto.codigo and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go


--Funcion que retorna la familia y el monto vendido

create function TotalVentaFamilia(@fechaini date, @fechafin date)
returns table
as
RETURN
(

	select top 100 nombre, dbo.VentaFamilia(codigo,@fechaini, @fechafin ) as Monto
	from familia_producto
	where  dbo.VentaFamilia(codigo,@fechaini, @fechafin )>0
	order by  dbo.VentaFamilia(codigo,@fechaini, @fechafin ) desc

)
go

create view VistaVentaFamilia
AS
    SELECT familia_producto.nombre, sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) as Venta
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join producto on productosXcotizacion.codigo_producto = producto.codigo
	join familia_producto on producto.codigo_familia = familia_producto.codigo
	where cotizaciones.probabilidad = 4
	group by  familia_producto.nombre
go

--------------------------------------------------------------------------

--Todo lo necesario para la consulta #2

--Funcion que retorna la cantidad de veces que un producto fue vendido

create function CantVecesProductoVendido(@consecutivo VARCHAR(20))
returns int
as
begin
	declare @result int;
	select @result = count(codigo_producto) 
	from cotizaciones 
	JOIN productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @consecutivo = codigo_producto and probabilidad = 4
	return @result
end
go

--funcion que retorna el monto total de la venta en un rango
create function VentaProducto(@codigo VARCHAR(20), @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @codigo = productosXcotizacion.codigo_producto and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la familia y el monto vendido

CREATE function TotalVentaProducto(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select TOP 10 nombre, dbo.VentaProducto(codigo,@fechaini, @fechafin ) as Monto, dbo.CantVecesProductoVendido(codigo) as Veces
	from producto
	where  dbo.VentaProducto(codigo,@fechaini, @fechafin )>0
	order bY  dbo.CantVecesProductoVendido(codigo) desc
)
GO

Create VIEW TopProductosVendidos
AS
	select TOP 10 nombre, 
	sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad)  AS monto
	from cotizaciones 
	JOIN productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join producto on productosXcotizacion.codigo_producto = producto.codigo
	where probabilidad = 4
	group by nombre
	order by  count(codigo_producto) desc

--------------------------------------------------------------------------

--Todo lo necesario para la consulta #3

--Funcion para sacar la cantidad de veces que un producto fue cotizado
go
create function CantVecesProductoCotizado(@consecutivo VARCHAR(20))
returns int
as
begin
	declare @result int;
	select @result = count(codigo_producto) from productosXcotizacion where @consecutivo = codigo_producto
	return @result
end
go

--funcion que retorna la cantidad de productos cotizados
Create function CotProducto(@codigo VARCHAR(20), @fechaini date, @fechafin date)
returns INt
as
begin
	declare @result int
	select @result = sum(productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @codigo = productosXcotizacion.codigo_producto and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que acomoda las cotizaciones por cantidad de estas
CREATE function TotalCotizacionProducto(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select TOP 10 nombre, dbo.CotProducto(codigo,@fechaini, @fechafin ) as Cantidad
	from producto
	where  dbo.VentaProducto(codigo,@fechaini, @fechafin )>0
	order bY  dbo.CotProducto(codigo,@fechaini, @fechafin ) desc
)
GO

go
Create VIEW TopProductosCotizados
AS
	select TOP 10 nombre, 
	sum(productosXcotizacion.cantidad)  AS Cantidad
	from cotizaciones 
	JOIN productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join producto on productosXcotizacion.codigo_producto = producto.codigo
	group by nombre
	order by  count(codigo_producto) desc


--------------------------------------------------------------------------
--Todo lo necesario para la consulta #4

--Funcion que calcula el total de ventas por sector en un rango de fecha dado
go
create function VentSector(@id smallint, @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @id = cotizaciones.sector and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaSector(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select sector, dbo.VentSector(id,@fechaini, @fechafin ) as Total
	from sector
	where  dbo.VentSector(id,@fechaini, @fechafin )>0
)
GO


CREATE VIEW VistaVentaSector
AS

	select  sector.sector,  sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) as venta
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join sector ON cotizaciones.sector = sector.id
	where cotizaciones.probabilidad = 4 
	Group by sector.sector
go




--------------------------------------------------------------------------
--Todo lo necesario para la consulta #5

--Funcion que calcula el total de ventas por zona en un rango de fecha dado

create function VentaZona(@id smallint, @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @id = cotizaciones.zona and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaZona(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select zona, dbo.VentaZona(id,@fechaini, @fechafin ) as Total
	from zona
	where  dbo.VentaZona(id,@fechaini, @fechafin )>0
)
GO


CREATE VIEW VistaVentaZona
AS
	select  zona.zona,  sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) as venta
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join zona ON cotizaciones.sector = zona.id
	where cotizaciones.probabilidad = 4 
	Group by zona.zona
GO


--------------------------------------------------------------------------
--Todo lo necesario para la consulta #6
--Cotizaciones y ventas x depto (comparativo)
go

-- @id = id del departamento
create function ventasXdepto(@id smallint)
returns table
as
RETURN
(
	select (select usuario.departamento from usuario where usuario.cedula = c.asesor) as departamento,c.numeroCotizacion as Venta, pc.precioNegociado* pc.cantidad as Total, pc.cantidad as productosVendidos, c.asesor, c.fechaCotizacion as fechaCotizacion 
	from cotizaciones c, productosXcotizacion pc
	where c.numeroCotizacion = pc.numero_cotizacion and (select usuario.departamento from usuario where usuario.cedula = c.asesor) = @id
)
go


create view VistaVentasCotXdepto
as
	select departamento.nombre, numeroCotizacion, sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) AS venta
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join usuario on cotizaciones.asesor = usuario.cedula
	join departamento on usuario.departamento = departamento.id
	where cotizaciones.probabilidad = 4 
	group by departamento.nombre, numeroCotizacion
go
--------------------------------------------------------------------------
--Todo lo necesario para la consulta #7

--Funcion que calcula el total de ventas por departamento en un rango de fecha dado
go
create function VentaDepartamento(@id smallint, @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join usuario on cotizaciones.asesor = usuario.cedula
	where @id = usuario.departamento and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaDepartamento(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select nombre, dbo.VentaDepartamento(id,@fechaini, @fechafin ) as Total
	from departamento
	where  dbo.VentaDepartamento(id,@fechaini, @fechafin )>0
)
GO

Create view VistaVentasDepartamento
as
	select departamento.nombre,  sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) AS venta
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join usuario on cotizaciones.asesor = usuario.cedula
	join departamento on usuario.departamento = departamento.id
	where cotizaciones.probabilidad = 4 
	group by departamento.nombre
GO

--------------------------------------------------------------------------
--Todo lo necesario para la consulta #8
--Ventas y cotizaciones por mes, año (barras)
create function cotizacionesXfecha(@mes int, @anio int)
returns table 
as
RETURN(
	select c.numeroCotizacion as Venta, pc.precioNegociado* pc.cantidad as Total, pc.cantidad as productosVendidos, c.asesor, c.fechaCotizacion as fechaCotizacion from cotizaciones c, productosXcotizacion pc
	where MONTH(c.fechaCotizacion) = @mes and YEAR(c.fechaCotizacion) = @anio and c.numeroCotizacion = pc.numero_cotizacion
	
)
GO


----------------------------------------------------------------------
--Todo lo necesario para la consulta #9
--ventas y cotizaciones por mes, por año, en valor presente

--drop function consultaTVP
GO
create function consultaTVP(@mes int, @anio int)
returns table
as
RETURN(
	select c.numeroCotizacion as cotizacion, tvp.nombreOportunidad as oportunidad, tvp.nombreCuenta as cuentaAsociada, tvp.totalCotizacion, tvp.totalValorPresente 
	from ValorPresenteCotizaciones tvp, cotizaciones c
	where MONTH(c.fechaCotizacion) = @mes 
	and YEAR(c.fechaCotizacion) = @anio 
	and c.numeroCotizacion = tvp.idCotizacion
)
go

create view VentaCotizacionesTVP
as
	select  vpc.idCotizacion, vpc.nombreOportunidad, vpc.totalCotizacion
	from ValorPresenteCotizaciones vpc
	order by vpc.totalCotizacion
go
----------------------------------------------------------------------

--Todo lo necesario para la consulta #10

create function VentaCliente(@cliente VARCHAR(20), @fechaini date, @fechafin date )
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join cliente on cotizaciones.nombreCuenta = cliente.nombre_cuenta
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @cliente = cotizaciones.nombreCuenta and cotizaciones.probabilidad = 4 and fechaCotizacion  between  @fechaini  and @fechafin  
	return @result
end
go


--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaClientes(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 nombre_cuenta, dbo.VentaCliente(nombre_cuenta,@fechaini, @fechafin ) as Total
	from cliente
	where  dbo.VentaCliente(nombre_cuenta,@fechaini, @fechafin )>0
	order by dbo.VentaCliente(nombre_cuenta,@fechaini, @fechafin )
)
GO


go
CREATE view topVentasClientes
as
select top 10 cliente.nombre_cuenta , dbo.totalDeLaVenta(nombre_cuenta) as [Venta]
from cliente
order by [Venta] DESC
go



----------------------------------------------------------------------
--Todo lo necesario para la consulta #11



Create VIEW ContactosXusuario
AS
select usuario.nombre, count(contacto.asesor) as contactos
from usuario
Join contacto on usuario.cedula = contacto.asesor
group by  usuario.nombre
go



----------------------------------------------------------------------
--Todo lo necesario para la consulta #12

create function VentaVendedor(@asesor VARCHAR(20), @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @asesor = asesor and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

CREATE function TotalVentaAsesores(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 nombre+' '+apellido1+' '+apellido2 AS Vendedor, dbo.VentaVendedor(cedula,@fechaini, @fechafin ) as Total
	from usuario
	where  dbo.VentaVendedor(cedula,@fechaini, @fechafin )>0
	order by dbo.VentaVendedor(cedula,@fechaini, @fechafin )
)
GO

CREATE view topVentasVendedor
as
select top 10 nombre , sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) as [Venta]
from cotizaciones
join usuario on cotizaciones.asesor = usuario.cedula
join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
where cotizaciones.probabilidad = 4
group by nombre
order by [Venta] DESC
go


----------------------------------------------------------------------
--Todo lo necesario para la consulta #13
-- Cantidad de ejecuciones con cierre por mes, por a�o
-- (ejecuciones que cierran en una fecha determinada)

CREATE function cierreEjecucionesXmesAnio (@mes int, @anio int)
returns table
as
RETURN
(
	select YEAR(ejecucion.fecha_cierra) as anio, MONTH(ejecucion.fecha_cierra) as mes, COUNT(*) as ejecuciones 
	from ejecucion
	where MONTH(ejecucion.fecha_cierra) = @anio and YEAR(ejecucion.fecha_cierra) = @mes
	group by fecha_cierra
)
GO
create view consultaCierreEjecuciones
as
	select e.IDejecucion, e.nombre,e.numero_cotizacion
	from ejecucion e
	
	where (SELECT CONVERT (date, GETDATE())) <= e.fecha_cierra
	group by e.IDejecucion, e.nombre, e.numero_cotizacion
	order by e.IDejecucion desc
go


----------------------------------------------------------------------
--Todo lo necesario para la consulta #14

GO
CREATE function casosEstado(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select estadoCaso.estado, COUNT(casos.estado) as casos
	from estadoCaso
	join casos on estadoCaso.id = casos.estado
	where fechaCreacion between @fechaini and @fechafin
	group by estadoCaso.estado
)
GO

create view vistaCasosEstado
as
	select estadoCaso.estado, COUNT(casos.estado) as casos
	from estadoCaso
	join casos on estadoCaso.id = casos.estado
	group by estadoCaso.estado
go

----------------------------------------------------------------------
--Todo lo necesario para la consulta #15

create function totalActividades(@cot VARCHAR(20), @fechaini date,@fechafin date)
returns INT 
as
begin
	declare @result int;
	select @result = count(numero_cotizacion) 
	from actividadXcotizacion 
	JOIN cotizaciones ON actividadXcotizacion.numero_cotizacion = cotizaciones.numeroCotizacion
	where @cot = numero_cotizacion AND fechaCotizacion BETWEEN @fechaini and @fechafin
	return @result
end
go

--  Funcion que devuelve el total de tareas por cotiZacion 
create function totalTareas(@cot VARCHAR(20),  @fechaini date,@fechafin date)
returns INT 
as
begin
	declare @result int;
	select @result = count(numero_cotizacion) 
	from tareaXcotizacion 
	JOIN cotizaciones ON tareaXcotizacion.numero_cotizacion = cotizaciones.numeroCotizacion
	where @cot = numero_cotizacion AND fechaCotizacion BETWEEN @fechaini and @fechafin
	return @result
end
go

CREATE function CotizacionesTotalTareas(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 numeroCotizacion, nombreOportunidad, 
	dbo.totalActividades(numeroCotizacion, @fechaini,@fechafin) 
	+ dbo.totalTareas(numeroCotizacion,  @fechaini, @fechafin) as [Total tareas y actividades]
	from cotizaciones
	order by[Total tareas y actividades] DESC

)
GO


create view VistaTareasActividadesCot
as

select top 10 numeroCotizacion, COUNT(tareaXcotizacion.numero_cotizacion) + count(actividadXcotizacion.numero_cotizacion) as Total
from cotizaciones
join tareaXcotizacion ON cotizaciones.numeroCotizacion = tareaXcotizacion.numero_cotizacion
JOIN actividadXcotizacion ON cotizaciones.numeroCotizacion = actividadXcotizacion.numero_cotizacion
group by numeroCotizacion
order by Total desc 




--Todo lo necesario para la consulta #16

--Funcion para obtener el monto de ventas por zona
GO
create function montoXzona(@zona smallint)
returns table
as
RETURN
(
	select numeroCotizacion, SUM(precioNegociado * cantidad) as monto
	from cotizaciones, productosXcotizacion
	where zona = @zona and numeroCotizacion = numero_cotizacion
	group by numeroCotizacion
)
go

create view clientesXzona
as
select z.zona, count(c.IDzona) as cantidad, (select SUM(monto) from dbo.montoXzona(z.id)) as monto from zona z, cliente c
where z.id = c.IDzona
group by z.zona, c.IDzona, z.id
go

select * from clientesXzona


----------------------------------------------------------------------

--Todo lo necesario para la consulta #17
GO
create function CantCotTipo(@id smallint, @fechaini date, @fechafin date)
returns int
as
begin
	declare @result int;
	select @result = count(tipo) 
	from  cotizaciones
	where @id = tipo and fechaCotizacion between @fechaini and @fechafin
	return @result 
end
go

CREATE function cotPorTipo(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	SELECT tipo, dbo.CantCotTipo(id, @fechaini, @fechafin) as [cantidad]
	FROM tipoCotizacion
)
GO


create view VistasCotTipo
as
select  tipoCotizacion.tipo, count(cotizaciones.tipo) as total
from tipoCotizacion
join cotizaciones on tipoCotizacion.id = cotizaciones.tipo
group by tipoCotizacion.tipo
go

----------------------------------------------------------------------
--Todo lo necesario para la consulta #18

--Funcion que devuelve top 10 de tareas con diferencia de dias
CREATE function diferenciaDias(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 numeroCotizacion, nombreOportunidad, nombreCuenta, DATEDIFF(DAY, fechaCotizacion, fechaCierra) as [Días de diferencía]
	from cotizaciones 
	join cliente on cotizaciones.nombreCuenta = cliente.nombre_cuenta
	where fechaCotizacion between @fechaini and @fechafin
	order by [Días de diferencía] DESC
)
GO

create view DiferenciaDiasCot
as
select top 10 numeroCotizacion, nombreOportunidad, nombreCuenta, DATEDIFF(DAY, fechaCotizacion, fechaCierra) as [Días de diferencía]
from cotizaciones 
join cliente on cotizaciones.nombreCuenta = cliente.nombre_cuenta
order by [Días de diferencía] DESC
go



----------------------------------------------------------------------
--Todo lo necesario para la consulta #19

GO
CREATE function casosTipo(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select tipo, COUNT(casos.tipoCaso) as casos
	from tipoCaso
	join casos on tipoCaso.id = casos.tipoCaso
	where fechaCreacion between @fechaini and @fechafin
	group by tipo
)
GO

CREATE VIEW VistaCasosTipo
as
	select tipo, COUNT(casos.tipoCaso) as casos
	from tipoCaso
	join casos on tipoCaso.id = casos.tipoCaso
	group by tipo
GO



----------------------------------------------------------------------
--Todo lo necesario para la consulta #20

--Funcion para las tareas mas antiguas sin cerrar

GO
create function tareasAbiertas(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 15 id, informacion, fechaCreacion
	from tarea 
	where tarea.estado = 1 and fechaCreacion between @fechaini and @fechafin
	order by fechaCreacion 
)
GO



go
Create view tareasSinCerrar
as
select top 15 fechaCreacion, informacion, usuario.nombre + ' '+ usuario.apellido1 + ' ' + usuario.apellido2 AS Nombre
from tarea 
join usuario ON tarea.asesor = usuario.cedula
where tarea.estado = 1
order by fechaCreacion 
go

select * from dbo.tareasAbiertas('2022-10-1', '2022-11-15')


insert into tarea values(1,'2022-11-11','2022-11-11','Hola','118470507', 1);
insert into tarea values(2,'2022-11-11','2022-11-14','Hola','118470507', 1);
insert into tarea values(3,'2022-11-11','2022-11-19','Hola','118470507', 1);
insert into tarea values(4,'2022-11-11','2022-10-1','Hola','118470507', 1);