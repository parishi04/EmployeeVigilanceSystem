
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.IO

Partial Class WebForms_QCDetails
    Inherits System.Web.UI.Page

#Region "Decleration"
    Dim sql As String
    Dim dt As New DataTable
    Dim cls As New DataConnect
    Dim webSrv As New WebReference.list
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                If IsAdmin() = False Then
                    Response.Redirect("Home.aspx")
                End If
                Dim RequestID As String = ""
                ViewState("status") = ""

                If Not Request.QueryString("ID") Is Nothing Then
                    RequestID = Request.QueryString("ID").ToString
                    ViewState("RequestID") = RequestID
                Else
                    ViewState("RequestID") = ""
                End If
                FillControlsOnLoad(RequestID)
                ControlVisibility()

            End If
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "Page_Load", ex.Message.ToString)
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
                Dim results As DataRow() = dt.Select("role_id = '1' or role_id='3'")
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
    Private Sub FillControlsOnLoad(ByVal RequestID As String)
        Try
            FillDdls()
            PageLoadValidations()
            'InitiateAttachmentGrid()

        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "FillControlsOnLoad", ex.Message.ToString)
        End Try
    End Sub

    Private Sub PageLoadValidations()
        Try
            Dim date_today As String = Date.Today.ToString("yyyy-MM-dd")
            Dim date_today_minus7 As String = Date.Today.AddDays(-7).ToString("yyyy-MM-dd")
            txtRegistrationDate.Attributes.Add("max", date_today)
            txtAssignmentDate.Attributes.Add("max", date_today)
            txtCompletionDate.Attributes.Add("max", date_today)
            txtSubmissionDate.Attributes.Add("max", date_today)
            txtApprovalDate.Attributes.Add("max", date_today)
            txtATRInitiationDate.Attributes.Add("max", date_today)
            txtComplaintClosingDate.Attributes.Add("max", date_today)
            txtRegistrationDate.Attributes.Add("min", date_today_minus7)
            txtAssignmentDate.Attributes.Add("min", date_today_minus7)
            txtCompletionDate.Attributes.Add("min", date_today_minus7)
            txtSubmissionDate.Attributes.Add("min", date_today_minus7)
            txtApprovalDate.Attributes.Add("min", date_today_minus7)
            txtATRInitiationDate.Attributes.Add("min", date_today_minus7)
            txtComplaintClosingDate.Attributes.Add("min", date_today_minus7)

            Me.txtComplaintSource.Attributes.Add("onKeyUp", "javascript:return CheckLength(this,500);")
            ' Me.txtComplaintGist.Attributes.Add("onKeyUp", "javascript:return CheckLength(this,5000);")
            Me.txtFindings.Attributes.Add("onKeyUp", "javascript:return CheckLength(this,5000);")
            Me.txtAtr.Attributes.Add("onKeyUp", "javascript:return CheckLength(this,5000);")
            Me.txtRemarks.Attributes.Add("onKeyUp", "javascript:return CheckLength(this,5000);")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitiateAttachmentGrid()
        Try
            'Dim dtAttach As New DataTable
            'Dim DcSNo As New DataColumn
            'Dim DcAttach As New DataColumn
            'dtAttach.Columns.Add(DcSNo)
            'dtAttach.Columns.Add(DcAttach)
            'ViewState("DTAttach") = dtAttach

            Dim sql As String = ""
            sql = "update tblFiles set active=0 where ReferenceNo='" + ViewState("RequestID") + "'"
            Dim result As Integer = 0
            result = cls.executedata(sql)
            If result > 0 Then

                grv_Attachment.DataSource = Nothing
                grv_Attachment.DataBind()
            Else

            End If

        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "InitiateKPIGrid", ex.Message.ToString)
        End Try
    End Sub

    Private Sub FillDdls()
        Try
            Dim sql As String = ""
            sql = "select Type_Display_text,type_value from [dbo].[tbl_Parameter] where Parameter_type ='RequestStatus' and active=1 and type_value<>'10'"
            sql += " select empno,display_name [Name] from id_user_mst where role_id =2 and active =1"
            Dim ds As DataSet
            ds = cls.GetDataSet(sql)
            If ds.Tables.Count > 0 Then
                Try
                    FillControls.fillDdl(ddlStatus, ds.Tables(0), "Type_Display_text", "type_value")
                Catch ex As Exception
                End Try
                Try
                    FillControls.fillDdl(ddlIO, ds.Tables(1), "Name", "empno")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "FillDdls", ex.Message.ToString)
        End Try
    End Sub



    Private Sub DisplayData(ByVal RequestID As String)
        Try
            sql = ""
            sql += " select [ReferenceNo], [RequestDate], [ComplaintSource], [ComplaintGist], [GroupSuspected], [IOEmpno], [IOName], [AssignDate], [InvestigationCompletionDate], [Findings], [ApprovalSubmissionDate], [ApprovalDate], [ATR], [ATRInitiationDate], [ComplaintClosingDate], [Remarks], [RequestStatus],[ChargesSubstantiated] from tblRequestMaster where active=1 and ReferenceNo='" + RequestID + "' "

            sql += " select Id,  [Name] from tblfiles where ReferenceNo ='" + RequestID + "' and active=1"

            Dim ds As DataSet

            ds = cls.GetDataSet(sql)

            If ds.Tables.Count > 0 Then

                Try
                    If ds.Tables(0).Rows.Count > 0 Then
                        lblRequestNo.Text = "Reference No : " + ds.Tables(0).Rows(0)(0).ToString
                        Try
                            txtRegistrationDate.Text = CType(ds.Tables(0).Rows(0)(1).ToString, DateTime).ToString("yyyy-MM-dd")
                        Catch ex As Exception
                        End Try
                        txtComplaintSource.Text = ds.Tables(0).Rows(0)(2).ToString

                        txtComplaintGist.Text = EncryptDecrypt.Decrypt(ds.Tables(0).Rows(0)(3).ToString)
                        txtGrp.Text = ds.Tables(0).Rows(0)(4).ToString
                        Try
                            ddlIO.SelectedIndex = -1
                            ddlIO.Items.FindByValue(ds.Tables(0).Rows(0)(5).ToString).Selected = True
                        Catch ex As Exception
                        End Try
                        Try
                            txtAssignmentDate.Text = CType(ds.Tables(0).Rows(0)(7).ToString, DateTime).ToString("yyyy-MM-dd")
                        Catch ex As Exception
                        End Try
                        Try
                            txtCompletionDate.Text = CType(ds.Tables(0).Rows(0)(8).ToString, DateTime).ToString("yyyy-MM-dd")
                        Catch ex As Exception
                        End Try
                        txtFindings.Text = EncryptDecrypt.Decrypt(ds.Tables(0).Rows(0)(9).ToString)
                        Try
                            txtSubmissionDate.Text = CType(ds.Tables(0).Rows(0)(10).ToString, DateTime).ToString("yyyy-MM-dd")
                        Catch ex As Exception
                        End Try
                        Try
                            txtApprovalDate.Text = CType(ds.Tables(0).Rows(0)(11).ToString, DateTime).ToString("yyyy-MM-dd")
                        Catch ex As Exception
                        End Try
                        txtAtr.Text = EncryptDecrypt.Decrypt(ds.Tables(0).Rows(0)(12).ToString)
                        Try
                            txtATRInitiationDate.Text = CType(ds.Tables(0).Rows(0)(13).ToString, Date).ToString("yyyy-MM-dd")
                        Catch ex As Exception
                        End Try
                        Try
                            txtComplaintClosingDate.Text = CType(ds.Tables(0).Rows(0)(14).ToString, Date).ToString("yyyy-MM-dd")
                        Catch ex As Exception
                        End Try
                        txtRemarks.Text = EncryptDecrypt.Decrypt(ds.Tables(0).Rows(0)(15).ToString)
                        Try
                            ddlStatus.SelectedIndex = -1
                            ddlStatus.Items.FindByValue(ds.Tables(0).Rows(0)(16).ToString).Selected = True
                        Catch ex As Exception
                            ddlStatus.Items.FindByValue("20").Selected = True
                        End Try
                        Try
                            ddlChargesSubstantiated.SelectedIndex = -1
                            ddlChargesSubstantiated.Items.FindByValue(ds.Tables(0).Rows(0)(17).ToString).Selected = True
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception

                End Try
                Try
                    FillControls.fillGrid(grv_Attachment, ds.Tables(1))
                    'ViewState("DTAttach") = ds.Tables(1)
                Catch ex As Exception
                    FillControls.fillGrid(grv_Attachment, Nothing)
                End Try

            End If
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "BindControlsOnPageLoad", ex.Message.ToString)

        End Try
    End Sub

    Private Sub ControlVisibility()
        Try
            Dim stage As String = ""
            If Not (Request.QueryString("ID") Is Nothing) Then
                ViewState("Action") = "Update"
                pnl_edit.Visible = True
                DisplayData(ViewState("RequestID"))
            Else
                ViewState("Action") = "Fresh"
                pnl_edit.Visible = False
            End If
            'If Not (Request.QueryString("Approve") Is Nothing) Then
            '    pnlApproval.Visible = True
            '    btnCancelSelf.Visible = False
            'End If

            'If Not Request.QueryString("View") Is Nothing Then
            '    If Request.QueryString("View").ToString = "1" Then
            '        btnSubmit.Visible = False
            '        btnSave.Visible = False
            '        btnCancelSelf.Visible = True
            '        pnlApproval.Visible = False
            '    End If
            '    If Request.QueryString("View").ToString = "2" Then
            '        btnSubmit.Visible = False
            '        btnSave.Visible = False
            '        btnCancelSelf.Visible = False
            '        pnlApproval.Visible = False
            '    End If
            'End If
            'If Not Request.QueryString("status") Is Nothing Then
            '    If Request.QueryString("status").ToString = "Cancelled" Then
            '        btnSubmit.Visible = False
            '        btnSave.Visible = False
            '        btnCancelSelf.Visible = False
            '        pnlApproval.Visible = False
            '    End If
            'End If
            'If ViewState("status") = "C" Then
            '    btnCancelSelf.Visible = False
            'End If
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "ControlVisibility", ex.Message.ToString)
        End Try
    End Sub

    Private Function GetStage(ByVal RequestID As String) As String
        Try
            Dim sql As String = ""
            sql = "select status from USB_tblRequestMaster where RequestID ='" + RequestID + "' and active=1"
            Dim dt As DataTable
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                hfStage.Value = dt.Rows(0)(0).ToString
                ViewState("status") = dt.Rows(0)(0).ToString
                Return dt.Rows(0)(0).ToString
            Else
                ViewState("status") = ""
                Return ""
            End If
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "GetStage", ex.Message.ToString)
            ViewState("status") = ""
            hfStage.Value = ""
            Return ""
        End Try
    End Function





    Protected Sub grv_Attachment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grv_Attachment.RowCommand
        Try
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

            Dim id As String = CType(row.FindControl("hf_ID"), HiddenField).Value
            If e.CommandName = "DeleteRow1" Then
                If DeleteAttachment(id) Then
                    Dim dt As DataTable = BindAttachment()
                    If dt.Rows.Count > 0 Then
                        grv_Attachment.DataSource = dt
                        grv_Attachment.DataBind()
                    Else
                        grv_Attachment.DataSource = Nothing
                        grv_Attachment.DataBind()
                    End If
                End If
            End If
            If e.CommandName = "cmdView1" Then
                DownloadFile(id)
            End If
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "grv_KPI_RowCommand", ex.Message.ToString)
        End Try
    End Sub
    Private Sub DownloadFile(ByVal Rowid As String)
        Dim id As Integer = Rowid
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim constr As String = ConfigurationManager.ConnectionStrings("Vigilance_Con").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                cmd.CommandText = "select Name, Data, ContentType from tblFiles where Id=@Id"
                cmd.Parameters.AddWithValue("@Id", id)
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    sdr.Read()
                    bytes = DirectCast(sdr("Data"), Byte())
                    contentType = sdr("ContentType").ToString()
                    fileName = sdr("Name").ToString()
                End Using
                con.Close()
            End Using
        End Using
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = contentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub btnRefreshAttachment_Click(sender As Object, e As EventArgs) Handles btnRefreshAttachment.Click 'refresh Ba Grid
        Try
            InitiateAttachmentGrid()

        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "btnRefreshEmp_Click", ex.Message.ToString)
        End Try
    End Sub

    Protected Sub btnAddAttachment_Click(sender As Object, e As EventArgs) Handles btnAddAttachment.Click

        Dim filesize As Integer = fileupload1.PostedFile.ContentLength
            Try
            If filesize <= 5242880 And filesize <> 0 Then

                If Path.GetExtension(fileupload1.FileName).ToLower() = ".pdf" Or Path.GetExtension(fileupload1.FileName).ToLower() = ".jpg" Or Path.GetExtension(fileupload1.FileName).ToLower() = ".jpeg" Or Path.GetExtension(fileupload1.FileName).ToLower() = ".png" Then
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Invalid Format Document!');", True)
                    Exit Sub
                End If
            ElseIf filesize = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('No File Uploaded');", True)
                Exit Sub
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('File Size is Greater than 5 mb !');", True)
                Exit Sub
                End If




                Dim filename As String = Upload()
            If filename <> "" Then
                Dim dt As DataTable = BindAttachment()
                If dt.Rows.Count > 0 Then
                    grv_Attachment.DataSource = dt
                    grv_Attachment.DataBind()
                Else
                    grv_Attachment.DataSource = Nothing
                    grv_Attachment.DataBind()
                End If
            Else

            End If

        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "btnAddKPI_Click", ex.Message.ToString)
        End Try
    End Sub

    Private Function BindAttachment() As DataTable
        Try
            Dim sql As String = ""
            sql = "select ID, [Name] from tblFiles where referenceNo='" + ViewState("RequestID") + "' and active=1 "
            Dim dt As DataTable
            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return New DataTable
            End If
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function
    Private Function DeleteAttachment(ByVal ID As String) As Boolean
        Try
            Dim sql As String = ""
            sql = "update tblFiles set active=0 where id='" + ID + "'"
            Dim result As Integer = 0
            result = cls.executedata(sql)
            If result > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    'Private Function Upload() As String
    '    Try
    '        Dim filename As String = Path.GetFileName(fileupload1.PostedFile.FileName)
    '        Dim contentType As String = fileupload1.PostedFile.ContentType
    '        Using fs As Stream = fileupload1.PostedFile.InputStream
    '            Using br As New BinaryReader(fs)
    '                Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
    '                Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
    '                Using con As New SqlConnection(constr)
    '                    Dim query As String = "insert into tblFiles values (@ReferenceNo, @Name, @ContentType, @Data,@Active,@InsBy,@InsOn)"
    '                    Using cmd As New SqlCommand(query)
    '                        cmd.Connection = con
    '                        cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = ViewState("RequestID")
    '                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
    '                        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contentType
    '                        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
    '                        cmd.Parameters.Add("@Active", SqlDbType.Binary).Value = "1"
    '                        cmd.Parameters.Add("@InsBy", SqlDbType.Binary).Value = Convert.ToString(Session("EmpID"))
    '                        con.Open()
    '                        cmd.ExecuteNonQuery()
    '                        con.Close()
    '                    End Using
    '                End Using
    '            End Using
    '        End Using
    '        Return filename
    '    Catch ex As Exception

    '    End Try

    '    ' Response.Redirect(Request.Url.AbsoluteUri)
    'End Function
    Private Function Upload() As String
        Try
            Dim filename As String = Path.GetFileName(fileupload1.PostedFile.FileName)
            Dim contentType As String = fileupload1.PostedFile.ContentType
            Using fs As Stream = fileupload1.PostedFile.InputStream
                Using br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("Vigilance_Con").ConnectionString
                    Using con As New SqlConnection(constr)
                        Dim query As String = "insert into tblFiles values (@ReferenceNo, @Name, @ContentType, @Data,@Active,@InsBy,@InsOn)"
                        Using cmd As New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = ViewState("RequestID")
                            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
                            cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contentType
                            cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
                            cmd.Parameters.Add("@Active", SqlDbType.VarChar).Value = "1"
                            cmd.Parameters.Add("@InsBy", SqlDbType.VarChar).Value = Convert.ToString(Session("EmpID"))
                            cmd.Parameters.Add("@InsOn", SqlDbType.DateTime).Value = Date.Now
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                    End Using
                End Using
            Return filename
        Catch ex As Exception
            Return ""
        End Try

        ' Response.Redirect(Request.Url.AbsoluteUri)
    End Function
    Public Function fileSubmit() As String
        Dim filesize As Integer = fileupload1.PostedFile.ContentLength
        Dim filename As String = fileupload1.FileName.ToString()
        filename = filename.Replace("&", "and")
        Dim PhotoNo As String
        Dim filepath As String = ""
        Dim filePathToSave As String = ""
        Try
            If filesize < 2000002 And filesize <> 0 Then

                If Path.GetExtension(fileupload1.FileName).ToLower() = ".pdf" Or Path.GetExtension(fileupload1.FileName).ToLower() = ".jpg" Or Path.GetExtension(fileupload1.FileName).ToLower() = ".jpeg" Or Path.GetExtension(fileupload1.FileName).ToLower() = ".png" Then


                    PhotoNo = "Attachment_" + Date.Now.ToString("ddMMMyyyyHHmmss") + "_" + filename.Replace("&", "and").Replace("/", "").Replace("#", "").Replace("\", "") '+ Path.GetExtension(fileupload1.FileName).ToLower

                    Try
                        '=================for Delete============================
                        If File.Exists(Server.MapPath("Attachment/" & PhotoNo.ToString())) Then
                            Try
                                File.Delete(Server.MapPath("Attachment/" & PhotoNo.ToString()))
                            Catch ex As Exception
                            End Try
                        End If
                        '=========================================

                        filepath = "/Attachment/" + PhotoNo.ToString() + ""
                        ' filePathToSave = Server.MapPath(filepath)
                        filePathToSave = PhotoNo.ToString()
                        fileupload1.SaveAs(Server.MapPath("~/Attachment") + "/" + PhotoNo.ToString())


                    Catch ex As Exception
                    End Try
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Invalid Format Document!');", True)
                    Return ""
                End If
            ElseIf filesize = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('No File Uploaded');", True)
                Return ""
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('File Size is Greater than 2 mb !');", True)
                Return ""
            End If
            Return filePathToSave
        Catch ex As Exception
            ErrorLog.WriteError("EurekaUpload.aspx", "btnSubmit_Click", ex.Message.ToString)
            Return ""
        End Try

    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            If ViewState("Action") = "Fresh" Then
                If validation_draft() = False Then
                    Exit Sub
                End If
                Dim RequestID As String = InsertFinal()
                If RequestID <> "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request Saved Successfully!! Your Reference ID is " + RequestID + "');", True)
                    'btnSubmit.Visible = True
                    ViewState("RequestID") = RequestID
                    ViewState("Action") = "Update"
                    'ViewState("Stage") = "10"
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request could not be Saved!');", True)
                End If
            ElseIf ViewState("Action") = "Update" Then
                If validation() = False Then
                    Exit Sub
                End If
                If updateFinal(ViewState("RequestID").ToString) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request updated successfully!');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request could not be updated!');", True)
                End If
            End If
            DisplayData(ViewState("RequestID"))
            Response.Redirect("~/Webforms/EditRequestBin.aspx?M=1")
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "btnSave_Click", ex.Message.ToString)
        End Try
    End Sub
    Private Sub ViewDocument(ByVal filepath As String)
        Try
            If filepath <> "" Then
                Dim file As New System.IO.FileInfo(filepath)
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.[End]()
                Else

                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub
    ''<WebMethod>
    ''Public Shared Function getEmployees(ByVal EmpName As String) As List(Of Employees)
    ''    Dim empObj As List(Of Employees) = New List(Of Employees)()
    ''    Dim cs As String = ConfigurationManager.ConnectionStrings("tele_data").ConnectionString

    ''    Try

    ''        Using con As SqlConnection = New SqlConnection(cs)

    ''            Using com As SqlCommand = New SqlCommand()

    ''                Dim sql As String = (Convert.ToString("select top 10 sap_name+'#'+Employee_Number+' ('+ Designation +')'+'$'+Department [Name]   from [Tele_TPC].dbo.Entity_Details where SAP_Name  like '%") & EmpName) + "%'  and employee_status='Active'"

    ''                com.CommandText = String.Format(sql)
    ''                com.Connection = con
    ''                con.Open()
    ''                Dim sdr As SqlDataReader = com.ExecuteReader()
    ''                Dim emp As Employees = Nothing

    ''                While sdr.Read()
    ''                    emp = New Employees()
    ''                    emp.EmpName = Convert.ToString(sdr("Name"))
    ''                    empObj.Add(emp)
    ''                End While
    ''            End Using
    ''        End Using

    ''    Catch ex As Exception
    ''        Console.WriteLine("Error {0}", ex.Message)
    ''    End Try

    ''    Return empObj
    ''End Function

    ''<Serializable>
    ''Public Class Employees
    ''    Public Property EmpName As String
    ''End Class




    Private Function InsertFinal() As String
        Try
            Dim RequestID As String = Insert_MainTable()
            If RequestID <> "" Then
                Return RequestID
            Else
                Return ""
            End If

        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "InsertFinal", ex.Message.ToString)

            Return ""
        End Try
    End Function

    Private Function Insert_MainTable() As String
        Try
            Dim RequestID As String = ""
            RequestID = create_new_Reference_No()

            sql += " INSERT INTO [dbo].[tblRequestMaster]"
            sql += "([ReferenceNo]"
            sql += ",[RequestDate]"
            sql += ",[ComplaintSource]"
            sql += ",[ComplaintGist]"
            sql += ",[GroupSuspected]"
            sql += ",[IOEmpno]"
            sql += ",[IOName]"
            sql += ",[AssignDate]"
            sql += "     ,[RequestStatus]"
            sql += "     ,[Active]"
            sql += "     ,[InsBy]"
            sql += "     ,[InsOn]"
            sql += "     ,[UpdtBy]"
            sql += "     ,[UpdtOn])"
            sql += "      VALUES"
            sql += "     ('" + RequestID + "'"
            sql += "     ,cast('" + txtRegistrationDate.Text + "' as date)"
            sql += "     ,'" + txtComplaintSource.Text.Replace("'", "''") + "'"
            sql += "     ,'" + EncryptDecrypt.encrypt(txtComplaintGist.Text.Trim).Replace("'", "''") + "'"
            sql += "     ,'" + txtGrp.Text.Trim.Replace("'", "''") + "'"
            sql += "     ,'" + ddlIO.SelectedValue + "'"
            sql += "     ,'" + ddlIO.SelectedItem.Text.Replace("'", "''") + "'"
            sql += "     ,cast('" + txtAssignmentDate.Text + "' as date)"
            sql += "     ,'10'"
            sql += "     ,'1'"
            sql += "     ,'" + Session("EmpID") + "'"
            sql += "     ,getdate()"
            sql += "     ,'" + Session("EmpID") + "'"
            sql += "     ,getdate())"
            Dim RowsAffected As Integer = cls.executedata(sql)
            If RowsAffected > 0 Then
                Return RequestID
            Else
                Return ""
            End If

        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "InsertMainTable", ex.Message.ToString)
            Return ""
        End Try
    End Function

    Public Function create_new_Reference_No() As String
        Try
            Dim CNO As DataTable
            sql = "select top 1  max(cast(substring (ReferenceNo ,13,len(ReferenceNo ))as bigint))  from tblRequestMaster order by 1 desc"
            CNO = cls.getdata(sql)
            Dim m As Integer
            If CNO.Rows.Count > 0 Then
                Dim Value As String = CNO.Rows(0)(0).ToString()
                If Value = "" Then
                    m = 1
                Else
                    m = Int32.Parse(Value) + 1
                End If
            Else
                Dim Value As String = CNO.Rows(0)(0).ToString()
                If Value = "" Then
                    m = 1
                Else
                    m = Int32.Parse(Value) + 1
                End If
            End If
            Return "VIG/2023-24/" + m.ToString  'm.ToString.PadLeft(5, "0")
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "create_new_Reference_No", ex.Message.ToString)
            Return "False"
        End Try
    End Function

    'Private Function Insert_NextApproval(ByVal RequestID As String) As String
    '    Try

    '        sql = ""
    '        sql = " exec USB_ProcInsertNextApproval '" + RequestID + "','" + Session("EmpID") + "','" + txtJustification.Text.Trim.Replace("'", "''") + "'"

    '        Dim result As Integer = cls.executedata(sql)
    '        If result > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        ErrorLog.WriteError("createRequest.aspx", "Insert_NextApproval", ex.Message.ToString)
    '        Return False
    '    End Try
    'End Function

    'Private Function Insert_NextApproval_History(ByVal RequestID As String, fromEmpno As String, ToEmpno As String, Remarks As String, stage As String, status As String) As String
    '    Try

    '        sql = ""
    '        sql = " exec USB_ProcInsertNextApproval_History '" + RequestID + "','" + fromEmpno + "','" + ToEmpno + "','" + Remarks.Replace("'", "''") + "','" + stage + "','" + status + "','" + Session("EmpID") + "'"

    '        Dim result As Integer = cls.executedata(sql)
    '        If result > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        ErrorLog.WriteError("createRequest.aspx", "Insert_NextApproval", ex.Message.ToString)
    '        Return False
    '    End Try
    'End Function

    Private Function Insert_Attachment(ByVal RequestID As String) As Boolean
        Try
            Dim sql As String = ""
            If Not ViewState("DTAttach") Is Nothing Then
                If ViewState("DTAttach").rows.count > 0 Then
                    Dim DTAttach As New DataTable
                    DTAttach = ViewState("DTAttach")
                    For Each row As DataRow In DTAttach.Rows
                        If row.RowState = DataRowState.Deleted Then
                            Continue For
                        End If
                        sql += " INSERT INTO [dbo].[tblattachment]"
                        sql += "           ([ReferenceNo]"
                        sql += "           ,[FileName]"
                        sql += "           ,[InsBy]"
                        sql += "           ,[InsOn]"

                        sql += "           ,[Active]"
                        sql += "           )"
                        sql += "     VALUES("
                        sql += "            '" + RequestID + "'"
                        sql += "            ,'" + row(1).ToString.Replace("'", "''") + "'"
                        sql += "           ,'" + Session("EmpID") + "'"
                        sql += "           ,getdate()"

                        sql += "           ,'1'"
                        sql += "           )"
                    Next
                    Dim rows_inserted As Integer = cls.executedata(sql)
                    If rows_inserted > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                Return False
            End If


        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "Insert_KPI", ex.Message.ToString)
            Return False
        End Try
    End Function








    'Private Sub DisableControls()
    '    Try
    '        panel1.Enabled = False
    '        '  panel2.Enabled = False
    '        panel3.Enabled = False
    '        panel4.Enabled = False
    '        ' panel5.Enabled = False
    '        panel6.Enabled = False

    '        ' PopCalendar5.Enabled = False
    '        'lbtnBack.Enabled = True
    '        'lbtnBack.ForeColor = Drawing.Color.White
    '        btnSave.ForeColor = Drawing.Color.White
    '        btnSubmit.ForeColor = Drawing.Color.White
    '        btnSave.Visible = False
    '        btnSubmit.Visible = False

    '        For i As Integer = 0 To grvTimePlan.Rows.Count - 1
    '            Try

    '                CType(grvTimePlan.Rows(i).FindControl("PopCalendar5"), RJS.Web.WebControl.PopCalendar).Visible = False
    '                CType(grvTimePlan.Rows(i).FindControl("PopCalendar6"), RJS.Web.WebControl.PopCalendar).Visible = False
    '            Catch ex As Exception
    '            End Try

    '        Next
    '        Try
    '            If ((Session("EmpID").ToString = ViewState("leader").ToString) Or (Session("EmpID").ToString = ViewState("projectInitiator").ToString)) And (CType(hfStage.Value, Integer) < 50 And CType(hfStage.Value, Integer) > 5) Then
    '                '   panel5.Enabled = True
    '                btnProceed.Visible = True
    '            Else
    '                btnProceed.Visible = False
    '            End If
    '        Catch ex As Exception

    '        End Try
    '    Catch ex As Exception
    '        ErrorLog.WriteError("createRequest.aspx", "DisableControls", ex.Message.ToString)
    '    End Try
    'End Sub


    Private Function validation_draft() As Boolean
        Try
            If txtRegistrationDate.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Registration Date!');", True)
                txtRegistrationDate.Focus()
                Return False
            End If
            If txtComplaintSource.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Complaint Source');", True)
                txtComplaintSource.Focus()
                Return False
            End If
            If txtAssignmentDate.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Select Assignment Date!');", True)
                txtAssignmentDate.Focus()
                Return False
            End If
            If txtGrp.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Group!');", True)
                txtGrp.Focus()
                Return False
            End If
            If ddlIO.SelectedIndex = 0 Or ddlIO.SelectedIndex = -1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Select Investigating Officer!');", True)
                ddlIO.Focus()
                Return False
            End If
            If txtComplaintGist.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Complaint Gist!');", True)
                ' txtComplaintGist.Focus()
                Return False
            End If
            If CType(txtAssignmentDate.Text, Date) < CType(txtRegistrationDate.Text, Date) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Assignment Date should be greater that Registeration Date!');", True)
                txtAssignmentDate.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "validation_draft", ex.Message.ToString)
            Return False
        End Try

    End Function


    Private Function updateFinal(ByVal RequestID As String) As Boolean
        Try
            Dim insertAttach As Boolean = False
            Dim strArray As New ArrayList
            sql = ""

            sql += " UPDATE [dbo].[tblRequestMaster]"
            sql += "    SET  "
            If txtRegistrationDate.Text = "" Then
                sql += "  [RequestDate] = NULL"
            Else
                sql += "  [RequestDate] = cast('" + txtRegistrationDate.Text.Trim.Replace("'", "''") + "' as date)"
            End If

            sql += " ,[ComplaintSource] = '" + txtComplaintSource.Text.Trim.Replace("'", "''") + "'"
            sql += " ,[ComplaintGist] = '" + EncryptDecrypt.Encrypt(txtComplaintGist.Text.Trim).Replace("'", "''") + "' "
            sql += " ,[GroupSuspected] = '" + txtGrp.Text.Trim.Replace("'", "''") + "' "
            sql += " ,[IOEmpno] = '" + ddlIO.SelectedValue + "' "
            sql += " ,[IOName] = '" + ddlIO.SelectedItem.Text + "' "
            If txtAssignmentDate.Text = "" Then
                sql += " ,[AssignDate] = NULL"
            Else
                sql += " ,[AssignDate] = cast('" + txtAssignmentDate.Text.Trim.Replace("'", "''") + "' as date)"
            End If
            If txtCompletionDate.Text = "" Then
                sql += " ,[InvestigationCompletionDate] = NULL"
            Else
                sql += " ,[InvestigationCompletionDate] = cast('" + txtCompletionDate.Text.Trim.Replace("'", "''") + "' as date)"
            End If

            sql += " ,[Findings] = '" + EncryptDecrypt.Encrypt(txtFindings.Text.Trim).Replace("'", "''") + "' "
            If txtSubmissionDate.Text = "" Then
                sql += " ,[ApprovalSubmissionDate] = NULL"
            Else
                sql += " ,[ApprovalSubmissionDate] = cast('" + txtSubmissionDate.Text.Trim.Replace("'", "''") + "' as date)"
            End If
            If txtApprovalDate.Text = "" Then
                sql += " ,[ApprovalDate] = NULL"
            Else
                sql += " ,[ApprovalDate] = cast('" + txtApprovalDate.Text.Trim.Replace("'", "''") + "' as date)"
            End If


            sql += " ,[ATR] = '" + EncryptDecrypt.Encrypt(txtAtr.Text).Replace("'", "''") + "' "
            If txtATRInitiationDate.Text = "" Then
                sql += " ,[ATRInitiationDate] = NULL"
            Else
                sql += " ,[ATRInitiationDate] = cast('" + txtATRInitiationDate.Text.Trim.Replace("'", "''") + "' as date)"
            End If
            If txtComplaintClosingDate.Text = "" Then
                sql += " ,[ComplaintClosingDate] = NULL"
            Else
                sql += " ,[ComplaintClosingDate] = cast('" + txtComplaintClosingDate.Text.Trim.Replace("'", "''") + "' as date)"
            End If

            sql += " ,[Remarks] = '" + EncryptDecrypt.Encrypt(txtRemarks.Text.Trim).Replace("'", "''") + "' "
            sql += " ,[RequestStatus] = '" + ddlStatus.SelectedValue + "' "
            sql += " ,[ChargesSubstantiated] = '" + ddlChargesSubstantiated.SelectedValue + "' "
            sql += " ,[UpdtBy] =  '" + Session("EmpID").ToString + "' "
            sql += " ,[UpdtOn] = getdate() "
            sql += "  WHERE referenceNo='" + RequestID + "' "

            strArray.Add(sql)
            'sql = ""
            'sql = "update S2_TblQC_ParamTrans set active=0 where RequestID='" + RequestID + "'"
            'sql = "delete from tblattachment where [ReferenceNo]='" + RequestID + "'"
            'strArray.Add(sql)

            Dim result As Integer = cls.executedata_batchmode(strArray)
            'If result = 2 Then
            '    insertAttach = Insert_Attachment(RequestID)
            'Else
            '    Return False
            'End If
            'If insertAttach = True Then
            '    Return True
            'Else
            '    Return False
            'End If
            If result = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "updateFinal", ex.Message.ToString)
            Return False
        End Try
    End Function


    'Private Function updateFinalStage(ByVal RequestID As String, ByVal Stage As String, ByVal Action As String, ByVal level As String) As Boolean
    '    Try
    '        Dim insertAttach As Boolean = False
    '        Dim strArray As New ArrayList
    '        sql = ""
    '        sql = " UPDATE [dbo].[USB_tblRequestMaster]"
    '        sql += "    SET "
    '        sql += "       [Stage] = '" + Stage + "'"
    '        If Action = "Approved" Then
    '            sql += "       ,[Status] = 'A'"
    '        ElseIf Action = "Cancelled" Then
    '            sql += "       ,[Status] = 'C'"
    '        End If

    '        sql += "       ,[UpdtOn] = GETDATE()"
    '        sql += "       ,[UpdtBy] = '" + Session("EmpID").ToString + "'"
    '        sql += "  WHERE RequestID ='" + RequestID + "'"

    '        strArray.Add(sql)
    '        If level = "RM" Then
    '            If Action = "Approved" Then
    '                sql = ""
    '                sql = " update usb_tblnextapproval set status='A',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate(), remarks='" + txtRemarks.Text.Trim.Replace("'", "''") + "', insOn=getdate()  where stage='20' and active=1 and RequestID='" + RequestID + "'"
    '                strArray.Add(sql)

    '                sql = ""
    '                sql = " update usb_tblnextapproval set status='P',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate()  where stage='30' and active=1 and RequestID='" + RequestID + "'"
    '                strArray.Add(sql)
    '            End If
    '            If Action = "Cancelled" Then
    '                sql = ""
    '                sql = " update usb_tblnextapproval set status='C',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate(), remarks='" + txtRemarks.Text.Trim.Replace("'", "''") + "', insOn=getdate()  where stage='20' and active=1 and RequestID='" + RequestID + "'"
    '                strArray.Add(sql)
    '            End If
    '        End If
    '        If level = "CS_HOG" Then
    '            If Action = "Approved" Then
    '                sql = ""
    '                sql = " update usb_tblnextapproval set status='A',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate(), remarks='" + txtRemarks.Text.Trim.Replace("'", "''") + "', insOn=getdate()  where stage='30' and active=1 and RequestID='" + RequestID + "'"
    '                strArray.Add(sql)

    '                sql = ""
    '                sql = " update usb_tblnextapproval set status='P',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate(), remarks='" + txtRemarks.Text.Trim.Replace("'", "''") + "', insOn=getdate()  where stage='40' and active=1 and RequestID='" + RequestID + "'"
    '                strArray.Add(sql)
    '            End If
    '            If Action = "Cancelled" Then
    '                sql = ""
    '                sql = " update usb_tblnextapproval set status='C',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate(), remarks='" + txtRemarks.Text.Trim.Replace("'", "''") + "', insOn=getdate()  where stage='30' and active=1 and RequestID='" + RequestID + "'"
    '                strArray.Add(sql)
    '            End If
    '        End If

    '        Dim result As Integer = cls.executedata_batchmode(strArray)
    '        If result > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        ErrorLog.WriteError("createRequest.aspx", "updateFinalStage", ex.Message.ToString)
    '        Return False
    '    End Try
    'End Function

    'Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
    '    Try
    '        If validation() = False Then
    '            Exit Sub
    '        End If
    '        Dim InsertNextApproval As Boolean = False
    '        Dim InsertNextApprovalHistory As Boolean = False
    '        Dim Update As Boolean = False
    '        If ViewState("Action") = "Fresh" Then

    '        ElseIf ViewState("Action") = "Update" Then
    '            Update = updateFinal(ViewState("RequestID").ToString, "10")

    '            InsertNextApproval = Insert_NextApproval(ViewState("RequestID"))
    '            InsertNextApprovalHistory = Insert_NextApproval_History(ViewState("RequestID"), Session("EmpID"), GetRMEmpNo(Session("EmpID")), txtJustification.Text.Trim, "10", "N")

    '        End If
    '        If Update = True And InsertNextApproval = True And InsertNextApprovalHistory = True Then
    '            sendmail_RequestSubmit(ViewState("RequestID").ToString)
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request ID : " + ViewState("RequestID").ToString + " Submitted Successfully! Please get it approved from your Reporting Manager for further action.');", True)
    '        Else
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request ID : " + ViewState("RequestID").ToString + "  could not be submitted!');", True)
    '        End If
    '        DisplayData(ViewState("RequestID"))
    '        DisableControls()
    '    Catch ex As Exception
    '        ErrorLog.WriteError("createRequest.aspx", "btnSubmit_Click", ex.Message.ToString)
    '    End Try
    'End Sub

    Private Function validation() As Boolean
        Try
            If ddlStatus.SelectedIndex = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Select Status!');", True)
                txtCompletionDate.Focus()
                Return False
            End If

            If ddlStatus.SelectedValue = "30" Then
                If txtCompletionDate.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Completion Date!');", True)
                    txtCompletionDate.Focus()
                    Return False
                End If
                If txtFindings.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Findings!');", True)
                    txtFindings.Focus()
                    Return False
                End If
                If txtSubmissionDate.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please enter date of submission for approval!');", True)
                    txtSubmissionDate.Focus()
                    Return False
                End If
                If txtApprovalDate.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Approval Date!');", True)
                    txtApprovalDate.Focus()
                    Return False
                End If

                If txtAtr.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter ATR on the remarks of CEO!');", True)
                    txtAtr.Focus()
                    Return False
                End If
                If txtATRInitiationDate.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter ATR Initiation Date by concerned dept!');", True)
                    txtATRInitiationDate.Focus()
                    Return False
                End If
                If txtComplaintClosingDate.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Complaint Closing Date!');", True)
                    txtComplaintClosingDate.Focus()
                    Return False
                End If
                If txtRemarks.Text.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Remarks!');", True)
                    txtRemarks.Focus()
                    Return False
                End If
                If ddlChargesSubstantiated.SelectedValue = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Select Charges Substantiated!');", True)
                    ddlChargesSubstantiated.Focus()
                    Return False
                End If
                If validation_Attachment() = False Then
                    Return False
                End If
            End If

            Try
                If CType(txtSubmissionDate.Text, Date) < CType(txtCompletionDate.Text, Date) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Date of submission for approval should be greater than Date of Completion of Investigation!');", True)
                    Return False
                End If
            Catch ex As Exception

            End Try
            Try
                If CType(txtCompletionDate.Text, Date) < CType(txtAssignmentDate.Text, Date) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Date of Completion of Investigation should be greater than Assignment Date!');", True)
                    Return False
                End If
            Catch ex As Exception

            End Try
            Try
                If CType(txtApprovalDate.Text, Date) < CType(txtSubmissionDate.Text, Date) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Approval Date should be greater than Date of submission for approval!');", True)
                    Return False
                End If
            Catch ex As Exception

            End Try
            Try
                If CType(txtATRInitiationDate.Text, Date) < CType(txtApprovalDate.Text, Date) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('ATR Initiation Date by concerned department should be greater than Approval Date!');", True)
                    Return False
                End If
            Catch ex As Exception

            End Try
            Try
                If CType(txtComplaintClosingDate.Text, Date) < CType(txtATRInitiationDate.Text, Date) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Complaint Closing Date by concerned department should be greater than ATR Initiation Date!');", True)
                    Return False
                End If
            Catch ex As Exception

            End Try

            Return True
        Catch ex As Exception
            ErrorLog.WriteError("createRequest.aspx", "validation", ex.Message.ToString)
            Return False
        End Try

    End Function


    Private Function validation_Attachment() As Boolean
        Try
            'If ViewState("DTAttach") Is Nothing Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('It is mandatory to add 1 Attachment';", True)
            '    'ScriptManager.RegisterStartupScript(updatepanel1, updatepanel1.GetType(), "msgbox", "alert('It is mandatory to add 1 KPI!');", True)

            '    Return False
            'End If

            If grv_Attachment.Rows.Count = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('It is mandatory to add 1 Attachment'", True)
                'ScriptManager.RegisterStartupScript(updatepanel1, updatepanel1.GetType(), "msgbox", "alert('It is mandatory to add 1 Attachment!');", True)

                Return False
            End If

            Return True
        Catch ex As Exception

            ErrorLog.WriteError("createRequest.aspx", "validation_Attachment", ex.Message.ToString)
            Return False
        End Try
    End Function
    ''Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownloadProjectCharter.Click
    ''    Try

    ''        Dim sql As String = "select isnull(FilePath,'') FilePath from S2_TblEureka_RegisterMain  where RequestID='" + ViewState("RequestID") + "' and active=1"
    ''        Dim dt As DataTable
    ''        Dim cls As New DataConnect
    ''        dt = cls.getdata(sql)
    ''        If dt.Rows.Count > 0 Then
    ''            If dt.Rows(0)(0).ToString = "" Then
    ''                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Eureka ID : " + ViewState("RequestID").ToString + " Project Charter Not Available.');", True)
    ''            Else
    ''                Dim path As String = Server.MapPath("~\Attachment\" & dt.Rows(0)(0).ToString)
    ''                ' Response.Redirect(Server.MapPath("~\Attachment\" & dt.Rows(0)(0).ToString))
    ''                ViewDocument(path)
    ''            End If
    ''        Else
    ''            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Eureka ID : " + ViewState("RequestID").ToString + " Project Charter Not Available.');", True)
    ''        End If
    ''    Catch ex As Exception
    ''        ' ErrorLog.WriteError("EurekaTPDDLLevel1Assess.aspx", "btnDownload_Click", ex.Message.ToString)
    ''    End Try
    ''End Sub
    ''Private Sub ViewDocument(ByVal filename As String)
    ''    Try
    ''        If filename <> "" Then

    ''            Dim file As New System.IO.FileInfo(filename)
    ''            If file.Exists Then
    ''                Response.Clear()
    ''                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
    ''                Response.AddHeader("Content-Length", file.Length.ToString())
    ''                Response.ContentType = "application/octet-stream"
    ''                Response.WriteFile(file.FullName)
    ''                Response.[End]()
    ''            Else
    ''                'Response.Write("This file does not exist.")
    ''            End If
    ''        End If
    ''    Catch ex As Exception
    ''        Response.Write(ex.Message)

    ''    End Try
    ''End Sub

    ''Protected Sub btnUpdateAdmin_Click(sender As Object, e As EventArgs) Handles btnUpdateAdmin.Click
    ''    Try
    ''        If validation() = False Then
    ''            Exit Sub
    ''        End If

    ''        'If ViewState("Action") = "Fresh" Then

    ''        'ElseIf ViewState("Action") = "Update" Then
    ''        '    updateFinal(ViewState("RequestID").ToString, "10")
    ''        '    sendmail()
    ''        'End If
    ''        Dim stage As String = GetStage(ViewState("RequestID").ToString)
    ''        Dim result As Boolean = updateFinal(ViewState("RequestID").ToString, stage)
    ''        If result = True Then
    ''            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Eureka ID : " + ViewState("RequestID").ToString + " Updated Successfully.');", True)
    ''            DisplayData(ViewState("RequestID").ToString)
    ''        Else
    ''            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Eureka ID : " + ViewState("RequestID").ToString + " could not be updated.');", True)
    ''        End If

    ''        ' DisableControls()
    ''    Catch ex As Exception
    ''        ErrorLog.WriteError("EurekaDetailsTPDDL.aspx", "btnSubmit_Click", ex.Message.ToString)
    ''    End Try
    ''End Sub
    'Private Sub sendmail_RequestAprrovedByL1(requestID As String)
    '    Try
    '        Dim subject As String = ""
    '        Dim mailbody As String = ""
    '        Dim toEmail As String = ""
    '        Dim ccEmail As String = ""
    '        Dim sql As String = "select Email_address,(select  Email_address from tele.dbo.entity_details manager where manager.employee_number=emp.emp_manager_no) manager_Email,SAP_Name from tele.dbo.entity_details emp where Employee_Number='" + txtEmpno.Text + "'"
    '        Dim dt As DataTable
    '        dt = cls.getdata(sql)
    '        If dt.Rows.Count > 0 Then
    '            subject = "USB Unblock request " + requestID + " - Approved by Reporting Manager " '+ dt.Rows(0)(2) + ""
    '            toEmail = "aamir.hussain@tatapower-ddl.com"
    '            ccEmail = dt.Rows(0)(0).ToString + ";" + dt.Rows(0)(1).ToString
    '            mailbody = getMailBody_RequestSubmit(requestID, "The same has been approved by their Reporting Manager. ")
    '            Dim mail As New SendMail
    '            mail.sendEMail(mailbody, toEmail, ccEmail, subject, "")
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub sendmail_RequestSubmit(requestID As String)
    '    Try
    '        Dim subject As String = ""
    '        Dim mailbody As String = ""
    '        Dim toEmail As String = ""
    '        Dim ccEmail As String = ""
    '        Dim sql As String = "select Email_address,(select  Email_address from tele.dbo.entity_details manager where manager.employee_number=emp.emp_manager_no) manager_Email,SAP_Name from tele.dbo.entity_details emp where Employee_Number='" + Session("EmpID").ToString + "'"
    '        Dim dt As DataTable
    '        dt = cls.getdata(sql)
    '        If dt.Rows.Count > 0 Then
    '            subject = "USB Unblock request " + requestID + " - raised by " + dt.Rows(0)(2) + ""
    '            toEmail = dt.Rows(0)(1).ToString
    '            ccEmail = dt.Rows(0)(0).ToString
    '            mailbody = getMailBody_RequestSubmit(requestID, "")
    '            Dim mail As New SendMail
    '            mail.sendEMail(mailbody, toEmail, ccEmail, subject, "")
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Function getMailBody_RequestSubmit(ByVal requestID As String, ByVal msg As String) As String
    '    Dim strMailBody As StringBuilder = New StringBuilder()
    '    Dim File As String
    '    File = Server.MapPath("../MailBody/Request_Register.html")
    '    Dim sr As StreamReader
    '    Dim fi As New FileInfo(File)
    '    If System.IO.File.Exists(File) Then
    '        sr = System.IO.File.OpenText(File)
    '        strMailBody.Append(sr.ReadToEnd())
    '        sr.Close()
    '    End If

    '    strMailBody = strMailBody.Replace("<!--RequestNo-->", requestID)
    '    strMailBody = strMailBody.Replace("<!--RaisedBy-->", txtEmpName.Text)
    '    strMailBody = strMailBody.Replace("<!--RaisedOn-->", Date.Today.ToString("dd MMM yyyy"))
    '    strMailBody = strMailBody.Replace("<!--Reason-->", txtJustification.Text)
    '    strMailBody = strMailBody.Replace("<!--Message-->", msg)
    '    Return strMailBody.ToString
    'End Function


    'Private Sub sendmail_AllApproved(requestID As String)
    '    Try
    '        Dim subject As String = ""
    '        Dim mailbody As String = ""
    '        Dim toEmail As String = ""
    '        Dim ccEmail As String = ""
    '        Dim sql As String = "select Email_address,(select  Email_address from tele.dbo.entity_details manager where manager.employee_number=emp.emp_manager_no) manager_Email,SAP_Name from tele.dbo.entity_details emp where Employee_Number='" + txtEmpno.Text + "'"
    '        Dim dt As DataTable
    '        dt = cls.getdata(sql)
    '        If dt.Rows.Count > 0 Then
    '            subject = "USB Unblock request " + requestID + " - Approved"
    '            toEmail = dt.Rows(0)(0).ToString
    '            ccEmail = dt.Rows(0)(1).ToString + ";aamir.hussain@tatapower-ddl.com"
    '            mailbody = getMailBody_AllApproved(requestID)
    '            Dim mail As New SendMail
    '            mail.sendEMail(mailbody, toEmail, ccEmail, subject, "")
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Function getMailBody_AllApproved(ByVal requestID As String) As String
    '    Dim strMailBody As StringBuilder = New StringBuilder()
    '    Dim File As String
    '    File = Server.MapPath("../MailBody/Request_AllApproved.html")
    '    Dim sr As StreamReader
    '    Dim fi As New FileInfo(File)
    '    If System.IO.File.Exists(File) Then
    '        sr = System.IO.File.OpenText(File)
    '        strMailBody.Append(sr.ReadToEnd())
    '        sr.Close()
    '    End If

    '    strMailBody = strMailBody.Replace("<!--RequestNo-->", requestID)
    '    strMailBody = strMailBody.Replace("<!--RaisedBy-->", txtEmpName.Text)
    '    strMailBody = strMailBody.Replace("<!--RaisedOn-->", Date.Today.ToString("dd MMM yyyy"))
    '    strMailBody = strMailBody.Replace("<!--Reason-->", txtJustification.Text)

    '    Return strMailBody.ToString
    'End Function



    'Private Sub sendmail_RejectRevertCancel(requestID As String, Action As String)
    '    Try
    '        Dim subject As String = ""
    '        Dim mailbody As String = ""
    '        Dim toEmail As String = ""
    '        Dim ccEmail As String = ""
    '        Dim sql As String = "select Email_address,(select  Email_address from tele.dbo.entity_details manager where manager.employee_number=emp.emp_manager_no) manager_Email,SAP_Name from tele.dbo.entity_details emp where Employee_Number='" + txtEmpno.Text + "'"
    '        Dim dt As DataTable
    '        dt = cls.getdata(sql)
    '        If dt.Rows.Count > 0 Then
    '            subject = "USB Unblock request " + requestID + " - " + Action + ""
    '            toEmail = dt.Rows(0)(0).ToString
    '            ccEmail = dt.Rows(0)(1).ToString
    '            mailbody = getMailBody_RejectRevertCancel(requestID, Action)
    '            Dim mail As New SendMail
    '            mail.sendEMail(mailbody, toEmail, ccEmail, subject, "")
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Function getMailBody_RejectRevertCancel(ByVal requestID As String, ByVal Action As String) As String
    '    Dim strMailBody As StringBuilder = New StringBuilder()
    '    Dim File As String
    '    File = Server.MapPath("../MailBody/RevertRejectCancel.html")
    '    Dim sr As StreamReader
    '    Dim fi As New FileInfo(File)
    '    If System.IO.File.Exists(File) Then
    '        sr = System.IO.File.OpenText(File)
    '        strMailBody.Append(sr.ReadToEnd())
    '        sr.Close()
    '    End If
    '    strMailBody = strMailBody.Replace("<!--Remarks-->", txtRemarks.Text)
    '    strMailBody = strMailBody.Replace("<!--Action-->", Action)
    '    strMailBody = strMailBody.Replace("<!--RequestNo-->", requestID)
    '    strMailBody = strMailBody.Replace("<!--RaisedBy-->", txtEmpName.Text)
    '    strMailBody = strMailBody.Replace("<!--RaisedOn-->", Date.Today.ToString("dd MMM yyyy"))
    '    strMailBody = strMailBody.Replace("<!--Reason-->", txtJustification.Text)

    '    Dim By_Name As String = webSrv.getEmpidNameDeptDesg(Session("userID")).Split("#")(1)
    '    strMailBody = strMailBody.Replace("<!--Employee-->", By_Name + " (" + Session("EmpID") + ")")

    '    Return strMailBody.ToString
    'End Function






    'Protected Sub txtExpectedStartDate_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        If txtExpectedStartDate.Text = "" Then
    '            txtExpectedEndDate.Text = ""
    '        Else
    '            ' txtExpectedEndDate.Enabled = True
    '            txtExpectedEndDate.Text = CType(txtExpectedStartDate.Text, Date).AddDays(2).ToString("yyyy-MM-dd")
    '            'txtExpectedEndDate.Enabled = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnAccept_Click(sender As Object, e As EventArgs)
    '    Try
    '        process_Request("Approved")
    '        Dim toEmpno As String = ""
    '        Dim Stage As String = ""
    '        Dim status As String = "A"
    '        If Not Request.QueryString("Approve") Is Nothing Then
    '            If Request.QueryString("Approve") = "RM" Then
    '                toEmpno = "92475"
    '                Stage = "20"
    '                sendmail_RequestAprrovedByL1(ViewState("RequestID").ToString)
    '            End If
    '            If Request.QueryString("Approve") = "CS_HOG" Then
    '                toEmpno = "IT"
    '                Stage = "30"
    '                sendmail_AllApproved(ViewState("RequestID").ToString)
    '            End If
    '        End If
    '        Insert_NextApproval_History(ViewState("RequestID"), Session("EmpID"), toEmpno, txtRemarks.Text, Stage, status)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub process_Request(ByVal Action As String)
    '    Try
    '        If Request.QueryString("ID") Is Nothing Then
    '            Exit Sub
    '        End If

    '        'If Request.QueryString("Approve") Is Nothing Then
    '        '    Exit Sub
    '        'End If

    '        Dim level As String = ""
    '        Dim stage As String = ""

    '        If Not Request.QueryString("Approve") Is Nothing Then
    '            If Request.QueryString("Approve") = "RM" Then
    '                level = "RM"
    '                stage = "20"
    '            End If
    '            If Request.QueryString("Approve") = "CS_HOG" Then
    '                level = "CS_HOG"
    '                stage = "30"
    '            End If
    '        Else
    '            level = "Self"
    '            stage = "10"
    '            txtRemarks.Text = txtJustification.Text
    '        End If
    '        If txtRemarks.Text.Trim = "" Then
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Remarks!');", True)
    '            txtRemarks.Focus()
    '            Exit Sub
    '        End If

    '        Dim resultUpdateMail As Boolean = False
    '        resultUpdateMail = updateFinalStage(ViewState("RequestID"), stage, Action, level)

    '        If resultUpdateMail Then
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request " + Action + " Successfully!');", True)
    '            DisplayData(ViewState("RequestID"))
    '            btnAccept.Visible = False
    '            btnCancel.Visible = False
    '            btnRevert.Visible = False
    '            txtRemarks.Enabled = False
    '        Else
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request could not be " + Action + "!');", True)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
    '    Try
    '        process_Request("Cancelled")
    '        Dim toEmpno As String = ""
    '        Dim Stage As String = ""
    '        Dim status As String = "C"
    '        If Not Request.QueryString("Approve") Is Nothing Then
    '            If Request.QueryString("Approve") = "RM" Then
    '                toEmpno = Session("EmpID")
    '                Stage = "20"
    '            End If
    '            If Request.QueryString("Approve") = "CS_HOG" Then
    '                toEmpno = Session("EmpID")
    '                Stage = "30"
    '            End If
    '        Else
    '            toEmpno = Session("EmpID")
    '            Stage = "10"
    '            txtRemarks.Text = txtJustification.Text
    '        End If
    '        Insert_NextApproval_History(ViewState("RequestID"), Session("EmpID"), toEmpno, txtRemarks.Text, Stage, status)
    '        ControlVisibility()
    '        btnCancelSelf.Visible = False
    '        sendmail_RejectRevertCancel(ViewState("RequestID"), "Cancelled")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnRevert_Click(sender As Object, e As EventArgs)
    '    Try
    '        'If validation() = False Then
    '        '    Exit Sub
    '        'End If
    '        If txtRemarks.Text.Trim = "" Then
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Remarks!');", True)
    '            txtJustification.Focus()
    '            Exit Sub
    '        End If
    '        Dim strArray As New ArrayList
    '        sql = ""
    '        sql = " UPDATE [dbo].[USB_tblRequestMaster]"
    '        sql += "    SET "
    '        sql += "       [Status] = 'R'"
    '        sql += "       ,[stage] = '5'"
    '        sql += "       ,[UpdtOn] = GETDATE()"
    '        sql += "       ,[UpdtBy] = '" + Session("EmpID").ToString + "'"
    '        sql += "  WHERE RequestID ='" + ViewState("RequestID") + "'"
    '        strArray.Add(sql)


    '        Dim stage As String = ""
    '        If Not (Request.QueryString("Approve") Is Nothing) Then
    '            If Request.QueryString("Approve") = "RM" Then
    '                stage = "20"
    '            End If
    '            If Request.QueryString("Approve") = "CS_HOG" Then
    '                stage = "30"
    '            End If
    '        End If

    '        sql = ""
    '        sql = " update usb_tblnextapproval set status='R',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate(), remarks='" + txtRemarks.Text.Trim.Replace("'", "''") + "', insOn=getdate()  where stage='" + stage + "' and active=1 and RequestID='" + ViewState("RequestID") + "'"
    '        strArray.Add(sql)

    '        sql = ""
    '        sql = " update usb_tblnextapproval set status='P',UpdtBy='" + Session("EmpID").ToString + "',updtOn=getdate()  where stage='10' and active=1 and RequestID='" + ViewState("RequestID") + "'"
    '        strArray.Add(sql)


    '        Dim result As String = cls.executedata_batchmode(strArray)
    '        If result > 0 Then
    '            Dim toEmpno As String = ""
    '            Dim Stage1 As String = ""
    '            Dim status As String = "R"
    '            If Not Request.QueryString("Approve") Is Nothing Then
    '                If Request.QueryString("Approve") = "RM" Then
    '                    toEmpno = GetRequestInitiatorEmpNo(ViewState("RequestID"))
    '                    stage = "20"
    '                End If
    '                If Request.QueryString("Approve") = "CS_HOG" Then
    '                    toEmpno = GetRequestInitiatorEmpNo(ViewState("RequestID"))
    '                    stage = "30"
    '                End If
    '            End If
    '            Insert_NextApproval_History(ViewState("RequestID"), Session("EmpID"), toEmpno, txtRemarks.Text, Stage1, status)


    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Request Reverted Sucessfully!');", True)
    '            DisplayData(ViewState("RequestID"))
    '            btnAccept.Visible = False
    '            btnCancel.Visible = False
    '            btnRevert.Visible = False
    '            txtRemarks.Enabled = False
    '            sendmail_RejectRevertCancel(ViewState("RequestID"), "Reverted")
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub





End Class
