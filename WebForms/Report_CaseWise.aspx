﻿<%@ Page Title="" Language="VB" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="false" CodeFile="Report_CaseWise.aspx.vb" Inherits="WebForms_Report_PrayasBA" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Ajax.Net" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <div class="container-fluid shape-container d-flex align-items-center py-lg-5">
                    <div class="col px-0">
                        <div class="row align-items-center justify-content-center">
                            <div class="col-lg-12">
                                <div class="card bg-secondary shadow border-0">

                                    <div class="card-body px-lg-3 py-lg-3">
                                        <div class="text-center text-muted mb-4">
                                            <div class="col-lg-12 text-center row">
                                                <label style="width: 100px">Reference ID</label>
                                                <asp:TextBox runat="server" ID="txtRequestId" CssClass="form-control  form-control-sm" Style="width: 280px" placeholder="For eg. VIG/2022-23/1"></asp:TextBox>
                                                
                                                <asp:LinkButton runat="server" ID="btnSubmit" class="btn btn-primary btn-icon mb-sm-0 ml-5">
                                                    <span class="btn-inner--text">Generate Report</span>
                                                    <span class="btn-inner--icon"><i class="fas fa-arrow-right"></i></span>
                                                </asp:LinkButton>

                                                <asp:LinkButton runat="server" class="btn btn-info btn-icon  mb-sm-0" ID="btnExport">

                                                    <span class="btn-inner--text">Export to Excel</span>
                                                    <span class="btn-inner--icon"><i class="fa fa-download"></i></span>
                                                </asp:LinkButton>
                                            </div>

                                            <br />
                                            <br />
                                            <asp:Panel runat="server" Style="overflow-x: scroll;">
                                                <asp:UpdatePanel runat="server" ID="up1">
                                                    <ContentTemplate>
                                                        <div id="demo_info" class="box"></div>
                                                        <asp:GridView runat="server" ID="grv_TPDDL" EmptyDataText="No Data" BorderStyle="None" BorderWidth="1px" EnableModelValidation="True" ShowHeader="true" CssClass="table table-sm text-center p-0 m-0" AutoGenerateColumns="false" GridLines="both" Style="font-size: 13px;" CellPadding="0" CellSpacing="0">
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="Attachments">
                                                                    <ItemTemplate>
                                                                        <div class="row mt-2">
                                                                            <div class="col-lg-12">
                                                                                <asp:Panel runat="server" Style="overflow-x: scroll">
                                                                                    <asp:GridView runat="server" ID="grv_Attachment" EmptyDataText="No Attachments Added" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ShowHeader="true" CssClass="border border-light " AutoGenerateColumns="false" Style="font-size: 12px; font-weight: bold; font-style: italic">
                                                                                        <Columns>

                                                                                            <asp:BoundField DataField="Name" HeaderText="Attachment" />
                                                                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="left">
                                                                                                <ItemTemplate>
                                                                                                    <asp:HiddenField runat="server" ID="hf_ID" Value='<%#Eval("ID")%>' />
                                                                                                    <asp:LinkButton ID="lnkView" runat="server" CommandName="cmdView1" OnClientClick="FinishButtonClick(this);" Text="&#8595;" CssClass="badge badge-inf bg-info badge-pill font-weight-bold text-white font-weight-bolder " Style='font-size: 10px;' OnClick="lnkView_Click"></asp:LinkButton>

                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="Sno" HeaderText="Sno" />
                                                                <asp:BoundField DataField="Reference No" HeaderText="Reference No" />
                                                                <asp:BoundField DataField="Registration Date" HeaderText="Registration Date" />
                                                                <asp:BoundField DataField="Complaint Source" HeaderText="Complaint Source" />
                                                                <asp:BoundField DataField="Investigating Officer" HeaderText="Investigating Officer" />
                                                                <asp:BoundField DataField="Complaint Gist" HeaderText="Complaint Gist" />
                                                                <asp:TemplateField HeaderText="Complaint Gist" ItemStyle-Width="150px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label20" runat="server" Text='<%# If(EncryptDecrypt.Decrypt((Eval("Complaint Gist"))).Length < 200, EncryptDecrypt.Decrypt(Eval("Complaint Gist")), EncryptDecrypt.Decrypt((Eval("Complaint Gist"))).Substring(0, 200) + "...")%>' ToolTip='<%#EncryptDecrypt.Decrypt(Eval("Complaint Gist")) %>' Width="500"></asp:Label>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" Visible='<%#SetVisibility(EncryptDecrypt.Decrypt(Eval("Complaint Gist")), 200) %>' OnClick="LinkButton1_Click">Read More...</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="Assignment Date" HeaderText="Assignment Date" />
                                                                <asp:BoundField DataField="Investigation Completion Date" HeaderText="Investigation Completion Date" />
                                                                <asp:BoundField DataField="Findings" HeaderText="Findings" />
                                                                <asp:TemplateField HeaderText="Findings">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFindings" runat="server" Text='<%# If(EncryptDecrypt.Decrypt((Eval("Findings"))).Length < 200, EncryptDecrypt.Decrypt(Eval("Findings")), EncryptDecrypt.Decrypt((Eval("Findings"))).Substring(0, 200) + "...")%>' ToolTip='<%#EncryptDecrypt.Decrypt(Eval("Findings")) %>' Width="500"></asp:Label>
                                                                        <asp:LinkButton ID="lbtnFindings" runat="server" Visible='<%#SetVisibility(EncryptDecrypt.Decrypt(Eval("Findings")), 200) %>' OnClick="lbtnFindings_Click">Read More...</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:BoundField DataField="Approval Submission Date" HeaderText="Approval Submission Date" />
                                                                <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" />
                                                                <asp:BoundField DataField="ATR" HeaderText="ATR" />
                                                                <asp:TemplateField HeaderText="ATR">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblATR" runat="server" Text='<%# If(EncryptDecrypt.Decrypt((Eval("ATR"))).Length < 200, EncryptDecrypt.Decrypt(Eval("ATR")), EncryptDecrypt.Decrypt((Eval("ATR"))).Substring(0, 200) + "...")%>' ToolTip='<%#EncryptDecrypt.Decrypt(Eval("ATR")) %>' Width="500"></asp:Label>
                                                                        <asp:LinkButton ID="lbtnATR" runat="server" Visible='<%#SetVisibility(EncryptDecrypt.Decrypt(Eval("ATR")), 200) %>' OnClick="lbtnATR_Click">Read More...</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:BoundField DataField="ATR Initiation Date" HeaderText="ATR Initiation Date" />
                                                                <asp:BoundField DataField="Complaint Closing Date" HeaderText="Complaint Closing Date" />
                                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# If(EncryptDecrypt.Decrypt((Eval("Remarks"))).Length < 200, EncryptDecrypt.Decrypt(Eval("Remarks")), EncryptDecrypt.Decrypt((Eval("Remarks"))).Substring(0, 200) + "...")%>' ToolTip='<%#EncryptDecrypt.Decrypt(Eval("Remarks")) %>' Width="500"></asp:Label>
                                                                        <asp:LinkButton ID="lbtnRemarks" runat="server" Visible='<%#SetVisibility(EncryptDecrypt.Decrypt(Eval("Complaint Gist")), 200) %>' OnClick="lbtnRemarks_Click">Read More...</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:BoundField DataField="Turn around time (Days) = Date of Submission for approval (–) Registration Date" HeaderText="Turn around time (Days) = Date of Submission for approval (–) Registration Date" />
                                                                <asp:BoundField DataField="Request Status" HeaderText="Request Status" />
                                                                <asp:BoundField DataField="Charges Substantiated" HeaderText="Charges Substantiated" />
                                                                <asp:BoundField DataField="Record Inserted On" HeaderText="Record Inserted On" Visible="false"  />
                                                                <asp:BoundField DataField="Last Updated On" HeaderText="Record Updated On" />
                                                                <asp:BoundField DataField="Updated By" HeaderText="Updated By" />







                                                            </Columns>
                                                            <FooterStyle />

                                                        </asp:GridView>

                                                      


                                                    </ContentTemplate>
                                                </asp:UpdatePanel>




                                            </asp:Panel>
                                            <br />

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>

     <%-- <script>
        $(document).ready(function () {
            $('#<%=grv_TPDDL.ClientID%>').prepend($("<thead></thead>").append($("#<%= grv_TPDDL.ClientID%>").find("tr:first"))).DataTable({
             stateSave: true,
             "responsive": true,
             "autoWidth": false,
         });
     });
        </script>--%>
</asp:Content>

