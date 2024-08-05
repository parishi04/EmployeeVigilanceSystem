<%@ Page Title="" Language="VB" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="false" CodeFile="UserMaster.aspx.vb" Inherits="WebForms_SparkHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 14px;
            color: #000;
        }

        textbox {
            font-size: 12px !important;
        }

        td {
            padding: 0px !important;
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
                        <div class="row   align-items-center justify-content-center">
                            <div class="col-lg-12">


                                <div class="col-lg-12">
                                    <div class="card shadow border-0">
                                        <div class="card-body px-lg-3 py-lg-3" style="padding: 0 !important">
                                            <div class="text-center text-muted mb-4">
                                                <%-- <div class="row">
                                                    <div class="col-lg-12 table-warning text-warning text-center"><span>PENDING AT MY LEVEL</span></div>
                                                </div>--%>



                                                 <div class="container-fluid breadcrumb bg-white" >
                                                         <div class="col-lg-12 mt-2">
                                                        <div class="col-lg-12 table-success text-success text-center p-2">
                                                            User Master
                                                        </div>
                                                    </div>
                  
                  <div class="row col-lg-12" style="padding:1%;">
                      <div class="col-lg-1 text-right">
                          <label>User</label>
                      </div>
                       <div class="col-lg-4 text-left">
                            <asp:Dropdownlist ID="ddl_user" runat="server" class="form-control form-control-sm" AutoPostBack="false"></asp:Dropdownlist>
                          <%-- <asp:textbox ID="txt_userid" runat="server" class="form-control form-control-sm" AutoPostBack="false" ></asp:textbox>--%>
                      </div>
                       <div class="col-lg-1 text-right">
                          <label>Role</label>
                      </div>
                       <div class="col-lg-2 text-left">
                            <asp:Dropdownlist ID="ddl_role" runat="server" class="form-control form-control-sm" AutoPostBack="false"></asp:Dropdownlist>
                      </div>
                       <div class="col-lg-1 text-right">
                           <label>Active</label>
                      </div>
                  
                       <div class="col-lg-2 text-left">
                            <asp:DropdownList ID="ddl_active" runat="server" class="form-control form-control-sm" AutoPostBack="false"></asp:DropdownList>
                      </div>
                       <div class="col-lg-1">
                           <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Add" AutoPostBack="false"/>
                      </div>
                  </div>

                   <div class="col-lg-12 text-center">
                <asp:Label ID="lbl_Msg" runat="server" Visible="false" CssClass="col-form-label font-weight-bold"></asp:Label>
          </div> 
         
                   <div class="container-fluid breadcrumb" id="div_grid" runat="server" style="margin-left:20%;margin-right:20%;">
            <div class="breadcrumb-item" style="width: 100%; overflow-x: scroll; overflow-y: scroll; max-height:250px   ;">
                <div class="form-group">

                     <asp:GridView ID="Grid_User" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false"
                        GridLines="Both"
                        CssClass="table table-striped table-hover text-center table-sm" EmptyDataText="No record found." Width="100%" style="text-align:center" Font-Size="14px">
                           <HeaderStyle cssclass="text-center"/>
                           <Columns>
                                <asp:BoundField HeaderText="Employee No" DataField="empno" ControlStyle-Font-Bold="true" />
                              <asp:BoundField HeaderText="Name" DataField="display_name" ControlStyle-Font-Bold="true" />
                                  
                                <asp:BoundField HeaderText="Role" DataField="Role" ControlStyle-Font-Bold="true" />
                                <asp:BoundField HeaderText="Active" DataField="Active" ControlStyle-Font-Bold="true" />
                                <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBtn_edit" runat="server" CommandName="Editcmd" CssClass="btn btn-link text-success">Edit</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBtn_delete" runat="server" CommandName="Deletecmd" CssClass="btn btn-link">Delete</asp:LinkButton>
                                                 <asp:HiddenField ID="sno" runat="server" Value='<%#Eval("sno") %>' />
                                            </ItemTemplate>
                                   
                                        </asp:TemplateField>
                           </Columns> 
                                                   
                       </asp:GridView>
                    </div> 
                </div> 
                       </div> 

            
                  </div>  



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

