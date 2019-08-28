
	
	--INSERT INTO AspNetUsers (UserId , RoleId)
	--values ()


	INSERT INTO AspNetUserRoles (UserId , RoleId)
	values
	--(
	--(SELECT ID  FROM  AspNetUsers  WHERE Email= N'Katya1'),
	--(SELECT ID  FROM  AspNetRoles  WHERE Name= N'Admin')
	--),
	((SELECT ID  FROM  AspNetUsers  WHERE Email= N'KatyaAdmin'),
	(SELECT ID  FROM  AspNetRoles  WHERE Name= N'Admin'))



	((SELECT ID  FROM  AspNetUsers  WHERE Email= N'KatyaAdmin'),
	(SELECT ID  FROM  AspNetRoles  WHERE Name= N'Admin'))