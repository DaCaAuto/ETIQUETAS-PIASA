Imports System.Threading
Imports System.Security
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Text
Imports System.Configuration
Imports Microsoft.Office.Interop


' Clase Estatica, no es necesario instanciar el objeto
Public NotInheritable Class Metodos

    Public Enum Bitacora
        INCIDENCIA
        QUERY
        SPS
        VISOR
    End Enum

    Public Shared Sub MoverControlAnimacion(ByVal prmControl As Object, ByVal prmIniX As Integer, ByVal prmIniY As Integer, ByVal prmFinX As Integer, ByVal prmFinY As Integer)
        Dim pPendiente As Double
        Dim pOrdenada As Double
        Dim pValorY As Double

        ' Ecuación de la Recta:  Y = pPendiente * X + pOrdenada
        pPendiente = (prmFinY - prmIniY) / (prmFinX - prmIniX)
        pOrdenada = prmIniY - pPendiente * prmIniX

        If prmIniX > prmFinX Then
            For index As Integer = prmIniX To prmFinX Step -1
                pValorY = pPendiente * index + pOrdenada

                prmControl.Location = New Point(index, CInt(pValorY))

                Application.DoEvents()
            Next
        Else
            For index As Integer = prmIniX To prmFinX
                pValorY = pPendiente * index + pOrdenada

                prmControl.Location = New Point(index, CInt(pValorY))

                Application.DoEvents()
            Next
        End If
    End Sub



    Public Shared Function TextoPlanoFromQuery(ByVal prmQuery As String, ByVal prmTitulo As String, ByVal prmFecha As String, _
                                      Optional ByVal prmNombreDato1 As String = "", Optional ByVal prmValueDato1 As String = "", _
                                      Optional ByVal prmNombreDato2 As String = "", Optional ByVal prmValueDato2 As String = "", _
                                      Optional ByVal prmNombreDato3 As String = "", Optional ByVal prmValueDato3 As String = "", _
                                      Optional ByVal prmRutaFile As String = "") As Boolean
        Try
            Dim db As New AccesoDatos
            Dim ds As DataSet
            ds = db.QueryToDataSet(prmQuery)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then


                    Dim pContenido As String = ""

                    pContenido = pContenido & prmTitulo
                    pContenido = pContenido & ""
                    pContenido = pContenido & ""
                    pContenido = pContenido & ""
                    pContenido = pContenido & ""
                    pContenido = pContenido & ""


                    For Each dr As DataRow In ds.Tables(0).Rows




                    Next
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Sub ComboFromDataSet(ByRef comboBox As ComboBox, ByVal dataSet As DataSet, ByVal obligarorio As Boolean)
        Try
            If Not dataSet Is Nothing Then
                If dataSet.Tables.Count > 0 Then

                    ' Si no es obligatorio agregar un campo vacio al ComboBox
                    If Not obligarorio Then
                        Dim dr As DataRow = dataSet.Tables(0).NewRow
                        dr(0) = DBNull.Value
                        dr(1) = ""
                        dataSet.Tables(0).Rows.Add(dr)
                    End If

                    ' Ordernar, Asignar y Mostrar en el ComboBox
                    dataSet.Tables(0).DefaultView.Sort = dataSet.Tables(0).Columns(1).ColumnName
                    comboBox.DataSource = dataSet.Tables(0)
                    comboBox.ValueMember = dataSet.Tables(0).Columns(0).ColumnName
                    comboBox.DisplayMember = dataSet.Tables(0).Columns(1).ColumnName
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub ComboFromQuery(ByRef comboBox As ComboBox, ByVal query As String, ByVal obligarorio As Boolean)
        Try
            Dim db As New AccesoDatos
            Dim dataSet As New DataSet
            dataSet = db.QueryToDataSet(query)

            If Not dataSet Is Nothing Then
                If dataSet.Tables.Count > 0 Then

                    ' Si no es obligatorio agregar un campo vacio al ComboBox
                    If Not obligarorio Then
                        Dim dr As DataRow = dataSet.Tables(0).NewRow
                        dr(0) = DBNull.Value
                        dr(1) = "..:Todos:.."
                        dataSet.Tables(0).Rows.Add(dr)
                    End If

                    ' Ordernar, Asignar y Mostrar en el ComboBox
                    dataSet.Tables(0).DefaultView.Sort = dataSet.Tables(0).Columns(1).ColumnName
                    comboBox.DataSource = dataSet.Tables(0)
                    comboBox.ValueMember = dataSet.Tables(0).Columns(0).ColumnName
                    comboBox.DisplayMember = dataSet.Tables(0).Columns(1).ColumnName
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub ListBoxFromDataSet(ByRef listBox As ListBox, ByVal dataSet As DataSet)
        Try

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub ListBoxFromQuery(ByRef listBox As ListBox, ByVal query As String)
        Try

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function ConfigAgregar(ByVal key As String, ByVal valor As String) As Boolean
        Try
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath)

            config.AppSettings.Settings.Add(key, valor)
            config.AppSettings.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection("appSettings")
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function ConfigActualizar(ByVal key As String, ByVal valor As String) As Boolean
        Try
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath)

            config.AppSettings.Settings.Item(key).Value = valor
            config.AppSettings.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection("appSettings")
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function ConfigEliminar(ByVal key As String) As Boolean
        Try
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath)

            config.AppSettings.Settings.Remove(key)
            config.AppSettings.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection("appSettings")
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function ConfigLeer(ByVal key As String, Optional ByVal prmLlaveEncript As String = "") As String
        Try
            Dim valor As String

            valor = ConfigurationManager.AppSettings(key)

            If prmLlaveEncript <> "" Then
                valor = Metodos.DesEncriptar(valor, prmLlaveEncript)
            End If

            Return valor
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function Encriptar(ByVal texto As String, ByVal llave As String) As String
        Try
            Dim objDES As New Cryptography.DESCryptoServiceProvider
            Dim textoBytes As Byte() = UTF8Encoding.UTF8.GetBytes(texto)

            ' Rellenar con F si la llave es menor a 8 Bytes
            If llave.Length < objDES.KeySize / 8 Then
                llave = llave & New String("F", objDES.KeySize / 8 - llave.Length)
            End If

            objDES.Key = UTF8Encoding.UTF8.GetBytes(Left(llave, 8))
            objDES.IV = UTF8Encoding.UTF8.GetBytes(Left(llave, 8))

            Dim ms As New MemoryStream
            Dim cs As New Cryptography.CryptoStream(ms, objDES.CreateEncryptor(), Cryptography.CryptoStreamMode.Write)

            cs.Write(textoBytes, 0, textoBytes.Length)
            cs.FlushFinalBlock()

            Return Convert.ToBase64String(ms.ToArray())
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function DesEncriptar(ByVal texto As String, ByVal llave As String) As String
        Try
            Dim objDES As New Cryptography.DESCryptoServiceProvider
            Dim textoBytes As Byte() = Convert.FromBase64String(texto)

            ' Rellenar con F si la llave es menor a 8 Bytes
            If llave.Length < objDES.KeySize / 8 Then
                llave = llave & New String("F", objDES.KeySize / 8 - llave.Length)
            End If

            objDES.Key = UTF8Encoding.UTF8.GetBytes(Left(llave, 8))
            objDES.IV = UTF8Encoding.UTF8.GetBytes(Left(llave, 8))

            Dim ms As New MemoryStream
            Dim cs As New Cryptography.CryptoStream(ms, objDES.CreateDecryptor(), Cryptography.CryptoStreamMode.Write)

            cs.Write(textoBytes, 0, textoBytes.Length)
            cs.FlushFinalBlock()

            Return UTF8Encoding.UTF8.GetString(ms.ToArray())
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function EnviarCorreo(ByVal cuerpo As String, ByVal asunto As String, ByVal correo As String, ByVal rutaArchivoAdjunto As String) As Boolean
        Try
            Dim msg As New MailMessage
            Dim smtp As New SmtpClient("automatica", 26)
            Dim credencial As New NetworkCredential("usuariopentagono@pentagonodemo.net", "automatica")

            msg.From = New MailAddress("notificacion@pentagonodemo.com")
            msg.To.Add(correo)
            msg.Subject = asunto
            msg.Body = cuerpo
            msg.BodyEncoding = System.Text.Encoding.ASCII
            msg.IsBodyHtml = True
            msg.Priority = MailPriority.Normal
            If rutaArchivoAdjunto <> "" Then
                Dim adjunto As New Attachment(rutaArchivoAdjunto)
                msg.Attachments.Add(adjunto)
            End If

            smtp.EnableSsl = False
            smtp.Credentials = credencial
            smtp.Send(msg)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function ArchivoToString(ByVal prmRuta As String) As String
        Try
            Dim pContent As String = vbNullString

            If File.Exists(prmRuta) Then
                pContent = File.ReadAllText(prmRuta)
            End If

            Return pContent
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function EscribirEnArchivo(ByVal texto As String, ByVal conFecha As Boolean, Optional ByVal rutaArchivo As String = "") As Boolean
        Try
            Dim preFecha As String = TimeOfDay

            Dim sw As New StreamWriter(IIf(rutaArchivo = "", Application.StartupPath & "\" & preFecha & "Archivo.txt", rutaArchivo), True)
            sw.WriteLine(IIf(conFecha, DateTime.Now.ToString() & ": " & texto, texto))
            sw.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function EscribirBitacora(ByVal tipo As Bitacora, ByVal excep As Exception, ByVal prmSubCarpeta As String) As Boolean
        Try
            Dim pLinea As String = Mid(excep.StackTrace, _
                                       IIf(excep.StackTrace.IndexOf("línea") = -1, _
                                           Len(excep.StackTrace) + 1, _
                                           excep.StackTrace.IndexOf("línea") + 1))

            EscribirBitacora(Bitacora.INCIDENCIA, excep.Message & " [" & pLinea & "]", prmSubCarpeta)

            Return True
        Catch ex As Exception
            EscribirBitacora(Bitacora.INCIDENCIA, excep.Message, prmSubCarpeta)

            Return False
        End Try
    End Function

    Public Shared Function EscribirBitacora(ByVal tipo As Bitacora, ByVal prmTexto As String, Optional ByVal prmSubCarpeta As String = "") As Boolean
        Try
            Dim preFecha As String = Now.Year.ToString() & Format(Now.Month, "00") & Format(Now.Day, "00")
            Dim preFechaHora As String = Format(Now.Date, "dd/MM/yyyy") & " " & Format(TimeOfDay, "HH:mm:ss")
            Dim rutaFinal As String

            Select Case tipo
                Case Bitacora.INCIDENCIA
                    If Not Directory.Exists(Application.StartupPath & "\BitacoraIncidencia") Then
                        Directory.CreateDirectory(Application.StartupPath & "\BitacoraIncidencia")
                    End If

                    If prmSubCarpeta <> "" Then
                        If Not Directory.Exists(Application.StartupPath & "\BitacoraIncidencia\" & prmSubCarpeta) Then
                            Directory.CreateDirectory(Application.StartupPath & "\BitacoraIncidencia\" & prmSubCarpeta)
                        End If

                        rutaFinal = Application.StartupPath & "\BitacoraIncidencia\" & prmSubCarpeta
                    Else
                        rutaFinal = Application.StartupPath & "\BitacoraIncidencia"
                    End If

                    Dim sw As New StreamWriter(rutaFinal & "\" & preFecha & " Incidencia.txt", True)
                    sw.WriteLine(preFechaHora & ":   " & prmTexto)
                    sw.Close()

                Case Bitacora.QUERY
                    If Not Directory.Exists(Application.StartupPath & "\BitacoraQuery") Then
                        Directory.CreateDirectory(Application.StartupPath & "\BitacoraQuery")
                    End If

                    If prmSubCarpeta <> "" Then
                        If Not Directory.Exists(Application.StartupPath & "\BitacoraQuery\" & prmSubCarpeta) Then
                            Directory.CreateDirectory(Application.StartupPath & "\BitacoraQuery\" & prmSubCarpeta)
                        End If

                        rutaFinal = Application.StartupPath & "\BitacoraQuery\" & prmSubCarpeta
                    Else
                        rutaFinal = Application.StartupPath & "\BitacoraQuery"
                    End If

                    Dim sw As New StreamWriter(rutaFinal & "\" & preFecha & " Query.txt", True)
                    sw.WriteLine(preFechaHora & ":   " & prmTexto)
                    sw.Close()

                Case Bitacora.SPS
                    If Not Directory.Exists(Application.StartupPath & "\BitacoraSPS") Then
                        Directory.CreateDirectory(Application.StartupPath & "\BitacoraSPS")
                    End If

                    If prmSubCarpeta <> "" Then
                        If Not Directory.Exists(Application.StartupPath & "\BitacoraSPS\" & prmSubCarpeta) Then
                            Directory.CreateDirectory(Application.StartupPath & "\BitacoraSPS\" & prmSubCarpeta)
                        End If

                        rutaFinal = Application.StartupPath & "\BitacoraSPS\" & prmSubCarpeta
                    Else
                        rutaFinal = Application.StartupPath & "\BitacoraSPS"
                    End If

                    Dim sw As New StreamWriter(rutaFinal & "\" & preFecha & " SPS.txt", True)
                    sw.WriteLine(preFechaHora & ":   " & prmTexto)
                    sw.Close()

                Case Bitacora.VISOR
                    If Not Directory.Exists(Application.StartupPath & "\BitacoraVisor") Then
                        Directory.CreateDirectory(Application.StartupPath & "\BitacoraVisor")
                    End If

                    If prmSubCarpeta <> "" Then
                        If Not Directory.Exists(Application.StartupPath & "\BitacoraVisor\" & prmSubCarpeta) Then
                            Directory.CreateDirectory(Application.StartupPath & "\BitacoraVisor\" & prmSubCarpeta)
                        End If

                        rutaFinal = Application.StartupPath & "\BitacoraVisor\" & prmSubCarpeta
                    Else
                        rutaFinal = Application.StartupPath & "\BitacoraVisor"
                    End If

                    Dim sw As New StreamWriter(rutaFinal & "\" & preFecha & " Visor.txt", True)
                    sw.WriteLine(preFechaHora & ":   " & prmTexto)
                    sw.Close()

                Case Else
                    If Not Directory.Exists(Application.StartupPath & "\BitacoraGeneral") Then
                        Directory.CreateDirectory(Application.StartupPath & "\BitacoraGeneral")
                    End If

                    If prmSubCarpeta <> "" Then
                        If Not Directory.Exists(Application.StartupPath & "\BitacoraGeneral\" & prmSubCarpeta) Then
                            Directory.CreateDirectory(Application.StartupPath & "\BitacoraGeneral\" & prmSubCarpeta)
                        End If

                        rutaFinal = Application.StartupPath & "\BitacoraGeneral\" & prmSubCarpeta
                    Else
                        rutaFinal = Application.StartupPath & "\BitacoraGeneral"
                    End If

                    Dim sw As New StreamWriter(rutaFinal & "\" & preFecha & " General.txt", True)
                    sw.WriteLine(preFechaHora & ":   " & prmTexto)
                    sw.Close()
            End Select
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function TiempoTranscurrido(ByVal prmFechaHoraInicio As DateTime) As String
        Try
            Dim pHora As String

            pHora = ""
            pHora = pHora & Fix(DateDiff(DateInterval.Second, prmFechaHoraInicio, Now) / 3600) & ":"
            pHora = pHora & Format(Fix((DateDiff(DateInterval.Second, prmFechaHoraInicio, Now) Mod 3600) / 60), "00") & ":"
            pHora = pHora & Format(Fix(DateDiff(DateInterval.Second, prmFechaHoraInicio, Now) Mod 60), "00")
            'pHora = pHora & "  (h:mm:ss)"
            Return pHora
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function CelsiusToFahrenheit(ByVal prmCelsius As Decimal) As Decimal
        Try
            Return 9 * prmCelsius / 5 + 32
        Catch ex As Exception
            Return prmCelsius
        End Try
    End Function

    Public Shared Function FahrenheitToCelsius(ByVal prmFahrenheit As Decimal) As Decimal
        Try
            Return 5 * (prmFahrenheit - 32) / 9
        Catch ex As Exception
            Return prmFahrenheit
        End Try
    End Function

    Public Shared Function TolFahrenheitToCelsius(ByVal prmFahrenheit As Decimal) As Decimal
        Try
            Dim pLimInf As Decimal = Metodos.FahrenheitToCelsius(prmFahrenheit)
            Dim pLimSup As Decimal = Metodos.FahrenheitToCelsius(0)

            Return pLimInf - pLimSup
        Catch ex As Exception
            Return prmFahrenheit
        End Try
    End Function

    Public Shared Function TolCelsiusToFahrenheit(ByVal prmCelsius As Decimal) As Decimal
        Try
            Dim pLimInf As Decimal = Metodos.CelsiusToFahrenheit(prmCelsius)
            Dim pLimSup As Decimal = Metodos.CelsiusToFahrenheit(0)

            Return pLimInf - pLimSup
        Catch ex As Exception
            Return prmCelsius
        End Try
    End Function

    Public Shared Function ConvertirToHMMSS(ByVal prmSegundos As Long) As String
        Try
            Dim pHora As String

            pHora = ""
            pHora = pHora & Fix(prmSegundos / 3600) & ":"
            pHora = pHora & Format(Fix((prmSegundos Mod 3600) / 60), "00") & ":"
            pHora = pHora & Format(Fix(prmSegundos Mod 60), "00")

            Return pHora
        Catch ex As Exception
            Return "0:00:00"
        End Try
    End Function

    Public Shared Function EspaciosFillTruc(ByVal prmDato As String, ByVal prmLongitud As Integer) As String
        Try
            Dim pDatoTratado As String = prmDato

            If Len(prmDato) > prmLongitud Then              ' Trucar si el dato es mayor al requerido
                pDatoTratado = Mid(prmDato, 1, prmLongitud)
            ElseIf Len(prmDato) = prmLongitud Then          ' Retornar sin modificacion
                pDatoTratado = prmDato
            ElseIf Len(prmDato) < prmLongitud Then          ' Retornar con espacios agregados
                pDatoTratado = prmDato & Space(prmLongitud - Len(prmDato))
            End If

            Return pDatoTratado
        Catch ex As Exception
            Return prmDato
        End Try
    End Function

    Public Shared Function DataTablePagina(ByVal prmDataTable As DataTable, ByVal prmNumPagina As Integer, ByVal prmNumRows As Integer)
        Try
            If prmDataTable Is Nothing Then
                Return Nothing
            End If

            ' Obtener el total de paginas
            Dim pTotalPaginas As Integer

            If prmDataTable.Rows.Count Mod prmNumPagina = 0 Then
                pTotalPaginas = prmDataTable.Rows.Count / prmNumRows
            Else
                pTotalPaginas = CInt(prmDataTable.Rows.Count / prmNumRows) + 1
            End If

            ' Clonar DataTable que se va retornar
            Dim dtNew As New DataTable
            dtNew = prmDataTable.Clone

            ' Generar Registros del Datable que se retornara
            Dim pInicio As Integer = 0
            Dim pFin As Integer = 0

            If prmNumPagina = 1 Then
                pFin = prmNumRows - 1
            Else
                pInicio = (CInt(prmNumPagina) - 1) * prmNumRows
                pFin = pInicio + prmNumRows
            End If

            If pFin > prmDataTable.Rows.Count - 1 Then
                pFin = prmDataTable.Rows.Count - 1
            End If

            For i As Integer = pInicio To pFin
                dtNew.ImportRow(prmDataTable.Rows(i))
            Next

            Return dtNew
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
