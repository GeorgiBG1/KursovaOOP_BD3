--1 way
SELECT c.Id, c.Name FROM Users AS u
LEFT OUTER JOIN UsersGames AS ug
ON u.Id = ug.UserId
LEFT OUTER JOIN Characters AS c
ON ug.CharacterId = c.Id
WHERE u.Id = 6

--2 way
SELECT c.Id, c.Name FROM UsersGames AS ug
LEFT OUTER JOIN Characters AS c
ON ug.CharacterId = c.Id
WHERE ug.UserId = 6