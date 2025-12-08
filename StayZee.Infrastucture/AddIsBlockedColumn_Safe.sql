-- Script to safely add IsBlocked column to Users table
-- This script checks if the column exists before adding it

IF NOT EXISTS (
    SELECT * FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'[dbo].[Users]') 
    AND name = 'IsBlocked'
)
BEGIN
    ALTER TABLE [Users] 
    ADD [IsBlocked] bit NOT NULL DEFAULT 0;
    
    PRINT 'IsBlocked column added successfully to Users table.';
END
ELSE
BEGIN
    PRINT 'IsBlocked column already exists in Users table.';
END
GO

-- Mark the migration as applied (if not already)
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory] 
    WHERE [MigrationId] = N'20251207070143_AddIsBlockedColumnOnly'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251207070143_AddIsBlockedColumnOnly', N'9.0.0');
    
    PRINT 'Migration marked as applied in EF history.';
END
ELSE
BEGIN
    PRINT 'Migration already exists in EF history.';
END
GO
