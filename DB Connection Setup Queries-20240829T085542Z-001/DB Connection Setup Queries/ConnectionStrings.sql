USE [ApplicationConfigurations]
GO

/****** Object:  Table [dbo].[ConnectionStrings]    Script Date: 03/04/2016 16:22:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConnectionStrings](
	[ConnectionId] [uniqueidentifier] NOT NULL,
	[DBName] [varchar](50) NULL,
	[ConnectionString] [varchar](200) NULL,
	[ProviderName] [varchar](100) NULL,
 CONSTRAINT [PK_ConnectionStrings] PRIMARY KEY CLUSTERED 
(
	[ConnectionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


to insert below data using insert statements
ConnectionId	DBName	ConnectionString	ProviderName
961119D8-AB34-486A-83F1-E5A512BA12D9	CPT_WO_SD	Data Source=54.154.28.148;Initial Catalog=CPT_WO_SD;User ID=sa;Pwd=1qaz2wsx;	System.Data.SqlClient


insert into [ApplicationConfigurations].[dbo].[ConnectionStrings] values
('36A541EA-FDBA-45ED-9315-64A82FD67A8E',
 'CPT_WO_SD_Dev',	
 'Data Source=54.154.28.148;Initial Catalog=CPT_WO_SD;User ID=sa;Pwd=1qaz2wsx;',
 'System.Data.SqlClient')  