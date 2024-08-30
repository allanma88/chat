# Architecture

This project is not a fully functional project, only implement the features of assignment, lack of some important parts, like authentication, authorization, UI.

## Extensions

The extension for the  start up of program, we currently only defined the user and signalr extension.
For a real product, should contains more extension like log, graceful shutdown, monitor

## Controllers

the web layer, only contains the web related logic, like api implementation and signin

## Hubs

the hub layer, only contains the message related logic

## Services

the business layer, the actually implemenation, mainly interacting with the database

## Database

the db layer, define the context and db model

## Model

the model for service and web layer

# How to run

+ config the project properly in the appsettings.Development.json file
+ run `dotnet ef database update` at the `src/Chat.Server` folder to apply the database migration
+ run the src\Chat.Server

# User Management

For demo purpose, I didn't use the Microsoft Identity framework, it's complex and if use the Microsoft Identity framework, there is no need to implement the User register and login API.

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

## Message Management 

+ run Chat.Client
+ run `connect user1@company.com`
+ request message API

```
https://127.0.0.1:7291/api/messages/send

{
    "sender": "service1",
    "recipient": "aa@aa.com",
    "timestamp": "2024-08-30T10:00:00",
    "content": "hello"
}
```

+ the Chat.Client will receive the message

## Basic Real-time Message Pushing

+ run two Chat.Client, let's call them client1 and client2
+ run `connect user1@company.com` in the client1 and `connect user2@company.com` in the client2
+ run `send user2@company.com hello` in the client1, you will receive the message in the client2

