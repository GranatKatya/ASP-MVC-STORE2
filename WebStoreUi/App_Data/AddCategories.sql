insert into Categories (Name)
values (N'Для тела'), (N'Для лица'), (N'Для волос')

GO

UPDATE Products
SET Category_Id = (SELECT Id FROM Categories WHERE Name = N'Для тела')
WHERE ID Between 1 AND 5

UPDATE Products
SET Category_Id = (SELECT Id FROM Categories WHERE Name = N'Для лица')
WHERE ID Between 6 AND 10

UPDATE Products
SET Category_Id = (SELECT Id FROM Categories WHERE Name = N'Для волос')
WHERE ID Between 11 AND 15