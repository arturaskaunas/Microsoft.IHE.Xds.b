USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'XDSRepositoryDB')
DROP DATABASE [XDSRepositoryDB]
GO
CREATE DATABASE [XDSRepositoryDB] 
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [XDSRepositoryDB].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [XDSRepositoryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [XDSRepositoryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [XDSRepositoryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [XDSRepositoryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [XDSRepositoryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [XDSRepositoryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [XDSRepositoryDB] SET  READ_WRITE 
GO
ALTER DATABASE [XDSRepositoryDB] SET RECOVERY FULL 
GO
ALTER DATABASE [XDSRepositoryDB] SET  MULTI_USER 
GO
ALTER DATABASE [XDSRepositoryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [XDSRepositoryDB] SET DB_CHAINING OFF 


USE [XDSRepositoryDB]


/****** Object:  ForeignKey [FK_AuditMessageParameter_AuditMessage]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_AuditMessageParameter_AuditMessage]') AND parent_object_id = OBJECT_ID(N'[AuditMessageParameterConfiguration]'))
ALTER TABLE [AuditMessageParameterConfiguration] DROP CONSTRAINT [FK_AuditMessageParameter_AuditMessage]
GO
/****** Object:  ForeignKey [FK_DocumentRepositoryMetadata_ContentUUID]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentRepositoryMetadata_ContentUUID]') AND parent_object_id = OBJECT_ID(N'[DocumentRepositoryMetadata]'))
ALTER TABLE [DocumentRepositoryMetadata] DROP CONSTRAINT [FK_DocumentRepositoryMetadata_ContentUUID]
GO
/****** Object:  StoredProcedure [dbo].[LoadContentIDByDocumentID_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadContentIDByDocumentID_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoadContentIDByDocumentID_DocumentRepositoryMetadata]
GO
/****** Object:  StoredProcedure [dbo].[LoadDocumentID_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadDocumentID_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoadDocumentID_DocumentRepositoryMetadata]
GO
/****** Object:  StoredProcedure [dbo].[LoadDocumentHashByDocID_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadDocumentHashByDocID_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoadDocumentHashByDocID_DocumentRepositoryMetadata]
GO
/****** Object:  StoredProcedure [dbo].[SaveRepositoryLogData_DocumentRepositoryLog]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveRepositoryLogData_DocumentRepositoryLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [SaveRepositoryLogData_DocumentRepositoryLog]
GO
/****** Object:  StoredProcedure [dbo].[LoadDocumentContent_DocumentContent]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadDocumentContent_DocumentContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoadDocumentContent_DocumentContent]
GO
/****** Object:  StoredProcedure [dbo].[SaveDocument_DocumentContent]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveDocument_DocumentContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [SaveDocument_DocumentContent]
GO
/****** Object:  StoredProcedure [dbo].[SaveDocumentMetadata_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveDocumentMetadata_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
DROP PROCEDURE [SaveDocumentMetadata_DocumentRepositoryMetadata]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_configurationDetails_ConfigurationEntry]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_configurationDetails_ConfigurationEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_configurationDetails_ConfigurationEntry]
GO
/****** Object:  StoredProcedure [dbo].[SaveDocumentEntryLog_DocumentEntryLog]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveDocumentEntryLog_DocumentEntryLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [SaveDocumentEntryLog_DocumentEntryLog]
GO
/****** Object:  Table [dbo].[AuditMessageParameterConfiguration]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AuditMessageParameterConfiguration]') AND type in (N'U'))
DROP TABLE [AuditMessageParameterConfiguration]
GO
/****** Object:  Table [dbo].[DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentRepositoryMetadata]') AND type in (N'U'))
DROP TABLE [DocumentRepositoryMetadata]
GO
/****** Object:  StoredProcedure [dbo].[LoadRepositoryUniqueId_ConfigurationEntry]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadRepositoryUniqueId_ConfigurationEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoadRepositoryUniqueId_ConfigurationEntry]
GO
/****** Object:  StoredProcedure [dbo].[LoadRepositoryUniqueIDByRepositoryKey_ConfigurationEntry]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadRepositoryUniqueIDByRepositoryKey_ConfigurationEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoadRepositoryUniqueIDByRepositoryKey_ConfigurationEntry]
GO
/****** Object:  Table [dbo].[AuditMessageConfiguration]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AuditMessageConfiguration]') AND type in (N'U'))
DROP TABLE [AuditMessageConfiguration]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_auditMessageConfigurationDetails]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_auditMessageConfigurationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_auditMessageConfigurationDetails]
GO
/****** Object:  UserDefinedFunction [dbo].[USP_SplitString]    Script Date: 12/25/2007 02:28:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[USP_SplitString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [USP_SplitString]
GO
/****** Object:  Table [dbo].[DocumentRepositoryLog]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentRepositoryLog]') AND type in (N'U'))
DROP TABLE [DocumentRepositoryLog]
GO
/****** Object:  Table [dbo].[DocumentContent]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentContent]') AND type in (N'U'))
DROP TABLE [DocumentContent]
GO
/****** Object:  Table [dbo].[ConfigurationEntry]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ConfigurationEntry]') AND type in (N'U'))
DROP TABLE [ConfigurationEntry]
GO
/****** Object:  Table [dbo].[DocumentEntryLog]    Script Date: 12/25/2007 02:28:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntryLog]') AND type in (N'U'))
DROP TABLE [DocumentEntryLog]
GO
/****** Object:  Table [dbo].[DocumentEntryLog]    Script Date: 12/25/2007 02:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntryLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [DocumentEntryLog](
	[DocumentEntryLogID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentRepositoryMetadataID] [int] NULL,
	[LogID] [int] NULL,
 CONSTRAINT [PK_DocumentryEntryLog] PRIMARY KEY CLUSTERED 
(
	[DocumentEntryLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ConfigurationEntry]    Script Date: 12/25/2007 02:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ConfigurationEntry]') AND type in (N'U'))
BEGIN
CREATE TABLE [ConfigurationEntry](
	[ConfigurationEntryID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigurationKey] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConfigurationValue] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ConfigurationEntry] PRIMARY KEY CLUSTERED 
(
	[ConfigurationEntryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [ConfigurationEntry] ON
INSERT [ConfigurationEntry] ([ConfigurationEntryID], [ConfigurationKey], [ConfigurationValue]) VALUES (1, N'validateMimeType', N'true')
INSERT [ConfigurationEntry] ([ConfigurationEntryID], [ConfigurationKey], [ConfigurationValue]) VALUES (2, N'repositoryUniqueID', N'2855d566-21f7-4c4f-b29a-4d59e3020096')
SET IDENTITY_INSERT [ConfigurationEntry] OFF
/****** Object:  Table [dbo].[DocumentContent]    Script Date: 12/25/2007 02:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [DocumentContent](
	[ContentUUIDDocument] [varbinary](max) NULL,
	[ContentID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_DocumentContent] PRIMARY KEY CLUSTERED 
(
	[ContentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[DocumentRepositoryLog]    Script Date: 12/25/2007 02:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentRepositoryLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [DocumentRepositoryLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[RequesterIdentity] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RequestMetadata] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Transaction] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartTime] [datetime] NULL,
	[FinishTime] [datetime] NULL,
	[Result] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_DocumentReposotiryLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  UserDefinedFunction [dbo].[USP_SplitString]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[USP_SplitString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [USP_SplitString](@String nvarchar(4000), @Delimiter char(1))
RETURNS @Results TABLE (Items nvarchar(4000))
AS
 
    BEGIN
    DECLARE @INDEX INT
    DECLARE @SLICE nvarchar(4000)
    -- HAVE TO SET TO 1 SO IT DOESNT EQUAL Z
    --     ERO FIRST TIME IN LOOP
    SELECT @INDEX = 1
    -- following line added 10/06/04 as null
    --      values cause issues
    IF @String IS NULL RETURN
    WHILE @INDEX !=0
 
        BEGIN      
            -- GET THE INDEX OF THE FIRST OCCURENCE OF THE SPLIT CHARACTER
            SELECT @INDEX = CHARINDEX(@Delimiter,@STRING)
            -- NOW PUSH EVERYTHING TO THE LEFT OF IT INTO THE SLICE VARIABLE
            IF @INDEX !=0
                        SELECT @SLICE = LEFT(@STRING,@INDEX - 1)
            ELSE
                       SELECT @SLICE = @STRING
            -- PUT THE ITEM INTO THE RESULTS SET
            INSERT INTO @Results(Items) VALUES(@SLICE)
            -- CHOP THE ITEM REMOVED OFF THE MAIN STRING
            SELECT @STRING = RIGHT(@STRING,LEN(@STRING) - @INDEX)
            -- BREAK OUT IF WE ARE DONE
            IF LEN(@STRING) = 0 BREAK
    END

    RETURN

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_auditMessageConfigurationDetails]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_auditMessageConfigurationDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_auditMessageConfigurationDetails] 
	@messageKey varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT AMC.auditMessageID, AMC.messageKey, AMC.messageValue, AMPC.parameterID, AMPC.auditMessageID, AMPC.parameterName, AMPC.parameterType, AMPC.parameterValue
FROM AuditMessageConfiguration AMC
INNER JOIN AuditMessageParameterConfiguration AMPC
ON AMPC.auditMessageID = AMC.auditMessageID
WHERE AMC.messageKey = @messageKey

END
' 
END
GO
/****** Object:  Table [dbo].[AuditMessageConfiguration]    Script Date: 12/25/2007 02:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AuditMessageConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [AuditMessageConfiguration](
	[auditMessageID] [int] IDENTITY(1,1) NOT NULL,
	[messageKey] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[messageValue] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_AuditMessage] PRIMARY KEY CLUSTERED 
(
	[auditMessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [AuditMessageConfiguration] ON
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (1, N'REPOSITORY-APP-START-ITI-20', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="E" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110100" displayName="Application Activity" codeSystemName="DCM"/><EventTypeCode code="110120" displayName="Application Start" codeSystemName="DCM" /></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID$" UserIsRequestor="false"><RoleIDCode code="110150" codeSystemName="DCM" displayName="Application" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (2, N'REPOSITORY-APP-STOP-ITI-20', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="E" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110100" displayName="Application Activity" codeSystemName="DCM"/><EventTypeCode code="110121" displayName="Application Stop" codeSystemName="DCM" /></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID$" UserIsRequestor="false"><RoleIDCode code="110150" codeSystemName="DCM" displayName="Application" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (3, N'REPOSITORY-P-AND-R-IMPORT-ITI-41', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="R" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110107" displayName="Import" codeSystemName="DCM"/><EventTypeCode code="ITI-41" codeSystemName="IHE Transactions" displayName="Provide and Register Document Set - b"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source" /></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$SubmissionSet.UniqueID$" ParticipantObjectTypeCode="2" ParticipantObjectTypeCodeRole="20"><ParticipantObjectIDTypeCode code="$SubmissionSet.ClassificationNode.UUID$" /></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (4, N'REPOSITORY-RDS-EXPORT-ITI-42', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="R" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110106" displayName="Export" codeSystemName="DCM"/><EventTypeCode code="ITI-42" codeSystemName="IHE Transactions" displayName="Register Document Set - b"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source" /></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$SubmissionSet.UniqueID$" ParticipantObjectTypeCode="2" ParticipantObjectTypeCodeRole="20"><ParticipantObjectIDTypeCode code="$SubmissionSet.ClassificationNode.UUID$" /></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (5, N'REPOSITORY-RDS-EXPORT-ITI-43', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="R" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110106" displayName="Export" codeSystemName="DCM"/><EventTypeCode code="ITI-43" codeSystemName="IHE Transactions" displayName="Retrieve Document Set"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source"/></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination"/></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$DocumentEntry.UUID$" ParticipantObjectTypeCode="3" ParticipantObjectTypeCodeRole="12"><ParticipantObjectIDTypeCode code="$Document.UUID$"/></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (6, N'REGISTRY-APP-START-ITI-20', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="E" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110100" displayName="Application Activity" codeSystemName="DCM"/><EventTypeCode code="110120" displayName="Application Start" codeSystemName="DCM" /></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID$" UserIsRequestor="false"><RoleIDCode code="110150" codeSystemName="DCM" displayName="Application" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (7, N'REGISTRY-APP-STOP-ITI-20', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="E" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110100" displayName="Application Activity" codeSystemName="DCM"/><EventTypeCode code="110121" displayName="Application Stop" codeSystemName="DCM" /></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID$" UserIsRequestor="false"><RoleIDCode code="110150" codeSystemName="DCM" displayName="Application" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (8, N'REGISTRY-RDS-IMPORT-ITI-42', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="R" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110106" displayName="Export" codeSystemName="DCM"/><EventTypeCode code="ITI-42" codeSystemName="IHE Transactions" displayName="Register Document Set - b"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source" /></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$SubmissionSet.UniqueID$" ParticipantObjectTypeCode="2" ParticipantObjectTypeCodeRole="20"><ParticipantObjectIDTypeCode code="$SubmissionSet.ClassificationNode.UUID$" /></ParticipantObjectIdentification></AuditMessage>')
SET IDENTITY_INSERT [AuditMessageConfiguration] OFF
/****** Object:  StoredProcedure [dbo].[LoadRepositoryUniqueIDByRepositoryKey_ConfigurationEntry]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadRepositoryUniqueIDByRepositoryKey_ConfigurationEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Get RepositoryUniqueID Value for the given Key
-- =====================================================================

CREATE PROCEDURE [LoadRepositoryUniqueIDByRepositoryKey_ConfigurationEntry]
			
			@RepositoryKey 	varchar(128)
			
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ConfigurationValue FROM ConfigurationEntry
	WHERE ConfigurationKey = @RepositoryKey
		
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[LoadRepositoryUniqueId_ConfigurationEntry]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadRepositoryUniqueId_ConfigurationEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Get RepositoryUniqueID Value for the given RepositoryUniqueId
-- =====================================================================

CREATE PROCEDURE [LoadRepositoryUniqueId_ConfigurationEntry]
			
			@RepositoryUniqueId 	varchar(128)
			
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ConfigurationValue FROM ConfigurationEntry
	WHERE ConfigurationValue=@RepositoryUniqueId
	
END
' 
END
GO
/****** Object:  Table [dbo].[DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentRepositoryMetadata]') AND type in (N'U'))
BEGIN
CREATE TABLE [DocumentRepositoryMetadata](
	[DocumentEntryUUID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocumentEntryURI] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocumentEntryHash] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocumentEntrySize] [float] NULL,
	[DocumentEntryMimeType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocumentRepositoryMetadataID] [int] IDENTITY(1,1) NOT NULL,
	[ContentUUID] [int] NULL,
	[DocumentName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_DocumentRepositoryMetadata] PRIMARY KEY CLUSTERED 
(
	[DocumentRepositoryMetadataID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[AuditMessageParameterConfiguration]    Script Date: 12/25/2007 02:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AuditMessageParameterConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [AuditMessageParameterConfiguration](
	[parameterID] [int] IDENTITY(1,1) NOT NULL,
	[auditMessageID] [int] NOT NULL,
	[parameterName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[parameterType] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[parameterValue] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
END
GO
SET IDENTITY_INSERT [AuditMessageParameterConfiguration] ON
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (1, 1, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (2, 1, N'$ActiveParticipant.UserID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (3, 1, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (4, 2, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (5, 2, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (6, 2, N'$ActiveParticipant.UserID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (7, 2, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (8, 3, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (9, 3, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (10, 3, N'$ActiveParticipant.UserID.Source$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (11, 3, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (12, 3, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (13, 3, N'$SubmissionSet.UniqueID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (14, 3, N'$SubmissionSet.ClassificationNode.UUID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (15, 4, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (16, 4, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (17, 4, N'$ActiveParticipant.UserID.Source$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (18, 4, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (19, 4, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (20, 4, N'$SubmissionSet.UniqueID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (21, 4, N'$SubmissionSet.ClassificationNode.UUID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (22, 5, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (23, 5, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (24, 5, N'$ActiveParticipant.UserID.Source$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (25, 5, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (26, 5, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (27, 5, N'$DocumentEntry.UUID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (28, 5, N'$Document.UUID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (29, 6, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (30, 6, N'$ActiveParticipant.UserID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (31, 6, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (32, 7, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (33, 7, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (34, 7, N'$ActiveParticipant.UserID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (35, 7, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (36, 8, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (37, 8, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (38, 8, N'$ActiveParticipant.UserID.Source$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (39, 8, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (40, 8, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (41, 8, N'$SubmissionSet.UniqueID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (42, 8, N'$SubmissionSet.ClassificationNode.UUID$', N'DYNAMIC', NULL)
SET IDENTITY_INSERT [AuditMessageParameterConfiguration] OFF
/****** Object:  StoredProcedure [dbo].[SaveDocumentEntryLog_DocumentEntryLog]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveDocumentEntryLog_DocumentEntryLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Log Repository MetadataID and Log ID
-- =====================================================================

CREATE PROCEDURE [SaveDocumentEntryLog_DocumentEntryLog]
			
AS
BEGIN

DECLARE @DocumentRepositoryMetadataID int
DECLARE @LogID int

select @DocumentRepositoryMetadataID=max(DocumentRepositoryMetadataID) from DocumentRepositoryMetadata

select @LogID=max(LogID) from DocumentRepositoryLog

INSERT INTO DocumentEntryLog
		 (	
			DocumentRepositoryMetadataID ,
			LogID
		)
VALUES
		(
			@DocumentRepositoryMetadataID ,
			@LogID
			
		) 

-- Returns the DocumentEntry UUID as the unique storag identofier 
--	for the inserted COntent ID
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_configurationDetails_ConfigurationEntry]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_configurationDetails_ConfigurationEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_configurationDetails_ConfigurationEntry] 
	-- Add the parameters for the stored procedure here
	@configurationKey varchar(50) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ConfigurationEntryID, ConfigurationKey, ConfigurationValue
	FROM ConfigurationEntry
	WHERE ConfigurationKey = @configurationKey

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SaveDocumentMetadata_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveDocumentMetadata_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Document Binary Data in DOcument Content Table Table
-- =====================================================================

CREATE PROCEDURE [SaveDocumentMetadata_DocumentRepositoryMetadata]
			
			@DocumentEntryUUID 	varchar(128),
			@DocumentEntryURI varchar(128),
			@DocumentEntryHash varchar(128),
			@DocumentEntrySize float ,
			@DocumentEntryMimeType varchar(50),
			@DocumentName varchar(25)
			
AS
BEGIN

declare @ContentUUID 	int

SELECT @ContentUUID=max(ContentID) from DocumentContent

INSERT INTO DocumentRepositoryMetadata
		 (	
			DocumentEntryUUID,
			DocumentEntryURI,
			DocumentEntryHash,
			DocumentEntrySize,
			DocumentEntryMimeType,
			ContentUUID,
			DocumentName
		)
VALUES
		(
			@DocumentEntryUUID,
			@DocumentEntryURI,
			@DocumentEntryHash,
			@DocumentEntrySize,
			@DocumentEntryMimeType,
			@ContentUUID,
			@DocumentName
			
		) 

-- Returns the DocumentEntry UUID as the unique storag identofier 
--	for the inserted COntent ID

	SELECT @DocumentEntryUUID = DocumentEntryUUID from DocumentRepositoryMetadata 
	WHERE ContentUUID = @ContentUUID
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SaveDocument_DocumentContent]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveDocument_DocumentContent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Document Binary Data in DOcument Content Table Table
-- =====================================================================

CREATE PROCEDURE [SaveDocument_DocumentContent]
			
			@ContentUUIDDocument varbinary(max),
			@ContentUUID int OUTPUT
			

AS
BEGIN

INSERT INTO DocumentContent(ContentUUIDDocument)
VALUES(@ContentUUIDDocument)

-- Return Sponsor ID

SELECT @ContentUUID = max(ContentID) from  DocumentContent
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[LoadDocumentContent_DocumentContent]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadDocumentContent_DocumentContent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Get RepositoryUniqueID Value for the given RepositoryUniqueId
-- =====================================================================

CREATE PROCEDURE [LoadDocumentContent_DocumentContent]
			
			@ContentID 	varchar(128)
			
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ContentUUIDDocument FROM DocumentContent
	WHERE ContentID = @ContentID
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SaveRepositoryLogData_DocumentRepositoryLog]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SaveRepositoryLogData_DocumentRepositoryLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Log Repository MetadataID and Log ID
-- =====================================================================

CREATE PROCEDURE [SaveRepositoryLogData_DocumentRepositoryLog]
			
			@RequesterIdentity varchar(50),
			@RequestMetadata text,
			@Transaction varchar(128),
			@StartTime datetime,
			@FinishTime datetime,
			@Result varchar(128)

AS
BEGIN

INSERT INTO DocumentRepositoryLog
		 (	
			RequesterIdentity,
			RequestMetadata,
			[Transaction],
			StartTime,
			FinishTime,
			Result
		)
VALUES
		(
@RequesterIdentity,
			@RequestMetadata,
			@Transaction,
			@StartTime,
			@FinishTime,
			@Result
		) 

-- Returns the DocumentEntry UUID as the unique storag identofier 
--	for the inserted COntent ID
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[LoadDocumentHashByDocID_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadDocumentHashByDocID_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Get hash Code for the given unique ID
-- =====================================================================

CREATE PROCEDURE [LoadDocumentHashByDocID_DocumentRepositoryMetadata]
			
			@DocumentEntryUUID 	varchar(128)
			
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DocumentEntryHash FROM DocumentRepositoryMetadata
	WHERE DocumentEntryUUID = @DocumentEntryUUID
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[LoadDocumentID_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadDocumentID_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Get uniqueid for the given unique ID
-- =====================================================================

CREATE PROCEDURE [LoadDocumentID_DocumentRepositoryMetadata]
			
			@DocumentEntryUUID 	varchar(128)
			
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DocumentEntryUUID FROM DocumentRepositoryMetadata
	WHERE DocumentEntryUUID = @DocumentEntryUUID
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[LoadContentIDByDocumentID_DocumentRepositoryMetadata]    Script Date: 12/25/2007 02:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LoadContentIDByDocumentID_DocumentRepositoryMetadata]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		B Anil kumar
-- Description:	To Get RepositoryUniqueID Value for the given RepositoryUniqueId
-- =====================================================================

CREATE PROCEDURE [LoadContentIDByDocumentID_DocumentRepositoryMetadata]
			
			@DocumentID 	varchar(128)
			
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DocumentEntryUUID, DocumentEntryURI, DocumentEntryHash, DocumentEntrySize, 
			DocumentEntryMimeType, DocumentRepositoryMetadataID, ContentUUID, DocumentName
	FROM DocumentRepositoryMetadata		
	WHERE DocumentEntryUUID = @DocumentID

END
' 
END
GO
/****** Object:  ForeignKey [FK_AuditMessageParameter_AuditMessage]    Script Date: 12/25/2007 02:28:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_AuditMessageParameter_AuditMessage]') AND parent_object_id = OBJECT_ID(N'[AuditMessageParameterConfiguration]'))
ALTER TABLE [AuditMessageParameterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_AuditMessageParameter_AuditMessage] FOREIGN KEY([auditMessageID])
REFERENCES [AuditMessageConfiguration] ([auditMessageID])
GO
ALTER TABLE [AuditMessageParameterConfiguration] CHECK CONSTRAINT [FK_AuditMessageParameter_AuditMessage]
GO
/****** Object:  ForeignKey [FK_DocumentRepositoryMetadata_ContentUUID]    Script Date: 12/25/2007 02:28:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentRepositoryMetadata_ContentUUID]') AND parent_object_id = OBJECT_ID(N'[DocumentRepositoryMetadata]'))
ALTER TABLE [DocumentRepositoryMetadata]  WITH CHECK ADD  CONSTRAINT [FK_DocumentRepositoryMetadata_ContentUUID] FOREIGN KEY([ContentUUID])
REFERENCES [DocumentContent] ([ContentID])
GO
ALTER TABLE [DocumentRepositoryMetadata] CHECK CONSTRAINT [FK_DocumentRepositoryMetadata_ContentUUID]
GO
