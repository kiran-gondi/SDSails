USE [ApplicationConfigurations]
GO

/****** Object:  Table [dbo].[ApplicationSettings]    Script Date: 03/04/2016 16:22:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ApplicationSettings](
	[AppSettingId] [uniqueidentifier] NOT NULL,
	[SettingName] [varchar](50) NULL,
	[SettingVal] [varchar](500) NULL,
	[CreatedTime] [datetime] NULL,
 CONSTRAINT [PK_ApplicationSettings] PRIMARY KEY CLUSTERED 
(
	[AppSettingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application Setting Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationSettings', @level2type=N'COLUMN',@level2name=N'AppSettingId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Setting Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationSettings', @level2type=N'COLUMN',@level2name=N'SettingName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Setting Value' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationSettings', @level2type=N'COLUMN',@level2name=N'SettingVal'
GO

ALTER TABLE [dbo].[ApplicationSettings] ADD  CONSTRAINT [DF_ApplicationSettings_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO


to insert the below data by using the new GUID
AppSettingId	SettingName	SettingVal	CreatedTime
CDFEAB40-7520-43F5-8287-C532E0203922	BindingName	basicHttpBinding	2016-03-03 20:20:49.133