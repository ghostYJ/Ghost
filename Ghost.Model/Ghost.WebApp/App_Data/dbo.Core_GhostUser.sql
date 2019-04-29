SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Core_GhostUser](
	[U_Id] [bigint] NOT NULL,
	[U_NickName] [nvarchar](50) NULL,
	[U_RealName] [nvarchar](50) NULL,
	[U_Sex] [int] NULL,
	[U_Mobile] [nvarchar](50) NULL,
	[U_PassWord] [nvarchar](50) NULL,
	[U_Email] [nvarchar](50) NULL,
	[U_Province] [nvarchar](50) NULL,
	[U_City] [nvarchar](50) NULL,
	[U_BirthDay] [datetime] NULL,
	[U_IsVip] [bit] NULL,
	[U_VipLevel] [int] NULL,
	[U_AccountBalance] [decimal](18, 2) NULL,
	[U_IsDisabled] [bit] NULL,
	[U_RecordTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Login_GhostUser] PRIMARY KEY CLUSTERED 
(
	[U_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO