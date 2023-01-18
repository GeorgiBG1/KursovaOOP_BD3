USE Diablo
GO
SELECT c.Id, c.Name, s.Strength, s.Defence, s.Mind, s.Speed, s.Luck FROM UsersGames AS ug
LEFT OUTER JOIN Characters AS c
ON ug.CharacterId = c.Id
LEFT OUTER JOIN [Statistics] AS s
ON c.StatisticId = s.Id
WHERE ug.GameId = 2