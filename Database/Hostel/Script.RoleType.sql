/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO Hostel_Roles(RoleId, [Role])
SELECT NEWID(), N'Owner' WHERE NOT EXISTS(SELECT RoleId FROM Hostel_Roles WHERE [Role] = N'Owner')
GO
INSERT INTO Hostel_Roles(RoleId, [Role])
SELECT NEWID(), N'Tenant' WHERE NOT EXISTS(SELECT RoleId FROM Hostel_Roles WHERE [Role] = N'Tenant')
GO