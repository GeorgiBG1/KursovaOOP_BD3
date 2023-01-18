USE Diablo
GO
SELECT i.Name, i.Price FROM Users AS u
LEFT OUTER JOIN UsersGames AS ug
ON u.Id = ug.UserId
LEFT OUTER JOIN UserGameItems AS ugi
ON ug.Id = ugi.UserGameId
LEFT OUTER JOIN Items AS i
ON i.Id = ugi.ItemId
WHERE u.Id = 11