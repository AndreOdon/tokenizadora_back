USE [Tokenizadora]
GO

INSERT INTO [User]
           ([Name], [UserName], [Password])
     VALUES
           ('User Test', 'usertest', 'GVXRpnXl5xo8y/avkbtwHQueO6hjsIsjdgLxzXsmE7A=')
GO

INSERT INTO [Region]
           ([Name], [Code])
     VALUES
           ('Região 011', '011'),
		   ('Região 016', '016'),
		   ('Região 017', '017'),
		   ('Região 018', '018')
GO

INSERT INTO [Plan]
           ([Name], [Minutes])
     VALUES
           ('FaleMais 30', 30),
           ('FaleMais 60', 60),
           ('FaleMais 120', 120)
GO

INSERT INTO [RegionTax]
           ([OriginRegionId], [DestinyRegionId], [Tax])
     VALUES
           (1, 2, 1.9),
           (2, 1, 2.9),
           (1, 3, 1.7),
           (3, 1, 2.7),
           (1, 4, 0.9),
           (4, 1, 1.9)
GO

