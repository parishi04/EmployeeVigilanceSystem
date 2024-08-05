
Imports System.Data

Partial Class Login
    Inherits System.Web.UI.Page
    Dim websrv As New WebReference.list
    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        Try

            If validation() = False Then
                Exit Sub
            End If
            Dim Empno As String = txtuser.Text.Trim
            If IsAuthorised(Empno) Then
                If txtpass.Text = "1234" Then
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Sorry! You are not authorised to access the application!');", True)
                    Exit Sub
                End If
            End If
            ' Dim Empno As String = txtuser.Text

            If txtuser.Text.Trim <> "" Then
                Session("userID") = txtuser.Text.Trim.ToLower
                Response.Redirect("~/WebForms/Employee1.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function validation() As Boolean
        Try

            If txtuser.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter User ID. Eg. 1234 ');", True)
                Return False
            End If
            If txtpass.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your System Password');", True)
                Return False
            End If
            'If websrv.GetAuthenticationfromActiveDirectory(txtuser.Text.Trim, txtpass.Text) Or txtpass.Text = "123#" Then
            'Else

            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your System Password');", True)
            '    Return False
            'End If
            'If String.Compare(Session("VerificationString").ToString(), TxtImgVer.Value.Trim()) <> 0 Then

            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Sorry!! you have entered wrong image verification code');", True)
            '    TxtImgVer.Value = ""
            '    Me.Img.ImageUrl = "Img.aspx"
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then





        End If
    End Sub


    Private Function IsAuthorised(Empno As String) As Boolean
        Try
            Dim sql As String = ""
            Dim dt As DataTable
            Dim cls As New DataConnect
            sql = "SELECT * FROM Employees where  employeeID ='" + Empno + "'" ' ) usr inner join (select * from id_role_mst where active =1 ) rl on usr.role_id =rl.sno"
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ErrorLog.WriteError("Login.aspx", "IsAuthorised", ex.Message.ToString)
            Return False
        End Try
    End Function
End Class
