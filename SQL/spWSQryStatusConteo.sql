

--exec spWSQryStatusConteo '50000034', '1', 'E497', '1937'
alter PROCEDURE spWSQryStatusConteo 
	@prmDocumento varchar(20),
	@prmPartida varchar(5),
	@prmEstacion varchar(15), 
	@prmUsuario varchar(15)
AS
BEGIN

	declare @wpIdReciboCompra bigint

	select @wpIdReciboCompra = R1.IdReciboCompra from regReciboCompra R1 inner join regOrdenCompra R2 on R1.IdOrdenCompra = R2.IdOrdenCompra where R2.OrdenCompra = @prmDocumento

	if @wpIdReciboCompra = null
	begin
		Select 'ER' Estado, 'ER: La orden de compra no existe o no ha sido liberada' Texto
		return 1
	end

	select 'OK' Estado, '' Texto

	declare @wpConteoActual int
	Select @wpConteoActual = Tag3 from regReciboCompraDet where IdReciboCompra = @wpIdReciboCompra and PartidaERP = @prmPartida

	
	if @wpConteoActual = 1
	begin
		Select top 1 R1.Tag3 Intento, R1.CantidadPendienteTotal, R2.conteo1 CantidadContada  from regReciboCompraDet R1
		inner join relReciboCompraDetConteo R2
		on R2.idReciboCompra = R1.IdReciboCompra
		where R1.IdReciboCompra = @wpIdReciboCompra and R1.PartidaERP = @prmPartida
		group by R1.Tag3, R1.CantidadPendienteTotal,  R2.conteo1 order by R2.conteo1 desc

	end

	ELSE if @wpConteoActual= 2
	begin
		Select top 1 R1.Tag3 Intento, R1.CantidadPendienteTotal, R2.conteo2 CantidadContada  from regReciboCompraDet R1
		inner join relReciboCompraDetConteo R2
		on R2.idReciboCompra = R1.IdReciboCompra
		where R1.IdReciboCompra = @wpIdReciboCompra and R1.PartidaERP = @prmPartida
		group by R1.Tag3, R1.CantidadPendienteTotal,  R2.conteo2 order by R2.conteo2 desc

	end

	else if @wpConteoActual = 3
	begin
		Select top 1 R1.Tag3 Intento, R1.CantidadPendienteTotal, R2.conteo3 CantidadContada  from regReciboCompraDet R1
		inner join relReciboCompraDetConteo R2
		on R2.idReciboCompra = R1.IdReciboCompra
		where R1.IdReciboCompra = @wpIdReciboCompra and R1.PartidaERP = @prmPartida
		group by R1.Tag3, R1.CantidadPendienteTotal,  R2.conteo3 order by R2.conteo3 desc

	end
	
	else
	begin
		Select distinct 0 Intento, R1.CantidadPendienteTotal, 0 CantidadContada  from regReciboCompraDet R1
		where R1.IdReciboCompra = @wpIdReciboCompra and R1.PartidaERP = @prmPartida
	end


  
END
GO


