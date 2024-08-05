<%@ Page Title="" Language="VB" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="false" CodeFile="CreateRequest.aspx.vb" Inherits="WebForms_QCDetails" MaintainScrollPositionOnPostback="true"  %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Ajax.Net" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table.test tr td label {
            margin-left: 7px;
        }

        .float {
            position: fixed;
            width: 150px;
            /*bottom:140px;*/
            right: 2px;
            z-index: 1000;
            text-align: center;
        }
    </style>
    <%--   <script type="text/javascript">
        debugger
        document.getElementById("lbtnBack").onclick = function () {
            location.href = "www.yoursite.com";
        };
</script>--%>
    <script>
        function CheckLength(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);

                alert(" Only " + long + " chars are acceptable");

            }
        }

    </script>
    <style>
        label {
            font-size: 12px;
            color: #000;
        }

        textbox {
            font-size: 12px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <asp:ScriptManager runat="server" ID="sp1"></asp:ScriptManager>--%>
    <asp:HiddenField ID="TabName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfStage" runat="server" />



    <div class="wrapper">
        <div class="section section-shaped section-lg">
           

            
            <div class="shape shape-style-1 bg-gradient-default">
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
        </div>

            <div class="page-header">

                <div class="col-lg-12 text-rights">
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success  float" Text="Save" Style="bottom:100px" Visible="true"  />

                   <%-- <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-success " Text="Submit" Visible="true" />

                    <asp:Button runat="server" ID="btnCancelSelf" CssClass="btn btn-danger " Text="Cancel Request" Style="width: 200px" Visible="false" />--%>
                </div>


                <div class="container-fluid shape-container d-flex align-items-center py-lg-2">
                    <div class="col px-0">
                        <div class="row align-items-center justify-content-center">
                            <div class="col-lg-12">

                                <div class="card bg-secondary shadow border-0">
                                    <div class="card-body px-lg-3 py-lg-2">
                                        <div class="text-center text-muted mb-4">
                                            <%--  <div class="col-lg-12 form-inline row">
                                                    <div class="col-lg-8"></div>
                                                    <div class="text-center">
                                                        <small><span style="color: red">*</span>Fields are mandatory to save as draft
                                                        </small>
                                                    </div>
                                                </div>    --%>

                                            <div class="row">
                                                <div class="col-lg-6 text-left">
                                                    <h5><span>
                                                        <asp:Label runat="server" ID="lblRequestNo" CssClass="btn btn-sm btn-fab  btn-round  btn-lg btn-info" Style="cursor: default!important; font-size: 13px"></asp:Label></span></h5>
                                                </div>
                                                <div class="col-lg-6 text-right">
                                                   <small><span style="color: red">*</span>Fields are mandatory to close the request
                                                        </small>
                                                </div>
                                            </div>

                                            <div id="main" class="text-left">
                                                <div class="row">
                                                    <div class="col-lg-1">
                                                        <label>Registration Date<span style="color: red">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox runat="server" ID="txtRegistrationDate" CssClass="form-control  form-control-sm" TextMode="Date"     dateFormat="dd-MM-yyyy"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-1">
                                                        <label>Complaint Source<span style="color: red">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox runat="server" ID="txtComplaintSource" CssClass="form-control  form-control-sm" placeholder="500 characters allowed"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-1">
                                                        <label>Assignment Date<span style="color: red">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox runat="server" ID="txtAssignmentDate" CssClass="form-control  form-control-sm" TextMode="Date" dateFormat="dd-MM-yyyy"></asp:TextBox>
                                                    </div>


                                                    <div class="col-lg-1">
                                                        <label>Group suspectedly involved<span style="color: red">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox runat="server" ID="txtGrp" CssClass="form-control  form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-1">
                                                        <label>Investigation Officer<span style="color: red">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:DropDownList runat="server" ID="ddlIO" CssClass="form-control  form-control-sm"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-lg-1">
                                                        <label>Complaint Gist<span style="color: red">*</span></label>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <%--<asp:TextBox runat="server" ID="txtComplaintGist" CssClass="form-control  form-control-sm" TextMode="MultiLine" Rows="3" placeholder="Maximum 5000 Characters Allowed"></asp:TextBox>--%>
                                                        <FTB:FreeTextBox ID="txtComplaintGist" EnableHtmlMode="false"  SupportFolder="bin/" ToolbarLayout="paragraphmenu,fontsizesmenu;bold,italic,underline,
                                             bulletedlist,numberedlist,  FontFacesMenu, FontSizesMenu, FontForeColorsMenu|; 
                                             FontForeColorPicker, FontBackColorsMenu; FontBackColorPicker, Bold, Italic, Underline;|
                                             Strikethrough, Superscript, Subscript, InsertImageFromGallery, CreateLink, Unlink, 
                                             RemoveFormat, JustifyLeft, JustifyRight, JustifyCenter, JustifyFull, BulletedList, 
                                             NumberedList, Indent, Outdent, Cut, Copy;| Paste, Delete, Undo, Redo, Print, Save, 
                                             ieSpellCheck, StyleMenu, SymbolsMenu,  InsertRule, InsertDate, 
                                             InsertTime, WordClean, InsertImage; InsertTable, EditTable, InsertTableRowBefore, 
                                             InsertTableRowAfter, DeleteTableRow, InsertTableColumnBefore, InsertTableColumnAfter, 
                                             DeleteTableColumn, InsertForm, InsertForm;| InsertTextBox, InsertTextArea, 
                                             InsertRadioButton, InsertCheckBox, InsertDropDownList, InsertButton, InsertDiv, 
                                             InsertImageFromGallery, Preview, SelectAll, EditStyle"
                                    runat="Server" Width="100%" Height="170px"
                                    ImageGalleryUrl="ftb.imagegallery.aspx?rif=/imagespath/&cif=~/images/">
                                </FTB:FreeTextBox>
                                                    </div>
                                                </div>

                                                <asp:Panel runat="server" ID="pnl_edit" Visible="false" CssClass="mt-2">

                                                    <div class="row">
                                                        <div class="col-lg-1">
                                                            <label>Date of Completion of Investigation<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox runat="server" ID="txtCompletionDate" CssClass="form-control  form-control-sm" TextMode="Date" dateFormat="dd-MM-yyyy"></asp:TextBox>

                                                        </div>
                                                        <div class="col-lg-1">
                                                            <label>Findings & recommendations of investigation<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-5">
                                                            <asp:TextBox runat="server" ID="txtFindings" CssClass="form-control  form-control-sm" TextMode="MultiLine" placeholder="Maximum 5000 Characters Allowed" Rows="3"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-1">
                                                            <label>Date of submission for approval<span style="color: red">*</span></label>
                                                        </div>

                                                        <div class="col-lg-2">
                                                            <asp:TextBox runat="server" ID="txtSubmissionDate" CssClass="form-control  form-control-sm" TextMode="Date"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <div class="row mt-2">
                                                        <div class="col-lg-1">
                                                            <label>Approval Date<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox runat="server" ID="txtApprovalDate" CssClass="form-control  form-control-sm" TextMode="Date"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-1">
                                                            <label>ATR on the remarks of CEO<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-5">
                                                            <asp:TextBox runat="server" ID="txtAtr" CssClass="form-control  form-control-sm" TextMode="MultiLine" Rows="3" placeholder="Maximum 5000 Characters Allowed"></asp:TextBox>
                                                        </div>

                                                        <div class="col-lg-1">
                                                            <label>ATR Initiation Date by concerned dept<span style="color: red">*</span></label>
                                                        </div>


                                                        <div class="col-lg-2">
                                                            <asp:TextBox runat="server" ID="txtATRInitiationDate" CssClass="form-control  form-control-sm" TextMode="Date"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-1">
                                                            <label>Complaint Closing Date<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox runat="server" ID="txtComplaintClosingDate" CssClass="form-control  form-control-sm" TextMode="Date"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-1">
                                                            <label>Remarks<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-5">
                                                            <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control  form-control-sm" TextMode="MultiLine" placeholder="Maximum 5000 Characters Allowed" Rows="3"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-1">
                                                            <label>Status<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control  form-control-sm"></asp:DropDownList>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                          <div class="col-lg-1">
                                                            <label>Charges Substantiated<span style="color: red">*</span></label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:DropDownList runat="server" ID="ddlChargesSubstantiated" CssClass="form-control  form-control-sm">
                                                                <asp:ListItem Value="" >Select</asp:ListItem>
                                                                 <asp:ListItem Value="Yes" >Yes</asp:ListItem>
                                                                 <asp:ListItem Value="No" >No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div id="attachment">
                                                        <asp:Panel runat="server" ID="panel3">
                                                            <%-- <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                                                                <ContentTemplate>--%>
                                                            <div class="row col-lg-12 mt-3">
                                                                <small><%--<span style="color: red">**</span>--%>Choose the file and Click on Add Button(+). File Formats Allowed - jpg,png,jpeg,pdf. Maximum file size - 5 mb.
                                                                </small>
                                                            </div>
                                                            <div class="row col-lg-12 mt-1">
                                                                <label class="col-lg-2">Choose Attachment</label>
                                                                <asp:FileUpload runat="server" ID="fileupload1" CssClass="col-lg-4" />
                                                                <div class="col-lg-2 form-inline">
                                                                    <asp:LinkButton runat="server" ID="btnAddAttachment" CssClass="btn btn-info btn-sm "> <i class="fa fa-plus"></i></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnRefreshAttachment" CssClass="btn btn-warning btn-sm"> <i class="fa fa-refresh"></i></asp:LinkButton>
                                                                </div>

                                                            </div>

                                                            <div class="row mt-2">
                                                                <div class="col-lg-2"></div>
                                                                <div class="col-lg-10">
                                                                    <asp:Panel runat="server" Style="overflow-x: scroll">
                                                                        <asp:GridView runat="server" ID="grv_Attachment" EmptyDataText="No Attachments Added" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ShowHeader="true" CssClass="border border-light " AutoGenerateColumns="false" Style="font-size: 12px; font-weight: bold; font-style: italic">
                                                                            <Columns>
                                                                               <%-- <asp:BoundField DataField="Column1" HeaderText="Sno" Visible="false"  />--%>
                                                                                <asp:BoundField DataField="Name" HeaderText="Attachment" />


                                                                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField runat="server" ID="hf_ID" Value='<%#Eval("ID")%>'/>
                                                                                        <asp:LinkButton ID="lnkView" runat="server" CommandName="cmdView1" OnClientClick="FinishButtonClick(this);" Text="&#8595;" CssClass="badge badge-inf bg-info badge-pill font-weight-bold text-white font-weight-bolder " Style='font-size: 10px;' ></asp:LinkButton>

                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkdelete" runat="server" CommandName="DeleteRow1" OnClientClick="FinishButtonClick(this);" Text="X" CssClass="badge badge-danger bg-danger badge-pill font-weight-bold"></asp:LinkButton>

                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                          <%--   </ContentTemplate>
                                                            </asp:UpdatePanel>--%>
                                                        </asp:Panel>
                                                    </div>

                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                              <%--  <asp:Panel runat="server" ID="pnlApproval" CssClass="mt-3" ClientIDMode="Static" Visible="false">
                                    <div class="card bg-secondary shadow border-0 p-4">
                                        <div class="row form-inline">
                                            <div class="col-lg-6">
                                              
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Button runat="server" ID="btnAccept" CssClass="btn btn-sm btn-success col-lg-3" Text="Accept Request" />
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-sm btn-danger col-lg-3" Text="Cancel Request" />
                                                <asp:Button runat="server" ID="btnRevert" CssClass="btn  btn-sm btn-warning col-lg-3" Text="Revert to Initiator" Visible="true" />

                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>--%>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script>

        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        }

        $(document).ready(function () {
            $("#txtEmployees").autocomplete({
                source: function (request, response) {
                    var param = { EmpName: $('#txtEmployees').val() };
                    $.ajax({
                        url: "PrayasDetailsTPDDL.aspx/getEmployees",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            //console.log(JSON.stringify(data));
                            response($.map(data.d, function (item) {
                                // console.log({  Name: item.EmpName });
                                return {
                                    value: item.EmpName
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 1 //This is the Char length of inputTextBox  
            }), { "mustMatch": true };
        })
    </script>
</asp:Content>

