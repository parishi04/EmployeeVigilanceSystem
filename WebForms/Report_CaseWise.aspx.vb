Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports ClosedXML.Excel

Partial Class WebForms_Report_PrayasBA
    Inherits System.Web.UI.Page
    Dim mCls As New DataConnect
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If validation() = False Then
                Exit Sub
            End If
            bindReport()
        Catch ex As Exception
            ErrorLog.WriteError("Report_All", "btnSubmit_Click", ex.Message.ToString)
        End Try
    End Sub
    Private Function validation() As Boolean
        Try

            If txtRequestId.Text.Trim = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Please Enter Reference ID!');", True)
                txtRequestId.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            ErrorLog.WriteError("Report_All", "validation", ex.Message.ToString)
        End Try
    End Function

    Private Sub bindReport()
        Try
            Dim sql As String
            Dim dt As DataTable
            Dim cls As New DataConnect

            sql = "select  ROW_NUMBER() OVER (ORDER BY ID) AS  [Sno], ReferenceNo [Reference No],convert(varchar(12),RequestDate,113) [Registration Date],ComplaintSource [Complaint Source],IOName [Investigating Officer],isnull(ComplaintGist,'') [Complaint Gist] ,convert(varchar(12),AssignDate,113) [Assignment Date],convert(varchar(12),InvestigationCompletionDate ,113) [Investigation Completion Date],isnull(Findings,'')Findings,convert(varchar(12),ApprovalSubmissionDate ,113) [Approval Submission Date], convert(varchar(12),ApprovalDate ,113) [Approval Date], isnull(ATR,'')ATR,convert(varchar(12),ATRInitiationDate  ,113) [ATR Initiation Date], convert(varchar(12),ComplaintClosingDate   ,113) [Complaint Closing Date] ,isnull(Remarks,'')Remarks,datediff(day,RequestDate,ApprovalSubmissionDate) [Turn around time (Days) = Date of Submission for approval (–) Registration Date],(select top 1 Type_Display_text  from tbl_Parameter where  Type_Value =RequestStatus) [Request Status],ChargesSubstantiated [Charges Substantiated], convert(varchar(12),InsOn,113) [Record Inserted On],convert(varchar(17),UpdtOn ,113) [Last Updated On], (select SAP_name from tele.dbo.entity_details where employee_number= UpdtBy) [Updated By]   from tblRequestMaster_log where Active =1 "
            If txtRequestId.Text.Trim <> "" Then
                sql += " and ReferenceNo='" + txtRequestId.Text.Trim + "'"
            End If
            sql += " order by id"

            dt = cls.getdata(sql)
            If dt.Rows.Count > 0 Then
                grv_TPDDL.Columns(5).Visible = True
                grv_TPDDL.Columns(9).Visible = True
                grv_TPDDL.Columns(13).Visible = True
                grv_TPDDL.Columns(17).Visible = True
                grv_TPDDL.DataSource = dt
                grv_TPDDL.DataBind()
                ViewState("DTReport") = dt
                grv_TPDDL.Columns(5).Visible = False
                grv_TPDDL.Columns(9).Visible = False
                grv_TPDDL.Columns(13).Visible = False
                grv_TPDDL.Columns(17).Visible = False

            Else
                grv_TPDDL.DataSource = Nothing
                grv_TPDDL.DataBind()
                ViewState("DTReport") = New DataTable()
            End If

        Catch ex As Exception
            ViewState("DTReport") = New DataTable()
            ErrorLog.WriteError("Report_All", "bindReport", ex.Message.ToString)
            grv_TPDDL.DataSource = Nothing
            grv_TPDDL.DataBind()
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If IsAdmin() = False Then
                    Response.Redirect("Home.aspx")
                End If
                ViewState("DTReport") = New DataTable()
            End If
        Catch ex As Exception
            ErrorLog.WriteError("Report_All", "Page_Load", ex.Message.ToString)
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
                Dim results As DataRow() = dt.Select("role_id = '1' or role_id='3' or role_id='5' or role_id='6'")
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


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If grv_TPDDL.Rows.Count > 0 Then

                'excel()
                ExportToExcel()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Data Not Available!');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grv_TPDDL_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grv_TPDDL.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim ID As String = e.Row.Cells(2).Text
                'Dim grv_Attachment As GridView = TryCast(e.Row.FindControl("grv_Attachment"), GridView)
                'Dim sql As String
                'sql = "select ID, [Name],Data from tblFiles where referenceNo='" + ID + "' and active=1 "
                'Try
                '    'Dim btnDownload As LinkButton = grv_Attachment.Rows

                '    grv_Attachment.DataSource = mCls.getdata(sql)
                '    grv_Attachment.DataBind()
                'Catch ex As Exception

                'End Try
                e.Row.Cells(5).Text = EncryptDecrypt.Decrypt(e.Row.Cells(5).Text)
                e.Row.Cells(9).Text = EncryptDecrypt.Decrypt(e.Row.Cells(9).Text)
                e.Row.Cells(13).Text = EncryptDecrypt.Decrypt(e.Row.Cells(13).Text)
                e.Row.Cells(17).Text = EncryptDecrypt.Decrypt(e.Row.Cells(17).Text)




            End If
        Catch ex As Exception
            ErrorLog.WriteError("Report_All", "grv_TPDDL_RowDataBound", ex.Message.ToString)
        End Try
    End Sub


    Protected Sub ExportToExcel()
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=VigilanceReport_CaseWise.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        'grv_TPDDL.Columns(0).Visible = False
        grv_TPDDL.Columns(6).Visible = False
        grv_TPDDL.Columns(10).Visible = False
        grv_TPDDL.Columns(14).Visible = False
        grv_TPDDL.Columns(18).Visible = False

        grv_TPDDL.Columns(5).Visible = True
        grv_TPDDL.Columns(9).Visible = True
        grv_TPDDL.Columns(13).Visible = True
        grv_TPDDL.Columns(17).Visible = True



        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'grv_TPDDL.AllowPaging = False
            'Me.BindGrid()

            grv_TPDDL.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In grv_TPDDL.HeaderRow.Cells
                cell.BackColor = grv_TPDDL.HeaderStyle.BackColor
                cell.BorderColor = Color.Black
                cell.BorderStyle = BorderStyle.Solid

                cell.BorderWidth = 1
            Next
            For Each row As GridViewRow In grv_TPDDL.Rows
                row.BackColor = Color.White
                row.BorderColor = Color.Black
                row.BorderStyle = BorderStyle.Solid
                row.BorderWidth = 1
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = grv_TPDDL.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = grv_TPDDL.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            grv_TPDDL.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()

        End Using

        grv_TPDDL.Columns(0).Visible = True
        grv_TPDDL.Columns(7).Visible = True
        grv_TPDDL.Columns(11).Visible = True
        grv_TPDDL.Columns(15).Visible = True
        grv_TPDDL.Columns(19).Visible = True

        grv_TPDDL.Columns(6).Visible = False
        grv_TPDDL.Columns(10).Visible = False
        grv_TPDDL.Columns(14).Visible = False
        grv_TPDDL.Columns(18).Visible = False

    End Sub




    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Dim clickedRow As GridViewRow = TryCast((CType(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim gv As GridView = TryCast(clickedRow.NamingContainer, GridView)
        'Dim lbCode As String = gv.DataKeys(clickedRow.RowIndex).Values(2).ToString()

        Dim AttachmentID As String = CType(clickedRow.FindControl("hf_ID"), HiddenField).Value
        DownloadFile(AttachmentID)
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


    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = TryCast(lb.NamingContainer, GridViewRow)
        Dim qst As Label = TryCast(row.FindControl("Label20"), Label)
        lb.Text = If((lb.Text = "Read More..."), "Hide...", "Read More...")
        Dim temp As String = qst.Text
        qst.Text = qst.ToolTip
        qst.ToolTip = temp
    End Sub
    Protected Sub lbtnFindings_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = TryCast(lb.NamingContainer, GridViewRow)
        Dim qst As Label = TryCast(row.FindControl("lblFindings"), Label)
        lb.Text = If((lb.Text = "Read More..."), "Hide...", "Read More...")
        Dim temp As String = qst.Text
        qst.Text = qst.ToolTip
        qst.ToolTip = temp
    End Sub
    Protected Sub lbtnATR_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = TryCast(lb.NamingContainer, GridViewRow)
        Dim qst As Label = TryCast(row.FindControl("lblATR"), Label)
        lb.Text = If((lb.Text = "Read More..."), "Hide...", "Read More...")
        Dim temp As String = qst.Text
        qst.Text = qst.ToolTip
        qst.ToolTip = temp
    End Sub
    Protected Sub lbtnRemarks_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = TryCast(lb.NamingContainer, GridViewRow)
        Dim qst As Label = TryCast(row.FindControl("lblRemarks"), Label)
        lb.Text = If((lb.Text = "Read More..."), "Hide...", "Read More...")
        Dim temp As String = qst.Text
        qst.Text = qst.ToolTip
        qst.ToolTip = temp
    End Sub

    Protected Function SetVisibility(ByVal Desc As Object, ByVal length As Integer) As Boolean
        Return Desc.ToString().Length > length
    End Function







End Class
