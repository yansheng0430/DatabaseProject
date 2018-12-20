USE BookStoreDB
GO

--這裡為Book的PROC
----------------------------------------------------------------------------------------------
CREATE PROC GetAllBooks
AS
	SELECT ISBN, BName, UnitPrice, Quantity, Author, dbo.GetCategoryIDMapType(CategoryID) AS Category, dbo.GetPublisherIDMapName(PublisherID) AS Publisher, PublishDate, BDescription, Cover
	FROM BOOK
GO


CREATE PROC CreateNewBook 
@ISBN NVARCHAR(13), @BName NVARCHAR(50), @UnitPrice INT, @Quantity INT, @Author NVARCHAR(50), 
@Category NVARCHAR(20), @Publisher NVARCHAR(20), @PublishDate DATETIME, @BDescription NVARCHAR(300),
@Cover NVARCHAR(100)
AS
	BEGIN TRAN
	INSERT INTO BOOK VALUES(@ISBN, @BName, @UnitPrice, @Quantity, @Author, dbo.GetCategoryTypeMapID(@Category), 
							dbo.GetPublisherNameMapID(@Publisher), @PublishDate, @BDescription, @Cover);
	COMMIT
GO

CREATE PROC ManageBook @EmployeeID NVARCHAR(10), @ISBN NVARCHAR(13), @Quantity INT, @ActionState NVARCHAR(10)
AS
	BEGIN TRAN
	INSERT INTO MANAGE(EmployeeID, ISBN, Quantity, ActionState) VALUES(@EmployeeID, @ISBN, @Quantity, @ActionState)
	IF @ActionState = 'Increase'
	BEGIN 
		UPDATE BOOK SET BOOK.Quantity = (BOOK.Quantity + @Quantity) WHERE BOOK.ISBN = @ISBN 
	END
	ELSE IF @ActionState = 'Decrease'
	BEGIN
		UPDATE BOOK SET BOOK.Quantity = (BOOK.Quantity - @Quantity) WHERE BOOK.ISBN = @ISBN
	END
	COMMIT
GO

CREATE PROC DeleteBook @ISBN NVARCHAR(13)
AS
	BEGIN TRAN
	DELETE FROM BOOK WHERE BOOK.ISBN = @ISBN
	COMMIT
GO

CREATE PROC EditBook
@ISBN NVARCHAR(13), @BName NVARCHAR(50), @UnitPrice INT, @Author NVARCHAR(50), 
@Category NVARCHAR(20), @Publisher NVARCHAR(20), @PublishDate DATETIME, @BDescription NVARCHAR(300),
@Cover NVARCHAR(100)
AS
	BEGIN TRAN
	UPDATE BOOK SET BOOK.BName = @BName, BOOK.UnitPrice = @UnitPrice, BOOK.Author = @Author,
					BOOK.CategoryID = dbo.GetCategoryTypeMapID(@Category), BOOK.PublisherID = dbo.GetPublisherNameMapID(@Publisher), 
					BOOK.PublishDate = @PublishDate, BOOK.BDescription = @BDescription, 
					BOOK.Cover = @Cover
	WHERE BOOK.ISBN = @ISBN
	COMMIT
GO


CREATE FUNCTION GetCategoryTypeMapID (@Category NVARCHAR(3))
RETURNS NVARCHAR(3)
BEGIN
	DECLARE @CategoryID NVARCHAR(3)
	SELECT @CategoryID = CategoryID
	FROM CATEGORY
	WHERE CType = @Category

	RETURN @CategoryID
END

GO


CREATE FUNCTION GetCategoryIDMapType (@CategoryID NVARCHAR(3))
RETURNS NVARCHAR(20)
BEGIN
	DECLARE @CType NVARCHAR(20)
	SELECT @CType = CType
	FROM CATEGORY
	WHERE CategoryID = @CategoryID

	RETURN @CType
END


CREATE FUNCTION GetPublisherNameMapID (@Publisher NVARCHAR(20))
RETURNS NVARCHAR(6)
BEGIN
	DECLARE @PublisherID NVARCHAR(6)
	SELECT @PublisherID = PublisherID
	FROM PUBLISHER
	WHERE PName = @Publisher

	RETURN @PublisherID
END

GO

