BEGIN TRANSACTION;
ALTER TABLE [Homes] DROP CONSTRAINT [FK_Homes_HomeApporavalStatuses_HomeApprovalStatusId];

ALTER TABLE [HomeApporavalStatuses] DROP CONSTRAINT [PK_HomeApporavalStatuses];

EXEC sp_rename N'[HomeApporavalStatuses]', N'HomeApprovalStatuses', 'OBJECT';

ALTER TABLE [Users] ADD [IsBlocked] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [HomeApprovalStatuses] ADD CONSTRAINT [PK_HomeApprovalStatuses] PRIMARY KEY ([HomeApprovalStatusId]);

ALTER TABLE [Homes] ADD CONSTRAINT [FK_Homes_HomeApprovalStatuses_HomeApprovalStatusId] FOREIGN KEY ([HomeApprovalStatusId]) REFERENCES [HomeApprovalStatuses] ([HomeApprovalStatusId]) ON DELETE NO ACTION;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251207064022_AddIsBlockedToUser', N'9.0.0');

COMMIT;
GO

