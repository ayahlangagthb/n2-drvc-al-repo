-- Table to store bets
CREATE TABLE Bets (
    BetID INTEGER IDENTITY(1,1) PRIMARY KEY,
    Player VARCHAR(255) NOT NULL,
	BetNumber INTEGER NOT NULL,
    Amount DECIMAL NOT NULL,
    BetType VARCHAR(255) NOT NULL,
	Payout DECIMAL NOT NULL,
    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Table to store information about each spin
CREATE TABLE Spins (
    SpinID INTEGER IDENTITY(1,1) PRIMARY KEY,
    WinningNumber INT,
    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
);
