<%@ Page Title="" Language="VB" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="false" CodeFile="EditRequestBin.aspx.vb" Inherits="WebForms_SparkHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .roundbtn {
            border-radius: 50%;
            height: 20px;
            width: 20px;
        }

        .float {
            position: fixed;
            width: 156px;
            /*bottom:140px;*/
            right: 3px;
            z-index: 500;
            text-align: center;
            padding-left: 1px;
            padding-right: 1px;
        }

        caption {
            padding-top: 0px;
            padding-bottom: 0px;
            text-align: center;
            caption-side: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>

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
                        <div class="row  align-items-center justify-content-center">
                            <div class="col-lg-12">
                                <div class="col-lg-12">
                                    <div class="card bg-secondary shadow border-0">
                                        <div class="card-body px-lg-3 py-lg-3">
                                            <div class="text-center text-muted mb-4">
                                                <div class="row">
                                                    <div class="col-lg-12">

                                                    <div class="col-lg-12 table-info text-info text-center p-2">
                                                        <span>Open/In-Progress Requests</span>
                                                        <asp:LinkButton runat="server" ID="btnExportPending" Style="font-size: 20px" class="fa fa-download text-info " ToolTip="Export to Excel"></asp:LinkButton>
                                                    </div>
                                                </div>
                                                </div>
                                                
                                                <asp:Panel runat="server" Style="overflow-x: scroll; max-height: 250px">
                                                    <asp:GridView runat="server" ID="grv_pending" EmptyDataText="No Data" BorderStyle="None" BorderWidth="1px" EnableModelValidation="True" ShowHeader="true" CssClass="table table-sm text-center caption " AutoGenerateColumns="true" GridLines="None" Style="font-size: 15px;">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="View/Edit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lbntView" CssClass="btn btn-info  btn-sm p-0  roundbtn">
                                                                <i class="fa fa-arrow-right  "></i>
                                                                    </asp:LinkButton>
                                                                    <%--<asp:HiddenField runat="server" ID="hf_StageID" Value='<%#Eval("Stage1")%>'/>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="text-center" />
                                                            </asp:TemplateField>

                                                            <%--<asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                                                            <asp:BoundField DataField="Hostname" HeaderText="Host Name" />
                                                            <asp:BoundField DataField="EMpno" HeaderText="Employee No." />
                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                            <asp:BoundField DataField="Department" HeaderText="Department" />
                                                            <asp:BoundField DataField="RMNAme" HeaderText="RM Name" />
                                                            <asp:BoundField DataField="expstartDt" HeaderText="Expected Start Date" />
                                                            <asp:BoundField DataField="ExpEndDt" HeaderText="Expected End Date" />
                                                            <asp:BoundField DataField="Stage" HeaderText="Stage" />
                                                            <asp:BoundField DataField="RequestCreationDate" HeaderText="Request Creation Date" />
                                                            <asp:BoundField DataField="Status" HeaderText="Status" />

                                                            <asp:BoundField DataField="Previous Details" HeaderText="Previous Details" HtmlEncode ="false"  />--%>
                                                        </Columns>

                                                        <FooterStyle />

                                                    </asp:GridView>
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
        <%--  <div class="separator separator-bottom separator-skew zindex-100">
                <svg x="0" y="0" viewBox="0 0 2560 100" preserveAspectRatio="none" version="1.1" xmlns="http://www.w3.org/2000/svg">
                    <polygon class="fill-white" points="2560 0 2560 100 0 100"></polygon>
                </svg>
            </div>--%>
    </div>

</asp:Content>

