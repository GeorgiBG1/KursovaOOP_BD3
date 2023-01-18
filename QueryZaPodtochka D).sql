USE Diablo
GO
--1 way
SELECT u.Id, u.Username, u.FirstName, u.LastName, u.Email, u.RegistrationDate, u.IpAddress FROM UsersGames AS ug
LEFT OUTER JOIN Users AS u
ON ug.UserId = u.Id
WHERE ug.GameId = 7
--2 way
SELECT u.Id, u.Username, u.FirstName, u.LastName, u.Email, u.RegistrationDate, u.IpAddress, ug.Id FROM Users AS u
LEFT OUTER JOIN UsersGames AS ug
ON u.Id = ug.UserId
WHERE ug.GameId = 7