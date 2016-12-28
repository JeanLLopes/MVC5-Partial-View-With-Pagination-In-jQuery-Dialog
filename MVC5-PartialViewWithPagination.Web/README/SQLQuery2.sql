USE [Cricketer]  
GO  
  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
SET ANSI_PADDING ON  
GO  
CREATE TABLE [dbo].[CricketerProfile](  
    [ID] [int] IDENTITY(1,1) NOT NULL,  
    [Name] [varchar](50) NULL,  
    [ODI] [int] NULL,  
    [Tests] [int] NULL,  
    [ODIRuns] [int] NULL,  
    [TestRuns] [int] NULL,  
    [Team] [int] NULL,  
 CONSTRAINT [PK_CricketerProfile] PRIMARY KEY CLUSTERED   
(  
    [ID] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY]  
  
GO  
SET ANSI_PADDING OFF  
GO  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
SET ANSI_PADDING ON  
GO  
CREATE TABLE [dbo].[Team](  
    [ID] [int] IDENTITY(1,1) NOT NULL,  
    [Name] [varchar](50) NULL,  
PRIMARY KEY CLUSTERED   
(  
    [ID] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY]  
  
GO  
SET ANSI_PADDING OFF  
GO  
SET IDENTITY_INSERT [dbo].[CricketerProfile] ON   
  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (1, N'Sachin Tendulkar', 463, 200, 18426, 15921, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (2, N'Saurav Ganguly', 311, 113, 11363, 7212, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (3, N'Rahul Dravid', 344, 164, 10889, 13228, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (4, N'V.V.S. Laxman', 86, 134, 2338, 8781, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (5, N'Virendar Sehwag', 251, 104, 8273, 8586, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (6, N'Yuvraj Singh', 293, 40, 8329, 1900, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (7, N'M. S. Dhoni', 283, 90, 9110, 4876, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (8, N'Virat Kohli', 176, 53, 7570, 4209, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (9, N'Harbhajan Singh', 236, 103, 1237, 2225, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (10, N'Anil Kumble', 271, 132, 938, 2506, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (11, N'Gautam Gambhir', 147, 58, 5238, 4154, 1)  
GO  
INSERT [dbo].[CricketerProfile] ([ID], [Name], [ODI], [Tests], [ODIRuns], [TestRuns], [Team]) VALUES (12, N'Rohit Sharma', 153, 21, 5131, 1184, 1)  
GO  
SET IDENTITY_INSERT [dbo].[CricketerProfile] OFF  
GO  
SET IDENTITY_INSERT [dbo].[Team] ON   
  
GO  
INSERT [dbo].[Team] ([ID], [Name]) VALUES (1, N'India')  
GO  
INSERT [dbo].[Team] ([ID], [Name]) VALUES (2, N'Australia')  
GO  
INSERT [dbo].[Team] ([ID], [Name]) VALUES (3, N'New Zeland')  
GO  
INSERT [dbo].[Team] ([ID], [Name]) VALUES (4, N'South Africa')  
GO  
INSERT [dbo].[Team] ([ID], [Name]) VALUES (5, N'West Indies')  
GO  
SET IDENTITY_INSERT [dbo].[Team] OFF  
GO  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
CREATE Proc [dbo].[BP_GetAllTeams]    
   
AS    
Begin    
    select * from Team (NOLOCK)  
End    
GO  
  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
  
--EXEC [BP_GetPlayersByTeam] 1, 1 , 4  
  
CREATE PROC [dbo].[BP_GetPlayersByTeam]  
    @TeamId INT ,  
    @PageNumber INT ,  
    @PageSize INT  
AS  
    BEGIN    
               ;  
        WITH    PlayerCte  
                  AS ( SELECT   ID ,  
                                Name ,  
                                ODI ,  
                                Tests ,  
                                ODIRuns ,  
                                TestRuns  
                       FROM     dbo.CricketerProfile (NOLOCK)  
                       WHERE    Team = @TeamId  
                     )  
            SELECT  * ,  
                    ( SELECT    COUNT(*)   
                      FROM      PlayerCte  
                    )AS TotalCount  
            FROM    PlayerCte  
            ORDER BY PlayerCte.ID  
                    OFFSET @PageSize * ( @PageNumber - 1 ) ROWS  
       FETCH NEXT @PageSize ROWS ONLY  
        OPTION  ( RECOMPILE );      
    END;    
GO  