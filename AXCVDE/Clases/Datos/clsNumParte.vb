Imports System.Data

Public Class clsNumParte

#Region "CONSTRUCTORES"

    Sub New()
    End Sub

#End Region

#Region "METODOS"

#Region "Metodos nativos"

    Public Function ListaNumParte(ByVal prmNumParte As String, ByVal prmEstatus As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaNumParte", prmNumParte, prmEstatus, prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Tabla = dt


                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al tratar de consultar números de parte [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CConfigAlmacen.ListaNumParte")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Consultar números de parte [SP1]"

            Return pRespuesta
        End Try
    End Function

    Public Function ListaLoteXNumParte(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaLote", prmNumParte, prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Tabla = dt


                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al tratar de consultar Lotes por número de parte [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsNumParte.ListaLoteXNumParte")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Consultarlotes por número de arte [SP1]"

            Return pRespuesta
        End Try
    End Function
    'Public Function ListaNumParte() As DataSet
    '    Try
    '        Dim dato As New clsDato
    '        Dim ds As New DataSet
    '        ds = dato.Tabla("spQryListaNumParte")
    '        Return ds
    '    Catch ex As Exception
    '        MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
    '    End Try
    'End Function

    'Public Function ListaNumParte(ByVal prmNumParte As String) As DataSet
    '    Try
    '        Dim dato As New clsDato
    '        Dim ds As New DataSet
    '        dato.Parametro("@prmNumParte") = prmNumParte
    '        ds = dato.Tabla("spQryListaNumParte")
    '        Return ds
    '    Catch ex As Exception
    '        MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
    '    End Try
    'End Function

    Public Function CreaNumParte(ByVal prmNumParte As String,
        ByVal prmDescripcionCorta As String,
        ByVal prmPlanta As String,
        ByVal prmCantidadEmpaque As String,
        ByVal prmCantidadPallet As String,
        ByVal prmUnidadMedida As String,
        ByVal prmRutaImagen As String,
        ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Try
            Dim dato As New clsDato

            Dim Estado As String = ""
            Dim Texto As String = ""

            dato.Parametro("@prmNuevoNumParte") = prmNumParte
            dato.Parametro("@prmFamilia") = "FAMILIA"
            dato.Parametro("@prmDescripcion") = prmDescripcionCorta
            dato.Parametro("@prmRevision") = prmRutaImagen
            dato.Parametro("@prmCantidadEmpaque") = prmCantidadEmpaque
            dato.Parametro("@prmCantidadPallet") = prmCantidadPallet
            dato.Parametro("@prmUnidadMedida") = prmUnidadMedida
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario

            dato.SP(True) = "spInsCreaNumParte"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While


            If Estado = "ER" Then
                Return Estado
            End If

            Return Texto

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Function ListaRevisionDesbloqueada(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteSinBloqueoRevision")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaRevisionBloqueada(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteConBloqueoRevision")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function DesbloqueaRevision(ByVal prmNumParte As String, ByVal prmRevision As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim dato As New clsDato
        Dim Estado As String = ""
        Dim Texto As String = ""
        Try
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmRevision") = prmRevision
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            dato.SP(True) = "spInsDesbloqueaRevision"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return Texto
    End Function

    Public Function BloqueaRevision(ByVal prmNumParte As String, ByVal prmRevision As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim dato As New clsDato
        Dim Estado As String = ""
        Dim Texto As String = ""
        Try
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmRevision") = prmRevision
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            dato.SP(True) = "spInsBloqueaRevision"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return Texto
    End Function

    Public Function ListaNumParteInventarios(ByVal prmNumParte$, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteInventarios")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

#End Region

#Region "Metodos Muestreo"

    Public Function ListaNumParteSinMuestreo(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As New DataSet
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteSinMuestreo")
            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function ListaNumParteConMuestreo(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As New DataSet
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteConMuestreo")
            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function AsociarNumParteMuestreo(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Dim dato As New clsDato

        Dim Estado As String
        Dim Texto As String = ""

        dato.Parametro("@prmNumParte") = prmNumParte
        dato.Parametro("@prmEstacion") = prmEstacion
        dato.Parametro("@prmUsuario") = prmUsuario
        dato.SP(True) = "spInsAsociaNumParteMuestreo"

        While dato.DataReader.Read
            Estado = dato.DataReader.Item("Estado")
            Texto = dato.DataReader.Item("Texto")
        End While

        Return Texto
    End Function

    Public Function DesasociarNumParteMuestreo(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Dim dato As New clsDato

        Dim Estado As String
        Dim Texto As String = ""

        dato.Parametro("@prmNumParte") = prmNumParte
        dato.Parametro("@prmEstacion") = prmEstacion
        dato.Parametro("@prmUsuario") = prmUsuario
        dato.SP(True) = "spUpdDesasociaNumParteMuestreo"

        While dato.DataReader.Read
            Estado = dato.DataReader.Item("Estado")
            Texto = dato.DataReader.Item("Texto")
        End While

        Return Texto
    End Function

#End Region

#Region "Metodos proveedor"

    Public Function AgregaEmpaquePallet(ByVal prmNumParte As String, ByVal prmRevision As String, ByVal prmIdProveedor As String, ByVal prmPzaPorEmpaque As Integer, ByVal prmEmpaquePorPallet As Integer,
                                          ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Try
            Dim dato As New clsDato

            Dim Estado As String
            Dim Texto As String = ""

            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmRevision") = prmRevision
            dato.Parametro("@prmIdProveedor") = prmIdProveedor
            dato.Parametro("@prmPzaPorEmpaque") = prmPzaPorEmpaque
            dato.Parametro("@prmEmpaquePorPallet") = prmEmpaquePorPallet
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario

            dato.SP(True) = "spInsAsociaNumParteProveedor"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            Return Texto
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function



#End Region

#Region "Metodos Kanban"

    'Public Function ListaNumParteSinKanban() As DataSet
    '    Try
    '        Dim dato As New clsDato
    '        Dim ds As DataSet

    '        ds = dato.Tabla("spQryListaNumParteSinKanban")

    '        Return ds
    '    Catch ex As Exception
    '        MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
    '    End Try
    'End Function

    Public Function ListaNumParteSinKanban(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As DataSet
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteSinKanban")

            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    'Public Function ListaNumParteConKanban() As DataSet
    '    Try
    '        Dim dato As New clsDato
    '        Dim ds As New DataSet
    '        ds = dato.Tabla("spQryListaNumParteConKanban")
    '        Return ds
    '    Catch ex As Exception
    '        MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
    '    End Try
    'End Function

    Public Function ListaNumParteConKanban(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As DataSet
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteConKanban")

            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function AsociaNumParteKanban(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim Estado As String = ""
        Dim Texto As String = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            dato.SP(True) = "spInsAsociaNumParteKanban"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            Return Texto
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function DesasociaNumParteKanban(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim Estado As String = ""
        Dim Texto As String = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            dato.SP(True) = "spUpdDesasociaNumParteKanban"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return Texto
    End Function

#End Region

#Region "Metodos Maquila"

    Public Function ListaNumParteSinMaquila(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As New DataSet
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteSinMaquila")
            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function ListaNumParteConMaquila(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As New DataSet
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaNumParteConMaquila")
            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function AsociarNumParteMaquila(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Dim dato As New clsDato

        Dim Estado As String
        Dim Texto As String = ""

        dato.Parametro("@prmNumParte") = prmNumParte
        dato.Parametro("@prmEstacion") = prmEstacion
        dato.Parametro("@prmUsuario") = prmUsuario
        dato.SP(True) = "spInsAsociaNumParteMaquila"

        While dato.DataReader.Read
            Estado = dato.DataReader.Item("Estado")
            Texto = dato.DataReader.Item("Texto")
        End While

        Return Texto
    End Function

    Public Function DesasociarNumParteMaquila(ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Dim dato As New clsDato

        Dim Estado As String
        Dim Texto As String = ""

        dato.Parametro("@prmNumParte") = prmNumParte
        dato.Parametro("@prmEstacion") = prmEstacion
        dato.Parametro("@prmUsuario") = prmUsuario
        dato.SP(True) = "spUpdDesasociaNumParteMaquila"

        While dato.DataReader.Read
            Estado = dato.DataReader.Item("Estado")
            Texto = dato.DataReader.Item("Texto")
        End While

        Return Texto
    End Function

#End Region

    Public Function BuscarArticuloRPK() As CResultado
        Dim pRespuesta As New CResultado
        Try

            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As New DataTable

            pResultado = db.SPToDataTable("spQryNumParteRPK", "@")

            If pResultado.Estado Then
                dt = pResultado.Resultado
                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If
            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = ex.Message
            Return pRespuesta
        End Try

    End Function
    Public Function ListaNumParteConsultas() As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaNumParteConsulta", "@")

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Tabla = dt


                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al listar jefes de turno [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CScrap.ListaJefesTurno")

            pRespuesta.Estado = False
            pRespuesta.Texto = ex.Message

            Return pRespuesta
        End Try
    End Function
#End Region


#Region "METODOS NUEVOS CATALOGO NUM PARTE"

    Friend Function ResultadoListaNumParte(ByVal prmBusqueda As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaNumParteAdmin", prmBusqueda, prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = CType(pResultado.Resultado, DataSet)

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy
                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Tabla = dt
                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString
                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al tratar de listar Artículos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar Artículos "
            Return pRespuesta
        End Try
    End Function

    Friend Function Agregar(ByVal prmNumParte As String, ByVal prmDesc1 As String, ByVal prmUPC As String, ByVal prmCategoria As String, ByVal prmDescCategoria As String, ByVal prmCliente As String, ByVal prmUM As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim sResultado As CResultado

            sResultado = db.SPToDataRow("spInsNumParte", prmNumParte, prmDesc1, prmUPC, prmCategoria, prmDescCategoria, prmCliente, prmUM, prmEstacion, prmUsuario)

            If sResultado.Estado Then
                dr = CType(sResultado.Resultado, DataRow)


                If dr("Estado") = "ER" Then
                    cRespuesta.Estado = False
                    cRespuesta.Texto = dr("Texto").ToString
                    cRespuesta.Resultado = Nothing
                Else
                    cRespuesta.Estado = True
                    cRespuesta.Texto = ""
                    cRespuesta.Resultado = Nothing
                End If
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = sResultado.Texto
                cRespuesta.Resultado = Nothing
            End If
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Friend Function Editar(ByVal prmIdNumParte As Int64, ByVal prmNumParte As String, ByVal prmDesc1 As String, ByVal prmUPC As String, ByVal prmCategoria As String, ByVal prmDescCategoria As String, ByVal prmCliente As String, ByVal prmUM As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim sResultado As CResultado

            sResultado = db.SPToDataRow("spUpdNumParte", prmIdNumParte, prmNumParte, prmDesc1, prmUPC, prmCategoria, prmDescCategoria, prmCliente, prmUM, prmEstacion, prmUsuario)

            If sResultado.Estado Then
                dr = CType(sResultado.Resultado, DataRow)


                If dr("Estado") = "ER" Then
                    cRespuesta.Estado = False
                    cRespuesta.Texto = dr("Texto").ToString
                    cRespuesta.Resultado = Nothing
                Else
                    cRespuesta.Estado = True
                    cRespuesta.Texto = ""
                    cRespuesta.Resultado = Nothing
                End If
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = sResultado.Texto
                cRespuesta.Resultado = Nothing
            End If
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function ListaNumPartePorCliente(ByVal prmCliente As String, ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaNumPartePorCliente", prmCliente, prmNumParte, prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Tabla = dt


                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al listar artículos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = ex.Message

            Return pRespuesta
        End Try
    End Function

#End Region

End Class
