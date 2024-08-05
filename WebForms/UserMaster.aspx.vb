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
            sql = "exec [procUserMaster] '','','','','','BindGrid',''"
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                Me.Grid_User.DataSource = dt
                Me.Grid_User.DataBind()
            Else
                Me.Grid_User.DataSource = Nothing
                Me.Grid_User.DataBind()
            End If

        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:bind_grid " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try

    End Sub

    Private Sub bind_ddl_user()
        Try
            sql = "select Sap_Name + '  (Employee ID : ' +Employee_Number +')', Employee_Number  from tele.dbo.entity_details  order by AD_Display_Name "
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                ddl_user.DataSource = dt
                ddl_user.DataValueField = dt.Columns(1).ToString
                ddl_user.DataTextField = dt.Columns(0).ToString
                ddl_user.DataBind()
                ddl_user.SelectedIndex = -1
                ddl_user.Items.Insert(0, "--Select--")
                ddl_user.Items.FindByValue("--Select--").Selected = True
            Else
                ddl_user.DataSource = dt
                ddl_user.DataBind()
                ddl_role.SelectedIndex = -1
                ddl_role.Items.Insert(0, "--Select--")
                ddl_role.Items.FindByValue("--Select--").Selected = True
            End If
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:bind_ddl_user " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub
    Private Sub bind_ddl_role()
        Try
            sql = " select role_name,sno from id_role_mst order by role_name "
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                ddl_role.DataSource = dt

                ddl_role.DataValueField = dt.Columns(1).ToString
                ddl_role.DataTextField = dt.Columns(0).ToString
                ddl_role.DataBind()
                ddl_role.SelectedIndex = -1
                ddl_role.Items.Insert(0, "--Select--")
                ddl_role.Items.FindByValue("--Select--").Selected = True
            Else
                ddl_role.DataSource = dt
                ddl_role.DataBind()
                ddl_role.SelectedIndex = -1
                ddl_role.Items.Insert(0, "--Select--")
                ddl_role.Items.FindByValue("--Select--").Selected = True
            End If
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:bind_ddl_role " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub

    Private Function validation() As Boolean
        Try
            If ddl_user.SelectedIndex = 0 Then
                Response.Write(cls.msgbox("Please Select User"))
                Response.Flush()
                Return False

            End If
            If ddl_role.SelectedIndex = 0 Then
                Response.Write(cls.msgbox("Please Select Role"))
                Response.Flush()
                Return False

            End If

            If ddl_active.SelectedIndex = 0 Then
                Response.Write(cls.msgbox("Please Select Active/Inactive"))
                Response.Flush()
                Return False

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

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
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:bind_ddl_active " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub

    Public Sub Clear()
        Try
            ddl_role.SelectedIndex = 0
            ddl_user.SelectedIndex = 0
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:Clear " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If IsAdmin() = False Then
                    Response.Redirect("Home.aspx")
                End If
                bind_grid()
                bind_ddl_user()
                bind_ddl_role()
                bind_ddl_active()
                ddl_active.SelectedIndex = 1
                ddl_active.Items.FindByText("Active").Selected = True
                ddl_active.Enabled = False
            End If
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:Page_Load " + ex.Message.ToString
            'Response.Redirect("../ErrorPage.aspx", False)
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

    Protected Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Try
            If validation() = True Then
                Dim arr() As String = ddl_user.SelectedItem.Text.Split("(Employee ID")
                Dim display_name As String = arr(0).Trim
                If btnadd.Text = "Add" Then

                    '                    CREATE Procedure [dbo].[procUserMaster]
                    '(
                    '--@user_id as Varchar(200),
                    '@empno as varchar(10),
                    '@display_name as varchar(500), 
                    '@role_id AS int,
                    '@sno as int,
                    '@UpdatedBy varchar(50),
                    '@action varchar(50),
                    '@active char(1)
                    ')


                    sql = "Exec procUserMaster '" + ddl_user.SelectedValue.Trim.Replace("'", "''") + "', '" + display_name + "' ,'" + ddl_role.SelectedValue + "','','" + Session("UserName") + "','Add','1'"
                    Try
                        Dim rows_affected As Integer = cls.executedata(sql)
                        If rows_affected > 0 Then
                            Response.Write(cls.msgbox("User has been added successfully"))
                        Else
                            Response.Write(cls.msgbox("User could not be added. Please try again later."))
                        End If
                    Catch ex As Exception
                        'Violation of PRIMARY KEY constraint
                        If ex.Message.ToString.Contains("Violation of PRIMARY KEY constraint") Then
                            Response.Write(cls.msgbox("User already exists!!!!"))
                            Clear()
                            ddl_user.Focus()
                            Exit Sub
                        Else
                            Response.Write(cls.msgbox(" Exception btnadd_Click() ADD Functionality!" + ex.Message.ToString))
                        End If
                    End Try

                    bind_grid()
                    Clear()

                ElseIf btnadd.Text = "Update" Then

                    sql = "Exec procUserMaster '" + ddl_user.SelectedValue.Trim.Replace("'", "''") + "', '" + display_name + "' ,'" + ddl_role.SelectedValue + "','" + Session("sno") + "','" + Session("UserName") + "','Update','" + ddl_active.SelectedValue + "'"
                    Try
                        Dim rows_affected As Integer = cls.executedata(sql)
                        If rows_affected > 0 Then
                            Response.Write(cls.msgbox("User has been updated successfully"))
                        Else
                            Response.Write(cls.msgbox("User could not be updated. Please try again later."))
                        End If
                    Catch ex As Exception
                        'Violation of PRIMARY KEY constraint
                        If ex.Message.ToString.Contains("Violation of PRIMARY KEY constraint") Then
                            Response.Write(cls.msgbox("User already exists!!!!"))
                            Clear()
                            ddl_user.Focus()
                            Exit Sub
                        Else
                            Response.Write(cls.msgbox(" Exception btnadd_Click() ADD Functionality!" + ex.Message.ToString))
                        End If
                    End Try

                    bind_grid()
                    Clear()
                    btnadd.Text = "Add"
                End If
            Else
                Response.Write(cls.msgbox("User Could not be Saved. Please try again later."))
                Response.Flush()
                Exit Sub
            End If


        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:btnadd_Click " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub

    Protected Sub Grid_User_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid_User.RowCommand
        Try

            'CREATE Procedure [dbo].[procUserMaster]
            '(
            '--@user_id as Varchar(200),
            '@empno as varchar(10),
            '@display_name as varchar(500), 
            '@role_id AS int,
            '@sno as int,
            '@UpdatedBy varchar(50),
            '@action varchar(50)
            ')

            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim sno As String = CType(CType(row.FindControl("sno"), HiddenField).Value, Integer)
            Session("Sno") = sno
            If e.CommandName = "Editcmd" Then

                sql = "select role_id from id_user_mst where sno='" + sno + "'"
                dt = Nothing
                dt = cls.getdata(sql)
                Dim empno As String = row.Cells(0).Text.Trim
                Dim role_id As String = dt.Rows(0)(0).ToString
                Try
                    ddl_user.SelectedValue = empno
                Catch ex As Exception
                    Response.Write(cls.msgbox("User not found!"))
                End Try
                Try
                    ddl_role.SelectedValue = role_id
                Catch ex As Exception
                    Response.Write(cls.msgbox("Role not found!"))
                End Try
                Try
                    ddl_active.SelectedIndex = -1
                    ddl_active.Items.FindByText(row.Cells(3).Text.Trim).Selected = True
                    ddl_active.Enabled = True
                Catch ex As Exception

                End Try
                btnadd.Text = "Update"

            End If
            If e.CommandName = "Deletecmd" Then
                sql = ""
                sql = "Exec procUserMaster '', '','' ,'" + sno + "','" + Session("UserName") + "','Delete',''"
                Dim rows_affected As Integer = cls.executedata(sql)
                If rows_affected > 0 Then
                    Response.Write(cls.msgbox("User has been deleted successfully"))
                Else
                    Response.Write(cls.msgbox("User could not be deleted. Please try again later."))
                End If
                bind_grid()
                Clear()

            End If
        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:Grid_User_RowCommand " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub


    Protected Sub Grid_User_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid_User.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim status As String = e.Row.Cells(3).Text.Trim
                If status = "Inactive" Then
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                    e.Row.Cells(5).Enabled = False
                Else
                    e.Row.Cells(5).ForeColor = Drawing.Color.Green
                    e.Row.Cells(5).Enabled = True
                End If


            End If

        Catch ex As Exception
            Session("err") = "Error Page:MasterForms/UserMaster " + " Function:Grid_User_RowDataBound " + ex.Message.ToString
            Response.Redirect("../ErrorPage.aspx", False)
        End Try
    End Sub
End Class
