CREATE TABLE Idea (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    project_name VARCHAR(100) NOT NULL,
	initials VARCHAR(5) NOT NULL,
	Description VARCHAR(1500) NOT NULL,
	risk VARCHAR(1000),
	team VARCHAR(100),
	plan_description VARCHAR(1000),
	ExpectedResults VARCHAR(1000), 
	[priority] INT,
	[status] INT DEFAULT 1,
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, 
	UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);
