
-- Create Database
CREATE DATABASE UserManagementSystem;
GO

USE UserManagementSystem;
GO

-- =============================================
-- 1. Admin Table
-- =============================================
CREATE TABLE Admins (
    AdminId INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(256) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastLoginDate DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- =============================================
-- 2. Users Table
-- =============================================
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(256) NOT NULL UNIQUE, -- Can store email OR phone number
    PasswordHash NVARCHAR(MAX) NOT NULL,
    PhoneNumber NVARCHAR(20) NULL,
    Age INT NULL,
    Gender NVARCHAR(10) NULL,
    Skills NVARCHAR(500) NULL,
    Education NVARCHAR(200) NULL,
    Address NVARCHAR(500) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- =============================================
-- 3. Create Indexes for Performance
-- =============================================
CREATE NONCLUSTERED INDEX IX_Users_Email ON Users(Email);
CREATE NONCLUSTERED INDEX IX_Users_PhoneNumber ON Users(PhoneNumber);
CREATE NONCLUSTERED INDEX IX_Admins_Email ON Admins(Email);
GO

-- =============================================
-- 4. Insert Sample Admin (Password: Admin@123)
-- =============================================
-- Note: In actual application, password will be hashed using BCrypt or similar
INSERT INTO Admins (Email, PasswordHash, CreatedDate, IsActive)
VALUES ('admin@example.com', 'AQAAAAEAACcQAAAAEJ7XvkjlKkVzL8VZ9xQ5w==', GETDATE(), 1);
GO

-- =============================================
-- 5. Insert Sample Users for Testing
-- =============================================
INSERT INTO Users (Name, Email, PasswordHash, PhoneNumber, Age, Gender, Skills, Education, Address, CreatedDate, IsActive)
VALUES 
('John Doe', 'john@example.com', 'HashedPassword123', '1234567890', 28, 'Male', 'C#, ASP.NET, SQL', 'Bachelor in CS', '123 Main St, City', GETDATE(), 1),
('Jane Smith', 'jane@example.com', 'HashedPassword456', '0987654321', 25, 'Female', 'JavaScript, React, Node', 'Master in IT', '456 Oak Ave, Town', GETDATE(), 1);
GO

-- =============================================
-- 6. Verification Queries
-- =============================================
SELECT * FROM Admins;
SELECT * FROM Users;
GO

-- =============================================
-- 7. Stored Procedures (Optional but Recommended)
-- =============================================

-- Get User by Email or Phone
CREATE PROCEDURE sp_GetUserByEmailOrPhone
    @credential NVARCHAR(256)
AS
BEGIN
    SELECT * FROM Users 
    WHERE (Email = @credential OR PhoneNumber = @credential) 
    AND IsActive = 1;
END
GO

-- Get All Active Users
CREATE PROCEDURE sp_GetAllActiveUsers
AS
BEGIN
    SELECT * FROM Users WHERE IsActive = 1 ORDER BY CreatedDate DESC;
END
GO
