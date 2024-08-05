<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee1.aspx.vb" Inherits="Login" %>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Employee Form</title>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body {
            background-image: url('/Images/employee form photo.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }

        .custom-form {
            background: rgba(255, 255, 255, 0.9);
            max-width: 900px;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        }

        .btn-submit {
            background-color: pink;
            border-color: pink;
            color: white;
        }

            .btn-submit:hover {
                background-color: #ff69b4; /* Darker shade of pink for hover effect */
                border-color: #ff69b4;
            }
    </style>
    <script type="text/javascript">
        function confirmDelete() {
            return confirm('Are you sure you want to delete this record?');
        }

        function confirmEdit() {
            return confirm('Are you sure you want to edit this record?');
        }
    </script>

</head>

<body>
    <div class="container">
        <div class="custom-form">
            <h2 class="text-center">Employee Form</h2>
            <!-- Personal Information -->
            <form runat="server" id="form1">
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="employee_id">Employee ID</label>
                        <asp:TextBox runat="server" ID="txtempId" class="form-control" placeholder="Enter Employee ID"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="firstName">First Name</label>
                        <asp:TextBox runat="server" ID="txtfirstName" class="form-control" placeholder="Enter First Name"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="lastName">Last Name</label>
                        <asp:TextBox runat="server" ID="txtlastName" class="form-control" placeholder="Enter Last Name"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="dob">Date of Birth </label>
                        <asp:TextBox runat="server" ID="txtdob" class="form-control" placeholder="Enter Date of Birth DD/MM/YYYY" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="gender">Gender</label>
                        <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control">
                            <asp:ListItem Value="">Select</asp:ListItem>
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            <asp:ListItem Text="Not to say" Value="Not to say"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="contact">Contact Number</label>
                        <asp:TextBox runat="server" ID="txtcontact" class="form-control" placeholder="Enter Contact Number"></asp:TextBox>
                    </div>
                </div>

                <!-- Contact Information Section -->
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="email">Email</label>
                        <asp:TextBox runat="server" ID="txtEmail" class="form-control" placeholder="Enter Email"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="pemail">Personal Email</label>
                        <asp:TextBox runat="server" ID="txtpemail" class="form-control" placeholder="Enter Personal Email"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <label for="address">Address</label>
                        <asp:TextBox runat="server" ID="txtaddress" class="form-control" placeholder="Enter Address"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="state">State</label>
                        <asp:TextBox runat="server" ID="txtstate" class="form-control" placeholder="Enter State"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="city">City</label>
                        <asp:TextBox runat="server" ID="txtcity" class="form-control" placeholder="Enter City"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="zip">Zip</label>
                        <asp:TextBox runat="server" ID="txtzip" class="form-control" placeholder="Enter Zip Code"></asp:TextBox>
                    </div>
                </div>

                <!-- Job Details Section-->
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="position">Position</label>
                        <asp:TextBox runat="server" ID="txtposition" class="form-control" placeholder="Enter Position"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <label for="dateofJoining">Date of Joining</label>
                        <asp:TextBox runat="server" ID="txtdate" class="form-control" placeholder="Enter Date of Joining" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="department">Department</label>
                        <asp:TextBox runat="server" ID="txtdept" class="form-control" placeholder="Enter Department"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="salary">Salary</label>
                        <asp:TextBox runat="server" ID="txtsalary" class="form-control" placeholder="Enter Salary"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="officeAddress">Office Address</label>
                        <asp:TextBox runat="server" ID="txtofficeAdd" class="form-control" placeholder="Enter Office Address"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="exp">Year of Experience</label>
                        <asp:TextBox runat="server" ID="txtexp" class="form-control" placeholder="Enter Year of Experience "></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="manager">Manager Name</label>
                        <asp:TextBox runat="server" ID="txtmanager" class="form-control" placeholder="Enter Manager Name"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <label for="assistant">Assistant Name</label>
                        <asp:TextBox runat="server" ID="txtassistant" class="form-control" placeholder="Enter Assistant Name"></asp:TextBox>
                    </div>

                </div>
                <div class="form-group">
                    <label for="expertise">Expertise</label>
                    <asp:TextBox runat="server" ID="txtexpertise" class="form-control" TextMode="MultiLine" Rows="4" Columns="50" placeholder="Enter Expertise"></asp:TextBox>
                </div>

                <asp:HiddenField ID="hfEmployeeID" runat="server" />
                <asp:Button runat="server" Text="Submit" CssClass="btn btn-submit" ID="btnSubmit" />



                <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowEditing="gvEmployees_RowEditing" OnRowUpdating="gvEmployees_RowUpdating" OnRowCancelingEdit="gvEmployees_RowCancelingEdit" DataKeyNames="EmployeeID" OnRowDeleting="gvEmployees_RowDeleting">
                    <Columns>


                        <asp:TemplateField HeaderText="Employee ID">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtempId" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of Birth">
                            <ItemTemplate>
                                <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("DOB", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDOB" runat="server" Text='<%# Bind("DOB", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Contact">
                            <ItemTemplate>
                                <asp:Label ID="lblContact" runat="server" Text='<%# Bind("Contact") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtcontact" runat="server" Text='<%# Bind("Contact") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirmEdit();" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirmDelete();" />
                            </ItemTemplate>
                        </asp:TemplateField>




                    </Columns>
                </asp:GridView>


            </form>
        </div>
    </div>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>

</html>