CREATE FUNCTION GetPublisherIDMapName (@PublisherID NVARCHAR(6))
RETURNS NVARCHAR(20)
BEGIN
	DECLARE @PName NVARCHAR(20)
	SELECT @PName = PName
	FROM PUBLISHER
	WHERE PublisherID = @PublisherID

	RETURN @PName
END

GO

CREATE PROC GetBooksByISBNKeyWord @ISBN NVARCHAR(13)
AS
	DECLARE @QueryISBN NVARCHAR(15)
	SET @QueryISBN = '%' + @ISBN + '%'

	SELECT ISBN, BName, UnitPrice, Quantity, Author, dbo.GetCategoryIDMapType(CategoryID) AS Category, dbo.GetPublisherIDMapName(PublisherID) AS Publisher, PublishDate, BDescription, Cover
	FROM BOOK
	WHERE ISBN LIKE @QueryISBN
GO

CREATE PROC GetBooksByNameKeyWord @Name NVARCHAR(13)
AS
	DECLARE @QueryName NVARCHAR(15)
	SET @QueryName = '%' + @Name + '%'

	SELECT ISBN, BName, UnitPrice, Quantity, Author, dbo.GetCategoryIDMapType(CategoryID) AS Category, dbo.GetPublisherIDMapName(PublisherID) AS Publisher, PublishDate, BDescription, Cover
	FROM BOOK
	WHERE BName LIKE @QueryName
GO	

CREATE PROC GetBooksByAuthorKeyWord @Author NVARCHAR(13)
AS
	DECLARE @QueryAuthor NVARCHAR(15)
	SET @QueryAuthor = '%' + @Author + '%'

	SELECT ISBN, BName, UnitPrice, Quantity, Author, dbo.GetCategoryIDMapType(CategoryID) AS Category, dbo.GetPublisherIDMapName(PublisherID) AS Publisher, PublishDate, BDescription, Cover
	FROM BOOK
	WHERE Author LIKE @QueryAuthor
GO	 

CREATE PROC GetBooksByPublisherKeyWord @Publisher NVARCHAR(13)
AS
	DECLARE @QueryPublisher NVARCHAR(15)
	SET @QueryPublisher = '%' + @Publisher + '%'

	SELECT ISBN, BName, UnitPrice, Quantity, Author, dbo.GetCategoryIDMapType(CategoryID) AS Category, dbo.GetPublisherIDMapName(PublisherID) AS Publisher, PublishDate, BDescription, Cover
	FROM BOOK
	WHERE PublisherID IN 
	(SELECT PublisherID 
	 FROM PUBLISHER 
	 WHERE PName LIKE @QueryPublisher) 
	
GO	 

CREATE PROC GetBooksByCategoryFilter @Category NVARCHAR(20)
AS
	SELECT ISBN, BName, UnitPrice, Quantity, Author, dbo.GetCategoryIDMapType(CategoryID) AS Category, dbo.GetPublisherIDMapName(PublisherID) AS Publisher, PublishDate, BDescription, Cover
	FROM BOOK
	WHERE CategoryID IN 
	(SELECT CategoryID
	 FROM CATEGORY
	 WHERE CType = @Category) 
GO

CREATE PROC GetBooksByPriceFilter @lowerPrice INT, @higherPrice INT
AS
	SELECT ISBN, BName, UnitPrice, Quantity, Author, dbo.GetCategoryIDMapType(CategoryID) AS Category, dbo.GetPublisherIDMapName(PublisherID) AS Publisher, PublishDate, BDescription, Cover
	FROM BOOK
	WHERE @lowerPrice <= UnitPrice AND @higherPrice >= UnitPrice
GO

--這裡為Category PROC
---------------------------------------------------------------------------------------

CREATE PROC GetAllCategories
AS
	WITH CATEGORYCONATIN
	AS
	(
		SELECT CategoryID, COUNT(*) AS BooksAmout
		FROM BOOK
		GROUP BY CategoryID
	)
	SELECT CATEGORY.CategoryID, CType, BooksAmout
	FROM CATEGORYCONATIN JOIN CATEGORY ON CATEGORYCONATIN.CategoryID = CATEGORY.CategoryID
GO

CREATE PROC CreateNewCategory @CategoryID NVARCHAR(3), @CType NVARCHAR(20)
AS
	BEGIN TRAN
	INSERT INTO CATEGORY VALUES(@CategoryID, @CType)
	COMMIT
GO

