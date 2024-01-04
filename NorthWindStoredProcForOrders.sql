USE [Northwind]
GO

CREATE PROCEDURE pr_GetOrderSummary
    @StartDate DATETIME,
    @EndDate DATETIME,
    @EmployeeID INT = NULL,
    @CustomerID NVARCHAR(5) = NULL
AS
BEGIN
    SELECT
        CONCAT(e.TitleOfCourtesy, ' ', e.FirstName, ' ', e.LastName) AS EmployeeFullName,
        s.CompanyName AS ShipperCompanyName,
        c.CompanyName AS CustomerCompanyName,
        COUNT(o.OrderID) AS NumberOfOrders,
        CONVERT(DATE, o.OrderDate) AS [Date],
        SUM(o.Freight) AS TotalFreightCost,
        COUNT(DISTINCT od.ProductID) AS NumberOfDifferentProducts,
        SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) AS TotalOrderValue
    FROM
        Orders o
        JOIN Employees e ON o.EmployeeID = e.EmployeeID
        JOIN Shippers s ON o.ShipVia = s.ShipperID
        JOIN Customers c ON o.CustomerID = c.CustomerID
        JOIN [Order Details] od ON o.OrderID = od.OrderID
    WHERE
        (@EmployeeID IS NULL OR o.EmployeeID = @EmployeeID) AND
        (@CustomerID IS NULL OR o.CustomerID = @CustomerID) AND
        o.OrderDate BETWEEN @StartDate AND @EndDate
    GROUP BY
        CONVERT(DATE, o.OrderDate),
        e.EmployeeID,
        CONCAT(e.TitleOfCourtesy, ' ', e.FirstName, ' ', e.LastName),
        s.ShipperID,
        s.CompanyName,
        c.CustomerID,
        c.CompanyName
    ORDER BY
        [Date],
        EmployeeFullName,
        CustomerCompanyName,
        ShipperCompanyName;
END
exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=5 , @CustomerID='VINET'