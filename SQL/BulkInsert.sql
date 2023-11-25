BULK INSERT MatchData
FROM 'C:\Users\ryohe\Documents\GitHub\Football-Insights-Premier-League\SQL\EPL_Matches.csv'
WITH (
    FIELDTERMINATOR = ',',  -- Specify the delimiter used in your CSV
    ROWTERMINATOR = '\n',   -- Specify the row terminator used in your CSV
    FIRSTROW = 2            -- Skip the first row as it contains headers
);
