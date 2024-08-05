Imports System.Data

Partial Class WebForms_Home
    Inherits System.Web.UI.Page
#Region "Decleration"
    Dim webSrv As New WebReference.list
    Dim DisplayName, EmployeeNo As String
    Dim EmployeeDetails As String()
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("EmployeeForm.aspx")
    End Sub

End Class
