Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc

Namespace exportPanel.Controllers
	Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			Return View()
		End Function

		Public Function GridViewPartial() As ActionResult
			Return PartialView(GetData())
		End Function
		Public Function Export() As ActionResult
			Dim dataStream = TryCast(Session("stream"), MemoryStream)
			Dim disposition As String = "attachment"
			Dim response = HttpContext.Response
			response.Clear()
			response.Buffer = False
			response.AppendHeader("Content-Type", String.Format("application/{0}", "pdf"))
			response.AppendHeader("Content-Transfer-Encoding", "binary")
			response.AppendHeader("Content-Disposition", String.Format("{0}; filename={1}.{2}", disposition, HttpUtility.UrlEncode("grid").Replace("+", "%20"), "pdf"))
			response.BinaryWrite(dataStream.ToArray())
			response.End()
			Return Nothing
		End Function
		Public Function CallbackExport() As ActionResult
			Thread.Sleep(5000)
			Dim stream As New MemoryStream()
			GridViewExtension.WritePdf(GetSettings(), GetData(), stream)
			Session("stream") = stream
			Return Nothing
		End Function

		Public Shared Function GetData() As Object
			Return Enumerable.Range(0, 10).Select(Function(i) New With {Key .ID = i, Key .Text = "Text " & i})
		End Function

		Public Shared Function GetSettings() As GridViewSettings
			Dim settings = New GridViewSettings()
			settings.Name = "gridView"
			settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartial"}
			Return settings
		End Function
	End Class
End Namespace