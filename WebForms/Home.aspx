<%@ Page Title="" Language="VB"  AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="WebForms_Home" %>



<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Home Page</title>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body {
            background-color: #f8f9fa;
        }

        .container {
            margin-top: 50px;
        }

        .card {
            margin-bottom: 20px;
        }
    </style>
</head>

<body>
    <div class="container">
        <div class="jumbotron text-center">
            <h1 class="display-4">Welcome to the Employee Management System</h1>
            <p class="lead">This is a simple web application to manage employee data.</p>
            <hr class="my-4">
            <p>Use the links below to navigate through the system.</p>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body text-center">
                        <h5 class="card-title">Employee Form</h5>
                        <p class="card-text">Add, edit, and view employee details.</p>
                        <a href="Employee1.aspx" class="btn btn-primary">Go to Employee Form</a>
                    </div>
                </div>
            </div>
            
            </div>
        </div>
    

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>

</html>
