SELECT * FROM Items
SELECT * FROM ItemTypes
SELECT * FROM [Statistics]

INSERT INTO [Statistics] (Strength, Defence, Mind, Speed, Luck)
VALUES(11, 7, 2, 19, 5);
INSERT INTO Items (Name, Price, MinLevel, StatisticId,ItemTypeId)
VALUES('Aqua Simulacra', 361.04, 15, 124, 8);

DELETE FROM Items
WHERE Name = 'Aqua Simulacra'
DELETE FROM [Statistics]
WHERE [Statistics].Id = 124
SELECT * FROM Items
SELECT * FROM [Statistics]