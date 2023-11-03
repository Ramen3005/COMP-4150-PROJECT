CREATE TABLE UserData (
    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
    UserName TEXT NOT NULL,
    Password TEXT NOT NULL,
    Type TEXT NOT NULL CHECK (length(Type) = 2),
    Manager INTEGER NOT NULL CHECK (Manager IN (0,1)), -- SQLite does not have a built-in boolean type, so INTEGER is used (0 = false, 1 = true)
    FullName TEXT
);
