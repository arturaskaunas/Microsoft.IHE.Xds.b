USE [master]
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'XDSRegistryDB')
DROP DATABASE [XDSRegistryDB]
GO

CREATE DATABASE [XDSRegistryDB]
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [XDSRegistryDB].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO

ALTER DATABASE [XDSRegistryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [XDSRegistryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [XDSRegistryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [XDSRegistryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [XDSRegistryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [XDSRegistryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [XDSRegistryDB] SET  READ_WRITE 
GO
ALTER DATABASE [XDSRegistryDB] SET RECOVERY FULL 
GO
ALTER DATABASE [XDSRegistryDB] SET  MULTI_USER 
GO
ALTER DATABASE [XDSRegistryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [XDSRegistryDB] SET DB_CHAINING OFF
GO

USE [XDSRegistryDB]

/****** Object:  ForeignKey [FK_AuditMessageParameter_AuditMessage]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_AuditMessageParameter_AuditMessage]') AND parent_object_id = OBJECT_ID(N'[AuditMessageParameterConfiguration]'))
ALTER TABLE [AuditMessageParameterConfiguration] DROP CONSTRAINT [FK_AuditMessageParameter_AuditMessage]
GO
/****** Object:  ForeignKey [FK_CodeValue_CodeType]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CodeValue_CodeType]') AND parent_object_id = OBJECT_ID(N'[CodeValue]'))
ALTER TABLE [CodeValue] DROP CONSTRAINT [FK_CodeValue_CodeType]
GO
/****** Object:  ForeignKey [FK_DocumentEntry_Patient]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntry_Patient]') AND parent_object_id = OBJECT_ID(N'[DocumentEntry]'))
ALTER TABLE [DocumentEntry] DROP CONSTRAINT [FK_DocumentEntry_Patient]
GO
/****** Object:  ForeignKey [FK_DocumentEntryAuthor_Author]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntryAuthor_Author]') AND parent_object_id = OBJECT_ID(N'[DocumentEntryAuthor]'))
ALTER TABLE [DocumentEntryAuthor] DROP CONSTRAINT [FK_DocumentEntryAuthor_Author]
GO
/****** Object:  ForeignKey [FK_DocumentEntryAuthor_DocumentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntryAuthor_DocumentEntry]') AND parent_object_id = OBJECT_ID(N'[DocumentEntryAuthor]'))
ALTER TABLE [DocumentEntryAuthor] DROP CONSTRAINT [FK_DocumentEntryAuthor_DocumentEntry]
GO
/****** Object:  ForeignKey [FK_DocumentEntryEventCodeList_DocumentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntryEventCodeList_DocumentEntry]') AND parent_object_id = OBJECT_ID(N'[DocumentEntryEventCodeList]'))
ALTER TABLE [DocumentEntryEventCodeList] DROP CONSTRAINT [FK_DocumentEntryEventCodeList_DocumentEntry]
GO
/****** Object:  ForeignKey [FK_Folder_Patient]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Folder_Patient]') AND parent_object_id = OBJECT_ID(N'[Folder]'))
ALTER TABLE [Folder] DROP CONSTRAINT [FK_Folder_Patient]
GO
/****** Object:  ForeignKey [FK_FolderCodeList_Folder]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_FolderCodeList_Folder]') AND parent_object_id = OBJECT_ID(N'[FolderCodeList]'))
ALTER TABLE [FolderCodeList] DROP CONSTRAINT [FK_FolderCodeList_Folder]
GO
/****** Object:  ForeignKey [FK_SubmissionSet_Patient]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSet_Patient]') AND parent_object_id = OBJECT_ID(N'[SubmissionSet]'))
ALTER TABLE [SubmissionSet] DROP CONSTRAINT [FK_SubmissionSet_Patient]
GO
/****** Object:  ForeignKey [FK_SubmissionSetAuthor_Author]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetAuthor_Author]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetAuthor]'))
ALTER TABLE [SubmissionSetAuthor] DROP CONSTRAINT [FK_SubmissionSetAuthor_Author]
GO
/****** Object:  ForeignKey [FK_SubmissionSetAuthor_SubmissionSet]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetAuthor_SubmissionSet]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetAuthor]'))
ALTER TABLE [SubmissionSetAuthor] DROP CONSTRAINT [FK_SubmissionSetAuthor_SubmissionSet]
GO
/****** Object:  ForeignKey [FK_SubmissionSetDocumentFolder_DocumentEntry]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetDocumentFolder_DocumentEntry]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]'))
ALTER TABLE [SubmissionSetDocumentFolder] DROP CONSTRAINT [FK_SubmissionSetDocumentFolder_DocumentEntry]
GO
/****** Object:  ForeignKey [FK_SubmissionSetDocumentFolder_Folder]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetDocumentFolder_Folder]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]'))
ALTER TABLE [SubmissionSetDocumentFolder] DROP CONSTRAINT [FK_SubmissionSetDocumentFolder_Folder]
GO
/****** Object:  ForeignKey [FK_SubmissionSetDocumentFolder_SubmissionSet]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetDocumentFolder_SubmissionSet]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]'))
ALTER TABLE [SubmissionSetDocumentFolder] DROP CONSTRAINT [FK_SubmissionSetDocumentFolder_SubmissionSet]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_configurationDetails_ConfigurationEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_configurationDetails_ConfigurationEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_configurationDetails_ConfigurationEntry]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetAuthor_by_submissionSetID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetAuthor_by_submissionSetID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_SubmissionSetAuthor_by_submissionSetID]
GO
/****** Object:  StoredProcedure [dbo].[usp_document_author_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_document_author_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_document_author_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_submissionSet_author_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_submissionSet_author_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_submissionSet_author_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_PatientMessageConfiguration_get]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PatientMessageConfiguration_get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PatientMessageConfiguration_get]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetDocumentFolder_By_EntryUUID_Or_UniqueID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetDocumentFolder_By_EntryUUID_Or_UniqueID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_SubmissionSetDocumentFolder_By_EntryUUID_Or_UniqueID]
GO
/****** Object:  StoredProcedure [dbo].[usp_submissionSetDocumentFolder_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_submissionSetDocumentFolder_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_submissionSetDocumentFolder_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentEntryEventCodeList_By_DocumentEntryID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentEntryEventCodeList_By_DocumentEntryID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_DocumentEntryEventCodeList_By_DocumentEntryID]
GO
/****** Object:  StoredProcedure [dbo].[usp_update_patientUID_Folder]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_patientUID_Folder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_update_patientUID_Folder]
GO
/****** Object:  StoredProcedure [dbo].[usp_folder_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_folder_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_folder_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetAndAssociation_By_entryUUID_Or_uniqueID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetAndAssociation_By_entryUUID_Or_uniqueID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_SubmissionSetAndAssociation_By_entryUUID_Or_uniqueID]
GO
/****** Object:  StoredProcedure [dbo].[usp_submissionSet_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_submissionSet_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_submissionSet_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_update_patientUID_SubmissionSet]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_patientUID_SubmissionSet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_update_patientUID_SubmissionSet]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_xmlEntries]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_xmlEntries]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_xmlEntries]
GO
/****** Object:  StoredProcedure [dbo].[usp_update_AvailabilityStatus_ExtrinsicObjectXml_documentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_AvailabilityStatus_ExtrinsicObjectXml_documentEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_update_AvailabilityStatus_ExtrinsicObjectXml_documentEntry]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_documentEntryXml]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_documentEntryXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_documentEntryXml]
GO
/****** Object:  StoredProcedure [dbo].[usp_documentEntry_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_documentEntry_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_documentEntry_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_set_DocuementAvailability]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_set_DocuementAvailability]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_set_DocuementAvailability]
GO
/****** Object:  StoredProcedure [dbo].[usp_update_patientUID_documentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_patientUID_documentEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_update_patientUID_documentEntry]
GO
/****** Object:  StoredProcedure [dbo].[usp_update_AvailabilityStatus_documentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_AvailabilityStatus_documentEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_update_AvailabilityStatus_documentEntry]
GO
/****** Object:  StoredProcedure [dbo].[usp_registryLog_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_registryLog_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_registryLog_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_Folders_By_entryUUID_Or_uniqueID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_Folders_By_entryUUID_Or_uniqueID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_Folders_By_entryUUID_Or_uniqueID]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_AssociationsBy_sourceObject_Or_targetObject]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_AssociationsBy_sourceObject_Or_targetObject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_AssociationsBy_sourceObject_Or_targetObject]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_FolderCodeList_By_folderIDS]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_FolderCodeList_By_folderIDS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_FolderCodeList_By_folderIDS]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_Association_For_folderIDs]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_Association_For_folderIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_Association_For_folderIDs]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_AssociationsBy_EntryUUID_Or_UniqueID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_AssociationsBy_EntryUUID_Or_UniqueID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_AssociationsBy_EntryUUID_Or_UniqueID]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_Folders_By_FolderIDs]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_Folders_By_FolderIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_Folders_By_FolderIDs]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetDocumentFolder_By_ID_And_associationType]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetDocumentFolder_By_ID_And_associationType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_SubmissionSetDocumentFolder_By_ID_And_associationType]
GO
/****** Object:  StoredProcedure [dbo].[usp_UniqueID_Duplicate_select]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_UniqueID_Duplicate_select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_UniqueID_Duplicate_select]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetsByTargetObjectIDs]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetsByTargetObjectIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_SubmissionSetsByTargetObjectIDs]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentsAndAssociations]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentsAndAssociations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_DocumentsAndAssociations]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentEntry_By_documentEntryIDs]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentEntry_By_documentEntryIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_DocumentEntry_By_documentEntryIDs]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentsBy_EntryUUID_Or_UniqueID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentsBy_EntryUUID_Or_UniqueID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_DocumentsBy_EntryUUID_Or_UniqueID]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_codeDetails_CodeType_CodeValue]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_codeDetails_CodeType_CodeValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_codeDetails_CodeType_CodeValue]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_folderDetails_Folder_Patient]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_folderDetails_Folder_Patient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_folderDetails_Folder_Patient]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_submissionSetDetails_SubmissionSet_Patient]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_submissionSetDetails_SubmissionSet_Patient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_submissionSetDetails_SubmissionSet_Patient]
GO
/****** Object:  StoredProcedure [dbo].[usp_update_PatientIDs]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_PatientIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_update_PatientIDs]
GO
/****** Object:  StoredProcedure [dbo].[usp_delete_PatientUID_Patient]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_delete_PatientUID_Patient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_delete_PatientUID_Patient]
GO
/****** Object:  StoredProcedure [dbo].[usp_patient_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_patient_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_patient_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_PatientID_Patient]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_PatientID_Patient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_PatientID_Patient]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_patient_Patientid]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_patient_Patientid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_patient_Patientid]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_documentEntryDetails_DocumentEntry_Patient]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_documentEntryDetails_DocumentEntry_Patient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_documentEntryDetails_DocumentEntry_Patient]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_storedQueryDetails_StoredQuery_StoredQueryParameter]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_storedQueryDetails_StoredQuery_StoredQueryParameter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_storedQueryDetails_StoredQuery_StoredQueryParameter]
GO
/****** Object:  Table [dbo].[FolderCodeList]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[FolderCodeList]') AND type in (N'U'))
DROP TABLE [FolderCodeList]
GO
/****** Object:  Table [dbo].[SubmissionSetAuthor]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SubmissionSetAuthor]') AND type in (N'U'))
DROP TABLE [SubmissionSetAuthor]
GO
/****** Object:  Table [dbo].[DocumentEntryAuthor]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntryAuthor]') AND type in (N'U'))
DROP TABLE [DocumentEntryAuthor]
GO
/****** Object:  Table [dbo].[SubmissionSetDocumentFolder]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]') AND type in (N'U'))
DROP TABLE [SubmissionSetDocumentFolder]
GO
/****** Object:  Table [dbo].[DocumentEntryEventCodeList]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntryEventCodeList]') AND type in (N'U'))
DROP TABLE [DocumentEntryEventCodeList]
GO
/****** Object:  Table [dbo].[AuditMessageParameterConfiguration]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AuditMessageParameterConfiguration]') AND type in (N'U'))
DROP TABLE [AuditMessageParameterConfiguration]
GO
/****** Object:  Table [dbo].[CodeValue]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CodeValue]') AND type in (N'U'))
DROP TABLE [CodeValue]
GO
/****** Object:  Table [dbo].[Folder]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Folder]') AND type in (N'U'))
DROP TABLE [Folder]
GO
/****** Object:  Table [dbo].[SubmissionSet]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SubmissionSet]') AND type in (N'U'))
DROP TABLE [SubmissionSet]
GO
/****** Object:  Table [dbo].[DocumentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntry]') AND type in (N'U'))
DROP TABLE [DocumentEntry]
GO
/****** Object:  Table [dbo].[ConfigurationEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ConfigurationEntry]') AND type in (N'U'))
DROP TABLE [ConfigurationEntry]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Author]') AND type in (N'U'))
DROP TABLE [Author]
GO
/****** Object:  Table [dbo].[PatientMessageConfiguration]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PatientMessageConfiguration]') AND type in (N'U'))
DROP TABLE [PatientMessageConfiguration]
GO
/****** Object:  StoredProcedure [dbo].[usp_folderCodeList_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_folderCodeList_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_folderCodeList_insert]
GO
/****** Object:  Table [dbo].[AuditMessageConfiguration]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AuditMessageConfiguration]') AND type in (N'U'))
DROP TABLE [AuditMessageConfiguration]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_auditMessageConfigurationDetails]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_auditMessageConfigurationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_auditMessageConfigurationDetails]
GO
/****** Object:  Table [dbo].[RegistryLog]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RegistryLog]') AND type in (N'U'))
DROP TABLE [RegistryLog]
GO
/****** Object:  Table [dbo].[StoredQuery]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[StoredQuery]') AND type in (N'U'))
DROP TABLE [StoredQuery]
GO
/****** Object:  StoredProcedure [dbo].[usp_documentEntryEventCodeList_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_documentEntryEventCodeList_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_documentEntryEventCodeList_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_codedValue_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_codedValue_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_codedValue_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_IdFromDocumentEntryUUID]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_IdFromDocumentEntryUUID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_get_IdFromDocumentEntryUUID]
GO
/****** Object:  UserDefinedFunction [dbo].[USP_SplitString]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[USP_SplitString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [USP_SplitString]
GO
/****** Object:  Table [dbo].[CodeType]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CodeType]') AND type in (N'U'))
DROP TABLE [CodeType]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Patient]') AND type in (N'U'))
DROP TABLE [Patient]
GO
/****** Object:  Table [dbo].[StoredQueryParameter]    Script Date: 06/13/2008 14:14:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[StoredQueryParameter]') AND type in (N'U'))
DROP TABLE [StoredQueryParameter]
GO
/****** Object:  StoredProcedure [dbo].[usp_codeSet_insert]    Script Date: 06/13/2008 14:14:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_codeSet_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_codeSet_insert]
GO
/****** Object:  StoredProcedure [dbo].[usp_codeSet_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_codeSet_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_codeSet_insert]
      (@codeSetID varchar(128)
           ,@displayName varchar(128))
AS

BEGIN

INSERT INTO [CodeSet]
           ([codeSetID]
           ,[displayName])
     VALUES
           (@codeSetID,@displayName)

END' 
END
GO
/****** Object:  Table [dbo].[StoredQueryParameter]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[StoredQueryParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [StoredQueryParameter](
	[storedQueryParameterID] [int] IDENTITY(1,1) NOT NULL,
	[storedQueryID] [int] NULL,
	[storedQueryUniqueID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[storedQueryParameterName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[attribute] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[isMandatory] [bit] NULL,
	[isMultiple] [bit] NULL,
	[dependentParameterName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[tableName] [varchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[joinConditionSQLCode] [varchar](8000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[whereConditionSQLCode] [varchar](8000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[storedQueryParameterSequence] [int] NULL,
 CONSTRAINT [PK_StoredQueryParameter] PRIMARY KEY CLUSTERED 
(
	[storedQueryParameterID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [StoredQueryParameter] ON
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (1, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryPatientId', N'XDSDocumentEntry.patientId', 1, 0, NULL, N'PATIENT PAT', N'PAT.patientID = DE.patientID', N'PAT.patientUID = ''$XDSDocumentEntryPatientId''', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (2, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryClassCode', N'XDSDocumentEntry.classCode', 0, 1, NULL, NULL, NULL, N'DE.classCodeValue IN $XDSDocumentEntryClassCode', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (3, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryClassCodeScheme', N'XDSDocumentEntry.classCode', 0, 1, N'$XDSDocumentEntryClassCode', NULL, NULL, N'DE.classCodeDisplayName IN $XDSDocumentEntryClassCodeScheme', 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (4, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryPracticeSettingCode', N'XDSDocumentEntry.practiceSettingCode', 0, 1, NULL, NULL, NULL, N'DE.practiceSettingCodeValue IN $XDSDocumentEntryPracticeSettingCode', 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (5, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryPracticeSettingCodeScheme', N'XDSDocumentEntry.practiceSettingCode', 0, 1, N'$XDSDocumentEntryPracticeSettingCode', NULL, NULL, N'DE.practiceSettingCodeDisplayName IN $XDSDocumentEntryPracticeSettingCodeScheme', 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (6, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryCreationTimeFrom', N'XDSDocumentEntry.creationTime', 0, 0, NULL, NULL, NULL, N'DE.creationTime >= CONVERT(DATETIME, ''$XDSDocumentEntryCreationTimeFrom'')', 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (7, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryCreationTimeTo', N'XDSDocumentEntry.creationTime', 0, 0, NULL, NULL, NULL, N'DE.creationTime < CONVERT(DATETIME, ''$XDSDocumentEntryCreationTimeTo'')', 7)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (8, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStartTimeFrom', N'XDSDocumentEntry.serviceStartTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStartTime >= CONVERT(DATETIME, ''$XDSDocumentEntryServiceStartTimeFrom'')', 8)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (9, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStartTimeTo', N'XDSDocumentEntry.serviceStartTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStartTime < CONVERT(DATETIME, ''$XDSDocumentEntryServiceStartTimeTo'')', 9)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (10, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStopTimeFrom', N'XDSDocumentEntry.serviceStopTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStopTime >= CONVERT(DATETIME, ''$XDSDocumentEntryServiceStopTimeFrom'')', 10)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (11, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStopTimeTo', N'XDSDocumentEntry.serviceStopTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStopTime < CONVERT(DATETIME, ''$XDSDocumentEntryServiceStopTimeTo'')', 11)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (12, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryHealthcareFacilityTypeCode', N'XDSDocumentEntry.healthcareFacilityTypeCode', 0, 1, NULL, NULL, NULL, N'DE.healthcareFacilityTypeCodeValue IN $XDSDocumentEntryHealthcareFacilityTypeCode', 12)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (13, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryHealthcareFacilityTypeCodeScheme', N'XDSDocumentEntry.healthcareFacilityTypeCode', 0, 1, N'$XDSDocumentEntryHealthcareFacilityTypeCode', NULL, NULL, N'DE.healthcareFacilityTypeCodeDisplayName IN $XDSDocumentEntryHealthcareFacilityTypeCodeScheme', 13)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (14, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryEventCodeList', N'XDSDocumentEntry.eventCodeList', 0, 1, NULL, N'DocumentEntryEventCodeList DEECL', N'DEECL.documentEntryId = DE.documentEntryId', N'DEECL.eventCodeValue IN $XDSDocumentEntryEventCodeList', 14)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (15, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryEventCodeListScheme', N'XDSDocumentEntry.eventCodeList', 0, 1, N'$XDSDocumentEntryEventCodeList', NULL, NULL, N'DEECL.eventCodeDisplayName IN $XDSDocumentEntryEventCodeListScheme', 15)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (16, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, N'DE.confidentialityCodeValue IN $XDSDocumentEntryConfidentialityCode', 16)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (17, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, N'DE.formatCodeValue IN $XDSDocumentEntryFormatCode', 18)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (18, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryStatus', N'XDSDocumentEntry.status', 1, 1, NULL, NULL, NULL, N'DE.availabilityStatus IN $XDSDocumentEntryStatus', 19)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (20, 1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryConfidentialityCodeScheme', N'XDSDocumentEntry.confidentialityCode', 0, 1, N'$XDSDocumentEntryConfidentialityCode', NULL, NULL, N'DE.confidentialityCodeDisplayName IN $XDSDocumentEntryConfidentialityCodeScheme', 17)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (21, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryPatientId', N'XDSDocumentEntry.patientId', 1, 0, NULL, N'PATIENT PAT', N'PAT.patientID = DE.patientID', N'PAT.patientUID = ''$XDSDocumentEntryPatientId''', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (22, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryClassCode', N'XDSDocumentEntry.classCode', 0, 1, NULL, NULL, NULL, N'DE.classCodeValue IN $XDSDocumentEntryClassCode', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (23, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryClassCodeScheme', N'XDSDocumentEntry.classCode', 0, 1, N'$XDSDocumentEntryClassCode', NULL, NULL, N'DE.classCodeDisplayName IN $XDSDocumentEntryClassCodeScheme', 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (24, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryPracticeSettingCode', N'XDSDocumentEntry.practiceSettingCode', 0, 1, NULL, NULL, NULL, N'DE.practiceSettingCodeValue IN $XDSDocumentEntryPracticeSettingCode', 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (25, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryPracticeSettingCodeScheme', N'XDSDocumentEntry.practiceSettingCode', 0, 1, N'$XDSDocumentEntryPracticeSettingCode', NULL, NULL, N'DE.practiceSettingCodeDisplayName IN $XDSDocumentEntryPracticeSettingCodeScheme', 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (26, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryCreationTimeFrom', N'XDSDocumentEntry.creationTime', 0, 0, NULL, NULL, NULL, N'DE.creationTime >= CONVERT(DATETIME, ''$XDSDocumentEntryCreationTimeFrom'')', 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (27, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryCreationTimeTo', N'XDSDocumentEntry.creationTime', 0, 0, NULL, NULL, NULL, N'DE.creationTime < CONVERT(DATETIME, ''$XDSDocumentEntryCreationTimeTo'')', 7)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (28, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStartTimeFrom', N'XDSDocumentEntry.serviceStartTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStartTime >= CONVERT(DATETIME, ''$XDSDocumentEntryServiceStartTimeFrom'')', 8)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (29, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStartTimeTo', N'XDSDocumentEntry.serviceStartTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStartTime < CONVERT(DATETIME, ''$XDSDocumentEntryServiceStartTimeTo'')', 9)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (30, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStopTimeFrom', N'XDSDocumentEntry.serviceStopTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStopTime >= CONVERT(DATETIME, ''$XDSDocumentEntryServiceStopTimeFrom'')', 10)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (31, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryServiceStopTimeTo', N'XDSDocumentEntry.serviceStopTime', 0, 0, NULL, NULL, NULL, N'DE.serviceStopTime < CONVERT(DATETIME, ''$XDSDocumentEntryServiceStopTimeTo'')', 11)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (32, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryHealthcareFacilityTypeCode', N'XDSDocumentEntry.healthcareFacilityTypeCode', 0, 1, NULL, NULL, NULL, N'DE.healthcareFacilityTypeCodeValue IN $XDSDocumentEntryHealthcareFacilityTypeCode', 12)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (33, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryHealthcareFacilityTypeCodeScheme', N'XDSDocumentEntry.healthcareFacilityTypeCode', 0, 1, N'$XDSDocumentEntryHealthcareFacilityTypeCode', NULL, NULL, N'DE.healthcareFacilityTypeCodeDisplayName IN $XDSDocumentEntryHealthcareFacilityTypeCodeScheme', 13)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (34, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryEventCodeList', N'XDSDocumentEntry.eventCodeList', 0, 1, NULL, N'DocumentEntryEventCodeList DEECL', N'DEECL.documentEntryId = DE.documentEntryId', N'DEECL.eventCodeValue IN $XDSDocumentEntryEventCodeList', 14)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (35, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryEventCodeListScheme', N'XDSDocumentEntry.eventCodeList', 0, 1, N'$XDSDocumentEntryEventCodeList', NULL, NULL, N'DEECL.eventCodeDisplayName IN $XDSDocumentEntryEventCodeListScheme', 15)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (36, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, N'DE.confidentialityCodeValue IN $XDSDocumentEntryConfidentialityCode', 16)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (37, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, N'DE.formatCodeValue IN $XDSDocumentEntryFormatCode', 18)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (38, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryStatus', N'XDSDocumentEntry.status', 1, 1, NULL, NULL, NULL, N'DE.availabilityStatus IN $XDSDocumentEntryStatus', 19)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (39, 16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'$XDSDocumentEntryConfidentialityCodeScheme', N'XDSDocumentEntry.confidentialityCode', 0, 1, N'$XDSDocumentEntryConfidentialityCode', NULL, NULL, N'DE.confidentialityCodeDisplayName IN $XDSDocumentEntryConfidentialityCodeScheme', 17)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (40, 17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetPatientId', N'XDSSubmissionSet.patientId', 1, 0, NULL, N'PATIENT PAT', N'PAT.patientID = SST.patientID', N'PAT.patientUID = ''$XDSSubmissionSetPatientId''', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (41, 17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetSourceId', N'XDSSubmissionSet.sourceId', 0, 1, NULL, NULL, NULL, N'SST.sourceID IN $XDSSubmissionSetSourceId', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (42, 17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetSubmissionTimeFrom', N'XDSSubmissionSet.submissionTime - Lowervalue', 0, 0, NULL, NULL, NULL, N'SST.submissionTime >= CONVERT(DATETIME, ''$XDSSubmissionSetSubmissionTimeFrom'')', 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (43, 17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetSubmissionTimeTo', N'XDSSubmissionSet.submissionTime - Uppervalue', 0, 0, NULL, NULL, NULL, N'SST.submissionTime < CONVERT(DATETIME, ''$XDSSubmissionSetSubmissionTimeTo'')', 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (44, 17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetAuthorPerson', N'XDSSubmissionSet.authorPerson', 0, 0, NULL, N'SubmissionSetAuthor SSA, Author ATHR', N'SSA.submissionSetID = SST.submissionSetID AND SSA.authorID = ATHR.authorID', N'ATHR.authorPerson LIKE $XDSSubmissionSetAuthorPerson', 7)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (45, 17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetContentType', N'XDSSubmissionSet.contentTypeCode', 0, 1, NULL, NULL, NULL, N'SST.contentTypeCodeValue IN $XDSSubmissionSetContentType', 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (46, 17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, N'SST.availabilityStatus IN $XDSSubmissionSetStatus', 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (48, 3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetPatientId', N'XDSSubmissionSet.patientId', 1, 0, NULL, N'PATIENT PAT', N'PAT.patientID = SST.patientID', N'PAT.patientUID = ''$XDSSubmissionSetPatientId''', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (49, 3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetSourceId', N'XDSSubmissionSet.sourceId', 0, 1, NULL, NULL, NULL, N'SST.sourceID IN $XDSSubmissionSetSourceId', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (50, 3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetSubmissionTimeFrom', N'XDSSubmissionSet.submissionTime - Lowervalue', 0, 0, NULL, NULL, NULL, N'SST.submissionTime >= CONVERT(DATETIME, ''$XDSSubmissionSetSubmissionTimeFrom'')', 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (52, 3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetSubmissionTimeTo', N'XDSSubmissionSet.submissionTime - Uppervalue', 0, 0, NULL, NULL, NULL, N'SST.submissionTime < CONVERT(DATETIME, ''$XDSSubmissionSetSubmissionTimeTo'')', 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (53, 3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetAuthorPerson', N'XDSSubmissionSet.authorPerson', 0, 0, NULL, N'SubmissionSetAuthor SSA, Author ATHR', N'SSA.submissionSetID = SST.submissionSetID AND SSA.authorID = ATHR.authorID', N'ATHR.authorPerson LIKE $XDSSubmissionSetAuthorPerson', 7)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (54, 3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetContentType', N'XDSSubmissionSet.contentTypeCode', 0, 1, NULL, NULL, NULL, N'SST.contentTypeCodeValue IN $XDSSubmissionSetContentType', 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (55, 3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, N'SST.availabilityStatus IN $XDSSubmissionSetStatus', 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (56, 4, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderPatientId', N'XDSFolder.patientId', 1, 0, NULL, N'PATIENT PAT', N'PAT.patientID = FLD.patientID', N'PAT.patientUID = ''$XDSFolderPatientId''', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (57, 4, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderLastUpdateTimeFrom', N'XDSFolder.lastUpdateTime lower value', 0, 0, NULL, NULL, NULL, N'FLD.lastUpdateTime >= CONVERT(DATETIME, ''$XDSFolderLastUpdateTimeFrom'')', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (58, 4, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderLastUpdateTimeTo', N'XDSFolder.lastUpdateTime upper bound', 0, 0, NULL, NULL, NULL, N'FLD.lastUpdateTime < CONVERT(DATETIME, ''$XDSFolderLastUpdateTimeTo'')', 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (59, 4, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderCodeList', N'XDSFolder.codeList', 0, 1, NULL, N'FOLDERCODELIST FCL', N'FCL.folderID = FLD.folderID', N'FCL.eventCodeValue IN $XDSFolderCodeList', 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (60, 4, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderCodeListScheme', N'XDSFolder.codeList', 0, 1, N'$XDSFolderCodeList', NULL, NULL, N'FCL.eventCodeDisplayName IN $XDSFolderCodeListScheme', 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (61, 4, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderStatus', N'XDSFolder.status', 1, 1, NULL, NULL, NULL, N'FLD.availabilityStatus IN $XDSFolderStatus', 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (62, 18, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderPatientId', N'XDSFolder.patientId', 1, 0, NULL, N'PATIENT PAT', N'PAT.patientID = FLD.patientID', N'PAT.patientUID = ''$XDSFolderPatientId''', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (63, 18, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderLastUpdateTimeFrom', N'XDSFolder.lastUpdateTime lower value', 0, 0, NULL, NULL, NULL, N'FLD.lastUpdateTime >= CONVERT(DATETIME, ''$XDSFolderLastUpdateTimeFrom'')', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (64, 18, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderLastUpdateTimeTo', N'XDSFolder.lastUpdateTime upper bound', 0, 0, NULL, NULL, NULL, N'FLD.lastUpdateTime < CONVERT(DATETIME, ''$XDSFolderLastUpdateTimeTo'')', 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (65, 18, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderCodeList', N'XDSFolder.codeList', 0, 1, NULL, N'FOLDERCODELIST FCL', N'FCL.folderID = FLD.folderID', N'FCL.eventCodeValue IN $XDSFolderCodeList', 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (66, 18, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderCodeListScheme', N'XDSFolder.codeList', 0, 1, N'$XDSFolderCodeList', NULL, NULL, N'FCL.eventCodeDisplayName IN $XDSFolderCodeListScheme', 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (67, 18, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'$XDSFolderStatus', N'XDSFolder.status', 1, 1, NULL, NULL, NULL, N'FLD.availabilityStatus IN $XDSFolderStatus', 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (68, 5, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$patientId', N'XDSFolder.patientId, XDSSubmissionSet.patientId, XDSDocumentEntry.patientId', 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (69, 5, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryStatus', N'XDSDocumentEntry.status', 1, 1, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (70, 5, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (71, 5, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSFolderStatus', N'XDSFolder.status', 1, 1, NULL, NULL, NULL, NULL, 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (72, 5, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (73, 5, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (74, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$patientId', N'XDSFolder.patientId, XDSSubmissionSet.patientId, XDSDocumentEntry.patientId', 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (75, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryStatus', N'XDSDocumentEntry.status', 1, 1, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (76, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (77, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSFolderStatus', N'XDSFolder.status', 1, 1, NULL, NULL, NULL, NULL, 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (78, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (79, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (80, 6, N'urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 1, NULL, NULL, NULL, N'DE.entryUUID IN $XDSDocumentEntryEntryUUID', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (81, 20, N'urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 1, NULL, NULL, NULL, N'DE.entryUUID IN $XDSDocumentEntryEntryUUID', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (82, 6, N'urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId', 0, 1, NULL, NULL, NULL, N'DE.uniqueID IN $XDSDocumentEntryUniqueId', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (83, 20, N'urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId', 0, 1, NULL, NULL, NULL, N'DE.uniqueID IN $XDSDocumentEntryUniqueId', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (84, 7, N'urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4', N'$XDSFolderEntryUUID', N'XDSFolder.entryUUID', 0, 1, NULL, NULL, NULL, N'FLD.entryUUID IN $XDSFolderEntryUUID', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (85, 21, N'urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4', N'$XDSFolderEntryUUID', N'XDSFolder.entryUUID', 0, 1, NULL, NULL, NULL, N'FLD.entryUUID IN $XDSFolderEntryUUID', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (86, 7, N'urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4', N'$XDSFolderUniqueId', N'XDSFolder.uniqueId', 0, 1, NULL, NULL, NULL, N'FLD.uniqueID IN $XDSFolderUniqueId', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (87, 21, N'urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4', N'$XDSFolderUniqueId', N'XDSFolder.uniqueId', 0, 1, NULL, NULL, NULL, N'FLD.uniqueID IN $XDSFolderUniqueId', 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (88, 8, N'urn:uuid:a7ae438b-4bc2-4642-93e9-be891f7bb155', N'$uuid', N' ', 1, 1, NULL, NULL, NULL, N' (sourceObject IN $uuid OR targetObject IN $uuid )', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (89, 22, N'urn:uuid:a7ae438b-4bc2-4642-93e9-be891f7bb155', N'$uuid', N' ', 1, 1, NULL, NULL, NULL, N' (sourceObject IN $uuid OR targetObject IN $uuid )', 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (90, 9, N'urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 1, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (91, 9, N'urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId', 0, 1, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (92, 23, N'urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 1, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (93, 23, N'urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId', 0, 1, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (94, 10, N'urn:uuid:51224314-5390-4169-9b91-b1980040715a', N'$uuid', N'XDSDocumentEntry. entryUUID and XDSFolder. entryUUID', 1, 1, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (95, 24, N'urn:uuid:51224314-5390-4169-9b91-b1980040715a', N'$uuid', N'XDSDocumentEntry. entryUUID and XDSFolder.entryUUID', 1, 1, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (96, 11, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSSubmissionSetEntryUUID', N'XDSSubmissionSet.entryUUID', 0, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (97, 11, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSSubmissionSetUniqueId', N'XDSSubmissionSet.uniqueId', 0, 0, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (98, 11, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (99, 11, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (100, 25, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSSubmissionSetEntryUUID', N'XDSSubmissionSet.entryUUID', 0, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (101, 25, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSSubmissionSetUniqueId', N'XDSSubmissionSet.uniqueId', 0, 0, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (102, 25, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (103, 25, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 4)
GO
print 'Processed 100 total records'
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (104, 12, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSFolderEntryUUID', N'XDSFolder.entryUUID', 0, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (105, 12, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSFolderUniqueId', N'XDSFolder.uniqueId', 0, 0, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (106, 12, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (107, 12, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (108, 26, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSFolderEntryUUID', N'XDSFolder.entryUUID', 0, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (109, 26, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSFolderUniqueId', N'XDSFolder.uniqueId', 0, 0, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (110, 26, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (111, 26, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (112, 13, N'urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 0, NULL, NULL, NULL, NULL, NULL)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (113, 13, N'urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId ', 0, 0, NULL, NULL, NULL, NULL, NULL)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (114, 27, N'urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 0, NULL, NULL, NULL, NULL, NULL)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (115, 27, N'urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId ', 0, 0, NULL, NULL, NULL, NULL, NULL)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (116, 14, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (117, 14, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId', 0, 0, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (118, 14, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'$AssociationTypes', N' ', 0, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (119, 28, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'$XDSDocumentEntryEntryUUID', N'XDSDocumentEntry.entryUUID', 0, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (120, 28, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'$XDSDocumentEntryUniqueId', N'XDSDocumentEntry.uniqueId', 0, 0, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (121, 28, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'$AssociationTypes', N' ', 0, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (122, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$patientId', N'XDSFolder.patientId, XDSSubmissionSet.patientId, XDSDocumentEntry.patientId', 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (123, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryStatus', N'XDSDocumentEntry.status', 1, 1, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (124, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (125, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (126, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSFolderStatus', N'XDSFolder.status', 1, 1, NULL, NULL, NULL, NULL, 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (127, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (128, 19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 6)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (129, 29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$patientId', N'XDSFolder.patientId, XDSSubmissionSet.patientId, XDSDocumentEntry.patientId', 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (130, 29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryStatus', N'XDSDocumentEntry.status', 1, 1, NULL, NULL, NULL, NULL, 2)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (131, 29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (132, 29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSSubmissionSetStatus', N'XDSSubmissionSet.status', 1, 1, NULL, NULL, NULL, NULL, 3)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (133, 29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSFolderStatus', N'XDSFolder.status', 1, 1, NULL, NULL, NULL, NULL, 4)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (134, 29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryFormatCode', N'XDSDocumentEntry.formatCode', 0, 1, NULL, NULL, NULL, NULL, 5)
INSERT [StoredQueryParameter] ([storedQueryParameterID], [storedQueryID], [storedQueryUniqueID], [storedQueryParameterName], [attribute], [isMandatory], [isMultiple], [dependentParameterName], [tableName], [joinConditionSQLCode], [whereConditionSQLCode], [storedQueryParameterSequence]) VALUES (135, 29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'$XDSDocumentEntryConfidentialityCode', N'XDSDocumentEntry.confidentialityCode', 0, 1, NULL, NULL, NULL, NULL, 6)
SET IDENTITY_INSERT [StoredQueryParameter] OFF
/****** Object:  Table [dbo].[Patient]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Patient]') AND type in (N'U'))
BEGIN
CREATE TABLE [Patient](
	[patientID] [int] IDENTITY(1,1) NOT NULL,
	[patientUID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[patientID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [Patient] ON
INSERT [Patient] ([patientID], [patientUID]) VALUES (4, N'498ef443e7ac4a6^^^&1.3.6.1.4.1.21367.2005.3.7&ISO')
INSERT [Patient] ([patientID], [patientUID]) VALUES (5, N'e21007e556864ca^^^&1.3.6.1.4.1.21367.2005.3.7&ISO')
INSERT [Patient] ([patientID], [patientUID]) VALUES (6, N'284c2500d0e949c^^^&1.3.6.1.4.1.21367.2005.3.7&ISO')
INSERT [Patient] ([patientID], [patientUID]) VALUES (7, N'abfb47b943d9426^^^&1.3.6.1.4.1.21367.2005.3.7&ISO')
INSERT [Patient] ([patientID], [patientUID]) VALUES (8, N'1.2.840.114350.1.13.99998.8734^^^34827G409')
INSERT [Patient] ([patientID], [patientUID]) VALUES (9, N'61d26732db0f474^^^&1.3.6.1.4.1.21367.2005.3.7&ISO')
INSERT [Patient] ([patientID], [patientUID]) VALUES (10, N'8fe78ac84449423^^^&1.3.6.1.4.1.21367.2005.3.7&ISO')
SET IDENTITY_INSERT [Patient] OFF
/****** Object:  Table [dbo].[CodeType]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CodeType]') AND type in (N'U'))
BEGIN
CREATE TABLE [CodeType](
	[codeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[displayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[classSchemeUUID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[codingScheme] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_CodeType] PRIMARY KEY CLUSTERED 
(
	[classSchemeUUID] ASC,
	[codingScheme] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_CodeType] UNIQUE NONCLUSTERED 
(
	[codeTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [CodeType] ON
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (1, N'XDSDocumentEntryStub', N'urn:uuid:10aa1a4b-715a-4120-bfd0-9760414112c8', N'ClassificationNode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (2, N'XDSFolder.codeList', N'urn:uuid:1ba97051-7806-41a8-a48b-8fce7af683c5', N'Connect-a-thon codeList')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (3, N'XDSDocumentEntry.eventCodeList', N'urn:uuid:2c6b8cb7-8b2a-4051-b291-b1ae6a575ef4', N'External Classification Scheme ')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (4, N'XDSDocumentEntry.uniqueId', N'urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab', N'ExternalIdentifier')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (5, N'XDSDocumentEntry.mimeType', N'urn:uuid:3dc97051-7806-41a8-a48b-8fce7af682d0', N'mimeType')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (6, N'XDSDocumentEntry.classCode', N'urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a', N'Connect-a-thon classCodes')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (7, N'XDSSubmissionSet.sourceId', N'urn:uuid:554ac39e-e3fe-47fe-b233-965d2a147832', N'External Identifer')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (8, N'XDSDocumentEntry.patientId', N'urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427', N'ExternalIdentifier')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (9, N'RPLC', N'urn:uuid:60fd13eb-b8f6-4f11-8f28-9ee000184339', N'ClassificationNode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (10, N'XDSSubmissionSet.patientId', N'urn:uuid:6b5aea1a-874d-4603-a4bc-96a0a7b38446', N'External Identifer')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (11, N'XDSFolder.uniqueId', N'urn:uuid:75df8f67-9973-4fbe-a900-df66cefecc5a', N'External Identifier')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (12, N'XDSDocumentEntry', N'urn:uuid:7edca82f-054d-47f2-a032-9b2a5b5186c1', N'ClassificationNode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (13, N'signs', N'urn:uuid:8ea93462-ad05-4cdc-8e54-a8084f6aff94', N'ClassificationNode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (14, N'APND', N'urn:uuid:917dc511-f7da-4417-8664-de25b34d3def', N'ClassificationNode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (15, N'XDSDocumentEntry.authorDescription', N'urn:uuid:93606bcf-9494-43ec-9b4e-a7748d1a838d', N'External Classification Scheme')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (16, N'XDSSubmissionSet.uniqueId', N'urn:uuid:96fdda7c-d067-4183-912e-bf5ee74998a8', N'External Identifer')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (17, N'XDSDocumentEntry.formatCode', N'urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d', N'1.2.840.10008.2.6.1')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (18, N'XDSDocumentEntry.formatCode', N'urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d', N'Connect-a-thon formatCodes')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (19, N'XDSDocumentEntry.formatCode', N'urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d', N'IHE BPPC')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (20, N'XDSSubmissionSet', N'urn:uuid:a54d6aa5-d40d-43f9-88c5-b4633d873bdd', N'ClassificationNode ')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (21, N'XDSSubmissionSet.authorDescription', N'urn:uuid:a7058bb9-b4e4-4307-ba5b-e3f0ab85e12d', N'External Classification Scheme ')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (22, N'XDSSubmissionSet.contentTypeCode', N'urn:uuid:aa543740-bdda-424e-8c96-df4873be8500', N'Connect-a-thon contentTypeCodes')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (23, N'XFRM_RPLC', N'urn:uuid:b76a27c7-af3c-4319-ba4c-b90c1dc45408', N'ClassificationNode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (24, N'XDSDocumentEntry.practiceSettingCode', N'urn:uuid:cccf5598-8b07-4b77-a05e-ae952c785ead', N'Connect-a-thon practiceSettingCodes')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (25, N'XDSFolder', N'urn:uuid:d9d542f3-6cc4-48b6-8870-ea235fbc94c2', N'ClassificationNode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (26, N'XFRM', N'urn:uuid:ede379e6-1147-4374-a943-8fcdcf1cd620', N'ClassificationNode ')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (27, N'XDSDocumentEntry.typeCode', N'urn:uuid:f0306f51-975f-434e-a61c-c59651d33983', N'Connect-a-thon TypeCode')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (28, N'XDSDocumentEntry.typeCode', N'urn:uuid:f0306f51-975f-434e-a61c-c59651d33983', N'LOINC')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (29, N'XDSDocumentEntry.healthCareFacilityTypeCode', N'urn:uuid:f33fb8ac-18af-42cc-ae0e-ed0b0bdb91e1', N'Connect-a-thon healthcareFacilityTypeCodes')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (30, N'XDSDocumentEntry.confidentialityCode', N'urn:uuid:f4f85eac-e6cb-4883-b524-f2705394840f', N'Connect-a-thon confidentialityCodes')
INSERT [CodeType] ([codeTypeID], [displayName], [classSchemeUUID], [codingScheme]) VALUES (31, N'XDSFolder.patientId', N'urn:uuid:f64ffdf0-4b97-4e06-b79f-a52b38ec2f8a', N'External Identifier')
SET IDENTITY_INSERT [CodeType] OFF
/****** Object:  UserDefinedFunction [dbo].[USP_SplitString]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[USP_SplitString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'

CREATE FUNCTION [USP_SplitString](@String nvarchar(4000), @Delimiter char(1))
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

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_IdFromDocumentEntryUUID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_IdFromDocumentEntryUUID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [usp_get_IdFromDocumentEntryUUID]
  @documentEntryUUID varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT documentEntryID from DocumentEntry where EntryUUID= @documentEntryUUID
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_codedValue_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_codedValue_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_codedValue_insert]
 (          @codedValueID int
           ,@codedValue varchar(128)
           ,@displayName varchar(128)
           ,@codeSetID varchar(128))   
AS

BEGIN 

INSERT INTO [CodedValue]
           ([codedValueID]
           ,[codedValue]
           ,[displayName]
           ,[codeSetID])
     VALUES
		   (@codedValueID
           ,@codedValue
           ,@displayName
           ,@codeSetID)

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_documentEntryEventCodeList_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_documentEntryEventCodeList_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_documentEntryEventCodeList_insert]
(
		   @documentEntryID int,
           @eventCodeValue varchar(128),
           @eventCodeDisplayName varchar(128)
)
AS

INSERT INTO [DocumentEntryEventCodeList]
           (
            documentEntryID
           ,eventCodeValue
           ,eventCodeDisplayName)

Values
(

            @documentEntryID
           ,@eventCodeValue
           ,@eventCodeDisplayName
)
' 
END
GO
/****** Object:  Table [dbo].[StoredQuery]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[StoredQuery]') AND type in (N'U'))
BEGIN
CREATE TABLE [StoredQuery](
	[storedQueryID] [int] IDENTITY(1,1) NOT NULL,
	[storedQueryUniqueID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[storedQueryName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[returnType] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[storedQuerySQLCode] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[storedQuerySequence] [int] NULL,
 CONSTRAINT [PK_StoredQuery] PRIMARY KEY CLUSTERED 
(
	[storedQueryID] ASC,
	[storedQueryUniqueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [StoredQuery] ON
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (1, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'FindDocuments', N'objectref', N'SELECT DE.entryUUID FROM  DocumentEntry DE', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (3, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'FindSubmissionSets', N'objectref', N'SELECT SST.entryUUID FROM  SubmissionSet SST', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (4, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'FindFolders', N'objectref', N'SELECT FLD.entryUUID FROM  FOLDER FLD', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (5, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'GetAll', N'objectref', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (6, N'urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4', N'GetDocuments', N'objectref', N'SELECT DE.entryUUID, DE.uniqueID, DE.extrinsicObjectXML FROM  DocumentEntry DE', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (7, N'urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4', N'GetFolders', N'objectref', N'SELECT FLD.entryUUID, FLD.uniqueID, FLD.folderXml FROM  FOLDER FLD', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (8, N'urn:uuid:a7ae438b-4bc2-4642-93e9-be891f7bb155', N'GetAssociations', N'objectref', N'SELECT SubmissionSetDocumentFolderID, submissionSetID, folderID, documentEntryID, sourceObject, targetObject, associationXml FROM SubmissionSetDocumentFolder SSDF', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (9, N'urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a', N'GetDocumentsAndAssociations', N'objectref', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (10, N'urn:uuid:51224314-5390-4169-9b91-b1980040715a', N'GetSubmissionSets', N'objectref', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (11, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'GetSubmissionSetAndContents', N'objectref', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (12, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'GetFolderAndContents', N'objectref', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (13, N'urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578', N'GetFoldersForDocument', N'objectref', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (14, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'GetRelatedDocuments', N'objectref', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (16, N'urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d', N'FindDocuments', N'leafclass', N'SELECT DE.entryUUID, DE.extrinsicObjectXML FROM  DocumentEntry DE', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (17, N'urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9', N'FindSubmissionSets', N'leafclass', N'SELECT SST.entryUUID, SST.submissionSetXml FROM  SubmissionSet SST', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (18, N'urn:uuid:958f3006-baad-4929-a4de-ff1114824431', N'FindFolders', N'leafclass', N'SELECT FLD.entryUUID, FLD.folderXml FROM  FOLDER FLD', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (19, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'GetAll', N'leafclass', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (20, N'urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4', N'GetDocuments', N'leafclass', N'SELECT DE.entryUUID, DE.uniqueID, DE.extrinsicObjectXML FROM  DocumentEntry DE', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (21, N'urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4', N'GetFolders', N'leafclass', N'SELECT FLD.entryUUID, FLD.uniqueID, FLD.folderXml FROM  FOLDER FLD', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (22, N'urn:uuid:a7ae438b-4bc2-4642-93e9-be891f7bb155', N'GetAssociations', N'leafclass', N'SELECT SubmissionSetDocumentFolderID, submissionSetID, folderID, documentEntryID, sourceObject, targetObject, associationXml FROM SubmissionSetDocumentFolder SSDF', 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (23, N'urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a', N'GetDocumentsAndAssociations', N'leafclass', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (24, N'urn:uuid:51224314-5390-4169-9b91-b1980040715a', N'GetSubmissionSets', N'leafclass', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (25, N'urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83', N'GetSubmissionSetAndContents', N'leafclass', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (26, N'urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7', N'GetFolderAndContents', N'leafclass', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (27, N'urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578', N'GetFoldersForDocument', N'leafclass', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (28, N'urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6', N'GetRelatedDocuments', N'leafclass', NULL, 1)
INSERT [StoredQuery] ([storedQueryID], [storedQueryUniqueID], [storedQueryName], [returnType], [storedQuerySQLCode], [storedQuerySequence]) VALUES (29, N'urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3', N'GetAll', N'leafclass', NULL, 1)
SET IDENTITY_INSERT [StoredQuery] OFF
/****** Object:  Table [dbo].[RegistryLog]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RegistryLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [RegistryLog](
	[logID] [int] IDENTITY(1,1) NOT NULL,
	[requesterIdentity] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[requestMetadata] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[transactionName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[startTime] [datetime] NULL,
	[finishTime] [datetime] NULL,
	[result] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[submissionSetID] [int] NULL,
 CONSTRAINT [PK_RegistryLog] PRIMARY KEY CLUSTERED 
(
	[logID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_auditMessageConfigurationDetails]    Script Date: 06/13/2008 14:14:12 ******/
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
/****** Object:  Table [dbo].[AuditMessageConfiguration]    Script Date: 06/13/2008 14:14:12 ******/
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
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (8, N'REGISTRY-RDS-IMPORT-ITI-42', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="C" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110107" displayName="Import" codeSystemName="DCM"/><EventTypeCode code="ITI-42" codeSystemName="IHE Transactions" displayName="Register Document Set - b"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source" /></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$SubmissionSet.UniqueID$" ParticipantObjectTypeCode="2" ParticipantObjectTypeCodeRole="20"><ParticipantObjectIDTypeCode code="$SubmissionSet.ClassificationNode.UUID$" /></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (9, N'REGISTRY-QUERY-ITI-18', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="E" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110112" displayName="Query" codeSystemName="DCM"/><EventTypeCode code="IHE-18" displayName="Registry Stored Query" codeSystemName="IHE Transactions" /></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source" /></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination" /></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$XDSPatient$" ParticipantObjectTypeCode="1" ParticipantObjectTypeCodeRole="24"><ParticipantObjectIDTypeCode code="2" /></ParticipantObjectIdentification><ParticipantObjectIdentification ParticipantObjectID="$AdhocQuery$" ParticipantObjectTypeCode="2" ParticipantObjectTypeCodeRole="24"><ParticipantObjectIDTypeCode code="ITI-18" codeSystemName="IHE Transactions" displayName="Reqistry Stored Query"/></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (10, N'REGISTRY-PATIENT-RECORD-ADD-ITI-44', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="C" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110110" displayName="Patient Record" codeSystemName="DCM"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source"/></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination"/></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$XDSPatientID$" ParticipantObjectTypeCode="1" ParticipantObjectTypeCodeRole="1"><ParticipantObjectIDTypeCode code="2"/></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (12, N'REGISTRY-PATIENT-RECORD-REVISED-ITI-44', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="U" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110110" displayName="Patient Record" codeSystemName="DCM"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source"/></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination"/></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$XDSPatientID$" ParticipantObjectTypeCode="1" ParticipantObjectTypeCodeRole="1"><ParticipantObjectIDTypeCode code="2"/></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (13, N'REGISTRY-PATIENT-RECORD-DUPLICATES-RESOLVED-UPDATE-ITI-44', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="U" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110110" displayName="Patient Record" codeSystemName="DCM"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source"/></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination"/></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$XDSPatientID$" ParticipantObjectTypeCode="1" ParticipantObjectTypeCodeRole="1"><ParticipantObjectIDTypeCode code="2"/></ParticipantObjectIdentification></AuditMessage>')
INSERT [AuditMessageConfiguration] ([auditMessageID], [messageKey], [messageValue]) VALUES (15, N'REGISTRY-PATIENT-RECORD-DUPLICATES-RESOLVED-DELETE-ITI-44', N'<?xml version="1.0" encoding="UTF-8"?><AuditMessage><EventIdentification EventActionCode="D" EventDateTime="$EventIdentification.EventDateTime$" EventOutcomeIndicator="$EventIdentification.EventOutcomeIndicator$"><EventID code="110110" displayName="Patient Record" codeSystemName="DCM"/></EventIdentification><ActiveParticipant UserID="$ActiveParticipant.UserID.Source$" UserIsRequestor="true"><RoleIDCode code="110153" codeSystemName="DCM" displayName="Source"/></ActiveParticipant><ActiveParticipant UserID="$ActiveParticipant.UserID.Destination$" UserIsRequestor="false"><RoleIDCode code="110152" codeSystemName="DCM" displayName="Destination"/></ActiveParticipant><AuditSourceIdentification AuditSourceID="$AuditSourceIdentification.AuditSourceID$"/><ParticipantObjectIdentification ParticipantObjectID="$XDSPatientID$" ParticipantObjectTypeCode="1" ParticipantObjectTypeCodeRole="1"><ParticipantObjectIDTypeCode code="2"/></ParticipantObjectIdentification></AuditMessage>')
SET IDENTITY_INSERT [AuditMessageConfiguration] OFF
/****** Object:  StoredProcedure [dbo].[usp_folderCodeList_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_folderCodeList_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_folderCodeList_insert]
(		    
 
 @folderID int
,@eventCodeValue varchar(128)
,@eventCodeDisplayName char(10)
)
AS
BEGIN
	INSERT INTO [FolderCodeList]
           (
            [folderID]
           ,[eventCodeValue]
           ,[eventCodeDisplayName])
     VALUES
		(	   @folderID
			   ,@eventCodeValue
			   ,@eventCodeDisplayName
		)
     
END
' 
END
GO
/****** Object:  Table [dbo].[PatientMessageConfiguration]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PatientMessageConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [PatientMessageConfiguration](
	[patientMessageID] [int] IDENTITY(1,1) NOT NULL,
	[patientMessageKey] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[patientMessageValue] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_PatientMessageConfiguration] PRIMARY KEY CLUSTERED 
(
	[patientMessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [PatientMessageConfiguration] ON
INSERT [PatientMessageConfiguration] ([patientMessageID], [patientMessageKey], [patientMessageValue]) VALUES (1, N'PATIENT-ADD-ACK', N'<?xml version="1.0" encoding="UTF-8"?><MCCI_IN000002UV01 xmlns="urn:hl7-org:v3" ITSVersion="XML_1.0"><id root="$NEW.GUID$"/><creationTime value="$creationTime$"/><interactionId root="$INTERACTION.ID$" extension="MCCI_IN000002UV01"/><processingCode code="P"/><processingModeCode code="R"/><acceptAckCode code="NE"/><receiver typeCode="RCV"><device determinerCode="INSTANCE"><id root="$RECEIVER.ROOT$"/></device></receiver><sender typeCode="SND"><device determinerCode="INSTANCE"><id root="$SENDER.ROOT$"/></device></sender><acknowledgement><typeCode code="$RESULT.CODE$"/><targetMessage><id root="$ORIGINAL.MESSAGE.ID$"/></targetMessage></acknowledgement></MCCI_IN000002UV01>')
INSERT [PatientMessageConfiguration] ([patientMessageID], [patientMessageKey], [patientMessageValue]) VALUES (2, N'PATIENT-REVISED-ACK', N'<?xml version="1.0" encoding="UTF-8"?><MCCI_IN000002UV01 xmlns="urn:hl7-org:v3" ITSVersion="XML_1.0"><id root="$NEW.GUID$"/><creationTime value="$creationTime$"/><interactionId root="$INTERACTION.ID$" extension="MCCI_IN000002UV01"/><processingCode code="P"/><processingModeCode code="R"/><acceptAckCode code="NE"/><receiver typeCode="RCV"><device determinerCode="INSTANCE"><id root="$RECEIVER.ROOT$"/></device></receiver><sender typeCode="SND"><device determinerCode="INSTANCE"><id root="$SENDER.ROOT$"/></device></sender><acknowledgement><typeCode code="$RESULT.CODE$"/><targetMessage><id root="$ORIGINAL.MESSAGE.ID$"/></targetMessage></acknowledgement></MCCI_IN000002UV01>')
INSERT [PatientMessageConfiguration] ([patientMessageID], [patientMessageKey], [patientMessageValue]) VALUES (3, N'PATIENT-DUPLICATES-RESOLVED-ACK', N'<?xml version="1.0" encoding="UTF-8"?><MCCI_IN000002UV01 xmlns="urn:hl7-org:v3" ITSVersion="XML_1.0"><id root="$NEW.GUID$"/><creationTime value="$creationTime$"/><interactionId root="$INTERACTION.ID$" extension="MCCI_IN000002UV01"/><processingCode code="P"/><processingModeCode code="R"/><acceptAckCode code="NE"/><receiver typeCode="RCV"><device determinerCode="INSTANCE"><id root="$RECEIVER.ROOT$"/></device></receiver><sender typeCode="SND"><device determinerCode="INSTANCE"><id root="$SENDER.ROOT$"/></device></sender><acknowledgement><typeCode code="$RESULT.CODE$"/><targetMessage><id root="$ORIGINAL.MESSAGE.ID$"/></targetMessage></acknowledgement></MCCI_IN000002UV01>')
SET IDENTITY_INSERT [PatientMessageConfiguration] OFF
/****** Object:  Table [dbo].[Author]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Author]') AND type in (N'U'))
BEGIN
CREATE TABLE [Author](
	[authorID] [int] IDENTITY(1,1) NOT NULL,
	[authorInstitution] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[authorPerson] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[authorRole] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[authorSpeciality] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[authorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ConfigurationEntry]    Script Date: 06/13/2008 14:14:12 ******/
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
INSERT [ConfigurationEntry] ([ConfigurationEntryID], [ConfigurationKey], [ConfigurationValue]) VALUES (2, N'validateMimeType', N'true')
SET IDENTITY_INSERT [ConfigurationEntry] OFF
/****** Object:  Table [dbo].[DocumentEntry]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntry]') AND type in (N'U'))
BEGIN
CREATE TABLE [DocumentEntry](
	[documentEntryID] [int] IDENTITY(1,1) NOT NULL,
	[availabilityStatus] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[classCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[classCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[comments] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[confidentialityCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[confidentialityCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[creationTime] [datetime] NULL,
	[entryUUID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[formatCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[formatCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[hash] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[healthcareFacilityTypeCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[healthcareFacilityTypeCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[languageCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[languageCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[legalAuthenticator] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[mimeType] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[parentDocumentID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[parentDocumentRelationship] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[patientID] [int] NULL,
	[practiceSettingCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[practiceSettingCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[serviceStartTime] [datetime] NULL,
	[serviceStopTime] [datetime] NULL,
	[size] [int] NULL,
	[sourcePatientID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[sourcePatientInfo] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[title] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[typeCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[typeCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[uniqueID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[URI] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[repositoryUniqueID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[extrinsicObjectXML] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_DocumentEntry] PRIMARY KEY CLUSTERED 
(
	[documentEntryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SubmissionSet]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SubmissionSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [SubmissionSet](
	[submissionSetID] [int] IDENTITY(1,1) NOT NULL,
	[availabilityStatus] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[comments] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[contentTypeCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[contentTypeCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[entryUUID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[patientID] [int] NULL,
	[sourceID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[submissionTime] [datetime] NULL,
	[title] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[uniqueID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[submissionSetXml] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_SubmissionSet] PRIMARY KEY CLUSTERED 
(
	[submissionSetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Folder]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Folder]') AND type in (N'U'))
BEGIN
CREATE TABLE [Folder](
	[folderID] [int] IDENTITY(1,1) NOT NULL,
	[availabilityStatus] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[comments] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[entryUUID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lastUpdateTime] [datetime] NULL,
	[patientID] [int] NULL,
	[title] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[uniqueID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[folderXml] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Folder] PRIMARY KEY CLUSTERED 
(
	[folderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[CodeValue]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CodeValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [CodeValue](
	[codeValueID] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[displayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[codeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_CodeValue] PRIMARY KEY CLUSTERED 
(
	[codeValueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [CodeValue] ON
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (1, N'Communication', N'Communication', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (2, N'Conference', N'Conference', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (3, N'Case conference', N'Case conference', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (4, N'Consult', N'Consult', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (5, N'Confirmatory consultation', N'Confirmatory consultation', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (6, N'Counseling', N'Counseling', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (7, N'Group counseling', N'Group counseling', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (8, N'Education', N'Education', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (9, N'History and Physical', N'History and Physical', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (10, N'Admission history and physical', N'Admission history and physical', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (11, N'Comprehensive history and physical', N'Comprehensive history and physical', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (12, N'Targeted history and physical', N'Targeted history and physical', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (13, N'Initial evaluation', N'Initial evaluation', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (14, N'Admission evaluation', N'Admission evaluation', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (15, N'Pre-operative evaluation and management', N'Pre-operative evaluation and management', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (16, N'Subsequent evaluation', N'Subsequent evaluation', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (17, N'Summarization of episode', N'Summarization of episode', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (18, N'Transfer summarization', N'Transfer summarization', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (19, N'Discharge summarization', N'Discharge summarization', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (20, N'Summary of death', N'Summary of death', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (21, N'Transfer of care referral', N'Transfer of care referral', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (22, N'Supervisory direction', N'Supervisory direction', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (23, N'Telephone encounter', N'Telephone encounter', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (24, N'Interventional Procedure', N'Interventional Procedure', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (25, N'Operative', N'Operative', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (26, N'Pathology Procedure', N'Pathology Procedure', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (27, N'Autopsy', N'Autopsy', 23)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (28, N'Communication', N'Communication', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (29, N'Evaluation and management', N'Evaluation and management', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (30, N'Conference', N'Conference', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (31, N'Case conference', N'Case conference', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (32, N'Consult', N'Consult', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (33, N'Confirmatory consultation', N'Confirmatory consultation', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (34, N'Counseling', N'Counseling', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (35, N'Group counseling', N'Group counseling', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (36, N'Education', N'Education', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (37, N'History and Physical', N'History and Physical', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (38, N'Admission history and physical', N'Admission history and physical', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (39, N'Comprehensive history and physical', N'Comprehensive history and physical', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (40, N'Targeted history and physical', N'Targeted history and physical', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (41, N'Initial evaluation', N'Initial evaluation', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (42, N'Admission evaluation', N'Admission evaluation', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (43, N'Pre-operative evaluation and management', N'Pre-operative evaluation and management', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (44, N'Subsequent evaluation', N'Subsequent evaluation', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (45, N'Summarization of episode', N'Summarization of episode', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (46, N'Transfer summarization', N'Transfer summarization', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (47, N'Discharge summarization', N'Discharge summarization', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (48, N'Summary of death', N'Summary of death', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (49, N'Transfer of care referral', N'Transfer of care referral', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (50, N'Supervisory direction', N'Supervisory direction', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (51, N'Telephone encounter', N'Telephone encounter', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (52, N'Interventional Procedure', N'Interventional Procedure', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (53, N'Operative', N'Operative', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (54, N'Pathology Procedure', N'Pathology Procedure', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (55, N'Autopsy', N'Autopsy', 7)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (56, N'C', N'Celebrity', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (57, N'D', N'Clinician', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (58, N'I', N'Individual', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (59, N'N', N'Normal', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (60, N'R', N'Restricted', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (61, N'S', N'Sensitive', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (62, N'T', N'Taboo', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (63, N'1.3.6.1.4.1.21367.2006.7.101', N'Clinical-Staff', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (64, N'1.3.6.1.4.1.21367.2006.7.102', N'Clinical-Spouse', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (65, N'1.3.6.1.4.1.21367.2006.7.103', N'Clinical-Attorney', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (66, N'1.3.6.1.4.1.21367.2006.7.104', N'Solar Drug Trial', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (67, N'1.3.6.1.4.1.21367.2006.7.105', N'Sun Spot Drug Trial', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (68, N'1.3.6.1.4.1.21367.2006.7.106', N'No Sharing', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (69, N'1.3.6.1.4.1.21367.2006.7.107', N'Normal Sharing', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (70, N'1.3.6.1.4.1.21367.2006.7.108', N'Normal Sharing Plus Research Use', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (71, N'1.3.6.1.4.1.21367.2006.7.109', N'Restricted VIP Sharing', 31)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (72, N'ScanTEXT/IHE 1.x', N'ScanTEXT/IHE 1.x', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (73, N'ScanPDF/IHE 1.x', N'ScanPDF/IHE 1.x', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (74, N'PDF/IHE 1.x', N'PDF/IHE 1.x', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (75, N'CDA/IHE 1.0', N'CDA/IHE 1.0', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (76, N'CDAR2/IHE 1.0', N'CDAR2/IHE 1.0', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (77, N'CCR/IHE 0.9', N'CCR/IHE 0.9', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (78, N'CCR V1.0', N'CCR V1.0', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (79, N'HL7/Lab 2.5', N'HL7/Lab 2.5', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (80, N'1.3.6.1.4.1.19376.1.5.3.1.1.2', N'XDS-MS', 20)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (81, N'1.3.6.1.4.1.19376.1.5.3.1.1.9', N'PPHP', 18)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (82, N'1.3.6.1.4.1.19376.1.5.3.1.1.10', N'EDR', 19)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (83, N'1.3.6.1.4.1.19376.1.5.3.1.1.5', N'XPHR Extract', 20)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (84, N'1.3.6.1.4.1.19376.1.5.3.1.1.6', N'XPHR Update', 18)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (85, N'1.3.6.1.4.1.19376.1.5.3.1.1.7', N'Privacy Consent Policy', 19)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (86, N'IHE/multipart', N'multipart', 20)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (87, N'1.2.840.10008.5.1.4.1.1.88.59', N'Key Object Selection Document', 18)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (88, N'Home', N'Home', 19)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (89, N'Assisted Living', N'Assisted Living', 20)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (90, N'Home Health Care', N'Home Health Care', 18)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (91, N'Hospital Setting', N'Hospital Setting', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (92, N'Acute care hospital', N'Acute care hospital', 20)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (93, N'Hospital Unit', N'Hospital Unit', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (94, N'Critical Care Unit', N'Critical Care Unit', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (95, N'Emergency Department', N'Emergency Department', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (96, N'Observation Ward', N'Observation Ward', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (97, N'Rehabilitation hospital', N'Rehabilitation hospital', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (98, N'Nursing Home', N'Nursing Home', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (99, N'Skilled Nursing Facility', N'Skilled Nursing Facility', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (100, N'Outpatient', N'Outpatient', 2)
GO
print 'Processed 100 total records'
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (101, N'Anesthesia', N'Anesthesia', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (102, N'Cardiology', N'Cardiology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (103, N'Case Manager', N'Case Manager', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (104, N'Chaplain', N'Chaplain', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (105, N'Chemotherapy', N'Chemotherapy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (106, N'Chiropractic', N'Chiropractic', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (107, N'Critical Care', N'Critical Care', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (108, N'Dentistry', N'Dentistry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (109, N'Diabetology', N'Diabetology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (110, N'Dialysis', N'Dialysis', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (111, N'Emergency', N'Emergency', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (112, N'Endocrinology', N'Endocrinology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (113, N'Gastroenterology', N'Gastroenterology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (114, N'General Medicine', N'General Medicine', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (115, N'General Surgery', N'General Surgery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (116, N'Gynecology', N'Gynecology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (117, N'Labor and Delivery', N'Labor and Delivery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (118, N'Laboratory', N'Laboratory', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (119, N'Multidisciplinary', N'Multidisciplinary', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (120, N'Neonatal Intensive Care', N'Neonatal Intensive Care', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (121, N'Neurosurgery', N'Neurosurgery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (122, N'Nursery', N'Nursery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (123, N'Nursing', N'Nursing', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (124, N'Obstetrics', N'Obstetrics', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (125, N'Occupational Therapy', N'Occupational Therapy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (126, N'Ophthalmology', N'Ophthalmology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (127, N'Optometry', N'Optometry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (128, N'Orthopedics', N'Orthopedics', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (129, N'Otorhinolaryngology', N'Otorhinolaryngology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (130, N'Pathology', N'Pathology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (131, N'Perioperative', N'Perioperative', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (132, N'Pharmacacy', N'Pharmacacy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (133, N'Physical Medicine', N'Physical Medicine', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (134, N'Plastic Surgery', N'Plastic Surgery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (135, N'Podiatry', N'Podiatry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (136, N'Psychiatry', N'Psychiatry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (137, N'Pulmonary', N'Pulmonary', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (138, N'Radiology', N'Radiology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (139, N'Social Services', N'Social Services', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (140, N'Speech Therapy', N'Speech Therapy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (141, N'Thyroidology', N'Thyroidology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (142, N'Tumor Board', N'Tumor Board', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (143, N'Urology', N'Urology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (144, N'Home', N'Home', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (145, N'Veterinary Medicine', N'Veterinary Medicine', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (146, N'Anesthesia', N'Anesthesia', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (147, N'Cardiology', N'Cardiology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (148, N'Case Manager', N'Case Manager', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (149, N'Chaplain', N'Chaplain', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (150, N'Chemotherapy', N'Chemotherapy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (151, N'Chiropractic', N'Chiropractic', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (152, N'Critical Care', N'Critical Care', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (153, N'Dentistry', N'Dentistry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (154, N'Diabetology', N'Diabetology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (155, N'Dialysis', N'Dialysis', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (156, N'Emergency', N'Emergency', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (157, N'Endocrinology', N'Endocrinology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (158, N'Gastroenterology', N'Gastroenterology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (159, N'General Medicine', N'General Medicine', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (160, N'General Surgery', N'General Surgery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (161, N'Gynecology', N'Gynecology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (162, N'Labor and Delivery', N'Labor and Delivery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (163, N'Laboratory', N'Laboratory', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (164, N'Multidisciplinary', N'Multidisciplinary', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (165, N'Neonatal Intensive Care', N'Neonatal Intensive Care', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (166, N'Neurosurgery', N'Neurosurgery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (167, N'Nursery', N'Nursery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (168, N'Nursing', N'Nursing', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (169, N'Obstetrics', N'Obstetrics', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (170, N'Occupational Therapy', N'Occupational Therapy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (171, N'Ophthalmology', N'Ophthalmology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (172, N'Optometry', N'Optometry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (173, N'Orthopedics', N'Orthopedics', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (174, N'Otorhinolaryngology', N'Otorhinolaryngology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (175, N'Pathology', N'Pathology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (176, N'Perioperative', N'Perioperative', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (177, N'Pharmacacy', N'Pharmacacy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (178, N'Physical Medicine', N'Physical Medicine', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (179, N'Plastic Surgery', N'Plastic Surgery', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (180, N'Podiatry', N'Podiatry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (181, N'Psychiatry', N'Psychiatry', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (182, N'Pulmonary', N'Pulmonary', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (183, N'Radiology', N'Radiology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (184, N'Social Services', N'Social Services', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (185, N'Speech Therapy', N'Speech Therapy', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (186, N'Thyroidology', N'Thyroidology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (187, N'Tumor Board', N'Tumor Board', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (188, N'Urology', N'Urology', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (189, N'Veterinary Medicine', N'Veterinary Medicine', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (190, N'Blood Pressure', N'Blood Pressure', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (191, N'34096-8', N'Nursing Home Comprehensive History and Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (192, N'34121-4', N'Interventional Procedure Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (193, N'18743-5', N'Autopsy Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (194, N'34095-0', N'Comprehensive History and Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (195, N'34098-4', N'Conference Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (196, N'11488-4', N'Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (197, N'28574-2', N'Discharge Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (198, N'18842-5', N'Discharge Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (199, N'34109-9', N'Evaluation And Management Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (200, N'34117-2', N'History And Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (201, N'28636-9', N'Initial Evaluation Note', 2)
GO
print 'Processed 200 total records'
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (202, N'28570-0', N'Procedure Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (203, N'11506-3', N'Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (204, N'34133-9', N'Summarization of Episode Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (205, N'11504-8', N'Surgical Operation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (206, N'34138-8', N'Targeted History And Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (207, N'34140-4', N'Transfer of Care Referral Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (208, N'18761-7', N'Transfer Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (209, N'34100-8', N'Critical Care Unit Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (210, N'34126-3', N'Critical Care Unit Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (211, N'34111-5', N'Emergency Department Evaluation And Management Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (212, N'15507-7', N'Emergency Department Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (213, N'34107-3', N'Home Health Education Procedure Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (214, N'34118-0', N'Home Health Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (215, N'34129-7', N'Home Health Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (216, N'34104-0', N'Hospital Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (217, N'34105-7', N'Hospital Discharge Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (218, N'34114-9', N'Hospital Group Counseling Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (219, N'11492-6', N'Hospital History and Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (220, N'34130-5', N'Hospital Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (221, N'34112-3', N'Inpatient Evaluation And Management Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (222, N'34097-6', N'Nursing Home Conference Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (223, N'34113-1', N'Nursing Home Evaluation And Management Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (224, N'34119-8', N'Nursing Home Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (225, N'24611-6', N'Outpatient Confirmatory Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (226, N'34108-1', N'Outpatient Evaluation And Management', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (227, N'34120-6', N'Outpatient Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (228, N'34131-3', N'Outpatient Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (229, N'34137-0', N'Outpatient Surgical Operation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (230, N'34123-0', N'Anesthesia Hospital Pre-Operative Evaluation And Management Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (231, N'28655-9', N'Attending Physician Discharge Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (232, N'28654-2', N'Attending Physician Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (233, N'18733-6', N'Attending Physician Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (234, N'34134-7', N'Attending Physician Outpatient Supervisory Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (235, N'34135-4', N'Attending Physician Cardiology Outpatient Supervisory Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (236, N'34136-2', N'Attending Physician Gastroenterology Outpatient Supervisory Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (237, N'34099-2', N'Cardiology Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (238, N'34094-3', N'Cardiology Hospital Admission History And Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (239, N'34124-8', N'Cardiology Outpatient Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (240, N'34125-5', N'Case Manager Home Health Care Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (241, N'28581-7', N'Chiropractor Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (242, N'18762-5', N'Chiropractor Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (243, N'18763-3', N'Consulting Physician Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (244, N'28569-2', N'Consulting Physician Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (245, N'34127-1', N'Dental Hygienist Outpatient Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (246, N'29761-4', N'Dentistry Discharge Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (247, N'28572-6', N'Dentistry Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (248, N'28577-5', N'Dentistry Procedure Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (249, N'28617-9', N'Dentistry Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (250, N'28583-3', N'Dentistry Surgical Operation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (251, N'28618-7', N'Dentistry Visit Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (252, N'34128-9', N'Dentistry Outpatient Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (253, N'34110-7', N'Diabetology Outpatient Evaluation And Management Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (254, N'18748-4', N'Diagnostic Imaging Report', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (255, N'34101-6', N'General Medicine Outpatient Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (256, N'34115-6', N'Medical Student Hospital History and Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (257, N'28621-1', N'Nurse Practitioner Initial Evaluation Note"', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (258, N'18764-1', N'Nurse Practitioner Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (259, N'28622-9', N'Nursing Discharge Assessment Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (260, N'29753-1', N'Nursing Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (261, N'28623-7', N'Nursing Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (262, N'34139-6', N'Nursing Telephone Encounter Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (263, N'28651-8', N'Nursing Transfer Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (264, N'18734-4', N'Occupational Therapy Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (265, N'11507-1', N'Occupational Therapy Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (266, N'34122-2', N'Pathology Pathology Procedure Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (267, N'34132-1', N'Pharmacy Outpatient Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (268, N'18735-1', N'Physical Therapy Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (269, N'11508-9', N'Physical Therapy Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (270, N'28579-1', N'Physical Therapy Visit Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (271, N'11490-0', N'Physician Discharge Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (272, N'28626-0', N'Physician History and Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (273, N'18736-9', N'Physician Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (274, N'11505-5', N'Physician Procedure Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (275, N'28573-4', N'Physician Surgical Operation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (276, N'28616-1', N'Physician Transfer Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (277, N'28568-4', N'Physician Emergency Department Visit Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (278, N'34106-5', N'Physician Hospital Discharge Summarization Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (279, N'34116-4', N'Physician Nursing Home History and Physical Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (280, N'18737-7', N'Podiatry Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (281, N'28625-2', N'Podiatry Procedure Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (282, N'11509-7', N'Podiatry Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (283, N'28624-5', N'Podiatry Surgical Operation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (284, N'28635-1', N'Psychiatry Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (285, N'28627-8', N'Psychiatry Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (286, N'34102-4', N'Psychiatry Hospital Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (287, N'18738-5', N'Psychology Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (288, N'11510-5', N'Psychology Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (289, N'34103-2', N'Pulmonary Consultation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (290, N'18739-3', N'Social Service Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (291, N'28656-7', N'Social Service Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (292, N'28653-4', N'Social Service Visit Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (293, N'18740-1', N'Speech Therapy Initial Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (294, N'11512-1', N'Speech Therapy Subsequent Visit Evaluation Note', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (295, N'Laboratory Report', N'Laboratory Report', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (296, N'34138-3', N'Remote Health Reading', 2)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (297, N'application/pdf', N'application/pdf', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (298, N'text/x-cda-r2+xml', N'text/x-cda-r2+xml', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (299, N'text/xml', N'text/xml', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (300, N'application/x-hl7', N'application/x-hl7', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (301, N'application/dicom', N'application/dicom', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (302, N'text/plain', N'text/plain', 5)
GO
print 'Processed 300 total records'
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (303, N'multipart/related', N'multipart/related', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (304, N'application/xml', N'application/xml', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (305, N'image/jpg', N'image/jpg', 5)
INSERT [CodeValue] ([codeValueID], [code], [displayName], [codeTypeID]) VALUES (306, N'image/jpeg', N'image/jpeg', 5)
SET IDENTITY_INSERT [CodeValue] OFF
/****** Object:  Table [dbo].[AuditMessageParameterConfiguration]    Script Date: 06/13/2008 14:14:12 ******/
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
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (43, 9, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (44, 9, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (45, 9, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (46, 9, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (47, 9, N'$XDSPatient$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (48, 9, N'$AdhocQuery$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (49, 10, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (50, 10, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (51, 10, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (52, 10, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (53, 10, N'$XDSPatientID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (54, 12, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (55, 12, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (56, 12, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (57, 12, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (58, 12, N'$XDSPatientID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (59, 13, N'$EventIdentification.EventDateTime$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (60, 13, N'$EventIdentification.EventOutcomeIndicator$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (61, 13, N'$ActiveParticipant.UserID.Destination$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (62, 13, N'$AuditSourceIdentification.AuditSourceID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (63, 13, N'$XDSPatientID$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (64, 13, N'$ParticipantObjectIdentification$', N'DYNAMIC', NULL)
INSERT [AuditMessageParameterConfiguration] ([parameterID], [auditMessageID], [parameterName], [parameterType], [parameterValue]) VALUES (65, 9, N'$ActiveParticipant.UserID.Source$', N'DYNAMIC', NULL)
SET IDENTITY_INSERT [AuditMessageParameterConfiguration] OFF
/****** Object:  Table [dbo].[DocumentEntryEventCodeList]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntryEventCodeList]') AND type in (N'U'))
BEGIN
CREATE TABLE [DocumentEntryEventCodeList](
	[documentEntryEventCodeListID] [int] IDENTITY(1,1) NOT NULL,
	[documentEntryID] [int] NULL,
	[eventCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[eventCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_DocumentEntryEventCodeList] PRIMARY KEY CLUSTERED 
(
	[documentEntryEventCodeListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SubmissionSetDocumentFolder]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]') AND type in (N'U'))
BEGIN
CREATE TABLE [SubmissionSetDocumentFolder](
	[SubmissionSetDocumentFolderID] [int] IDENTITY(1,1) NOT NULL,
	[submissionSetID] [int] NULL,
	[folderID] [int] NULL,
	[documentEntryID] [int] NULL,
	[sourceObject] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[targetObject] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[associationXml] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[documentEntryEntryUUID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[documentEntryUniqueID] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[associationType] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_SubmissionSetDocumentFolder] PRIMARY KEY CLUSTERED 
(
	[SubmissionSetDocumentFolderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[DocumentEntryAuthor]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DocumentEntryAuthor]') AND type in (N'U'))
BEGIN
CREATE TABLE [DocumentEntryAuthor](
	[documentEntryAuthorID] [int] IDENTITY(1,1) NOT NULL,
	[authorID] [int] NOT NULL,
	[documentEntryID] [int] NOT NULL,
 CONSTRAINT [PK_DocumentEntryAuthor] PRIMARY KEY CLUSTERED 
(
	[documentEntryAuthorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SubmissionSetAuthor]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SubmissionSetAuthor]') AND type in (N'U'))
BEGIN
CREATE TABLE [SubmissionSetAuthor](
	[submissionSetAuthorID] [int] IDENTITY(1,1) NOT NULL,
	[authorID] [int] NOT NULL,
	[submissionSetID] [int] NOT NULL,
 CONSTRAINT [PK_SubmissionSetAuthor] PRIMARY KEY CLUSTERED 
(
	[submissionSetAuthorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[FolderCodeList]    Script Date: 06/13/2008 14:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[FolderCodeList]') AND type in (N'U'))
BEGIN
CREATE TABLE [FolderCodeList](
	[folderCodeListID] [int] IDENTITY(1,1) NOT NULL,
	[folderID] [int] NOT NULL,
	[eventCodeValue] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[eventCodeDisplayName] [varchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_FolderCodeList] PRIMARY KEY CLUSTERED 
(
	[folderCodeListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_storedQueryDetails_StoredQuery_StoredQueryParameter]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_storedQueryDetails_StoredQuery_StoredQueryParameter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: July 11, 2007
-- Description:	Gets Stored Query details for a given query uuid.
-- =============================================
CREATE PROCEDURE [usp_get_storedQueryDetails_StoredQuery_StoredQueryParameter]
	-- Add the parameters for the stored procedure here
	@queryUUID varchar(128), @returnType varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
	sq.storedQueryID, sq.storedQueryUniqueID, sq.storedQueryName,
	sq.returnType, sq.storedQuerySQLCode, sq.storedQuerySequence,
	sqp.storedQueryParameterID, sqp.storedQueryParameterName, sqp.attribute,
	sqp.isMandatory, sqp.isMultiple, sqp.dependentParameterName, sqp.tableName, sqp.joinConditionSQLCode,
	sqp.whereConditionSQLCode, sqp.storedQueryParameterSequence
	FROM StoredQuery sq
	INNER JOIN StoredQueryParameter sqp ON 
	sqp.storedQueryID = sq.storedQueryID 
	AND sqp.storedQueryUniqueID = sq.storedQueryUniqueID
	WHERE sq.storedQueryUniqueID = @queryUUID AND sq.returnType = @returnType

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_documentEntryDetails_DocumentEntry_Patient]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_documentEntryDetails_DocumentEntry_Patient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_documentEntryDetails_DocumentEntry_Patient] 
	@availabilityStatus varchar(128), 
	@patientUID varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DE.documentEntryID
      ,DE.availabilityStatus
      ,DE.classCodeValue
      ,DE.classCodeDisplayName
      ,DE.comments
      ,DE.confidentialityCodeValue
      ,DE.confidentialityCodeDisplayName
      ,DE.creationTime
      ,DE.entryUUID
      ,DE.formatCodeValue
      ,DE.formatCodeDisplayName
      ,DE.hash
      ,DE.healthcareFacilityTypeCodeValue
      ,DE.healthcareFacilityTypeCodeDisplayName
      ,DE.languageCodeValue
      ,DE.languageCodeDisplayName
      ,DE.legalAuthenticator
      ,DE.mimeType
      ,DE.parentDocumentID
      ,DE.parentDocumentRelationship
      ,DE.patientID
      ,DE.practiceSettingCodeValue
      ,DE.practiceSettingCodeDisplayName
      ,DE.serviceStartTime
      ,DE.serviceStopTime
      ,DE.size
      ,DE.sourcePatientID
      ,DE.sourcePatientInfo
      ,DE.title
      ,DE.typeCodeValue
      ,DE.typeCodeDisplayName
      ,DE.uniqueID
      ,DE.URI
      ,DE.repositoryUniqueID
      ,DE.extrinsicObjectXML
  FROM DocumentEntry DE
INNER JOIN Patient PAT ON PAT.patientID = DE.patientID
WHERE DE.availabilityStatus IN (SELECT Items FROM dbo.USP_SplitString(@availabilityStatus,'','') )
AND PAT.patientUID = @patientUID


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_patient_Patientid]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_patient_Patientid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [usp_get_patient_Patientid]
      (@patientUID varchar(128))    
AS
BEGIN

SET NOCOUNT ON;

SELECT PatientID
FROM [Patient]    
WHERE PatientUID = @patientUID

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_PatientID_Patient]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_PatientID_Patient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: July 11, 2007
-- Description:	Gets the Patient ID for a given Patient UUID
-- =============================================
CREATE PROCEDURE [usp_get_PatientID_Patient] 
	-- Add the parameters for the stored procedure here
	@patientUUID varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT patientID FROM PATIENT WHERE patientUID =  @patientUUID
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_patient_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_patient_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_patient_insert]
(
 @patientUID varchar(128)
,@patientID int output
)
AS
BEGIN

	INSERT INTO [Patient]([patientUID]) VALUES (@patientUID)

	SELECT @patientID = SCOPE_IDENTITY()
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_delete_PatientUID_Patient]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_delete_PatientUID_Patient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_delete_PatientUID_Patient] 
	@patientUID varchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM PATIENT WHERE patientUID = @patientUID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_update_PatientIDs]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_PatientIDs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_update_PatientIDs] 
	@oldPatientUID varchar(128),
	@newPatientUID varchar(128)
AS
BEGIN

SET NOCOUNT ON;

DECLARE @oldPatientID int
DECLARE @newPatientID int

--OLD Patient ID
SELECT @oldPatientID = patientID FROM PATIENT WHERE patientUID =  @oldPatientUID;

--New Patient ID
SELECT @newPatientID = patientID FROM PATIENT WHERE patientUID =  @newPatientUID;

--Update All the Required Tables

--DocumentEntry
UPDATE DocumentEntry SET patientID = @newPatientID WHERE patientID = @oldPatientID;

--Folder
UPDATE Folder SET patientID = @newPatientID WHERE patientID = @oldPatientID;

--SubmissionSet
UPDATE SubmissionSet SET patientID = @newPatientID WHERE patientID = @oldPatientID;


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_submissionSetDetails_SubmissionSet_Patient]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_submissionSetDetails_SubmissionSet_Patient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_submissionSetDetails_SubmissionSet_Patient] 
	@availabilityStatus varchar(128), 
	@patientUID varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT SST.submissionSetID
      ,SST.availabilityStatus
      ,SST.comments
      ,SST.contentTypeCodeValue
      ,SST.contentTypeCodeDisplayName
      ,SST.entryUUID
      ,SST.patientID
      ,SST.sourceID
      ,SST.submissionTime
      ,SST.title
      ,SST.uniqueID
      ,SST.submissionSetXml
FROM SubmissionSet SST
INNER JOIN Patient PAT ON PAT.patientID = SST.patientID
WHERE SST.availabilityStatus IN (SELECT Items FROM dbo.USP_SplitString(@availabilityStatus,'','') )
AND PAT.patientUID = @patientUID


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_folderDetails_Folder_Patient]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_folderDetails_Folder_Patient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_folderDetails_Folder_Patient] 
	@availabilityStatus varchar(128), 
	@patientUID varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT FLD.folderID
      ,FLD.availabilityStatus
      ,FLD.comments
      ,FLD.entryUUID
      ,FLD.lastUpdateTime
      ,FLD.patientID
      ,FLD.title
      ,FLD.uniqueID
      ,FLD.folderXml
  FROM Folder FLD
INNER JOIN Patient PAT ON PAT.patientID = FLD.patientID
WHERE FLD.availabilityStatus IN (SELECT Items FROM dbo.USP_SplitString(@availabilityStatus,'','') )
AND PAT.patientUID = @patientUID

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_codeDetails_CodeType_CodeValue]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_codeDetails_CodeType_CodeValue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_codeDetails_CodeType_CodeValue] 
	-- Add the parameters for the stored procedure here
	@codingScheme varchar(128) = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT CT.codeTypeID, CT.displayName codeTypeDisplayName, CT.classSchemeUUID, 
CT.codingScheme, CV.codeValueID, CV.code, CV.displayName codeValueDisplayName
FROM CodeType CT
INNER JOIN CodeValue CV ON
CV.codeTypeID = CT.codeTypeID
WHERE CT.codingScheme = @codingScheme

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentsBy_EntryUUID_Or_UniqueID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentsBy_EntryUUID_Or_UniqueID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_DocumentsBy_EntryUUID_Or_UniqueID] 
	-- Add the parameters for the stored procedure here
	@documentEntryEntryUUIDs varchar(1024) = null, 
	@documentEntryUniqueIDs varchar(1024) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @documentEntryEntryUUIDs IS NULL
BEGIN   
 
	SELECT DE.documentEntryID, DE.availabilityStatus, DE.entryUUID, DE.patientID, DE.uniqueID, DE.repositoryUniqueID, DE.hash, DE.size, DE.extrinsicObjectXML 
	FROM DocumentEntry DE
	WHERE DE.uniqueID IN (SELECT Items FROM USP_SplitString(@documentEntryUniqueIDs,'','') );

END
ELSE
BEGIN

	SELECT DE.documentEntryID, DE.availabilityStatus, DE.entryUUID, DE.patientID, DE.uniqueID, DE.repositoryUniqueID, DE.hash, DE.size, DE.extrinsicObjectXML 
	FROM DocumentEntry DE
	WHERE DE.entryUUID IN (SELECT Items FROM USP_SplitString(@documentEntryEntryUUIDs,'','') );

END


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentEntry_By_documentEntryIDs]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentEntry_By_documentEntryIDs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_DocumentEntry_By_documentEntryIDs] 
	@documentEntryIDs varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT documentEntryID, availabilityStatus, classCodeValue, classCodeDisplayName, comments, confidentialityCodeValue, confidentialityCodeDisplayName, creationTime, entryUUID, formatCodeValue, formatCodeDisplayName, hash, healthcareFacilityTypeCodeValue, healthcareFacilityTypeCodeDisplayName, languageCodeValue, languageCodeDisplayName, legalAuthenticator, mimeType, parentDocumentID, parentDocumentRelationship, patientID, practiceSettingCodeValue, practiceSettingCodeDisplayName, serviceStartTime, serviceStopTime, size, sourcePatientID, sourcePatientInfo, title, typeCodeValue, typeCodeDisplayName, uniqueID, URI, repositoryUniqueID, extrinsicObjectXML
	FROM DocumentEntry
	WHERE documentEntryID IN(SELECT Items FROM USP_SplitString(@documentEntryIDs,'',''))

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentsAndAssociations]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentsAndAssociations]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_DocumentsAndAssociations] 
	-- Add the parameters for the stored procedure here
	@documentEntryEntryUUIDs varchar(128) = null, 
	@documentEntryUniqueIDs varchar(128) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @documentEntryEntryUUIDs IS NULL
BEGIN   
 
	SELECT DE.documentEntryID, DE.availabilityStatus, DE.entryUUID, DE.patientID, DE.uniqueID, DE.repositoryUniqueID, DE.extrinsicObjectXML 
	FROM DocumentEntry DE
	WHERE DE.uniqueID IN (SELECT Items FROM dbo.USP_SplitString(@documentEntryUniqueIDs,'','') );

	SELECT SSDF.documentEntryID, SSDF.associationXml 
	FROM SubmissionSetDocumentFolder SSDF 
	WHERE SSDF.documentEntryUniqueID IN (SELECT Items FROM dbo.USP_SplitString(@documentEntryUniqueIDs,'','') );

END
ELSE
BEGIN

	SELECT DE.documentEntryID, DE.availabilityStatus, DE.entryUUID, DE.patientID, DE.uniqueID, DE.repositoryUniqueID, DE.extrinsicObjectXML 
	FROM DocumentEntry DE
	WHERE DE.entryUUID IN (SELECT Items FROM dbo.USP_SplitString(@documentEntryEntryUUIDs,'','') );

	SELECT SSDF.documentEntryID, SSDF.associationXml 
	FROM SubmissionSetDocumentFolder SSDF 
	WHERE SSDF.documentEntryEntryUUID IN (SELECT Items FROM dbo.USP_SplitString(@documentEntryEntryUUIDs,'','') );

END


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetsByTargetObjectIDs]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetsByTargetObjectIDs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_SubmissionSetsByTargetObjectIDs] 
	@targetObjectIDs varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


		SELECT SST.submissionSetID, SST.submissionSetXml, SSTDF.SubmissionSetDocumentFolderID, SSTDF.associationXml 
		FROM SubmissionSet SST
		INNER JOIN SubmissionSetDocumentFolder SSTDF
		ON SST.submissionSetID = SSTDF.submissionSetID
		WHERE SSTDF.targetObject IN (SELECT Items FROM USP_SplitString(@targetObjectIDs,'','')) AND
		SSTDF.submissionSetID IS NOT NULL

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UniqueID_Duplicate_select]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_UniqueID_Duplicate_select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [usp_UniqueID_Duplicate_select]
           (
		    @uniqueIds varchar(1024)
           )
           
AS
BEGIN

SELECT uniqueid from folder where uniqueid in (select Items from dbo.USP_SplitString(@uniqueIds,'',''))
UNION
SELECT uniqueid from submissionSet where uniqueid in (select Items from dbo.USP_SplitString(@uniqueIds,'',''))


--Document Unique Ids are not required to be part of validation
--SELECT uniqueid from documentEntry where uniqueid in (select Items from dbo.USP_SplitString(@uniqueIds,'',''))
--UNION
--SELECT uniqueid from folder where uniqueid in (select Items from dbo.USP_SplitString(@uniqueIds,'',''))
--UNION
--SELECT uniqueid from submissionSet where uniqueid in (select Items from dbo.USP_SplitString(@uniqueIds,'',''))

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetDocumentFolder_By_ID_And_associationType]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetDocumentFolder_By_ID_And_associationType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_SubmissionSetDocumentFolder_By_ID_And_associationType] 
	-- Add the parameters for the stored procedure here
	@documentEntryUUID varchar(1024), 
	@documentEntryUniqueID varchar(1024),
	@associationTypes varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @documentEntryUUID IS NULL
BEGIN   
 
	SELECT SubmissionSetDocumentFolderID, submissionSetID, folderID, documentEntryID, sourceObject, targetObject, associationXml, documentEntryEntryUUID, documentEntryUniqueID, associationType
	FROM SubmissionSetDocumentFolder 
	WHERE documentEntryUniqueID = @documentEntryUniqueID AND
	associationType IN (SELECT Items FROM USP_SplitString(@associationTypes,'',''))

END
ELSE
BEGIN

	SELECT SubmissionSetDocumentFolderID, submissionSetID, folderID, documentEntryID, sourceObject, targetObject, associationXml, documentEntryEntryUUID, documentEntryUniqueID, associationType
	FROM SubmissionSetDocumentFolder 
	WHERE documentEntryEntryUUID = @documentEntryUUID AND
	associationType IN (SELECT Items FROM USP_SplitString(@associationTypes,'',''))

END

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_Folders_By_FolderIDs]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_Folders_By_FolderIDs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_Folders_By_FolderIDs] 
	@folderIDs varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT  folderID, availabilityStatus, comments, entryUUID, lastUpdateTime, patientID, title, uniqueID, folderXml
	FROM FOLDER
	WHERE folderID IN(SELECT Items FROM USP_SplitString(@folderIDs,'',''))

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_AssociationsBy_EntryUUID_Or_UniqueID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_AssociationsBy_EntryUUID_Or_UniqueID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_AssociationsBy_EntryUUID_Or_UniqueID] 
	-- Add the parameters for the stored procedure here
	@documentEntryEntryUUIDs varchar(1024) = null, 
	@documentEntryUniqueIDs varchar(1024) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @documentEntryEntryUUIDs IS NULL
BEGIN   
 
	SELECT SSDF.documentEntryID, SSDF.associationXml 
	FROM SubmissionSetDocumentFolder SSDF 
	WHERE SSDF.documentEntryUniqueID IN (SELECT Items FROM dbo.USP_SplitString(@documentEntryUniqueIDs,'','') );

END
ELSE
BEGIN

	SELECT SSDF.documentEntryID, SSDF.associationXml 
	FROM SubmissionSetDocumentFolder SSDF 
	WHERE SSDF.documentEntryEntryUUID IN (SELECT Items FROM dbo.USP_SplitString(@documentEntryEntryUUIDs,'','') );

END


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_Association_For_folderIDs]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_Association_For_folderIDs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_Association_For_folderIDs] 
	@folderIDs varchar(1024) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT SSTDF.SubmissionSetDocumentFolderID, SSTDF.submissionSetID, SSTDF.folderID, SSTDF.documentEntryID, SSTDF.sourceObject, SSTDF.targetObject, SSTDF.associationXml, SSTDF.documentEntryEntryUUID, SSTDF.documentEntryUniqueID
FROM SubmissionSetDocumentFolder SSTDF
WHERE SSTDF.folderID IN (SELECT Items FROM USP_SplitString(@folderIDs,'','') )
AND SSTDF.documentEntryID IS NOT NULL

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_FolderCodeList_By_folderIDS]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_FolderCodeList_By_folderIDS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_FolderCodeList_By_folderIDS] 
	@folderIDs varchar(1024),
	@eventCodeValues varchar(1024),
	@eventCodeDisplayNames varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT folderCodeListID, folderID, eventCodeValue, eventCodeDisplayName
	FROM FolderCodeList
	WHERE folderID IN (SELECT Items FROM USP_SplitString(@folderIDs,'',''))
	AND eventCodeValue IN (SELECT Items FROM USP_SplitString(@eventCodeValues,'',''))
	AND eventCodeDisplayName IN (SELECT Items FROM USP_SplitString(@eventCodeDisplayNames,'',''));

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_AssociationsBy_sourceObject_Or_targetObject]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_AssociationsBy_sourceObject_Or_targetObject]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_AssociationsBy_sourceObject_Or_targetObject]
	-- Add the parameters for the stored procedure here
	@uuids varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT SubmissionSetDocumentFolderID, submissionSetID, folderID, documentEntryID, sourceObject, targetObject, associationXml 
	FROM SubmissionSetDocumentFolder SSDF
	WHERE (sourceObject IN (SELECT Items FROM USP_SplitString(@uuids,'','')) OR targetObject IN (SELECT Items FROM USP_SplitString(@uuids,'','')) )


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_Folders_By_entryUUID_Or_uniqueID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_Folders_By_entryUUID_Or_uniqueID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_Folders_By_entryUUID_Or_uniqueID] 
	@entryUUID varchar(1024) = null, 
	@uniqueID varchar(1024) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @entryUUID IS NULL
BEGIN 

SELECT FLD.folderID, FLD.availabilityStatus, FLD.entryUUID, FLD.patientID, FLD.uniqueID, FLD.folderXml
FROM FOLDER FLD
WHERE FLD.uniqueID IN (SELECT Items FROM USP_SplitString(@uniqueID,'',''));

END
ELSE
BEGIN

SELECT FLD.folderID, FLD.availabilityStatus, FLD.entryUUID, FLD.patientID, FLD.uniqueID, FLD.folderXml
FROM FOLDER FLD
WHERE FLD.entryUUID IN (SELECT Items FROM USP_SplitString(@entryUUID,'',''));


END

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_registryLog_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_registryLog_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [usp_registryLog_insert]
           (
			
           @requesterIdentity varchar(128)
           ,@requestMetadata text
           ,@transactionName varchar(128)
           ,@startTime datetime
           ,@finishTime datetime
           ,@result varchar(128)
           ,@submissionSetID int)	

AS
BEGIN

INSERT INTO [RegistryLog]
           (
           [requesterIdentity]
           ,[requestMetadata]
           ,[transactionName]
           ,[startTime]
           ,[finishTime]
           ,[result]
           ,[submissionSetID])
     VALUES
        (
			
           @requesterIdentity
           ,@requestMetadata
           ,@transactionName
           ,@startTime
           ,@finishTime
           ,@result
           ,@submissionSetID)
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_update_AvailabilityStatus_documentEntry]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_AvailabilityStatus_documentEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_update_AvailabilityStatus_documentEntry] 
	@entryUUID varchar(128), 
	@availabilityStatus varchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [DocumentEntry] SET availabilityStatus = @availabilityStatus WHERE entryUUID = @entryUUID;
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_update_patientUID_documentEntry]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_patientUID_documentEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_update_patientUID_documentEntry] 
@documentEntryID int,
@patientUID varchar(128),
@extrinsicObjectXML text
AS
BEGIN

	UPDATE DocumentEntry SET sourcePatientID = @patientUID, extrinsicObjectXML = @extrinsicObjectXML
	WHERE documentEntryID = @documentEntryID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_set_DocuementAvailability]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_set_DocuementAvailability]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [usp_set_DocuementAvailability]
(
  @documentEntryID int ,
  @AvailStatus varchar(128)
 )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update DocumentEntry 
	SET AvailabilityStatus = @AvailStatus  
	where DocumentEntryID = @documentEntryID

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_documentEntry_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_documentEntry_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_documentEntry_insert]
         (  
           @availabilityStatus varchar(128)
           ,@classCodeValue varchar(128)
           ,@classCodeDisplayName varchar(128)			
           ,@comments text
           ,@confidentialityCodeValue varchar(128)
           ,@confidentialityCodeDisplayName varchar(128)
           ,@creationTime datetime
           ,@entryUUID varchar(128)
           ,@formatCodeValue varchar(128)
           ,@formatCodeDisplayName varchar(128)
           ,@hash varchar(128)
           ,@healthcareFacilityTypeCodeValue varchar(128)
           ,@healthcareFacilityTypeCodeDisplayName varchar(128)
           ,@languageCodeValue varchar(128)
           ,@languageCodeDisplayName varchar(128)
           ,@legalAuthenticator varchar(128)
           ,@mimeType varchar(128)
           ,@parentDocumentID varchar(128)
           ,@parentDocumentRelationship varchar(128)
           ,@patientID int
           ,@practiceSettingCodeValue varchar(128)
           ,@practiceSettingCodeDisplayName varchar(128)
           ,@serviceStartTime datetime
           ,@serviceStopTime datetime
           ,@size int
           ,@sourcePatientID varchar(128)
           ,@sourcePatientInfo text
           ,@title varchar(128)
           ,@typeCodeValue varchar(128)
           ,@typeCodeDisplayName varchar(128)
           ,@uniqueID varchar(128)
           ,@URI varchar(1000)
           ,@repositoryUniqueID varchar(128)
		   ,@extrinsicObjectXML text
           ,@documentEntryID int OUTPUT )

AS

BEGIN

INSERT INTO [DocumentEntry]
          
values(
           @availabilityStatus
           ,@classCodeValue
           ,@classCodeDisplayName
           ,@comments
           ,@confidentialityCodeValue
           ,@confidentialityCodeDisplayName
           ,@creationTime
           ,@entryUUID
           ,@formatCodeValue
           ,@formatCodeDisplayName
           ,@hash
           ,@healthcareFacilityTypeCodeValue
           ,@healthcareFacilityTypeCodeDisplayName
           ,@languageCodeValue
           ,@languageCodeDisplayName
           ,@legalAuthenticator
           ,@mimeType
           ,@parentDocumentID
           ,@parentDocumentRelationship
           ,@patientID
           ,@practiceSettingCodeValue
           ,@practiceSettingCodeDisplayName
           ,@serviceStartTime
           ,@serviceStopTime
           ,@size
           ,@sourcePatientID
           ,@sourcePatientInfo
           ,@title
           ,@typeCodeValue
           ,@typeCodeDisplayName
           ,@uniqueID
           ,@URI
           ,@repositoryUniqueID
		   ,@extrinsicObjectXML
   )

	SELECT @documentEntryID = SCOPE_IDENTITY()

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_documentEntryXml]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_documentEntryXml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_documentEntryXml] 
	-- Add the parameters for the stored procedure here
	@documentEntryID int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT documentEntryID, patientID, extrinsicObjectXML 
	FROM DocumentEntry
	WHERE documentEntryID = @documentEntryID
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_update_AvailabilityStatus_ExtrinsicObjectXml_documentEntry]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_AvailabilityStatus_ExtrinsicObjectXml_documentEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================  
-- Author:  Sachin Joshi  
-- Create date:   
-- Description:   
-- =============================================  
CREATE PROCEDURE [usp_update_AvailabilityStatus_ExtrinsicObjectXml_documentEntry]   
 @entryUUID varchar(128),   
 @availabilityStatus varchar(128),
 @extrinsicObjectXML text
AS  
BEGIN  
 SET NOCOUNT ON;  
  
	UPDATE [DocumentEntry] SET availabilityStatus = @availabilityStatus, extrinsicObjectXML = @extrinsicObjectXML 
	WHERE entryUUID = @entryUUID;  

END  ' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_xmlEntries]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_xmlEntries]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_xmlEntries] 
	@patientID int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT documentEntryID, sourcePatientID, extrinsicObjectXML FROM DocumentEntry WHERE patientID = @patientID;

	SELECT folderID, folderXml FROM Folder WHERE patientID = @patientID;

	SELECT submissionSetID, submissionSetXml FROM SubmissionSet WHERE patientID = @patientID;

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_update_patientUID_SubmissionSet]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_patientUID_SubmissionSet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_update_patientUID_SubmissionSet]
@submissionSetID int,
@submissionSetXml text
AS
BEGIN

	UPDATE SubmissionSet SET submissionSetXml = @submissionSetXml
	WHERE submissionSetID = @submissionSetID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_submissionSet_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_submissionSet_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_submissionSet_insert]
(
            @availabilityStatus varchar(128)
           ,@comments text
           ,@contentTypeCodeValue varchar(128)
           ,@contentTypeCodeDisplayName varchar(128)
           ,@entryUUID varchar(128)
           ,@patientID int
           ,@sourceID varchar(128)
           ,@submissionTime datetime
           ,@title varchar(128)
           ,@uniqueID varchar(128)
		   ,@submissionSetXml text
           ,@submissionSetID int OUTPUT
          )

AS
BEGIN
INSERT INTO [SubmissionSet]
           (
            [availabilityStatus]
           ,[comments]
           ,[contentTypeCodeValue]
           ,[contentTypeCodeDisplayName]
           ,[entryUUID]
           ,[patientID]
           ,[sourceID]
           ,[submissionTime]
           ,[title]
		   ,[submissionSetXml]
           ,[uniqueID])
     VALUES
            (
			@availabilityStatus
           ,@comments
           ,@contentTypeCodeValue
           ,@contentTypeCodeDisplayName
           ,@entryUUID
           ,@patientID
           ,@sourceID
           ,@submissionTime
           ,@title
		   ,@submissionSetXml
           ,@uniqueID
			)

 SELECT @submissionSetID=SCOPE_IDENTITY()

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetAndAssociation_By_entryUUID_Or_uniqueID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetAndAssociation_By_entryUUID_Or_uniqueID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_SubmissionSetAndAssociation_By_entryUUID_Or_uniqueID] 
	-- Add the parameters for the stored procedure here
	@entryUUID varchar(1024) = null, 
	@uniqueID varchar(1024) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @entryUUID IS NULL
BEGIN   
 
	SELECT SST.submissionSetID, SST.entryUUID, SST.uniqueID, SST.submissionSetXml, SSTDF.SubmissionSetDocumentFolderID, SSTDF.folderID, SSTDF.documentEntryID, SSTDF.sourceObject, SSTDF.targetObject, SSTDF.associationXml
	FROM SubmissionSet SST
	INNER JOIN SubmissionSetDocumentFolder SSTDF
	ON SSTDF.submissionSetID = SST.submissionSetID	
	WHERE SST.uniqueID = @uniqueID;

END
ELSE
BEGIN

	SELECT SST.submissionSetID, SST.entryUUID, SST.uniqueID, SST.submissionSetXml, SSTDF.SubmissionSetDocumentFolderID, SSTDF.folderID, SSTDF.documentEntryID, SSTDF.sourceObject, SSTDF.targetObject, SSTDF.associationXml
	FROM SubmissionSet SST
	INNER JOIN SubmissionSetDocumentFolder SSTDF
	ON SSTDF.submissionSetID = SST.submissionSetID	
	WHERE SST.entryUUID = @entryUUID;

END


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_folder_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_folder_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_folder_insert]
          (
            @availabilityStatus varchar(128)
           ,@comments text
           ,@entryUUID varchar(128)
           ,@lastUpdateTime datetime
           ,@patientID int
           ,@title varchar(128)
           ,@uniqueID varchar(128)
		   ,@folderXml text			
           ,@folderID int OUTPUT
          )
AS
BEGIN
INSERT INTO [Folder]
           (
            [availabilityStatus]
           ,[comments]
           ,[entryUUID]
           ,[lastUpdateTime]
           ,[patientID]
           ,[title]
           ,[uniqueID]
		   ,[folderXml]
           )
     VALUES
           (
		    @availabilityStatus
           ,@comments
           ,@entryUUID
           ,@lastUpdateTime
           ,@patientID
           ,@title
           ,@uniqueID
		   ,@folderXml
            )

SELECT @folderID=SCOPE_IDENTITY()

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_update_patientUID_Folder]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_update_patientUID_Folder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_update_patientUID_Folder] 
@folderID int,
@folderXml text
AS
BEGIN

	UPDATE Folder SET folderXml = @folderXml
	WHERE folderID = @folderID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_DocumentEntryEventCodeList_By_DocumentEntryID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_DocumentEntryEventCodeList_By_DocumentEntryID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_DocumentEntryEventCodeList_By_DocumentEntryID] 
	@documentEntryID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT documentEntryEventCodeListID, documentEntryID, eventCodeValue, eventCodeDisplayName
	FROM DocumentEntryEventCodeList
	WHERE documentEntryID = @documentEntryID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_submissionSetDocumentFolder_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_submissionSetDocumentFolder_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_submissionSetDocumentFolder_insert]

 (
	@documentEntryID int
   ,@submissionSetID int
   ,@folderID int
	,@sourceObject varchar(128)
	,@targetObject varchar(128)
	,@associationXml text
	,@documentEntryEntryUUID varchar(128)
	,@documentEntryUniqueID varchar(128)
	,@associationType varchar(128)
)
AS
BEGIN
	 INSERT INTO [SubmissionSetDocumentFolder]
           (
				 [documentEntryID]
				,[submissionSetID]
				,[folderID]
				,[sourceObject]
				,[targetObject]
				,[associationXml]
				,[documentEntryEntryUUID]
				,[documentEntryUniqueID]
				,[associationType]
			)
     VALUES
		    (
				 @documentEntryID
				,@submissionSetID
				,@folderID
				,@sourceObject
				,@targetObject
				,@associationXml
				,@documentEntryEntryUUID
				,@documentEntryUniqueID
				,@associationType
             )
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetDocumentFolder_By_EntryUUID_Or_UniqueID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetDocumentFolder_By_EntryUUID_Or_UniqueID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_SubmissionSetDocumentFolder_By_EntryUUID_Or_UniqueID] 
	-- Add the parameters for the stored procedure here
	@documentEntryUUID varchar(1024), 
	@documentEntryUniqueID varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @documentEntryUUID IS NULL
BEGIN   
 
	SELECT SubmissionSetDocumentFolderID, submissionSetID, folderID, documentEntryID, sourceObject, targetObject, associationType, associationXml, documentEntryEntryUUID, documentEntryUniqueID
	FROM SubmissionSetDocumentFolder 
	WHERE documentEntryUniqueID = @documentEntryUniqueID;

END
ELSE
BEGIN

	SELECT SubmissionSetDocumentFolderID, submissionSetID, folderID, documentEntryID, sourceObject, targetObject, associationType, associationXml, documentEntryEntryUUID, documentEntryUniqueID
	FROM SubmissionSetDocumentFolder 
	WHERE documentEntryEntryUUID = @documentEntryUUID;

END

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_PatientMessageConfiguration_get]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PatientMessageConfiguration_get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_PatientMessageConfiguration_get] 
	@patientMessageKey varchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	SELECT patientMessageID, patientMessageKey, patientMessageValue
	FROM PatientMessageConfiguration
	WHERE patientMessageKey = @patientMessageKey;
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_submissionSet_author_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_submissionSet_author_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [usp_submissionSet_author_insert]
(
		    @submissionSetID int
           ,@authorInstitution varchar(128)
           ,@authorPerson varchar(128)
           ,@authorRole varchar(128)
           ,@authorSpeciality varchar(128)
           ,@authorID int OUTPUT
)
AS
BEGIN

 

     INSERT INTO [Author]
           (
            [authorInstitution]
           ,[authorPerson]
           ,[authorRole]
           ,[authorSpeciality]
           )
     VALUES
           (
            @authorInstitution
           ,@authorPerson
           ,@authorRole
           ,@authorSpeciality
           )

    SELECT @authorID = SCOPE_IDENTITY()


    INSERT INTO  [SubmissionSetAuthor]
        ([authorID],[submissionSetID])
    VALUES
        (
         @authorID,@submissionSetID
		)
 
END
--If any error occurs in between the transaction, SQL Server automatically rolls it back.

' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_document_author_insert]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_document_author_insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [usp_document_author_insert]
(
		    @documentID int
           ,@authorInstitution varchar(128)
           ,@authorPerson varchar(128)
           ,@authorRole varchar(128)
           ,@authorSpeciality varchar(128)
           ,@authorID  int OUTPUT
)
AS
BEGIN

	

     INSERT INTO [Author]
           (
            [authorInstitution]
           ,[authorPerson]
           ,[authorRole]
           ,[authorSpeciality]
           )
     VALUES
           (
            @authorInstitution
           ,@authorPerson
           ,@authorRole
           ,@authorSpeciality
           )

    SELECT @authorID = SCOPE_IDENTITY()


    INSERT INTO  [DocumentEntryAuthor]
        ([authorID],[documentEntryID])
    VALUES
        (
         @authorID,@documentID
		)
  
END
--If any error occurs in between the transaction, SQL Server automatically rolls it back.
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_SubmissionSetAuthor_by_submissionSetID]    Script Date: 06/13/2008 14:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_get_SubmissionSetAuthor_by_submissionSetID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Sachin Joshi
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [usp_get_SubmissionSetAuthor_by_submissionSetID] 
	@submissionSetID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT AUT.authorID, AUT.authorInstitution, AUT.authorPerson, AUT.authorRole, AUT.authorSpeciality, SSTA.submissionSetID
	FROM Author AUT
	INNER JOIN SubmissionSetAuthor SSTA ON SSTA.authorID = AUT.authorID
	WHERE SSTA.submissionSetID = @submissionSetID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_get_configurationDetails_ConfigurationEntry]    Script Date: 06/13/2008 14:14:12 ******/
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
/****** Object:  ForeignKey [FK_AuditMessageParameter_AuditMessage]    Script Date: 06/13/2008 14:14:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_AuditMessageParameter_AuditMessage]') AND parent_object_id = OBJECT_ID(N'[AuditMessageParameterConfiguration]'))
ALTER TABLE [AuditMessageParameterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_AuditMessageParameter_AuditMessage] FOREIGN KEY([auditMessageID])
REFERENCES [AuditMessageConfiguration] ([auditMessageID])
GO
ALTER TABLE [AuditMessageParameterConfiguration] CHECK CONSTRAINT [FK_AuditMessageParameter_AuditMessage]
GO
/****** Object:  ForeignKey [FK_CodeValue_CodeType]    Script Date: 06/13/2008 14:14:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CodeValue_CodeType]') AND parent_object_id = OBJECT_ID(N'[CodeValue]'))
ALTER TABLE [CodeValue]  WITH CHECK ADD  CONSTRAINT [FK_CodeValue_CodeType] FOREIGN KEY([codeTypeID])
REFERENCES [CodeType] ([codeTypeID])
GO
ALTER TABLE [CodeValue] CHECK CONSTRAINT [FK_CodeValue_CodeType]
GO
/****** Object:  ForeignKey [FK_DocumentEntry_Patient]    Script Date: 06/13/2008 14:14:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntry_Patient]') AND parent_object_id = OBJECT_ID(N'[DocumentEntry]'))
ALTER TABLE [DocumentEntry]  WITH CHECK ADD  CONSTRAINT [FK_DocumentEntry_Patient] FOREIGN KEY([patientID])
REFERENCES [Patient] ([patientID])
GO
ALTER TABLE [DocumentEntry] CHECK CONSTRAINT [FK_DocumentEntry_Patient]
GO
/****** Object:  ForeignKey [FK_DocumentEntryAuthor_Author]    Script Date: 06/13/2008 14:14:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntryAuthor_Author]') AND parent_object_id = OBJECT_ID(N'[DocumentEntryAuthor]'))
ALTER TABLE [DocumentEntryAuthor]  WITH CHECK ADD  CONSTRAINT [FK_DocumentEntryAuthor_Author] FOREIGN KEY([authorID])
REFERENCES [Author] ([authorID])
GO
ALTER TABLE [DocumentEntryAuthor] CHECK CONSTRAINT [FK_DocumentEntryAuthor_Author]
GO
/****** Object:  ForeignKey [FK_DocumentEntryAuthor_DocumentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntryAuthor_DocumentEntry]') AND parent_object_id = OBJECT_ID(N'[DocumentEntryAuthor]'))
ALTER TABLE [DocumentEntryAuthor]  WITH CHECK ADD  CONSTRAINT [FK_DocumentEntryAuthor_DocumentEntry] FOREIGN KEY([documentEntryID])
REFERENCES [DocumentEntry] ([documentEntryID])
GO
ALTER TABLE [DocumentEntryAuthor] CHECK CONSTRAINT [FK_DocumentEntryAuthor_DocumentEntry]
GO
/****** Object:  ForeignKey [FK_DocumentEntryEventCodeList_DocumentEntry]    Script Date: 06/13/2008 14:14:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_DocumentEntryEventCodeList_DocumentEntry]') AND parent_object_id = OBJECT_ID(N'[DocumentEntryEventCodeList]'))
ALTER TABLE [DocumentEntryEventCodeList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentEntryEventCodeList_DocumentEntry] FOREIGN KEY([documentEntryID])
REFERENCES [DocumentEntry] ([documentEntryID])
GO
ALTER TABLE [DocumentEntryEventCodeList] CHECK CONSTRAINT [FK_DocumentEntryEventCodeList_DocumentEntry]
GO
/****** Object:  ForeignKey [FK_Folder_Patient]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Folder_Patient]') AND parent_object_id = OBJECT_ID(N'[Folder]'))
ALTER TABLE [Folder]  WITH CHECK ADD  CONSTRAINT [FK_Folder_Patient] FOREIGN KEY([patientID])
REFERENCES [Patient] ([patientID])
GO
ALTER TABLE [Folder] CHECK CONSTRAINT [FK_Folder_Patient]
GO
/****** Object:  ForeignKey [FK_FolderCodeList_Folder]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_FolderCodeList_Folder]') AND parent_object_id = OBJECT_ID(N'[FolderCodeList]'))
ALTER TABLE [FolderCodeList]  WITH CHECK ADD  CONSTRAINT [FK_FolderCodeList_Folder] FOREIGN KEY([folderID])
REFERENCES [Folder] ([folderID])
GO
ALTER TABLE [FolderCodeList] CHECK CONSTRAINT [FK_FolderCodeList_Folder]
GO
/****** Object:  ForeignKey [FK_SubmissionSet_Patient]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSet_Patient]') AND parent_object_id = OBJECT_ID(N'[SubmissionSet]'))
ALTER TABLE [SubmissionSet]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionSet_Patient] FOREIGN KEY([patientID])
REFERENCES [Patient] ([patientID])
GO
ALTER TABLE [SubmissionSet] CHECK CONSTRAINT [FK_SubmissionSet_Patient]
GO
/****** Object:  ForeignKey [FK_SubmissionSetAuthor_Author]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetAuthor_Author]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetAuthor]'))
ALTER TABLE [SubmissionSetAuthor]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionSetAuthor_Author] FOREIGN KEY([authorID])
REFERENCES [Author] ([authorID])
GO
ALTER TABLE [SubmissionSetAuthor] CHECK CONSTRAINT [FK_SubmissionSetAuthor_Author]
GO
/****** Object:  ForeignKey [FK_SubmissionSetAuthor_SubmissionSet]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetAuthor_SubmissionSet]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetAuthor]'))
ALTER TABLE [SubmissionSetAuthor]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionSetAuthor_SubmissionSet] FOREIGN KEY([submissionSetID])
REFERENCES [SubmissionSet] ([submissionSetID])
GO
ALTER TABLE [SubmissionSetAuthor] CHECK CONSTRAINT [FK_SubmissionSetAuthor_SubmissionSet]
GO
/****** Object:  ForeignKey [FK_SubmissionSetDocumentFolder_DocumentEntry]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetDocumentFolder_DocumentEntry]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]'))
ALTER TABLE [SubmissionSetDocumentFolder]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionSetDocumentFolder_DocumentEntry] FOREIGN KEY([documentEntryID])
REFERENCES [DocumentEntry] ([documentEntryID])
GO
ALTER TABLE [SubmissionSetDocumentFolder] CHECK CONSTRAINT [FK_SubmissionSetDocumentFolder_DocumentEntry]
GO
/****** Object:  ForeignKey [FK_SubmissionSetDocumentFolder_Folder]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetDocumentFolder_Folder]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]'))
ALTER TABLE [SubmissionSetDocumentFolder]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionSetDocumentFolder_Folder] FOREIGN KEY([folderID])
REFERENCES [Folder] ([folderID])
GO
ALTER TABLE [SubmissionSetDocumentFolder] CHECK CONSTRAINT [FK_SubmissionSetDocumentFolder_Folder]
GO
/****** Object:  ForeignKey [FK_SubmissionSetDocumentFolder_SubmissionSet]    Script Date: 06/13/2008 14:14:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SubmissionSetDocumentFolder_SubmissionSet]') AND parent_object_id = OBJECT_ID(N'[SubmissionSetDocumentFolder]'))
ALTER TABLE [SubmissionSetDocumentFolder]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionSetDocumentFolder_SubmissionSet] FOREIGN KEY([submissionSetID])
REFERENCES [SubmissionSet] ([submissionSetID])
GO
ALTER TABLE [SubmissionSetDocumentFolder] CHECK CONSTRAINT [FK_SubmissionSetDocumentFolder_SubmissionSet]
GO
