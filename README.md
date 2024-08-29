# Architecture

This project is not a fully functional project, only implement the features of assignment, lack of some important parts, like authentication, authorization, UI

## Extensions

The extension for the  start up of program, we currently only defined the user extension for configuration of user service.
For a real product, should contains more extension like log, graceful shutdown, monitor

## Controllers

the web layer, only contains the web related logic, like api implementation and signin

## Services

the business layer, the actually implemenation, mainly interacting with the database

## Database

the db layer, define the context and db model

## Model

the model for service and web layer

# How to run

config the project properly in the appsettings.Development.json file
run the src\Chat.Server

# User Management

### Register

```
POST https://127.0.0.1:7291/api/users/register

{
    "name": "User Name",
    "email": "Email",
    "password": "Password"
}
```

Password requirement: at least 8 length, must contains the upper case letter, lower case letter, digit, non alpha numeric letter

### Login

```
POST https://127.0.0.1:7291/api/users/login

{
    "email": "email",
    "password": "password"
}
```

# Database Migration

dotnet ef migrations add InitialCreate

dotnet ef database update