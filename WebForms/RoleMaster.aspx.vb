Imports System.Data

Partial Class WebForms_SparkHome
    Inherits System.Web.UI.Page
#Region "Declaration"
    Dim sql As String
    Dim dt As New DataTable
    Dim cls As New DataConnect
#End Region
#Region "Methods"
    Private Sub bind_grid()
        Try
            sql = "Exec [procRoleMaster] '','','','','BindGrid',''"
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                Me.Grid_role.DataSource = dt
                Me.Grid_role.DataBind()
            Else
                Me.Grid_role.DataSource = Nothing
                Me.Grid_role.DataBind()
            End If

        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:bind_grid " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try

    End Sub
    Private Sub clear()
        Try
            txtName.Text = ""
            txtDesc.Text = ""

        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:clear " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub
    Private Sub bind_ddl_active()
        Try
            sql = "select  type_value,type_display_text from tbl_parameter where parameter_type='Status' and active='1' order by type_display_text "
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                ddl_active.DataSource = dt
                ddl_active.DataValueField = dt.Columns(0).ToString
                ddl_active.DataTextField = dt.Columns(1).ToString
                ddl_active.DataBind()
                ddl_active.SelectedIndex = -1
                ddl_active.Items.Insert(0, "--Select--")
                ddl_active.Items.FindByValue("--Select--").Selected = True
            Else
                ddl_active.DataSource = dt
                ddl_active.DataBind()
                ddl_active.SelectedIndex = -1
                ddl_active.Items.Insert(0, "--Select--")
                ddl_active.Items.FindByValue("--Select--").Selected = True
            End If
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:bind_ddl_active " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub
#End Region

    Protected Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Try
            If Trim(Me.txtName.Text.Trim) = "" Then
                Response.Write(cls.msgbox("Please Enter role name"))
                Response.Flush()
                Exit Sub
            End If
            If Trim(Me.txtDesc.Text.Trim) = "" Then
                Response.Write(cls.msgbox("Please Enter role description"))
                Response.Flush()
                Exit Sub
            End If
            If ddl_active.SelectedIndex = 0 Then
                Response.Write(cls.msgbox("Please Select Active/Inactive"))
                Response.Flush()
                Exit Sub
            End If
            If btnadd.Text = "Add" Then
                'Format=Exec procRoleMaster 'rolename','desc','sno','updatedby','action','active'
                sql = "Exec procRoleMaster '" + txtName.Text.Replace("'", "''") + "','" + txtDesc.Text.Replace("'", "''") + "','','" + Session("UserName") + "','Add','1'"

                Try
                    Dim rows_affected As Integer = cls.executedata(sql)
                    If rows_affected > 0 Then
                        Response.Write(cls.msgbox("Role has been added successfully"))
                    Else
                        Response.Write(cls.msgbox("Role could not be added. Please try again later."))
                    End If
                Catch ex As Exception
                    'Violation of PRIMARY KEY constraint
                    If ex.Message.ToString.Contains("Violation of PRIMARY KEY constraint") Then
                        Response.Write(cls.msgbox(" A Similar Record already exists!!!!"))
                        clear()
                        txtName.Focus()
                        Exit Sub
                    Else
                        Response.Write(cls.msgbox(" Exception btnadd_Click() ADD Functionality!" + ex.Message.ToString))
                    End If
                End Try
                bind_grid()
                clear()
                txtName.Focus()

            ElseIf btnadd.Text = "Update" Then
                sql = "Exec procRoleMaster '" + txtName.Text.Replace("'", "''") + "','" + txtDesc.Text.Replace("'", "''") + "','" + Session("Sno") + "','" + Session("UserName") + "','Update','" + ddl_active.SelectedValue + "'"
                Try
                    Dim rows_affected As Integer = cls.executedata(sql)
                    If rows_affected > 0 Then
                        Response.Write(cls.msgbox("Role has been updated successfully"))
                    Else
                        Response.Write(cls.msgbox("Role could not be updated. Please try again later."))
                    End If
                Catch ex As Exception
                    'Violation of PRIMARY KEY constraint
                    If ex.Message.ToString.Contains("Violation of PRIMARY KEY constraint") Then
                        Response.Write(cls.msgbox(" A Similar Record already exists!!!!"))
                        clear()
                        txtName.Focus()
                        Exit Sub
                    Else
                        Response.Write(cls.msgbox(" Exception btnadd_Click() ADD Functionality!" + ex.Message.ToString))
                    End If
                End Try
                bind_grid()
                clear()
                txtName.Focus()
                ddl_active.SelectedIndex = 1
                ddl_active.Items.FindByText("Active").Selected = True
                ddl_active.Enabled = False
                btnadd.Text = "Add"
            End If

        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:btnadd_Click " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub

    Protected Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        Try
            clear()
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:btnclear_Click " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub

    Protected Sub Grid_role_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid_role.RowCommand
        Try
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim sno As String = CType(CType(row.FindControl("sno"), HiddenField).Value, Integer)
            Session("Sno") = sno
            If e.CommandName = "Editcmd" Then
                Dim name As String = row.Cells(0).Text.Trim
                Dim description As String = row.Cells(1).Text.Trim
                txtName.Text = name
                txtDesc.Text = description
                Try
                    ddl_active.SelectedIndex = -1
                    ddl_active.Items.FindByText(row.Cells(2).Text.Trim).Selected = True
                    ddl_active.Enabled = True
                Catch ex As Exception

                End Try
                btnadd.Text = "Update"

            End If
            If e.CommandName = "Deletecmd" Then
                sql = ""
                sql = "Exec procRoleMaster '" + txtName.Text.Replace("'", "''") + "','" + txtDesc.Text.Replace("'", "''") + "','" + Session("Sno") + "','" + Session("UserName") + "','delete',''"
                Dim rows_affected As Integer = cls.executedata(sql)
                If rows_affected > 0 Then
                    Response.Write(cls.msgbox("Role has been deleted successfully"))
                Else
                    Response.Write(cls.msgbox("Role could not be deleted. Please try again later."))
                End If
                bind_grid()
                clear()
            End If
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:Grid_role_RowCommand " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If IsAdmin() = False Then
                    Response.Redirect("Home.aspx")
                End If
                bind_grid()
                txtName.Focus()
                bind_ddl_active()
                ddl_active.SelectedIndex = 1
                ddl_active.Items.FindByText("Active").Selected = True
                ddl_active.Enabled = False
            End If
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:Page_Load " + ex.Message.ToString
            ' Response.Redirect("../ErrorPage.html", False)
        End Try
    End Sub


    Protected Sub Grid_role_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid_role.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim Role As String = e.Row.Cells(0).Text.Trim
                If Role = "Admin" Or Role = "Initiator" Then
                    e.Row.Cells(4).Visible = False
                    e.Row.Cells(4).Visible = False
                    e.Row.Cells(3).Visible = False
                    e.Row.Cells(3).Visible = False
                Else

                End If


            End If

        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/RoleMaster " + " Function:Grid_role_RowDataBound " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub

    Private Function IsAdmin() As Boolean
        Try
            Dim sql As String = ""
            Dim dt As DataTable
            Dim cls As New DataConnect
            Dim role As String = ""
            sql = " select display_name,empno,role_id  from id_user_mst where empno ='" + Session("EmpID").ToString + "' and active=1"
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                Dim results As DataRow() = dt.Select("role_id = '1'")
                If results.Length > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception

            ErrorLog.WriteError("RoleMaster.aspx", "IsAdmin", ex.Message.ToString)
            Return False
        End Try
    End Function
End Class
