
Imports System.Data
Imports System.Net
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Configuration




Partial Class Login
    Inherits System.Web.UI.Page
    Dim websrv As New WebReference.list

    Protected Sub gvEmployees_RowEditing(sender As Object, e As GridViewEditEventArgs)
        ' Set the row to be edited
        gvEmployees.EditIndex = e.NewEditIndex
        BindGridView()

        ' Get the data key value (EmployeeID) for the selected row
        Dim employeeID As String = gvEmployees.DataKeys(e.NewEditIndex).Value.ToString()

        ' Fetch the data for the selected EmployeeID and populate the form fields
        Dim cls As New DataConnect
        Dim sql As String = "SELECT * FROM [dbo].[Employees] WHERE EmployeeID = '" + employeeID + "'"
        Dim dt As DataTable = cls.getdata(sql)

        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)

            txtempId.Text = row("EmployeeID").ToString()
            txtdept.Text = row("Department").ToString()
            txtsalary.Text = row("Salary").ToString()
            txtlastName.Text = row("LastName").ToString()
            txtfirstName.Text = row("FirstName").ToString()
            txtdob.Text = row("DOB").ToString()
            ddlGender.SelectedValue = row("Gender").ToString()
            txtEmail.Text = row("Email").ToString()
            txtpemail.Text = row("Personal_Email").ToString()
            txtcontact.Text = row("Contact").ToString()
            txtstate.Text = row("State_Name").ToString()
            txtcity.Text = row("City").ToString()
            txtzip.Text = row("Zip_code").ToString()
            txtposition.Text = row("Position").ToString()
            txtdate.Text = row("Date_Of_Joining").ToString()
            txtofficeAdd.Text = row("Office_Add").ToString()
            txtmanager.Text = row("Manager").ToString()
            txtassistant.Text = row("Assistant").ToString()
            txtexp.Text = row("Experience").ToString()
            txtexpertise.Text = row("Expertise").ToString()

            hfEmployeeID.Value = row("EmployeeID").ToString()
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGridView()
        End If
    End Sub
    Private Sub BindGridView()
        Dim cls As New DataConnect()
        Dim sql As String = "SELECT * FROM [dbo].[Employees] WHERE IsDeleted = 0"
        Dim dt As New System.Data.DataTable()
        dt = cls.getdata(sql)
        gvEmployees.DataSource = dt
        gvEmployees.DataBind()
    End Sub
    Protected Sub gvEmployees_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        ' Handle row updating here
    End Sub
    Protected Sub gvEmployees_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvEmployees.EditIndex = -1
        BindGridView()
    End Sub

    Protected Sub gvEmployees_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            ' Get the EmployeeID from the row being deleted
            Dim employeeID As String = gvEmployees.DataKeys(e.RowIndex).Value.ToString()

            ' Create the SQL delete command
            Dim sql As String = "UPDATE [dbo].[Employees] SET IsDeleted = 1 WHERE EmployeeID = '" + employeeID + "'"


            ' Execute the delete command
            Dim cls As New DataConnect()
            Dim result As Integer = cls.executedata(sql)

            If result > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Data deleted successfully');", True)
                BindGridView()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Data could not be deleted');", True)
            End If
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('" & ex.Message.ToString() & "');", True)
        End Try
    End Sub

    Protected Sub gvEmployees_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gvEmployees.Rows(index)

        Select Case e.CommandName
            Case "Edit"
                ' Handle edit command here
                gvEmployees.EditIndex = index
                BindGridView()

            Case "Delete"
                ' Handle delete command here
                Dim employeeID As String = DirectCast(row.FindControl("lblEmployeeID"), Label).Text
                Dim cls As New DataConnect()
                Dim sql As String = "UPDATE [dbo].[Employees] SET IsDeleted = 1 WHERE EmployeeID = '" + employeeID + "'"
                Dim result As Integer = cls.executedata(sql)
                If result > 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Record deleted successfully');", True)
                    BindGridView()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Record could not be deleted');", True)
                End If
        End Select
    End Sub

    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If validation() Then

                Dim sql As String = ""
                Dim cls As New DataConnect

                If String.IsNullOrEmpty(hfEmployeeID.Value) Then

                    sql = " INSERT INTO [dbo].[Employees]([EmployeeID],[Department],[Salary],[LastName],[FirstName],[DOB],[Gender],[Email],[Personal_Email],[Contact],[State_Name],[City],[Zip_code],[Position],[Date_Of_Joining],[Office_Add],[Manager],[Assistant],[Experience],[Expertise],[Inserted_By],[Inserted_On],[Updated_By],[Updated_On])VALUES('" + txtempId.Text + "','" + txtdept.Text + "','" + txtsalary.Text + "','" + txtlastName.Text + "','" + txtfirstName.Text + "','" + txtdob.Text + "','" + ddlGender.SelectedValue + "','" + txtEmail.Text + "','" + txtpemail.Text + "','" + txtcontact.Text + "','" + txtstate.Text + "','" + txtcity.Text + "','" + txtzip.Text + "','" + txtposition.Text + "','" + txtdate.Text + "','" + txtofficeAdd.Text + "','" + txtmanager.Text + "','" + txtassistant.Text + "','" + txtexp.Text + "','" + txtexpertise.Text + "','" + Session("userID").ToString + "', getdate(),'" + Session("userID").ToString + "' , getdate())"

                Else
                    sql = "UPDATE [dbo].[Employees] SET " & "FirstName = '" + txtfirstName.Text + "'," & "DOB = '" + txtdob.Text + "'," & "Email = '" + txtEmail.Text + "'," & "Contact = '" + txtcontact.Text + "' " & "WHERE EmployeeID = '" + hfEmployeeID.Value + "'"

                End If
                Dim result As Integer = cls.executedata(sql)
                    If result > 0 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('data saved successfully');", True)
                        BindGridView()
                        ClearForm()
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('data could not be saved');", True)
                    End If

                End If



        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('" + ex.Message.ToString + "');", True)

        End Try

    End Sub
    Private Sub ClearForm()
        txtempId.Text = String.Empty
        txtdept.Text = String.Empty
        txtsalary.Text = String.Empty
        txtlastName.Text = String.Empty
        txtfirstName.Text = String.Empty
        txtdob.Text = String.Empty
        ddlGender.SelectedIndex = -1
        txtEmail.Text = String.Empty
        txtpemail.Text = String.Empty
        txtcontact.Text = String.Empty
        txtstate.Text = String.Empty
        txtcity.Text = String.Empty
        txtzip.Text = String.Empty
        txtposition.Text = String.Empty
        txtdate.Text = String.Empty
        txtofficeAdd.Text = String.Empty
        txtmanager.Text = String.Empty
        txtassistant.Text = String.Empty
        txtexp.Text = String.Empty
        txtexpertise.Text = String.Empty
    End Sub

    Private Function validation() As Boolean
        Try

            If txtempId.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter emp ID. Eg. 07801032021');", True)
                Return False
            End If

            If txtdept.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Department');", True)
                Return False
            End If
            If txtsalary.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Salary');", True)
                Return False

            End If

            If txtfirstName.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your First Name');", True)
                Return False

            End If
            If txtlastName.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Last Name');", True)
                Return False

            End If
            If txtdob.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Date of Birth');", True)
                Return False

            End If
            If ddlGender.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Gender');", True)
                Return False

            End If
            If txtEmail.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Email');", True)
                Return False

            End If
            If txtpemail.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Personal Email');", True)
                Return False

            End If
            If txtcontact.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Contact Number');", True)
                Return False

            End If
            If txtstate.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your State');", True)
                Return False

            End If
            If txtcity.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your City');", True)
                Return False

            End If
            If txtzip.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Zip Code');", True)
                Return False

            End If
            If txtposition.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Position');", True)
                Return False

            End If
            If txtdate.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Date of Joining');", True)
                Return False

            End If
            If txtofficeAdd.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Office Address');", True)
                Return False

            End If
            If txtmanager.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Manager Name');", True)
                Return False

            End If
            If txtassistant.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Assistant Name');", True)
                Return False

            End If
            If txtexp.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Your Years of Experience');", True)
                Return False

            End If
            If txtexpertise.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter your Expertise');", True)
                Return False

            End If


            Dim dob As DateTime
            If DateTime.TryParse(txtdob.Text, dob) Then
                Dim today As DateTime = DateTime.Today
                Dim age As Integer = today.Year - dob.Year
                If (dob > today.AddYears(-age)) Then age -= 1

                If age < 18 Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('You must be at least 18 years old.');", True)
                    Return False
                End If

            End If

            Dim empID As String = txtempId.Text.Trim()
            If Not IsNumeric(empID) Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Employee ID must be a number.');", True)
                Return False
            End If

            Dim contact As String = txtcontact.Text.Trim()
            If Not IsNumeric(contact) Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Please enter a valid phone number containing only digits.');", True)
                Return False
            End If


            Dim experience As String = txtexp.Text.Trim()
            If Not IsNumeric(experience) OrElse String.IsNullOrEmpty(experience) Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Please enter a valid number for years of experience.');", True)
                Return False
            End If
            Dim salary As String = txtsalary.Text.Trim()
            If Not IsNumeric(salary) OrElse String.IsNullOrEmpty(experience) Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Please enter a valid number for salary .');", True)
                Return False
            End If

            Dim zip As String = txtzip.Text.Trim()
            If Not IsNumeric(zip) OrElse String.IsNullOrEmpty(experience) Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Please enter a valid number for zip code .');", True)
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function






End Class


