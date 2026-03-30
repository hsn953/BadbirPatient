
USE [BADBIR_Dev5]
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingStatuslkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingStatuslkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientCohortTrackingStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Get_List

AS


				
				SELECT
					[ptrackingstatusid],
					[ptrackingstatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortTrackingStatuslkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingStatuslkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingStatuslkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingStatuslkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientCohortTrackingStatuslkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [ptrackingstatusid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ptrackingstatusid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ptrackingstatusid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortTrackingStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[ptrackingstatusid], O.[ptrackingstatus], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbPatientCohortTrackingStatuslkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ptrackingstatusid] = PageIndex.[ptrackingstatusid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortTrackingStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingStatuslkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingStatuslkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingStatuslkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientCohortTrackingStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Insert
(

	@Ptrackingstatusid int   ,

	@Ptrackingstatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPatientCohortTrackingStatuslkp]
					(
					[ptrackingstatusid]
					,[ptrackingstatus]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Ptrackingstatusid
					,@Ptrackingstatus
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingStatuslkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingStatuslkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingStatuslkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientCohortTrackingStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Update
(

	@Ptrackingstatusid int   ,

	@OriginalPtrackingstatusid int   ,

	@Ptrackingstatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientCohortTrackingStatuslkp]
				SET
					[ptrackingstatusid] = @Ptrackingstatusid
					,[ptrackingstatus] = @Ptrackingstatus
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[ptrackingstatusid] = @OriginalPtrackingstatusid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingStatuslkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingStatuslkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingStatuslkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientCohortTrackingStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Delete
(

	@Ptrackingstatusid int   
)
AS


				DELETE FROM [dbo].[bbPatientCohortTrackingStatuslkp] WITH (ROWLOCK) 
				WHERE
					[ptrackingstatusid] = @Ptrackingstatusid
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingStatuslkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingStatuslkp_GetByPtrackingstatusid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingStatuslkp_GetByPtrackingstatusid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_GetByPtrackingstatusid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTrackingStatuslkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_GetByPtrackingstatusid
(

	@Ptrackingstatusid int   
)
AS


				SELECT
					[ptrackingstatusid],
					[ptrackingstatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortTrackingStatuslkp]
				WHERE
					[ptrackingstatusid] = @Ptrackingstatusid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingStatuslkp_GetByPtrackingstatusid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingStatuslkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingStatuslkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientCohortTrackingStatuslkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingStatuslkp_Find
(

	@SearchUsingOR bit   = null ,

	@Ptrackingstatusid int   = null ,

	@Ptrackingstatus varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ptrackingstatusid]
	, [ptrackingstatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientCohortTrackingStatuslkp]
    WHERE 
	 ([ptrackingstatusid] = @Ptrackingstatusid OR @Ptrackingstatusid IS NULL)
	AND ([ptrackingstatus] = @Ptrackingstatus OR @Ptrackingstatus IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ptrackingstatusid]
	, [ptrackingstatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientCohortTrackingStatuslkp]
    WHERE 
	 ([ptrackingstatusid] = @Ptrackingstatusid AND @Ptrackingstatusid is not null)
	OR ([ptrackingstatus] = @Ptrackingstatus AND @Ptrackingstatus is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingStatuslkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatient table
-- Table Comment: Patient
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_Get_List

AS


				
				SELECT
					[patientid],
					[firststudyno],
					[studyidlastfive],
					[dateconsented],
					[consentformreceived],
					[phrn],
					[pnhs],
					[genderid],
					[dateofbirth],
					[title],
					[surname],
					[forenames],
					[address1],
					[address2],
					[address3],
					[address_town],
					[address_county],
					[address_postcode],
					[phone],
					[emailaddress],
					[countryresidence],
					[statusid],
					[losttoFUP],
					[registrationcohortid],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[tempstudyno],
					[consentversion],
					[statusdetailid],
					[consentfileaddress],
					[deathdate],
					[altStudyID234Digits],
					[altDeathDate],
					[consentedByBadbirUserID]
				FROM
					[dbo].[bbPatient]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatient_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatient table passing page index and page count parameters
-- Table Comment: Patient
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [patientid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([patientid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [patientid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatient]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[patientid], O.[firststudyno], O.[studyidlastfive], O.[dateconsented], O.[consentformreceived], O.[phrn], O.[pnhs], O.[genderid], O.[dateofbirth], O.[title], O.[surname], O.[forenames], O.[address1], O.[address2], O.[address3], O.[address_town], O.[address_county], O.[address_postcode], O.[phone], O.[emailaddress], O.[countryresidence], O.[statusid], O.[losttoFUP], O.[registrationcohortid], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[tempstudyno], O.[consentversion], O.[statusdetailid], O.[consentfileaddress], O.[deathdate], O.[altStudyID234Digits], O.[altDeathDate], O.[consentedByBadbirUserID]
				FROM
				    [dbo].[bbPatient] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[patientid] = PageIndex.[patientid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatient]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatient_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatient table
-- Table Comment: Patient
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_Insert
(

	@Patientid int    OUTPUT,

	@Firststudyno int   ,

	@Studyidlastfive int   ,

	@Dateconsented datetime   ,

	@Consentformreceived datetime   ,

	@Phrn varchar (256)  ,

	@Pnhs varchar (256)  ,

	@Genderid int   ,

	@Dateofbirth datetime   ,

	@Title varchar (256)  ,

	@Surname varchar (256)  ,

	@Forenames varchar (256)  ,

	@Address1 varchar (256)  ,

	@Address2 varchar (256)  ,

	@Address3 varchar (256)  ,

	@AddressTown varchar (256)  ,

	@AddressCounty varchar (256)  ,

	@AddressPostcode varchar (256)  ,

	@Phone varchar (256)  ,

	@Emailaddress varchar (256)  ,

	@Countryresidence varchar (256)  ,

	@Statusid int   ,

	@LosttoFup bit   ,

	@Registrationcohortid int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Tempstudyno int   ,

	@Consentversion float   ,

	@Statusdetailid int   ,

	@Consentfileaddress varchar (255)  ,

	@Deathdate date   ,

	@AltStudyId234Digits int   ,

	@AltDeathDate date   ,

	@ConsentedByBadbirUserId int   
)
AS


				
				INSERT INTO [dbo].[bbPatient]
					(
					[firststudyno]
					,[studyidlastfive]
					,[dateconsented]
					,[consentformreceived]
					,[phrn]
					,[pnhs]
					,[genderid]
					,[dateofbirth]
					,[title]
					,[surname]
					,[forenames]
					,[address1]
					,[address2]
					,[address3]
					,[address_town]
					,[address_county]
					,[address_postcode]
					,[phone]
					,[emailaddress]
					,[countryresidence]
					,[statusid]
					,[losttoFUP]
					,[registrationcohortid]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[tempstudyno]
					,[consentversion]
					,[statusdetailid]
					,[consentfileaddress]
					,[deathdate]
					,[altStudyID234Digits]
					,[altDeathDate]
					,[consentedByBadbirUserID]
					)
				VALUES
					(
					@Firststudyno
					,@Studyidlastfive
					,@Dateconsented
					,@Consentformreceived
					,@Phrn
					,@Pnhs
					,@Genderid
					,@Dateofbirth
					,@Title
					,@Surname
					,@Forenames
					,@Address1
					,@Address2
					,@Address3
					,@AddressTown
					,@AddressCounty
					,@AddressPostcode
					,@Phone
					,@Emailaddress
					,@Countryresidence
					,@Statusid
					,@LosttoFup
					,@Registrationcohortid
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@Tempstudyno
					,@Consentversion
					,@Statusdetailid
					,@Consentfileaddress
					,@Deathdate
					,@AltStudyId234Digits
					,@AltDeathDate
					,@ConsentedByBadbirUserId
					)
				-- Get the identity value
				SET @Patientid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatient_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatient table
-- Table Comment: Patient
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_Update
(

	@Patientid int   ,

	@Firststudyno int   ,

	@Studyidlastfive int   ,

	@Dateconsented datetime   ,

	@Consentformreceived datetime   ,

	@Phrn varchar (256)  ,

	@Pnhs varchar (256)  ,

	@Genderid int   ,

	@Dateofbirth datetime   ,

	@Title varchar (256)  ,

	@Surname varchar (256)  ,

	@Forenames varchar (256)  ,

	@Address1 varchar (256)  ,

	@Address2 varchar (256)  ,

	@Address3 varchar (256)  ,

	@AddressTown varchar (256)  ,

	@AddressCounty varchar (256)  ,

	@AddressPostcode varchar (256)  ,

	@Phone varchar (256)  ,

	@Emailaddress varchar (256)  ,

	@Countryresidence varchar (256)  ,

	@Statusid int   ,

	@LosttoFup bit   ,

	@Registrationcohortid int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Tempstudyno int   ,

	@Consentversion float   ,

	@Statusdetailid int   ,

	@Consentfileaddress varchar (255)  ,

	@Deathdate date   ,

	@AltStudyId234Digits int   ,

	@AltDeathDate date   ,

	@ConsentedByBadbirUserId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatient]
				SET
					[firststudyno] = @Firststudyno
					,[studyidlastfive] = @Studyidlastfive
					,[dateconsented] = @Dateconsented
					,[consentformreceived] = @Consentformreceived
					,[phrn] = @Phrn
					,[pnhs] = @Pnhs
					,[genderid] = @Genderid
					,[dateofbirth] = @Dateofbirth
					,[title] = @Title
					,[surname] = @Surname
					,[forenames] = @Forenames
					,[address1] = @Address1
					,[address2] = @Address2
					,[address3] = @Address3
					,[address_town] = @AddressTown
					,[address_county] = @AddressCounty
					,[address_postcode] = @AddressPostcode
					,[phone] = @Phone
					,[emailaddress] = @Emailaddress
					,[countryresidence] = @Countryresidence
					,[statusid] = @Statusid
					,[losttoFUP] = @LosttoFup
					,[registrationcohortid] = @Registrationcohortid
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[tempstudyno] = @Tempstudyno
					,[consentversion] = @Consentversion
					,[statusdetailid] = @Statusdetailid
					,[consentfileaddress] = @Consentfileaddress
					,[deathdate] = @Deathdate
					,[altStudyID234Digits] = @AltStudyId234Digits
					,[altDeathDate] = @AltDeathDate
					,[consentedByBadbirUserID] = @ConsentedByBadbirUserId
				WHERE
[patientid] = @Patientid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatient_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatient table
-- Table Comment: Patient
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_Delete
(

	@Patientid int   
)
AS


				DELETE FROM [dbo].[bbPatient] WITH (ROWLOCK) 
				WHERE
					[patientid] = @Patientid
					
			

GO
GRANT EXEC ON dbo.znt_bbPatient_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_GetByStatusdetailid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_GetByStatusdetailid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_GetByStatusdetailid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatient table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_GetByStatusdetailid
(

	@Statusdetailid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[patientid],
					[firststudyno],
					[studyidlastfive],
					[dateconsented],
					[consentformreceived],
					[phrn],
					[pnhs],
					[genderid],
					[dateofbirth],
					[title],
					[surname],
					[forenames],
					[address1],
					[address2],
					[address3],
					[address_town],
					[address_county],
					[address_postcode],
					[phone],
					[emailaddress],
					[countryresidence],
					[statusid],
					[losttoFUP],
					[registrationcohortid],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[tempstudyno],
					[consentversion],
					[statusdetailid],
					[consentfileaddress],
					[deathdate],
					[altStudyID234Digits],
					[altDeathDate],
					[consentedByBadbirUserID]
				FROM
					[dbo].[bbPatient]
				WHERE
					[statusdetailid] = @Statusdetailid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatient_GetByStatusdetailid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_GetByStatusid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_GetByStatusid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_GetByStatusid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatient table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_GetByStatusid
(

	@Statusid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[patientid],
					[firststudyno],
					[studyidlastfive],
					[dateconsented],
					[consentformreceived],
					[phrn],
					[pnhs],
					[genderid],
					[dateofbirth],
					[title],
					[surname],
					[forenames],
					[address1],
					[address2],
					[address3],
					[address_town],
					[address_county],
					[address_postcode],
					[phone],
					[emailaddress],
					[countryresidence],
					[statusid],
					[losttoFUP],
					[registrationcohortid],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[tempstudyno],
					[consentversion],
					[statusdetailid],
					[consentfileaddress],
					[deathdate],
					[altStudyID234Digits],
					[altDeathDate],
					[consentedByBadbirUserID]
				FROM
					[dbo].[bbPatient]
				WHERE
					[statusid] = @Statusid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatient_GetByStatusid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_GetByGenderid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_GetByGenderid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_GetByGenderid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatient table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_GetByGenderid
(

	@Genderid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[patientid],
					[firststudyno],
					[studyidlastfive],
					[dateconsented],
					[consentformreceived],
					[phrn],
					[pnhs],
					[genderid],
					[dateofbirth],
					[title],
					[surname],
					[forenames],
					[address1],
					[address2],
					[address3],
					[address_town],
					[address_county],
					[address_postcode],
					[phone],
					[emailaddress],
					[countryresidence],
					[statusid],
					[losttoFUP],
					[registrationcohortid],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[tempstudyno],
					[consentversion],
					[statusdetailid],
					[consentfileaddress],
					[deathdate],
					[altStudyID234Digits],
					[altDeathDate],
					[consentedByBadbirUserID]
				FROM
					[dbo].[bbPatient]
				WHERE
					[genderid] = @Genderid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatient_GetByGenderid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_GetByPatientid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_GetByPatientid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_GetByPatientid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatient table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_GetByPatientid
(

	@Patientid int   
)
AS


				SELECT
					[patientid],
					[firststudyno],
					[studyidlastfive],
					[dateconsented],
					[consentformreceived],
					[phrn],
					[pnhs],
					[genderid],
					[dateofbirth],
					[title],
					[surname],
					[forenames],
					[address1],
					[address2],
					[address3],
					[address_town],
					[address_county],
					[address_postcode],
					[phone],
					[emailaddress],
					[countryresidence],
					[statusid],
					[losttoFUP],
					[registrationcohortid],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[tempstudyno],
					[consentversion],
					[statusdetailid],
					[consentfileaddress],
					[deathdate],
					[altStudyID234Digits],
					[altDeathDate],
					[consentedByBadbirUserID]
				FROM
					[dbo].[bbPatient]
				WHERE
					[patientid] = @Patientid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatient_GetByPatientid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_GetByExternalStudyIdFromBbPatientExternalStudyLink procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_GetByExternalStudyIdFromBbPatientExternalStudyLink') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_GetByExternalStudyIdFromBbPatientExternalStudyLink
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records through a junction table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_GetByExternalStudyIdFromBbPatientExternalStudyLink
(

	@ExternalStudyId int   
)
AS


SELECT dbo.[bbPatient].[patientid]
       ,dbo.[bbPatient].[firststudyno]
       ,dbo.[bbPatient].[studyidlastfive]
       ,dbo.[bbPatient].[dateconsented]
       ,dbo.[bbPatient].[consentformreceived]
       ,dbo.[bbPatient].[phrn]
       ,dbo.[bbPatient].[pnhs]
       ,dbo.[bbPatient].[genderid]
       ,dbo.[bbPatient].[dateofbirth]
       ,dbo.[bbPatient].[title]
       ,dbo.[bbPatient].[surname]
       ,dbo.[bbPatient].[forenames]
       ,dbo.[bbPatient].[address1]
       ,dbo.[bbPatient].[address2]
       ,dbo.[bbPatient].[address3]
       ,dbo.[bbPatient].[address_town]
       ,dbo.[bbPatient].[address_county]
       ,dbo.[bbPatient].[address_postcode]
       ,dbo.[bbPatient].[phone]
       ,dbo.[bbPatient].[emailaddress]
       ,dbo.[bbPatient].[countryresidence]
       ,dbo.[bbPatient].[statusid]
       ,dbo.[bbPatient].[losttoFUP]
       ,dbo.[bbPatient].[registrationcohortid]
       ,dbo.[bbPatient].[createdbyid]
       ,dbo.[bbPatient].[createdbyname]
       ,dbo.[bbPatient].[createddate]
       ,dbo.[bbPatient].[lastupdatedbyid]
       ,dbo.[bbPatient].[lastupdatedbyname]
       ,dbo.[bbPatient].[lastupdateddate]
       ,dbo.[bbPatient].[tempstudyno]
       ,dbo.[bbPatient].[consentversion]
       ,dbo.[bbPatient].[statusdetailid]
       ,dbo.[bbPatient].[consentfileaddress]
       ,dbo.[bbPatient].[deathdate]
       ,dbo.[bbPatient].[altStudyID234Digits]
       ,dbo.[bbPatient].[altDeathDate]
       ,dbo.[bbPatient].[consentedByBadbirUserID]
  FROM dbo.[bbPatient]
 WHERE EXISTS (SELECT 1
                 FROM dbo.[bbPatientExternalStudyLink] 
                WHERE dbo.[bbPatientExternalStudyLink].[externalStudyID] = @ExternalStudyId
                  AND dbo.[bbPatientExternalStudyLink].[patientid] = dbo.[bbPatient].[patientid]
                  )
				SELECT @@ROWCOUNT			
				

GO
GRANT EXEC ON dbo.znt_bbPatient_GetByExternalStudyIdFromBbPatientExternalStudyLink TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatient_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatient_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatient_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatient table passing nullable parameters
-- Table Comment: Patient
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatient_Find
(

	@SearchUsingOR bit   = null ,

	@Patientid int   = null ,

	@Firststudyno int   = null ,

	@Studyidlastfive int   = null ,

	@Dateconsented datetime   = null ,

	@Consentformreceived datetime   = null ,

	@Phrn varchar (256)  = null ,

	@Pnhs varchar (256)  = null ,

	@Genderid int   = null ,

	@Dateofbirth datetime   = null ,

	@Title varchar (256)  = null ,

	@Surname varchar (256)  = null ,

	@Forenames varchar (256)  = null ,

	@Address1 varchar (256)  = null ,

	@Address2 varchar (256)  = null ,

	@Address3 varchar (256)  = null ,

	@AddressTown varchar (256)  = null ,

	@AddressCounty varchar (256)  = null ,

	@AddressPostcode varchar (256)  = null ,

	@Phone varchar (256)  = null ,

	@Emailaddress varchar (256)  = null ,

	@Countryresidence varchar (256)  = null ,

	@Statusid int   = null ,

	@LosttoFup bit   = null ,

	@Registrationcohortid int   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@Tempstudyno int   = null ,

	@Consentversion float   = null ,

	@Statusdetailid int   = null ,

	@Consentfileaddress varchar (255)  = null ,

	@Deathdate date   = null ,

	@AltStudyId234Digits int   = null ,

	@AltDeathDate date   = null ,

	@ConsentedByBadbirUserId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [patientid]
	, [firststudyno]
	, [studyidlastfive]
	, [dateconsented]
	, [consentformreceived]
	, [phrn]
	, [pnhs]
	, [genderid]
	, [dateofbirth]
	, [title]
	, [surname]
	, [forenames]
	, [address1]
	, [address2]
	, [address3]
	, [address_town]
	, [address_county]
	, [address_postcode]
	, [phone]
	, [emailaddress]
	, [countryresidence]
	, [statusid]
	, [losttoFUP]
	, [registrationcohortid]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [tempstudyno]
	, [consentversion]
	, [statusdetailid]
	, [consentfileaddress]
	, [deathdate]
	, [altStudyID234Digits]
	, [altDeathDate]
	, [consentedByBadbirUserID]
    FROM
	[dbo].[bbPatient]
    WHERE 
	 ([patientid] = @Patientid OR @Patientid IS NULL)
	AND ([firststudyno] = @Firststudyno OR @Firststudyno IS NULL)
	AND ([studyidlastfive] = @Studyidlastfive OR @Studyidlastfive IS NULL)
	AND ([dateconsented] = @Dateconsented OR @Dateconsented IS NULL)
	AND ([consentformreceived] = @Consentformreceived OR @Consentformreceived IS NULL)
	AND ([phrn] = @Phrn OR @Phrn IS NULL)
	AND ([pnhs] = @Pnhs OR @Pnhs IS NULL)
	AND ([genderid] = @Genderid OR @Genderid IS NULL)
	AND ([dateofbirth] = @Dateofbirth OR @Dateofbirth IS NULL)
	AND ([title] = @Title OR @Title IS NULL)
	AND ([surname] = @Surname OR @Surname IS NULL)
	AND ([forenames] = @Forenames OR @Forenames IS NULL)
	AND ([address1] = @Address1 OR @Address1 IS NULL)
	AND ([address2] = @Address2 OR @Address2 IS NULL)
	AND ([address3] = @Address3 OR @Address3 IS NULL)
	AND ([address_town] = @AddressTown OR @AddressTown IS NULL)
	AND ([address_county] = @AddressCounty OR @AddressCounty IS NULL)
	AND ([address_postcode] = @AddressPostcode OR @AddressPostcode IS NULL)
	AND ([phone] = @Phone OR @Phone IS NULL)
	AND ([emailaddress] = @Emailaddress OR @Emailaddress IS NULL)
	AND ([countryresidence] = @Countryresidence OR @Countryresidence IS NULL)
	AND ([statusid] = @Statusid OR @Statusid IS NULL)
	AND ([losttoFUP] = @LosttoFup OR @LosttoFup IS NULL)
	AND ([registrationcohortid] = @Registrationcohortid OR @Registrationcohortid IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([tempstudyno] = @Tempstudyno OR @Tempstudyno IS NULL)
	AND ([consentversion] = @Consentversion OR @Consentversion IS NULL)
	AND ([statusdetailid] = @Statusdetailid OR @Statusdetailid IS NULL)
	AND ([consentfileaddress] = @Consentfileaddress OR @Consentfileaddress IS NULL)
	AND ([deathdate] = @Deathdate OR @Deathdate IS NULL)
	AND ([altStudyID234Digits] = @AltStudyId234Digits OR @AltStudyId234Digits IS NULL)
	AND ([altDeathDate] = @AltDeathDate OR @AltDeathDate IS NULL)
	AND ([consentedByBadbirUserID] = @ConsentedByBadbirUserId OR @ConsentedByBadbirUserId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [patientid]
	, [firststudyno]
	, [studyidlastfive]
	, [dateconsented]
	, [consentformreceived]
	, [phrn]
	, [pnhs]
	, [genderid]
	, [dateofbirth]
	, [title]
	, [surname]
	, [forenames]
	, [address1]
	, [address2]
	, [address3]
	, [address_town]
	, [address_county]
	, [address_postcode]
	, [phone]
	, [emailaddress]
	, [countryresidence]
	, [statusid]
	, [losttoFUP]
	, [registrationcohortid]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [tempstudyno]
	, [consentversion]
	, [statusdetailid]
	, [consentfileaddress]
	, [deathdate]
	, [altStudyID234Digits]
	, [altDeathDate]
	, [consentedByBadbirUserID]
    FROM
	[dbo].[bbPatient]
    WHERE 
	 ([patientid] = @Patientid AND @Patientid is not null)
	OR ([firststudyno] = @Firststudyno AND @Firststudyno is not null)
	OR ([studyidlastfive] = @Studyidlastfive AND @Studyidlastfive is not null)
	OR ([dateconsented] = @Dateconsented AND @Dateconsented is not null)
	OR ([consentformreceived] = @Consentformreceived AND @Consentformreceived is not null)
	OR ([phrn] = @Phrn AND @Phrn is not null)
	OR ([pnhs] = @Pnhs AND @Pnhs is not null)
	OR ([genderid] = @Genderid AND @Genderid is not null)
	OR ([dateofbirth] = @Dateofbirth AND @Dateofbirth is not null)
	OR ([title] = @Title AND @Title is not null)
	OR ([surname] = @Surname AND @Surname is not null)
	OR ([forenames] = @Forenames AND @Forenames is not null)
	OR ([address1] = @Address1 AND @Address1 is not null)
	OR ([address2] = @Address2 AND @Address2 is not null)
	OR ([address3] = @Address3 AND @Address3 is not null)
	OR ([address_town] = @AddressTown AND @AddressTown is not null)
	OR ([address_county] = @AddressCounty AND @AddressCounty is not null)
	OR ([address_postcode] = @AddressPostcode AND @AddressPostcode is not null)
	OR ([phone] = @Phone AND @Phone is not null)
	OR ([emailaddress] = @Emailaddress AND @Emailaddress is not null)
	OR ([countryresidence] = @Countryresidence AND @Countryresidence is not null)
	OR ([statusid] = @Statusid AND @Statusid is not null)
	OR ([losttoFUP] = @LosttoFup AND @LosttoFup is not null)
	OR ([registrationcohortid] = @Registrationcohortid AND @Registrationcohortid is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([tempstudyno] = @Tempstudyno AND @Tempstudyno is not null)
	OR ([consentversion] = @Consentversion AND @Consentversion is not null)
	OR ([statusdetailid] = @Statusdetailid AND @Statusdetailid is not null)
	OR ([consentfileaddress] = @Consentfileaddress AND @Consentfileaddress is not null)
	OR ([deathdate] = @Deathdate AND @Deathdate is not null)
	OR ([altStudyID234Digits] = @AltStudyId234Digits AND @AltStudyId234Digits is not null)
	OR ([altDeathDate] = @AltDeathDate AND @AltDeathDate is not null)
	OR ([consentedByBadbirUserID] = @ConsentedByBadbirUserId AND @ConsentedByBadbirUserId is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatient_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentreRegionlkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentreRegionlkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentreRegionlkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbCentreRegionlkp table
-- Table Comment: Region for centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentreRegionlkp_Get_List

AS


				
				SELECT
					[centreregid],
					[centrereregion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCentreRegionlkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbCentreRegionlkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentreRegionlkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentreRegionlkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentreRegionlkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbCentreRegionlkp table passing page index and page count parameters
-- Table Comment: Region for centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentreRegionlkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [centreregid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([centreregid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [centreregid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbCentreRegionlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[centreregid], O.[centrereregion], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbCentreRegionlkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[centreregid] = PageIndex.[centreregid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbCentreRegionlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbCentreRegionlkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentreRegionlkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentreRegionlkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentreRegionlkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbCentreRegionlkp table
-- Table Comment: Region for centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentreRegionlkp_Insert
(

	@Centreregid int    OUTPUT,

	@Centrereregion varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbCentreRegionlkp]
					(
					[centrereregion]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Centrereregion
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Centreregid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbCentreRegionlkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentreRegionlkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentreRegionlkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentreRegionlkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbCentreRegionlkp table
-- Table Comment: Region for centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentreRegionlkp_Update
(

	@Centreregid int   ,

	@Centrereregion varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbCentreRegionlkp]
				SET
					[centrereregion] = @Centrereregion
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[centreregid] = @Centreregid 
				
			

GO
GRANT EXEC ON dbo.znt_bbCentreRegionlkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentreRegionlkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentreRegionlkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentreRegionlkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbCentreRegionlkp table
-- Table Comment: Region for centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentreRegionlkp_Delete
(

	@Centreregid int   
)
AS


				DELETE FROM [dbo].[bbCentreRegionlkp] WITH (ROWLOCK) 
				WHERE
					[centreregid] = @Centreregid
					
			

GO
GRANT EXEC ON dbo.znt_bbCentreRegionlkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentreRegionlkp_GetByCentreregid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentreRegionlkp_GetByCentreregid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentreRegionlkp_GetByCentreregid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCentreRegionlkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentreRegionlkp_GetByCentreregid
(

	@Centreregid int   
)
AS


				SELECT
					[centreregid],
					[centrereregion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCentreRegionlkp]
				WHERE
					[centreregid] = @Centreregid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbCentreRegionlkp_GetByCentreregid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentreRegionlkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentreRegionlkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentreRegionlkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbCentreRegionlkp table passing nullable parameters
-- Table Comment: Region for centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentreRegionlkp_Find
(

	@SearchUsingOR bit   = null ,

	@Centreregid int   = null ,

	@Centrereregion varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [centreregid]
	, [centrereregion]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCentreRegionlkp]
    WHERE 
	 ([centreregid] = @Centreregid OR @Centreregid IS NULL)
	AND ([centrereregion] = @Centrereregion OR @Centrereregion IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [centreregid]
	, [centrereregion]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCentreRegionlkp]
    WHERE 
	 ([centreregid] = @Centreregid AND @Centreregid is not null)
	OR ([centrereregion] = @Centrereregion AND @Centrereregion is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbCentreRegionlkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentrestatus_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentrestatus_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentrestatus_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbCentrestatus table
-- Table Comment: Centre status
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentrestatus_Get_List

AS


				
				SELECT
					[centrestatusid],
					[centrestatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCentrestatus]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbCentrestatus_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentrestatus_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentrestatus_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentrestatus_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbCentrestatus table passing page index and page count parameters
-- Table Comment: Centre status
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentrestatus_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [centrestatusid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([centrestatusid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [centrestatusid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbCentrestatus]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[centrestatusid], O.[centrestatus], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbCentrestatus] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[centrestatusid] = PageIndex.[centrestatusid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbCentrestatus]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbCentrestatus_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentrestatus_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentrestatus_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentrestatus_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbCentrestatus table
-- Table Comment: Centre status
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentrestatus_Insert
(

	@Centrestatusid int    OUTPUT,

	@Centrestatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbCentrestatus]
					(
					[centrestatus]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Centrestatus
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Centrestatusid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbCentrestatus_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentrestatus_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentrestatus_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentrestatus_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbCentrestatus table
-- Table Comment: Centre status
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentrestatus_Update
(

	@Centrestatusid int   ,

	@Centrestatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbCentrestatus]
				SET
					[centrestatus] = @Centrestatus
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[centrestatusid] = @Centrestatusid 
				
			

GO
GRANT EXEC ON dbo.znt_bbCentrestatus_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentrestatus_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentrestatus_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentrestatus_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbCentrestatus table
-- Table Comment: Centre status
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentrestatus_Delete
(

	@Centrestatusid int   
)
AS


				DELETE FROM [dbo].[bbCentrestatus] WITH (ROWLOCK) 
				WHERE
					[centrestatusid] = @Centrestatusid
					
			

GO
GRANT EXEC ON dbo.znt_bbCentrestatus_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentrestatus_GetByCentrestatusid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentrestatus_GetByCentrestatusid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentrestatus_GetByCentrestatusid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCentrestatus table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentrestatus_GetByCentrestatusid
(

	@Centrestatusid int   
)
AS


				SELECT
					[centrestatusid],
					[centrestatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCentrestatus]
				WHERE
					[centrestatusid] = @Centrestatusid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbCentrestatus_GetByCentrestatusid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentrestatus_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentrestatus_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentrestatus_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbCentrestatus table passing nullable parameters
-- Table Comment: Centre status
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentrestatus_Find
(

	@SearchUsingOR bit   = null ,

	@Centrestatusid int   = null ,

	@Centrestatus varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [centrestatusid]
	, [centrestatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCentrestatus]
    WHERE 
	 ([centrestatusid] = @Centrestatusid OR @Centrestatusid IS NULL)
	AND ([centrestatus] = @Centrestatus OR @Centrestatus IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [centrestatusid]
	, [centrestatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCentrestatus]
    WHERE 
	 ([centrestatusid] = @Centrestatusid AND @Centrestatusid is not null)
	OR ([centrestatus] = @Centrestatus AND @Centrestatus is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbCentrestatus_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbUKCRNregionlkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbUKCRNregionlkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbUKCRNregionlkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbUKCRNregionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbUKCRNregionlkp_Get_List

AS


				
				SELECT
					[UKCRNregid],
					[UKCRNRegion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbUKCRNregionlkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbUKCRNregionlkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbUKCRNregionlkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbUKCRNregionlkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbUKCRNregionlkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbUKCRNregionlkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbUKCRNregionlkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [UKCRNregid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([UKCRNregid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [UKCRNregid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbUKCRNregionlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[UKCRNregid], O.[UKCRNRegion], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbUKCRNregionlkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[UKCRNregid] = PageIndex.[UKCRNregid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbUKCRNregionlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbUKCRNregionlkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbUKCRNregionlkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbUKCRNregionlkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbUKCRNregionlkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbUKCRNregionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbUKCRNregionlkp_Insert
(

	@UkcrNregid int    OUTPUT,

	@UkcrnRegion varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbUKCRNregionlkp]
					(
					[UKCRNRegion]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@UkcrnRegion
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @UkcrNregid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbUKCRNregionlkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbUKCRNregionlkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbUKCRNregionlkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbUKCRNregionlkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbUKCRNregionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbUKCRNregionlkp_Update
(

	@UkcrNregid int   ,

	@UkcrnRegion varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbUKCRNregionlkp]
				SET
					[UKCRNRegion] = @UkcrnRegion
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[UKCRNregid] = @UkcrNregid 
				
			

GO
GRANT EXEC ON dbo.znt_bbUKCRNregionlkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbUKCRNregionlkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbUKCRNregionlkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbUKCRNregionlkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbUKCRNregionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbUKCRNregionlkp_Delete
(

	@UkcrNregid int   
)
AS


				DELETE FROM [dbo].[bbUKCRNregionlkp] WITH (ROWLOCK) 
				WHERE
					[UKCRNregid] = @UkcrNregid
					
			

GO
GRANT EXEC ON dbo.znt_bbUKCRNregionlkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbUKCRNregionlkp_GetByUkcrNregid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbUKCRNregionlkp_GetByUkcrNregid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbUKCRNregionlkp_GetByUkcrNregid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbUKCRNregionlkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbUKCRNregionlkp_GetByUkcrNregid
(

	@UkcrNregid int   
)
AS


				SELECT
					[UKCRNregid],
					[UKCRNRegion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbUKCRNregionlkp]
				WHERE
					[UKCRNregid] = @UkcrNregid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbUKCRNregionlkp_GetByUkcrNregid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbUKCRNregionlkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbUKCRNregionlkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbUKCRNregionlkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbUKCRNregionlkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbUKCRNregionlkp_Find
(

	@SearchUsingOR bit   = null ,

	@UkcrNregid int   = null ,

	@UkcrnRegion varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [UKCRNregid]
	, [UKCRNRegion]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbUKCRNregionlkp]
    WHERE 
	 ([UKCRNregid] = @UkcrNregid OR @UkcrNregid IS NULL)
	AND ([UKCRNRegion] = @UkcrnRegion OR @UkcrnRegion IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [UKCRNregid]
	, [UKCRNRegion]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbUKCRNregionlkp]
    WHERE 
	 ([UKCRNregid] = @UkcrNregid AND @UkcrNregid is not null)
	OR ([UKCRNRegion] = @UkcrnRegion AND @UkcrnRegion is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbUKCRNregionlkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbCentre table
-- Table Comment: Centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_Get_List

AS


				
				SELECT
					[centreid],
					[centrename],
					[address1],
					[address2],
					[address3],
					[address4],
					[address5],
					[postcode],
					[mapref],
					[centreregionid],
					[centrestatusid],
					[piid],
					[picontactdate],
					[ssisubmittedrddate],
					[lrecapproveddate],
					[rdapproveddate],
					[setupdate],
					[pat1recruiteddate],
					[financecontact],
					[contactdetails],
					[accountnumber],
					[CLRNstatus],
					[UKCRNregionid],
					[UKCRN_contact],
					[UKCRN_sitecode],
					[UKCRN_sitenumber],
					[skip_nhs_number],
					[comments],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[primarycontact],
					[picontact],
					[piidName],
					[rndcontact],
					[rndreference],
					[teamadditioncomments],
					[auditstatus],
					[auditedby],
					[auditnotes],
					[amd7_sentdate],
					[amd7_approvaldate],
					[ctamdaug13_sentdate],
					[ctamdaug13_approvaldate],
					[otherActiveUsers],
					[audit2status],
					[audit2by],
					[audit2notes],
					[audit3status],
					[audit3by],
					[audit3notes],
					[audit4status],
					[audit4by],
					[audit4notes],
					[audit1Date],
					[audit2Date],
					[audit3Date],
					[audit4Date],
					[audit1RptSentDate],
					[audit2RptSentDate],
					[audit3RptSentDate],
					[audit4RptSentDate],
					[audit1RptRcvdDate],
					[audit2RptRcvdDate],
					[audit3RptRcvdDate],
					[audit4RptRcvdDate],
					[amd8_sentdate],
					[amd8_approvaldate],
					[amd9_sentdate],
					[amd9_approvaldate],
					[altCentreID],
					[isTrainingCentre],
					[audit5status],
					[audit5by],
					[audit5notes],
					[audit5Date],
					[audit5RptSentDate],
					[audit5RptRcvdDate],
					[audit6status],
					[audit6by],
					[audit6notes],
					[audit6Date],
					[audit6RptSentDate],
					[audit6RptRcvdDate],
					[audit7status],
					[audit7by],
					[audit7notes],
					[audit7Date],
					[audit7RptSentDate],
					[audit7RptRcvdDate],
					[audit8status],
					[audit8by],
					[audit8notes],
					[audit8Date],
					[audit8RptSentDate],
					[audit8RptRcvdDate],
					[amd10_sentdate],
					[amd10_approvaldate],
					[amd11_sentdate],
					[amd11_approvaldate]
				FROM
					[dbo].[bbCentre]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbCentre_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbCentre table passing page index and page count parameters
-- Table Comment: Centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [centreid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([centreid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [centreid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbCentre]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[centreid], O.[centrename], O.[address1], O.[address2], O.[address3], O.[address4], O.[address5], O.[postcode], O.[mapref], O.[centreregionid], O.[centrestatusid], O.[piid], O.[picontactdate], O.[ssisubmittedrddate], O.[lrecapproveddate], O.[rdapproveddate], O.[setupdate], O.[pat1recruiteddate], O.[financecontact], O.[contactdetails], O.[accountnumber], O.[CLRNstatus], O.[UKCRNregionid], O.[UKCRN_contact], O.[UKCRN_sitecode], O.[UKCRN_sitenumber], O.[skip_nhs_number], O.[comments], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[primarycontact], O.[picontact], O.[piidName], O.[rndcontact], O.[rndreference], O.[teamadditioncomments], O.[auditstatus], O.[auditedby], O.[auditnotes], O.[amd7_sentdate], O.[amd7_approvaldate], O.[ctamdaug13_sentdate], O.[ctamdaug13_approvaldate], O.[otherActiveUsers], O.[audit2status], O.[audit2by], O.[audit2notes], O.[audit3status], O.[audit3by], O.[audit3notes], O.[audit4status], O.[audit4by], O.[audit4notes], O.[audit1Date], O.[audit2Date], O.[audit3Date], O.[audit4Date], O.[audit1RptSentDate], O.[audit2RptSentDate], O.[audit3RptSentDate], O.[audit4RptSentDate], O.[audit1RptRcvdDate], O.[audit2RptRcvdDate], O.[audit3RptRcvdDate], O.[audit4RptRcvdDate], O.[amd8_sentdate], O.[amd8_approvaldate], O.[amd9_sentdate], O.[amd9_approvaldate], O.[altCentreID], O.[isTrainingCentre], O.[audit5status], O.[audit5by], O.[audit5notes], O.[audit5Date], O.[audit5RptSentDate], O.[audit5RptRcvdDate], O.[audit6status], O.[audit6by], O.[audit6notes], O.[audit6Date], O.[audit6RptSentDate], O.[audit6RptRcvdDate], O.[audit7status], O.[audit7by], O.[audit7notes], O.[audit7Date], O.[audit7RptSentDate], O.[audit7RptRcvdDate], O.[audit8status], O.[audit8by], O.[audit8notes], O.[audit8Date], O.[audit8RptSentDate], O.[audit8RptRcvdDate], O.[amd10_sentdate], O.[amd10_approvaldate], O.[amd11_sentdate], O.[amd11_approvaldate]
				FROM
				    [dbo].[bbCentre] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[centreid] = PageIndex.[centreid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbCentre]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbCentre_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbCentre table
-- Table Comment: Centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_Insert
(

	@Centreid int    OUTPUT,

	@Centrename varchar (150)  ,

	@Address1 varchar (50)  ,

	@Address2 varchar (50)  ,

	@Address3 varchar (50)  ,

	@Address4 varchar (50)  ,

	@Address5 varchar (50)  ,

	@Postcode varchar (10)  ,

	@Mapref varchar (20)  ,

	@Centreregionid int   ,

	@Centrestatusid int   ,

	@Piid int   ,

	@Picontactdate datetime   ,

	@Ssisubmittedrddate datetime   ,

	@Lrecapproveddate datetime   ,

	@Rdapproveddate datetime   ,

	@Setupdate datetime   ,

	@Pat1recruiteddate datetime   ,

	@Financecontact varchar (255)  ,

	@Contactdetails varchar (255)  ,

	@Accountnumber varchar (100)  ,

	@ClrNstatus varchar (150)  ,

	@UkcrNregionid int   ,

	@UkcrnContact varchar (50)  ,

	@UkcrnSitecode varchar (100)  ,

	@UkcrnSitenumber varchar (16)  ,

	@SkipNhsNumber bit   ,

	@Comments text   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Primarycontact varchar (MAX)  ,

	@Picontact varchar (MAX)  ,

	@PiidName varchar (MAX)  ,

	@Rndcontact varchar (MAX)  ,

	@Rndreference varchar (MAX)  ,

	@Teamadditioncomments varchar (MAX)  ,

	@Auditstatus varchar (MAX)  ,

	@Auditedby varchar (MAX)  ,

	@Auditnotes varchar (MAX)  ,

	@Amd7Sentdate datetime   ,

	@Amd7Approvaldate datetime   ,

	@Ctamdaug13Sentdate datetime   ,

	@Ctamdaug13Approvaldate datetime   ,

	@OtherActiveUsers varchar (1024)  ,

	@Audit2status varchar (MAX)  ,

	@Audit2by varchar (MAX)  ,

	@Audit2notes varchar (MAX)  ,

	@Audit3status varchar (MAX)  ,

	@Audit3by varchar (MAX)  ,

	@Audit3notes varchar (MAX)  ,

	@Audit4status varchar (MAX)  ,

	@Audit4by varchar (MAX)  ,

	@Audit4notes varchar (MAX)  ,

	@Audit1Date date   ,

	@Audit2Date date   ,

	@Audit3Date date   ,

	@Audit4Date date   ,

	@Audit1RptSentDate date   ,

	@Audit2RptSentDate date   ,

	@Audit3RptSentDate date   ,

	@Audit4RptSentDate date   ,

	@Audit1RptRcvdDate date   ,

	@Audit2RptRcvdDate date   ,

	@Audit3RptRcvdDate date   ,

	@Audit4RptRcvdDate date   ,

	@Amd8Sentdate datetime   ,

	@Amd8Approvaldate datetime   ,

	@Amd9Sentdate datetime   ,

	@Amd9Approvaldate datetime   ,

	@AltCentreId char (4)  ,

	@IsTrainingCentre int   ,

	@Audit5status varchar (MAX)  ,

	@Audit5by varchar (MAX)  ,

	@Audit5notes varchar (MAX)  ,

	@Audit5Date date   ,

	@Audit5RptSentDate date   ,

	@Audit5RptRcvdDate date   ,

	@Audit6status varchar (MAX)  ,

	@Audit6by varchar (MAX)  ,

	@Audit6notes varchar (MAX)  ,

	@Audit6Date date   ,

	@Audit6RptSentDate date   ,

	@Audit6RptRcvdDate date   ,

	@Audit7status varchar (MAX)  ,

	@Audit7by varchar (MAX)  ,

	@Audit7notes varchar (MAX)  ,

	@Audit7Date date   ,

	@Audit7RptSentDate date   ,

	@Audit7RptRcvdDate date   ,

	@Audit8status varchar (MAX)  ,

	@Audit8by varchar (MAX)  ,

	@Audit8notes varchar (MAX)  ,

	@Audit8Date date   ,

	@Audit8RptSentDate date   ,

	@Audit8RptRcvdDate date   ,

	@Amd10Sentdate date   ,

	@Amd10Approvaldate date   ,

	@Amd11Sentdate date   ,

	@Amd11Approvaldate date   
)
AS


				
				INSERT INTO [dbo].[bbCentre]
					(
					[centrename]
					,[address1]
					,[address2]
					,[address3]
					,[address4]
					,[address5]
					,[postcode]
					,[mapref]
					,[centreregionid]
					,[centrestatusid]
					,[piid]
					,[picontactdate]
					,[ssisubmittedrddate]
					,[lrecapproveddate]
					,[rdapproveddate]
					,[setupdate]
					,[pat1recruiteddate]
					,[financecontact]
					,[contactdetails]
					,[accountnumber]
					,[CLRNstatus]
					,[UKCRNregionid]
					,[UKCRN_contact]
					,[UKCRN_sitecode]
					,[UKCRN_sitenumber]
					,[skip_nhs_number]
					,[comments]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[primarycontact]
					,[picontact]
					,[piidName]
					,[rndcontact]
					,[rndreference]
					,[teamadditioncomments]
					,[auditstatus]
					,[auditedby]
					,[auditnotes]
					,[amd7_sentdate]
					,[amd7_approvaldate]
					,[ctamdaug13_sentdate]
					,[ctamdaug13_approvaldate]
					,[otherActiveUsers]
					,[audit2status]
					,[audit2by]
					,[audit2notes]
					,[audit3status]
					,[audit3by]
					,[audit3notes]
					,[audit4status]
					,[audit4by]
					,[audit4notes]
					,[audit1Date]
					,[audit2Date]
					,[audit3Date]
					,[audit4Date]
					,[audit1RptSentDate]
					,[audit2RptSentDate]
					,[audit3RptSentDate]
					,[audit4RptSentDate]
					,[audit1RptRcvdDate]
					,[audit2RptRcvdDate]
					,[audit3RptRcvdDate]
					,[audit4RptRcvdDate]
					,[amd8_sentdate]
					,[amd8_approvaldate]
					,[amd9_sentdate]
					,[amd9_approvaldate]
					,[altCentreID]
					,[isTrainingCentre]
					,[audit5status]
					,[audit5by]
					,[audit5notes]
					,[audit5Date]
					,[audit5RptSentDate]
					,[audit5RptRcvdDate]
					,[audit6status]
					,[audit6by]
					,[audit6notes]
					,[audit6Date]
					,[audit6RptSentDate]
					,[audit6RptRcvdDate]
					,[audit7status]
					,[audit7by]
					,[audit7notes]
					,[audit7Date]
					,[audit7RptSentDate]
					,[audit7RptRcvdDate]
					,[audit8status]
					,[audit8by]
					,[audit8notes]
					,[audit8Date]
					,[audit8RptSentDate]
					,[audit8RptRcvdDate]
					,[amd10_sentdate]
					,[amd10_approvaldate]
					,[amd11_sentdate]
					,[amd11_approvaldate]
					)
				VALUES
					(
					@Centrename
					,@Address1
					,@Address2
					,@Address3
					,@Address4
					,@Address5
					,@Postcode
					,@Mapref
					,@Centreregionid
					,@Centrestatusid
					,@Piid
					,@Picontactdate
					,@Ssisubmittedrddate
					,@Lrecapproveddate
					,@Rdapproveddate
					,@Setupdate
					,@Pat1recruiteddate
					,@Financecontact
					,@Contactdetails
					,@Accountnumber
					,@ClrNstatus
					,@UkcrNregionid
					,@UkcrnContact
					,@UkcrnSitecode
					,@UkcrnSitenumber
					,@SkipNhsNumber
					,@Comments
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@Primarycontact
					,@Picontact
					,@PiidName
					,@Rndcontact
					,@Rndreference
					,@Teamadditioncomments
					,@Auditstatus
					,@Auditedby
					,@Auditnotes
					,@Amd7Sentdate
					,@Amd7Approvaldate
					,@Ctamdaug13Sentdate
					,@Ctamdaug13Approvaldate
					,@OtherActiveUsers
					,@Audit2status
					,@Audit2by
					,@Audit2notes
					,@Audit3status
					,@Audit3by
					,@Audit3notes
					,@Audit4status
					,@Audit4by
					,@Audit4notes
					,@Audit1Date
					,@Audit2Date
					,@Audit3Date
					,@Audit4Date
					,@Audit1RptSentDate
					,@Audit2RptSentDate
					,@Audit3RptSentDate
					,@Audit4RptSentDate
					,@Audit1RptRcvdDate
					,@Audit2RptRcvdDate
					,@Audit3RptRcvdDate
					,@Audit4RptRcvdDate
					,@Amd8Sentdate
					,@Amd8Approvaldate
					,@Amd9Sentdate
					,@Amd9Approvaldate
					,@AltCentreId
					,@IsTrainingCentre
					,@Audit5status
					,@Audit5by
					,@Audit5notes
					,@Audit5Date
					,@Audit5RptSentDate
					,@Audit5RptRcvdDate
					,@Audit6status
					,@Audit6by
					,@Audit6notes
					,@Audit6Date
					,@Audit6RptSentDate
					,@Audit6RptRcvdDate
					,@Audit7status
					,@Audit7by
					,@Audit7notes
					,@Audit7Date
					,@Audit7RptSentDate
					,@Audit7RptRcvdDate
					,@Audit8status
					,@Audit8by
					,@Audit8notes
					,@Audit8Date
					,@Audit8RptSentDate
					,@Audit8RptRcvdDate
					,@Amd10Sentdate
					,@Amd10Approvaldate
					,@Amd11Sentdate
					,@Amd11Approvaldate
					)
				-- Get the identity value
				SET @Centreid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbCentre_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbCentre table
-- Table Comment: Centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_Update
(

	@Centreid int   ,

	@Centrename varchar (150)  ,

	@Address1 varchar (50)  ,

	@Address2 varchar (50)  ,

	@Address3 varchar (50)  ,

	@Address4 varchar (50)  ,

	@Address5 varchar (50)  ,

	@Postcode varchar (10)  ,

	@Mapref varchar (20)  ,

	@Centreregionid int   ,

	@Centrestatusid int   ,

	@Piid int   ,

	@Picontactdate datetime   ,

	@Ssisubmittedrddate datetime   ,

	@Lrecapproveddate datetime   ,

	@Rdapproveddate datetime   ,

	@Setupdate datetime   ,

	@Pat1recruiteddate datetime   ,

	@Financecontact varchar (255)  ,

	@Contactdetails varchar (255)  ,

	@Accountnumber varchar (100)  ,

	@ClrNstatus varchar (150)  ,

	@UkcrNregionid int   ,

	@UkcrnContact varchar (50)  ,

	@UkcrnSitecode varchar (100)  ,

	@UkcrnSitenumber varchar (16)  ,

	@SkipNhsNumber bit   ,

	@Comments text   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Primarycontact varchar (MAX)  ,

	@Picontact varchar (MAX)  ,

	@PiidName varchar (MAX)  ,

	@Rndcontact varchar (MAX)  ,

	@Rndreference varchar (MAX)  ,

	@Teamadditioncomments varchar (MAX)  ,

	@Auditstatus varchar (MAX)  ,

	@Auditedby varchar (MAX)  ,

	@Auditnotes varchar (MAX)  ,

	@Amd7Sentdate datetime   ,

	@Amd7Approvaldate datetime   ,

	@Ctamdaug13Sentdate datetime   ,

	@Ctamdaug13Approvaldate datetime   ,

	@OtherActiveUsers varchar (1024)  ,

	@Audit2status varchar (MAX)  ,

	@Audit2by varchar (MAX)  ,

	@Audit2notes varchar (MAX)  ,

	@Audit3status varchar (MAX)  ,

	@Audit3by varchar (MAX)  ,

	@Audit3notes varchar (MAX)  ,

	@Audit4status varchar (MAX)  ,

	@Audit4by varchar (MAX)  ,

	@Audit4notes varchar (MAX)  ,

	@Audit1Date date   ,

	@Audit2Date date   ,

	@Audit3Date date   ,

	@Audit4Date date   ,

	@Audit1RptSentDate date   ,

	@Audit2RptSentDate date   ,

	@Audit3RptSentDate date   ,

	@Audit4RptSentDate date   ,

	@Audit1RptRcvdDate date   ,

	@Audit2RptRcvdDate date   ,

	@Audit3RptRcvdDate date   ,

	@Audit4RptRcvdDate date   ,

	@Amd8Sentdate datetime   ,

	@Amd8Approvaldate datetime   ,

	@Amd9Sentdate datetime   ,

	@Amd9Approvaldate datetime   ,

	@AltCentreId char (4)  ,

	@IsTrainingCentre int   ,

	@Audit5status varchar (MAX)  ,

	@Audit5by varchar (MAX)  ,

	@Audit5notes varchar (MAX)  ,

	@Audit5Date date   ,

	@Audit5RptSentDate date   ,

	@Audit5RptRcvdDate date   ,

	@Audit6status varchar (MAX)  ,

	@Audit6by varchar (MAX)  ,

	@Audit6notes varchar (MAX)  ,

	@Audit6Date date   ,

	@Audit6RptSentDate date   ,

	@Audit6RptRcvdDate date   ,

	@Audit7status varchar (MAX)  ,

	@Audit7by varchar (MAX)  ,

	@Audit7notes varchar (MAX)  ,

	@Audit7Date date   ,

	@Audit7RptSentDate date   ,

	@Audit7RptRcvdDate date   ,

	@Audit8status varchar (MAX)  ,

	@Audit8by varchar (MAX)  ,

	@Audit8notes varchar (MAX)  ,

	@Audit8Date date   ,

	@Audit8RptSentDate date   ,

	@Audit8RptRcvdDate date   ,

	@Amd10Sentdate date   ,

	@Amd10Approvaldate date   ,

	@Amd11Sentdate date   ,

	@Amd11Approvaldate date   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbCentre]
				SET
					[centrename] = @Centrename
					,[address1] = @Address1
					,[address2] = @Address2
					,[address3] = @Address3
					,[address4] = @Address4
					,[address5] = @Address5
					,[postcode] = @Postcode
					,[mapref] = @Mapref
					,[centreregionid] = @Centreregionid
					,[centrestatusid] = @Centrestatusid
					,[piid] = @Piid
					,[picontactdate] = @Picontactdate
					,[ssisubmittedrddate] = @Ssisubmittedrddate
					,[lrecapproveddate] = @Lrecapproveddate
					,[rdapproveddate] = @Rdapproveddate
					,[setupdate] = @Setupdate
					,[pat1recruiteddate] = @Pat1recruiteddate
					,[financecontact] = @Financecontact
					,[contactdetails] = @Contactdetails
					,[accountnumber] = @Accountnumber
					,[CLRNstatus] = @ClrNstatus
					,[UKCRNregionid] = @UkcrNregionid
					,[UKCRN_contact] = @UkcrnContact
					,[UKCRN_sitecode] = @UkcrnSitecode
					,[UKCRN_sitenumber] = @UkcrnSitenumber
					,[skip_nhs_number] = @SkipNhsNumber
					,[comments] = @Comments
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[primarycontact] = @Primarycontact
					,[picontact] = @Picontact
					,[piidName] = @PiidName
					,[rndcontact] = @Rndcontact
					,[rndreference] = @Rndreference
					,[teamadditioncomments] = @Teamadditioncomments
					,[auditstatus] = @Auditstatus
					,[auditedby] = @Auditedby
					,[auditnotes] = @Auditnotes
					,[amd7_sentdate] = @Amd7Sentdate
					,[amd7_approvaldate] = @Amd7Approvaldate
					,[ctamdaug13_sentdate] = @Ctamdaug13Sentdate
					,[ctamdaug13_approvaldate] = @Ctamdaug13Approvaldate
					,[otherActiveUsers] = @OtherActiveUsers
					,[audit2status] = @Audit2status
					,[audit2by] = @Audit2by
					,[audit2notes] = @Audit2notes
					,[audit3status] = @Audit3status
					,[audit3by] = @Audit3by
					,[audit3notes] = @Audit3notes
					,[audit4status] = @Audit4status
					,[audit4by] = @Audit4by
					,[audit4notes] = @Audit4notes
					,[audit1Date] = @Audit1Date
					,[audit2Date] = @Audit2Date
					,[audit3Date] = @Audit3Date
					,[audit4Date] = @Audit4Date
					,[audit1RptSentDate] = @Audit1RptSentDate
					,[audit2RptSentDate] = @Audit2RptSentDate
					,[audit3RptSentDate] = @Audit3RptSentDate
					,[audit4RptSentDate] = @Audit4RptSentDate
					,[audit1RptRcvdDate] = @Audit1RptRcvdDate
					,[audit2RptRcvdDate] = @Audit2RptRcvdDate
					,[audit3RptRcvdDate] = @Audit3RptRcvdDate
					,[audit4RptRcvdDate] = @Audit4RptRcvdDate
					,[amd8_sentdate] = @Amd8Sentdate
					,[amd8_approvaldate] = @Amd8Approvaldate
					,[amd9_sentdate] = @Amd9Sentdate
					,[amd9_approvaldate] = @Amd9Approvaldate
					,[altCentreID] = @AltCentreId
					,[isTrainingCentre] = @IsTrainingCentre
					,[audit5status] = @Audit5status
					,[audit5by] = @Audit5by
					,[audit5notes] = @Audit5notes
					,[audit5Date] = @Audit5Date
					,[audit5RptSentDate] = @Audit5RptSentDate
					,[audit5RptRcvdDate] = @Audit5RptRcvdDate
					,[audit6status] = @Audit6status
					,[audit6by] = @Audit6by
					,[audit6notes] = @Audit6notes
					,[audit6Date] = @Audit6Date
					,[audit6RptSentDate] = @Audit6RptSentDate
					,[audit6RptRcvdDate] = @Audit6RptRcvdDate
					,[audit7status] = @Audit7status
					,[audit7by] = @Audit7by
					,[audit7notes] = @Audit7notes
					,[audit7Date] = @Audit7Date
					,[audit7RptSentDate] = @Audit7RptSentDate
					,[audit7RptRcvdDate] = @Audit7RptRcvdDate
					,[audit8status] = @Audit8status
					,[audit8by] = @Audit8by
					,[audit8notes] = @Audit8notes
					,[audit8Date] = @Audit8Date
					,[audit8RptSentDate] = @Audit8RptSentDate
					,[audit8RptRcvdDate] = @Audit8RptRcvdDate
					,[amd10_sentdate] = @Amd10Sentdate
					,[amd10_approvaldate] = @Amd10Approvaldate
					,[amd11_sentdate] = @Amd11Sentdate
					,[amd11_approvaldate] = @Amd11Approvaldate
				WHERE
[centreid] = @Centreid 
				
			

GO
GRANT EXEC ON dbo.znt_bbCentre_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbCentre table
-- Table Comment: Centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_Delete
(

	@Centreid int   
)
AS


				DELETE FROM [dbo].[bbCentre] WITH (ROWLOCK) 
				WHERE
					[centreid] = @Centreid
					
			

GO
GRANT EXEC ON dbo.znt_bbCentre_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetByCentreregionid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetByCentreregionid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetByCentreregionid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCentre table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetByCentreregionid
(

	@Centreregionid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[centreid],
					[centrename],
					[address1],
					[address2],
					[address3],
					[address4],
					[address5],
					[postcode],
					[mapref],
					[centreregionid],
					[centrestatusid],
					[piid],
					[picontactdate],
					[ssisubmittedrddate],
					[lrecapproveddate],
					[rdapproveddate],
					[setupdate],
					[pat1recruiteddate],
					[financecontact],
					[contactdetails],
					[accountnumber],
					[CLRNstatus],
					[UKCRNregionid],
					[UKCRN_contact],
					[UKCRN_sitecode],
					[UKCRN_sitenumber],
					[skip_nhs_number],
					[comments],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[primarycontact],
					[picontact],
					[piidName],
					[rndcontact],
					[rndreference],
					[teamadditioncomments],
					[auditstatus],
					[auditedby],
					[auditnotes],
					[amd7_sentdate],
					[amd7_approvaldate],
					[ctamdaug13_sentdate],
					[ctamdaug13_approvaldate],
					[otherActiveUsers],
					[audit2status],
					[audit2by],
					[audit2notes],
					[audit3status],
					[audit3by],
					[audit3notes],
					[audit4status],
					[audit4by],
					[audit4notes],
					[audit1Date],
					[audit2Date],
					[audit3Date],
					[audit4Date],
					[audit1RptSentDate],
					[audit2RptSentDate],
					[audit3RptSentDate],
					[audit4RptSentDate],
					[audit1RptRcvdDate],
					[audit2RptRcvdDate],
					[audit3RptRcvdDate],
					[audit4RptRcvdDate],
					[amd8_sentdate],
					[amd8_approvaldate],
					[amd9_sentdate],
					[amd9_approvaldate],
					[altCentreID],
					[isTrainingCentre],
					[audit5status],
					[audit5by],
					[audit5notes],
					[audit5Date],
					[audit5RptSentDate],
					[audit5RptRcvdDate],
					[audit6status],
					[audit6by],
					[audit6notes],
					[audit6Date],
					[audit6RptSentDate],
					[audit6RptRcvdDate],
					[audit7status],
					[audit7by],
					[audit7notes],
					[audit7Date],
					[audit7RptSentDate],
					[audit7RptRcvdDate],
					[audit8status],
					[audit8by],
					[audit8notes],
					[audit8Date],
					[audit8RptSentDate],
					[audit8RptRcvdDate],
					[amd10_sentdate],
					[amd10_approvaldate],
					[amd11_sentdate],
					[amd11_approvaldate]
				FROM
					[dbo].[bbCentre]
				WHERE
					[centreregionid] = @Centreregionid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbCentre_GetByCentreregionid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetByCentrestatusid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetByCentrestatusid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetByCentrestatusid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCentre table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetByCentrestatusid
(

	@Centrestatusid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[centreid],
					[centrename],
					[address1],
					[address2],
					[address3],
					[address4],
					[address5],
					[postcode],
					[mapref],
					[centreregionid],
					[centrestatusid],
					[piid],
					[picontactdate],
					[ssisubmittedrddate],
					[lrecapproveddate],
					[rdapproveddate],
					[setupdate],
					[pat1recruiteddate],
					[financecontact],
					[contactdetails],
					[accountnumber],
					[CLRNstatus],
					[UKCRNregionid],
					[UKCRN_contact],
					[UKCRN_sitecode],
					[UKCRN_sitenumber],
					[skip_nhs_number],
					[comments],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[primarycontact],
					[picontact],
					[piidName],
					[rndcontact],
					[rndreference],
					[teamadditioncomments],
					[auditstatus],
					[auditedby],
					[auditnotes],
					[amd7_sentdate],
					[amd7_approvaldate],
					[ctamdaug13_sentdate],
					[ctamdaug13_approvaldate],
					[otherActiveUsers],
					[audit2status],
					[audit2by],
					[audit2notes],
					[audit3status],
					[audit3by],
					[audit3notes],
					[audit4status],
					[audit4by],
					[audit4notes],
					[audit1Date],
					[audit2Date],
					[audit3Date],
					[audit4Date],
					[audit1RptSentDate],
					[audit2RptSentDate],
					[audit3RptSentDate],
					[audit4RptSentDate],
					[audit1RptRcvdDate],
					[audit2RptRcvdDate],
					[audit3RptRcvdDate],
					[audit4RptRcvdDate],
					[amd8_sentdate],
					[amd8_approvaldate],
					[amd9_sentdate],
					[amd9_approvaldate],
					[altCentreID],
					[isTrainingCentre],
					[audit5status],
					[audit5by],
					[audit5notes],
					[audit5Date],
					[audit5RptSentDate],
					[audit5RptRcvdDate],
					[audit6status],
					[audit6by],
					[audit6notes],
					[audit6Date],
					[audit6RptSentDate],
					[audit6RptRcvdDate],
					[audit7status],
					[audit7by],
					[audit7notes],
					[audit7Date],
					[audit7RptSentDate],
					[audit7RptRcvdDate],
					[audit8status],
					[audit8by],
					[audit8notes],
					[audit8Date],
					[audit8RptSentDate],
					[audit8RptRcvdDate],
					[amd10_sentdate],
					[amd10_approvaldate],
					[amd11_sentdate],
					[amd11_approvaldate]
				FROM
					[dbo].[bbCentre]
				WHERE
					[centrestatusid] = @Centrestatusid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbCentre_GetByCentrestatusid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetByUkcrNregionid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetByUkcrNregionid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetByUkcrNregionid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCentre table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetByUkcrNregionid
(

	@UkcrNregionid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[centreid],
					[centrename],
					[address1],
					[address2],
					[address3],
					[address4],
					[address5],
					[postcode],
					[mapref],
					[centreregionid],
					[centrestatusid],
					[piid],
					[picontactdate],
					[ssisubmittedrddate],
					[lrecapproveddate],
					[rdapproveddate],
					[setupdate],
					[pat1recruiteddate],
					[financecontact],
					[contactdetails],
					[accountnumber],
					[CLRNstatus],
					[UKCRNregionid],
					[UKCRN_contact],
					[UKCRN_sitecode],
					[UKCRN_sitenumber],
					[skip_nhs_number],
					[comments],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[primarycontact],
					[picontact],
					[piidName],
					[rndcontact],
					[rndreference],
					[teamadditioncomments],
					[auditstatus],
					[auditedby],
					[auditnotes],
					[amd7_sentdate],
					[amd7_approvaldate],
					[ctamdaug13_sentdate],
					[ctamdaug13_approvaldate],
					[otherActiveUsers],
					[audit2status],
					[audit2by],
					[audit2notes],
					[audit3status],
					[audit3by],
					[audit3notes],
					[audit4status],
					[audit4by],
					[audit4notes],
					[audit1Date],
					[audit2Date],
					[audit3Date],
					[audit4Date],
					[audit1RptSentDate],
					[audit2RptSentDate],
					[audit3RptSentDate],
					[audit4RptSentDate],
					[audit1RptRcvdDate],
					[audit2RptRcvdDate],
					[audit3RptRcvdDate],
					[audit4RptRcvdDate],
					[amd8_sentdate],
					[amd8_approvaldate],
					[amd9_sentdate],
					[amd9_approvaldate],
					[altCentreID],
					[isTrainingCentre],
					[audit5status],
					[audit5by],
					[audit5notes],
					[audit5Date],
					[audit5RptSentDate],
					[audit5RptRcvdDate],
					[audit6status],
					[audit6by],
					[audit6notes],
					[audit6Date],
					[audit6RptSentDate],
					[audit6RptRcvdDate],
					[audit7status],
					[audit7by],
					[audit7notes],
					[audit7Date],
					[audit7RptSentDate],
					[audit7RptRcvdDate],
					[audit8status],
					[audit8by],
					[audit8notes],
					[audit8Date],
					[audit8RptSentDate],
					[audit8RptRcvdDate],
					[amd10_sentdate],
					[amd10_approvaldate],
					[amd11_sentdate],
					[amd11_approvaldate]
				FROM
					[dbo].[bbCentre]
				WHERE
					[UKCRNregionid] = @UkcrNregionid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbCentre_GetByUkcrNregionid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetByCentreid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetByCentreid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetByCentreid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCentre table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetByCentreid
(

	@Centreid int   
)
AS


				SELECT
					[centreid],
					[centrename],
					[address1],
					[address2],
					[address3],
					[address4],
					[address5],
					[postcode],
					[mapref],
					[centreregionid],
					[centrestatusid],
					[piid],
					[picontactdate],
					[ssisubmittedrddate],
					[lrecapproveddate],
					[rdapproveddate],
					[setupdate],
					[pat1recruiteddate],
					[financecontact],
					[contactdetails],
					[accountnumber],
					[CLRNstatus],
					[UKCRNregionid],
					[UKCRN_contact],
					[UKCRN_sitecode],
					[UKCRN_sitenumber],
					[skip_nhs_number],
					[comments],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[primarycontact],
					[picontact],
					[piidName],
					[rndcontact],
					[rndreference],
					[teamadditioncomments],
					[auditstatus],
					[auditedby],
					[auditnotes],
					[amd7_sentdate],
					[amd7_approvaldate],
					[ctamdaug13_sentdate],
					[ctamdaug13_approvaldate],
					[otherActiveUsers],
					[audit2status],
					[audit2by],
					[audit2notes],
					[audit3status],
					[audit3by],
					[audit3notes],
					[audit4status],
					[audit4by],
					[audit4notes],
					[audit1Date],
					[audit2Date],
					[audit3Date],
					[audit4Date],
					[audit1RptSentDate],
					[audit2RptSentDate],
					[audit3RptSentDate],
					[audit4RptSentDate],
					[audit1RptRcvdDate],
					[audit2RptRcvdDate],
					[audit3RptRcvdDate],
					[audit4RptRcvdDate],
					[amd8_sentdate],
					[amd8_approvaldate],
					[amd9_sentdate],
					[amd9_approvaldate],
					[altCentreID],
					[isTrainingCentre],
					[audit5status],
					[audit5by],
					[audit5notes],
					[audit5Date],
					[audit5RptSentDate],
					[audit5RptRcvdDate],
					[audit6status],
					[audit6by],
					[audit6notes],
					[audit6Date],
					[audit6RptSentDate],
					[audit6RptRcvdDate],
					[audit7status],
					[audit7by],
					[audit7notes],
					[audit7Date],
					[audit7RptSentDate],
					[audit7RptRcvdDate],
					[audit8status],
					[audit8by],
					[audit8notes],
					[audit8Date],
					[audit8RptSentDate],
					[audit8RptRcvdDate],
					[amd10_sentdate],
					[amd10_approvaldate],
					[amd11_sentdate],
					[amd11_approvaldate]
				FROM
					[dbo].[bbCentre]
				WHERE
					[centreid] = @Centreid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbCentre_GetByCentreid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetByAltCentreId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetByAltCentreId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetByAltCentreId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCentre table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetByAltCentreId
(

	@AltCentreId char (4)  
)
AS


				SELECT
					[centreid],
					[centrename],
					[address1],
					[address2],
					[address3],
					[address4],
					[address5],
					[postcode],
					[mapref],
					[centreregionid],
					[centrestatusid],
					[piid],
					[picontactdate],
					[ssisubmittedrddate],
					[lrecapproveddate],
					[rdapproveddate],
					[setupdate],
					[pat1recruiteddate],
					[financecontact],
					[contactdetails],
					[accountnumber],
					[CLRNstatus],
					[UKCRNregionid],
					[UKCRN_contact],
					[UKCRN_sitecode],
					[UKCRN_sitenumber],
					[skip_nhs_number],
					[comments],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[primarycontact],
					[picontact],
					[piidName],
					[rndcontact],
					[rndreference],
					[teamadditioncomments],
					[auditstatus],
					[auditedby],
					[auditnotes],
					[amd7_sentdate],
					[amd7_approvaldate],
					[ctamdaug13_sentdate],
					[ctamdaug13_approvaldate],
					[otherActiveUsers],
					[audit2status],
					[audit2by],
					[audit2notes],
					[audit3status],
					[audit3by],
					[audit3notes],
					[audit4status],
					[audit4by],
					[audit4notes],
					[audit1Date],
					[audit2Date],
					[audit3Date],
					[audit4Date],
					[audit1RptSentDate],
					[audit2RptSentDate],
					[audit3RptSentDate],
					[audit4RptSentDate],
					[audit1RptRcvdDate],
					[audit2RptRcvdDate],
					[audit3RptRcvdDate],
					[audit4RptRcvdDate],
					[amd8_sentdate],
					[amd8_approvaldate],
					[amd9_sentdate],
					[amd9_approvaldate],
					[altCentreID],
					[isTrainingCentre],
					[audit5status],
					[audit5by],
					[audit5notes],
					[audit5Date],
					[audit5RptSentDate],
					[audit5RptRcvdDate],
					[audit6status],
					[audit6by],
					[audit6notes],
					[audit6Date],
					[audit6RptSentDate],
					[audit6RptRcvdDate],
					[audit7status],
					[audit7by],
					[audit7notes],
					[audit7Date],
					[audit7RptSentDate],
					[audit7RptRcvdDate],
					[audit8status],
					[audit8by],
					[audit8notes],
					[audit8Date],
					[audit8RptSentDate],
					[audit8RptRcvdDate],
					[amd10_sentdate],
					[amd10_approvaldate],
					[amd11_sentdate],
					[amd11_approvaldate]
				FROM
					[dbo].[bbCentre]
				WHERE
					[altCentreID] = @AltCentreId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbCentre_GetByAltCentreId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetByUseridFromBbAdditionalUserAndCentre procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetByUseridFromBbAdditionalUserAndCentre') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetByUseridFromBbAdditionalUserAndCentre
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records through a junction table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetByUseridFromBbAdditionalUserAndCentre
(

	@Userid uniqueidentifier   
)
AS


SELECT dbo.[bbCentre].[centreid]
       ,dbo.[bbCentre].[centrename]
       ,dbo.[bbCentre].[address1]
       ,dbo.[bbCentre].[address2]
       ,dbo.[bbCentre].[address3]
       ,dbo.[bbCentre].[address4]
       ,dbo.[bbCentre].[address5]
       ,dbo.[bbCentre].[postcode]
       ,dbo.[bbCentre].[mapref]
       ,dbo.[bbCentre].[centreregionid]
       ,dbo.[bbCentre].[centrestatusid]
       ,dbo.[bbCentre].[piid]
       ,dbo.[bbCentre].[picontactdate]
       ,dbo.[bbCentre].[ssisubmittedrddate]
       ,dbo.[bbCentre].[lrecapproveddate]
       ,dbo.[bbCentre].[rdapproveddate]
       ,dbo.[bbCentre].[setupdate]
       ,dbo.[bbCentre].[pat1recruiteddate]
       ,dbo.[bbCentre].[financecontact]
       ,dbo.[bbCentre].[contactdetails]
       ,dbo.[bbCentre].[accountnumber]
       ,dbo.[bbCentre].[CLRNstatus]
       ,dbo.[bbCentre].[UKCRNregionid]
       ,dbo.[bbCentre].[UKCRN_contact]
       ,dbo.[bbCentre].[UKCRN_sitecode]
       ,dbo.[bbCentre].[UKCRN_sitenumber]
       ,dbo.[bbCentre].[skip_nhs_number]
       ,dbo.[bbCentre].[comments]
       ,dbo.[bbCentre].[createdbyid]
       ,dbo.[bbCentre].[createdbyname]
       ,dbo.[bbCentre].[createddate]
       ,dbo.[bbCentre].[lastupdatedbyid]
       ,dbo.[bbCentre].[lastupdatedbyname]
       ,dbo.[bbCentre].[lastupdateddate]
       ,dbo.[bbCentre].[primarycontact]
       ,dbo.[bbCentre].[picontact]
       ,dbo.[bbCentre].[piidName]
       ,dbo.[bbCentre].[rndcontact]
       ,dbo.[bbCentre].[rndreference]
       ,dbo.[bbCentre].[teamadditioncomments]
       ,dbo.[bbCentre].[auditstatus]
       ,dbo.[bbCentre].[auditedby]
       ,dbo.[bbCentre].[auditnotes]
       ,dbo.[bbCentre].[amd7_sentdate]
       ,dbo.[bbCentre].[amd7_approvaldate]
       ,dbo.[bbCentre].[ctamdaug13_sentdate]
       ,dbo.[bbCentre].[ctamdaug13_approvaldate]
       ,dbo.[bbCentre].[otherActiveUsers]
       ,dbo.[bbCentre].[audit2status]
       ,dbo.[bbCentre].[audit2by]
       ,dbo.[bbCentre].[audit2notes]
       ,dbo.[bbCentre].[audit3status]
       ,dbo.[bbCentre].[audit3by]
       ,dbo.[bbCentre].[audit3notes]
       ,dbo.[bbCentre].[audit4status]
       ,dbo.[bbCentre].[audit4by]
       ,dbo.[bbCentre].[audit4notes]
       ,dbo.[bbCentre].[audit1Date]
       ,dbo.[bbCentre].[audit2Date]
       ,dbo.[bbCentre].[audit3Date]
       ,dbo.[bbCentre].[audit4Date]
       ,dbo.[bbCentre].[audit1RptSentDate]
       ,dbo.[bbCentre].[audit2RptSentDate]
       ,dbo.[bbCentre].[audit3RptSentDate]
       ,dbo.[bbCentre].[audit4RptSentDate]
       ,dbo.[bbCentre].[audit1RptRcvdDate]
       ,dbo.[bbCentre].[audit2RptRcvdDate]
       ,dbo.[bbCentre].[audit3RptRcvdDate]
       ,dbo.[bbCentre].[audit4RptRcvdDate]
       ,dbo.[bbCentre].[amd8_sentdate]
       ,dbo.[bbCentre].[amd8_approvaldate]
       ,dbo.[bbCentre].[amd9_sentdate]
       ,dbo.[bbCentre].[amd9_approvaldate]
       ,dbo.[bbCentre].[altCentreID]
       ,dbo.[bbCentre].[isTrainingCentre]
       ,dbo.[bbCentre].[audit5status]
       ,dbo.[bbCentre].[audit5by]
       ,dbo.[bbCentre].[audit5notes]
       ,dbo.[bbCentre].[audit5Date]
       ,dbo.[bbCentre].[audit5RptSentDate]
       ,dbo.[bbCentre].[audit5RptRcvdDate]
       ,dbo.[bbCentre].[audit6status]
       ,dbo.[bbCentre].[audit6by]
       ,dbo.[bbCentre].[audit6notes]
       ,dbo.[bbCentre].[audit6Date]
       ,dbo.[bbCentre].[audit6RptSentDate]
       ,dbo.[bbCentre].[audit6RptRcvdDate]
       ,dbo.[bbCentre].[audit7status]
       ,dbo.[bbCentre].[audit7by]
       ,dbo.[bbCentre].[audit7notes]
       ,dbo.[bbCentre].[audit7Date]
       ,dbo.[bbCentre].[audit7RptSentDate]
       ,dbo.[bbCentre].[audit7RptRcvdDate]
       ,dbo.[bbCentre].[audit8status]
       ,dbo.[bbCentre].[audit8by]
       ,dbo.[bbCentre].[audit8notes]
       ,dbo.[bbCentre].[audit8Date]
       ,dbo.[bbCentre].[audit8RptSentDate]
       ,dbo.[bbCentre].[audit8RptRcvdDate]
       ,dbo.[bbCentre].[amd10_sentdate]
       ,dbo.[bbCentre].[amd10_approvaldate]
       ,dbo.[bbCentre].[amd11_sentdate]
       ,dbo.[bbCentre].[amd11_approvaldate]
  FROM dbo.[bbCentre]
 WHERE EXISTS (SELECT 1
                 FROM dbo.[bbAdditionalUserAndCentre] 
                WHERE dbo.[bbAdditionalUserAndCentre].[userid] = @Userid
                  AND dbo.[bbAdditionalUserAndCentre].[Centreid] = dbo.[bbCentre].[centreid]
                  )
				SELECT @@ROWCOUNT			
				

GO
GRANT EXEC ON dbo.znt_bbCentre_GetByUseridFromBbAdditionalUserAndCentre TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_GetByExternalStudyIdFromBbCentreExternalStudyLink procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_GetByExternalStudyIdFromBbCentreExternalStudyLink') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_GetByExternalStudyIdFromBbCentreExternalStudyLink
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records through a junction table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_GetByExternalStudyIdFromBbCentreExternalStudyLink
(

	@ExternalStudyId int   
)
AS


SELECT dbo.[bbCentre].[centreid]
       ,dbo.[bbCentre].[centrename]
       ,dbo.[bbCentre].[address1]
       ,dbo.[bbCentre].[address2]
       ,dbo.[bbCentre].[address3]
       ,dbo.[bbCentre].[address4]
       ,dbo.[bbCentre].[address5]
       ,dbo.[bbCentre].[postcode]
       ,dbo.[bbCentre].[mapref]
       ,dbo.[bbCentre].[centreregionid]
       ,dbo.[bbCentre].[centrestatusid]
       ,dbo.[bbCentre].[piid]
       ,dbo.[bbCentre].[picontactdate]
       ,dbo.[bbCentre].[ssisubmittedrddate]
       ,dbo.[bbCentre].[lrecapproveddate]
       ,dbo.[bbCentre].[rdapproveddate]
       ,dbo.[bbCentre].[setupdate]
       ,dbo.[bbCentre].[pat1recruiteddate]
       ,dbo.[bbCentre].[financecontact]
       ,dbo.[bbCentre].[contactdetails]
       ,dbo.[bbCentre].[accountnumber]
       ,dbo.[bbCentre].[CLRNstatus]
       ,dbo.[bbCentre].[UKCRNregionid]
       ,dbo.[bbCentre].[UKCRN_contact]
       ,dbo.[bbCentre].[UKCRN_sitecode]
       ,dbo.[bbCentre].[UKCRN_sitenumber]
       ,dbo.[bbCentre].[skip_nhs_number]
       ,dbo.[bbCentre].[comments]
       ,dbo.[bbCentre].[createdbyid]
       ,dbo.[bbCentre].[createdbyname]
       ,dbo.[bbCentre].[createddate]
       ,dbo.[bbCentre].[lastupdatedbyid]
       ,dbo.[bbCentre].[lastupdatedbyname]
       ,dbo.[bbCentre].[lastupdateddate]
       ,dbo.[bbCentre].[primarycontact]
       ,dbo.[bbCentre].[picontact]
       ,dbo.[bbCentre].[piidName]
       ,dbo.[bbCentre].[rndcontact]
       ,dbo.[bbCentre].[rndreference]
       ,dbo.[bbCentre].[teamadditioncomments]
       ,dbo.[bbCentre].[auditstatus]
       ,dbo.[bbCentre].[auditedby]
       ,dbo.[bbCentre].[auditnotes]
       ,dbo.[bbCentre].[amd7_sentdate]
       ,dbo.[bbCentre].[amd7_approvaldate]
       ,dbo.[bbCentre].[ctamdaug13_sentdate]
       ,dbo.[bbCentre].[ctamdaug13_approvaldate]
       ,dbo.[bbCentre].[otherActiveUsers]
       ,dbo.[bbCentre].[audit2status]
       ,dbo.[bbCentre].[audit2by]
       ,dbo.[bbCentre].[audit2notes]
       ,dbo.[bbCentre].[audit3status]
       ,dbo.[bbCentre].[audit3by]
       ,dbo.[bbCentre].[audit3notes]
       ,dbo.[bbCentre].[audit4status]
       ,dbo.[bbCentre].[audit4by]
       ,dbo.[bbCentre].[audit4notes]
       ,dbo.[bbCentre].[audit1Date]
       ,dbo.[bbCentre].[audit2Date]
       ,dbo.[bbCentre].[audit3Date]
       ,dbo.[bbCentre].[audit4Date]
       ,dbo.[bbCentre].[audit1RptSentDate]
       ,dbo.[bbCentre].[audit2RptSentDate]
       ,dbo.[bbCentre].[audit3RptSentDate]
       ,dbo.[bbCentre].[audit4RptSentDate]
       ,dbo.[bbCentre].[audit1RptRcvdDate]
       ,dbo.[bbCentre].[audit2RptRcvdDate]
       ,dbo.[bbCentre].[audit3RptRcvdDate]
       ,dbo.[bbCentre].[audit4RptRcvdDate]
       ,dbo.[bbCentre].[amd8_sentdate]
       ,dbo.[bbCentre].[amd8_approvaldate]
       ,dbo.[bbCentre].[amd9_sentdate]
       ,dbo.[bbCentre].[amd9_approvaldate]
       ,dbo.[bbCentre].[altCentreID]
       ,dbo.[bbCentre].[isTrainingCentre]
       ,dbo.[bbCentre].[audit5status]
       ,dbo.[bbCentre].[audit5by]
       ,dbo.[bbCentre].[audit5notes]
       ,dbo.[bbCentre].[audit5Date]
       ,dbo.[bbCentre].[audit5RptSentDate]
       ,dbo.[bbCentre].[audit5RptRcvdDate]
       ,dbo.[bbCentre].[audit6status]
       ,dbo.[bbCentre].[audit6by]
       ,dbo.[bbCentre].[audit6notes]
       ,dbo.[bbCentre].[audit6Date]
       ,dbo.[bbCentre].[audit6RptSentDate]
       ,dbo.[bbCentre].[audit6RptRcvdDate]
       ,dbo.[bbCentre].[audit7status]
       ,dbo.[bbCentre].[audit7by]
       ,dbo.[bbCentre].[audit7notes]
       ,dbo.[bbCentre].[audit7Date]
       ,dbo.[bbCentre].[audit7RptSentDate]
       ,dbo.[bbCentre].[audit7RptRcvdDate]
       ,dbo.[bbCentre].[audit8status]
       ,dbo.[bbCentre].[audit8by]
       ,dbo.[bbCentre].[audit8notes]
       ,dbo.[bbCentre].[audit8Date]
       ,dbo.[bbCentre].[audit8RptSentDate]
       ,dbo.[bbCentre].[audit8RptRcvdDate]
       ,dbo.[bbCentre].[amd10_sentdate]
       ,dbo.[bbCentre].[amd10_approvaldate]
       ,dbo.[bbCentre].[amd11_sentdate]
       ,dbo.[bbCentre].[amd11_approvaldate]
  FROM dbo.[bbCentre]
 WHERE EXISTS (SELECT 1
                 FROM dbo.[bbCentreExternalStudyLink] 
                WHERE dbo.[bbCentreExternalStudyLink].[externalStudyID] = @ExternalStudyId
                  AND dbo.[bbCentreExternalStudyLink].[centreid] = dbo.[bbCentre].[centreid]
                  )
				SELECT @@ROWCOUNT			
				

GO
GRANT EXEC ON dbo.znt_bbCentre_GetByExternalStudyIdFromBbCentreExternalStudyLink TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCentre_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCentre_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCentre_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbCentre table passing nullable parameters
-- Table Comment: Centre
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCentre_Find
(

	@SearchUsingOR bit   = null ,

	@Centreid int   = null ,

	@Centrename varchar (150)  = null ,

	@Address1 varchar (50)  = null ,

	@Address2 varchar (50)  = null ,

	@Address3 varchar (50)  = null ,

	@Address4 varchar (50)  = null ,

	@Address5 varchar (50)  = null ,

	@Postcode varchar (10)  = null ,

	@Mapref varchar (20)  = null ,

	@Centreregionid int   = null ,

	@Centrestatusid int   = null ,

	@Piid int   = null ,

	@Picontactdate datetime   = null ,

	@Ssisubmittedrddate datetime   = null ,

	@Lrecapproveddate datetime   = null ,

	@Rdapproveddate datetime   = null ,

	@Setupdate datetime   = null ,

	@Pat1recruiteddate datetime   = null ,

	@Financecontact varchar (255)  = null ,

	@Contactdetails varchar (255)  = null ,

	@Accountnumber varchar (100)  = null ,

	@ClrNstatus varchar (150)  = null ,

	@UkcrNregionid int   = null ,

	@UkcrnContact varchar (50)  = null ,

	@UkcrnSitecode varchar (100)  = null ,

	@UkcrnSitenumber varchar (16)  = null ,

	@SkipNhsNumber bit   = null ,

	@Comments text   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@Primarycontact varchar (MAX)  = null ,

	@Picontact varchar (MAX)  = null ,

	@PiidName varchar (MAX)  = null ,

	@Rndcontact varchar (MAX)  = null ,

	@Rndreference varchar (MAX)  = null ,

	@Teamadditioncomments varchar (MAX)  = null ,

	@Auditstatus varchar (MAX)  = null ,

	@Auditedby varchar (MAX)  = null ,

	@Auditnotes varchar (MAX)  = null ,

	@Amd7Sentdate datetime   = null ,

	@Amd7Approvaldate datetime   = null ,

	@Ctamdaug13Sentdate datetime   = null ,

	@Ctamdaug13Approvaldate datetime   = null ,

	@OtherActiveUsers varchar (1024)  = null ,

	@Audit2status varchar (MAX)  = null ,

	@Audit2by varchar (MAX)  = null ,

	@Audit2notes varchar (MAX)  = null ,

	@Audit3status varchar (MAX)  = null ,

	@Audit3by varchar (MAX)  = null ,

	@Audit3notes varchar (MAX)  = null ,

	@Audit4status varchar (MAX)  = null ,

	@Audit4by varchar (MAX)  = null ,

	@Audit4notes varchar (MAX)  = null ,

	@Audit1Date date   = null ,

	@Audit2Date date   = null ,

	@Audit3Date date   = null ,

	@Audit4Date date   = null ,

	@Audit1RptSentDate date   = null ,

	@Audit2RptSentDate date   = null ,

	@Audit3RptSentDate date   = null ,

	@Audit4RptSentDate date   = null ,

	@Audit1RptRcvdDate date   = null ,

	@Audit2RptRcvdDate date   = null ,

	@Audit3RptRcvdDate date   = null ,

	@Audit4RptRcvdDate date   = null ,

	@Amd8Sentdate datetime   = null ,

	@Amd8Approvaldate datetime   = null ,

	@Amd9Sentdate datetime   = null ,

	@Amd9Approvaldate datetime   = null ,

	@AltCentreId char (4)  = null ,

	@IsTrainingCentre int   = null ,

	@Audit5status varchar (MAX)  = null ,

	@Audit5by varchar (MAX)  = null ,

	@Audit5notes varchar (MAX)  = null ,

	@Audit5Date date   = null ,

	@Audit5RptSentDate date   = null ,

	@Audit5RptRcvdDate date   = null ,

	@Audit6status varchar (MAX)  = null ,

	@Audit6by varchar (MAX)  = null ,

	@Audit6notes varchar (MAX)  = null ,

	@Audit6Date date   = null ,

	@Audit6RptSentDate date   = null ,

	@Audit6RptRcvdDate date   = null ,

	@Audit7status varchar (MAX)  = null ,

	@Audit7by varchar (MAX)  = null ,

	@Audit7notes varchar (MAX)  = null ,

	@Audit7Date date   = null ,

	@Audit7RptSentDate date   = null ,

	@Audit7RptRcvdDate date   = null ,

	@Audit8status varchar (MAX)  = null ,

	@Audit8by varchar (MAX)  = null ,

	@Audit8notes varchar (MAX)  = null ,

	@Audit8Date date   = null ,

	@Audit8RptSentDate date   = null ,

	@Audit8RptRcvdDate date   = null ,

	@Amd10Sentdate date   = null ,

	@Amd10Approvaldate date   = null ,

	@Amd11Sentdate date   = null ,

	@Amd11Approvaldate date   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [centreid]
	, [centrename]
	, [address1]
	, [address2]
	, [address3]
	, [address4]
	, [address5]
	, [postcode]
	, [mapref]
	, [centreregionid]
	, [centrestatusid]
	, [piid]
	, [picontactdate]
	, [ssisubmittedrddate]
	, [lrecapproveddate]
	, [rdapproveddate]
	, [setupdate]
	, [pat1recruiteddate]
	, [financecontact]
	, [contactdetails]
	, [accountnumber]
	, [CLRNstatus]
	, [UKCRNregionid]
	, [UKCRN_contact]
	, [UKCRN_sitecode]
	, [UKCRN_sitenumber]
	, [skip_nhs_number]
	, [comments]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [primarycontact]
	, [picontact]
	, [piidName]
	, [rndcontact]
	, [rndreference]
	, [teamadditioncomments]
	, [auditstatus]
	, [auditedby]
	, [auditnotes]
	, [amd7_sentdate]
	, [amd7_approvaldate]
	, [ctamdaug13_sentdate]
	, [ctamdaug13_approvaldate]
	, [otherActiveUsers]
	, [audit2status]
	, [audit2by]
	, [audit2notes]
	, [audit3status]
	, [audit3by]
	, [audit3notes]
	, [audit4status]
	, [audit4by]
	, [audit4notes]
	, [audit1Date]
	, [audit2Date]
	, [audit3Date]
	, [audit4Date]
	, [audit1RptSentDate]
	, [audit2RptSentDate]
	, [audit3RptSentDate]
	, [audit4RptSentDate]
	, [audit1RptRcvdDate]
	, [audit2RptRcvdDate]
	, [audit3RptRcvdDate]
	, [audit4RptRcvdDate]
	, [amd8_sentdate]
	, [amd8_approvaldate]
	, [amd9_sentdate]
	, [amd9_approvaldate]
	, [altCentreID]
	, [isTrainingCentre]
	, [audit5status]
	, [audit5by]
	, [audit5notes]
	, [audit5Date]
	, [audit5RptSentDate]
	, [audit5RptRcvdDate]
	, [audit6status]
	, [audit6by]
	, [audit6notes]
	, [audit6Date]
	, [audit6RptSentDate]
	, [audit6RptRcvdDate]
	, [audit7status]
	, [audit7by]
	, [audit7notes]
	, [audit7Date]
	, [audit7RptSentDate]
	, [audit7RptRcvdDate]
	, [audit8status]
	, [audit8by]
	, [audit8notes]
	, [audit8Date]
	, [audit8RptSentDate]
	, [audit8RptRcvdDate]
	, [amd10_sentdate]
	, [amd10_approvaldate]
	, [amd11_sentdate]
	, [amd11_approvaldate]
    FROM
	[dbo].[bbCentre]
    WHERE 
	 ([centreid] = @Centreid OR @Centreid IS NULL)
	AND ([centrename] = @Centrename OR @Centrename IS NULL)
	AND ([address1] = @Address1 OR @Address1 IS NULL)
	AND ([address2] = @Address2 OR @Address2 IS NULL)
	AND ([address3] = @Address3 OR @Address3 IS NULL)
	AND ([address4] = @Address4 OR @Address4 IS NULL)
	AND ([address5] = @Address5 OR @Address5 IS NULL)
	AND ([postcode] = @Postcode OR @Postcode IS NULL)
	AND ([mapref] = @Mapref OR @Mapref IS NULL)
	AND ([centreregionid] = @Centreregionid OR @Centreregionid IS NULL)
	AND ([centrestatusid] = @Centrestatusid OR @Centrestatusid IS NULL)
	AND ([piid] = @Piid OR @Piid IS NULL)
	AND ([picontactdate] = @Picontactdate OR @Picontactdate IS NULL)
	AND ([ssisubmittedrddate] = @Ssisubmittedrddate OR @Ssisubmittedrddate IS NULL)
	AND ([lrecapproveddate] = @Lrecapproveddate OR @Lrecapproveddate IS NULL)
	AND ([rdapproveddate] = @Rdapproveddate OR @Rdapproveddate IS NULL)
	AND ([setupdate] = @Setupdate OR @Setupdate IS NULL)
	AND ([pat1recruiteddate] = @Pat1recruiteddate OR @Pat1recruiteddate IS NULL)
	AND ([financecontact] = @Financecontact OR @Financecontact IS NULL)
	AND ([contactdetails] = @Contactdetails OR @Contactdetails IS NULL)
	AND ([accountnumber] = @Accountnumber OR @Accountnumber IS NULL)
	AND ([CLRNstatus] = @ClrNstatus OR @ClrNstatus IS NULL)
	AND ([UKCRNregionid] = @UkcrNregionid OR @UkcrNregionid IS NULL)
	AND ([UKCRN_contact] = @UkcrnContact OR @UkcrnContact IS NULL)
	AND ([UKCRN_sitecode] = @UkcrnSitecode OR @UkcrnSitecode IS NULL)
	AND ([UKCRN_sitenumber] = @UkcrnSitenumber OR @UkcrnSitenumber IS NULL)
	AND ([skip_nhs_number] = @SkipNhsNumber OR @SkipNhsNumber IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([primarycontact] = @Primarycontact OR @Primarycontact IS NULL)
	AND ([picontact] = @Picontact OR @Picontact IS NULL)
	AND ([piidName] = @PiidName OR @PiidName IS NULL)
	AND ([rndcontact] = @Rndcontact OR @Rndcontact IS NULL)
	AND ([rndreference] = @Rndreference OR @Rndreference IS NULL)
	AND ([teamadditioncomments] = @Teamadditioncomments OR @Teamadditioncomments IS NULL)
	AND ([auditstatus] = @Auditstatus OR @Auditstatus IS NULL)
	AND ([auditedby] = @Auditedby OR @Auditedby IS NULL)
	AND ([auditnotes] = @Auditnotes OR @Auditnotes IS NULL)
	AND ([amd7_sentdate] = @Amd7Sentdate OR @Amd7Sentdate IS NULL)
	AND ([amd7_approvaldate] = @Amd7Approvaldate OR @Amd7Approvaldate IS NULL)
	AND ([ctamdaug13_sentdate] = @Ctamdaug13Sentdate OR @Ctamdaug13Sentdate IS NULL)
	AND ([ctamdaug13_approvaldate] = @Ctamdaug13Approvaldate OR @Ctamdaug13Approvaldate IS NULL)
	AND ([otherActiveUsers] = @OtherActiveUsers OR @OtherActiveUsers IS NULL)
	AND ([audit2status] = @Audit2status OR @Audit2status IS NULL)
	AND ([audit2by] = @Audit2by OR @Audit2by IS NULL)
	AND ([audit2notes] = @Audit2notes OR @Audit2notes IS NULL)
	AND ([audit3status] = @Audit3status OR @Audit3status IS NULL)
	AND ([audit3by] = @Audit3by OR @Audit3by IS NULL)
	AND ([audit3notes] = @Audit3notes OR @Audit3notes IS NULL)
	AND ([audit4status] = @Audit4status OR @Audit4status IS NULL)
	AND ([audit4by] = @Audit4by OR @Audit4by IS NULL)
	AND ([audit4notes] = @Audit4notes OR @Audit4notes IS NULL)
	AND ([audit1Date] = @Audit1Date OR @Audit1Date IS NULL)
	AND ([audit2Date] = @Audit2Date OR @Audit2Date IS NULL)
	AND ([audit3Date] = @Audit3Date OR @Audit3Date IS NULL)
	AND ([audit4Date] = @Audit4Date OR @Audit4Date IS NULL)
	AND ([audit1RptSentDate] = @Audit1RptSentDate OR @Audit1RptSentDate IS NULL)
	AND ([audit2RptSentDate] = @Audit2RptSentDate OR @Audit2RptSentDate IS NULL)
	AND ([audit3RptSentDate] = @Audit3RptSentDate OR @Audit3RptSentDate IS NULL)
	AND ([audit4RptSentDate] = @Audit4RptSentDate OR @Audit4RptSentDate IS NULL)
	AND ([audit1RptRcvdDate] = @Audit1RptRcvdDate OR @Audit1RptRcvdDate IS NULL)
	AND ([audit2RptRcvdDate] = @Audit2RptRcvdDate OR @Audit2RptRcvdDate IS NULL)
	AND ([audit3RptRcvdDate] = @Audit3RptRcvdDate OR @Audit3RptRcvdDate IS NULL)
	AND ([audit4RptRcvdDate] = @Audit4RptRcvdDate OR @Audit4RptRcvdDate IS NULL)
	AND ([amd8_sentdate] = @Amd8Sentdate OR @Amd8Sentdate IS NULL)
	AND ([amd8_approvaldate] = @Amd8Approvaldate OR @Amd8Approvaldate IS NULL)
	AND ([amd9_sentdate] = @Amd9Sentdate OR @Amd9Sentdate IS NULL)
	AND ([amd9_approvaldate] = @Amd9Approvaldate OR @Amd9Approvaldate IS NULL)
	AND ([altCentreID] = @AltCentreId OR @AltCentreId IS NULL)
	AND ([isTrainingCentre] = @IsTrainingCentre OR @IsTrainingCentre IS NULL)
	AND ([audit5status] = @Audit5status OR @Audit5status IS NULL)
	AND ([audit5by] = @Audit5by OR @Audit5by IS NULL)
	AND ([audit5notes] = @Audit5notes OR @Audit5notes IS NULL)
	AND ([audit5Date] = @Audit5Date OR @Audit5Date IS NULL)
	AND ([audit5RptSentDate] = @Audit5RptSentDate OR @Audit5RptSentDate IS NULL)
	AND ([audit5RptRcvdDate] = @Audit5RptRcvdDate OR @Audit5RptRcvdDate IS NULL)
	AND ([audit6status] = @Audit6status OR @Audit6status IS NULL)
	AND ([audit6by] = @Audit6by OR @Audit6by IS NULL)
	AND ([audit6notes] = @Audit6notes OR @Audit6notes IS NULL)
	AND ([audit6Date] = @Audit6Date OR @Audit6Date IS NULL)
	AND ([audit6RptSentDate] = @Audit6RptSentDate OR @Audit6RptSentDate IS NULL)
	AND ([audit6RptRcvdDate] = @Audit6RptRcvdDate OR @Audit6RptRcvdDate IS NULL)
	AND ([audit7status] = @Audit7status OR @Audit7status IS NULL)
	AND ([audit7by] = @Audit7by OR @Audit7by IS NULL)
	AND ([audit7notes] = @Audit7notes OR @Audit7notes IS NULL)
	AND ([audit7Date] = @Audit7Date OR @Audit7Date IS NULL)
	AND ([audit7RptSentDate] = @Audit7RptSentDate OR @Audit7RptSentDate IS NULL)
	AND ([audit7RptRcvdDate] = @Audit7RptRcvdDate OR @Audit7RptRcvdDate IS NULL)
	AND ([audit8status] = @Audit8status OR @Audit8status IS NULL)
	AND ([audit8by] = @Audit8by OR @Audit8by IS NULL)
	AND ([audit8notes] = @Audit8notes OR @Audit8notes IS NULL)
	AND ([audit8Date] = @Audit8Date OR @Audit8Date IS NULL)
	AND ([audit8RptSentDate] = @Audit8RptSentDate OR @Audit8RptSentDate IS NULL)
	AND ([audit8RptRcvdDate] = @Audit8RptRcvdDate OR @Audit8RptRcvdDate IS NULL)
	AND ([amd10_sentdate] = @Amd10Sentdate OR @Amd10Sentdate IS NULL)
	AND ([amd10_approvaldate] = @Amd10Approvaldate OR @Amd10Approvaldate IS NULL)
	AND ([amd11_sentdate] = @Amd11Sentdate OR @Amd11Sentdate IS NULL)
	AND ([amd11_approvaldate] = @Amd11Approvaldate OR @Amd11Approvaldate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [centreid]
	, [centrename]
	, [address1]
	, [address2]
	, [address3]
	, [address4]
	, [address5]
	, [postcode]
	, [mapref]
	, [centreregionid]
	, [centrestatusid]
	, [piid]
	, [picontactdate]
	, [ssisubmittedrddate]
	, [lrecapproveddate]
	, [rdapproveddate]
	, [setupdate]
	, [pat1recruiteddate]
	, [financecontact]
	, [contactdetails]
	, [accountnumber]
	, [CLRNstatus]
	, [UKCRNregionid]
	, [UKCRN_contact]
	, [UKCRN_sitecode]
	, [UKCRN_sitenumber]
	, [skip_nhs_number]
	, [comments]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [primarycontact]
	, [picontact]
	, [piidName]
	, [rndcontact]
	, [rndreference]
	, [teamadditioncomments]
	, [auditstatus]
	, [auditedby]
	, [auditnotes]
	, [amd7_sentdate]
	, [amd7_approvaldate]
	, [ctamdaug13_sentdate]
	, [ctamdaug13_approvaldate]
	, [otherActiveUsers]
	, [audit2status]
	, [audit2by]
	, [audit2notes]
	, [audit3status]
	, [audit3by]
	, [audit3notes]
	, [audit4status]
	, [audit4by]
	, [audit4notes]
	, [audit1Date]
	, [audit2Date]
	, [audit3Date]
	, [audit4Date]
	, [audit1RptSentDate]
	, [audit2RptSentDate]
	, [audit3RptSentDate]
	, [audit4RptSentDate]
	, [audit1RptRcvdDate]
	, [audit2RptRcvdDate]
	, [audit3RptRcvdDate]
	, [audit4RptRcvdDate]
	, [amd8_sentdate]
	, [amd8_approvaldate]
	, [amd9_sentdate]
	, [amd9_approvaldate]
	, [altCentreID]
	, [isTrainingCentre]
	, [audit5status]
	, [audit5by]
	, [audit5notes]
	, [audit5Date]
	, [audit5RptSentDate]
	, [audit5RptRcvdDate]
	, [audit6status]
	, [audit6by]
	, [audit6notes]
	, [audit6Date]
	, [audit6RptSentDate]
	, [audit6RptRcvdDate]
	, [audit7status]
	, [audit7by]
	, [audit7notes]
	, [audit7Date]
	, [audit7RptSentDate]
	, [audit7RptRcvdDate]
	, [audit8status]
	, [audit8by]
	, [audit8notes]
	, [audit8Date]
	, [audit8RptSentDate]
	, [audit8RptRcvdDate]
	, [amd10_sentdate]
	, [amd10_approvaldate]
	, [amd11_sentdate]
	, [amd11_approvaldate]
    FROM
	[dbo].[bbCentre]
    WHERE 
	 ([centreid] = @Centreid AND @Centreid is not null)
	OR ([centrename] = @Centrename AND @Centrename is not null)
	OR ([address1] = @Address1 AND @Address1 is not null)
	OR ([address2] = @Address2 AND @Address2 is not null)
	OR ([address3] = @Address3 AND @Address3 is not null)
	OR ([address4] = @Address4 AND @Address4 is not null)
	OR ([address5] = @Address5 AND @Address5 is not null)
	OR ([postcode] = @Postcode AND @Postcode is not null)
	OR ([mapref] = @Mapref AND @Mapref is not null)
	OR ([centreregionid] = @Centreregionid AND @Centreregionid is not null)
	OR ([centrestatusid] = @Centrestatusid AND @Centrestatusid is not null)
	OR ([piid] = @Piid AND @Piid is not null)
	OR ([picontactdate] = @Picontactdate AND @Picontactdate is not null)
	OR ([ssisubmittedrddate] = @Ssisubmittedrddate AND @Ssisubmittedrddate is not null)
	OR ([lrecapproveddate] = @Lrecapproveddate AND @Lrecapproveddate is not null)
	OR ([rdapproveddate] = @Rdapproveddate AND @Rdapproveddate is not null)
	OR ([setupdate] = @Setupdate AND @Setupdate is not null)
	OR ([pat1recruiteddate] = @Pat1recruiteddate AND @Pat1recruiteddate is not null)
	OR ([financecontact] = @Financecontact AND @Financecontact is not null)
	OR ([contactdetails] = @Contactdetails AND @Contactdetails is not null)
	OR ([accountnumber] = @Accountnumber AND @Accountnumber is not null)
	OR ([CLRNstatus] = @ClrNstatus AND @ClrNstatus is not null)
	OR ([UKCRNregionid] = @UkcrNregionid AND @UkcrNregionid is not null)
	OR ([UKCRN_contact] = @UkcrnContact AND @UkcrnContact is not null)
	OR ([UKCRN_sitecode] = @UkcrnSitecode AND @UkcrnSitecode is not null)
	OR ([UKCRN_sitenumber] = @UkcrnSitenumber AND @UkcrnSitenumber is not null)
	OR ([skip_nhs_number] = @SkipNhsNumber AND @SkipNhsNumber is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([primarycontact] = @Primarycontact AND @Primarycontact is not null)
	OR ([picontact] = @Picontact AND @Picontact is not null)
	OR ([piidName] = @PiidName AND @PiidName is not null)
	OR ([rndcontact] = @Rndcontact AND @Rndcontact is not null)
	OR ([rndreference] = @Rndreference AND @Rndreference is not null)
	OR ([teamadditioncomments] = @Teamadditioncomments AND @Teamadditioncomments is not null)
	OR ([auditstatus] = @Auditstatus AND @Auditstatus is not null)
	OR ([auditedby] = @Auditedby AND @Auditedby is not null)
	OR ([auditnotes] = @Auditnotes AND @Auditnotes is not null)
	OR ([amd7_sentdate] = @Amd7Sentdate AND @Amd7Sentdate is not null)
	OR ([amd7_approvaldate] = @Amd7Approvaldate AND @Amd7Approvaldate is not null)
	OR ([ctamdaug13_sentdate] = @Ctamdaug13Sentdate AND @Ctamdaug13Sentdate is not null)
	OR ([ctamdaug13_approvaldate] = @Ctamdaug13Approvaldate AND @Ctamdaug13Approvaldate is not null)
	OR ([otherActiveUsers] = @OtherActiveUsers AND @OtherActiveUsers is not null)
	OR ([audit2status] = @Audit2status AND @Audit2status is not null)
	OR ([audit2by] = @Audit2by AND @Audit2by is not null)
	OR ([audit2notes] = @Audit2notes AND @Audit2notes is not null)
	OR ([audit3status] = @Audit3status AND @Audit3status is not null)
	OR ([audit3by] = @Audit3by AND @Audit3by is not null)
	OR ([audit3notes] = @Audit3notes AND @Audit3notes is not null)
	OR ([audit4status] = @Audit4status AND @Audit4status is not null)
	OR ([audit4by] = @Audit4by AND @Audit4by is not null)
	OR ([audit4notes] = @Audit4notes AND @Audit4notes is not null)
	OR ([audit1Date] = @Audit1Date AND @Audit1Date is not null)
	OR ([audit2Date] = @Audit2Date AND @Audit2Date is not null)
	OR ([audit3Date] = @Audit3Date AND @Audit3Date is not null)
	OR ([audit4Date] = @Audit4Date AND @Audit4Date is not null)
	OR ([audit1RptSentDate] = @Audit1RptSentDate AND @Audit1RptSentDate is not null)
	OR ([audit2RptSentDate] = @Audit2RptSentDate AND @Audit2RptSentDate is not null)
	OR ([audit3RptSentDate] = @Audit3RptSentDate AND @Audit3RptSentDate is not null)
	OR ([audit4RptSentDate] = @Audit4RptSentDate AND @Audit4RptSentDate is not null)
	OR ([audit1RptRcvdDate] = @Audit1RptRcvdDate AND @Audit1RptRcvdDate is not null)
	OR ([audit2RptRcvdDate] = @Audit2RptRcvdDate AND @Audit2RptRcvdDate is not null)
	OR ([audit3RptRcvdDate] = @Audit3RptRcvdDate AND @Audit3RptRcvdDate is not null)
	OR ([audit4RptRcvdDate] = @Audit4RptRcvdDate AND @Audit4RptRcvdDate is not null)
	OR ([amd8_sentdate] = @Amd8Sentdate AND @Amd8Sentdate is not null)
	OR ([amd8_approvaldate] = @Amd8Approvaldate AND @Amd8Approvaldate is not null)
	OR ([amd9_sentdate] = @Amd9Sentdate AND @Amd9Sentdate is not null)
	OR ([amd9_approvaldate] = @Amd9Approvaldate AND @Amd9Approvaldate is not null)
	OR ([altCentreID] = @AltCentreId AND @AltCentreId is not null)
	OR ([isTrainingCentre] = @IsTrainingCentre AND @IsTrainingCentre is not null)
	OR ([audit5status] = @Audit5status AND @Audit5status is not null)
	OR ([audit5by] = @Audit5by AND @Audit5by is not null)
	OR ([audit5notes] = @Audit5notes AND @Audit5notes is not null)
	OR ([audit5Date] = @Audit5Date AND @Audit5Date is not null)
	OR ([audit5RptSentDate] = @Audit5RptSentDate AND @Audit5RptSentDate is not null)
	OR ([audit5RptRcvdDate] = @Audit5RptRcvdDate AND @Audit5RptRcvdDate is not null)
	OR ([audit6status] = @Audit6status AND @Audit6status is not null)
	OR ([audit6by] = @Audit6by AND @Audit6by is not null)
	OR ([audit6notes] = @Audit6notes AND @Audit6notes is not null)
	OR ([audit6Date] = @Audit6Date AND @Audit6Date is not null)
	OR ([audit6RptSentDate] = @Audit6RptSentDate AND @Audit6RptSentDate is not null)
	OR ([audit6RptRcvdDate] = @Audit6RptRcvdDate AND @Audit6RptRcvdDate is not null)
	OR ([audit7status] = @Audit7status AND @Audit7status is not null)
	OR ([audit7by] = @Audit7by AND @Audit7by is not null)
	OR ([audit7notes] = @Audit7notes AND @Audit7notes is not null)
	OR ([audit7Date] = @Audit7Date AND @Audit7Date is not null)
	OR ([audit7RptSentDate] = @Audit7RptSentDate AND @Audit7RptSentDate is not null)
	OR ([audit7RptRcvdDate] = @Audit7RptRcvdDate AND @Audit7RptRcvdDate is not null)
	OR ([audit8status] = @Audit8status AND @Audit8status is not null)
	OR ([audit8by] = @Audit8by AND @Audit8by is not null)
	OR ([audit8notes] = @Audit8notes AND @Audit8notes is not null)
	OR ([audit8Date] = @Audit8Date AND @Audit8Date is not null)
	OR ([audit8RptSentDate] = @Audit8RptSentDate AND @Audit8RptSentDate is not null)
	OR ([audit8RptRcvdDate] = @Audit8RptRcvdDate AND @Audit8RptRcvdDate is not null)
	OR ([amd10_sentdate] = @Amd10Sentdate AND @Amd10Sentdate is not null)
	OR ([amd10_approvaldate] = @Amd10Approvaldate AND @Amd10Approvaldate is not null)
	OR ([amd11_sentdate] = @Amd11Sentdate AND @Amd11Sentdate is not null)
	OR ([amd11_approvaldate] = @Amd11Approvaldate AND @Amd11Approvaldate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbCentre_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientdrug table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_Get_List

AS


				
				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientdrug table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [patdrugid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([patdrugid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [patdrugid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientdrug]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[patdrugid], O.[FupId], O.[drugid], O.[startday], O.[startmonth], O.[startyear], O.[startdate], O.[startestimated], O.[stopday], O.[stopmonth], O.[stopyear], O.[stopdate], O.[stopreasonid], O.[otherstopreason], O.[discontinued], O.[dose], O.[doseunits], O.[doseunitid], O.[frequency], O.[commonfrequencyid], O.[firstbiologic], O.[systemic], O.[inceightymgloadingdose], O.[intermittentlyreceived], O.[enteredasconventional], O.[enteredascurrent], O.[enteredasbiologic], O.[enteredaspast], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[doseDateReason], O.[datesReconfirmed], O.[dosageReconfirmed], O.[genericLoadingDose], O.[enteredassmallmolecule], O.[adminStopReasonID], O.[stopReasonChecked]
				FROM
				    [dbo].[bbPatientdrug] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[patdrugid] = PageIndex.[patdrugid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientdrug]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientdrug table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_Insert
(

	@Patdrugid int    OUTPUT,

	@FupId int   ,

	@Drugid int   ,

	@Startday int   ,

	@Startmonth int   ,

	@Startyear int   ,

	@Startdate datetime   ,

	@Startestimated bit   ,

	@Stopday int   ,

	@Stopmonth int   ,

	@Stopyear int   ,

	@Stopdate datetime   ,

	@Stopreasonid int   ,

	@Otherstopreason varchar (250)  ,

	@Discontinued int   ,

	@Dose float   ,

	@Doseunits varchar (50)  ,

	@Doseunitid int   ,

	@Frequency int   ,

	@Commonfrequencyid int   ,

	@Firstbiologic bit   ,

	@Systemic bit   ,

	@Inceightymgloadingdose bit   ,

	@Intermittentlyreceived bit   ,

	@Enteredasconventional bit   ,

	@Enteredascurrent bit   ,

	@Enteredasbiologic bit   ,

	@Enteredaspast bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@DoseDateReason varchar (1024)  ,

	@DatesReconfirmed tinyint   ,

	@DosageReconfirmed tinyint   ,

	@GenericLoadingDose int   ,

	@Enteredassmallmolecule bit   ,

	@AdminStopReasonId int   ,

	@StopReasonChecked bit   
)
AS


				
				INSERT INTO [dbo].[bbPatientdrug]
					(
					[FupId]
					,[drugid]
					,[startday]
					,[startmonth]
					,[startyear]
					,[startdate]
					,[startestimated]
					,[stopday]
					,[stopmonth]
					,[stopyear]
					,[stopdate]
					,[stopreasonid]
					,[otherstopreason]
					,[discontinued]
					,[dose]
					,[doseunits]
					,[doseunitid]
					,[frequency]
					,[commonfrequencyid]
					,[firstbiologic]
					,[systemic]
					,[inceightymgloadingdose]
					,[intermittentlyreceived]
					,[enteredasconventional]
					,[enteredascurrent]
					,[enteredasbiologic]
					,[enteredaspast]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[doseDateReason]
					,[datesReconfirmed]
					,[dosageReconfirmed]
					,[genericLoadingDose]
					,[enteredassmallmolecule]
					,[adminStopReasonID]
					,[stopReasonChecked]
					)
				VALUES
					(
					@FupId
					,@Drugid
					,@Startday
					,@Startmonth
					,@Startyear
					,@Startdate
					,@Startestimated
					,@Stopday
					,@Stopmonth
					,@Stopyear
					,@Stopdate
					,@Stopreasonid
					,@Otherstopreason
					,@Discontinued
					,@Dose
					,@Doseunits
					,@Doseunitid
					,@Frequency
					,@Commonfrequencyid
					,@Firstbiologic
					,@Systemic
					,@Inceightymgloadingdose
					,@Intermittentlyreceived
					,@Enteredasconventional
					,@Enteredascurrent
					,@Enteredasbiologic
					,@Enteredaspast
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@DoseDateReason
					,@DatesReconfirmed
					,@DosageReconfirmed
					,@GenericLoadingDose
					,@Enteredassmallmolecule
					,@AdminStopReasonId
					,@StopReasonChecked
					)
				-- Get the identity value
				SET @Patdrugid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientdrug table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_Update
(

	@Patdrugid int   ,

	@FupId int   ,

	@Drugid int   ,

	@Startday int   ,

	@Startmonth int   ,

	@Startyear int   ,

	@Startdate datetime   ,

	@Startestimated bit   ,

	@Stopday int   ,

	@Stopmonth int   ,

	@Stopyear int   ,

	@Stopdate datetime   ,

	@Stopreasonid int   ,

	@Otherstopreason varchar (250)  ,

	@Discontinued int   ,

	@Dose float   ,

	@Doseunits varchar (50)  ,

	@Doseunitid int   ,

	@Frequency int   ,

	@Commonfrequencyid int   ,

	@Firstbiologic bit   ,

	@Systemic bit   ,

	@Inceightymgloadingdose bit   ,

	@Intermittentlyreceived bit   ,

	@Enteredasconventional bit   ,

	@Enteredascurrent bit   ,

	@Enteredasbiologic bit   ,

	@Enteredaspast bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@DoseDateReason varchar (1024)  ,

	@DatesReconfirmed tinyint   ,

	@DosageReconfirmed tinyint   ,

	@GenericLoadingDose int   ,

	@Enteredassmallmolecule bit   ,

	@AdminStopReasonId int   ,

	@StopReasonChecked bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientdrug]
				SET
					[FupId] = @FupId
					,[drugid] = @Drugid
					,[startday] = @Startday
					,[startmonth] = @Startmonth
					,[startyear] = @Startyear
					,[startdate] = @Startdate
					,[startestimated] = @Startestimated
					,[stopday] = @Stopday
					,[stopmonth] = @Stopmonth
					,[stopyear] = @Stopyear
					,[stopdate] = @Stopdate
					,[stopreasonid] = @Stopreasonid
					,[otherstopreason] = @Otherstopreason
					,[discontinued] = @Discontinued
					,[dose] = @Dose
					,[doseunits] = @Doseunits
					,[doseunitid] = @Doseunitid
					,[frequency] = @Frequency
					,[commonfrequencyid] = @Commonfrequencyid
					,[firstbiologic] = @Firstbiologic
					,[systemic] = @Systemic
					,[inceightymgloadingdose] = @Inceightymgloadingdose
					,[intermittentlyreceived] = @Intermittentlyreceived
					,[enteredasconventional] = @Enteredasconventional
					,[enteredascurrent] = @Enteredascurrent
					,[enteredasbiologic] = @Enteredasbiologic
					,[enteredaspast] = @Enteredaspast
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[doseDateReason] = @DoseDateReason
					,[datesReconfirmed] = @DatesReconfirmed
					,[dosageReconfirmed] = @DosageReconfirmed
					,[genericLoadingDose] = @GenericLoadingDose
					,[enteredassmallmolecule] = @Enteredassmallmolecule
					,[adminStopReasonID] = @AdminStopReasonId
					,[stopReasonChecked] = @StopReasonChecked
				WHERE
[patdrugid] = @Patdrugid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientdrug table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_Delete
(

	@Patdrugid int   
)
AS


				DELETE FROM [dbo].[bbPatientdrug] WITH (ROWLOCK) 
				WHERE
					[patdrugid] = @Patdrugid
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByCommonfrequencyid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByCommonfrequencyid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByCommonfrequencyid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByCommonfrequencyid
(

	@Commonfrequencyid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[commonfrequencyid] = @Commonfrequencyid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByCommonfrequencyid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByDoseunitid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByDoseunitid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByDoseunitid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByDoseunitid
(

	@Doseunitid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[doseunitid] = @Doseunitid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByDoseunitid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByDrugid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByDrugid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByDrugid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByDrugid
(

	@Drugid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[drugid] = @Drugid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByDrugid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByStopreasonid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByStopreasonid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByStopreasonid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByStopreasonid
(

	@Stopreasonid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[stopreasonid] = @Stopreasonid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByStopreasonid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByFupId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByFupId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByFupId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByFupId
(

	@FupId int   
)
AS


				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[FupId] = @FupId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByFupId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByStartyearStartmonthStartdayStopyearStopmonthStopday procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByStartyearStartmonthStartdayStopyearStopmonthStopday') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByStartyearStartmonthStartdayStopyearStopmonthStopday
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByStartyearStartmonthStartdayStopyearStopmonthStopday
(

	@Startyear int   ,

	@Startmonth int   ,

	@Startday int   ,

	@Stopyear int   ,

	@Stopmonth int   ,

	@Stopday int   
)
AS


				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[startyear] = @Startyear
					AND [startmonth] = @Startmonth
					AND [startday] = @Startday
					AND [stopyear] = @Stopyear
					AND [stopmonth] = @Stopmonth
					AND [stopday] = @Stopday
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByStartyearStartmonthStartdayStopyearStopmonthStopday TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByStopyearStopmonthStopday procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByStopyearStopmonthStopday') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByStopyearStopmonthStopday
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByStopyearStopmonthStopday
(

	@Stopyear int   ,

	@Stopmonth int   ,

	@Stopday int   
)
AS


				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[stopyear] = @Stopyear
					AND [stopmonth] = @Stopmonth
					AND [stopday] = @Stopday
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByStopyearStopmonthStopday TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_GetByPatdrugid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_GetByPatdrugid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_GetByPatdrugid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrug table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_GetByPatdrugid
(

	@Patdrugid int   
)
AS


				SELECT
					[patdrugid],
					[FupId],
					[drugid],
					[startday],
					[startmonth],
					[startyear],
					[startdate],
					[startestimated],
					[stopday],
					[stopmonth],
					[stopyear],
					[stopdate],
					[stopreasonid],
					[otherstopreason],
					[discontinued],
					[dose],
					[doseunits],
					[doseunitid],
					[frequency],
					[commonfrequencyid],
					[firstbiologic],
					[systemic],
					[inceightymgloadingdose],
					[intermittentlyreceived],
					[enteredasconventional],
					[enteredascurrent],
					[enteredasbiologic],
					[enteredaspast],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[doseDateReason],
					[datesReconfirmed],
					[dosageReconfirmed],
					[genericLoadingDose],
					[enteredassmallmolecule],
					[adminStopReasonID],
					[stopReasonChecked]
				FROM
					[dbo].[bbPatientdrug]
				WHERE
					[patdrugid] = @Patdrugid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_GetByPatdrugid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrug_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrug_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrug_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientdrug table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrug_Find
(

	@SearchUsingOR bit   = null ,

	@Patdrugid int   = null ,

	@FupId int   = null ,

	@Drugid int   = null ,

	@Startday int   = null ,

	@Startmonth int   = null ,

	@Startyear int   = null ,

	@Startdate datetime   = null ,

	@Startestimated bit   = null ,

	@Stopday int   = null ,

	@Stopmonth int   = null ,

	@Stopyear int   = null ,

	@Stopdate datetime   = null ,

	@Stopreasonid int   = null ,

	@Otherstopreason varchar (250)  = null ,

	@Discontinued int   = null ,

	@Dose float   = null ,

	@Doseunits varchar (50)  = null ,

	@Doseunitid int   = null ,

	@Frequency int   = null ,

	@Commonfrequencyid int   = null ,

	@Firstbiologic bit   = null ,

	@Systemic bit   = null ,

	@Inceightymgloadingdose bit   = null ,

	@Intermittentlyreceived bit   = null ,

	@Enteredasconventional bit   = null ,

	@Enteredascurrent bit   = null ,

	@Enteredasbiologic bit   = null ,

	@Enteredaspast bit   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@DoseDateReason varchar (1024)  = null ,

	@DatesReconfirmed tinyint   = null ,

	@DosageReconfirmed tinyint   = null ,

	@GenericLoadingDose int   = null ,

	@Enteredassmallmolecule bit   = null ,

	@AdminStopReasonId int   = null ,

	@StopReasonChecked bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [patdrugid]
	, [FupId]
	, [drugid]
	, [startday]
	, [startmonth]
	, [startyear]
	, [startdate]
	, [startestimated]
	, [stopday]
	, [stopmonth]
	, [stopyear]
	, [stopdate]
	, [stopreasonid]
	, [otherstopreason]
	, [discontinued]
	, [dose]
	, [doseunits]
	, [doseunitid]
	, [frequency]
	, [commonfrequencyid]
	, [firstbiologic]
	, [systemic]
	, [inceightymgloadingdose]
	, [intermittentlyreceived]
	, [enteredasconventional]
	, [enteredascurrent]
	, [enteredasbiologic]
	, [enteredaspast]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [doseDateReason]
	, [datesReconfirmed]
	, [dosageReconfirmed]
	, [genericLoadingDose]
	, [enteredassmallmolecule]
	, [adminStopReasonID]
	, [stopReasonChecked]
    FROM
	[dbo].[bbPatientdrug]
    WHERE 
	 ([patdrugid] = @Patdrugid OR @Patdrugid IS NULL)
	AND ([FupId] = @FupId OR @FupId IS NULL)
	AND ([drugid] = @Drugid OR @Drugid IS NULL)
	AND ([startday] = @Startday OR @Startday IS NULL)
	AND ([startmonth] = @Startmonth OR @Startmonth IS NULL)
	AND ([startyear] = @Startyear OR @Startyear IS NULL)
	AND ([startdate] = @Startdate OR @Startdate IS NULL)
	AND ([startestimated] = @Startestimated OR @Startestimated IS NULL)
	AND ([stopday] = @Stopday OR @Stopday IS NULL)
	AND ([stopmonth] = @Stopmonth OR @Stopmonth IS NULL)
	AND ([stopyear] = @Stopyear OR @Stopyear IS NULL)
	AND ([stopdate] = @Stopdate OR @Stopdate IS NULL)
	AND ([stopreasonid] = @Stopreasonid OR @Stopreasonid IS NULL)
	AND ([otherstopreason] = @Otherstopreason OR @Otherstopreason IS NULL)
	AND ([discontinued] = @Discontinued OR @Discontinued IS NULL)
	AND ([dose] = @Dose OR @Dose IS NULL)
	AND ([doseunits] = @Doseunits OR @Doseunits IS NULL)
	AND ([doseunitid] = @Doseunitid OR @Doseunitid IS NULL)
	AND ([frequency] = @Frequency OR @Frequency IS NULL)
	AND ([commonfrequencyid] = @Commonfrequencyid OR @Commonfrequencyid IS NULL)
	AND ([firstbiologic] = @Firstbiologic OR @Firstbiologic IS NULL)
	AND ([systemic] = @Systemic OR @Systemic IS NULL)
	AND ([inceightymgloadingdose] = @Inceightymgloadingdose OR @Inceightymgloadingdose IS NULL)
	AND ([intermittentlyreceived] = @Intermittentlyreceived OR @Intermittentlyreceived IS NULL)
	AND ([enteredasconventional] = @Enteredasconventional OR @Enteredasconventional IS NULL)
	AND ([enteredascurrent] = @Enteredascurrent OR @Enteredascurrent IS NULL)
	AND ([enteredasbiologic] = @Enteredasbiologic OR @Enteredasbiologic IS NULL)
	AND ([enteredaspast] = @Enteredaspast OR @Enteredaspast IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([doseDateReason] = @DoseDateReason OR @DoseDateReason IS NULL)
	AND ([datesReconfirmed] = @DatesReconfirmed OR @DatesReconfirmed IS NULL)
	AND ([dosageReconfirmed] = @DosageReconfirmed OR @DosageReconfirmed IS NULL)
	AND ([genericLoadingDose] = @GenericLoadingDose OR @GenericLoadingDose IS NULL)
	AND ([enteredassmallmolecule] = @Enteredassmallmolecule OR @Enteredassmallmolecule IS NULL)
	AND ([adminStopReasonID] = @AdminStopReasonId OR @AdminStopReasonId IS NULL)
	AND ([stopReasonChecked] = @StopReasonChecked OR @StopReasonChecked IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [patdrugid]
	, [FupId]
	, [drugid]
	, [startday]
	, [startmonth]
	, [startyear]
	, [startdate]
	, [startestimated]
	, [stopday]
	, [stopmonth]
	, [stopyear]
	, [stopdate]
	, [stopreasonid]
	, [otherstopreason]
	, [discontinued]
	, [dose]
	, [doseunits]
	, [doseunitid]
	, [frequency]
	, [commonfrequencyid]
	, [firstbiologic]
	, [systemic]
	, [inceightymgloadingdose]
	, [intermittentlyreceived]
	, [enteredasconventional]
	, [enteredascurrent]
	, [enteredasbiologic]
	, [enteredaspast]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [doseDateReason]
	, [datesReconfirmed]
	, [dosageReconfirmed]
	, [genericLoadingDose]
	, [enteredassmallmolecule]
	, [adminStopReasonID]
	, [stopReasonChecked]
    FROM
	[dbo].[bbPatientdrug]
    WHERE 
	 ([patdrugid] = @Patdrugid AND @Patdrugid is not null)
	OR ([FupId] = @FupId AND @FupId is not null)
	OR ([drugid] = @Drugid AND @Drugid is not null)
	OR ([startday] = @Startday AND @Startday is not null)
	OR ([startmonth] = @Startmonth AND @Startmonth is not null)
	OR ([startyear] = @Startyear AND @Startyear is not null)
	OR ([startdate] = @Startdate AND @Startdate is not null)
	OR ([startestimated] = @Startestimated AND @Startestimated is not null)
	OR ([stopday] = @Stopday AND @Stopday is not null)
	OR ([stopmonth] = @Stopmonth AND @Stopmonth is not null)
	OR ([stopyear] = @Stopyear AND @Stopyear is not null)
	OR ([stopdate] = @Stopdate AND @Stopdate is not null)
	OR ([stopreasonid] = @Stopreasonid AND @Stopreasonid is not null)
	OR ([otherstopreason] = @Otherstopreason AND @Otherstopreason is not null)
	OR ([discontinued] = @Discontinued AND @Discontinued is not null)
	OR ([dose] = @Dose AND @Dose is not null)
	OR ([doseunits] = @Doseunits AND @Doseunits is not null)
	OR ([doseunitid] = @Doseunitid AND @Doseunitid is not null)
	OR ([frequency] = @Frequency AND @Frequency is not null)
	OR ([commonfrequencyid] = @Commonfrequencyid AND @Commonfrequencyid is not null)
	OR ([firstbiologic] = @Firstbiologic AND @Firstbiologic is not null)
	OR ([systemic] = @Systemic AND @Systemic is not null)
	OR ([inceightymgloadingdose] = @Inceightymgloadingdose AND @Inceightymgloadingdose is not null)
	OR ([intermittentlyreceived] = @Intermittentlyreceived AND @Intermittentlyreceived is not null)
	OR ([enteredasconventional] = @Enteredasconventional AND @Enteredasconventional is not null)
	OR ([enteredascurrent] = @Enteredascurrent AND @Enteredascurrent is not null)
	OR ([enteredasbiologic] = @Enteredasbiologic AND @Enteredasbiologic is not null)
	OR ([enteredaspast] = @Enteredaspast AND @Enteredaspast is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([doseDateReason] = @DoseDateReason AND @DoseDateReason is not null)
	OR ([datesReconfirmed] = @DatesReconfirmed AND @DatesReconfirmed is not null)
	OR ([dosageReconfirmed] = @DosageReconfirmed AND @DosageReconfirmed is not null)
	OR ([genericLoadingDose] = @GenericLoadingDose AND @GenericLoadingDose is not null)
	OR ([enteredassmallmolecule] = @Enteredassmallmolecule AND @Enteredassmallmolecule is not null)
	OR ([adminStopReasonID] = @AdminStopReasonId AND @AdminStopReasonId is not null)
	OR ([stopReasonChecked] = @StopReasonChecked AND @StopReasonChecked is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientdrug_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryTypelkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryTypelkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryTypelkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbQueryTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryTypelkp_Get_List

AS


				
				SELECT
					[QueryTypeId],
					[QueryType],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[showForCliniciansPages],
					[showForPVSystem],
					[showForAdminQueryPages],
					[showForCentreQueryPages]
				FROM
					[dbo].[bbQueryTypelkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbQueryTypelkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryTypelkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryTypelkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryTypelkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbQueryTypelkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryTypelkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [QueryTypeId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([QueryTypeId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [QueryTypeId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryTypelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[QueryTypeId], O.[QueryType], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[showForCliniciansPages], O.[showForPVSystem], O.[showForAdminQueryPages], O.[showForCentreQueryPages]
				FROM
				    [dbo].[bbQueryTypelkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[QueryTypeId] = PageIndex.[QueryTypeId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryTypelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbQueryTypelkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryTypelkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryTypelkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryTypelkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbQueryTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryTypelkp_Insert
(

	@QueryTypeId int    OUTPUT,

	@QueryType varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@ShowForCliniciansPages bit   ,

	@ShowForPvSystem bit   ,

	@ShowForAdminQueryPages bit   ,

	@ShowForCentreQueryPages bit   
)
AS


				
				INSERT INTO [dbo].[bbQueryTypelkp]
					(
					[QueryType]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[showForCliniciansPages]
					,[showForPVSystem]
					,[showForAdminQueryPages]
					,[showForCentreQueryPages]
					)
				VALUES
					(
					@QueryType
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@ShowForCliniciansPages
					,@ShowForPvSystem
					,@ShowForAdminQueryPages
					,@ShowForCentreQueryPages
					)
				-- Get the identity value
				SET @QueryTypeId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbQueryTypelkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryTypelkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryTypelkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryTypelkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbQueryTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryTypelkp_Update
(

	@QueryTypeId int   ,

	@QueryType varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@ShowForCliniciansPages bit   ,

	@ShowForPvSystem bit   ,

	@ShowForAdminQueryPages bit   ,

	@ShowForCentreQueryPages bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbQueryTypelkp]
				SET
					[QueryType] = @QueryType
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[showForCliniciansPages] = @ShowForCliniciansPages
					,[showForPVSystem] = @ShowForPvSystem
					,[showForAdminQueryPages] = @ShowForAdminQueryPages
					,[showForCentreQueryPages] = @ShowForCentreQueryPages
				WHERE
[QueryTypeId] = @QueryTypeId 
				
			

GO
GRANT EXEC ON dbo.znt_bbQueryTypelkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryTypelkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryTypelkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryTypelkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbQueryTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryTypelkp_Delete
(

	@QueryTypeId int   
)
AS


				DELETE FROM [dbo].[bbQueryTypelkp] WITH (ROWLOCK) 
				WHERE
					[QueryTypeId] = @QueryTypeId
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryTypelkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryTypelkp_GetByQueryTypeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryTypelkp_GetByQueryTypeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryTypelkp_GetByQueryTypeId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryTypelkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryTypelkp_GetByQueryTypeId
(

	@QueryTypeId int   
)
AS


				SELECT
					[QueryTypeId],
					[QueryType],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[showForCliniciansPages],
					[showForPVSystem],
					[showForAdminQueryPages],
					[showForCentreQueryPages]
				FROM
					[dbo].[bbQueryTypelkp]
				WHERE
					[QueryTypeId] = @QueryTypeId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryTypelkp_GetByQueryTypeId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryTypelkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryTypelkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryTypelkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbQueryTypelkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryTypelkp_Find
(

	@SearchUsingOR bit   = null ,

	@QueryTypeId int   = null ,

	@QueryType varchar (100)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@ShowForCliniciansPages bit   = null ,

	@ShowForPvSystem bit   = null ,

	@ShowForAdminQueryPages bit   = null ,

	@ShowForCentreQueryPages bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [QueryTypeId]
	, [QueryType]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [showForCliniciansPages]
	, [showForPVSystem]
	, [showForAdminQueryPages]
	, [showForCentreQueryPages]
    FROM
	[dbo].[bbQueryTypelkp]
    WHERE 
	 ([QueryTypeId] = @QueryTypeId OR @QueryTypeId IS NULL)
	AND ([QueryType] = @QueryType OR @QueryType IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([showForCliniciansPages] = @ShowForCliniciansPages OR @ShowForCliniciansPages IS NULL)
	AND ([showForPVSystem] = @ShowForPvSystem OR @ShowForPvSystem IS NULL)
	AND ([showForAdminQueryPages] = @ShowForAdminQueryPages OR @ShowForAdminQueryPages IS NULL)
	AND ([showForCentreQueryPages] = @ShowForCentreQueryPages OR @ShowForCentreQueryPages IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [QueryTypeId]
	, [QueryType]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [showForCliniciansPages]
	, [showForPVSystem]
	, [showForAdminQueryPages]
	, [showForCentreQueryPages]
    FROM
	[dbo].[bbQueryTypelkp]
    WHERE 
	 ([QueryTypeId] = @QueryTypeId AND @QueryTypeId is not null)
	OR ([QueryType] = @QueryType AND @QueryType is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([showForCliniciansPages] = @ShowForCliniciansPages AND @ShowForCliniciansPages is not null)
	OR ([showForPVSystem] = @ShowForPvSystem AND @ShowForPvSystem is not null)
	OR ([showForAdminQueryPages] = @ShowForAdminQueryPages AND @ShowForAdminQueryPages is not null)
	OR ([showForCentreQueryPages] = @ShowForCentreQueryPages AND @ShowForCentreQueryPages is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbQueryTypelkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryStatuslkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryStatuslkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryStatuslkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbQueryStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryStatuslkp_Get_List

AS


				
				SELECT
					[QueryStatusId],
					[QueryStatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryStatuslkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbQueryStatuslkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryStatuslkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryStatuslkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryStatuslkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbQueryStatuslkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryStatuslkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [QueryStatusId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([QueryStatusId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [QueryStatusId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[QueryStatusId], O.[QueryStatus], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbQueryStatuslkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[QueryStatusId] = PageIndex.[QueryStatusId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbQueryStatuslkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryStatuslkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryStatuslkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryStatuslkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbQueryStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryStatuslkp_Insert
(

	@QueryStatusId int    OUTPUT,

	@QueryStatus varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbQueryStatuslkp]
					(
					[QueryStatus]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@QueryStatus
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @QueryStatusId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbQueryStatuslkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryStatuslkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryStatuslkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryStatuslkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbQueryStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryStatuslkp_Update
(

	@QueryStatusId int   ,

	@QueryStatus varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbQueryStatuslkp]
				SET
					[QueryStatus] = @QueryStatus
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[QueryStatusId] = @QueryStatusId 
				
			

GO
GRANT EXEC ON dbo.znt_bbQueryStatuslkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryStatuslkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryStatuslkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryStatuslkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbQueryStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryStatuslkp_Delete
(

	@QueryStatusId int   
)
AS


				DELETE FROM [dbo].[bbQueryStatuslkp] WITH (ROWLOCK) 
				WHERE
					[QueryStatusId] = @QueryStatusId
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryStatuslkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryStatuslkp_GetByQueryStatusId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryStatuslkp_GetByQueryStatusId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryStatuslkp_GetByQueryStatusId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryStatuslkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryStatuslkp_GetByQueryStatusId
(

	@QueryStatusId int   
)
AS


				SELECT
					[QueryStatusId],
					[QueryStatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryStatuslkp]
				WHERE
					[QueryStatusId] = @QueryStatusId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryStatuslkp_GetByQueryStatusId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryStatuslkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryStatuslkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryStatuslkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbQueryStatuslkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryStatuslkp_Find
(

	@SearchUsingOR bit   = null ,

	@QueryStatusId int   = null ,

	@QueryStatus varchar (100)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [QueryStatusId]
	, [QueryStatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbQueryStatuslkp]
    WHERE 
	 ([QueryStatusId] = @QueryStatusId OR @QueryStatusId IS NULL)
	AND ([QueryStatus] = @QueryStatus OR @QueryStatus IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [QueryStatusId]
	, [QueryStatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbQueryStatuslkp]
    WHERE 
	 ([QueryStatusId] = @QueryStatusId AND @QueryStatusId is not null)
	OR ([QueryStatus] = @QueryStatus AND @QueryStatus is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbQueryStatuslkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatusDetaillkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatusDetaillkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientStatusDetaillkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Get_List

AS


				
				SELECT
					[pstatusdetailid],
					[pstatusdetail],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientStatusDetaillkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatusDetaillkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatusDetaillkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatusDetaillkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatusDetaillkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientStatusDetaillkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatusDetaillkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [pstatusdetailid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([pstatusdetailid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [pstatusdetailid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientStatusDetaillkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[pstatusdetailid], O.[pstatusdetail], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbPatientStatusDetaillkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[pstatusdetailid] = PageIndex.[pstatusdetailid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientStatusDetaillkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatusDetaillkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatusDetaillkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatusDetaillkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientStatusDetaillkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Insert
(

	@Pstatusdetailid int    OUTPUT,

	@Pstatusdetail varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPatientStatusDetaillkp]
					(
					[pstatusdetail]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Pstatusdetail
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Pstatusdetailid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatusDetaillkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatusDetaillkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatusDetaillkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientStatusDetaillkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Update
(

	@Pstatusdetailid int   ,

	@Pstatusdetail varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientStatusDetaillkp]
				SET
					[pstatusdetail] = @Pstatusdetail
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[pstatusdetailid] = @Pstatusdetailid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatusDetaillkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatusDetaillkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatusDetaillkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientStatusDetaillkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Delete
(

	@Pstatusdetailid int   
)
AS


				DELETE FROM [dbo].[bbPatientStatusDetaillkp] WITH (ROWLOCK) 
				WHERE
					[pstatusdetailid] = @Pstatusdetailid
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatusDetaillkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatusDetaillkp_GetByPstatusdetailid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatusDetaillkp_GetByPstatusdetailid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatusDetaillkp_GetByPstatusdetailid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientStatusDetaillkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatusDetaillkp_GetByPstatusdetailid
(

	@Pstatusdetailid int   
)
AS


				SELECT
					[pstatusdetailid],
					[pstatusdetail],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientStatusDetaillkp]
				WHERE
					[pstatusdetailid] = @Pstatusdetailid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatusDetaillkp_GetByPstatusdetailid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatusDetaillkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatusDetaillkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientStatusDetaillkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatusDetaillkp_Find
(

	@SearchUsingOR bit   = null ,

	@Pstatusdetailid int   = null ,

	@Pstatusdetail varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [pstatusdetailid]
	, [pstatusdetail]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientStatusDetaillkp]
    WHERE 
	 ([pstatusdetailid] = @Pstatusdetailid OR @Pstatusdetailid IS NULL)
	AND ([pstatusdetail] = @Pstatusdetail OR @Pstatusdetail IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [pstatusdetailid]
	, [pstatusdetail]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientStatusDetaillkp]
    WHERE 
	 ([pstatusdetailid] = @Pstatusdetailid AND @Pstatusdetailid is not null)
	OR ([pstatusdetail] = @Pstatusdetail AND @Pstatusdetail is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientStatusDetaillkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientdrugdose table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_Get_List

AS


				
				SELECT
					[doseid],
					[patdrugid],
					[doseday],
					[dosemonth],
					[doseyear],
					[dosedate],
					[dose],
					[dateadded],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[batch],
					[manualEntry]
				FROM
					[dbo].[bbPatientdrugdose]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientdrugdose table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [doseid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([doseid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [doseid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientdrugdose]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[doseid], O.[patdrugid], O.[doseday], O.[dosemonth], O.[doseyear], O.[dosedate], O.[dose], O.[dateadded], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[batch], O.[manualEntry]
				FROM
				    [dbo].[bbPatientdrugdose] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[doseid] = PageIndex.[doseid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientdrugdose]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientdrugdose table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_Insert
(

	@Doseid int    OUTPUT,

	@Patdrugid int   ,

	@Doseday int   ,

	@Dosemonth int   ,

	@Doseyear int   ,

	@Dosedate datetime   ,

	@Dose varchar (50)  ,

	@Dateadded datetime   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Batch varchar (64)  ,

	@ManualEntry bit   
)
AS


				
				INSERT INTO [dbo].[bbPatientdrugdose]
					(
					[patdrugid]
					,[doseday]
					,[dosemonth]
					,[doseyear]
					,[dosedate]
					,[dose]
					,[dateadded]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[batch]
					,[manualEntry]
					)
				VALUES
					(
					@Patdrugid
					,@Doseday
					,@Dosemonth
					,@Doseyear
					,@Dosedate
					,@Dose
					,@Dateadded
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@Batch
					,@ManualEntry
					)
				-- Get the identity value
				SET @Doseid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientdrugdose table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_Update
(

	@Doseid int   ,

	@Patdrugid int   ,

	@Doseday int   ,

	@Dosemonth int   ,

	@Doseyear int   ,

	@Dosedate datetime   ,

	@Dose varchar (50)  ,

	@Dateadded datetime   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Batch varchar (64)  ,

	@ManualEntry bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientdrugdose]
				SET
					[patdrugid] = @Patdrugid
					,[doseday] = @Doseday
					,[dosemonth] = @Dosemonth
					,[doseyear] = @Doseyear
					,[dosedate] = @Dosedate
					,[dose] = @Dose
					,[dateadded] = @Dateadded
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[batch] = @Batch
					,[manualEntry] = @ManualEntry
				WHERE
[doseid] = @Doseid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientdrugdose table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_Delete
(

	@Doseid int   
)
AS


				DELETE FROM [dbo].[bbPatientdrugdose] WITH (ROWLOCK) 
				WHERE
					[doseid] = @Doseid
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_GetByPatdrugid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_GetByPatdrugid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_GetByPatdrugid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrugdose table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_GetByPatdrugid
(

	@Patdrugid int   
)
AS


				SELECT
					[doseid],
					[patdrugid],
					[doseday],
					[dosemonth],
					[doseyear],
					[dosedate],
					[dose],
					[dateadded],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[batch],
					[manualEntry]
				FROM
					[dbo].[bbPatientdrugdose]
				WHERE
					[patdrugid] = @Patdrugid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_GetByPatdrugid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_GetByDoseid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_GetByDoseid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_GetByDoseid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientdrugdose table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_GetByDoseid
(

	@Doseid int   
)
AS


				SELECT
					[doseid],
					[patdrugid],
					[doseday],
					[dosemonth],
					[doseyear],
					[dosedate],
					[dose],
					[dateadded],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[batch],
					[manualEntry]
				FROM
					[dbo].[bbPatientdrugdose]
				WHERE
					[doseid] = @Doseid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_GetByDoseid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientdrugdose_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientdrugdose_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientdrugdose_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientdrugdose table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientdrugdose_Find
(

	@SearchUsingOR bit   = null ,

	@Doseid int   = null ,

	@Patdrugid int   = null ,

	@Doseday int   = null ,

	@Dosemonth int   = null ,

	@Doseyear int   = null ,

	@Dosedate datetime   = null ,

	@Dose varchar (50)  = null ,

	@Dateadded datetime   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@Batch varchar (64)  = null ,

	@ManualEntry bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [doseid]
	, [patdrugid]
	, [doseday]
	, [dosemonth]
	, [doseyear]
	, [dosedate]
	, [dose]
	, [dateadded]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [batch]
	, [manualEntry]
    FROM
	[dbo].[bbPatientdrugdose]
    WHERE 
	 ([doseid] = @Doseid OR @Doseid IS NULL)
	AND ([patdrugid] = @Patdrugid OR @Patdrugid IS NULL)
	AND ([doseday] = @Doseday OR @Doseday IS NULL)
	AND ([dosemonth] = @Dosemonth OR @Dosemonth IS NULL)
	AND ([doseyear] = @Doseyear OR @Doseyear IS NULL)
	AND ([dosedate] = @Dosedate OR @Dosedate IS NULL)
	AND ([dose] = @Dose OR @Dose IS NULL)
	AND ([dateadded] = @Dateadded OR @Dateadded IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([batch] = @Batch OR @Batch IS NULL)
	AND ([manualEntry] = @ManualEntry OR @ManualEntry IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [doseid]
	, [patdrugid]
	, [doseday]
	, [dosemonth]
	, [doseyear]
	, [dosedate]
	, [dose]
	, [dateadded]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [batch]
	, [manualEntry]
    FROM
	[dbo].[bbPatientdrugdose]
    WHERE 
	 ([doseid] = @Doseid AND @Doseid is not null)
	OR ([patdrugid] = @Patdrugid AND @Patdrugid is not null)
	OR ([doseday] = @Doseday AND @Doseday is not null)
	OR ([dosemonth] = @Dosemonth AND @Dosemonth is not null)
	OR ([doseyear] = @Doseyear AND @Doseyear is not null)
	OR ([dosedate] = @Dosedate AND @Dosedate is not null)
	OR ([dose] = @Dose AND @Dose is not null)
	OR ([dateadded] = @Dateadded AND @Dateadded is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([batch] = @Batch AND @Batch is not null)
	OR ([manualEntry] = @ManualEntry AND @ManualEntry is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientdrugdose_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_Get_List

AS


				
				SELECT
					[FupId],
					[birthtown],
					[birthcountry],
					[workstatusid],
					[occupation],
					[ethnicityid],
					[otherethnicity],
					[outdooroccupation],
					[livetropical],
					[eversmoked],
					[eversmokednumbercigsperday],
					[agestart],
					[agestop],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drnkbeeravg],
					[drnkwineavg],
					[drnkspiritsavg],
					[drinkalcohol],
					[drnkunitsavg],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[systolic],
					[diastolic],
					[height],
					[weight],
					[waist],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[weightMissing],
					[waistMissing],
					[smokingMissing],
					[drinkingMissing]
				FROM
					[dbo].[bbPatientLifestyle]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientLifestyle table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [FupId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([FupId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [FupId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientLifestyle]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[FupId], O.[birthtown], O.[birthcountry], O.[workstatusid], O.[occupation], O.[ethnicityid], O.[otherethnicity], O.[outdooroccupation], O.[livetropical], O.[eversmoked], O.[eversmokednumbercigsperday], O.[agestart], O.[agestop], O.[currentlysmoke], O.[currentlysmokenumbercigsperday], O.[drnkbeeravg], O.[drnkwineavg], O.[drnkspiritsavg], O.[drinkalcohol], O.[drnkunitsavg], O.[admittedtohospital], O.[newdrugs], O.[newclinics], O.[systolic], O.[diastolic], O.[height], O.[weight], O.[waist], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[weightMissing], O.[waistMissing], O.[smokingMissing], O.[drinkingMissing]
				FROM
				    [dbo].[bbPatientLifestyle] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[FupId] = PageIndex.[FupId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientLifestyle]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_Insert
(

	@FupId int   ,

	@Birthtown varchar (256)  ,

	@Birthcountry varchar (256)  ,

	@Workstatusid int   ,

	@Occupation varchar (50)  ,

	@Ethnicityid int   ,

	@Otherethnicity varchar (50)  ,

	@Outdooroccupation bit   ,

	@Livetropical bit   ,

	@Eversmoked bit   ,

	@Eversmokednumbercigsperday int   ,

	@Agestart int   ,

	@Agestop int   ,

	@Currentlysmoke bit   ,

	@Currentlysmokenumbercigsperday int   ,

	@Drnkbeeravg int   ,

	@Drnkwineavg int   ,

	@Drnkspiritsavg int   ,

	@Drinkalcohol bit   ,

	@Drnkunitsavg int   ,

	@Admittedtohospital int   ,

	@Newdrugs int   ,

	@Newclinics int   ,

	@Systolic float   ,

	@Diastolic float   ,

	@Height float   ,

	@Weight float   ,

	@Waist float   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@WeightMissing bit   ,

	@WaistMissing bit   ,

	@SmokingMissing bit   ,

	@DrinkingMissing bit   
)
AS


				
				INSERT INTO [dbo].[bbPatientLifestyle]
					(
					[FupId]
					,[birthtown]
					,[birthcountry]
					,[workstatusid]
					,[occupation]
					,[ethnicityid]
					,[otherethnicity]
					,[outdooroccupation]
					,[livetropical]
					,[eversmoked]
					,[eversmokednumbercigsperday]
					,[agestart]
					,[agestop]
					,[currentlysmoke]
					,[currentlysmokenumbercigsperday]
					,[drnkbeeravg]
					,[drnkwineavg]
					,[drnkspiritsavg]
					,[drinkalcohol]
					,[drnkunitsavg]
					,[admittedtohospital]
					,[newdrugs]
					,[newclinics]
					,[systolic]
					,[diastolic]
					,[height]
					,[weight]
					,[waist]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[weightMissing]
					,[waistMissing]
					,[smokingMissing]
					,[drinkingMissing]
					)
				VALUES
					(
					@FupId
					,@Birthtown
					,@Birthcountry
					,@Workstatusid
					,@Occupation
					,@Ethnicityid
					,@Otherethnicity
					,@Outdooroccupation
					,@Livetropical
					,@Eversmoked
					,@Eversmokednumbercigsperday
					,@Agestart
					,@Agestop
					,@Currentlysmoke
					,@Currentlysmokenumbercigsperday
					,@Drnkbeeravg
					,@Drnkwineavg
					,@Drnkspiritsavg
					,@Drinkalcohol
					,@Drnkunitsavg
					,@Admittedtohospital
					,@Newdrugs
					,@Newclinics
					,@Systolic
					,@Diastolic
					,@Height
					,@Weight
					,@Waist
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@WeightMissing
					,@WaistMissing
					,@SmokingMissing
					,@DrinkingMissing
					)
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_Update
(

	@FupId int   ,

	@OriginalFupId int   ,

	@Birthtown varchar (256)  ,

	@Birthcountry varchar (256)  ,

	@Workstatusid int   ,

	@Occupation varchar (50)  ,

	@Ethnicityid int   ,

	@Otherethnicity varchar (50)  ,

	@Outdooroccupation bit   ,

	@Livetropical bit   ,

	@Eversmoked bit   ,

	@Eversmokednumbercigsperday int   ,

	@Agestart int   ,

	@Agestop int   ,

	@Currentlysmoke bit   ,

	@Currentlysmokenumbercigsperday int   ,

	@Drnkbeeravg int   ,

	@Drnkwineavg int   ,

	@Drnkspiritsavg int   ,

	@Drinkalcohol bit   ,

	@Drnkunitsavg int   ,

	@Admittedtohospital int   ,

	@Newdrugs int   ,

	@Newclinics int   ,

	@Systolic float   ,

	@Diastolic float   ,

	@Height float   ,

	@Weight float   ,

	@Waist float   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@WeightMissing bit   ,

	@WaistMissing bit   ,

	@SmokingMissing bit   ,

	@DrinkingMissing bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientLifestyle]
				SET
					[FupId] = @FupId
					,[birthtown] = @Birthtown
					,[birthcountry] = @Birthcountry
					,[workstatusid] = @Workstatusid
					,[occupation] = @Occupation
					,[ethnicityid] = @Ethnicityid
					,[otherethnicity] = @Otherethnicity
					,[outdooroccupation] = @Outdooroccupation
					,[livetropical] = @Livetropical
					,[eversmoked] = @Eversmoked
					,[eversmokednumbercigsperday] = @Eversmokednumbercigsperday
					,[agestart] = @Agestart
					,[agestop] = @Agestop
					,[currentlysmoke] = @Currentlysmoke
					,[currentlysmokenumbercigsperday] = @Currentlysmokenumbercigsperday
					,[drnkbeeravg] = @Drnkbeeravg
					,[drnkwineavg] = @Drnkwineavg
					,[drnkspiritsavg] = @Drnkspiritsavg
					,[drinkalcohol] = @Drinkalcohol
					,[drnkunitsavg] = @Drnkunitsavg
					,[admittedtohospital] = @Admittedtohospital
					,[newdrugs] = @Newdrugs
					,[newclinics] = @Newclinics
					,[systolic] = @Systolic
					,[diastolic] = @Diastolic
					,[height] = @Height
					,[weight] = @Weight
					,[waist] = @Waist
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[weightMissing] = @WeightMissing
					,[waistMissing] = @WaistMissing
					,[smokingMissing] = @SmokingMissing
					,[drinkingMissing] = @DrinkingMissing
				WHERE
[FupId] = @OriginalFupId 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_Delete
(

	@FupId int   
)
AS


				DELETE FROM [dbo].[bbPatientLifestyle] WITH (ROWLOCK) 
				WHERE
					[FupId] = @FupId
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_GetByEthnicityid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_GetByEthnicityid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_GetByEthnicityid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientLifestyle table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_GetByEthnicityid
(

	@Ethnicityid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FupId],
					[birthtown],
					[birthcountry],
					[workstatusid],
					[occupation],
					[ethnicityid],
					[otherethnicity],
					[outdooroccupation],
					[livetropical],
					[eversmoked],
					[eversmokednumbercigsperday],
					[agestart],
					[agestop],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drnkbeeravg],
					[drnkwineavg],
					[drnkspiritsavg],
					[drinkalcohol],
					[drnkunitsavg],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[systolic],
					[diastolic],
					[height],
					[weight],
					[waist],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[weightMissing],
					[waistMissing],
					[smokingMissing],
					[drinkingMissing]
				FROM
					[dbo].[bbPatientLifestyle]
				WHERE
					[ethnicityid] = @Ethnicityid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_GetByEthnicityid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_GetByWorkstatusid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_GetByWorkstatusid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_GetByWorkstatusid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientLifestyle table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_GetByWorkstatusid
(

	@Workstatusid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FupId],
					[birthtown],
					[birthcountry],
					[workstatusid],
					[occupation],
					[ethnicityid],
					[otherethnicity],
					[outdooroccupation],
					[livetropical],
					[eversmoked],
					[eversmokednumbercigsperday],
					[agestart],
					[agestop],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drnkbeeravg],
					[drnkwineavg],
					[drnkspiritsavg],
					[drinkalcohol],
					[drnkunitsavg],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[systolic],
					[diastolic],
					[height],
					[weight],
					[waist],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[weightMissing],
					[waistMissing],
					[smokingMissing],
					[drinkingMissing]
				FROM
					[dbo].[bbPatientLifestyle]
				WHERE
					[workstatusid] = @Workstatusid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_GetByWorkstatusid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_GetByFupId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_GetByFupId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_GetByFupId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientLifestyle table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_GetByFupId
(

	@FupId int   
)
AS


				SELECT
					[FupId],
					[birthtown],
					[birthcountry],
					[workstatusid],
					[occupation],
					[ethnicityid],
					[otherethnicity],
					[outdooroccupation],
					[livetropical],
					[eversmoked],
					[eversmokednumbercigsperday],
					[agestart],
					[agestop],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drnkbeeravg],
					[drnkwineavg],
					[drnkspiritsavg],
					[drinkalcohol],
					[drnkunitsavg],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[systolic],
					[diastolic],
					[height],
					[weight],
					[waist],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[weightMissing],
					[waistMissing],
					[smokingMissing],
					[drinkingMissing]
				FROM
					[dbo].[bbPatientLifestyle]
				WHERE
					[FupId] = @FupId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_GetByFupId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientLifestyle_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientLifestyle_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientLifestyle_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientLifestyle table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientLifestyle_Find
(

	@SearchUsingOR bit   = null ,

	@FupId int   = null ,

	@Birthtown varchar (256)  = null ,

	@Birthcountry varchar (256)  = null ,

	@Workstatusid int   = null ,

	@Occupation varchar (50)  = null ,

	@Ethnicityid int   = null ,

	@Otherethnicity varchar (50)  = null ,

	@Outdooroccupation bit   = null ,

	@Livetropical bit   = null ,

	@Eversmoked bit   = null ,

	@Eversmokednumbercigsperday int   = null ,

	@Agestart int   = null ,

	@Agestop int   = null ,

	@Currentlysmoke bit   = null ,

	@Currentlysmokenumbercigsperday int   = null ,

	@Drnkbeeravg int   = null ,

	@Drnkwineavg int   = null ,

	@Drnkspiritsavg int   = null ,

	@Drinkalcohol bit   = null ,

	@Drnkunitsavg int   = null ,

	@Admittedtohospital int   = null ,

	@Newdrugs int   = null ,

	@Newclinics int   = null ,

	@Systolic float   = null ,

	@Diastolic float   = null ,

	@Height float   = null ,

	@Weight float   = null ,

	@Waist float   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@WeightMissing bit   = null ,

	@WaistMissing bit   = null ,

	@SmokingMissing bit   = null ,

	@DrinkingMissing bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FupId]
	, [birthtown]
	, [birthcountry]
	, [workstatusid]
	, [occupation]
	, [ethnicityid]
	, [otherethnicity]
	, [outdooroccupation]
	, [livetropical]
	, [eversmoked]
	, [eversmokednumbercigsperday]
	, [agestart]
	, [agestop]
	, [currentlysmoke]
	, [currentlysmokenumbercigsperday]
	, [drnkbeeravg]
	, [drnkwineavg]
	, [drnkspiritsavg]
	, [drinkalcohol]
	, [drnkunitsavg]
	, [admittedtohospital]
	, [newdrugs]
	, [newclinics]
	, [systolic]
	, [diastolic]
	, [height]
	, [weight]
	, [waist]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [weightMissing]
	, [waistMissing]
	, [smokingMissing]
	, [drinkingMissing]
    FROM
	[dbo].[bbPatientLifestyle]
    WHERE 
	 ([FupId] = @FupId OR @FupId IS NULL)
	AND ([birthtown] = @Birthtown OR @Birthtown IS NULL)
	AND ([birthcountry] = @Birthcountry OR @Birthcountry IS NULL)
	AND ([workstatusid] = @Workstatusid OR @Workstatusid IS NULL)
	AND ([occupation] = @Occupation OR @Occupation IS NULL)
	AND ([ethnicityid] = @Ethnicityid OR @Ethnicityid IS NULL)
	AND ([otherethnicity] = @Otherethnicity OR @Otherethnicity IS NULL)
	AND ([outdooroccupation] = @Outdooroccupation OR @Outdooroccupation IS NULL)
	AND ([livetropical] = @Livetropical OR @Livetropical IS NULL)
	AND ([eversmoked] = @Eversmoked OR @Eversmoked IS NULL)
	AND ([eversmokednumbercigsperday] = @Eversmokednumbercigsperday OR @Eversmokednumbercigsperday IS NULL)
	AND ([agestart] = @Agestart OR @Agestart IS NULL)
	AND ([agestop] = @Agestop OR @Agestop IS NULL)
	AND ([currentlysmoke] = @Currentlysmoke OR @Currentlysmoke IS NULL)
	AND ([currentlysmokenumbercigsperday] = @Currentlysmokenumbercigsperday OR @Currentlysmokenumbercigsperday IS NULL)
	AND ([drnkbeeravg] = @Drnkbeeravg OR @Drnkbeeravg IS NULL)
	AND ([drnkwineavg] = @Drnkwineavg OR @Drnkwineavg IS NULL)
	AND ([drnkspiritsavg] = @Drnkspiritsavg OR @Drnkspiritsavg IS NULL)
	AND ([drinkalcohol] = @Drinkalcohol OR @Drinkalcohol IS NULL)
	AND ([drnkunitsavg] = @Drnkunitsavg OR @Drnkunitsavg IS NULL)
	AND ([admittedtohospital] = @Admittedtohospital OR @Admittedtohospital IS NULL)
	AND ([newdrugs] = @Newdrugs OR @Newdrugs IS NULL)
	AND ([newclinics] = @Newclinics OR @Newclinics IS NULL)
	AND ([systolic] = @Systolic OR @Systolic IS NULL)
	AND ([diastolic] = @Diastolic OR @Diastolic IS NULL)
	AND ([height] = @Height OR @Height IS NULL)
	AND ([weight] = @Weight OR @Weight IS NULL)
	AND ([waist] = @Waist OR @Waist IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([weightMissing] = @WeightMissing OR @WeightMissing IS NULL)
	AND ([waistMissing] = @WaistMissing OR @WaistMissing IS NULL)
	AND ([smokingMissing] = @SmokingMissing OR @SmokingMissing IS NULL)
	AND ([drinkingMissing] = @DrinkingMissing OR @DrinkingMissing IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [FupId]
	, [birthtown]
	, [birthcountry]
	, [workstatusid]
	, [occupation]
	, [ethnicityid]
	, [otherethnicity]
	, [outdooroccupation]
	, [livetropical]
	, [eversmoked]
	, [eversmokednumbercigsperday]
	, [agestart]
	, [agestop]
	, [currentlysmoke]
	, [currentlysmokenumbercigsperday]
	, [drnkbeeravg]
	, [drnkwineavg]
	, [drnkspiritsavg]
	, [drinkalcohol]
	, [drnkunitsavg]
	, [admittedtohospital]
	, [newdrugs]
	, [newclinics]
	, [systolic]
	, [diastolic]
	, [height]
	, [weight]
	, [waist]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [weightMissing]
	, [waistMissing]
	, [smokingMissing]
	, [drinkingMissing]
    FROM
	[dbo].[bbPatientLifestyle]
    WHERE 
	 ([FupId] = @FupId AND @FupId is not null)
	OR ([birthtown] = @Birthtown AND @Birthtown is not null)
	OR ([birthcountry] = @Birthcountry AND @Birthcountry is not null)
	OR ([workstatusid] = @Workstatusid AND @Workstatusid is not null)
	OR ([occupation] = @Occupation AND @Occupation is not null)
	OR ([ethnicityid] = @Ethnicityid AND @Ethnicityid is not null)
	OR ([otherethnicity] = @Otherethnicity AND @Otherethnicity is not null)
	OR ([outdooroccupation] = @Outdooroccupation AND @Outdooroccupation is not null)
	OR ([livetropical] = @Livetropical AND @Livetropical is not null)
	OR ([eversmoked] = @Eversmoked AND @Eversmoked is not null)
	OR ([eversmokednumbercigsperday] = @Eversmokednumbercigsperday AND @Eversmokednumbercigsperday is not null)
	OR ([agestart] = @Agestart AND @Agestart is not null)
	OR ([agestop] = @Agestop AND @Agestop is not null)
	OR ([currentlysmoke] = @Currentlysmoke AND @Currentlysmoke is not null)
	OR ([currentlysmokenumbercigsperday] = @Currentlysmokenumbercigsperday AND @Currentlysmokenumbercigsperday is not null)
	OR ([drnkbeeravg] = @Drnkbeeravg AND @Drnkbeeravg is not null)
	OR ([drnkwineavg] = @Drnkwineavg AND @Drnkwineavg is not null)
	OR ([drnkspiritsavg] = @Drnkspiritsavg AND @Drnkspiritsavg is not null)
	OR ([drinkalcohol] = @Drinkalcohol AND @Drinkalcohol is not null)
	OR ([drnkunitsavg] = @Drnkunitsavg AND @Drnkunitsavg is not null)
	OR ([admittedtohospital] = @Admittedtohospital AND @Admittedtohospital is not null)
	OR ([newdrugs] = @Newdrugs AND @Newdrugs is not null)
	OR ([newclinics] = @Newclinics AND @Newclinics is not null)
	OR ([systolic] = @Systolic AND @Systolic is not null)
	OR ([diastolic] = @Diastolic AND @Diastolic is not null)
	OR ([height] = @Height AND @Height is not null)
	OR ([weight] = @Weight AND @Weight is not null)
	OR ([waist] = @Waist AND @Waist is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([weightMissing] = @WeightMissing AND @WeightMissing is not null)
	OR ([waistMissing] = @WaistMissing AND @WaistMissing is not null)
	OR ([smokingMissing] = @SmokingMissing AND @SmokingMissing is not null)
	OR ([drinkingMissing] = @DrinkingMissing AND @DrinkingMissing is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientLifestyle_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbQueryForCentre table
-- Table Comment: This table stores all queries, their related flags, subject and linked ids to other tables. Adverse Events are linked by a many to many link in a separate Link table.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_Get_List

AS


				
				SELECT
					[QId],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest]
				FROM
					[dbo].[bbQueryForCentre]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbQueryForCentre table passing page index and page count parameters
-- Table Comment: This table stores all queries, their related flags, subject and linked ids to other tables. Adverse Events are linked by a many to many link in a separate Link table.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [QId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([QId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [QId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryForCentre]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[QId], O.[centreid], O.[messagecount], O.[adminunread], O.[clinicianunread], O.[QueryTypeId], O.[QueryStatusId], O.[include], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[subject], O.[secondRequest]
				FROM
				    [dbo].[bbQueryForCentre] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[QId] = PageIndex.[QId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryForCentre]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbQueryForCentre table
-- Table Comment: This table stores all queries, their related flags, subject and linked ids to other tables. Adverse Events are linked by a many to many link in a separate Link table.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_Insert
(

	@Qid int    OUTPUT,

	@Centreid int   ,

	@Messagecount int   ,

	@Adminunread bit   ,

	@Clinicianunread bit   ,

	@QueryTypeId int   ,

	@QueryStatusId int   ,

	@Include bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Subject varchar (MAX)  ,

	@SecondRequest bit   
)
AS


				
				INSERT INTO [dbo].[bbQueryForCentre]
					(
					[centreid]
					,[messagecount]
					,[adminunread]
					,[clinicianunread]
					,[QueryTypeId]
					,[QueryStatusId]
					,[include]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[subject]
					,[secondRequest]
					)
				VALUES
					(
					@Centreid
					,@Messagecount
					,@Adminunread
					,@Clinicianunread
					,@QueryTypeId
					,@QueryStatusId
					,@Include
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@Subject
					,@SecondRequest
					)
				-- Get the identity value
				SET @Qid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbQueryForCentre table
-- Table Comment: This table stores all queries, their related flags, subject and linked ids to other tables. Adverse Events are linked by a many to many link in a separate Link table.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_Update
(

	@Qid int   ,

	@Centreid int   ,

	@Messagecount int   ,

	@Adminunread bit   ,

	@Clinicianunread bit   ,

	@QueryTypeId int   ,

	@QueryStatusId int   ,

	@Include bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Subject varchar (MAX)  ,

	@SecondRequest bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbQueryForCentre]
				SET
					[centreid] = @Centreid
					,[messagecount] = @Messagecount
					,[adminunread] = @Adminunread
					,[clinicianunread] = @Clinicianunread
					,[QueryTypeId] = @QueryTypeId
					,[QueryStatusId] = @QueryStatusId
					,[include] = @Include
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[subject] = @Subject
					,[secondRequest] = @SecondRequest
				WHERE
[QId] = @Qid 
				
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbQueryForCentre table
-- Table Comment: This table stores all queries, their related flags, subject and linked ids to other tables. Adverse Events are linked by a many to many link in a separate Link table.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_Delete
(

	@Qid int   
)
AS


				DELETE FROM [dbo].[bbQueryForCentre] WITH (ROWLOCK) 
				WHERE
					[QId] = @Qid
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_GetByCentreid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_GetByCentreid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_GetByCentreid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryForCentre table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_GetByCentreid
(

	@Centreid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[QId],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest]
				FROM
					[dbo].[bbQueryForCentre]
				WHERE
					[centreid] = @Centreid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_GetByCentreid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_GetByQueryStatusId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_GetByQueryStatusId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_GetByQueryStatusId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryForCentre table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_GetByQueryStatusId
(

	@QueryStatusId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[QId],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest]
				FROM
					[dbo].[bbQueryForCentre]
				WHERE
					[QueryStatusId] = @QueryStatusId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_GetByQueryStatusId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_GetByQueryTypeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_GetByQueryTypeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_GetByQueryTypeId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryForCentre table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_GetByQueryTypeId
(

	@QueryTypeId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[QId],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest]
				FROM
					[dbo].[bbQueryForCentre]
				WHERE
					[QueryTypeId] = @QueryTypeId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_GetByQueryTypeId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_GetByQid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_GetByQid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_GetByQid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryForCentre table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_GetByQid
(

	@Qid int   
)
AS


				SELECT
					[QId],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest]
				FROM
					[dbo].[bbQueryForCentre]
				WHERE
					[QId] = @Qid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_GetByQid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentre_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentre_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentre_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbQueryForCentre table passing nullable parameters
-- Table Comment: This table stores all queries, their related flags, subject and linked ids to other tables. Adverse Events are linked by a many to many link in a separate Link table.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentre_Find
(

	@SearchUsingOR bit   = null ,

	@Qid int   = null ,

	@Centreid int   = null ,

	@Messagecount int   = null ,

	@Adminunread bit   = null ,

	@Clinicianunread bit   = null ,

	@QueryTypeId int   = null ,

	@QueryStatusId int   = null ,

	@Include bit   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@Subject varchar (MAX)  = null ,

	@SecondRequest bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [QId]
	, [centreid]
	, [messagecount]
	, [adminunread]
	, [clinicianunread]
	, [QueryTypeId]
	, [QueryStatusId]
	, [include]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [subject]
	, [secondRequest]
    FROM
	[dbo].[bbQueryForCentre]
    WHERE 
	 ([QId] = @Qid OR @Qid IS NULL)
	AND ([centreid] = @Centreid OR @Centreid IS NULL)
	AND ([messagecount] = @Messagecount OR @Messagecount IS NULL)
	AND ([adminunread] = @Adminunread OR @Adminunread IS NULL)
	AND ([clinicianunread] = @Clinicianunread OR @Clinicianunread IS NULL)
	AND ([QueryTypeId] = @QueryTypeId OR @QueryTypeId IS NULL)
	AND ([QueryStatusId] = @QueryStatusId OR @QueryStatusId IS NULL)
	AND ([include] = @Include OR @Include IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([subject] = @Subject OR @Subject IS NULL)
	AND ([secondRequest] = @SecondRequest OR @SecondRequest IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [QId]
	, [centreid]
	, [messagecount]
	, [adminunread]
	, [clinicianunread]
	, [QueryTypeId]
	, [QueryStatusId]
	, [include]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [subject]
	, [secondRequest]
    FROM
	[dbo].[bbQueryForCentre]
    WHERE 
	 ([QId] = @Qid AND @Qid is not null)
	OR ([centreid] = @Centreid AND @Centreid is not null)
	OR ([messagecount] = @Messagecount AND @Messagecount is not null)
	OR ([adminunread] = @Adminunread AND @Adminunread is not null)
	OR ([clinicianunread] = @Clinicianunread AND @Clinicianunread is not null)
	OR ([QueryTypeId] = @QueryTypeId AND @QueryTypeId is not null)
	OR ([QueryStatusId] = @QueryStatusId AND @QueryStatusId is not null)
	OR ([include] = @Include AND @Include is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([subject] = @Subject AND @Subject is not null)
	OR ([secondRequest] = @SecondRequest AND @SecondRequest is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbQueryForCentre_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatuslkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatuslkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatuslkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatuslkp_Get_List

AS


				
				SELECT
					[pstatusid],
					[pstatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[isValidStatus],
					[isNotLiveStatus],
					[isNotTrainingStatus],
					[showInClinicianList]
				FROM
					[dbo].[bbPatientStatuslkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatuslkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatuslkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatuslkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatuslkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientStatuslkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatuslkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [pstatusid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([pstatusid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [pstatusid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[pstatusid], O.[pstatus], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[isValidStatus], O.[isNotLiveStatus], O.[isNotTrainingStatus], O.[showInClinicianList]
				FROM
				    [dbo].[bbPatientStatuslkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[pstatusid] = PageIndex.[pstatusid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatuslkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatuslkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatuslkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatuslkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatuslkp_Insert
(

	@Pstatusid int    OUTPUT,

	@Pstatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@IsValidStatus int   ,

	@IsNotLiveStatus int   ,

	@IsNotTrainingStatus int   ,

	@ShowInClinicianList int   
)
AS


				
				INSERT INTO [dbo].[bbPatientStatuslkp]
					(
					[pstatus]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[isValidStatus]
					,[isNotLiveStatus]
					,[isNotTrainingStatus]
					,[showInClinicianList]
					)
				VALUES
					(
					@Pstatus
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@IsValidStatus
					,@IsNotLiveStatus
					,@IsNotTrainingStatus
					,@ShowInClinicianList
					)
				-- Get the identity value
				SET @Pstatusid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatuslkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatuslkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatuslkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatuslkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatuslkp_Update
(

	@Pstatusid int   ,

	@Pstatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@IsValidStatus int   ,

	@IsNotLiveStatus int   ,

	@IsNotTrainingStatus int   ,

	@ShowInClinicianList int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientStatuslkp]
				SET
					[pstatus] = @Pstatus
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[isValidStatus] = @IsValidStatus
					,[isNotLiveStatus] = @IsNotLiveStatus
					,[isNotTrainingStatus] = @IsNotTrainingStatus
					,[showInClinicianList] = @ShowInClinicianList
				WHERE
[pstatusid] = @Pstatusid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatuslkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatuslkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatuslkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatuslkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatuslkp_Delete
(

	@Pstatusid int   
)
AS


				DELETE FROM [dbo].[bbPatientStatuslkp] WITH (ROWLOCK) 
				WHERE
					[pstatusid] = @Pstatusid
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatuslkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatuslkp_GetByPstatusid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatuslkp_GetByPstatusid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatuslkp_GetByPstatusid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientStatuslkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatuslkp_GetByPstatusid
(

	@Pstatusid int   
)
AS


				SELECT
					[pstatusid],
					[pstatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[isValidStatus],
					[isNotLiveStatus],
					[isNotTrainingStatus],
					[showInClinicianList]
				FROM
					[dbo].[bbPatientStatuslkp]
				WHERE
					[pstatusid] = @Pstatusid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientStatuslkp_GetByPstatusid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientStatuslkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientStatuslkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientStatuslkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientStatuslkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientStatuslkp_Find
(

	@SearchUsingOR bit   = null ,

	@Pstatusid int   = null ,

	@Pstatus varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@IsValidStatus int   = null ,

	@IsNotLiveStatus int   = null ,

	@IsNotTrainingStatus int   = null ,

	@ShowInClinicianList int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [pstatusid]
	, [pstatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [isValidStatus]
	, [isNotLiveStatus]
	, [isNotTrainingStatus]
	, [showInClinicianList]
    FROM
	[dbo].[bbPatientStatuslkp]
    WHERE 
	 ([pstatusid] = @Pstatusid OR @Pstatusid IS NULL)
	AND ([pstatus] = @Pstatus OR @Pstatus IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([isValidStatus] = @IsValidStatus OR @IsValidStatus IS NULL)
	AND ([isNotLiveStatus] = @IsNotLiveStatus OR @IsNotLiveStatus IS NULL)
	AND ([isNotTrainingStatus] = @IsNotTrainingStatus OR @IsNotTrainingStatus IS NULL)
	AND ([showInClinicianList] = @ShowInClinicianList OR @ShowInClinicianList IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [pstatusid]
	, [pstatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [isValidStatus]
	, [isNotLiveStatus]
	, [isNotTrainingStatus]
	, [showInClinicianList]
    FROM
	[dbo].[bbPatientStatuslkp]
    WHERE 
	 ([pstatusid] = @Pstatusid AND @Pstatusid is not null)
	OR ([pstatus] = @Pstatus AND @Pstatus is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([isValidStatus] = @IsValidStatus AND @IsValidStatus is not null)
	OR ([isNotLiveStatus] = @IsNotLiveStatus AND @IsNotLiveStatus is not null)
	OR ([isNotTrainingStatus] = @IsNotTrainingStatus AND @IsNotTrainingStatus is not null)
	OR ([showInClinicianList] = @ShowInClinicianList AND @ShowInClinicianList is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientStatuslkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbQueryForCentreMessage table
-- Table Comment: This table contains all responses to queries. A query can have multiple responses.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_Get_List

AS


				
				SELECT
					[QMId],
					[QId],
					[message],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryForCentreMessage]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbQueryForCentreMessage table passing page index and page count parameters
-- Table Comment: This table contains all responses to queries. A query can have multiple responses.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [QMId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([QMId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [QMId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryForCentreMessage]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[QMId], O.[QId], O.[message], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbQueryForCentreMessage] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[QMId] = PageIndex.[QMId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryForCentreMessage]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbQueryForCentreMessage table
-- Table Comment: This table contains all responses to queries. A query can have multiple responses.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_Insert
(

	@QmId int    OUTPUT,

	@Qid int   ,

	@Message varchar (MAX)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbQueryForCentreMessage]
					(
					[QId]
					,[message]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Qid
					,@Message
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @QmId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbQueryForCentreMessage table
-- Table Comment: This table contains all responses to queries. A query can have multiple responses.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_Update
(

	@QmId int   ,

	@Qid int   ,

	@Message varchar (MAX)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbQueryForCentreMessage]
				SET
					[QId] = @Qid
					,[message] = @Message
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[QMId] = @QmId 
				
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbQueryForCentreMessage table
-- Table Comment: This table contains all responses to queries. A query can have multiple responses.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_Delete
(

	@QmId int   
)
AS


				DELETE FROM [dbo].[bbQueryForCentreMessage] WITH (ROWLOCK) 
				WHERE
					[QMId] = @QmId
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_GetByQid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_GetByQid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_GetByQid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryForCentreMessage table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_GetByQid
(

	@Qid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[QMId],
					[QId],
					[message],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryForCentreMessage]
				WHERE
					[QId] = @Qid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_GetByQid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_GetByQmId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_GetByQmId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_GetByQmId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryForCentreMessage table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_GetByQmId
(

	@QmId int   
)
AS


				SELECT
					[QMId],
					[QId],
					[message],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryForCentreMessage]
				WHERE
					[QMId] = @QmId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_GetByQmId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryForCentreMessage_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryForCentreMessage_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryForCentreMessage_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbQueryForCentreMessage table passing nullable parameters
-- Table Comment: This table contains all responses to queries. A query can have multiple responses.
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryForCentreMessage_Find
(

	@SearchUsingOR bit   = null ,

	@QmId int   = null ,

	@Qid int   = null ,

	@Message varchar (MAX)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [QMId]
	, [QId]
	, [message]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbQueryForCentreMessage]
    WHERE 
	 ([QMId] = @QmId OR @QmId IS NULL)
	AND ([QId] = @Qid OR @Qid IS NULL)
	AND ([message] = @Message OR @Message IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [QMId]
	, [QId]
	, [message]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbQueryForCentreMessage]
    WHERE 
	 ([QMId] = @QmId AND @QmId is not null)
	OR ([QId] = @Qid AND @Qid is not null)
	OR ([message] = @Message AND @Message is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbQueryForCentreMessage_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbGenderlkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbGenderlkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbGenderlkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbGenderlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbGenderlkp_Get_List

AS


				
				SELECT
					[genderid],
					[gender],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbGenderlkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbGenderlkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbGenderlkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbGenderlkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbGenderlkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbGenderlkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbGenderlkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [genderid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([genderid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [genderid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbGenderlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[genderid], O.[gender], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbGenderlkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[genderid] = PageIndex.[genderid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbGenderlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbGenderlkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbGenderlkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbGenderlkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbGenderlkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbGenderlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbGenderlkp_Insert
(

	@Genderid int    OUTPUT,

	@Gender varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbGenderlkp]
					(
					[gender]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Gender
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Genderid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbGenderlkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbGenderlkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbGenderlkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbGenderlkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbGenderlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbGenderlkp_Update
(

	@Genderid int   ,

	@Gender varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbGenderlkp]
				SET
					[gender] = @Gender
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[genderid] = @Genderid 
				
			

GO
GRANT EXEC ON dbo.znt_bbGenderlkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbGenderlkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbGenderlkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbGenderlkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbGenderlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbGenderlkp_Delete
(

	@Genderid int   
)
AS


				DELETE FROM [dbo].[bbGenderlkp] WITH (ROWLOCK) 
				WHERE
					[genderid] = @Genderid
					
			

GO
GRANT EXEC ON dbo.znt_bbGenderlkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbGenderlkp_GetByGenderid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbGenderlkp_GetByGenderid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbGenderlkp_GetByGenderid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbGenderlkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbGenderlkp_GetByGenderid
(

	@Genderid int   
)
AS


				SELECT
					[genderid],
					[gender],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbGenderlkp]
				WHERE
					[genderid] = @Genderid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbGenderlkp_GetByGenderid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbGenderlkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbGenderlkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbGenderlkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbGenderlkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbGenderlkp_Find
(

	@SearchUsingOR bit   = null ,

	@Genderid int   = null ,

	@Gender varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [genderid]
	, [gender]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbGenderlkp]
    WHERE 
	 ([genderid] = @Genderid OR @Genderid IS NULL)
	AND ([gender] = @Gender OR @Gender IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [genderid]
	, [gender]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbGenderlkp]
    WHERE 
	 ([genderid] = @Genderid AND @Genderid is not null)
	OR ([gender] = @Gender AND @Gender is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbGenderlkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientCohortHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_Get_List

AS


				
				SELECT
					[chid],
					[patientid],
					[studyno],
					[cohortid],
					[datefrom],
					[dateto],
					[datetoFUP],
					[regcentreid],
					[consentversion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortHistory]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientCohortHistory table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [chid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([chid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [chid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortHistory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[chid], O.[patientid], O.[studyno], O.[cohortid], O.[datefrom], O.[dateto], O.[datetoFUP], O.[regcentreid], O.[consentversion], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbPatientCohortHistory] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[chid] = PageIndex.[chid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortHistory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientCohortHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_Insert
(

	@Chid int    OUTPUT,

	@Patientid int   ,

	@Studyno int   ,

	@Cohortid int   ,

	@Datefrom datetime   ,

	@Dateto datetime   ,

	@DatetoFup int   ,

	@Regcentreid int   ,

	@Consentversion varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPatientCohortHistory]
					(
					[patientid]
					,[studyno]
					,[cohortid]
					,[datefrom]
					,[dateto]
					,[datetoFUP]
					,[regcentreid]
					,[consentversion]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Patientid
					,@Studyno
					,@Cohortid
					,@Datefrom
					,@Dateto
					,@DatetoFup
					,@Regcentreid
					,@Consentversion
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Chid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientCohortHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_Update
(

	@Chid int   ,

	@Patientid int   ,

	@Studyno int   ,

	@Cohortid int   ,

	@Datefrom datetime   ,

	@Dateto datetime   ,

	@DatetoFup int   ,

	@Regcentreid int   ,

	@Consentversion varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientCohortHistory]
				SET
					[patientid] = @Patientid
					,[studyno] = @Studyno
					,[cohortid] = @Cohortid
					,[datefrom] = @Datefrom
					,[dateto] = @Dateto
					,[datetoFUP] = @DatetoFup
					,[regcentreid] = @Regcentreid
					,[consentversion] = @Consentversion
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[chid] = @Chid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientCohortHistory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_Delete
(

	@Chid int   
)
AS


				DELETE FROM [dbo].[bbPatientCohortHistory] WITH (ROWLOCK) 
				WHERE
					[chid] = @Chid
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_GetByCohortid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_GetByCohortid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_GetByCohortid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortHistory table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_GetByCohortid
(

	@Cohortid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[chid],
					[patientid],
					[studyno],
					[cohortid],
					[datefrom],
					[dateto],
					[datetoFUP],
					[regcentreid],
					[consentversion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortHistory]
				WHERE
					[cohortid] = @Cohortid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_GetByCohortid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_GetByPatientid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_GetByPatientid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_GetByPatientid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortHistory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_GetByPatientid
(

	@Patientid int   
)
AS


				SELECT
					[chid],
					[patientid],
					[studyno],
					[cohortid],
					[datefrom],
					[dateto],
					[datetoFUP],
					[regcentreid],
					[consentversion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortHistory]
				WHERE
					[patientid] = @Patientid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_GetByPatientid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_GetByRegcentreid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_GetByRegcentreid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_GetByRegcentreid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortHistory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_GetByRegcentreid
(

	@Regcentreid int   
)
AS


				SELECT
					[chid],
					[patientid],
					[studyno],
					[cohortid],
					[datefrom],
					[dateto],
					[datetoFUP],
					[regcentreid],
					[consentversion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortHistory]
				WHERE
					[regcentreid] = @Regcentreid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_GetByRegcentreid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_GetByStudyno procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_GetByStudyno') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_GetByStudyno
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortHistory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_GetByStudyno
(

	@Studyno int   
)
AS


				SELECT
					[chid],
					[patientid],
					[studyno],
					[cohortid],
					[datefrom],
					[dateto],
					[datetoFUP],
					[regcentreid],
					[consentversion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortHistory]
				WHERE
					[studyno] = @Studyno
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_GetByStudyno TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_GetByChid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_GetByChid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_GetByChid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortHistory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_GetByChid
(

	@Chid int   
)
AS


				SELECT
					[chid],
					[patientid],
					[studyno],
					[cohortid],
					[datefrom],
					[dateto],
					[datetoFUP],
					[regcentreid],
					[consentversion],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortHistory]
				WHERE
					[chid] = @Chid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_GetByChid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortHistory_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortHistory_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortHistory_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientCohortHistory table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortHistory_Find
(

	@SearchUsingOR bit   = null ,

	@Chid int   = null ,

	@Patientid int   = null ,

	@Studyno int   = null ,

	@Cohortid int   = null ,

	@Datefrom datetime   = null ,

	@Dateto datetime   = null ,

	@DatetoFup int   = null ,

	@Regcentreid int   = null ,

	@Consentversion varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [chid]
	, [patientid]
	, [studyno]
	, [cohortid]
	, [datefrom]
	, [dateto]
	, [datetoFUP]
	, [regcentreid]
	, [consentversion]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientCohortHistory]
    WHERE 
	 ([chid] = @Chid OR @Chid IS NULL)
	AND ([patientid] = @Patientid OR @Patientid IS NULL)
	AND ([studyno] = @Studyno OR @Studyno IS NULL)
	AND ([cohortid] = @Cohortid OR @Cohortid IS NULL)
	AND ([datefrom] = @Datefrom OR @Datefrom IS NULL)
	AND ([dateto] = @Dateto OR @Dateto IS NULL)
	AND ([datetoFUP] = @DatetoFup OR @DatetoFup IS NULL)
	AND ([regcentreid] = @Regcentreid OR @Regcentreid IS NULL)
	AND ([consentversion] = @Consentversion OR @Consentversion IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [chid]
	, [patientid]
	, [studyno]
	, [cohortid]
	, [datefrom]
	, [dateto]
	, [datetoFUP]
	, [regcentreid]
	, [consentversion]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientCohortHistory]
    WHERE 
	 ([chid] = @Chid AND @Chid is not null)
	OR ([patientid] = @Patientid AND @Patientid is not null)
	OR ([studyno] = @Studyno AND @Studyno is not null)
	OR ([cohortid] = @Cohortid AND @Cohortid is not null)
	OR ([datefrom] = @Datefrom AND @Datefrom is not null)
	OR ([dateto] = @Dateto AND @Dateto is not null)
	OR ([datetoFUP] = @DatetoFup AND @DatetoFup is not null)
	OR ([regcentreid] = @Regcentreid AND @Regcentreid is not null)
	OR ([consentversion] = @Consentversion AND @Consentversion is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientCohortHistory_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbQuery table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_Get_List

AS


				
				SELECT
					[QId],
					[chid],
					[fupnumber],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest],
					[isAuditQuery]
				FROM
					[dbo].[bbQuery]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbQuery_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbQuery table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [QId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([QId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [QId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbQuery]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[QId], O.[chid], O.[fupnumber], O.[centreid], O.[messagecount], O.[adminunread], O.[clinicianunread], O.[QueryTypeId], O.[QueryStatusId], O.[include], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[subject], O.[secondRequest], O.[isAuditQuery]
				FROM
				    [dbo].[bbQuery] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[QId] = PageIndex.[QId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbQuery]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbQuery_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbQuery table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_Insert
(

	@Qid int    OUTPUT,

	@Chid int   ,

	@Fupnumber int   ,

	@Centreid int   ,

	@Messagecount int   ,

	@Adminunread bit   ,

	@Clinicianunread bit   ,

	@QueryTypeId int   ,

	@QueryStatusId int   ,

	@Include bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Subject varchar (MAX)  ,

	@SecondRequest bit   ,

	@IsAuditQuery bit   
)
AS


				
				INSERT INTO [dbo].[bbQuery]
					(
					[chid]
					,[fupnumber]
					,[centreid]
					,[messagecount]
					,[adminunread]
					,[clinicianunread]
					,[QueryTypeId]
					,[QueryStatusId]
					,[include]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[subject]
					,[secondRequest]
					,[isAuditQuery]
					)
				VALUES
					(
					@Chid
					,@Fupnumber
					,@Centreid
					,@Messagecount
					,@Adminunread
					,@Clinicianunread
					,@QueryTypeId
					,@QueryStatusId
					,@Include
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@Subject
					,@SecondRequest
					,@IsAuditQuery
					)
				-- Get the identity value
				SET @Qid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbQuery_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbQuery table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_Update
(

	@Qid int   ,

	@Chid int   ,

	@Fupnumber int   ,

	@Centreid int   ,

	@Messagecount int   ,

	@Adminunread bit   ,

	@Clinicianunread bit   ,

	@QueryTypeId int   ,

	@QueryStatusId int   ,

	@Include bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Subject varchar (MAX)  ,

	@SecondRequest bit   ,

	@IsAuditQuery bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbQuery]
				SET
					[chid] = @Chid
					,[fupnumber] = @Fupnumber
					,[centreid] = @Centreid
					,[messagecount] = @Messagecount
					,[adminunread] = @Adminunread
					,[clinicianunread] = @Clinicianunread
					,[QueryTypeId] = @QueryTypeId
					,[QueryStatusId] = @QueryStatusId
					,[include] = @Include
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[subject] = @Subject
					,[secondRequest] = @SecondRequest
					,[isAuditQuery] = @IsAuditQuery
				WHERE
[QId] = @Qid 
				
			

GO
GRANT EXEC ON dbo.znt_bbQuery_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbQuery table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_Delete
(

	@Qid int   
)
AS


				DELETE FROM [dbo].[bbQuery] WITH (ROWLOCK) 
				WHERE
					[QId] = @Qid
					
			

GO
GRANT EXEC ON dbo.znt_bbQuery_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetByCentreid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetByCentreid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetByCentreid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQuery table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetByCentreid
(

	@Centreid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[QId],
					[chid],
					[fupnumber],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest],
					[isAuditQuery]
				FROM
					[dbo].[bbQuery]
				WHERE
					[centreid] = @Centreid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbQuery_GetByCentreid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetByChid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetByChid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetByChid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQuery table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetByChid
(

	@Chid int   
)
AS


				SELECT
					[QId],
					[chid],
					[fupnumber],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest],
					[isAuditQuery]
				FROM
					[dbo].[bbQuery]
				WHERE
					[chid] = @Chid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQuery_GetByChid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetByChidFupnumber procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetByChidFupnumber') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetByChidFupnumber
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQuery table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetByChidFupnumber
(

	@Chid int   ,

	@Fupnumber int   
)
AS


				SELECT
					[QId],
					[chid],
					[fupnumber],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest],
					[isAuditQuery]
				FROM
					[dbo].[bbQuery]
				WHERE
					[chid] = @Chid
					AND [fupnumber] = @Fupnumber
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQuery_GetByChidFupnumber TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetByQueryStatusId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetByQueryStatusId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetByQueryStatusId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQuery table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetByQueryStatusId
(

	@QueryStatusId int   
)
AS


				SELECT
					[QId],
					[chid],
					[fupnumber],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest],
					[isAuditQuery]
				FROM
					[dbo].[bbQuery]
				WHERE
					[QueryStatusId] = @QueryStatusId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQuery_GetByQueryStatusId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetByQueryTypeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetByQueryTypeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetByQueryTypeId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQuery table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetByQueryTypeId
(

	@QueryTypeId int   
)
AS


				SELECT
					[QId],
					[chid],
					[fupnumber],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest],
					[isAuditQuery]
				FROM
					[dbo].[bbQuery]
				WHERE
					[QueryTypeId] = @QueryTypeId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQuery_GetByQueryTypeId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetByQid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetByQid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetByQid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQuery table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetByQid
(

	@Qid int   
)
AS


				SELECT
					[QId],
					[chid],
					[fupnumber],
					[centreid],
					[messagecount],
					[adminunread],
					[clinicianunread],
					[QueryTypeId],
					[QueryStatusId],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subject],
					[secondRequest],
					[isAuditQuery]
				FROM
					[dbo].[bbQuery]
				WHERE
					[QId] = @Qid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQuery_GetByQid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_GetByFupaeidFromBbAdverseEventQueryLink procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_GetByFupaeidFromBbAdverseEventQueryLink') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_GetByFupaeidFromBbAdverseEventQueryLink
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records through a junction table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_GetByFupaeidFromBbAdverseEventQueryLink
(

	@Fupaeid int   
)
AS


SELECT dbo.[bbQuery].[QId]
       ,dbo.[bbQuery].[chid]
       ,dbo.[bbQuery].[fupnumber]
       ,dbo.[bbQuery].[centreid]
       ,dbo.[bbQuery].[messagecount]
       ,dbo.[bbQuery].[adminunread]
       ,dbo.[bbQuery].[clinicianunread]
       ,dbo.[bbQuery].[QueryTypeId]
       ,dbo.[bbQuery].[QueryStatusId]
       ,dbo.[bbQuery].[include]
       ,dbo.[bbQuery].[createdbyid]
       ,dbo.[bbQuery].[createdbyname]
       ,dbo.[bbQuery].[createddate]
       ,dbo.[bbQuery].[lastupdatedbyid]
       ,dbo.[bbQuery].[lastupdatedbyname]
       ,dbo.[bbQuery].[lastupdateddate]
       ,dbo.[bbQuery].[subject]
       ,dbo.[bbQuery].[secondRequest]
       ,dbo.[bbQuery].[isAuditQuery]
  FROM dbo.[bbQuery]
 WHERE EXISTS (SELECT 1
                 FROM dbo.[BbAdverseEventQueryLink] 
                WHERE dbo.[BbAdverseEventQueryLink].[fupaeid] = @Fupaeid
                  AND dbo.[BbAdverseEventQueryLink].[qid] = dbo.[bbQuery].[QId]
                  )
				SELECT @@ROWCOUNT			
				

GO
GRANT EXEC ON dbo.znt_bbQuery_GetByFupaeidFromBbAdverseEventQueryLink TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQuery_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQuery_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQuery_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbQuery table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQuery_Find
(

	@SearchUsingOR bit   = null ,

	@Qid int   = null ,

	@Chid int   = null ,

	@Fupnumber int   = null ,

	@Centreid int   = null ,

	@Messagecount int   = null ,

	@Adminunread bit   = null ,

	@Clinicianunread bit   = null ,

	@QueryTypeId int   = null ,

	@QueryStatusId int   = null ,

	@Include bit   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@Subject varchar (MAX)  = null ,

	@SecondRequest bit   = null ,

	@IsAuditQuery bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [QId]
	, [chid]
	, [fupnumber]
	, [centreid]
	, [messagecount]
	, [adminunread]
	, [clinicianunread]
	, [QueryTypeId]
	, [QueryStatusId]
	, [include]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [subject]
	, [secondRequest]
	, [isAuditQuery]
    FROM
	[dbo].[bbQuery]
    WHERE 
	 ([QId] = @Qid OR @Qid IS NULL)
	AND ([chid] = @Chid OR @Chid IS NULL)
	AND ([fupnumber] = @Fupnumber OR @Fupnumber IS NULL)
	AND ([centreid] = @Centreid OR @Centreid IS NULL)
	AND ([messagecount] = @Messagecount OR @Messagecount IS NULL)
	AND ([adminunread] = @Adminunread OR @Adminunread IS NULL)
	AND ([clinicianunread] = @Clinicianunread OR @Clinicianunread IS NULL)
	AND ([QueryTypeId] = @QueryTypeId OR @QueryTypeId IS NULL)
	AND ([QueryStatusId] = @QueryStatusId OR @QueryStatusId IS NULL)
	AND ([include] = @Include OR @Include IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([subject] = @Subject OR @Subject IS NULL)
	AND ([secondRequest] = @SecondRequest OR @SecondRequest IS NULL)
	AND ([isAuditQuery] = @IsAuditQuery OR @IsAuditQuery IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [QId]
	, [chid]
	, [fupnumber]
	, [centreid]
	, [messagecount]
	, [adminunread]
	, [clinicianunread]
	, [QueryTypeId]
	, [QueryStatusId]
	, [include]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [subject]
	, [secondRequest]
	, [isAuditQuery]
    FROM
	[dbo].[bbQuery]
    WHERE 
	 ([QId] = @Qid AND @Qid is not null)
	OR ([chid] = @Chid AND @Chid is not null)
	OR ([fupnumber] = @Fupnumber AND @Fupnumber is not null)
	OR ([centreid] = @Centreid AND @Centreid is not null)
	OR ([messagecount] = @Messagecount AND @Messagecount is not null)
	OR ([adminunread] = @Adminunread AND @Adminunread is not null)
	OR ([clinicianunread] = @Clinicianunread AND @Clinicianunread is not null)
	OR ([QueryTypeId] = @QueryTypeId AND @QueryTypeId is not null)
	OR ([QueryStatusId] = @QueryStatusId AND @QueryStatusId is not null)
	OR ([include] = @Include AND @Include is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([subject] = @Subject AND @Subject is not null)
	OR ([secondRequest] = @SecondRequest AND @SecondRequest is not null)
	OR ([isAuditQuery] = @IsAuditQuery AND @IsAuditQuery is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbQuery_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbQueryMessage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_Get_List

AS


				
				SELECT
					[QMId],
					[QId],
					[message],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryMessage]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbQueryMessage table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [QMId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([QMId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [QMId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryMessage]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[QMId], O.[QId], O.[message], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbQueryMessage] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[QMId] = PageIndex.[QMId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbQueryMessage]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbQueryMessage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_Insert
(

	@QmId int    OUTPUT,

	@Qid int   ,

	@Message varchar (MAX)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbQueryMessage]
					(
					[QId]
					,[message]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Qid
					,@Message
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @QmId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbQueryMessage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_Update
(

	@QmId int   ,

	@Qid int   ,

	@Message varchar (MAX)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbQueryMessage]
				SET
					[QId] = @Qid
					,[message] = @Message
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[QMId] = @QmId 
				
			

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbQueryMessage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_Delete
(

	@QmId int   
)
AS


				DELETE FROM [dbo].[bbQueryMessage] WITH (ROWLOCK) 
				WHERE
					[QMId] = @QmId
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_GetByQid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_GetByQid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_GetByQid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryMessage table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_GetByQid
(

	@Qid int   
)
AS


				SELECT
					[QMId],
					[QId],
					[message],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryMessage]
				WHERE
					[QId] = @Qid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_GetByQid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_GetByQmId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_GetByQmId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_GetByQmId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbQueryMessage table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_GetByQmId
(

	@QmId int   
)
AS


				SELECT
					[QMId],
					[QId],
					[message],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbQueryMessage]
				WHERE
					[QMId] = @QmId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_GetByQmId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbQueryMessage_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbQueryMessage_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbQueryMessage_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbQueryMessage table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbQueryMessage_Find
(

	@SearchUsingOR bit   = null ,

	@QmId int   = null ,

	@Qid int   = null ,

	@Message varchar (MAX)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [QMId]
	, [QId]
	, [message]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbQueryMessage]
    WHERE 
	 ([QMId] = @QmId OR @QmId IS NULL)
	AND ([QId] = @Qid OR @Qid IS NULL)
	AND ([message] = @Message OR @Message IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [QMId]
	, [QId]
	, [message]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbQueryMessage]
    WHERE 
	 ([QMId] = @QmId AND @QmId is not null)
	OR ([QId] = @Qid AND @Qid is not null)
	OR ([message] = @Message AND @Message is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbQueryMessage_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbSaeClinicianlkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbSaeClinicianlkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbSaeClinicianlkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbSaeClinicianlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbSaeClinicianlkp_Get_List

AS


				
				SELECT
					[saeid],
					[saecode],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbSaeClinicianlkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbSaeClinicianlkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbSaeClinicianlkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbSaeClinicianlkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbSaeClinicianlkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbSaeClinicianlkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbSaeClinicianlkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [saeid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([saeid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [saeid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbSaeClinicianlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[saeid], O.[saecode], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbSaeClinicianlkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[saeid] = PageIndex.[saeid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbSaeClinicianlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbSaeClinicianlkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbSaeClinicianlkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbSaeClinicianlkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbSaeClinicianlkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbSaeClinicianlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbSaeClinicianlkp_Insert
(

	@Saeid int    OUTPUT,

	@Saecode varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbSaeClinicianlkp]
					(
					[saecode]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Saecode
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Saeid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbSaeClinicianlkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbSaeClinicianlkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbSaeClinicianlkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbSaeClinicianlkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbSaeClinicianlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbSaeClinicianlkp_Update
(

	@Saeid int   ,

	@Saecode varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbSaeClinicianlkp]
				SET
					[saecode] = @Saecode
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[saeid] = @Saeid 
				
			

GO
GRANT EXEC ON dbo.znt_bbSaeClinicianlkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbSaeClinicianlkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbSaeClinicianlkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbSaeClinicianlkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbSaeClinicianlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbSaeClinicianlkp_Delete
(

	@Saeid int   
)
AS


				DELETE FROM [dbo].[bbSaeClinicianlkp] WITH (ROWLOCK) 
				WHERE
					[saeid] = @Saeid
					
			

GO
GRANT EXEC ON dbo.znt_bbSaeClinicianlkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbSaeClinicianlkp_GetBySaeid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbSaeClinicianlkp_GetBySaeid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbSaeClinicianlkp_GetBySaeid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbSaeClinicianlkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbSaeClinicianlkp_GetBySaeid
(

	@Saeid int   
)
AS


				SELECT
					[saeid],
					[saecode],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbSaeClinicianlkp]
				WHERE
					[saeid] = @Saeid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbSaeClinicianlkp_GetBySaeid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbSaeClinicianlkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbSaeClinicianlkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbSaeClinicianlkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbSaeClinicianlkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbSaeClinicianlkp_Find
(

	@SearchUsingOR bit   = null ,

	@Saeid int   = null ,

	@Saecode varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [saeid]
	, [saecode]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbSaeClinicianlkp]
    WHERE 
	 ([saeid] = @Saeid OR @Saeid IS NULL)
	AND ([saecode] = @Saecode OR @Saecode IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [saeid]
	, [saecode]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbSaeClinicianlkp]
    WHERE 
	 ([saeid] = @Saeid AND @Saeid is not null)
	OR ([saecode] = @Saecode AND @Saecode is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbSaeClinicianlkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCohortlkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCohortlkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCohortlkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbCohortlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCohortlkp_Get_List

AS


				
				SELECT
					[cohortid],
					[cohort],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCohortlkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbCohortlkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCohortlkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCohortlkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCohortlkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbCohortlkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCohortlkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [cohortid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([cohortid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [cohortid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbCohortlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[cohortid], O.[cohort], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbCohortlkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[cohortid] = PageIndex.[cohortid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbCohortlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbCohortlkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCohortlkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCohortlkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCohortlkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbCohortlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCohortlkp_Insert
(

	@Cohortid int    OUTPUT,

	@Cohort varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbCohortlkp]
					(
					[cohort]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Cohort
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Cohortid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbCohortlkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCohortlkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCohortlkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCohortlkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbCohortlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCohortlkp_Update
(

	@Cohortid int   ,

	@Cohort varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbCohortlkp]
				SET
					[cohort] = @Cohort
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[cohortid] = @Cohortid 
				
			

GO
GRANT EXEC ON dbo.znt_bbCohortlkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCohortlkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCohortlkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCohortlkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbCohortlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCohortlkp_Delete
(

	@Cohortid int   
)
AS


				DELETE FROM [dbo].[bbCohortlkp] WITH (ROWLOCK) 
				WHERE
					[cohortid] = @Cohortid
					
			

GO
GRANT EXEC ON dbo.znt_bbCohortlkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCohortlkp_GetByCohortid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCohortlkp_GetByCohortid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCohortlkp_GetByCohortid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCohortlkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCohortlkp_GetByCohortid
(

	@Cohortid int   
)
AS


				SELECT
					[cohortid],
					[cohort],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCohortlkp]
				WHERE
					[cohortid] = @Cohortid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbCohortlkp_GetByCohortid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCohortlkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCohortlkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCohortlkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbCohortlkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCohortlkp_Find
(

	@SearchUsingOR bit   = null ,

	@Cohortid int   = null ,

	@Cohort varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [cohortid]
	, [cohort]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCohortlkp]
    WHERE 
	 ([cohortid] = @Cohortid OR @Cohortid IS NULL)
	AND ([cohort] = @Cohort OR @Cohort IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [cohortid]
	, [cohort]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCohortlkp]
    WHERE 
	 ([cohortid] = @Cohortid AND @Cohortid is not null)
	OR ([cohort] = @Cohort AND @Cohort is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbCohortlkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbTitlelkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbTitlelkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbTitlelkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbTitlelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbTitlelkp_Get_List

AS


				
				SELECT
					[titleid],
					[title],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbTitlelkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbTitlelkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbTitlelkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbTitlelkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbTitlelkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbTitlelkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbTitlelkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [titleid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([titleid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [titleid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbTitlelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[titleid], O.[title], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbTitlelkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[titleid] = PageIndex.[titleid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbTitlelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbTitlelkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbTitlelkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbTitlelkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbTitlelkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbTitlelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbTitlelkp_Insert
(

	@Titleid int   ,

	@Title varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbTitlelkp]
					(
					[titleid]
					,[title]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Titleid
					,@Title
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
									
							
			

GO
GRANT EXEC ON dbo.znt_bbTitlelkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbTitlelkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbTitlelkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbTitlelkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbTitlelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbTitlelkp_Update
(

	@Titleid int   ,

	@OriginalTitleid int   ,

	@Title varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbTitlelkp]
				SET
					[titleid] = @Titleid
					,[title] = @Title
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[titleid] = @OriginalTitleid 
				
			

GO
GRANT EXEC ON dbo.znt_bbTitlelkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbTitlelkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbTitlelkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbTitlelkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbTitlelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbTitlelkp_Delete
(

	@Titleid int   
)
AS


				DELETE FROM [dbo].[bbTitlelkp] WITH (ROWLOCK) 
				WHERE
					[titleid] = @Titleid
					
			

GO
GRANT EXEC ON dbo.znt_bbTitlelkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbTitlelkp_GetByTitleid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbTitlelkp_GetByTitleid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbTitlelkp_GetByTitleid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbTitlelkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbTitlelkp_GetByTitleid
(

	@Titleid int   
)
AS


				SELECT
					[titleid],
					[title],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbTitlelkp]
				WHERE
					[titleid] = @Titleid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbTitlelkp_GetByTitleid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbTitlelkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbTitlelkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbTitlelkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbTitlelkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbTitlelkp_Find
(

	@SearchUsingOR bit   = null ,

	@Titleid int   = null ,

	@Title varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [titleid]
	, [title]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbTitlelkp]
    WHERE 
	 ([titleid] = @Titleid OR @Titleid IS NULL)
	AND ([title] = @Title OR @Title IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [titleid]
	, [title]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbTitlelkp]
    WHERE 
	 ([titleid] = @Titleid AND @Titleid is not null)
	OR ([title] = @Title AND @Title is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbTitlelkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPositionRolelkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPositionRolelkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPositionRolelkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPositionRolelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPositionRolelkp_Get_List

AS


				
				SELECT
					[positionid],
					[position],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPositionRolelkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPositionRolelkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPositionRolelkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPositionRolelkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPositionRolelkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPositionRolelkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPositionRolelkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [positionid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([positionid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [positionid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPositionRolelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[positionid], O.[position], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbPositionRolelkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[positionid] = PageIndex.[positionid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPositionRolelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPositionRolelkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPositionRolelkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPositionRolelkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPositionRolelkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPositionRolelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPositionRolelkp_Insert
(

	@Positionid int    OUTPUT,

	@Position varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPositionRolelkp]
					(
					[position]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Position
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Positionid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPositionRolelkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPositionRolelkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPositionRolelkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPositionRolelkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPositionRolelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPositionRolelkp_Update
(

	@Positionid int   ,

	@Position varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPositionRolelkp]
				SET
					[position] = @Position
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[positionid] = @Positionid 
				
			

GO
GRANT EXEC ON dbo.znt_bbPositionRolelkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPositionRolelkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPositionRolelkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPositionRolelkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPositionRolelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPositionRolelkp_Delete
(

	@Positionid int   
)
AS


				DELETE FROM [dbo].[bbPositionRolelkp] WITH (ROWLOCK) 
				WHERE
					[positionid] = @Positionid
					
			

GO
GRANT EXEC ON dbo.znt_bbPositionRolelkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPositionRolelkp_GetByPositionid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPositionRolelkp_GetByPositionid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPositionRolelkp_GetByPositionid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPositionRolelkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPositionRolelkp_GetByPositionid
(

	@Positionid int   
)
AS


				SELECT
					[positionid],
					[position],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPositionRolelkp]
				WHERE
					[positionid] = @Positionid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPositionRolelkp_GetByPositionid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPositionRolelkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPositionRolelkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPositionRolelkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPositionRolelkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPositionRolelkp_Find
(

	@SearchUsingOR bit   = null ,

	@Positionid int   = null ,

	@Position varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [positionid]
	, [position]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPositionRolelkp]
    WHERE 
	 ([positionid] = @Positionid OR @Positionid IS NULL)
	AND ([position] = @Position OR @Position IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [positionid]
	, [position]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPositionRolelkp]
    WHERE 
	 ([positionid] = @Positionid AND @Positionid is not null)
	OR ([position] = @Position AND @Position is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPositionRolelkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbWorkStatuslkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbWorkStatuslkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbWorkStatuslkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbWorkStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbWorkStatuslkp_Get_List

AS


				
				SELECT
					[worstatuskid],
					[workstatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbWorkStatuslkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbWorkStatuslkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbWorkStatuslkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbWorkStatuslkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbWorkStatuslkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbWorkStatuslkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbWorkStatuslkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [worstatuskid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([worstatuskid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [worstatuskid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbWorkStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[worstatuskid], O.[workstatus], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbWorkStatuslkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[worstatuskid] = PageIndex.[worstatuskid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbWorkStatuslkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbWorkStatuslkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbWorkStatuslkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbWorkStatuslkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbWorkStatuslkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbWorkStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbWorkStatuslkp_Insert
(

	@Worstatuskid int    OUTPUT,

	@Workstatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbWorkStatuslkp]
					(
					[workstatus]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Workstatus
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Worstatuskid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbWorkStatuslkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbWorkStatuslkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbWorkStatuslkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbWorkStatuslkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbWorkStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbWorkStatuslkp_Update
(

	@Worstatuskid int   ,

	@Workstatus varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbWorkStatuslkp]
				SET
					[workstatus] = @Workstatus
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[worstatuskid] = @Worstatuskid 
				
			

GO
GRANT EXEC ON dbo.znt_bbWorkStatuslkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbWorkStatuslkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbWorkStatuslkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbWorkStatuslkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbWorkStatuslkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbWorkStatuslkp_Delete
(

	@Worstatuskid int   
)
AS


				DELETE FROM [dbo].[bbWorkStatuslkp] WITH (ROWLOCK) 
				WHERE
					[worstatuskid] = @Worstatuskid
					
			

GO
GRANT EXEC ON dbo.znt_bbWorkStatuslkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbWorkStatuslkp_GetByWorstatuskid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbWorkStatuslkp_GetByWorstatuskid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbWorkStatuslkp_GetByWorstatuskid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbWorkStatuslkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbWorkStatuslkp_GetByWorstatuskid
(

	@Worstatuskid int   
)
AS


				SELECT
					[worstatuskid],
					[workstatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbWorkStatuslkp]
				WHERE
					[worstatuskid] = @Worstatuskid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbWorkStatuslkp_GetByWorstatuskid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbWorkStatuslkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbWorkStatuslkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbWorkStatuslkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbWorkStatuslkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbWorkStatuslkp_Find
(

	@SearchUsingOR bit   = null ,

	@Worstatuskid int   = null ,

	@Workstatus varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [worstatuskid]
	, [workstatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbWorkStatuslkp]
    WHERE 
	 ([worstatuskid] = @Worstatuskid OR @Worstatuskid IS NULL)
	AND ([workstatus] = @Workstatus OR @Workstatus IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [worstatuskid]
	, [workstatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbWorkStatuslkp]
    WHERE 
	 ([worstatuskid] = @Worstatuskid AND @Worstatuskid is not null)
	OR ([workstatus] = @Workstatus AND @Workstatus is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbWorkStatuslkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPappPatientMedProblemFup table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Get_List

AS


				
				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[occupation],
					[employmentstatus],
					[hospitaladmissions],
					[newdrugs],
					[newclinics],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPappPatientMedProblemFup]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPappPatientMedProblemFup table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [FormID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([FormID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [FormID]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientMedProblemFup]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[FormID], O.[chid], O.[PappFupId], O.[occupation], O.[employmentstatus], O.[hospitaladmissions], O.[newdrugs], O.[newclinics], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbPappPatientMedProblemFup] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[FormID] = PageIndex.[FormID]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientMedProblemFup]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPappPatientMedProblemFup table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Insert
(

	@FormId int   ,

	@Chid int   ,

	@PappFupId int   ,

	@Occupation varchar (50)  ,

	@Employmentstatus int   ,

	@Hospitaladmissions int   ,

	@Newdrugs int   ,

	@Newclinics int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPappPatientMedProblemFup]
					(
					[FormID]
					,[chid]
					,[PappFupId]
					,[occupation]
					,[employmentstatus]
					,[hospitaladmissions]
					,[newdrugs]
					,[newclinics]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@FormId
					,@Chid
					,@PappFupId
					,@Occupation
					,@Employmentstatus
					,@Hospitaladmissions
					,@Newdrugs
					,@Newclinics
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPappPatientMedProblemFup table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Update
(

	@FormId int   ,

	@OriginalFormId int   ,

	@Chid int   ,

	@PappFupId int   ,

	@Occupation varchar (50)  ,

	@Employmentstatus int   ,

	@Hospitaladmissions int   ,

	@Newdrugs int   ,

	@Newclinics int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPappPatientMedProblemFup]
				SET
					[FormID] = @FormId
					,[chid] = @Chid
					,[PappFupId] = @PappFupId
					,[occupation] = @Occupation
					,[employmentstatus] = @Employmentstatus
					,[hospitaladmissions] = @Hospitaladmissions
					,[newdrugs] = @Newdrugs
					,[newclinics] = @Newclinics
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[FormID] = @OriginalFormId 
				
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPappPatientMedProblemFup table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Delete
(

	@FormId int   
)
AS


				DELETE FROM [dbo].[bbPappPatientMedProblemFup] WITH (ROWLOCK) 
				WHERE
					[FormID] = @FormId
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_GetByChid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_GetByChid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_GetByChid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientMedProblemFup table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_GetByChid
(

	@Chid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[occupation],
					[employmentstatus],
					[hospitaladmissions],
					[newdrugs],
					[newclinics],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPappPatientMedProblemFup]
				WHERE
					[chid] = @Chid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_GetByChid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_GetByFormId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_GetByFormId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_GetByFormId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientMedProblemFup table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_GetByFormId
(

	@FormId int   
)
AS


				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[occupation],
					[employmentstatus],
					[hospitaladmissions],
					[newdrugs],
					[newclinics],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPappPatientMedProblemFup]
				WHERE
					[FormID] = @FormId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_GetByFormId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientMedProblemFup_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientMedProblemFup_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPappPatientMedProblemFup table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientMedProblemFup_Find
(

	@SearchUsingOR bit   = null ,

	@FormId int   = null ,

	@Chid int   = null ,

	@PappFupId int   = null ,

	@Occupation varchar (50)  = null ,

	@Employmentstatus int   = null ,

	@Hospitaladmissions int   = null ,

	@Newdrugs int   = null ,

	@Newclinics int   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FormID]
	, [chid]
	, [PappFupId]
	, [occupation]
	, [employmentstatus]
	, [hospitaladmissions]
	, [newdrugs]
	, [newclinics]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPappPatientMedProblemFup]
    WHERE 
	 ([FormID] = @FormId OR @FormId IS NULL)
	AND ([chid] = @Chid OR @Chid IS NULL)
	AND ([PappFupId] = @PappFupId OR @PappFupId IS NULL)
	AND ([occupation] = @Occupation OR @Occupation IS NULL)
	AND ([employmentstatus] = @Employmentstatus OR @Employmentstatus IS NULL)
	AND ([hospitaladmissions] = @Hospitaladmissions OR @Hospitaladmissions IS NULL)
	AND ([newdrugs] = @Newdrugs OR @Newdrugs IS NULL)
	AND ([newclinics] = @Newclinics OR @Newclinics IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [FormID]
	, [chid]
	, [PappFupId]
	, [occupation]
	, [employmentstatus]
	, [hospitaladmissions]
	, [newdrugs]
	, [newclinics]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPappPatientMedProblemFup]
    WHERE 
	 ([FormID] = @FormId AND @FormId is not null)
	OR ([chid] = @Chid AND @Chid is not null)
	OR ([PappFupId] = @PappFupId AND @PappFupId is not null)
	OR ([occupation] = @Occupation AND @Occupation is not null)
	OR ([employmentstatus] = @Employmentstatus AND @Employmentstatus is not null)
	OR ([hospitaladmissions] = @Hospitaladmissions AND @Hospitaladmissions is not null)
	OR ([newdrugs] = @Newdrugs AND @Newdrugs is not null)
	OR ([newclinics] = @Newclinics AND @Newclinics is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPappPatientMedProblemFup_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbAdditionalUserDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_Get_List

AS


				
				SELECT
					[userid],
					[BADBIRuserid],
					[title],
					[fName],
					[lName],
					[position],
					[phone],
					[hospital],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[baselineTrainingDate],
					[baselineTrainingBy],
					[baselineTrainingNotes],
					[fupTrainingDate],
					[fupTrainingBy],
					[fupTrainingNotes],
					[require2FA],
					[flag1PersonalData],
					[flag2EmailResearch],
					[flag3EmailNewsletter],
					[flag4EmailCentreRpt]
				FROM
					[dbo].[bbAdditionalUserDetail]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbAdditionalUserDetail table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [userid] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([userid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [userid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbAdditionalUserDetail]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[userid], O.[BADBIRuserid], O.[title], O.[fName], O.[lName], O.[position], O.[phone], O.[hospital], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[baselineTrainingDate], O.[baselineTrainingBy], O.[baselineTrainingNotes], O.[fupTrainingDate], O.[fupTrainingBy], O.[fupTrainingNotes], O.[require2FA], O.[flag1PersonalData], O.[flag2EmailResearch], O.[flag3EmailNewsletter], O.[flag4EmailCentreRpt]
				FROM
				    [dbo].[bbAdditionalUserDetail] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[userid] = PageIndex.[userid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbAdditionalUserDetail]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbAdditionalUserDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_Insert
(

	@Userid uniqueidentifier   ,

	@BadbiRuserid int    OUTPUT,

	@Title varchar (10)  ,

	@FName varchar (50)  ,

	@LName varchar (50)  ,

	@Position varchar (100)  ,

	@Phone varchar (50)  ,

	@Hospital varchar (150)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@BaselineTrainingDate datetime   ,

	@BaselineTrainingBy varchar (256)  ,

	@BaselineTrainingNotes varchar (1024)  ,

	@FupTrainingDate datetime   ,

	@FupTrainingBy varchar (256)  ,

	@FupTrainingNotes varchar (1024)  ,

	@Require2Fa bit   ,

	@Flag1PersonalData bit   ,

	@Flag2EmailResearch bit   ,

	@Flag3EmailNewsletter bit   ,

	@Flag4EmailCentreRpt bit   
)
AS


				
				INSERT INTO [dbo].[bbAdditionalUserDetail]
					(
					[userid]
					,[title]
					,[fName]
					,[lName]
					,[position]
					,[phone]
					,[hospital]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[baselineTrainingDate]
					,[baselineTrainingBy]
					,[baselineTrainingNotes]
					,[fupTrainingDate]
					,[fupTrainingBy]
					,[fupTrainingNotes]
					,[require2FA]
					,[flag1PersonalData]
					,[flag2EmailResearch]
					,[flag3EmailNewsletter]
					,[flag4EmailCentreRpt]
					)
				VALUES
					(
					@Userid
					,@Title
					,@FName
					,@LName
					,@Position
					,@Phone
					,@Hospital
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@BaselineTrainingDate
					,@BaselineTrainingBy
					,@BaselineTrainingNotes
					,@FupTrainingDate
					,@FupTrainingBy
					,@FupTrainingNotes
					,@Require2Fa
					,@Flag1PersonalData
					,@Flag2EmailResearch
					,@Flag3EmailNewsletter
					,@Flag4EmailCentreRpt
					)
				-- Get the identity value
				SET @BadbiRuserid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbAdditionalUserDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_Update
(

	@Userid uniqueidentifier   ,

	@OriginalUserid uniqueidentifier   ,

	@BadbiRuserid int   ,

	@Title varchar (10)  ,

	@FName varchar (50)  ,

	@LName varchar (50)  ,

	@Position varchar (100)  ,

	@Phone varchar (50)  ,

	@Hospital varchar (150)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@BaselineTrainingDate datetime   ,

	@BaselineTrainingBy varchar (256)  ,

	@BaselineTrainingNotes varchar (1024)  ,

	@FupTrainingDate datetime   ,

	@FupTrainingBy varchar (256)  ,

	@FupTrainingNotes varchar (1024)  ,

	@Require2Fa bit   ,

	@Flag1PersonalData bit   ,

	@Flag2EmailResearch bit   ,

	@Flag3EmailNewsletter bit   ,

	@Flag4EmailCentreRpt bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbAdditionalUserDetail]
				SET
					[userid] = @Userid
					,[title] = @Title
					,[fName] = @FName
					,[lName] = @LName
					,[position] = @Position
					,[phone] = @Phone
					,[hospital] = @Hospital
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[baselineTrainingDate] = @BaselineTrainingDate
					,[baselineTrainingBy] = @BaselineTrainingBy
					,[baselineTrainingNotes] = @BaselineTrainingNotes
					,[fupTrainingDate] = @FupTrainingDate
					,[fupTrainingBy] = @FupTrainingBy
					,[fupTrainingNotes] = @FupTrainingNotes
					,[require2FA] = @Require2Fa
					,[flag1PersonalData] = @Flag1PersonalData
					,[flag2EmailResearch] = @Flag2EmailResearch
					,[flag3EmailNewsletter] = @Flag3EmailNewsletter
					,[flag4EmailCentreRpt] = @Flag4EmailCentreRpt
				WHERE
[userid] = @OriginalUserid 
				
			

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbAdditionalUserDetail table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_Delete
(

	@Userid uniqueidentifier   
)
AS


				DELETE FROM [dbo].[bbAdditionalUserDetail] WITH (ROWLOCK) 
				WHERE
					[userid] = @Userid
					
			

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_GetByBadbiRuserid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_GetByBadbiRuserid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_GetByBadbiRuserid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbAdditionalUserDetail table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_GetByBadbiRuserid
(

	@BadbiRuserid int   
)
AS


				SELECT
					[userid],
					[BADBIRuserid],
					[title],
					[fName],
					[lName],
					[position],
					[phone],
					[hospital],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[baselineTrainingDate],
					[baselineTrainingBy],
					[baselineTrainingNotes],
					[fupTrainingDate],
					[fupTrainingBy],
					[fupTrainingNotes],
					[require2FA],
					[flag1PersonalData],
					[flag2EmailResearch],
					[flag3EmailNewsletter],
					[flag4EmailCentreRpt]
				FROM
					[dbo].[bbAdditionalUserDetail]
				WHERE
					[BADBIRuserid] = @BadbiRuserid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_GetByBadbiRuserid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_GetByUserid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_GetByUserid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_GetByUserid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbAdditionalUserDetail table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_GetByUserid
(

	@Userid uniqueidentifier   
)
AS


				SELECT
					[userid],
					[BADBIRuserid],
					[title],
					[fName],
					[lName],
					[position],
					[phone],
					[hospital],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[baselineTrainingDate],
					[baselineTrainingBy],
					[baselineTrainingNotes],
					[fupTrainingDate],
					[fupTrainingBy],
					[fupTrainingNotes],
					[require2FA],
					[flag1PersonalData],
					[flag2EmailResearch],
					[flag3EmailNewsletter],
					[flag4EmailCentreRpt]
				FROM
					[dbo].[bbAdditionalUserDetail]
				WHERE
					[userid] = @Userid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_GetByUserid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_GetByCentreidFromBbAdditionalUserAndCentre procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_GetByCentreidFromBbAdditionalUserAndCentre') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_GetByCentreidFromBbAdditionalUserAndCentre
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records through a junction table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_GetByCentreidFromBbAdditionalUserAndCentre
(

	@Centreid int   
)
AS


SELECT dbo.[bbAdditionalUserDetail].[userid]
       ,dbo.[bbAdditionalUserDetail].[BADBIRuserid]
       ,dbo.[bbAdditionalUserDetail].[title]
       ,dbo.[bbAdditionalUserDetail].[fName]
       ,dbo.[bbAdditionalUserDetail].[lName]
       ,dbo.[bbAdditionalUserDetail].[position]
       ,dbo.[bbAdditionalUserDetail].[phone]
       ,dbo.[bbAdditionalUserDetail].[hospital]
       ,dbo.[bbAdditionalUserDetail].[createdbyid]
       ,dbo.[bbAdditionalUserDetail].[createdbyname]
       ,dbo.[bbAdditionalUserDetail].[createddate]
       ,dbo.[bbAdditionalUserDetail].[lastupdatedbyid]
       ,dbo.[bbAdditionalUserDetail].[lastupdatedbyname]
       ,dbo.[bbAdditionalUserDetail].[lastupdateddate]
       ,dbo.[bbAdditionalUserDetail].[baselineTrainingDate]
       ,dbo.[bbAdditionalUserDetail].[baselineTrainingBy]
       ,dbo.[bbAdditionalUserDetail].[baselineTrainingNotes]
       ,dbo.[bbAdditionalUserDetail].[fupTrainingDate]
       ,dbo.[bbAdditionalUserDetail].[fupTrainingBy]
       ,dbo.[bbAdditionalUserDetail].[fupTrainingNotes]
       ,dbo.[bbAdditionalUserDetail].[require2FA]
       ,dbo.[bbAdditionalUserDetail].[flag1PersonalData]
       ,dbo.[bbAdditionalUserDetail].[flag2EmailResearch]
       ,dbo.[bbAdditionalUserDetail].[flag3EmailNewsletter]
       ,dbo.[bbAdditionalUserDetail].[flag4EmailCentreRpt]
  FROM dbo.[bbAdditionalUserDetail]
 WHERE EXISTS (SELECT 1
                 FROM dbo.[bbAdditionalUserAndCentre] 
                WHERE dbo.[bbAdditionalUserAndCentre].[Centreid] = @Centreid
                  AND dbo.[bbAdditionalUserAndCentre].[userid] = dbo.[bbAdditionalUserDetail].[userid]
                  )
				SELECT @@ROWCOUNT			
				

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_GetByCentreidFromBbAdditionalUserAndCentre TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAdditionalUserDetail_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAdditionalUserDetail_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAdditionalUserDetail_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbAdditionalUserDetail table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAdditionalUserDetail_Find
(

	@SearchUsingOR bit   = null ,

	@Userid uniqueidentifier   = null ,

	@BadbiRuserid int   = null ,

	@Title varchar (10)  = null ,

	@FName varchar (50)  = null ,

	@LName varchar (50)  = null ,

	@Position varchar (100)  = null ,

	@Phone varchar (50)  = null ,

	@Hospital varchar (150)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@BaselineTrainingDate datetime   = null ,

	@BaselineTrainingBy varchar (256)  = null ,

	@BaselineTrainingNotes varchar (1024)  = null ,

	@FupTrainingDate datetime   = null ,

	@FupTrainingBy varchar (256)  = null ,

	@FupTrainingNotes varchar (1024)  = null ,

	@Require2Fa bit   = null ,

	@Flag1PersonalData bit   = null ,

	@Flag2EmailResearch bit   = null ,

	@Flag3EmailNewsletter bit   = null ,

	@Flag4EmailCentreRpt bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [userid]
	, [BADBIRuserid]
	, [title]
	, [fName]
	, [lName]
	, [position]
	, [phone]
	, [hospital]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [baselineTrainingDate]
	, [baselineTrainingBy]
	, [baselineTrainingNotes]
	, [fupTrainingDate]
	, [fupTrainingBy]
	, [fupTrainingNotes]
	, [require2FA]
	, [flag1PersonalData]
	, [flag2EmailResearch]
	, [flag3EmailNewsletter]
	, [flag4EmailCentreRpt]
    FROM
	[dbo].[bbAdditionalUserDetail]
    WHERE 
	 ([userid] = @Userid OR @Userid IS NULL)
	AND ([BADBIRuserid] = @BadbiRuserid OR @BadbiRuserid IS NULL)
	AND ([title] = @Title OR @Title IS NULL)
	AND ([fName] = @FName OR @FName IS NULL)
	AND ([lName] = @LName OR @LName IS NULL)
	AND ([position] = @Position OR @Position IS NULL)
	AND ([phone] = @Phone OR @Phone IS NULL)
	AND ([hospital] = @Hospital OR @Hospital IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([baselineTrainingDate] = @BaselineTrainingDate OR @BaselineTrainingDate IS NULL)
	AND ([baselineTrainingBy] = @BaselineTrainingBy OR @BaselineTrainingBy IS NULL)
	AND ([baselineTrainingNotes] = @BaselineTrainingNotes OR @BaselineTrainingNotes IS NULL)
	AND ([fupTrainingDate] = @FupTrainingDate OR @FupTrainingDate IS NULL)
	AND ([fupTrainingBy] = @FupTrainingBy OR @FupTrainingBy IS NULL)
	AND ([fupTrainingNotes] = @FupTrainingNotes OR @FupTrainingNotes IS NULL)
	AND ([require2FA] = @Require2Fa OR @Require2Fa IS NULL)
	AND ([flag1PersonalData] = @Flag1PersonalData OR @Flag1PersonalData IS NULL)
	AND ([flag2EmailResearch] = @Flag2EmailResearch OR @Flag2EmailResearch IS NULL)
	AND ([flag3EmailNewsletter] = @Flag3EmailNewsletter OR @Flag3EmailNewsletter IS NULL)
	AND ([flag4EmailCentreRpt] = @Flag4EmailCentreRpt OR @Flag4EmailCentreRpt IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [userid]
	, [BADBIRuserid]
	, [title]
	, [fName]
	, [lName]
	, [position]
	, [phone]
	, [hospital]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [baselineTrainingDate]
	, [baselineTrainingBy]
	, [baselineTrainingNotes]
	, [fupTrainingDate]
	, [fupTrainingBy]
	, [fupTrainingNotes]
	, [require2FA]
	, [flag1PersonalData]
	, [flag2EmailResearch]
	, [flag3EmailNewsletter]
	, [flag4EmailCentreRpt]
    FROM
	[dbo].[bbAdditionalUserDetail]
    WHERE 
	 ([userid] = @Userid AND @Userid is not null)
	OR ([BADBIRuserid] = @BadbiRuserid AND @BadbiRuserid is not null)
	OR ([title] = @Title AND @Title is not null)
	OR ([fName] = @FName AND @FName is not null)
	OR ([lName] = @LName AND @LName is not null)
	OR ([position] = @Position AND @Position is not null)
	OR ([phone] = @Phone AND @Phone is not null)
	OR ([hospital] = @Hospital AND @Hospital is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([baselineTrainingDate] = @BaselineTrainingDate AND @BaselineTrainingDate is not null)
	OR ([baselineTrainingBy] = @BaselineTrainingBy AND @BaselineTrainingBy is not null)
	OR ([baselineTrainingNotes] = @BaselineTrainingNotes AND @BaselineTrainingNotes is not null)
	OR ([fupTrainingDate] = @FupTrainingDate AND @FupTrainingDate is not null)
	OR ([fupTrainingBy] = @FupTrainingBy AND @FupTrainingBy is not null)
	OR ([fupTrainingNotes] = @FupTrainingNotes AND @FupTrainingNotes is not null)
	OR ([require2FA] = @Require2Fa AND @Require2Fa is not null)
	OR ([flag1PersonalData] = @Flag1PersonalData AND @Flag1PersonalData is not null)
	OR ([flag2EmailResearch] = @Flag2EmailResearch AND @Flag2EmailResearch is not null)
	OR ([flag3EmailNewsletter] = @Flag3EmailNewsletter AND @Flag3EmailNewsletter is not null)
	OR ([flag4EmailCentreRpt] = @Flag4EmailCentreRpt AND @Flag4EmailCentreRpt is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbAdditionalUserDetail_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAnswerlkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAnswerlkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAnswerlkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbAnswerlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAnswerlkp_Get_List

AS


				
				SELECT
					[answerid],
					[answerdescription],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbAnswerlkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbAnswerlkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAnswerlkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAnswerlkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAnswerlkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbAnswerlkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAnswerlkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [answerid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([answerid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [answerid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbAnswerlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[answerid], O.[answerdescription], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbAnswerlkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[answerid] = PageIndex.[answerid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbAnswerlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbAnswerlkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAnswerlkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAnswerlkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAnswerlkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbAnswerlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAnswerlkp_Insert
(

	@Answerid int    OUTPUT,

	@Answerdescription varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbAnswerlkp]
					(
					[answerdescription]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Answerdescription
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Answerid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbAnswerlkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAnswerlkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAnswerlkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAnswerlkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbAnswerlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAnswerlkp_Update
(

	@Answerid int   ,

	@Answerdescription varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbAnswerlkp]
				SET
					[answerdescription] = @Answerdescription
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[answerid] = @Answerid 
				
			

GO
GRANT EXEC ON dbo.znt_bbAnswerlkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAnswerlkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAnswerlkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAnswerlkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbAnswerlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAnswerlkp_Delete
(

	@Answerid int   
)
AS


				DELETE FROM [dbo].[bbAnswerlkp] WITH (ROWLOCK) 
				WHERE
					[answerid] = @Answerid
					
			

GO
GRANT EXEC ON dbo.znt_bbAnswerlkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAnswerlkp_GetByAnswerid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAnswerlkp_GetByAnswerid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAnswerlkp_GetByAnswerid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbAnswerlkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAnswerlkp_GetByAnswerid
(

	@Answerid int   
)
AS


				SELECT
					[answerid],
					[answerdescription],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbAnswerlkp]
				WHERE
					[answerid] = @Answerid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbAnswerlkp_GetByAnswerid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbAnswerlkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbAnswerlkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbAnswerlkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbAnswerlkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbAnswerlkp_Find
(

	@SearchUsingOR bit   = null ,

	@Answerid int   = null ,

	@Answerdescription varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [answerid]
	, [answerdescription]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbAnswerlkp]
    WHERE 
	 ([answerid] = @Answerid OR @Answerid IS NULL)
	AND ([answerdescription] = @Answerdescription OR @Answerdescription IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [answerid]
	, [answerdescription]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbAnswerlkp]
    WHERE 
	 ([answerid] = @Answerid AND @Answerid is not null)
	OR ([answerdescription] = @Answerdescription AND @Answerdescription is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbAnswerlkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCommonFrequencylkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCommonFrequencylkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCommonFrequencylkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbCommonFrequencylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCommonFrequencylkp_Get_List

AS


				
				SELECT
					[commonfrequencyid],
					[commonfrequency],
					[displayorder],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCommonFrequencylkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbCommonFrequencylkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCommonFrequencylkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCommonFrequencylkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCommonFrequencylkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbCommonFrequencylkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCommonFrequencylkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [commonfrequencyid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([commonfrequencyid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [commonfrequencyid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbCommonFrequencylkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[commonfrequencyid], O.[commonfrequency], O.[displayorder], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbCommonFrequencylkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[commonfrequencyid] = PageIndex.[commonfrequencyid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbCommonFrequencylkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbCommonFrequencylkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCommonFrequencylkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCommonFrequencylkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCommonFrequencylkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbCommonFrequencylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCommonFrequencylkp_Insert
(

	@Commonfrequencyid int    OUTPUT,

	@Commonfrequency varchar (50)  ,

	@Displayorder int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbCommonFrequencylkp]
					(
					[commonfrequency]
					,[displayorder]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Commonfrequency
					,@Displayorder
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Commonfrequencyid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbCommonFrequencylkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCommonFrequencylkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCommonFrequencylkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCommonFrequencylkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbCommonFrequencylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCommonFrequencylkp_Update
(

	@Commonfrequencyid int   ,

	@Commonfrequency varchar (50)  ,

	@Displayorder int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbCommonFrequencylkp]
				SET
					[commonfrequency] = @Commonfrequency
					,[displayorder] = @Displayorder
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[commonfrequencyid] = @Commonfrequencyid 
				
			

GO
GRANT EXEC ON dbo.znt_bbCommonFrequencylkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCommonFrequencylkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCommonFrequencylkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCommonFrequencylkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbCommonFrequencylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCommonFrequencylkp_Delete
(

	@Commonfrequencyid int   
)
AS


				DELETE FROM [dbo].[bbCommonFrequencylkp] WITH (ROWLOCK) 
				WHERE
					[commonfrequencyid] = @Commonfrequencyid
					
			

GO
GRANT EXEC ON dbo.znt_bbCommonFrequencylkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCommonFrequencylkp_GetByCommonfrequencyid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCommonFrequencylkp_GetByCommonfrequencyid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCommonFrequencylkp_GetByCommonfrequencyid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbCommonFrequencylkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCommonFrequencylkp_GetByCommonfrequencyid
(

	@Commonfrequencyid int   
)
AS


				SELECT
					[commonfrequencyid],
					[commonfrequency],
					[displayorder],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbCommonFrequencylkp]
				WHERE
					[commonfrequencyid] = @Commonfrequencyid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbCommonFrequencylkp_GetByCommonfrequencyid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbCommonFrequencylkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbCommonFrequencylkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbCommonFrequencylkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbCommonFrequencylkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbCommonFrequencylkp_Find
(

	@SearchUsingOR bit   = null ,

	@Commonfrequencyid int   = null ,

	@Commonfrequency varchar (50)  = null ,

	@Displayorder int   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [commonfrequencyid]
	, [commonfrequency]
	, [displayorder]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCommonFrequencylkp]
    WHERE 
	 ([commonfrequencyid] = @Commonfrequencyid OR @Commonfrequencyid IS NULL)
	AND ([commonfrequency] = @Commonfrequency OR @Commonfrequency IS NULL)
	AND ([displayorder] = @Displayorder OR @Displayorder IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [commonfrequencyid]
	, [commonfrequency]
	, [displayorder]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbCommonFrequencylkp]
    WHERE 
	 ([commonfrequencyid] = @Commonfrequencyid AND @Commonfrequencyid is not null)
	OR ([commonfrequency] = @Commonfrequency AND @Commonfrequency is not null)
	OR ([displayorder] = @Displayorder AND @Displayorder is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbCommonFrequencylkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbConfigFactory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_Get_List

AS


				
				SELECT
					[configid],
					[configname],
					[typeID],
					[textVal],
					[intVal],
					[floatVal],
					[inuse],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbConfigFactory]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbConfigFactory table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [configid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([configid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [configid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbConfigFactory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[configid], O.[configname], O.[typeID], O.[textVal], O.[intVal], O.[floatVal], O.[inuse], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbConfigFactory] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[configid] = PageIndex.[configid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbConfigFactory]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbConfigFactory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_Insert
(

	@Configid int    OUTPUT,

	@Configname varchar (50)  ,

	@TypeId tinyint   ,

	@TextVal varchar (1024)  ,

	@IntVal int   ,

	@FloatVal float   ,

	@Inuse bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbConfigFactory]
					(
					[configname]
					,[typeID]
					,[textVal]
					,[intVal]
					,[floatVal]
					,[inuse]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Configname
					,@TypeId
					,@TextVal
					,@IntVal
					,@FloatVal
					,@Inuse
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Configid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbConfigFactory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_Update
(

	@Configid int   ,

	@Configname varchar (50)  ,

	@TypeId tinyint   ,

	@TextVal varchar (1024)  ,

	@IntVal int   ,

	@FloatVal float   ,

	@Inuse bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbConfigFactory]
				SET
					[configname] = @Configname
					,[typeID] = @TypeId
					,[textVal] = @TextVal
					,[intVal] = @IntVal
					,[floatVal] = @FloatVal
					,[inuse] = @Inuse
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[configid] = @Configid 
				
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbConfigFactory table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_Delete
(

	@Configid int   
)
AS


				DELETE FROM [dbo].[bbConfigFactory] WITH (ROWLOCK) 
				WHERE
					[configid] = @Configid
					
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_GetByInuse procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_GetByInuse') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_GetByInuse
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbConfigFactory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_GetByInuse
(

	@Inuse bit   
)
AS


				SELECT
					[configid],
					[configname],
					[typeID],
					[textVal],
					[intVal],
					[floatVal],
					[inuse],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbConfigFactory]
				WHERE
					[inuse] = @Inuse
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_GetByInuse TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_GetByTypeIdInuse procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_GetByTypeIdInuse') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_GetByTypeIdInuse
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbConfigFactory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_GetByTypeIdInuse
(

	@TypeId tinyint   ,

	@Inuse bit   
)
AS


				SELECT
					[configid],
					[configname],
					[typeID],
					[textVal],
					[intVal],
					[floatVal],
					[inuse],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbConfigFactory]
				WHERE
					[typeID] = @TypeId
					AND [inuse] = @Inuse
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_GetByTypeIdInuse TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_GetByConfigid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_GetByConfigid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_GetByConfigid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbConfigFactory table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_GetByConfigid
(

	@Configid int   
)
AS


				SELECT
					[configid],
					[configname],
					[typeID],
					[textVal],
					[intVal],
					[floatVal],
					[inuse],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbConfigFactory]
				WHERE
					[configid] = @Configid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_GetByConfigid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbConfigFactory_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbConfigFactory_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbConfigFactory_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbConfigFactory table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbConfigFactory_Find
(

	@SearchUsingOR bit   = null ,

	@Configid int   = null ,

	@Configname varchar (50)  = null ,

	@TypeId tinyint   = null ,

	@TextVal varchar (1024)  = null ,

	@IntVal int   = null ,

	@FloatVal float   = null ,

	@Inuse bit   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [configid]
	, [configname]
	, [typeID]
	, [textVal]
	, [intVal]
	, [floatVal]
	, [inuse]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbConfigFactory]
    WHERE 
	 ([configid] = @Configid OR @Configid IS NULL)
	AND ([configname] = @Configname OR @Configname IS NULL)
	AND ([typeID] = @TypeId OR @TypeId IS NULL)
	AND ([textVal] = @TextVal OR @TextVal IS NULL)
	AND ([intVal] = @IntVal OR @IntVal IS NULL)
	AND ([floatVal] = @FloatVal OR @FloatVal IS NULL)
	AND ([inuse] = @Inuse OR @Inuse IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [configid]
	, [configname]
	, [typeID]
	, [textVal]
	, [intVal]
	, [floatVal]
	, [inuse]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbConfigFactory]
    WHERE 
	 ([configid] = @Configid AND @Configid is not null)
	OR ([configname] = @Configname AND @Configname is not null)
	OR ([typeID] = @TypeId AND @TypeId is not null)
	OR ([textVal] = @TextVal AND @TextVal is not null)
	OR ([intVal] = @IntVal AND @IntVal is not null)
	OR ([floatVal] = @FloatVal AND @FloatVal is not null)
	OR ([inuse] = @Inuse AND @Inuse is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbConfigFactory_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbEthnicitylkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbEthnicitylkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbEthnicitylkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbEthnicitylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbEthnicitylkp_Get_List

AS


				
				SELECT
					[ethnicityid],
					[ethnicity],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbEthnicitylkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbEthnicitylkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbEthnicitylkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbEthnicitylkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbEthnicitylkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbEthnicitylkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbEthnicitylkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [ethnicityid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ethnicityid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [ethnicityid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbEthnicitylkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[ethnicityid], O.[ethnicity], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbEthnicitylkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ethnicityid] = PageIndex.[ethnicityid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbEthnicitylkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbEthnicitylkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbEthnicitylkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbEthnicitylkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbEthnicitylkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbEthnicitylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbEthnicitylkp_Insert
(

	@Ethnicityid int    OUTPUT,

	@Ethnicity varchar (255)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbEthnicitylkp]
					(
					[ethnicity]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Ethnicity
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Ethnicityid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbEthnicitylkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbEthnicitylkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbEthnicitylkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbEthnicitylkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbEthnicitylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbEthnicitylkp_Update
(

	@Ethnicityid int   ,

	@Ethnicity varchar (255)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbEthnicitylkp]
				SET
					[ethnicity] = @Ethnicity
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[ethnicityid] = @Ethnicityid 
				
			

GO
GRANT EXEC ON dbo.znt_bbEthnicitylkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbEthnicitylkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbEthnicitylkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbEthnicitylkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbEthnicitylkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbEthnicitylkp_Delete
(

	@Ethnicityid int   
)
AS


				DELETE FROM [dbo].[bbEthnicitylkp] WITH (ROWLOCK) 
				WHERE
					[ethnicityid] = @Ethnicityid
					
			

GO
GRANT EXEC ON dbo.znt_bbEthnicitylkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbEthnicitylkp_GetByEthnicityid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbEthnicitylkp_GetByEthnicityid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbEthnicitylkp_GetByEthnicityid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbEthnicitylkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbEthnicitylkp_GetByEthnicityid
(

	@Ethnicityid int   
)
AS


				SELECT
					[ethnicityid],
					[ethnicity],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbEthnicitylkp]
				WHERE
					[ethnicityid] = @Ethnicityid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbEthnicitylkp_GetByEthnicityid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbEthnicitylkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbEthnicitylkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbEthnicitylkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbEthnicitylkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbEthnicitylkp_Find
(

	@SearchUsingOR bit   = null ,

	@Ethnicityid int   = null ,

	@Ethnicity varchar (255)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ethnicityid]
	, [ethnicity]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbEthnicitylkp]
    WHERE 
	 ([ethnicityid] = @Ethnicityid OR @Ethnicityid IS NULL)
	AND ([ethnicity] = @Ethnicity OR @Ethnicity IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ethnicityid]
	, [ethnicity]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbEthnicitylkp]
    WHERE 
	 ([ethnicityid] = @Ethnicityid AND @Ethnicityid is not null)
	OR ([ethnicity] = @Ethnicity AND @Ethnicity is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbEthnicitylkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbFileStorage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_Get_List

AS


				
				SELECT
					[fileID],
					[fileName],
					[fileCaption],
					[FileSize],
					[UploadDate],
					[UploadedByID],
					[UploadedByName],
					[ForeignKey],
					[ForeignKeyType],
					[ForeignKeyTypeDescription]
				FROM
					[dbo].[bbFileStorage]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbFileStorage_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbFileStorage table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [fileID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([fileID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [fileID]'
				SET @SQL = @SQL + ' FROM [dbo].[bbFileStorage]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[fileID], O.[fileName], O.[fileCaption], O.[FileSize], O.[UploadDate], O.[UploadedByID], O.[UploadedByName], O.[ForeignKey], O.[ForeignKeyType], O.[ForeignKeyTypeDescription]
				FROM
				    [dbo].[bbFileStorage] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[fileID] = PageIndex.[fileID]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbFileStorage]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbFileStorage_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbFileStorage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_Insert
(

	@FileId int    OUTPUT,

	@FileName varchar (255)  ,

	@FileCaption varchar (255)  ,

	@FileSize int   ,

	@UploadDate datetime   ,

	@UploadedById int   ,

	@UploadedByName varchar (255)  ,

	@ForeignKey int   ,

	@ForeignKeyType int   ,

	@ForeignKeyTypeDescription varchar (255)  
)
AS


				
				INSERT INTO [dbo].[bbFileStorage]
					(
					[fileName]
					,[fileCaption]
					,[FileSize]
					,[UploadDate]
					,[UploadedByID]
					,[UploadedByName]
					,[ForeignKey]
					,[ForeignKeyType]
					,[ForeignKeyTypeDescription]
					)
				VALUES
					(
					@FileName
					,@FileCaption
					,@FileSize
					,@UploadDate
					,@UploadedById
					,@UploadedByName
					,@ForeignKey
					,@ForeignKeyType
					,@ForeignKeyTypeDescription
					)
				-- Get the identity value
				SET @FileId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbFileStorage_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbFileStorage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_Update
(

	@FileId int   ,

	@FileName varchar (255)  ,

	@FileCaption varchar (255)  ,

	@FileSize int   ,

	@UploadDate datetime   ,

	@UploadedById int   ,

	@UploadedByName varchar (255)  ,

	@ForeignKey int   ,

	@ForeignKeyType int   ,

	@ForeignKeyTypeDescription varchar (255)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbFileStorage]
				SET
					[fileName] = @FileName
					,[fileCaption] = @FileCaption
					,[FileSize] = @FileSize
					,[UploadDate] = @UploadDate
					,[UploadedByID] = @UploadedById
					,[UploadedByName] = @UploadedByName
					,[ForeignKey] = @ForeignKey
					,[ForeignKeyType] = @ForeignKeyType
					,[ForeignKeyTypeDescription] = @ForeignKeyTypeDescription
				WHERE
[fileID] = @FileId 
				
			

GO
GRANT EXEC ON dbo.znt_bbFileStorage_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbFileStorage table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_Delete
(

	@FileId int   
)
AS


				DELETE FROM [dbo].[bbFileStorage] WITH (ROWLOCK) 
				WHERE
					[fileID] = @FileId
					
			

GO
GRANT EXEC ON dbo.znt_bbFileStorage_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_GetByForeignKeyForeignKeyType procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_GetByForeignKeyForeignKeyType') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_GetByForeignKeyForeignKeyType
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbFileStorage table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_GetByForeignKeyForeignKeyType
(

	@ForeignKey int   ,

	@ForeignKeyType int   
)
AS


				SELECT
					[fileID],
					[fileName],
					[fileCaption],
					[FileSize],
					[UploadDate],
					[UploadedByID],
					[UploadedByName],
					[ForeignKey],
					[ForeignKeyType],
					[ForeignKeyTypeDescription]
				FROM
					[dbo].[bbFileStorage]
				WHERE
					[ForeignKey] = @ForeignKey
					AND [ForeignKeyType] = @ForeignKeyType
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbFileStorage_GetByForeignKeyForeignKeyType TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_GetByFileId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_GetByFileId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_GetByFileId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbFileStorage table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_GetByFileId
(

	@FileId int   
)
AS


				SELECT
					[fileID],
					[fileName],
					[fileCaption],
					[FileSize],
					[UploadDate],
					[UploadedByID],
					[UploadedByName],
					[ForeignKey],
					[ForeignKeyType],
					[ForeignKeyTypeDescription]
				FROM
					[dbo].[bbFileStorage]
				WHERE
					[fileID] = @FileId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbFileStorage_GetByFileId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbFileStorage_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbFileStorage_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbFileStorage_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbFileStorage table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbFileStorage_Find
(

	@SearchUsingOR bit   = null ,

	@FileId int   = null ,

	@FileName varchar (255)  = null ,

	@FileCaption varchar (255)  = null ,

	@FileSize int   = null ,

	@UploadDate datetime   = null ,

	@UploadedById int   = null ,

	@UploadedByName varchar (255)  = null ,

	@ForeignKey int   = null ,

	@ForeignKeyType int   = null ,

	@ForeignKeyTypeDescription varchar (255)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [fileID]
	, [fileName]
	, [fileCaption]
	, [FileSize]
	, [UploadDate]
	, [UploadedByID]
	, [UploadedByName]
	, [ForeignKey]
	, [ForeignKeyType]
	, [ForeignKeyTypeDescription]
    FROM
	[dbo].[bbFileStorage]
    WHERE 
	 ([fileID] = @FileId OR @FileId IS NULL)
	AND ([fileName] = @FileName OR @FileName IS NULL)
	AND ([fileCaption] = @FileCaption OR @FileCaption IS NULL)
	AND ([FileSize] = @FileSize OR @FileSize IS NULL)
	AND ([UploadDate] = @UploadDate OR @UploadDate IS NULL)
	AND ([UploadedByID] = @UploadedById OR @UploadedById IS NULL)
	AND ([UploadedByName] = @UploadedByName OR @UploadedByName IS NULL)
	AND ([ForeignKey] = @ForeignKey OR @ForeignKey IS NULL)
	AND ([ForeignKeyType] = @ForeignKeyType OR @ForeignKeyType IS NULL)
	AND ([ForeignKeyTypeDescription] = @ForeignKeyTypeDescription OR @ForeignKeyTypeDescription IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [fileID]
	, [fileName]
	, [fileCaption]
	, [FileSize]
	, [UploadDate]
	, [UploadedByID]
	, [UploadedByName]
	, [ForeignKey]
	, [ForeignKeyType]
	, [ForeignKeyTypeDescription]
    FROM
	[dbo].[bbFileStorage]
    WHERE 
	 ([fileID] = @FileId AND @FileId is not null)
	OR ([fileName] = @FileName AND @FileName is not null)
	OR ([fileCaption] = @FileCaption AND @FileCaption is not null)
	OR ([FileSize] = @FileSize AND @FileSize is not null)
	OR ([UploadDate] = @UploadDate AND @UploadDate is not null)
	OR ([UploadedByID] = @UploadedById AND @UploadedById is not null)
	OR ([UploadedByName] = @UploadedByName AND @UploadedByName is not null)
	OR ([ForeignKey] = @ForeignKey AND @ForeignKey is not null)
	OR ([ForeignKeyType] = @ForeignKeyType AND @ForeignKeyType is not null)
	OR ([ForeignKeyTypeDescription] = @ForeignKeyTypeDescription AND @ForeignKeyTypeDescription is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbFileStorage_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbLesionlkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbLesionlkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbLesionlkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbLesionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbLesionlkp_Get_List

AS


				
				SELECT
					[lesionid],
					[lesiondescription],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbLesionlkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbLesionlkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbLesionlkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbLesionlkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbLesionlkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbLesionlkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbLesionlkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [lesionid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([lesionid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [lesionid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbLesionlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[lesionid], O.[lesiondescription], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbLesionlkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[lesionid] = PageIndex.[lesionid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbLesionlkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbLesionlkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbLesionlkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbLesionlkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbLesionlkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbLesionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbLesionlkp_Insert
(

	@Lesionid int    OUTPUT,

	@Lesiondescription varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbLesionlkp]
					(
					[lesiondescription]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Lesiondescription
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Lesionid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbLesionlkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbLesionlkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbLesionlkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbLesionlkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbLesionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbLesionlkp_Update
(

	@Lesionid int   ,

	@Lesiondescription varchar (50)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbLesionlkp]
				SET
					[lesiondescription] = @Lesiondescription
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[lesionid] = @Lesionid 
				
			

GO
GRANT EXEC ON dbo.znt_bbLesionlkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbLesionlkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbLesionlkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbLesionlkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbLesionlkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbLesionlkp_Delete
(

	@Lesionid int   
)
AS


				DELETE FROM [dbo].[bbLesionlkp] WITH (ROWLOCK) 
				WHERE
					[lesionid] = @Lesionid
					
			

GO
GRANT EXEC ON dbo.znt_bbLesionlkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbLesionlkp_GetByLesionid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbLesionlkp_GetByLesionid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbLesionlkp_GetByLesionid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbLesionlkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbLesionlkp_GetByLesionid
(

	@Lesionid int   
)
AS


				SELECT
					[lesionid],
					[lesiondescription],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbLesionlkp]
				WHERE
					[lesionid] = @Lesionid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbLesionlkp_GetByLesionid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbLesionlkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbLesionlkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbLesionlkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbLesionlkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbLesionlkp_Find
(

	@SearchUsingOR bit   = null ,

	@Lesionid int   = null ,

	@Lesiondescription varchar (50)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [lesionid]
	, [lesiondescription]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbLesionlkp]
    WHERE 
	 ([lesionid] = @Lesionid OR @Lesionid IS NULL)
	AND ([lesiondescription] = @Lesiondescription OR @Lesiondescription IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [lesionid]
	, [lesiondescription]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbLesionlkp]
    WHERE 
	 ([lesionid] = @Lesionid AND @Lesionid is not null)
	OR ([lesiondescription] = @Lesiondescription AND @Lesiondescription is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbLesionlkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientCohortTrackingClinicAttendanceLkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Get_List

AS


				
				SELECT
					[clinicAttendanceId],
					[clinicAttendanceText],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortTrackingClinicAttendanceLkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientCohortTrackingClinicAttendanceLkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [clinicAttendanceId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([clinicAttendanceId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [clinicAttendanceId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortTrackingClinicAttendanceLkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[clinicAttendanceId], O.[clinicAttendanceText], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbPatientCohortTrackingClinicAttendanceLkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[clinicAttendanceId] = PageIndex.[clinicAttendanceId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortTrackingClinicAttendanceLkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientCohortTrackingClinicAttendanceLkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Insert
(

	@ClinicAttendanceId int    OUTPUT,

	@ClinicAttendanceText varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPatientCohortTrackingClinicAttendanceLkp]
					(
					[clinicAttendanceText]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@ClinicAttendanceText
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @ClinicAttendanceId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientCohortTrackingClinicAttendanceLkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Update
(

	@ClinicAttendanceId int   ,

	@ClinicAttendanceText varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientCohortTrackingClinicAttendanceLkp]
				SET
					[clinicAttendanceText] = @ClinicAttendanceText
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[clinicAttendanceId] = @ClinicAttendanceId 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientCohortTrackingClinicAttendanceLkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Delete
(

	@ClinicAttendanceId int   
)
AS


				DELETE FROM [dbo].[bbPatientCohortTrackingClinicAttendanceLkp] WITH (ROWLOCK) 
				WHERE
					[clinicAttendanceId] = @ClinicAttendanceId
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetByClinicAttendanceId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetByClinicAttendanceId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetByClinicAttendanceId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTrackingClinicAttendanceLkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetByClinicAttendanceId
(

	@ClinicAttendanceId int   
)
AS


				SELECT
					[clinicAttendanceId],
					[clinicAttendanceText],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPatientCohortTrackingClinicAttendanceLkp]
				WHERE
					[clinicAttendanceId] = @ClinicAttendanceId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_GetByClinicAttendanceId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientCohortTrackingClinicAttendanceLkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Find
(

	@SearchUsingOR bit   = null ,

	@ClinicAttendanceId int   = null ,

	@ClinicAttendanceText varchar (100)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [clinicAttendanceId]
	, [clinicAttendanceText]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientCohortTrackingClinicAttendanceLkp]
    WHERE 
	 ([clinicAttendanceId] = @ClinicAttendanceId OR @ClinicAttendanceId IS NULL)
	AND ([clinicAttendanceText] = @ClinicAttendanceText OR @ClinicAttendanceText IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [clinicAttendanceId]
	, [clinicAttendanceText]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPatientCohortTrackingClinicAttendanceLkp]
    WHERE 
	 ([clinicAttendanceId] = @ClinicAttendanceId AND @ClinicAttendanceId is not null)
	OR ([clinicAttendanceText] = @ClinicAttendanceText AND @ClinicAttendanceText is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTrackingClinicAttendanceLkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the BbLoginLog table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_Get_List

AS


				
				SELECT
					[RowID],
					[SessionID],
					[BadbirUserID],
					[IP],
					[LoginTime],
					[LogoutTime],
					[isOnline],
					[LastReqTime],
					[UserAgent]
				FROM
					[dbo].[BbLoginLog]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the BbLoginLog table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [RowID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([RowID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [RowID]'
				SET @SQL = @SQL + ' FROM [dbo].[BbLoginLog]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[RowID], O.[SessionID], O.[BadbirUserID], O.[IP], O.[LoginTime], O.[LogoutTime], O.[isOnline], O.[LastReqTime], O.[UserAgent]
				FROM
				    [dbo].[BbLoginLog] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[RowID] = PageIndex.[RowID]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[BbLoginLog]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the BbLoginLog table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_Insert
(

	@RowId int    OUTPUT,

	@SessionId nvarchar (50)  ,

	@BadbirUserId int   ,

	@Ip nvarchar (15)  ,

	@LoginTime datetime   ,

	@LogoutTime datetime   ,

	@IsOnline bit   ,

	@LastReqTime datetime   ,

	@UserAgent nvarchar (1024)  
)
AS


				
				INSERT INTO [dbo].[BbLoginLog]
					(
					[SessionID]
					,[BadbirUserID]
					,[IP]
					,[LoginTime]
					,[LogoutTime]
					,[isOnline]
					,[LastReqTime]
					,[UserAgent]
					)
				VALUES
					(
					@SessionId
					,@BadbirUserId
					,@Ip
					,@LoginTime
					,@LogoutTime
					,@IsOnline
					,@LastReqTime
					,@UserAgent
					)
				-- Get the identity value
				SET @RowId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the BbLoginLog table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_Update
(

	@RowId int   ,

	@SessionId nvarchar (50)  ,

	@BadbirUserId int   ,

	@Ip nvarchar (15)  ,

	@LoginTime datetime   ,

	@LogoutTime datetime   ,

	@IsOnline bit   ,

	@LastReqTime datetime   ,

	@UserAgent nvarchar (1024)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[BbLoginLog]
				SET
					[SessionID] = @SessionId
					,[BadbirUserID] = @BadbirUserId
					,[IP] = @Ip
					,[LoginTime] = @LoginTime
					,[LogoutTime] = @LogoutTime
					,[isOnline] = @IsOnline
					,[LastReqTime] = @LastReqTime
					,[UserAgent] = @UserAgent
				WHERE
[RowID] = @RowId 
				
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the BbLoginLog table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_Delete
(

	@RowId int   
)
AS


				DELETE FROM [dbo].[BbLoginLog] WITH (ROWLOCK) 
				WHERE
					[RowID] = @RowId
					
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_GetByIsOnline procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_GetByIsOnline') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_GetByIsOnline
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the BbLoginLog table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_GetByIsOnline
(

	@IsOnline bit   
)
AS


				SELECT
					[RowID],
					[SessionID],
					[BadbirUserID],
					[IP],
					[LoginTime],
					[LogoutTime],
					[isOnline],
					[LastReqTime],
					[UserAgent]
				FROM
					[dbo].[BbLoginLog]
				WHERE
					[isOnline] = @IsOnline
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_GetByIsOnline TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_GetBySessionIdLogoutTime procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_GetBySessionIdLogoutTime') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_GetBySessionIdLogoutTime
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the BbLoginLog table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_GetBySessionIdLogoutTime
(

	@SessionId nvarchar (50)  ,

	@LogoutTime datetime   
)
AS


				SELECT
					[RowID],
					[SessionID],
					[BadbirUserID],
					[IP],
					[LoginTime],
					[LogoutTime],
					[isOnline],
					[LastReqTime],
					[UserAgent]
				FROM
					[dbo].[BbLoginLog]
				WHERE
					[SessionID] = @SessionId
					AND [LogoutTime] = @LogoutTime
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_GetBySessionIdLogoutTime TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_GetBySessionIdBadbirUserIdIsOnline procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_GetBySessionIdBadbirUserIdIsOnline') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_GetBySessionIdBadbirUserIdIsOnline
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the BbLoginLog table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_GetBySessionIdBadbirUserIdIsOnline
(

	@SessionId nvarchar (50)  ,

	@BadbirUserId int   ,

	@IsOnline bit   
)
AS


				SELECT
					[RowID],
					[SessionID],
					[BadbirUserID],
					[IP],
					[LoginTime],
					[LogoutTime],
					[isOnline],
					[LastReqTime],
					[UserAgent]
				FROM
					[dbo].[BbLoginLog]
				WHERE
					[SessionID] = @SessionId
					AND [BadbirUserID] = @BadbirUserId
					AND [isOnline] = @IsOnline
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_GetBySessionIdBadbirUserIdIsOnline TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_GetByRowId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_GetByRowId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_GetByRowId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the BbLoginLog table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_GetByRowId
(

	@RowId int   
)
AS


				SELECT
					[RowID],
					[SessionID],
					[BadbirUserID],
					[IP],
					[LoginTime],
					[LogoutTime],
					[isOnline],
					[LastReqTime],
					[UserAgent]
				FROM
					[dbo].[BbLoginLog]
				WHERE
					[RowID] = @RowId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_BbLoginLog_GetByRowId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_BbLoginLog_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_BbLoginLog_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_BbLoginLog_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the BbLoginLog table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_BbLoginLog_Find
(

	@SearchUsingOR bit   = null ,

	@RowId int   = null ,

	@SessionId nvarchar (50)  = null ,

	@BadbirUserId int   = null ,

	@Ip nvarchar (15)  = null ,

	@LoginTime datetime   = null ,

	@LogoutTime datetime   = null ,

	@IsOnline bit   = null ,

	@LastReqTime datetime   = null ,

	@UserAgent nvarchar (1024)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [RowID]
	, [SessionID]
	, [BadbirUserID]
	, [IP]
	, [LoginTime]
	, [LogoutTime]
	, [isOnline]
	, [LastReqTime]
	, [UserAgent]
    FROM
	[dbo].[BbLoginLog]
    WHERE 
	 ([RowID] = @RowId OR @RowId IS NULL)
	AND ([SessionID] = @SessionId OR @SessionId IS NULL)
	AND ([BadbirUserID] = @BadbirUserId OR @BadbirUserId IS NULL)
	AND ([IP] = @Ip OR @Ip IS NULL)
	AND ([LoginTime] = @LoginTime OR @LoginTime IS NULL)
	AND ([LogoutTime] = @LogoutTime OR @LogoutTime IS NULL)
	AND ([isOnline] = @IsOnline OR @IsOnline IS NULL)
	AND ([LastReqTime] = @LastReqTime OR @LastReqTime IS NULL)
	AND ([UserAgent] = @UserAgent OR @UserAgent IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [RowID]
	, [SessionID]
	, [BadbirUserID]
	, [IP]
	, [LoginTime]
	, [LogoutTime]
	, [isOnline]
	, [LastReqTime]
	, [UserAgent]
    FROM
	[dbo].[BbLoginLog]
    WHERE 
	 ([RowID] = @RowId AND @RowId is not null)
	OR ([SessionID] = @SessionId AND @SessionId is not null)
	OR ([BadbirUserID] = @BadbirUserId AND @BadbirUserId is not null)
	OR ([IP] = @Ip AND @Ip is not null)
	OR ([LoginTime] = @LoginTime AND @LoginTime is not null)
	OR ([LogoutTime] = @LogoutTime AND @LogoutTime is not null)
	OR ([isOnline] = @IsOnline AND @IsOnline is not null)
	OR ([LastReqTime] = @LastReqTime AND @LastReqTime is not null)
	OR ([UserAgent] = @UserAgent AND @UserAgent is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_BbLoginLog_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbMailingListSubscriptions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_Get_List

AS


				
				SELECT
					[bbMLSid],
					[bbMLid],
					[UserName],
					[EmailAddress],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subsCentreid]
				FROM
					[dbo].[bbMailingListSubscriptions]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbMailingListSubscriptions table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [bbMLSid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([bbMLSid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [bbMLSid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbMailingListSubscriptions]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[bbMLSid], O.[bbMLid], O.[UserName], O.[EmailAddress], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[subsCentreid]
				FROM
				    [dbo].[bbMailingListSubscriptions] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[bbMLSid] = PageIndex.[bbMLSid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbMailingListSubscriptions]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbMailingListSubscriptions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_Insert
(

	@BbMlSid int    OUTPUT,

	@BbMlid int   ,

	@UserName nvarchar (255)  ,

	@EmailAddress nvarchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@SubsCentreid int   
)
AS


				
				INSERT INTO [dbo].[bbMailingListSubscriptions]
					(
					[bbMLid]
					,[UserName]
					,[EmailAddress]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[subsCentreid]
					)
				VALUES
					(
					@BbMlid
					,@UserName
					,@EmailAddress
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@SubsCentreid
					)
				-- Get the identity value
				SET @BbMlSid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbMailingListSubscriptions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_Update
(

	@BbMlSid int   ,

	@BbMlid int   ,

	@UserName nvarchar (255)  ,

	@EmailAddress nvarchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@SubsCentreid int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbMailingListSubscriptions]
				SET
					[bbMLid] = @BbMlid
					,[UserName] = @UserName
					,[EmailAddress] = @EmailAddress
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[subsCentreid] = @SubsCentreid
				WHERE
[bbMLSid] = @BbMlSid 
				
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbMailingListSubscriptions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_Delete
(

	@BbMlSid int   
)
AS


				DELETE FROM [dbo].[bbMailingListSubscriptions] WITH (ROWLOCK) 
				WHERE
					[bbMLSid] = @BbMlSid
					
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_GetByBbMlid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_GetByBbMlid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_GetByBbMlid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbMailingListSubscriptions table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_GetByBbMlid
(

	@BbMlid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[bbMLSid],
					[bbMLid],
					[UserName],
					[EmailAddress],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subsCentreid]
				FROM
					[dbo].[bbMailingListSubscriptions]
				WHERE
					[bbMLid] = @BbMlid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_GetByBbMlid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_GetBySubsCentreid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_GetBySubsCentreid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_GetBySubsCentreid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbMailingListSubscriptions table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_GetBySubsCentreid
(

	@SubsCentreid int   
)
AS


				SELECT
					[bbMLSid],
					[bbMLid],
					[UserName],
					[EmailAddress],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subsCentreid]
				FROM
					[dbo].[bbMailingListSubscriptions]
				WHERE
					[subsCentreid] = @SubsCentreid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_GetBySubsCentreid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_GetByUserName procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_GetByUserName') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_GetByUserName
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbMailingListSubscriptions table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_GetByUserName
(

	@UserName nvarchar (255)  
)
AS


				SELECT
					[bbMLSid],
					[bbMLid],
					[UserName],
					[EmailAddress],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subsCentreid]
				FROM
					[dbo].[bbMailingListSubscriptions]
				WHERE
					[UserName] = @UserName
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_GetByUserName TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_GetByBbMlSid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_GetByBbMlSid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_GetByBbMlSid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbMailingListSubscriptions table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_GetByBbMlSid
(

	@BbMlSid int   
)
AS


				SELECT
					[bbMLSid],
					[bbMLid],
					[UserName],
					[EmailAddress],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[subsCentreid]
				FROM
					[dbo].[bbMailingListSubscriptions]
				WHERE
					[bbMLSid] = @BbMlSid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_GetByBbMlSid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingListSubscriptions_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingListSubscriptions_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingListSubscriptions_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbMailingListSubscriptions table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingListSubscriptions_Find
(

	@SearchUsingOR bit   = null ,

	@BbMlSid int   = null ,

	@BbMlid int   = null ,

	@UserName nvarchar (255)  = null ,

	@EmailAddress nvarchar (100)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@SubsCentreid int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [bbMLSid]
	, [bbMLid]
	, [UserName]
	, [EmailAddress]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [subsCentreid]
    FROM
	[dbo].[bbMailingListSubscriptions]
    WHERE 
	 ([bbMLSid] = @BbMlSid OR @BbMlSid IS NULL)
	AND ([bbMLid] = @BbMlid OR @BbMlid IS NULL)
	AND ([UserName] = @UserName OR @UserName IS NULL)
	AND ([EmailAddress] = @EmailAddress OR @EmailAddress IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([subsCentreid] = @SubsCentreid OR @SubsCentreid IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [bbMLSid]
	, [bbMLid]
	, [UserName]
	, [EmailAddress]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [subsCentreid]
    FROM
	[dbo].[bbMailingListSubscriptions]
    WHERE 
	 ([bbMLSid] = @BbMlSid AND @BbMlSid is not null)
	OR ([bbMLid] = @BbMlid AND @BbMlid is not null)
	OR ([UserName] = @UserName AND @UserName is not null)
	OR ([EmailAddress] = @EmailAddress AND @EmailAddress is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([subsCentreid] = @SubsCentreid AND @SubsCentreid is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbMailingListSubscriptions_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotificationTypelkp_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotificationTypelkp_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotificationTypelkp_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbNotificationTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotificationTypelkp_Get_List

AS


				
				SELECT
					[NTypeID],
					[NType],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbNotificationTypelkp]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbNotificationTypelkp_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotificationTypelkp_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotificationTypelkp_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotificationTypelkp_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbNotificationTypelkp table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotificationTypelkp_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [NTypeID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([NTypeID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [NTypeID]'
				SET @SQL = @SQL + ' FROM [dbo].[bbNotificationTypelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[NTypeID], O.[NType], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbNotificationTypelkp] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[NTypeID] = PageIndex.[NTypeID]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbNotificationTypelkp]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbNotificationTypelkp_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotificationTypelkp_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotificationTypelkp_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotificationTypelkp_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbNotificationTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotificationTypelkp_Insert
(

	@NtypeId int    OUTPUT,

	@Ntype varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbNotificationTypelkp]
					(
					[NType]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@Ntype
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @NtypeId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbNotificationTypelkp_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotificationTypelkp_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotificationTypelkp_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotificationTypelkp_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbNotificationTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotificationTypelkp_Update
(

	@NtypeId int   ,

	@Ntype varchar (100)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbNotificationTypelkp]
				SET
					[NType] = @Ntype
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[NTypeID] = @NtypeId 
				
			

GO
GRANT EXEC ON dbo.znt_bbNotificationTypelkp_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotificationTypelkp_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotificationTypelkp_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotificationTypelkp_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbNotificationTypelkp table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotificationTypelkp_Delete
(

	@NtypeId int   
)
AS


				DELETE FROM [dbo].[bbNotificationTypelkp] WITH (ROWLOCK) 
				WHERE
					[NTypeID] = @NtypeId
					
			

GO
GRANT EXEC ON dbo.znt_bbNotificationTypelkp_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotificationTypelkp_GetByNtypeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotificationTypelkp_GetByNtypeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotificationTypelkp_GetByNtypeId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbNotificationTypelkp table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotificationTypelkp_GetByNtypeId
(

	@NtypeId int   
)
AS


				SELECT
					[NTypeID],
					[NType],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbNotificationTypelkp]
				WHERE
					[NTypeID] = @NtypeId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbNotificationTypelkp_GetByNtypeId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotificationTypelkp_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotificationTypelkp_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotificationTypelkp_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbNotificationTypelkp table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotificationTypelkp_Find
(

	@SearchUsingOR bit   = null ,

	@NtypeId int   = null ,

	@Ntype varchar (100)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [NTypeID]
	, [NType]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbNotificationTypelkp]
    WHERE 
	 ([NTypeID] = @NtypeId OR @NtypeId IS NULL)
	AND ([NType] = @Ntype OR @Ntype IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [NTypeID]
	, [NType]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbNotificationTypelkp]
    WHERE 
	 ([NTypeID] = @NtypeId AND @NtypeId is not null)
	OR ([NType] = @Ntype AND @Ntype is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbNotificationTypelkp_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbNotification table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_Get_List

AS


				
				SELECT
					[Nid],
					[InboxID],
					[TypeID],
					[Unread],
					[DateRead],
					[StatusID],
					[DateArchived],
					[GroupID],
					[Chid],
					[BADBIRUserID],
					[HighPriority],
					[NText],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbNotification]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbNotification_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbNotification table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Nid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Nid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [Nid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbNotification]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[Nid], O.[InboxID], O.[TypeID], O.[Unread], O.[DateRead], O.[StatusID], O.[DateArchived], O.[GroupID], O.[Chid], O.[BADBIRUserID], O.[HighPriority], O.[NText], O.[include], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbNotification] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Nid] = PageIndex.[Nid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbNotification]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbNotification_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbNotification table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_Insert
(

	@Nid int    OUTPUT,

	@InboxId tinyint   ,

	@TypeId int   ,

	@Unread bit   ,

	@DateRead datetime   ,

	@StatusId bit   ,

	@DateArchived datetime   ,

	@GroupId int   ,

	@Chid int   ,

	@BadbirUserId int   ,

	@HighPriority bit   ,

	@Ntext varchar (MAX)  ,

	@Include bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbNotification]
					(
					[InboxID]
					,[TypeID]
					,[Unread]
					,[DateRead]
					,[StatusID]
					,[DateArchived]
					,[GroupID]
					,[Chid]
					,[BADBIRUserID]
					,[HighPriority]
					,[NText]
					,[include]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@InboxId
					,@TypeId
					,@Unread
					,@DateRead
					,@StatusId
					,@DateArchived
					,@GroupId
					,@Chid
					,@BadbirUserId
					,@HighPriority
					,@Ntext
					,@Include
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
				-- Get the identity value
				SET @Nid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbNotification_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbNotification table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_Update
(

	@Nid int   ,

	@InboxId tinyint   ,

	@TypeId int   ,

	@Unread bit   ,

	@DateRead datetime   ,

	@StatusId bit   ,

	@DateArchived datetime   ,

	@GroupId int   ,

	@Chid int   ,

	@BadbirUserId int   ,

	@HighPriority bit   ,

	@Ntext varchar (MAX)  ,

	@Include bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbNotification]
				SET
					[InboxID] = @InboxId
					,[TypeID] = @TypeId
					,[Unread] = @Unread
					,[DateRead] = @DateRead
					,[StatusID] = @StatusId
					,[DateArchived] = @DateArchived
					,[GroupID] = @GroupId
					,[Chid] = @Chid
					,[BADBIRUserID] = @BadbirUserId
					,[HighPriority] = @HighPriority
					,[NText] = @Ntext
					,[include] = @Include
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[Nid] = @Nid 
				
			

GO
GRANT EXEC ON dbo.znt_bbNotification_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbNotification table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_Delete
(

	@Nid int   
)
AS


				DELETE FROM [dbo].[bbNotification] WITH (ROWLOCK) 
				WHERE
					[Nid] = @Nid
					
			

GO
GRANT EXEC ON dbo.znt_bbNotification_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_GetByTypeId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_GetByTypeId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_GetByTypeId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbNotification table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_GetByTypeId
(

	@TypeId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[Nid],
					[InboxID],
					[TypeID],
					[Unread],
					[DateRead],
					[StatusID],
					[DateArchived],
					[GroupID],
					[Chid],
					[BADBIRUserID],
					[HighPriority],
					[NText],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbNotification]
				WHERE
					[TypeID] = @TypeId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbNotification_GetByTypeId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_GetByNid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_GetByNid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_GetByNid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbNotification table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_GetByNid
(

	@Nid int   
)
AS


				SELECT
					[Nid],
					[InboxID],
					[TypeID],
					[Unread],
					[DateRead],
					[StatusID],
					[DateArchived],
					[GroupID],
					[Chid],
					[BADBIRUserID],
					[HighPriority],
					[NText],
					[include],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbNotification]
				WHERE
					[Nid] = @Nid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbNotification_GetByNid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbNotification_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbNotification_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbNotification_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbNotification table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbNotification_Find
(

	@SearchUsingOR bit   = null ,

	@Nid int   = null ,

	@InboxId tinyint   = null ,

	@TypeId int   = null ,

	@Unread bit   = null ,

	@DateRead datetime   = null ,

	@StatusId bit   = null ,

	@DateArchived datetime   = null ,

	@GroupId int   = null ,

	@Chid int   = null ,

	@BadbirUserId int   = null ,

	@HighPriority bit   = null ,

	@Ntext varchar (MAX)  = null ,

	@Include bit   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Nid]
	, [InboxID]
	, [TypeID]
	, [Unread]
	, [DateRead]
	, [StatusID]
	, [DateArchived]
	, [GroupID]
	, [Chid]
	, [BADBIRUserID]
	, [HighPriority]
	, [NText]
	, [include]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbNotification]
    WHERE 
	 ([Nid] = @Nid OR @Nid IS NULL)
	AND ([InboxID] = @InboxId OR @InboxId IS NULL)
	AND ([TypeID] = @TypeId OR @TypeId IS NULL)
	AND ([Unread] = @Unread OR @Unread IS NULL)
	AND ([DateRead] = @DateRead OR @DateRead IS NULL)
	AND ([StatusID] = @StatusId OR @StatusId IS NULL)
	AND ([DateArchived] = @DateArchived OR @DateArchived IS NULL)
	AND ([GroupID] = @GroupId OR @GroupId IS NULL)
	AND ([Chid] = @Chid OR @Chid IS NULL)
	AND ([BADBIRUserID] = @BadbirUserId OR @BadbirUserId IS NULL)
	AND ([HighPriority] = @HighPriority OR @HighPriority IS NULL)
	AND ([NText] = @Ntext OR @Ntext IS NULL)
	AND ([include] = @Include OR @Include IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Nid]
	, [InboxID]
	, [TypeID]
	, [Unread]
	, [DateRead]
	, [StatusID]
	, [DateArchived]
	, [GroupID]
	, [Chid]
	, [BADBIRUserID]
	, [HighPriority]
	, [NText]
	, [include]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbNotification]
    WHERE 
	 ([Nid] = @Nid AND @Nid is not null)
	OR ([InboxID] = @InboxId AND @InboxId is not null)
	OR ([TypeID] = @TypeId AND @TypeId is not null)
	OR ([Unread] = @Unread AND @Unread is not null)
	OR ([DateRead] = @DateRead AND @DateRead is not null)
	OR ([StatusID] = @StatusId AND @StatusId is not null)
	OR ([DateArchived] = @DateArchived AND @DateArchived is not null)
	OR ([GroupID] = @GroupId AND @GroupId is not null)
	OR ([Chid] = @Chid AND @Chid is not null)
	OR ([BADBIRUserID] = @BadbirUserId AND @BadbirUserId is not null)
	OR ([HighPriority] = @HighPriority AND @HighPriority is not null)
	OR ([NText] = @Ntext AND @Ntext is not null)
	OR ([include] = @Include AND @Include is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbNotification_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPappPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_Get_List

AS


				
				SELECT
					[PappFupId],
					[PatientId],
					[potentialFupCode],
					[importedFupId],
					[visitdate],
					[clinicAttendance],
					[dateentered],
					[comments],
					[pappFupStatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[checkedbyid],
					[checkedbyname],
					[checkedbydate]
				FROM
					[dbo].[bbPappPatientCohortTracking]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPappPatientCohortTracking table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [PappFupId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PappFupId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [PappFupId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientCohortTracking]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[PappFupId], O.[PatientId], O.[potentialFupCode], O.[importedFupId], O.[visitdate], O.[clinicAttendance], O.[dateentered], O.[comments], O.[pappFupStatus], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[checkedbyid], O.[checkedbyname], O.[checkedbydate]
				FROM
				    [dbo].[bbPappPatientCohortTracking] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PappFupId] = PageIndex.[PappFupId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientCohortTracking]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPappPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_Insert
(

	@PappFupId int    OUTPUT,

	@PatientId int   ,

	@PotentialFupCode int   ,

	@ImportedFupId int   ,

	@Visitdate datetime   ,

	@ClinicAttendance int   ,

	@Dateentered datetime   ,

	@Comments text   ,

	@PappFupStatus int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Checkedbyid int   ,

	@Checkedbyname varchar (100)  ,

	@Checkedbydate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPappPatientCohortTracking]
					(
					[PatientId]
					,[potentialFupCode]
					,[importedFupId]
					,[visitdate]
					,[clinicAttendance]
					,[dateentered]
					,[comments]
					,[pappFupStatus]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[checkedbyid]
					,[checkedbyname]
					,[checkedbydate]
					)
				VALUES
					(
					@PatientId
					,@PotentialFupCode
					,@ImportedFupId
					,@Visitdate
					,@ClinicAttendance
					,@Dateentered
					,@Comments
					,@PappFupStatus
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@Checkedbyid
					,@Checkedbyname
					,@Checkedbydate
					)
				-- Get the identity value
				SET @PappFupId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPappPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_Update
(

	@PappFupId int   ,

	@PatientId int   ,

	@PotentialFupCode int   ,

	@ImportedFupId int   ,

	@Visitdate datetime   ,

	@ClinicAttendance int   ,

	@Dateentered datetime   ,

	@Comments text   ,

	@PappFupStatus int   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Checkedbyid int   ,

	@Checkedbyname varchar (100)  ,

	@Checkedbydate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPappPatientCohortTracking]
				SET
					[PatientId] = @PatientId
					,[potentialFupCode] = @PotentialFupCode
					,[importedFupId] = @ImportedFupId
					,[visitdate] = @Visitdate
					,[clinicAttendance] = @ClinicAttendance
					,[dateentered] = @Dateentered
					,[comments] = @Comments
					,[pappFupStatus] = @PappFupStatus
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[checkedbyid] = @Checkedbyid
					,[checkedbyname] = @Checkedbyname
					,[checkedbydate] = @Checkedbydate
				WHERE
[PappFupId] = @PappFupId 
				
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPappPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_Delete
(

	@PappFupId int   
)
AS


				DELETE FROM [dbo].[bbPappPatientCohortTracking] WITH (ROWLOCK) 
				WHERE
					[PappFupId] = @PappFupId
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_GetByPatientId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_GetByPatientId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByPatientId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientCohortTracking table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByPatientId
(

	@PatientId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PappFupId],
					[PatientId],
					[potentialFupCode],
					[importedFupId],
					[visitdate],
					[clinicAttendance],
					[dateentered],
					[comments],
					[pappFupStatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[checkedbyid],
					[checkedbyname],
					[checkedbydate]
				FROM
					[dbo].[bbPappPatientCohortTracking]
				WHERE
					[PatientId] = @PatientId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_GetByPatientId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_GetByImportedFupId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_GetByImportedFupId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByImportedFupId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientCohortTracking table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByImportedFupId
(

	@ImportedFupId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PappFupId],
					[PatientId],
					[potentialFupCode],
					[importedFupId],
					[visitdate],
					[clinicAttendance],
					[dateentered],
					[comments],
					[pappFupStatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[checkedbyid],
					[checkedbyname],
					[checkedbydate]
				FROM
					[dbo].[bbPappPatientCohortTracking]
				WHERE
					[importedFupId] = @ImportedFupId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_GetByImportedFupId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_GetByClinicAttendance procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_GetByClinicAttendance') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByClinicAttendance
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientCohortTracking table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByClinicAttendance
(

	@ClinicAttendance int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PappFupId],
					[PatientId],
					[potentialFupCode],
					[importedFupId],
					[visitdate],
					[clinicAttendance],
					[dateentered],
					[comments],
					[pappFupStatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[checkedbyid],
					[checkedbyname],
					[checkedbydate]
				FROM
					[dbo].[bbPappPatientCohortTracking]
				WHERE
					[clinicAttendance] = @ClinicAttendance
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_GetByClinicAttendance TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_GetByPappFupId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_GetByPappFupId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByPappFupId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientCohortTracking table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_GetByPappFupId
(

	@PappFupId int   
)
AS


				SELECT
					[PappFupId],
					[PatientId],
					[potentialFupCode],
					[importedFupId],
					[visitdate],
					[clinicAttendance],
					[dateentered],
					[comments],
					[pappFupStatus],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[checkedbyid],
					[checkedbyname],
					[checkedbydate]
				FROM
					[dbo].[bbPappPatientCohortTracking]
				WHERE
					[PappFupId] = @PappFupId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_GetByPappFupId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientCohortTracking_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientCohortTracking_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientCohortTracking_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPappPatientCohortTracking table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientCohortTracking_Find
(

	@SearchUsingOR bit   = null ,

	@PappFupId int   = null ,

	@PatientId int   = null ,

	@PotentialFupCode int   = null ,

	@ImportedFupId int   = null ,

	@Visitdate datetime   = null ,

	@ClinicAttendance int   = null ,

	@Dateentered datetime   = null ,

	@Comments text   = null ,

	@PappFupStatus int   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@Checkedbyid int   = null ,

	@Checkedbyname varchar (100)  = null ,

	@Checkedbydate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PappFupId]
	, [PatientId]
	, [potentialFupCode]
	, [importedFupId]
	, [visitdate]
	, [clinicAttendance]
	, [dateentered]
	, [comments]
	, [pappFupStatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [checkedbyid]
	, [checkedbyname]
	, [checkedbydate]
    FROM
	[dbo].[bbPappPatientCohortTracking]
    WHERE 
	 ([PappFupId] = @PappFupId OR @PappFupId IS NULL)
	AND ([PatientId] = @PatientId OR @PatientId IS NULL)
	AND ([potentialFupCode] = @PotentialFupCode OR @PotentialFupCode IS NULL)
	AND ([importedFupId] = @ImportedFupId OR @ImportedFupId IS NULL)
	AND ([visitdate] = @Visitdate OR @Visitdate IS NULL)
	AND ([clinicAttendance] = @ClinicAttendance OR @ClinicAttendance IS NULL)
	AND ([dateentered] = @Dateentered OR @Dateentered IS NULL)
	AND ([pappFupStatus] = @PappFupStatus OR @PappFupStatus IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([checkedbyid] = @Checkedbyid OR @Checkedbyid IS NULL)
	AND ([checkedbyname] = @Checkedbyname OR @Checkedbyname IS NULL)
	AND ([checkedbydate] = @Checkedbydate OR @Checkedbydate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PappFupId]
	, [PatientId]
	, [potentialFupCode]
	, [importedFupId]
	, [visitdate]
	, [clinicAttendance]
	, [dateentered]
	, [comments]
	, [pappFupStatus]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [checkedbyid]
	, [checkedbyname]
	, [checkedbydate]
    FROM
	[dbo].[bbPappPatientCohortTracking]
    WHERE 
	 ([PappFupId] = @PappFupId AND @PappFupId is not null)
	OR ([PatientId] = @PatientId AND @PatientId is not null)
	OR ([potentialFupCode] = @PotentialFupCode AND @PotentialFupCode is not null)
	OR ([importedFupId] = @ImportedFupId AND @ImportedFupId is not null)
	OR ([visitdate] = @Visitdate AND @Visitdate is not null)
	OR ([clinicAttendance] = @ClinicAttendance AND @ClinicAttendance is not null)
	OR ([dateentered] = @Dateentered AND @Dateentered is not null)
	OR ([pappFupStatus] = @PappFupStatus AND @PappFupStatus is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([checkedbyid] = @Checkedbyid AND @Checkedbyid is not null)
	OR ([checkedbyname] = @Checkedbyname AND @Checkedbyname is not null)
	OR ([checkedbydate] = @Checkedbydate AND @Checkedbydate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPappPatientCohortTracking_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPappPatientDLQI table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_Get_List

AS


				
				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[diagnosis],
					[itchsore_score],
					[embsc_score],
					[shophg_score],
					[clothes_score],
					[socleis_score],
					[sport_score],
					[workstud_score],
					[workstudno_score],
					[partcrf_score],
					[sexdif_score],
					[treatment_score],
					[total_score],
					[datecomp],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[skipBreakup],
					[dlqiMissing]
				FROM
					[dbo].[bbPappPatientDLQI]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPappPatientDLQI table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [FormID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([FormID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [FormID]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientDLQI]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[FormID], O.[chid], O.[PappFupId], O.[diagnosis], O.[itchsore_score], O.[embsc_score], O.[shophg_score], O.[clothes_score], O.[socleis_score], O.[sport_score], O.[workstud_score], O.[workstudno_score], O.[partcrf_score], O.[sexdif_score], O.[treatment_score], O.[total_score], O.[datecomp], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[skipBreakup], O.[dlqiMissing]
				FROM
				    [dbo].[bbPappPatientDLQI] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[FormID] = PageIndex.[FormID]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientDLQI]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPappPatientDLQI table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_Insert
(

	@FormId int   ,

	@Chid int   ,

	@PappFupId int   ,

	@Diagnosis varchar (50)  ,

	@ItchsoreScore int   ,

	@EmbscScore int   ,

	@ShophgScore int   ,

	@ClothesScore int   ,

	@SocleisScore int   ,

	@SportScore int   ,

	@WorkstudScore int   ,

	@WorkstudnoScore int   ,

	@PartcrfScore int   ,

	@SexdifScore int   ,

	@TreatmentScore int   ,

	@TotalScore int   ,

	@Datecomp datetime   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@SkipBreakup int   ,

	@DlqiMissing bit   
)
AS


				
				INSERT INTO [dbo].[bbPappPatientDLQI]
					(
					[FormID]
					,[chid]
					,[PappFupId]
					,[diagnosis]
					,[itchsore_score]
					,[embsc_score]
					,[shophg_score]
					,[clothes_score]
					,[socleis_score]
					,[sport_score]
					,[workstud_score]
					,[workstudno_score]
					,[partcrf_score]
					,[sexdif_score]
					,[treatment_score]
					,[total_score]
					,[datecomp]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[skipBreakup]
					,[dlqiMissing]
					)
				VALUES
					(
					@FormId
					,@Chid
					,@PappFupId
					,@Diagnosis
					,@ItchsoreScore
					,@EmbscScore
					,@ShophgScore
					,@ClothesScore
					,@SocleisScore
					,@SportScore
					,@WorkstudScore
					,@WorkstudnoScore
					,@PartcrfScore
					,@SexdifScore
					,@TreatmentScore
					,@TotalScore
					,@Datecomp
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@SkipBreakup
					,@DlqiMissing
					)
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPappPatientDLQI table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_Update
(

	@FormId int   ,

	@OriginalFormId int   ,

	@Chid int   ,

	@PappFupId int   ,

	@Diagnosis varchar (50)  ,

	@ItchsoreScore int   ,

	@EmbscScore int   ,

	@ShophgScore int   ,

	@ClothesScore int   ,

	@SocleisScore int   ,

	@SportScore int   ,

	@WorkstudScore int   ,

	@WorkstudnoScore int   ,

	@PartcrfScore int   ,

	@SexdifScore int   ,

	@TreatmentScore int   ,

	@TotalScore int   ,

	@Datecomp datetime   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@SkipBreakup int   ,

	@DlqiMissing bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPappPatientDLQI]
				SET
					[FormID] = @FormId
					,[chid] = @Chid
					,[PappFupId] = @PappFupId
					,[diagnosis] = @Diagnosis
					,[itchsore_score] = @ItchsoreScore
					,[embsc_score] = @EmbscScore
					,[shophg_score] = @ShophgScore
					,[clothes_score] = @ClothesScore
					,[socleis_score] = @SocleisScore
					,[sport_score] = @SportScore
					,[workstud_score] = @WorkstudScore
					,[workstudno_score] = @WorkstudnoScore
					,[partcrf_score] = @PartcrfScore
					,[sexdif_score] = @SexdifScore
					,[treatment_score] = @TreatmentScore
					,[total_score] = @TotalScore
					,[datecomp] = @Datecomp
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[skipBreakup] = @SkipBreakup
					,[dlqiMissing] = @DlqiMissing
				WHERE
[FormID] = @OriginalFormId 
				
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPappPatientDLQI table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_Delete
(

	@FormId int   
)
AS


				DELETE FROM [dbo].[bbPappPatientDLQI] WITH (ROWLOCK) 
				WHERE
					[FormID] = @FormId
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_GetByChid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_GetByChid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_GetByChid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientDLQI table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_GetByChid
(

	@Chid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[diagnosis],
					[itchsore_score],
					[embsc_score],
					[shophg_score],
					[clothes_score],
					[socleis_score],
					[sport_score],
					[workstud_score],
					[workstudno_score],
					[partcrf_score],
					[sexdif_score],
					[treatment_score],
					[total_score],
					[datecomp],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[skipBreakup],
					[dlqiMissing]
				FROM
					[dbo].[bbPappPatientDLQI]
				WHERE
					[chid] = @Chid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_GetByChid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_GetByFormId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_GetByFormId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_GetByFormId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientDLQI table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_GetByFormId
(

	@FormId int   
)
AS


				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[diagnosis],
					[itchsore_score],
					[embsc_score],
					[shophg_score],
					[clothes_score],
					[socleis_score],
					[sport_score],
					[workstud_score],
					[workstudno_score],
					[partcrf_score],
					[sexdif_score],
					[treatment_score],
					[total_score],
					[datecomp],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[skipBreakup],
					[dlqiMissing]
				FROM
					[dbo].[bbPappPatientDLQI]
				WHERE
					[FormID] = @FormId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_GetByFormId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientDLQI_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientDLQI_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientDLQI_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPappPatientDLQI table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientDLQI_Find
(

	@SearchUsingOR bit   = null ,

	@FormId int   = null ,

	@Chid int   = null ,

	@PappFupId int   = null ,

	@Diagnosis varchar (50)  = null ,

	@ItchsoreScore int   = null ,

	@EmbscScore int   = null ,

	@ShophgScore int   = null ,

	@ClothesScore int   = null ,

	@SocleisScore int   = null ,

	@SportScore int   = null ,

	@WorkstudScore int   = null ,

	@WorkstudnoScore int   = null ,

	@PartcrfScore int   = null ,

	@SexdifScore int   = null ,

	@TreatmentScore int   = null ,

	@TotalScore int   = null ,

	@Datecomp datetime   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@SkipBreakup int   = null ,

	@DlqiMissing bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FormID]
	, [chid]
	, [PappFupId]
	, [diagnosis]
	, [itchsore_score]
	, [embsc_score]
	, [shophg_score]
	, [clothes_score]
	, [socleis_score]
	, [sport_score]
	, [workstud_score]
	, [workstudno_score]
	, [partcrf_score]
	, [sexdif_score]
	, [treatment_score]
	, [total_score]
	, [datecomp]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [skipBreakup]
	, [dlqiMissing]
    FROM
	[dbo].[bbPappPatientDLQI]
    WHERE 
	 ([FormID] = @FormId OR @FormId IS NULL)
	AND ([chid] = @Chid OR @Chid IS NULL)
	AND ([PappFupId] = @PappFupId OR @PappFupId IS NULL)
	AND ([diagnosis] = @Diagnosis OR @Diagnosis IS NULL)
	AND ([itchsore_score] = @ItchsoreScore OR @ItchsoreScore IS NULL)
	AND ([embsc_score] = @EmbscScore OR @EmbscScore IS NULL)
	AND ([shophg_score] = @ShophgScore OR @ShophgScore IS NULL)
	AND ([clothes_score] = @ClothesScore OR @ClothesScore IS NULL)
	AND ([socleis_score] = @SocleisScore OR @SocleisScore IS NULL)
	AND ([sport_score] = @SportScore OR @SportScore IS NULL)
	AND ([workstud_score] = @WorkstudScore OR @WorkstudScore IS NULL)
	AND ([workstudno_score] = @WorkstudnoScore OR @WorkstudnoScore IS NULL)
	AND ([partcrf_score] = @PartcrfScore OR @PartcrfScore IS NULL)
	AND ([sexdif_score] = @SexdifScore OR @SexdifScore IS NULL)
	AND ([treatment_score] = @TreatmentScore OR @TreatmentScore IS NULL)
	AND ([total_score] = @TotalScore OR @TotalScore IS NULL)
	AND ([datecomp] = @Datecomp OR @Datecomp IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([skipBreakup] = @SkipBreakup OR @SkipBreakup IS NULL)
	AND ([dlqiMissing] = @DlqiMissing OR @DlqiMissing IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [FormID]
	, [chid]
	, [PappFupId]
	, [diagnosis]
	, [itchsore_score]
	, [embsc_score]
	, [shophg_score]
	, [clothes_score]
	, [socleis_score]
	, [sport_score]
	, [workstud_score]
	, [workstudno_score]
	, [partcrf_score]
	, [sexdif_score]
	, [treatment_score]
	, [total_score]
	, [datecomp]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [skipBreakup]
	, [dlqiMissing]
    FROM
	[dbo].[bbPappPatientDLQI]
    WHERE 
	 ([FormID] = @FormId AND @FormId is not null)
	OR ([chid] = @Chid AND @Chid is not null)
	OR ([PappFupId] = @PappFupId AND @PappFupId is not null)
	OR ([diagnosis] = @Diagnosis AND @Diagnosis is not null)
	OR ([itchsore_score] = @ItchsoreScore AND @ItchsoreScore is not null)
	OR ([embsc_score] = @EmbscScore AND @EmbscScore is not null)
	OR ([shophg_score] = @ShophgScore AND @ShophgScore is not null)
	OR ([clothes_score] = @ClothesScore AND @ClothesScore is not null)
	OR ([socleis_score] = @SocleisScore AND @SocleisScore is not null)
	OR ([sport_score] = @SportScore AND @SportScore is not null)
	OR ([workstud_score] = @WorkstudScore AND @WorkstudScore is not null)
	OR ([workstudno_score] = @WorkstudnoScore AND @WorkstudnoScore is not null)
	OR ([partcrf_score] = @PartcrfScore AND @PartcrfScore is not null)
	OR ([sexdif_score] = @SexdifScore AND @SexdifScore is not null)
	OR ([treatment_score] = @TreatmentScore AND @TreatmentScore is not null)
	OR ([total_score] = @TotalScore AND @TotalScore is not null)
	OR ([datecomp] = @Datecomp AND @Datecomp is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([skipBreakup] = @SkipBreakup AND @SkipBreakup is not null)
	OR ([dlqiMissing] = @DlqiMissing AND @DlqiMissing is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPappPatientDLQI_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPappPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_Get_List

AS


				
				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[workstatusid],
					[occupation],
					[smokingMissing],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drinkingMissing],
					[drinkalcohol],
					[drnkunitsavg],
					[datecompleted],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPappPatientLifestyle]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPappPatientLifestyle table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [FormID] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([FormID])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [FormID]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientLifestyle]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[FormID], O.[chid], O.[PappFupId], O.[admittedtohospital], O.[newdrugs], O.[newclinics], O.[workstatusid], O.[occupation], O.[smokingMissing], O.[currentlysmoke], O.[currentlysmokenumbercigsperday], O.[drinkingMissing], O.[drinkalcohol], O.[drnkunitsavg], O.[datecompleted], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate]
				FROM
				    [dbo].[bbPappPatientLifestyle] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[FormID] = PageIndex.[FormID]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPappPatientLifestyle]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPappPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_Insert
(

	@FormId int   ,

	@Chid int   ,

	@PappFupId int   ,

	@Admittedtohospital int   ,

	@Newdrugs int   ,

	@Newclinics int   ,

	@Workstatusid int   ,

	@Occupation varchar (50)  ,

	@SmokingMissing bit   ,

	@Currentlysmoke bit   ,

	@Currentlysmokenumbercigsperday int   ,

	@DrinkingMissing bit   ,

	@Drinkalcohol bit   ,

	@Drnkunitsavg int   ,

	@Datecompleted datetime   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				INSERT INTO [dbo].[bbPappPatientLifestyle]
					(
					[FormID]
					,[chid]
					,[PappFupId]
					,[admittedtohospital]
					,[newdrugs]
					,[newclinics]
					,[workstatusid]
					,[occupation]
					,[smokingMissing]
					,[currentlysmoke]
					,[currentlysmokenumbercigsperday]
					,[drinkingMissing]
					,[drinkalcohol]
					,[drnkunitsavg]
					,[datecompleted]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					)
				VALUES
					(
					@FormId
					,@Chid
					,@PappFupId
					,@Admittedtohospital
					,@Newdrugs
					,@Newclinics
					,@Workstatusid
					,@Occupation
					,@SmokingMissing
					,@Currentlysmoke
					,@Currentlysmokenumbercigsperday
					,@DrinkingMissing
					,@Drinkalcohol
					,@Drnkunitsavg
					,@Datecompleted
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					)
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPappPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_Update
(

	@FormId int   ,

	@OriginalFormId int   ,

	@Chid int   ,

	@PappFupId int   ,

	@Admittedtohospital int   ,

	@Newdrugs int   ,

	@Newclinics int   ,

	@Workstatusid int   ,

	@Occupation varchar (50)  ,

	@SmokingMissing bit   ,

	@Currentlysmoke bit   ,

	@Currentlysmokenumbercigsperday int   ,

	@DrinkingMissing bit   ,

	@Drinkalcohol bit   ,

	@Drnkunitsavg int   ,

	@Datecompleted datetime   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPappPatientLifestyle]
				SET
					[FormID] = @FormId
					,[chid] = @Chid
					,[PappFupId] = @PappFupId
					,[admittedtohospital] = @Admittedtohospital
					,[newdrugs] = @Newdrugs
					,[newclinics] = @Newclinics
					,[workstatusid] = @Workstatusid
					,[occupation] = @Occupation
					,[smokingMissing] = @SmokingMissing
					,[currentlysmoke] = @Currentlysmoke
					,[currentlysmokenumbercigsperday] = @Currentlysmokenumbercigsperday
					,[drinkingMissing] = @DrinkingMissing
					,[drinkalcohol] = @Drinkalcohol
					,[drnkunitsavg] = @Drnkunitsavg
					,[datecompleted] = @Datecompleted
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
				WHERE
[FormID] = @OriginalFormId 
				
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPappPatientLifestyle table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_Delete
(

	@FormId int   
)
AS


				DELETE FROM [dbo].[bbPappPatientLifestyle] WITH (ROWLOCK) 
				WHERE
					[FormID] = @FormId
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_GetByWorkstatusid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_GetByWorkstatusid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_GetByWorkstatusid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientLifestyle table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_GetByWorkstatusid
(

	@Workstatusid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[workstatusid],
					[occupation],
					[smokingMissing],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drinkingMissing],
					[drinkalcohol],
					[drnkunitsavg],
					[datecompleted],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPappPatientLifestyle]
				WHERE
					[workstatusid] = @Workstatusid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_GetByWorkstatusid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_GetByChid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_GetByChid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_GetByChid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientLifestyle table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_GetByChid
(

	@Chid int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[workstatusid],
					[occupation],
					[smokingMissing],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drinkingMissing],
					[drinkalcohol],
					[drnkunitsavg],
					[datecompleted],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPappPatientLifestyle]
				WHERE
					[chid] = @Chid
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_GetByChid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_GetByFormId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_GetByFormId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_GetByFormId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPappPatientLifestyle table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_GetByFormId
(

	@FormId int   
)
AS


				SELECT
					[FormID],
					[chid],
					[PappFupId],
					[admittedtohospital],
					[newdrugs],
					[newclinics],
					[workstatusid],
					[occupation],
					[smokingMissing],
					[currentlysmoke],
					[currentlysmokenumbercigsperday],
					[drinkingMissing],
					[drinkalcohol],
					[drnkunitsavg],
					[datecompleted],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate]
				FROM
					[dbo].[bbPappPatientLifestyle]
				WHERE
					[FormID] = @FormId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_GetByFormId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPappPatientLifestyle_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPappPatientLifestyle_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPappPatientLifestyle_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPappPatientLifestyle table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPappPatientLifestyle_Find
(

	@SearchUsingOR bit   = null ,

	@FormId int   = null ,

	@Chid int   = null ,

	@PappFupId int   = null ,

	@Admittedtohospital int   = null ,

	@Newdrugs int   = null ,

	@Newclinics int   = null ,

	@Workstatusid int   = null ,

	@Occupation varchar (50)  = null ,

	@SmokingMissing bit   = null ,

	@Currentlysmoke bit   = null ,

	@Currentlysmokenumbercigsperday int   = null ,

	@DrinkingMissing bit   = null ,

	@Drinkalcohol bit   = null ,

	@Drnkunitsavg int   = null ,

	@Datecompleted datetime   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FormID]
	, [chid]
	, [PappFupId]
	, [admittedtohospital]
	, [newdrugs]
	, [newclinics]
	, [workstatusid]
	, [occupation]
	, [smokingMissing]
	, [currentlysmoke]
	, [currentlysmokenumbercigsperday]
	, [drinkingMissing]
	, [drinkalcohol]
	, [drnkunitsavg]
	, [datecompleted]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPappPatientLifestyle]
    WHERE 
	 ([FormID] = @FormId OR @FormId IS NULL)
	AND ([chid] = @Chid OR @Chid IS NULL)
	AND ([PappFupId] = @PappFupId OR @PappFupId IS NULL)
	AND ([admittedtohospital] = @Admittedtohospital OR @Admittedtohospital IS NULL)
	AND ([newdrugs] = @Newdrugs OR @Newdrugs IS NULL)
	AND ([newclinics] = @Newclinics OR @Newclinics IS NULL)
	AND ([workstatusid] = @Workstatusid OR @Workstatusid IS NULL)
	AND ([occupation] = @Occupation OR @Occupation IS NULL)
	AND ([smokingMissing] = @SmokingMissing OR @SmokingMissing IS NULL)
	AND ([currentlysmoke] = @Currentlysmoke OR @Currentlysmoke IS NULL)
	AND ([currentlysmokenumbercigsperday] = @Currentlysmokenumbercigsperday OR @Currentlysmokenumbercigsperday IS NULL)
	AND ([drinkingMissing] = @DrinkingMissing OR @DrinkingMissing IS NULL)
	AND ([drinkalcohol] = @Drinkalcohol OR @Drinkalcohol IS NULL)
	AND ([drnkunitsavg] = @Drnkunitsavg OR @Drnkunitsavg IS NULL)
	AND ([datecompleted] = @Datecompleted OR @Datecompleted IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [FormID]
	, [chid]
	, [PappFupId]
	, [admittedtohospital]
	, [newdrugs]
	, [newclinics]
	, [workstatusid]
	, [occupation]
	, [smokingMissing]
	, [currentlysmoke]
	, [currentlysmokenumbercigsperday]
	, [drinkingMissing]
	, [drinkalcohol]
	, [drnkunitsavg]
	, [datecompleted]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
    FROM
	[dbo].[bbPappPatientLifestyle]
    WHERE 
	 ([FormID] = @FormId AND @FormId is not null)
	OR ([chid] = @Chid AND @Chid is not null)
	OR ([PappFupId] = @PappFupId AND @PappFupId is not null)
	OR ([admittedtohospital] = @Admittedtohospital AND @Admittedtohospital is not null)
	OR ([newdrugs] = @Newdrugs AND @Newdrugs is not null)
	OR ([newclinics] = @Newclinics AND @Newclinics is not null)
	OR ([workstatusid] = @Workstatusid AND @Workstatusid is not null)
	OR ([occupation] = @Occupation AND @Occupation is not null)
	OR ([smokingMissing] = @SmokingMissing AND @SmokingMissing is not null)
	OR ([currentlysmoke] = @Currentlysmoke AND @Currentlysmoke is not null)
	OR ([currentlysmokenumbercigsperday] = @Currentlysmokenumbercigsperday AND @Currentlysmokenumbercigsperday is not null)
	OR ([drinkingMissing] = @DrinkingMissing AND @DrinkingMissing is not null)
	OR ([drinkalcohol] = @Drinkalcohol AND @Drinkalcohol is not null)
	OR ([drnkunitsavg] = @Drnkunitsavg AND @Drnkunitsavg is not null)
	OR ([datecompleted] = @Datecompleted AND @Datecompleted is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPappPatientLifestyle_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbMailingLists table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_Get_List

AS


				
				SELECT
					[bbMLid],
					[MLName],
					[MLDescription],
					[RoleName],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[TotalEmailsSent],
					[LastEmailSentOn]
				FROM
					[dbo].[bbMailingLists]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbMailingLists_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbMailingLists table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [bbMLid] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([bbMLid])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [bbMLid]'
				SET @SQL = @SQL + ' FROM [dbo].[bbMailingLists]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[bbMLid], O.[MLName], O.[MLDescription], O.[RoleName], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[TotalEmailsSent], O.[LastEmailSentOn]
				FROM
				    [dbo].[bbMailingLists] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[bbMLid] = PageIndex.[bbMLid]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbMailingLists]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbMailingLists_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbMailingLists table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_Insert
(

	@BbMlid int    OUTPUT,

	@MlName nvarchar (256)  ,

	@MlDescription nvarchar (MAX)  ,

	@RoleName nvarchar (256)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@TotalEmailsSent int   ,

	@LastEmailSentOn datetime   
)
AS


				
				INSERT INTO [dbo].[bbMailingLists]
					(
					[MLName]
					,[MLDescription]
					,[RoleName]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[TotalEmailsSent]
					,[LastEmailSentOn]
					)
				VALUES
					(
					@MlName
					,@MlDescription
					,@RoleName
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@TotalEmailsSent
					,@LastEmailSentOn
					)
				-- Get the identity value
				SET @BbMlid = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbMailingLists_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbMailingLists table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_Update
(

	@BbMlid int   ,

	@MlName nvarchar (256)  ,

	@MlDescription nvarchar (MAX)  ,

	@RoleName nvarchar (256)  ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@TotalEmailsSent int   ,

	@LastEmailSentOn datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbMailingLists]
				SET
					[MLName] = @MlName
					,[MLDescription] = @MlDescription
					,[RoleName] = @RoleName
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[TotalEmailsSent] = @TotalEmailsSent
					,[LastEmailSentOn] = @LastEmailSentOn
				WHERE
[bbMLid] = @BbMlid 
				
			

GO
GRANT EXEC ON dbo.znt_bbMailingLists_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbMailingLists table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_Delete
(

	@BbMlid int   
)
AS


				DELETE FROM [dbo].[bbMailingLists] WITH (ROWLOCK) 
				WHERE
					[bbMLid] = @BbMlid
					
			

GO
GRANT EXEC ON dbo.znt_bbMailingLists_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_GetByRoleName procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_GetByRoleName') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_GetByRoleName
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbMailingLists table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_GetByRoleName
(

	@RoleName nvarchar (256)  
)
AS


				SELECT
					[bbMLid],
					[MLName],
					[MLDescription],
					[RoleName],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[TotalEmailsSent],
					[LastEmailSentOn]
				FROM
					[dbo].[bbMailingLists]
				WHERE
					[RoleName] = @RoleName
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbMailingLists_GetByRoleName TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_GetByBbMlid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_GetByBbMlid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_GetByBbMlid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbMailingLists table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_GetByBbMlid
(

	@BbMlid int   
)
AS


				SELECT
					[bbMLid],
					[MLName],
					[MLDescription],
					[RoleName],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[TotalEmailsSent],
					[LastEmailSentOn]
				FROM
					[dbo].[bbMailingLists]
				WHERE
					[bbMLid] = @BbMlid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbMailingLists_GetByBbMlid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbMailingLists_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbMailingLists_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbMailingLists_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbMailingLists table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbMailingLists_Find
(

	@SearchUsingOR bit   = null ,

	@BbMlid int   = null ,

	@MlName nvarchar (256)  = null ,

	@MlDescription nvarchar (MAX)  = null ,

	@RoleName nvarchar (256)  = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@TotalEmailsSent int   = null ,

	@LastEmailSentOn datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [bbMLid]
	, [MLName]
	, [MLDescription]
	, [RoleName]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [TotalEmailsSent]
	, [LastEmailSentOn]
    FROM
	[dbo].[bbMailingLists]
    WHERE 
	 ([bbMLid] = @BbMlid OR @BbMlid IS NULL)
	AND ([MLName] = @MlName OR @MlName IS NULL)
	AND ([MLDescription] = @MlDescription OR @MlDescription IS NULL)
	AND ([RoleName] = @RoleName OR @RoleName IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([TotalEmailsSent] = @TotalEmailsSent OR @TotalEmailsSent IS NULL)
	AND ([LastEmailSentOn] = @LastEmailSentOn OR @LastEmailSentOn IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [bbMLid]
	, [MLName]
	, [MLDescription]
	, [RoleName]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [TotalEmailsSent]
	, [LastEmailSentOn]
    FROM
	[dbo].[bbMailingLists]
    WHERE 
	 ([bbMLid] = @BbMlid AND @BbMlid is not null)
	OR ([MLName] = @MlName AND @MlName is not null)
	OR ([MLDescription] = @MlDescription AND @MlDescription is not null)
	OR ([RoleName] = @RoleName AND @RoleName is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([TotalEmailsSent] = @TotalEmailsSent AND @TotalEmailsSent is not null)
	OR ([LastEmailSentOn] = @LastEmailSentOn AND @LastEmailSentOn is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbMailingLists_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_Get_List
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets all records from the bbPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_Get_List

AS


				
				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
					
				SELECT @@ROWCOUNT
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_Get_List TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Gets records from the bbPatientCohortTracking table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetPaged
(

	@WhereClause varchar (8000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [FupId] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([FupId])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [FupId]'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortTracking]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[FupId], O.[chid], O.[fupcode], O.[studynocurrent], O.[centreidcurrent], O.[consultantidcurrent], O.[clinicVisitdate], O.[duedate], O.[editWindowFrom], O.[editWindowTo], O.[fupstatus], O.[datavalid], O.[feedbackstatus], O.[comments], O.[dateentered], O.[hasnocurrenttherapy], O.[hasnobiologictherapy], O.[hasnoconventionaltherapy], O.[hasnoprevioustherapy], O.[hasnocomorbidities], O.[hasnolesions], O.[hasnouvtherapy], O.[hasnolabvalues], O.[hasnoadverseevents], O.[hasnodiseaseseverity], O.[hasnopasi], O.[hasnoadditionalinfo], O.[hasnomedicalproblems], O.[hasnodlqi], O.[hasnolifestylefactors], O.[cageinapplicable], O.[haqinapplicable], O.[discontinuedbiotherapy], O.[checkedbyid], O.[checkedbydate], O.[haspreviousantipsoriaticdrugs], O.[haschangedbiologictherapy], O.[hasnewadverseevents], O.[createdbyid], O.[createdbyname], O.[createddate], O.[lastupdatedbyid], O.[lastupdatedbyname], O.[lastupdateddate], O.[haspsoriaticarthiritis], O.[hasinflamatoryarthiritis], O.[psoriaticarthiritisonset], O.[truncated_fup_applicable], O.[psoriaticarthiritisonsetdate], O.[hasnoSMITherapy], O.[clinicAttendance]
				FROM
				    [dbo].[bbPatientCohortTracking] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[FupId] = PageIndex.[FupId]
				ORDER BY
				    PageIndex.IndexId
                
				-- get row count
				SET @SQL = 'SELECT COUNT(1) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[bbPatientCohortTracking]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetPaged TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_Insert
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Inserts a record into the bbPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_Insert
(

	@FupId int    OUTPUT,

	@Chid int   ,

	@Fupcode int   ,

	@Studynocurrent int   ,

	@Centreidcurrent int   ,

	@Consultantidcurrent int   ,

	@ClinicVisitdate datetime   ,

	@Duedate datetime   ,

	@EditWindowFrom datetime   ,

	@EditWindowTo datetime   ,

	@Fupstatus int   ,

	@Datavalid int   ,

	@Feedbackstatus int   ,

	@Comments text   ,

	@Dateentered datetime   ,

	@Hasnocurrenttherapy bit   ,

	@Hasnobiologictherapy bit   ,

	@Hasnoconventionaltherapy bit   ,

	@Hasnoprevioustherapy bit   ,

	@Hasnocomorbidities bit   ,

	@Hasnolesions bit   ,

	@Hasnouvtherapy bit   ,

	@Hasnolabvalues bit   ,

	@Hasnoadverseevents bit   ,

	@Hasnodiseaseseverity bit   ,

	@Hasnopasi bit   ,

	@Hasnoadditionalinfo bit   ,

	@Hasnomedicalproblems bit   ,

	@Hasnodlqi bit   ,

	@Hasnolifestylefactors bit   ,

	@Cageinapplicable bit   ,

	@Haqinapplicable bit   ,

	@Discontinuedbiotherapy bit   ,

	@Checkedbyid int   ,

	@Checkedbydate datetime   ,

	@Haspreviousantipsoriaticdrugs bit   ,

	@Haschangedbiologictherapy bit   ,

	@Hasnewadverseevents bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Haspsoriaticarthiritis bit   ,

	@Hasinflamatoryarthiritis bit   ,

	@Psoriaticarthiritisonset int   ,

	@TruncatedFupApplicable int   ,

	@Psoriaticarthiritisonsetdate datetime   ,

	@HasnoSmiTherapy bit   ,

	@ClinicAttendance int   
)
AS


				
				INSERT INTO [dbo].[bbPatientCohortTracking]
					(
					[chid]
					,[fupcode]
					,[studynocurrent]
					,[centreidcurrent]
					,[consultantidcurrent]
					,[clinicVisitdate]
					,[duedate]
					,[editWindowFrom]
					,[editWindowTo]
					,[fupstatus]
					,[datavalid]
					,[feedbackstatus]
					,[comments]
					,[dateentered]
					,[hasnocurrenttherapy]
					,[hasnobiologictherapy]
					,[hasnoconventionaltherapy]
					,[hasnoprevioustherapy]
					,[hasnocomorbidities]
					,[hasnolesions]
					,[hasnouvtherapy]
					,[hasnolabvalues]
					,[hasnoadverseevents]
					,[hasnodiseaseseverity]
					,[hasnopasi]
					,[hasnoadditionalinfo]
					,[hasnomedicalproblems]
					,[hasnodlqi]
					,[hasnolifestylefactors]
					,[cageinapplicable]
					,[haqinapplicable]
					,[discontinuedbiotherapy]
					,[checkedbyid]
					,[checkedbydate]
					,[haspreviousantipsoriaticdrugs]
					,[haschangedbiologictherapy]
					,[hasnewadverseevents]
					,[createdbyid]
					,[createdbyname]
					,[createddate]
					,[lastupdatedbyid]
					,[lastupdatedbyname]
					,[lastupdateddate]
					,[haspsoriaticarthiritis]
					,[hasinflamatoryarthiritis]
					,[psoriaticarthiritisonset]
					,[truncated_fup_applicable]
					,[psoriaticarthiritisonsetdate]
					,[hasnoSMITherapy]
					,[clinicAttendance]
					)
				VALUES
					(
					@Chid
					,@Fupcode
					,@Studynocurrent
					,@Centreidcurrent
					,@Consultantidcurrent
					,@ClinicVisitdate
					,@Duedate
					,@EditWindowFrom
					,@EditWindowTo
					,@Fupstatus
					,@Datavalid
					,@Feedbackstatus
					,@Comments
					,@Dateentered
					,@Hasnocurrenttherapy
					,@Hasnobiologictherapy
					,@Hasnoconventionaltherapy
					,@Hasnoprevioustherapy
					,@Hasnocomorbidities
					,@Hasnolesions
					,@Hasnouvtherapy
					,@Hasnolabvalues
					,@Hasnoadverseevents
					,@Hasnodiseaseseverity
					,@Hasnopasi
					,@Hasnoadditionalinfo
					,@Hasnomedicalproblems
					,@Hasnodlqi
					,@Hasnolifestylefactors
					,@Cageinapplicable
					,@Haqinapplicable
					,@Discontinuedbiotherapy
					,@Checkedbyid
					,@Checkedbydate
					,@Haspreviousantipsoriaticdrugs
					,@Haschangedbiologictherapy
					,@Hasnewadverseevents
					,@Createdbyid
					,@Createdbyname
					,@Createddate
					,@Lastupdatedbyid
					,@Lastupdatedbyname
					,@Lastupdateddate
					,@Haspsoriaticarthiritis
					,@Hasinflamatoryarthiritis
					,@Psoriaticarthiritisonset
					,@TruncatedFupApplicable
					,@Psoriaticarthiritisonsetdate
					,@HasnoSmiTherapy
					,@ClinicAttendance
					)
				-- Get the identity value
				SET @FupId = SCOPE_IDENTITY()
									
							
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_Insert TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_Update
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Updates a record in the bbPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_Update
(

	@FupId int   ,

	@Chid int   ,

	@Fupcode int   ,

	@Studynocurrent int   ,

	@Centreidcurrent int   ,

	@Consultantidcurrent int   ,

	@ClinicVisitdate datetime   ,

	@Duedate datetime   ,

	@EditWindowFrom datetime   ,

	@EditWindowTo datetime   ,

	@Fupstatus int   ,

	@Datavalid int   ,

	@Feedbackstatus int   ,

	@Comments text   ,

	@Dateentered datetime   ,

	@Hasnocurrenttherapy bit   ,

	@Hasnobiologictherapy bit   ,

	@Hasnoconventionaltherapy bit   ,

	@Hasnoprevioustherapy bit   ,

	@Hasnocomorbidities bit   ,

	@Hasnolesions bit   ,

	@Hasnouvtherapy bit   ,

	@Hasnolabvalues bit   ,

	@Hasnoadverseevents bit   ,

	@Hasnodiseaseseverity bit   ,

	@Hasnopasi bit   ,

	@Hasnoadditionalinfo bit   ,

	@Hasnomedicalproblems bit   ,

	@Hasnodlqi bit   ,

	@Hasnolifestylefactors bit   ,

	@Cageinapplicable bit   ,

	@Haqinapplicable bit   ,

	@Discontinuedbiotherapy bit   ,

	@Checkedbyid int   ,

	@Checkedbydate datetime   ,

	@Haspreviousantipsoriaticdrugs bit   ,

	@Haschangedbiologictherapy bit   ,

	@Hasnewadverseevents bit   ,

	@Createdbyid int   ,

	@Createdbyname varchar (100)  ,

	@Createddate datetime   ,

	@Lastupdatedbyid int   ,

	@Lastupdatedbyname varchar (100)  ,

	@Lastupdateddate datetime   ,

	@Haspsoriaticarthiritis bit   ,

	@Hasinflamatoryarthiritis bit   ,

	@Psoriaticarthiritisonset int   ,

	@TruncatedFupApplicable int   ,

	@Psoriaticarthiritisonsetdate datetime   ,

	@HasnoSmiTherapy bit   ,

	@ClinicAttendance int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[bbPatientCohortTracking]
				SET
					[chid] = @Chid
					,[fupcode] = @Fupcode
					,[studynocurrent] = @Studynocurrent
					,[centreidcurrent] = @Centreidcurrent
					,[consultantidcurrent] = @Consultantidcurrent
					,[clinicVisitdate] = @ClinicVisitdate
					,[duedate] = @Duedate
					,[editWindowFrom] = @EditWindowFrom
					,[editWindowTo] = @EditWindowTo
					,[fupstatus] = @Fupstatus
					,[datavalid] = @Datavalid
					,[feedbackstatus] = @Feedbackstatus
					,[comments] = @Comments
					,[dateentered] = @Dateentered
					,[hasnocurrenttherapy] = @Hasnocurrenttherapy
					,[hasnobiologictherapy] = @Hasnobiologictherapy
					,[hasnoconventionaltherapy] = @Hasnoconventionaltherapy
					,[hasnoprevioustherapy] = @Hasnoprevioustherapy
					,[hasnocomorbidities] = @Hasnocomorbidities
					,[hasnolesions] = @Hasnolesions
					,[hasnouvtherapy] = @Hasnouvtherapy
					,[hasnolabvalues] = @Hasnolabvalues
					,[hasnoadverseevents] = @Hasnoadverseevents
					,[hasnodiseaseseverity] = @Hasnodiseaseseverity
					,[hasnopasi] = @Hasnopasi
					,[hasnoadditionalinfo] = @Hasnoadditionalinfo
					,[hasnomedicalproblems] = @Hasnomedicalproblems
					,[hasnodlqi] = @Hasnodlqi
					,[hasnolifestylefactors] = @Hasnolifestylefactors
					,[cageinapplicable] = @Cageinapplicable
					,[haqinapplicable] = @Haqinapplicable
					,[discontinuedbiotherapy] = @Discontinuedbiotherapy
					,[checkedbyid] = @Checkedbyid
					,[checkedbydate] = @Checkedbydate
					,[haspreviousantipsoriaticdrugs] = @Haspreviousantipsoriaticdrugs
					,[haschangedbiologictherapy] = @Haschangedbiologictherapy
					,[hasnewadverseevents] = @Hasnewadverseevents
					,[createdbyid] = @Createdbyid
					,[createdbyname] = @Createdbyname
					,[createddate] = @Createddate
					,[lastupdatedbyid] = @Lastupdatedbyid
					,[lastupdatedbyname] = @Lastupdatedbyname
					,[lastupdateddate] = @Lastupdateddate
					,[haspsoriaticarthiritis] = @Haspsoriaticarthiritis
					,[hasinflamatoryarthiritis] = @Hasinflamatoryarthiritis
					,[psoriaticarthiritisonset] = @Psoriaticarthiritisonset
					,[truncated_fup_applicable] = @TruncatedFupApplicable
					,[psoriaticarthiritisonsetdate] = @Psoriaticarthiritisonsetdate
					,[hasnoSMITherapy] = @HasnoSmiTherapy
					,[clinicAttendance] = @ClinicAttendance
				WHERE
[FupId] = @FupId 
				
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_Update TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_Delete
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Deletes a record in the bbPatientCohortTracking table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_Delete
(

	@FupId int   
)
AS


				DELETE FROM [dbo].[bbPatientCohortTracking] WITH (ROWLOCK) 
				WHERE
					[FupId] = @FupId
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_Delete TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByCentreidcurrent procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByCentreidcurrent') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByCentreidcurrent
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByCentreidcurrent
(

	@Centreidcurrent int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[centreidcurrent] = @Centreidcurrent
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByCentreidcurrent TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByFupstatus procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByFupstatus') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByFupstatus
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByFupstatus
(

	@Fupstatus int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[fupstatus] = @Fupstatus
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByFupstatus TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByClinicAttendance procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByClinicAttendance') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByClinicAttendance
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByClinicAttendance
(

	@ClinicAttendance int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[clinicAttendance] = @ClinicAttendance
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByClinicAttendance TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByChidFupcode procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByChidFupcode') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByChidFupcode
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByChidFupcode
(

	@Chid int   ,

	@Fupcode int   
)
AS


				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[chid] = @Chid
					AND [fupcode] = @Fupcode
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByChidFupcode TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByChid procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByChid') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByChid
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByChid
(

	@Chid int   
)
AS


				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[chid] = @Chid
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByChid TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByDateentered procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByDateentered') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByDateentered
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByDateentered
(

	@Dateentered datetime   
)
AS


				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[dateentered] = @Dateentered
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByDateentered TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByEditWindowFromEditWindowTo procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByEditWindowFromEditWindowTo') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByEditWindowFromEditWindowTo
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByEditWindowFromEditWindowTo
(

	@EditWindowFrom datetime   ,

	@EditWindowTo datetime   
)
AS


				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[editWindowFrom] = @EditWindowFrom
					AND [editWindowTo] = @EditWindowTo
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByEditWindowFromEditWindowTo TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByEditWindowTo procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByEditWindowTo') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByEditWindowTo
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByEditWindowTo
(

	@EditWindowTo datetime   
)
AS


				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[editWindowTo] = @EditWindowTo
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByEditWindowTo TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_GetByFupId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_GetByFupId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_GetByFupId
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Select records from the bbPatientCohortTracking table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_GetByFupId
(

	@FupId int   
)
AS


				SELECT
					[FupId],
					[chid],
					[fupcode],
					[studynocurrent],
					[centreidcurrent],
					[consultantidcurrent],
					[clinicVisitdate],
					[duedate],
					[editWindowFrom],
					[editWindowTo],
					[fupstatus],
					[datavalid],
					[feedbackstatus],
					[comments],
					[dateentered],
					[hasnocurrenttherapy],
					[hasnobiologictherapy],
					[hasnoconventionaltherapy],
					[hasnoprevioustherapy],
					[hasnocomorbidities],
					[hasnolesions],
					[hasnouvtherapy],
					[hasnolabvalues],
					[hasnoadverseevents],
					[hasnodiseaseseverity],
					[hasnopasi],
					[hasnoadditionalinfo],
					[hasnomedicalproblems],
					[hasnodlqi],
					[hasnolifestylefactors],
					[cageinapplicable],
					[haqinapplicable],
					[discontinuedbiotherapy],
					[checkedbyid],
					[checkedbydate],
					[haspreviousantipsoriaticdrugs],
					[haschangedbiologictherapy],
					[hasnewadverseevents],
					[createdbyid],
					[createdbyname],
					[createddate],
					[lastupdatedbyid],
					[lastupdatedbyname],
					[lastupdateddate],
					[haspsoriaticarthiritis],
					[hasinflamatoryarthiritis],
					[psoriaticarthiritisonset],
					[truncated_fup_applicable],
					[psoriaticarthiritisonsetdate],
					[hasnoSMITherapy],
					[clinicAttendance]
				FROM
					[dbo].[bbPatientCohortTracking]
				WHERE
					[FupId] = @FupId
				SELECT @@ROWCOUNT
					
			

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_GetByFupId TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.znt_bbPatientCohortTracking_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.znt_bbPatientCohortTracking_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.znt_bbPatientCohortTracking_Find
GO

/*
----------------------------------------------------------------------------------------------------
-- Date Created: 03 July 2020

-- Created By: The University of Manchester (www.manchester.ac.uk)
-- Purpose: Finds records in the bbPatientCohortTracking table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.znt_bbPatientCohortTracking_Find
(

	@SearchUsingOR bit   = null ,

	@FupId int   = null ,

	@Chid int   = null ,

	@Fupcode int   = null ,

	@Studynocurrent int   = null ,

	@Centreidcurrent int   = null ,

	@Consultantidcurrent int   = null ,

	@ClinicVisitdate datetime   = null ,

	@Duedate datetime   = null ,

	@EditWindowFrom datetime   = null ,

	@EditWindowTo datetime   = null ,

	@Fupstatus int   = null ,

	@Datavalid int   = null ,

	@Feedbackstatus int   = null ,

	@Comments text   = null ,

	@Dateentered datetime   = null ,

	@Hasnocurrenttherapy bit   = null ,

	@Hasnobiologictherapy bit   = null ,

	@Hasnoconventionaltherapy bit   = null ,

	@Hasnoprevioustherapy bit   = null ,

	@Hasnocomorbidities bit   = null ,

	@Hasnolesions bit   = null ,

	@Hasnouvtherapy bit   = null ,

	@Hasnolabvalues bit   = null ,

	@Hasnoadverseevents bit   = null ,

	@Hasnodiseaseseverity bit   = null ,

	@Hasnopasi bit   = null ,

	@Hasnoadditionalinfo bit   = null ,

	@Hasnomedicalproblems bit   = null ,

	@Hasnodlqi bit   = null ,

	@Hasnolifestylefactors bit   = null ,

	@Cageinapplicable bit   = null ,

	@Haqinapplicable bit   = null ,

	@Discontinuedbiotherapy bit   = null ,

	@Checkedbyid int   = null ,

	@Checkedbydate datetime   = null ,

	@Haspreviousantipsoriaticdrugs bit   = null ,

	@Haschangedbiologictherapy bit   = null ,

	@Hasnewadverseevents bit   = null ,

	@Createdbyid int   = null ,

	@Createdbyname varchar (100)  = null ,

	@Createddate datetime   = null ,

	@Lastupdatedbyid int   = null ,

	@Lastupdatedbyname varchar (100)  = null ,

	@Lastupdateddate datetime   = null ,

	@Haspsoriaticarthiritis bit   = null ,

	@Hasinflamatoryarthiritis bit   = null ,

	@Psoriaticarthiritisonset int   = null ,

	@TruncatedFupApplicable int   = null ,

	@Psoriaticarthiritisonsetdate datetime   = null ,

	@HasnoSmiTherapy bit   = null ,

	@ClinicAttendance int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FupId]
	, [chid]
	, [fupcode]
	, [studynocurrent]
	, [centreidcurrent]
	, [consultantidcurrent]
	, [clinicVisitdate]
	, [duedate]
	, [editWindowFrom]
	, [editWindowTo]
	, [fupstatus]
	, [datavalid]
	, [feedbackstatus]
	, [comments]
	, [dateentered]
	, [hasnocurrenttherapy]
	, [hasnobiologictherapy]
	, [hasnoconventionaltherapy]
	, [hasnoprevioustherapy]
	, [hasnocomorbidities]
	, [hasnolesions]
	, [hasnouvtherapy]
	, [hasnolabvalues]
	, [hasnoadverseevents]
	, [hasnodiseaseseverity]
	, [hasnopasi]
	, [hasnoadditionalinfo]
	, [hasnomedicalproblems]
	, [hasnodlqi]
	, [hasnolifestylefactors]
	, [cageinapplicable]
	, [haqinapplicable]
	, [discontinuedbiotherapy]
	, [checkedbyid]
	, [checkedbydate]
	, [haspreviousantipsoriaticdrugs]
	, [haschangedbiologictherapy]
	, [hasnewadverseevents]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [haspsoriaticarthiritis]
	, [hasinflamatoryarthiritis]
	, [psoriaticarthiritisonset]
	, [truncated_fup_applicable]
	, [psoriaticarthiritisonsetdate]
	, [hasnoSMITherapy]
	, [clinicAttendance]
    FROM
	[dbo].[bbPatientCohortTracking]
    WHERE 
	 ([FupId] = @FupId OR @FupId IS NULL)
	AND ([chid] = @Chid OR @Chid IS NULL)
	AND ([fupcode] = @Fupcode OR @Fupcode IS NULL)
	AND ([studynocurrent] = @Studynocurrent OR @Studynocurrent IS NULL)
	AND ([centreidcurrent] = @Centreidcurrent OR @Centreidcurrent IS NULL)
	AND ([consultantidcurrent] = @Consultantidcurrent OR @Consultantidcurrent IS NULL)
	AND ([clinicVisitdate] = @ClinicVisitdate OR @ClinicVisitdate IS NULL)
	AND ([duedate] = @Duedate OR @Duedate IS NULL)
	AND ([editWindowFrom] = @EditWindowFrom OR @EditWindowFrom IS NULL)
	AND ([editWindowTo] = @EditWindowTo OR @EditWindowTo IS NULL)
	AND ([fupstatus] = @Fupstatus OR @Fupstatus IS NULL)
	AND ([datavalid] = @Datavalid OR @Datavalid IS NULL)
	AND ([feedbackstatus] = @Feedbackstatus OR @Feedbackstatus IS NULL)
	AND ([dateentered] = @Dateentered OR @Dateentered IS NULL)
	AND ([hasnocurrenttherapy] = @Hasnocurrenttherapy OR @Hasnocurrenttherapy IS NULL)
	AND ([hasnobiologictherapy] = @Hasnobiologictherapy OR @Hasnobiologictherapy IS NULL)
	AND ([hasnoconventionaltherapy] = @Hasnoconventionaltherapy OR @Hasnoconventionaltherapy IS NULL)
	AND ([hasnoprevioustherapy] = @Hasnoprevioustherapy OR @Hasnoprevioustherapy IS NULL)
	AND ([hasnocomorbidities] = @Hasnocomorbidities OR @Hasnocomorbidities IS NULL)
	AND ([hasnolesions] = @Hasnolesions OR @Hasnolesions IS NULL)
	AND ([hasnouvtherapy] = @Hasnouvtherapy OR @Hasnouvtherapy IS NULL)
	AND ([hasnolabvalues] = @Hasnolabvalues OR @Hasnolabvalues IS NULL)
	AND ([hasnoadverseevents] = @Hasnoadverseevents OR @Hasnoadverseevents IS NULL)
	AND ([hasnodiseaseseverity] = @Hasnodiseaseseverity OR @Hasnodiseaseseverity IS NULL)
	AND ([hasnopasi] = @Hasnopasi OR @Hasnopasi IS NULL)
	AND ([hasnoadditionalinfo] = @Hasnoadditionalinfo OR @Hasnoadditionalinfo IS NULL)
	AND ([hasnomedicalproblems] = @Hasnomedicalproblems OR @Hasnomedicalproblems IS NULL)
	AND ([hasnodlqi] = @Hasnodlqi OR @Hasnodlqi IS NULL)
	AND ([hasnolifestylefactors] = @Hasnolifestylefactors OR @Hasnolifestylefactors IS NULL)
	AND ([cageinapplicable] = @Cageinapplicable OR @Cageinapplicable IS NULL)
	AND ([haqinapplicable] = @Haqinapplicable OR @Haqinapplicable IS NULL)
	AND ([discontinuedbiotherapy] = @Discontinuedbiotherapy OR @Discontinuedbiotherapy IS NULL)
	AND ([checkedbyid] = @Checkedbyid OR @Checkedbyid IS NULL)
	AND ([checkedbydate] = @Checkedbydate OR @Checkedbydate IS NULL)
	AND ([haspreviousantipsoriaticdrugs] = @Haspreviousantipsoriaticdrugs OR @Haspreviousantipsoriaticdrugs IS NULL)
	AND ([haschangedbiologictherapy] = @Haschangedbiologictherapy OR @Haschangedbiologictherapy IS NULL)
	AND ([hasnewadverseevents] = @Hasnewadverseevents OR @Hasnewadverseevents IS NULL)
	AND ([createdbyid] = @Createdbyid OR @Createdbyid IS NULL)
	AND ([createdbyname] = @Createdbyname OR @Createdbyname IS NULL)
	AND ([createddate] = @Createddate OR @Createddate IS NULL)
	AND ([lastupdatedbyid] = @Lastupdatedbyid OR @Lastupdatedbyid IS NULL)
	AND ([lastupdatedbyname] = @Lastupdatedbyname OR @Lastupdatedbyname IS NULL)
	AND ([lastupdateddate] = @Lastupdateddate OR @Lastupdateddate IS NULL)
	AND ([haspsoriaticarthiritis] = @Haspsoriaticarthiritis OR @Haspsoriaticarthiritis IS NULL)
	AND ([hasinflamatoryarthiritis] = @Hasinflamatoryarthiritis OR @Hasinflamatoryarthiritis IS NULL)
	AND ([psoriaticarthiritisonset] = @Psoriaticarthiritisonset OR @Psoriaticarthiritisonset IS NULL)
	AND ([truncated_fup_applicable] = @TruncatedFupApplicable OR @TruncatedFupApplicable IS NULL)
	AND ([psoriaticarthiritisonsetdate] = @Psoriaticarthiritisonsetdate OR @Psoriaticarthiritisonsetdate IS NULL)
	AND ([hasnoSMITherapy] = @HasnoSmiTherapy OR @HasnoSmiTherapy IS NULL)
	AND ([clinicAttendance] = @ClinicAttendance OR @ClinicAttendance IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [FupId]
	, [chid]
	, [fupcode]
	, [studynocurrent]
	, [centreidcurrent]
	, [consultantidcurrent]
	, [clinicVisitdate]
	, [duedate]
	, [editWindowFrom]
	, [editWindowTo]
	, [fupstatus]
	, [datavalid]
	, [feedbackstatus]
	, [comments]
	, [dateentered]
	, [hasnocurrenttherapy]
	, [hasnobiologictherapy]
	, [hasnoconventionaltherapy]
	, [hasnoprevioustherapy]
	, [hasnocomorbidities]
	, [hasnolesions]
	, [hasnouvtherapy]
	, [hasnolabvalues]
	, [hasnoadverseevents]
	, [hasnodiseaseseverity]
	, [hasnopasi]
	, [hasnoadditionalinfo]
	, [hasnomedicalproblems]
	, [hasnodlqi]
	, [hasnolifestylefactors]
	, [cageinapplicable]
	, [haqinapplicable]
	, [discontinuedbiotherapy]
	, [checkedbyid]
	, [checkedbydate]
	, [haspreviousantipsoriaticdrugs]
	, [haschangedbiologictherapy]
	, [hasnewadverseevents]
	, [createdbyid]
	, [createdbyname]
	, [createddate]
	, [lastupdatedbyid]
	, [lastupdatedbyname]
	, [lastupdateddate]
	, [haspsoriaticarthiritis]
	, [hasinflamatoryarthiritis]
	, [psoriaticarthiritisonset]
	, [truncated_fup_applicable]
	, [psoriaticarthiritisonsetdate]
	, [hasnoSMITherapy]
	, [clinicAttendance]
    FROM
	[dbo].[bbPatientCohortTracking]
    WHERE 
	 ([FupId] = @FupId AND @FupId is not null)
	OR ([chid] = @Chid AND @Chid is not null)
	OR ([fupcode] = @Fupcode AND @Fupcode is not null)
	OR ([studynocurrent] = @Studynocurrent AND @Studynocurrent is not null)
	OR ([centreidcurrent] = @Centreidcurrent AND @Centreidcurrent is not null)
	OR ([consultantidcurrent] = @Consultantidcurrent AND @Consultantidcurrent is not null)
	OR ([clinicVisitdate] = @ClinicVisitdate AND @ClinicVisitdate is not null)
	OR ([duedate] = @Duedate AND @Duedate is not null)
	OR ([editWindowFrom] = @EditWindowFrom AND @EditWindowFrom is not null)
	OR ([editWindowTo] = @EditWindowTo AND @EditWindowTo is not null)
	OR ([fupstatus] = @Fupstatus AND @Fupstatus is not null)
	OR ([datavalid] = @Datavalid AND @Datavalid is not null)
	OR ([feedbackstatus] = @Feedbackstatus AND @Feedbackstatus is not null)
	OR ([dateentered] = @Dateentered AND @Dateentered is not null)
	OR ([hasnocurrenttherapy] = @Hasnocurrenttherapy AND @Hasnocurrenttherapy is not null)
	OR ([hasnobiologictherapy] = @Hasnobiologictherapy AND @Hasnobiologictherapy is not null)
	OR ([hasnoconventionaltherapy] = @Hasnoconventionaltherapy AND @Hasnoconventionaltherapy is not null)
	OR ([hasnoprevioustherapy] = @Hasnoprevioustherapy AND @Hasnoprevioustherapy is not null)
	OR ([hasnocomorbidities] = @Hasnocomorbidities AND @Hasnocomorbidities is not null)
	OR ([hasnolesions] = @Hasnolesions AND @Hasnolesions is not null)
	OR ([hasnouvtherapy] = @Hasnouvtherapy AND @Hasnouvtherapy is not null)
	OR ([hasnolabvalues] = @Hasnolabvalues AND @Hasnolabvalues is not null)
	OR ([hasnoadverseevents] = @Hasnoadverseevents AND @Hasnoadverseevents is not null)
	OR ([hasnodiseaseseverity] = @Hasnodiseaseseverity AND @Hasnodiseaseseverity is not null)
	OR ([hasnopasi] = @Hasnopasi AND @Hasnopasi is not null)
	OR ([hasnoadditionalinfo] = @Hasnoadditionalinfo AND @Hasnoadditionalinfo is not null)
	OR ([hasnomedicalproblems] = @Hasnomedicalproblems AND @Hasnomedicalproblems is not null)
	OR ([hasnodlqi] = @Hasnodlqi AND @Hasnodlqi is not null)
	OR ([hasnolifestylefactors] = @Hasnolifestylefactors AND @Hasnolifestylefactors is not null)
	OR ([cageinapplicable] = @Cageinapplicable AND @Cageinapplicable is not null)
	OR ([haqinapplicable] = @Haqinapplicable AND @Haqinapplicable is not null)
	OR ([discontinuedbiotherapy] = @Discontinuedbiotherapy AND @Discontinuedbiotherapy is not null)
	OR ([checkedbyid] = @Checkedbyid AND @Checkedbyid is not null)
	OR ([checkedbydate] = @Checkedbydate AND @Checkedbydate is not null)
	OR ([haspreviousantipsoriaticdrugs] = @Haspreviousantipsoriaticdrugs AND @Haspreviousantipsoriaticdrugs is not null)
	OR ([haschangedbiologictherapy] = @Haschangedbiologictherapy AND @Haschangedbiologictherapy is not null)
	OR ([hasnewadverseevents] = @Hasnewadverseevents AND @Hasnewadverseevents is not null)
	OR ([createdbyid] = @Createdbyid AND @Createdbyid is not null)
	OR ([createdbyname] = @Createdbyname AND @Createdbyname is not null)
	OR ([createddate] = @Createddate AND @Createddate is not null)
	OR ([lastupdatedbyid] = @Lastupdatedbyid AND @Lastupdatedbyid is not null)
	OR ([lastupdatedbyname] = @Lastupdatedbyname AND @Lastupdatedbyname is not null)
	OR ([lastupdateddate] = @Lastupdateddate AND @Lastupdateddate is not null)
	OR ([haspsoriaticarthiritis] = @Haspsoriaticarthiritis AND @Haspsoriaticarthiritis is not null)
	OR ([hasinflamatoryarthiritis] = @Hasinflamatoryarthiritis AND @Hasinflamatoryarthiritis is not null)
	OR ([psoriaticarthiritisonset] = @Psoriaticarthiritisonset AND @Psoriaticarthiritisonset is not null)
	OR ([truncated_fup_applicable] = @TruncatedFupApplicable AND @TruncatedFupApplicable is not null)
	OR ([psoriaticarthiritisonsetdate] = @Psoriaticarthiritisonsetdate AND @Psoriaticarthiritisonsetdate is not null)
	OR ([hasnoSMITherapy] = @HasnoSmiTherapy AND @HasnoSmiTherapy is not null)
	OR ([clinicAttendance] = @ClinicAttendance AND @ClinicAttendance is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
GRANT EXEC ON dbo.znt_bbPatientCohortTracking_Find TO bb_external

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

