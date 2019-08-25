CREATE TABLE [dbo].[OrderItems] (
    [OrderIWWWtemId]   INT IDENTITY (1, 1) NOT NULL,
    [UserInfo1Id]      INT NULL,
    [DeliveryMethodId] INT NULL,
    [PaymentMethodId]  INT NULL,
    CONSTRAINT [PK_dbo.OrderItems] PRIMARY KEY CLUSTERED ([OrderIWWWtemId] ASC),
    CONSTRAINT [FK_dbo.OrderItems_dbo.DeliveryMethods_DeliveryMethodId] FOREIGN KEY ([DeliveryMethodId]) REFERENCES [dbo].[DeliveryMethods] ([DeliveryMethodId]),
    CONSTRAINT [FK_dbo.OrderItems_dbo.PaymentMethods_PaymentMethodId] FOREIGN KEY ([PaymentMethodId]) REFERENCES [dbo].[PaymentMethods] ([PaymentMethodId]),
    CONSTRAINT [FK_dbo.OrderItems_dbo.UserInfoes_UserInfoId] FOREIGN KEY ([UserInfo1Id]) REFERENCES [dbo].[UserInfoes] ([UserIfoId])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserInfoId]
    ON [dbo].[OrderItems]([UserInfo1Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DeliveryMethodId]
    ON [dbo].[OrderItems]([DeliveryMethodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PaymentMethodId]
    ON [dbo].[OrderItems]([PaymentMethodId] ASC);

