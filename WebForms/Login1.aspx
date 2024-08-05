<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login1.aspx.vb" Inherits="Login" %>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Employee Login</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">

    <style>
        body {
            background-image: url('/Images/login%201.jpg'); 
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            font-family: 'Arial', sans-serif;
            color: #333;
        }
        .custom-form {
            background: rgba(255, 255, 255, 0.9);
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.3);
            width: 300px;
        }
        .container h2 {
            margin-bottom: 20px;
            font-weight: bold;
        }
        .form-control {
            border-radius: 20px;
        }
        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            border-radius: 100px;
        }
        .btn-primary:hover {
            background-color: #0056b3;
            border-color: #0056b3;
        }
    </style>
</head>

    <body class="login-page" style="overflow-y: hidden;">
   
    
        <div class="container">
        <div class="custom-form">
        <h2 class="text-center">Employee Login</h2>

                            <form role="form" runat="server">
                                <div class="form-group ">
                                    <label for="username">Username</label>
                                    <div class="input-group input-group-alternative">
                                        <div class="input-group-prepend">
                                          
                                        </div>
                                        <asp:TextBox runat="server" ID="txtuser" class="form-control" placeholder="For Eg. 07801032021"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                <div class="form-group focused">
                                    <label for="password">Password</label>
                                    <div class="input-group input-group-alternative">
                                        <div class="input-group-prepend">
                                          
                                        </div>
                                       
                                        <asp:TextBox runat="server" ID="txtpass" class="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                                    </div>

                                </div>

                                
                                <div class="text-center mb-2">
                                    <asp:Button runat="server" ID="btnsubmit" class="btn btn-primary my-6" Text="Login" />

                                </div>
                            </form>
                        </div>
                    
                    <br />
               
        
  


      

     

    <!-- Bootstrap and jQuery -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    
</body>
</html>