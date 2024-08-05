Imports System.Data

Partial Class WebForms_SparkHome
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If IsAdmin() = False Then
                    Response.Redirect("Home.aspx")
                End If
                If Request.QueryString("M") Is Nothing Then
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Data Saved Successfully!');", True)
                End If
                ViewState("data") = Nothing
                bindRequests()
            End If
        Catch ex As Exception
            ErrorLog.WriteError("EUREKAHome.aspx", "Page_Load", ex.Message.ToString)
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
    Private Sub bindRequests()
        Try
            Dim sql As String = ""
            sql = " select  ROW_NUMBER() OVER (ORDER BY ID) AS  [Sno], ReferenceNo [Reference No],convert(varchar(12),RequestDate,113) [Registration Date],ComplaintSource [Complaint Source],GroupSuspected [Group Suspected],IOName [Investigating Officer] ,convert(varchar(12),AssignDate,113) [Assignment Date], (select top 1 Type_Display_text  from tbl_Parameter where  Type_Value =RequestStatus) [Request Status], convert(varchar(12),InsOn,113) [Record Inserted On],convert(varchar(12),UpdtOn ,113) [Last Updated On], (select SAP_name from tele.dbo.entity_details where employee_number= UpdtBy) [Updated By]   from tblRequestMaster where RequestStatus  in ('10','20') and Active =1 order by id"
            Dim ds As DataSet
            Dim cls As New DataConnect

            ds = cls.GetDataSet(sql)
            If ds.Tables.Count > 0 Then
                Try
                    FillControls.fillGrid(grv_pending, ds.Tables(0))
                Catch ex As Exception
                    FillControls.fillGrid(grv_pending, Nothing)
                End Try
                'Try
                '    FillControls.fillGrid(grv_Initiated, ds.Tables(1))
                'Catch ex As Exception
                '    FillControls.fillGrid(grv_Initiated, Nothing)
                'End Try
                'Try
                '    FillControls.fillGrid(grv_processed, ds.Tables(2))
                'Catch ex As Exception
                '    FillControls.fillGrid(grv_processed, Nothing)
                'End Try
            Else
                FillControls.fillGrid(grv_pending, Nothing)
                'FillControls.fillGrid(grv_Initiated, Nothing)
                'FillControls.fillGrid(grv_processed, Nothing)
            End If

        Catch ex As Exception
            ErrorLog.WriteError("EUREKAHome.aspx", "bindProjects", ex.Message.ToString)
            grv_pending.DataSource = Nothing
            grv_pending.DataBind()
        End Try
    End Sub

    Protected Sub grv_Projects_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grv_pending.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim RequestID As String = e.Row.Cells(2).Text.Trim
                ' Dim stage As String = CType(e.Row.FindControl("hf_StageID"), HiddenField).Value
                Dim lbntView As LinkButton = CType(e.Row.FindControl("lbntView"), LinkButton)
                'If stage = "5" Then
                '    lbntView.PostBackUrl = "~/WebForms/CreateRequest.aspx?ID=" + RequestID + ""
                'Else
                lbntView.PostBackUrl = "~/WebForms/CreateRequest.aspx?ID=" + RequestID
                'End If

            End If
        Catch ex As Exception
            ErrorLog.WriteError("EUREKAHome.aspx", "grv_Projects_RowDataBound", ex.Message.ToString)
        End Try
    End Sub
    Protected Sub btnExportPending_Click(sender As Object, e As EventArgs) Handles btnExportPending.Click
        Try
            If grv_pending.Rows.Count > 0 Then
                ExportToExcel(grv_pending, "Vigilance-Open Requests", "Vigilance-Open Requests")
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Data Not Available!');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ExportToExcel(ByVal MyGridView As GridView, ByVal FileName As String, ByVal HeaderText As String)

        Try
            MyGridView.GridLines = GridLines.Both
            MyGridView.Columns(0).Visible = False
            Response.Clear()
            Response.Buffer = True

            Response.AddHeader("content-disposition", "attachment;filename=" & FileName & ".xls")
            Response.Charset = ""

            Response.ContentType = "application/vnd.xls"
            Dim oStringWriter As New System.IO.StringWriter()
            ''*****************************************************************
            oStringWriter.WriteLine(HeaderText)
            ''*****************************************************************
            Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
            MyGridView.RenderControl(oHtmlTextWriter)



            Response.Output.Write(oStringWriter.ToString())
            Response.Flush()
            Response.End()

        Catch ex As Exception
        Finally
            MyGridView.GridLines = GridLines.None
            MyGridView.Columns(0).Visible = True
        End Try
    End Sub
End Class
