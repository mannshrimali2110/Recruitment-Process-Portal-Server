using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace recruitment_process_portal_server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcquisitionSources",
                columns: table => new
                {
                    SourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcquisitionSources", x => x.SourceID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "ClosureReasons",
                columns: table => new
                {
                    ReasonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosureReasons", x => x.ReasonID);
                });

            migrationBuilder.CreateTable(
                name: "DocVerifyStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocVerifyStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTypes",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTypes", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "JobStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_AppUsers_UserRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "UserRoles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateProfiles",
                columns: table => new
                {
                    CandidateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    SourceID = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ResumeFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeRawText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateProfiles", x => x.CandidateID);
                    table.ForeignKey(
                        name: "FK_CandidateProfiles_AcquisitionSources_SourceID",
                        column: x => x.SourceID,
                        principalTable: "AcquisitionSources",
                        principalColumn: "SourceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateProfiles_AppUsers_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_CandidateProfiles_AppUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentEvents",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentEvents", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_RecruitmentEvents_AppUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AppUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateDocuments",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateID = table.Column<int>(type: "int", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    VerifiedByUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateDocuments", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_CandidateDocuments_AppUsers_VerifiedByUserID",
                        column: x => x.VerifiedByUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_CandidateDocuments_CandidateProfiles_CandidateID",
                        column: x => x.CandidateID,
                        principalTable: "CandidateProfiles",
                        principalColumn: "CandidateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateDocuments_DocVerifyStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "DocVerifyStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkill",
                columns: table => new
                {
                    CandidateID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedByUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkill", x => new { x.CandidateID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_CandidateSkill_AppUsers_VerifiedByUserID",
                        column: x => x.VerifiedByUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CandidateSkill_CandidateProfiles_CandidateID",
                        column: x => x.CandidateID,
                        principalTable: "CandidateProfiles",
                        principalColumn: "CandidateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    ClosureReasonID = table.Column<int>(type: "int", nullable: true),
                    SelectedCandidateID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.PositionID);
                    table.ForeignKey(
                        name: "FK_JobPositions_AppUsers_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPositions_CandidateProfiles_SelectedCandidateID",
                        column: x => x.SelectedCandidateID,
                        principalTable: "CandidateProfiles",
                        principalColumn: "CandidateID");
                    table.ForeignKey(
                        name: "FK_JobPositions_ClosureReasons_ClosureReasonID",
                        column: x => x.ClosureReasonID,
                        principalTable: "ClosureReasons",
                        principalColumn: "ReasonID");
                    table.ForeignKey(
                        name: "FK_JobPositions_JobStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "JobStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPositions_RecruitmentEvents_EventID",
                        column: x => x.EventID,
                        principalTable: "RecruitmentEvents",
                        principalColumn: "EventID");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRecords",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateID = table.Column<int>(type: "int", nullable: false),
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WorkStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobPositionPositionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRecords", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_EmployeeRecords_CandidateProfiles_CandidateID",
                        column: x => x.CandidateID,
                        principalTable: "CandidateProfiles",
                        principalColumn: "CandidateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRecords_JobPositions_JobPositionPositionID",
                        column: x => x.JobPositionPositionID,
                        principalTable: "JobPositions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidateLinks",
                columns: table => new
                {
                    LinkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    CandidateID = table.Column<int>(type: "int", nullable: false),
                    CurrentStatusID = table.Column<int>(type: "int", nullable: false),
                    LinkedByUserID = table.Column<int>(type: "int", nullable: true),
                    LinkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandidateProfileCandidateID = table.Column<int>(type: "int", nullable: true),
                    JobPositionPositionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateLinks", x => x.LinkID);
                    table.ForeignKey(
                        name: "FK_JobCandidateLinks_AppUsers_LinkedByUserID",
                        column: x => x.LinkedByUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_JobCandidateLinks_ApplicationStatuses_CurrentStatusID",
                        column: x => x.CurrentStatusID,
                        principalTable: "ApplicationStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobCandidateLinks_CandidateProfiles_CandidateID",
                        column: x => x.CandidateID,
                        principalTable: "CandidateProfiles",
                        principalColumn: "CandidateID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobCandidateLinks_CandidateProfiles_CandidateProfileCandidateID",
                        column: x => x.CandidateProfileCandidateID,
                        principalTable: "CandidateProfiles",
                        principalColumn: "CandidateID");
                    table.ForeignKey(
                        name: "FK_JobCandidateLinks_JobPositions_JobPositionPositionID",
                        column: x => x.JobPositionPositionID,
                        principalTable: "JobPositions",
                        principalColumn: "PositionID");
                    table.ForeignKey(
                        name: "FK_JobCandidateLinks_JobPositions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "JobPositions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobSkillDefinitions",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    IsMinimum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkillDefinitions", x => new { x.PositionID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_JobSkillDefinitions_JobPositions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "JobPositions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkillDefinitions_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewSchedules",
                columns: table => new
                {
                    InterviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    ScheduledStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    JobCandidateLinkLinkID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSchedules", x => x.InterviewID);
                    table.ForeignKey(
                        name: "FK_InterviewSchedules_InterviewTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "InterviewTypes",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewSchedules_JobCandidateLinks_JobCandidateLinkLinkID",
                        column: x => x.JobCandidateLinkLinkID,
                        principalTable: "JobCandidateLinks",
                        principalColumn: "LinkID");
                    table.ForeignKey(
                        name: "FK_InterviewSchedules_JobCandidateLinks_LinkID",
                        column: x => x.LinkID,
                        principalTable: "JobCandidateLinks",
                        principalColumn: "LinkID");
                });

            migrationBuilder.CreateTable(
                name: "OfferLetters",
                columns: table => new
                {
                    OfferID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkID = table.Column<int>(type: "int", nullable: false),
                    OfferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CTC = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OfferStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferLetters", x => x.OfferID);
                    table.ForeignKey(
                        name: "FK_OfferLetters_JobCandidateLinks_LinkID",
                        column: x => x.LinkID,
                        principalTable: "JobCandidateLinks",
                        principalColumn: "LinkID");
                });

            migrationBuilder.CreateTable(
                name: "ScreeningFeedbacks",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkID = table.Column<int>(type: "int", nullable: false),
                    ReviewerUserID = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreeningDecision = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningFeedbacks", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_ScreeningFeedbacks_AppUsers_ReviewerUserID",
                        column: x => x.ReviewerUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScreeningFeedbacks_JobCandidateLinks_LinkID",
                        column: x => x.LinkID,
                        principalTable: "JobCandidateLinks",
                        principalColumn: "LinkID");
                });

            migrationBuilder.CreateTable(
                name: "StatusChangeLogs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkID = table.Column<int>(type: "int", nullable: false),
                    OldStatusID = table.Column<int>(type: "int", nullable: false),
                    NewStatusID = table.Column<int>(type: "int", nullable: false),
                    ChangedByUserID = table.Column<int>(type: "int", nullable: false),
                    ReasonText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusChangeLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_StatusChangeLogs_AppUsers_ChangedByUserID",
                        column: x => x.ChangedByUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_StatusChangeLogs_ApplicationStatuses_NewStatusID",
                        column: x => x.NewStatusID,
                        principalTable: "ApplicationStatuses",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_StatusChangeLogs_ApplicationStatuses_OldStatusID",
                        column: x => x.OldStatusID,
                        principalTable: "ApplicationStatuses",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_StatusChangeLogs_JobCandidateLinks_LinkID",
                        column: x => x.LinkID,
                        principalTable: "JobCandidateLinks",
                        principalColumn: "LinkID");
                });

            migrationBuilder.CreateTable(
                name: "InterviewPanels",
                columns: table => new
                {
                    InterviewID = table.Column<int>(type: "int", nullable: false),
                    InterviewerUserID = table.Column<int>(type: "int", nullable: false),
                    PanelRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewPanels", x => new { x.InterviewID, x.InterviewerUserID });
                    table.ForeignKey(
                        name: "FK_InterviewPanels_AppUsers_InterviewerUserID",
                        column: x => x.InterviewerUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewPanels_InterviewSchedules_InterviewID",
                        column: x => x.InterviewID,
                        principalTable: "InterviewSchedules",
                        principalColumn: "InterviewID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewResults",
                columns: table => new
                {
                    ResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterviewID = table.Column<int>(type: "int", nullable: false),
                    InterviewerUserID = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewResults", x => x.ResultID);
                    table.ForeignKey(
                        name: "FK_InterviewResults_AppUsers_InterviewerUserID",
                        column: x => x.InterviewerUserID,
                        principalTable: "AppUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewResults_InterviewSchedules_InterviewID",
                        column: x => x.InterviewID,
                        principalTable: "InterviewSchedules",
                        principalColumn: "InterviewID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillRatings",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillRatings", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_SkillRatings_InterviewResults_ResultID",
                        column: x => x.ResultID,
                        principalTable: "InterviewResults",
                        principalColumn: "ResultID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillRatings_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleID", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, "Manages job openings, candidates, interviews", "Recruiter" },
                    { 2, "Culture fit, negotiations, documentation", "HR" },
                    { 3, "Provides interview feedback", "Interviewer" },
                    { 4, "Screens CVs and shortlists candidates", "Reviewer" },
                    { 5, "Manages users, roles, system configuration", "Admin" },
                    { 6, "Applies to jobs and uploads documents", "Candidate" },
                    { 7, "Read-only access to system data", "Viewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleID",
                table: "AppUsers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDocuments_CandidateID",
                table: "CandidateDocuments",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDocuments_StatusID",
                table: "CandidateDocuments",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDocuments_VerifiedByUserID",
                table: "CandidateDocuments",
                column: "VerifiedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProfiles_CreatedByUserID",
                table: "CandidateProfiles",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProfiles_SourceID",
                table: "CandidateProfiles",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProfiles_UserID",
                table: "CandidateProfiles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_SkillID",
                table: "CandidateSkill",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_VerifiedByUserID",
                table: "CandidateSkill",
                column: "VerifiedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRecords_CandidateID",
                table: "EmployeeRecords",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRecords_JobPositionPositionID",
                table: "EmployeeRecords",
                column: "JobPositionPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewPanels_InterviewerUserID",
                table: "InterviewPanels",
                column: "InterviewerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewResults_InterviewerUserID",
                table: "InterviewResults",
                column: "InterviewerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewResults_InterviewID",
                table: "InterviewResults",
                column: "InterviewID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_JobCandidateLinkLinkID",
                table: "InterviewSchedules",
                column: "JobCandidateLinkLinkID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_LinkID",
                table: "InterviewSchedules",
                column: "LinkID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_TypeID",
                table: "InterviewSchedules",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateLinks_CandidateID",
                table: "JobCandidateLinks",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateLinks_CandidateProfileCandidateID",
                table: "JobCandidateLinks",
                column: "CandidateProfileCandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateLinks_CurrentStatusID",
                table: "JobCandidateLinks",
                column: "CurrentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateLinks_JobPositionPositionID",
                table: "JobCandidateLinks",
                column: "JobPositionPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateLinks_LinkedByUserID",
                table: "JobCandidateLinks",
                column: "LinkedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateLinks_PositionID",
                table: "JobCandidateLinks",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_ClosureReasonID",
                table: "JobPositions",
                column: "ClosureReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_CreatedByUserID",
                table: "JobPositions",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_EventID",
                table: "JobPositions",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_SelectedCandidateID",
                table: "JobPositions",
                column: "SelectedCandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_StatusID",
                table: "JobPositions",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkillDefinitions_SkillID",
                table: "JobSkillDefinitions",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLetters_LinkID",
                table: "OfferLetters",
                column: "LinkID");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentEvents_CreatedBy",
                table: "RecruitmentEvents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningFeedbacks_LinkID",
                table: "ScreeningFeedbacks",
                column: "LinkID");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningFeedbacks_ReviewerUserID",
                table: "ScreeningFeedbacks",
                column: "ReviewerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillRatings_ResultID",
                table: "SkillRatings",
                column: "ResultID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillRatings_SkillID",
                table: "SkillRatings",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeLogs_ChangedByUserID",
                table: "StatusChangeLogs",
                column: "ChangedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeLogs_LinkID",
                table: "StatusChangeLogs",
                column: "LinkID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeLogs_NewStatusID",
                table: "StatusChangeLogs",
                column: "NewStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeLogs_OldStatusID",
                table: "StatusChangeLogs",
                column: "OldStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateDocuments");

            migrationBuilder.DropTable(
                name: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "EmployeeRecords");

            migrationBuilder.DropTable(
                name: "InterviewPanels");

            migrationBuilder.DropTable(
                name: "JobSkillDefinitions");

            migrationBuilder.DropTable(
                name: "OfferLetters");

            migrationBuilder.DropTable(
                name: "ScreeningFeedbacks");

            migrationBuilder.DropTable(
                name: "SkillRatings");

            migrationBuilder.DropTable(
                name: "StatusChangeLogs");

            migrationBuilder.DropTable(
                name: "DocVerifyStatuses");

            migrationBuilder.DropTable(
                name: "InterviewResults");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "InterviewSchedules");

            migrationBuilder.DropTable(
                name: "InterviewTypes");

            migrationBuilder.DropTable(
                name: "JobCandidateLinks");

            migrationBuilder.DropTable(
                name: "ApplicationStatuses");

            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropTable(
                name: "CandidateProfiles");

            migrationBuilder.DropTable(
                name: "ClosureReasons");

            migrationBuilder.DropTable(
                name: "JobStatuses");

            migrationBuilder.DropTable(
                name: "RecruitmentEvents");

            migrationBuilder.DropTable(
                name: "AcquisitionSources");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
