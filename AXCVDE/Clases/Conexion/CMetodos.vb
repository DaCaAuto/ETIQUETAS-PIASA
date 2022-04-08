Imports System.Threading
Imports System.Security
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Text
Imports System.Configuration
Imports System.Runtime.InteropServices
Imports System.Drawing

Public NotInheritable Class CMetodos
    Public Declare Function SendARP Lib "iphlpapi.dll" Alias "SendARP" _
       (ByVal DestIP As Long, ByVal SrcIP As Integer, <Out()> ByVal pMacAddr As Byte(), ByRef PhyAddrLen As Integer) As Integer

    Public Enum Bitacora
        INCIDENCIA
        QUERY
        SPS
        VISOR
    End Enum

  

    Public Shared Function ConfigLeer(ByVal key As String, Optional ByVal prmLlaveEncript As String = "") As String
        Try
            Dim valor As String

            valor = ConfigurationManager.AppSettings(key)
            valor = ConfigurationManager.AppSettings(key)

            If prmLlaveEncript <> "" Then
                valor = CMetodos.DesEncriptar(valor, prmLlaveEncript)
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

   

    Public Shared Function EscribirBitacora(ByVal tipo As Bitacora, ByVal excep As Exception, ByVal prmSubCarpeta As String) As Boolean
        Try
            Dim pLinea As String = Mid(excep.StackTrace, _
                                       IIf(excep.StackTrace.IndexOf("línea") = -1, _
                                           Len(excep.StackTrace) + 1, _
                                           excep.StackTrace.IndexOf("línea") + 1))

            'oEscribirBitacora(Bitacora.INCIDENCIA, excep.Message & " [" & pLinea & "]", prmSubCarpeta)

            Return True
        Catch ex As Exception
            ' EscribirBitacora(Bitacora.INCIDENCIA, excep.Message, prmSubCarpeta)

            Return False
        End Try
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

    Public Shared Function ConvertirSegToHMMSS(ByVal prmSegundos As Long) As String
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

    Public Shared Function MACfromIP(ByVal prmIP As String) As String
        Try
            If Trim(prmIP) <> "" Then
                Dim pTempAddr As IPHostEntry = Dns.GetHostEntry(Trim(prmIP))
                Dim pTempAd As System.Net.IPAddress() = pTempAddr.AddressList

                For Each ipAddress As IPAddress In pTempAd
                    If Trim(ipAddress.ToString) = Trim(prmIP) Then
                        Dim pArray As Byte() = New Byte(6) {}
                        Dim r As Long = SendARP(CLng(ipAddress.Address), 0, pArray, pArray.Length)
                        Dim pMac As String = BitConverter.ToString(pArray, 0, pArray.Length - 1)

                        If pMac = "00-00-00-00-00-00" Or pMac = "" Then
                            pArray = New Byte(6) {}
                            r = SendARP(CLng(ipAddress.Address), 0, pArray, pArray.Length)
                            pMac = BitConverter.ToString(pArray, 0, pArray.Length - 1)
                        End If

                        Return pMac
                    End If
                Next
            End If

            Return ""
        Catch ex As Exception   ' Host desconocido
            ' EscribirBitacora(Bitacora.INCIDENCIA, ex.Message, "Metodos.MACfromIP")
            Return ""
        End Try
    End Function

    Public Shared Function ImageToBytes(prmImage As Image) As Byte()
        Try
            If Not prmImage Is Nothing Then
                Dim pBitmapBytes As Byte()

                Dim pBitmap As New Bitmap(prmImage)
                Dim stream As New MemoryStream

                pBitmap.Save(stream, prmImage.RawFormat)
                pBitmapBytes = stream.ToArray

                Return pBitmapBytes
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function BytesToBitmap(prmImageBytes() As Byte) As Bitmap
        Try
            If Not prmImageBytes Is Nothing Then
                Dim ms As New MemoryStream(prmImageBytes)
                Dim bmp As New Bitmap(ms)

                Return bmp
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
