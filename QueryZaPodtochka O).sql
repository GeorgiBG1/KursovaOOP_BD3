USE Diablo
GO
--All items which are not allowed
SELECT * FROM GameTypeForbiddenItems
--The item is allowed when itemId = 7 and gameTypeId = 2
SELECT * FROM GameTypeForbiddenItems
WHERE GameTypeForbiddenItems.ItemId = 7 AND GameTypeForbiddenItems.GameTypeId = 2
--The item is not allowed when itemId = 7 or 9 and gameTypeId = 1
SELECT * FROM GameTypeForbiddenItems
WHERE (GameTypeForbiddenItems.ItemId = 7 OR GameTypeForbiddenItems.ItemId = 9) AND GameTypeForbiddenItems.GameTypeId = 1