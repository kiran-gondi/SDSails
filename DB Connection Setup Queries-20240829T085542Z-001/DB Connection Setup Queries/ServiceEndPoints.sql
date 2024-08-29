USE [ApplicationConfigurations]
GO

/****** Object:  Table [dbo].[ServiceEndPoints]    Script Date: 03/04/2016 16:23:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ServiceEndPoints](
	[ServiceEndPtId] [uniqueidentifier] NOT NULL,
	[ServiceName] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[Binding] [varchar](100) NULL,
 CONSTRAINT [PK_ServiceEndPoints] PRIMARY KEY CLUSTERED 
(
	[ServiceEndPtId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Service End Point' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ServiceEndPoints', @level2type=N'COLUMN',@level2name=N'ServiceEndPtId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Service Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ServiceEndPoints', @level2type=N'COLUMN',@level2name=N'ServiceName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ServiceEndPoints', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Binding' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ServiceEndPoints', @level2type=N'COLUMN',@level2name=N'Binding'
GO


