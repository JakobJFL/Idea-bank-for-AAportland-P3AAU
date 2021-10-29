CREATE TABLE Idea (
    idea_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    project_name VARCHAR(100) NOT NULL,
	initials VARCHAR(5) NOT NULL,
	idea_description VARCHAR(1500) NOT NULL,
	risk VARCHAR(1000),
	team VARCHAR(100),
	plan_description VARCHAR(1000),
	expected_results VARCHAR(1000), 
	[priority] INT,
	[status] INT DEFAULT 1,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, 
	updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);