CREATE PROC EditCategory @CategoryID NVARCHAR(3), @CType NVARCHAR(20)
AS
	BEGIN TRAN
		UPDATE CATEGORY SET CType = @CType WHERE CategoryID = @CategoryID
	COMMIT
GO

--這裡為Publisher PROC
---------------------------------------------------------------------------------------

CREATE PROC GetAllPublishers
AS
	SELECT PublisherID, PName, Phone, PAddress
	FROM PUBLISHER
GO

CREATE PROC GetPublisherByName @PName NVARCHAR(20)
AS
	SELECT PublisherID, PName, Phone, PAddress
	FROM PUBLISHER
	WHERE PName = @PName
GO

CREATE PROC CreateNewPublisher @PublisherID NVARCHAR(6), @PName NVARCHAR(20), @Phone NVARCHAR(10), @PAddress NVARCHAR(50)
AS
	BEGIN TRAN
		INSERT INTO PUBLISHER VALUES(@PublisherID, @PName, @Phone, @PAddress) 
	COMMIT
GO

CREATE PROC EditPublisher @PublisherID NVARCHAR(6), @PName NVARCHAR(20), @Phone NVARCHAR(10), @PAddress NVARCHAR(50)
AS
	BEGIN TRAN
		UPDATE PUBLISHER SET PName = @PName, Phone = @Phone, PAddress = @PAddress
		WHERE PublisherID = @PublisherID
	COMMIT
GO

--這裡為Customer PROC
---------------------------------------------------------------------------------------

CREATE PROC AuthenticateCustomer @Account NVARCHAR(50), @Password NVARCHAR(50)
AS 
	BEGIN TRAN
		SELECT * FROM CUSTOMER WHERE Account = @Account AND Passward = @Password
	COMMIT
GO

CREATE PROC CreateCustomer @FirstName NVARCHAR(20), @LastName NVARCHAR(20), @Sex BIT,
			@CellPhone NVARCHAR(10), @CAddress NVARCHAR(50), @Email NVARCHAR(50),
			@Account NVARCHAR(50), @Password NVARCHAR(50)
AS 
	BEGIN TRAN
		INSERT INTO CUSTOMER VALUES(dbo.CreateCustomerID (), @FirstName, @LastName, @Sex, @CellPhone, @CAddress, @Email, @Account, @Password)		
	COMMIT
GO

CREATE FUNCTION CreateCustomerID ()
RETURNS NVARCHAR(10)
BEGIN
	DECLARE @CustomerID NVARCHAR(10)
	DECLARE @NewCustomerID NVARCHAR(10)
	DECLARE @NowDate NVARCHAR(6)

	SET @CustomerID =
	(	
		SELECT TOP(1) CustomerID
		FROM CUSTOMER
		ORDER BY CustomerID DESC
	)
	SET @NowDate = CONVERT(NVARCHAR(6), GETDATE(), 112)
	
	IF SUBSTRING(@CustomerID, 2, 6) = @NowDate
		SET @NewCustomerID = 'C' + @NowDate + FORMAT((SUBSTRING(@CustomerID, 8, 3) + 1), '000')	 
	ELSE
		SET @NewCustomerID = 'C' + @NowDate + '001'
	RETURN @NewCustomerID
END
GO

CREATE PROC CheckCustomerAccountExits @Account NVARCHAR(50)
AS
	SELECT CustomerID
	FROM CUSTOMER
	WHERE Account = @Account
GO

--這裡為Employee PROC
---------------------------------------------------------------------------------------

CREATE PROC AuthenticateEmployee @Account NVARCHAR(50), @Password NVARCHAR(50)
AS 
	BEGIN TRAN
		SELECT * FROM EMPLOYEE WHERE Account = @Account AND Passward = @Password
	COMMIT
GO

--這裡為Intresting PROC
---------------------------------------------------------------------------------------

CREATE PROC AddIntrestingBook @CustomerID NVARCHAR(10), @ISBN NVARCHAR(13)
AS
	BEGIN TRAN 
		IF NOT EXISTS (SELECT * FROM INTRESTING WHERE CustomerID = @CustomerID AND ISBN = @ISBN)
			INSERT INTO INTRESTING(CustomerID, ISBN) VALUES(@CustomerID, @ISBN)
	COMMIT
GO

