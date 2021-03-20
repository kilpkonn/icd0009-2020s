### Database document
> 2021-03-20 16:53:13
#### __EFMigrationsHistory  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|MigrationId| |NVARCHAR(150)|Y|
2| |ProductVersion| |NVARCHAR(32)|Y|
#### AspNetRoleClaims  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |INT|Y|
2| |RoleId| |UNIQUEIDENTIFIER|Y|
3| |ClaimType| |NVARCHAR(MAX)|N|
4| |ClaimValue| |NVARCHAR(MAX)|N|
#### AspNetRoles  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |Name| |NVARCHAR(256)|N|
3| |NormalizedName| |NVARCHAR(256)|N|
4| |ConcurrencyStamp| |NVARCHAR(MAX)|N|
#### AspNetUserClaims  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |INT|Y|
2| |UserId| |UNIQUEIDENTIFIER|Y|
3| |ClaimType| |NVARCHAR(MAX)|N|
4| |ClaimValue| |NVARCHAR(MAX)|N|
#### AspNetUserLogins  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|LoginProvider| |NVARCHAR(450)|Y|
2|PRI|ProviderKey| |NVARCHAR(450)|Y|
3| |ProviderDisplayName| |NVARCHAR(MAX)|N|
4| |UserId| |UNIQUEIDENTIFIER|Y|
#### AspNetUserRoles  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|UserId| |UNIQUEIDENTIFIER|Y|
2|PRI|RoleId| |UNIQUEIDENTIFIER|Y|
#### AspNetUsers  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |DisplayName| |NVARCHAR(64)|Y|
3| |UserName| |NVARCHAR(256)|N|
4| |NormalizedUserName| |NVARCHAR(256)|N|
5| |Email| |NVARCHAR(256)|N|
6| |NormalizedEmail| |NVARCHAR(256)|N|
7| |EmailConfirmed| |BIT|Y|
8| |PasswordHash| |NVARCHAR(MAX)|N|
9| |SecurityStamp| |NVARCHAR(MAX)|N|
10| |ConcurrencyStamp| |NVARCHAR(MAX)|N|
11| |PhoneNumber| |NVARCHAR(MAX)|N|
12| |PhoneNumberConfirmed| |BIT|Y|
13| |TwoFactorEnabled| |BIT|Y|
14| |LockoutEnd| |DATETIMEOFFSET|N|
15| |LockoutEnabled| |BIT|Y|
16| |AccessFailedCount| |INT|Y|
#### AspNetUserTokens  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|UserId| |UNIQUEIDENTIFIER|Y|
2|PRI|LoginProvider| |NVARCHAR(450)|Y|
3|PRI|Name| |NVARCHAR(450)|Y|
4| |Value| |NVARCHAR(MAX)|N|
#### CarAccesses  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |AppUserId| |UNIQUEIDENTIFIER|Y|
3| |CarId| |UNIQUEIDENTIFIER|Y|
4| |CarAccessTypeId| |UNIQUEIDENTIFIER|Y|
5| |CreatedBy| |UNIQUEIDENTIFIER|Y|
6| |CreatedAt| |DATETIME2|Y|
7| |UpdatedBy| |UNIQUEIDENTIFIER|Y|
8| |UpdatedAt| |DATETIME2|Y|
#### CarAccessTypes  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |Name| |NVARCHAR(MAX)|Y|
3| |AccessLevel| |INT|Y|
#### CarErrorCodes  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |CanId| |INT|Y|
3| |CanData| |BIGINT|Y|
4| |CarId| |UNIQUEIDENTIFIER|Y|
5| |CreatedBy| |UNIQUEIDENTIFIER|Y|
6| |CreatedAt| |DATETIME2|Y|
7| |UpdatedBy| |UNIQUEIDENTIFIER|Y|
8| |UpdatedAt| |DATETIME2|Y|
#### CarMarks  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |Name| |NVARCHAR(MAX)|Y|
3| |CreatedBy| |UNIQUEIDENTIFIER|Y|
4| |CreatedAt| |DATETIME2|Y|
5| |UpdatedBy| |UNIQUEIDENTIFIER|Y|
6| |UpdatedAt| |DATETIME2|Y|
#### CarModels  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |Name| |NVARCHAR(MAX)|Y|
3| |ReleaseDate| |DATETIME2|Y|
4| |CarMarkId| |UNIQUEIDENTIFIER|Y|
5| |CreatedBy| |UNIQUEIDENTIFIER|Y|
6| |CreatedAt| |DATETIME2|Y|
7| |UpdatedBy| |UNIQUEIDENTIFIER|Y|
8| |UpdatedAt| |DATETIME2|Y|
#### Cars  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |CarModelId| |UNIQUEIDENTIFIER|Y|
3| |AppUserId| |UNIQUEIDENTIFIER|Y|
4| |CreatedBy| |UNIQUEIDENTIFIER|Y|
5| |CreatedAt| |DATETIME2|Y|
6| |UpdatedBy| |UNIQUEIDENTIFIER|Y|
7| |UpdatedAt| |DATETIME2|Y|
#### CarTypes  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |Name| |NVARCHAR(MAX)|Y|
3| |CarModelId| |UNIQUEIDENTIFIER|Y|
4| |CreatedBy| |UNIQUEIDENTIFIER|Y|
5| |CreatedAt| |DATETIME2|Y|
6| |UpdatedBy| |UNIQUEIDENTIFIER|Y|
7| |UpdatedAt| |DATETIME2|Y|
#### GasRefills  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |AmountRefilled| |REAL|Y|
3| |Timestamp| |DATETIME2|Y|
4| |Cost| |REAL|Y|
5| |AppUserId| |UNIQUEIDENTIFIER|Y|
6| |CarId| |UNIQUEIDENTIFIER|N|
#### TrackLocations  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |Lat| |FLOAT|Y|
3| |Lng| |FLOAT|Y|
4| |Elevation| |REAL|Y|
5| |Accuracy| |REAL|Y|
6| |ElevationAccuracy| |REAL|Y|
7| |Speed| |REAL|Y|
8| |Rpm| |REAL|Y|
9| |TrackId| |UNIQUEIDENTIFIER|N|
#### Tracks  
NO | KEY | COLUMN | COMMENT | DATA_TYPE | NOTNULL | REMARK
:---: | :---: | --- | --- | --- | :---: | ---
1|PRI|Id| |UNIQUEIDENTIFIER|Y|
2| |StartTimestamp| |DATETIME2|Y|
3| |EndTimestamp| |DATETIME2|Y|
4| |Distance| |REAL|Y|
5| |CarId| |UNIQUEIDENTIFIER|Y|
6| |AppUserId| |UNIQUEIDENTIFIER|Y|
7| |CreatedBy| |UNIQUEIDENTIFIER|Y|
8| |CreatedAt| |DATETIME2|Y|
9| |UpdatedBy| |UNIQUEIDENTIFIER|Y|
10| |UpdatedAt| |DATETIME2|Y|
