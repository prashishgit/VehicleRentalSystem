
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/07/2021 21:18:04
-- Generated from EDMX file: G:\Project\Project\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VehicleRentalDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tblBooking_tblItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblBooking] DROP CONSTRAINT [FK_tblBooking_tblItem];
GO
IF OBJECT_ID(N'[dbo].[FK_tblBooking_tblUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblBooking] DROP CONSTRAINT [FK_tblBooking_tblUser];
GO
IF OBJECT_ID(N'[dbo].[FK_tblComments_tblItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblComments] DROP CONSTRAINT [FK_tblComments_tblItem];
GO
IF OBJECT_ID(N'[dbo].[FK_tblItem_tblCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblItem] DROP CONSTRAINT [FK_tblItem_tblCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_tblTestimony_tblUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblTestimony] DROP CONSTRAINT [FK_tblTestimony_tblUser];
GO
IF OBJECT_ID(N'[dbo].[FK_tblUser_tblRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblUser] DROP CONSTRAINT [FK_tblUser_tblRole];
GO
IF OBJECT_ID(N'[dbo].[FK_tblUserRole_tblRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblUserRole] DROP CONSTRAINT [FK_tblUserRole_tblRole];
GO
IF OBJECT_ID(N'[dbo].[FK_tblUserRole_tblUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblUserRole] DROP CONSTRAINT [FK_tblUserRole_tblUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[tblBanner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblBanner];
GO
IF OBJECT_ID(N'[dbo].[tblBooking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblBooking];
GO
IF OBJECT_ID(N'[dbo].[tblCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblCategory];
GO
IF OBJECT_ID(N'[dbo].[tblComments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblComments];
GO
IF OBJECT_ID(N'[dbo].[tblContact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblContact];
GO
IF OBJECT_ID(N'[dbo].[tblItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblItem];
GO
IF OBJECT_ID(N'[dbo].[tblRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblRole];
GO
IF OBJECT_ID(N'[dbo].[tblTestimony]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblTestimony];
GO
IF OBJECT_ID(N'[dbo].[tblUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUser];
GO
IF OBJECT_ID(N'[dbo].[tblUserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUserRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'tblBanners'
CREATE TABLE [dbo].[tblBanners] (
    [BannerId] int IDENTITY(1,1) NOT NULL,
    [Photo] nvarchar(50)  NULL,
    [Title] nvarchar(50)  NULL,
    [Description] nvarchar(50)  NOT NULL,
    [HeadingOne] nvarchar(50)  NULL,
    [HeadingTwo] nvarchar(50)  NULL
);
GO

-- Creating table 'tblBookings'
CREATE TABLE [dbo].[tblBookings] (
    [BookingId] int IDENTITY(1,1) NOT NULL,
    [VehicleId] int  NULL,
    [UserId] int  NULL,
    [PickUpDate] nvarchar(50)  NULL,
    [DropOffDate] nvarchar(50)  NULL,
    [TotalAmount] int  NULL,
    [AmountPaid] int  NULL,
    [BookingStatus] nvarchar(50)  NULL
);
GO

-- Creating table 'tblCategories'
CREATE TABLE [dbo].[tblCategories] (
    [VehicleCategoryId] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(50)  NULL
);
GO

-- Creating table 'tblComments'
CREATE TABLE [dbo].[tblComments] (
    [CommentId] int IDENTITY(1,1) NOT NULL,
    [Comments] varchar(7999)  NULL,
    [ThisDateTime] datetime  NULL,
    [VehicleId] int  NULL,
    [Rating] int  NULL,
    [UserName] nvarchar(50)  NULL
);
GO

-- Creating table 'tblContacts'
CREATE TABLE [dbo].[tblContacts] (
    [ContactId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Subject] nvarchar(50)  NULL,
    [Message] nvarchar(max)  NULL
);
GO

-- Creating table 'tblItems'
CREATE TABLE [dbo].[tblItems] (
    [VehicleId] int IDENTITY(1,1) NOT NULL,
    [VehicleCategoryId] int  NULL,
    [VehiclePrice] nvarchar(50)  NULL,
    [VehicleTitle] nvarchar(500)  NULL,
    [VehiclePhoto] nvarchar(50)  NULL,
    [Description] nvarchar(max)  NULL,
    [VehicleStatus] nvarchar(50)  NULL
);
GO

-- Creating table 'tblRoles'
CREATE TABLE [dbo].[tblRoles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(50)  NULL
);
GO

-- Creating table 'tblTestimonies'
CREATE TABLE [dbo].[tblTestimonies] (
    [TestimonyId] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [Location] nvarchar(50)  NULL,
    [TestimonyDescription] nvarchar(max)  NULL
);
GO

-- Creating table 'tblUsers'
CREATE TABLE [dbo].[tblUsers] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [FullName] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Photo] nvarchar(50)  NULL,
    [CitizenshipPhoto] nvarchar(50)  NULL
);
GO

-- Creating table 'tblUserRoles'
CREATE TABLE [dbo].[tblUserRoles] (
    [UserRoleId] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NULL,
    [UserId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [BannerId] in table 'tblBanners'
ALTER TABLE [dbo].[tblBanners]
ADD CONSTRAINT [PK_tblBanners]
    PRIMARY KEY CLUSTERED ([BannerId] ASC);
GO

-- Creating primary key on [BookingId] in table 'tblBookings'
ALTER TABLE [dbo].[tblBookings]
ADD CONSTRAINT [PK_tblBookings]
    PRIMARY KEY CLUSTERED ([BookingId] ASC);
GO

-- Creating primary key on [VehicleCategoryId] in table 'tblCategories'
ALTER TABLE [dbo].[tblCategories]
ADD CONSTRAINT [PK_tblCategories]
    PRIMARY KEY CLUSTERED ([VehicleCategoryId] ASC);
GO

-- Creating primary key on [CommentId] in table 'tblComments'
ALTER TABLE [dbo].[tblComments]
ADD CONSTRAINT [PK_tblComments]
    PRIMARY KEY CLUSTERED ([CommentId] ASC);
GO

-- Creating primary key on [ContactId] in table 'tblContacts'
ALTER TABLE [dbo].[tblContacts]
ADD CONSTRAINT [PK_tblContacts]
    PRIMARY KEY CLUSTERED ([ContactId] ASC);
GO

-- Creating primary key on [VehicleId] in table 'tblItems'
ALTER TABLE [dbo].[tblItems]
ADD CONSTRAINT [PK_tblItems]
    PRIMARY KEY CLUSTERED ([VehicleId] ASC);
GO

-- Creating primary key on [RoleId] in table 'tblRoles'
ALTER TABLE [dbo].[tblRoles]
ADD CONSTRAINT [PK_tblRoles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [TestimonyId] in table 'tblTestimonies'
ALTER TABLE [dbo].[tblTestimonies]
ADD CONSTRAINT [PK_tblTestimonies]
    PRIMARY KEY CLUSTERED ([TestimonyId] ASC);
GO

-- Creating primary key on [UserId] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [PK_tblUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserRoleId] in table 'tblUserRoles'
ALTER TABLE [dbo].[tblUserRoles]
ADD CONSTRAINT [PK_tblUserRoles]
    PRIMARY KEY CLUSTERED ([UserRoleId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [VehicleId] in table 'tblBookings'
ALTER TABLE [dbo].[tblBookings]
ADD CONSTRAINT [FK_tblBooking_tblItem]
    FOREIGN KEY ([VehicleId])
    REFERENCES [dbo].[tblItems]
        ([VehicleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblBooking_tblItem'
CREATE INDEX [IX_FK_tblBooking_tblItem]
ON [dbo].[tblBookings]
    ([VehicleId]);
GO

-- Creating foreign key on [UserId] in table 'tblBookings'
ALTER TABLE [dbo].[tblBookings]
ADD CONSTRAINT [FK_tblBooking_tblUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblBooking_tblUser'
CREATE INDEX [IX_FK_tblBooking_tblUser]
ON [dbo].[tblBookings]
    ([UserId]);
GO

-- Creating foreign key on [VehicleCategoryId] in table 'tblItems'
ALTER TABLE [dbo].[tblItems]
ADD CONSTRAINT [FK_tblItem_tblCategory]
    FOREIGN KEY ([VehicleCategoryId])
    REFERENCES [dbo].[tblCategories]
        ([VehicleCategoryId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblItem_tblCategory'
CREATE INDEX [IX_FK_tblItem_tblCategory]
ON [dbo].[tblItems]
    ([VehicleCategoryId]);
GO

-- Creating foreign key on [VehicleId] in table 'tblComments'
ALTER TABLE [dbo].[tblComments]
ADD CONSTRAINT [FK_tblComments_tblItem]
    FOREIGN KEY ([VehicleId])
    REFERENCES [dbo].[tblItems]
        ([VehicleId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblComments_tblItem'
CREATE INDEX [IX_FK_tblComments_tblItem]
ON [dbo].[tblComments]
    ([VehicleId]);
GO

-- Creating foreign key on [RoleId] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [FK_tblUser_tblRole]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[tblRoles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblUser_tblRole'
CREATE INDEX [IX_FK_tblUser_tblRole]
ON [dbo].[tblUsers]
    ([RoleId]);
GO

-- Creating foreign key on [RoleId] in table 'tblUserRoles'
ALTER TABLE [dbo].[tblUserRoles]
ADD CONSTRAINT [FK_tblUserRole_tblRole]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[tblRoles]
        ([RoleId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblUserRole_tblRole'
CREATE INDEX [IX_FK_tblUserRole_tblRole]
ON [dbo].[tblUserRoles]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'tblTestimonies'
ALTER TABLE [dbo].[tblTestimonies]
ADD CONSTRAINT [FK_tblTestimony_tblUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblTestimony_tblUser'
CREATE INDEX [IX_FK_tblTestimony_tblUser]
ON [dbo].[tblTestimonies]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'tblUserRoles'
ALTER TABLE [dbo].[tblUserRoles]
ADD CONSTRAINT [FK_tblUserRole_tblUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblUserRole_tblUser'
CREATE INDEX [IX_FK_tblUserRole_tblUser]
ON [dbo].[tblUserRoles]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------