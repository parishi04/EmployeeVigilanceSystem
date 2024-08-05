Imports System.Data

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

#Region "Decleration"
    Dim webSrv As New WebReference.list
    Dim DisplayName, EmployeeNo As String
    Dim EmployeeDetails As String()
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("userID") Is Nothing Then
                    If Session("userID") = "" Then
                        Response.Redirect("~/Webforms/Login.aspx")
                    End If
                    EmployeeDetails = webSrv.getEmpidNameDeptDesg(Session("userID").ToString).Split("#")
                    lblName.Text = EmployeeDetails(1).ToString
                    Session("EmpID") = EmployeeDetails(0).ToString
                    IsAdmin()
                    'If Session("EmpID") = "92475" Then
                    '    li_CS.Visible = True
                    'Else
                    '    li_CS.Visible = False
                    'End If
                Else
                    Response.Redirect("~/Webforms/Login.aspx")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub IsAdmin()
        Try
            Dim sql As String = ""
            Dim dt As DataTable
            Dim cls As New DataConnect
            Dim role As String = ""
            sql = " select display_name,empno,role_id  from id_user_mst where empno ='" + Session("EmpID").ToString + "' and active=1"
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                role = dt.Rows(0)(2).ToString
                If role = "1" Then
                    li_Admin.Visible = True
                    li_request.Visible = True
                    li_report.Visible = True
                    'a_createrequest.Visible = True
                    'a_editrequest.Visible = True
                End If
                If role = "3" Then
                    li_request.Visible = True
                    li_report.Visible = True
                    'a_createrequest.Visible = True
                    'a_editrequest.Visible = True
                End If
                If role = "5" Then
                    li_report.Visible = True
                End If
                If role = "6" Then
                    li_report.Visible = True
                End If
            Else
                'txtempno.Text = Session("EmpID").ToString
                'txtempno.Enabled = False
            End If
        Catch ex As Exception
            ErrorLog.WriteError("ReportAll.aspx", "IsAdmin", ex.Message.ToString)
        End Try
    End Sub


    Protected Sub lbtnLogout_Click(sender As Object, e As EventArgs) Handles lbtnLogout.Click
        Try
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/Webforms/Login.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class

