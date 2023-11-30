-- Create mSupplier table
CREATE TABLE mSupplier (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    CreatedBy NVARCHAR(255) NOT NULL,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedBy NVARCHAR(255),
    LastModifiedDateTime DATETIME2
);

-- Create mItem table
CREATE TABLE mItem (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    SellingPrice DECIMAL NOT NULL,
    CreatedBy NVARCHAR(255) NOT NULL,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedBy NVARCHAR(255),
    LastModifiedDateTime DATETIME2
);

-- Create mLocation table
CREATE TABLE mLocation (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    CreatedBy NVARCHAR(255) NOT NULL,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedBy NVARCHAR(255),
    LastModifiedDateTime DATETIME2
);

-- Create mSupplierItem table
CREATE TABLE mSupplierItem (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdSupplier INT,
    IdItem INT,
    Price DECIMAL NOT NULL,
    CreatedBy NVARCHAR(255) NOT NULL,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedBy NVARCHAR(255),
    LastModifiedDateTime DATETIME2,
    CONSTRAINT FK_mSupplierItem_Supplier FOREIGN KEY (IdSupplier) REFERENCES mSupplier(Id),
    CONSTRAINT FK_mSupplierItem_Item FOREIGN KEY (IdItem) REFERENCES mItem(Id)
);

-- Create mItemLocation table
CREATE TABLE mItemLocation (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdItem INT,
    IdLocation INT,
    Stock INT NOT NULL,
    CreatedBy NVARCHAR(255) NOT NULL,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedBy NVARCHAR(255),
    LastModifiedDateTime DATETIME2,
    CONSTRAINT FK_mItemLocation_Item FOREIGN KEY (IdItem) REFERENCES mItem(Id),
    CONSTRAINT FK_mItemLocation_Location FOREIGN KEY (IdLocation) REFERENCES mLocation(Id)
);
