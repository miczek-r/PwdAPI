CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Homes` (
    `HomeId` int NOT NULL AUTO_INCREMENT,
    `HomeName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Street` longtext CHARACTER SET utf8mb4 NULL,
    `HouseNumber` longtext CHARACTER SET utf8mb4 NULL,
    `PostCode` longtext CHARACTER SET utf8mb4 NULL,
    `City` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Homes` PRIMARY KEY (`HomeId`)
);

CREATE TABLE `TypesOfExpenses` (
    `TypeOfExpenseId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_TypesOfExpenses` PRIMARY KEY (`TypeOfExpenseId`)
);

CREATE TABLE `Users` (
    `UserId` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext CHARACTER SET utf8mb4 NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `Email` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Saldo` decimal(65,30) NOT NULL,
    `HomeId` int NULL,
    `ActivationToken` longtext CHARACTER SET utf8mb4 NULL,
    `PasswordRestorationToken` longtext CHARACTER SET utf8mb4 NULL,
    `ExpenseLimit` decimal(65,30) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`UserId`),
    CONSTRAINT `FK_Users_Homes_HomeId` FOREIGN KEY (`HomeId`) REFERENCES `Homes` (`HomeId`) ON DELETE RESTRICT
);

CREATE TABLE `Expenses` (
    `ExpenseId` int NOT NULL AUTO_INCREMENT,
    `NameOfExpense` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Amount` decimal(65,30) NOT NULL,
    `ExpenseDate` datetime(6) NOT NULL,
    `TypeOfExpenseId` int NOT NULL,
    `OwnerId` int NOT NULL,
    `HomeId` int NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_Expenses` PRIMARY KEY (`ExpenseId`),
    CONSTRAINT `FK_Expenses_Homes_HomeId` FOREIGN KEY (`HomeId`) REFERENCES `Homes` (`HomeId`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Expenses_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Expenses_HomeId` ON `Expenses` (`HomeId`);

CREATE INDEX `IX_Expenses_UserId` ON `Expenses` (`UserId`);

CREATE UNIQUE INDEX `IX_Users_Email` ON `Users` (`Email`);

CREATE INDEX `IX_Users_HomeId` ON `Users` (`HomeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210508080947_Initial-Migration', '5.0.5');

COMMIT;

