# Web Store App #

## Overview ##
The web store app is a software that helps customers purchase products from your business. Designed with functionality that would make virtual shopping much simpler!

### It's online ###
https://snowlionstore.azurewebsites.net/

### Functionality ###
* Add a new customer
* Display details of an order
* Place orders to store locations for customers
* View order history of customer
* View order history of location
* View location inventory
* The customer should be able to purchase multiple products
* Order histories should have the option to be sorted by date (latest to oldest and vice versa).
* The manager should be able to replenish inventory

### User Stories ###
* As a user, I can use mu username to sign in.
* As a user, I can sign up to create a new account.
* As a user, I can select store and view the list of items available at that location.
* As a user, I can add multiple products to purchase.
* As a user, I can view my profile.
* As a user, I can see my order history.
* As an admin, I can create new locations, new stores, new products and update item inventory. 

### Additional Features ###
* Exception Handling
* Input validation
* Logging
* Unit Tests using xUnit and Moq
* Data are persisted to the database
* Web store is deployed to the azure cloud using Azure

### Tech Stack: ###
* C#
* EF Core
* ASP.NET MVC
* PostgreSQL DB
* Github Actions
* Azure
* xUnit
* Moq
* Serilog
