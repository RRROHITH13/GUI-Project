drop database uservs;
-- Create the database
CREATE DATABASE  uservs;

-- Switch to the uservs database
USE uservs;

-- Create the users table
CREATE TABLE  users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL
);

CREATE TABLE Appointments (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Slots INT,
    Time VARCHAR(50),
    Date DATE,
    Specialization VARCHAR(100)
);

drop table appointments;
drop table Doctors;
