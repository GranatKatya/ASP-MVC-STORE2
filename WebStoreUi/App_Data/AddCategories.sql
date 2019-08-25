insert into Categories (Name)
values (N'Для тела'), (N'Для лица'), (N'Для волос')

GO

UPDATE Products
SET [CategoryId] = (SELECT Id FROM Categories WHERE Name = N'Для тела')
WHERE ID Between 1 AND 5

UPDATE Products
SET [CategoryId] = (SELECT Id FROM Categories WHERE Name = N'Для лица')
WHERE ID Between 6 AND 10

UPDATE Products
SET [CategoryId] = (SELECT Id FROM Categories WHERE Name = N'Для волос')
WHERE ID Between 11 AND 15




EXEC sp_rename 'OrderItems.ID', 'OrderIWWWtemId', 'COLUMN';






--  --CONSTRAINT [FK_Orders_OrderItemId] FOREIGN KEY ([OrderItemId]) REFERENCES [dbo].[OrderItems] ([Id])

--ALTER TABLE Orders DROP CONSTRAINT [FK_Orders_OrderItemId];
--ALTER TABLE OrderItems DROP CONSTRAINT [PK_dbo.OrderItems];

--ALTER TABLE OrderItems DROP COLUMN Id
----ALTER TABLE OrderItems ADD Id INT IDENTITY(1,1)
--ALTER TABLE OrderItems
--   ADD ID INT IDENTITY
--       CONSTRAINT PK_OrderItems PRIMARY KEY CLUSTERED

--ALTER TABLE Orders
--ADD CONSTRAINT FK_Orders_OrderItemId FOREIGN KEY (OrderItemId)
--    REFERENCES OrderItems(Id);










----ALTER TABLE CartItems DROP CONSTRAINT CartItemId

----DROP INDEX IX_CartItemId   
----    ON CartItems;  

--	ALTER TABLE CartItems   
--DROP CONSTRAINT Products_CartItemId;   