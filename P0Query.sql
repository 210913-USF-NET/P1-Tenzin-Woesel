DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS LineItems;
DROP TABLE IF EXISTS [Order];
DROP TABLE IF EXISTS Customer;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS StoreFront;
CREATE TABLE Customer (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(25),
    Address VARCHAR(25),
    Email VARCHAR(50)
);
CREATE TABLE StoreFront (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(50),
    Address VARCHAR(25)
);
CREATE TABLE [Order] (
    Id INT PRIMARY KEY IDENTITY,
    Total DECIMAL,
    OrderDate DATETIME NOT NULL DEFAULT(GETDATE()),
    CustomerId INT FOREIGN KEY REFERENCES Customer(Id) ON DELETE CASCADE NOT NULL,
    StoreFrontId INT FOREIGN KEY REFERENCES StoreFront(Id) ON DELETE CASCADE NOT NULL
);
CREATE TABLE [Product] (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(25),
    [Price] DECIMAL(6, 2),
    Description VARCHAR(200),
    Category VARCHAR(25)
);
CREATE TABLE Inventory (
    Id INT PRIMARY KEY IDENTITY,
    StoreId INT FOREIGN KEY REFERENCES StoreFront(Id),
    ProductId INT FOREIGN KEY REFERENCES Product(Id),
    Quantity INT,
);
CREATE TABLE LineItems (
    Id INT PRIMARY KEY IDENTITY,
    Quantity INT NOT NULL,
    ProductId INT FOREIGN KEY REFERENCES Product(Id),
    OrderId INT FOREIGN KEY REFERENCES [Order](Id)
);
-- =========================================================
select *
from [Product];
select *
from [Order];
select *
from [StoreFront];
select *
from [Customer];
select *
from [Inventory];
select *
from [LineItems];
INSERT INTO Customer(Name, Address, Email)
VALUES ('Tenzin', '234 Address', 'ten@gmail.com'),
    ('Tsering', '236 New', 'ten@net.com'),
    ('Bhungu', 'main', 'gin@gin.com');
INSERT INTO Product(Name, Price, Description, Category)
VALUES ('Cushions', 129.20, 'Cushion 7x31x72', 'Sofa'),
    (
        'Chair',
        129.20,
        'Head rest and tiltable office chair',
        'Chairs'
    ),
    (
        'Laptop',
        124,
        'Asus Vivobook s15',
        'Electronics'
    );
INSERT INTO StoreFront(Name, Address)
VALUES ('SLS 1', 'Queens'),
    ('SLS 2', 'Brooklyn'),
    ('SLS 3', 'Bronx'),
    ('SLS 4', 'Manhattan');
INSERT INTO Inventory(StoreId, ProductId, Quantity)
VALUES (1, 2, 10),
    (4, 3, 30),
    (3, 1, 6);
INSERT INTO [Order](Total, OrderDate, CustomerId, StoreFrontId)
VALUES (140, GetDate(), 1, 1),
    (200, GetDate(), 2, 4),
    (150, GetDate(), 3, 2),
    (500, GetDate(), 2, 3);
INSERT INTO LineItems(Quantity, ProductId, OrderId)
VALUES (3, 1, 2),
    (1, 2, 3),
    (1, 3, 4);