insert into Orders (OrderItemId,State)
values
(1, N'new'),
(2,N'done'),
(5,N'send'),
(8,N'done'),
(7,N'new'),
(4,N'send'),
(2,N'paid')


  --CONSTRAINT [FK_dbo.Orders_dbo.OrderItems_OrderItem_OrderIWWWtemId] FOREIGN KEY ([OrderItem_OrderIWWWtemId]) REFERENCES [dbo].[OrderItems] ([OrderIWWWtemId])
 
 --ALTER TABLE Orders DROP CONSTRAINT [FK_dbo.Orders_dbo.OrderItems_OrderItem_OrderIWWWtemId] ;
 --DROP INDEX Orders.IX_OrderItem_OrderIWWWtemId
 --ALTER TABLE Orders DROP COLUMN  OrderItem_OrderIWWWtemId



--ALTER TABLE Orders
--ADD CONSTRAINT FK_OrderItems
--FOREIGN KEY (OrderItemsId) REFERENCES OrderItems(OrderIWWWtemId);