CREATE PROC DeleteIntrestingBook @CustomerID NVARCHAR(10), @ISBN NVARCHAR(13)
AS
	BEGIN TRAN 
		DELETE FROM INTRESTING WHERE CustomerID = @CustomerID AND ISBN = @ISBN
	COMMIT
GO

CREATE PROC GetIntrestingBooksByCustomerID @CustomerID NVARCHAR(10)
AS
	SELECT CustomerID, BOOK.ISBN, BName, UnitPrice, Cover, AddDate 
	FROM INTRESTING JOIN BOOK ON INTRESTING.ISBN = BOOK.ISBN 
	WHERE CustomerID = @CustomerID
GO

--這裡為ShoppingCart PROC
---------------------------------------------------------------------------------------

CREATE PROC AddShoppingCartBook @CustomerID NVARCHAR(10), @ISBN NVARCHAR(13), @Amount INT
AS
	BEGIN TRAN
		IF @Amount > 0 AND NOT EXISTS (SELECT * FROM SHOP_CAR WHERE CustomerID = @CustomerID AND ISBN = @ISBN)
			INSERT INTO SHOP_CAR VALUES(@CustomerID, @ISBN, @Amount)
	COMMIT
GO

CREATE PROC ChangeShoppingCartBookAmount @CustomerID NVARCHAR(10), @ISBN NVARCHAR(13), @Amount INT
AS
	BEGIN TRAN
		IF @Amount > 0
			UPDATE SHOP_CAR SET Amount = @Amount WHERE CustomerID = @CustomerID AND ISBN = @ISBN
	COMMIT
GO

CREATE PROC DeleteShoppingCart @CustomerID NVARCHAR(10), @ISBN NVARCHAR(13)
AS
	BEGIN TRAN
		DELETE FROM SHOP_CAR WHERE CustomerID = @CustomerID AND ISBN = @ISBN
	COMMIT
GO

CREATE PROC CheckShoppingCartBookExist @CustomerID NVARCHAR(10), @ISBN NVARCHAR(13)
AS
	SELECT COUNT(*)
	FROM SHOP_CAR
	WHERE CustomerID = @CustomerID AND ISBN = @ISBN
GO

CREATE PROC GetShoppingCartByCustomerID @CustomerID NVARCHAR(10)
AS
	SELECT CustomerID, SHOP_CAR.ISBN, BName, UnitPrice, SHOP_CAR.Amount, BOOK.Cover
	FROM SHOP_CAR JOIN BOOK ON SHOP_CAR.ISBN = BOOK.ISBN 
	WHERE CustomerID = @CustomerID
GO

--這裡為Orders PROC
CREATE PROC CreateNewOrder @CustomerID NVARCHAR(10), @CreditCard NVARCHAR(16), @Phone NVARCHAR(10), @Address NVARCHAR(50), @Email NVARCHAR(50), @FirstName NVARCHAR(20), @LastName NVARCHAR(20)
AS
	BEGIN TRAN
		DECLARE @NewOrderID NVARCHAR(15)
		SET @NewOrderID = dbo.CreateOrderID()
		
		INSERT INTO ORDERS(OrderID, OwnerID, CreditCard, Phone, OAddress, Email, FirstName, LastName) 
		VALUES(@NewOrderID, @CustomerID, @CreditCard, @Phone, @Address, @Email, @FirstName, @LastName)

		INSERT INTO CONTAIN
		SELECT @NewOrderID, ISBN, Amount
		FROM SHOP_CAR WHERE CustomerID = @CustomerID

		DELETE FROM SHOP_CAR WHERE  CustomerID = @CustomerID
	COMMIT
GO

CREATE FUNCTION CreateOrderID ()
RETURNS NVARCHAR(15)
BEGIN
	DECLARE @OrderID NVARCHAR(15)
	DECLARE @NewOrderID NVARCHAR(15)
	DECLARE @NowDate NVARCHAR(8)
	
	SET @OrderID =  
	(	SELECT TOP(1 )OrderID
		FROM ORDERS
		ORDER BY OrderID DESC
	)
	SET @NowDate = CONVERT(NVARCHAR(8), GETDATE(), 112)

	IF SUBSTRING(@OrderID, 1, 8) = @NowDate
		SET @NewOrderID = @NowDate + FORMAT((SUBSTRING(@OrderID, 9, 7) + 1), '0000000')	 
	ELSE
		SET @NewOrderID = @NowDate + '0000001'
	RETURN @NewOrderID
END

