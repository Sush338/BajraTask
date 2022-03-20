create database Product;

USE [Product]
GO
CREATE TABLE [dbo].[EmployeesRecord](
	[EmployeeId] [int] NOT NULL,
	[FirstName] [varchar](200) NOT NULL,
	[MiddleName] [varchar](200) NULL,
	[LastName] [varchar](200) NOT NULL,
	[joinDate] [smalldatetime] NULL,
	[MonthlySalary] [decimal](18, 0) NULL
) ON [PRIMARY]
GO



Insert into [EmployeesRecord]  values ('Ram','Kumar','Basnet','2022-02-03 00:00:00',25000)
Insert into [EmployeesRecord]  values ('Hari','Lal','Shrestha','2021-01-03 00:00:00',30000)
Insert into [EmployeesRecord]  values ('Prabin','Kumar','Chapagain','2021-12-03 00:00:00',50000)
Insert into [EmployeesRecord]  values ('Sushant','Kumar','Dahal','2021-11-03 00:00:00',35000)
Insert into [EmployeesRecord]  values ('Sushant','Bahadur','Karki','2021-11-03 00:00:00',35000)


--Write a Single Query to find the Maximum Total Earnings and Total Number of Employees who have the
--Maximum Total Earnings as MaximumTotalEarnings and TotalEmployees.

--Solution:

select top 1 count(*) as TotalEmployees, (DATEDIFF(MONTH,joinDate,getdate()) * MonthlySalary) as totalEarning 
from EmployeesRecord group by DATEDIFF(MONTH,joinDate,getdate()) * MonthlySalary order by totalEarning desc;



