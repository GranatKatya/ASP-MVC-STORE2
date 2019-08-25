insert into OrderItems ( UserInfoId, DeliveryMethodId, PaymentMethodId)
values
(4,4,4),
(1,2,2),
(1,3,3),
(1,4,4),
(5,1,5),
(6,6,6),

(1,1,1)


EXEC sp_rename 'OrderItems.UserInfoId', 'UserInfoId2', 'COLUMN';

EXEC sp_rename 'UserInfoes.UserInfoId', 'UserIfoId', 'COLUMN';