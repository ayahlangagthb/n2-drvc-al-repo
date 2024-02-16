USE [Northwind]
GO
/****** Object:  StoredProcedure [dbo].[pr_PlaceBet]    Script Date: 03/01/2024 17:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to place a bet
ALTER PROCEDURE [dbo].[pr_PlaceBet]
    @Player TEXT,
    @Amount DECIMAL,
	@BetNumber INTEGER,
    @BetType TEXT
AS
BEGIN
    INSERT INTO Bets (Player, Amount, BetType, BetNumber) VALUES (@Player, @Amount, @BetType, @BetNumber);
END;

-- Stored Procedure to perform a roulette spin
CREATE PROCEDURE pr_SpinRoulette
    @WinningNumber INT
AS
BEGIN
    INSERT INTO Spins (WinningNumber) VALUES (@WinningNumber);
END;

USE [Northwind]
GO
/****** Object:  StoredProcedure [dbo].[pr_Payout]    Script Date: 03/01/2024 16:49:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Stored Procedure to calculate and perform payouts
CREATE PROCEDURE [dbo].[pr_Payout]
AS
BEGIN

WITH CTE AS (
     SELECT TOP 1 WinningNumber
     FROM Spins
	 ORDER BY Timestamp DESC
)
    -- Payout logic based on the winning number
 UPDATE Bets
 SET Payout = 
        CASE 
            WHEN BetType = 'Even' AND CTE.WinningNumber % 2 = 0 THEN Amount * 2 -- Example: Even bets
            WHEN BetType = 'Odd' AND CTE.WinningNumber % 2 <> 0 THEN Amount * 2 -- Example: Odd bets
            WHEN BetType = 'Number' AND  BetNumber = CTE.WinningNumber THEN Amount * 36 -- Example: Specific number bets
            ELSE 0 -- No payout for other bets
        END

    FROM CTE
    WHERE Payout IS NULL; -- Only update bets that haven't been paid out yet

	SELECT TOP 1 Payout
	FROM Bets
	ORDER BY Timestamp DESC
END

-- Stored Procedure to retrieve previous spins
CREATE PROCEDURE pr_ShowPreviousSpins
AS
BEGIN
    SELECT * FROM Spins ORDER BY Timestamp DESC;
END;
