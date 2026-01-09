using Microsoft.EntityFrameworkCore;
using recruitment_process_portal_server.Data.Configurations;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data;


public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<JobStatus> JobStatuses => Set<JobStatus>();
    public DbSet<ApplicationStatus> ApplicationStatuses => Set<ApplicationStatus>();

    public DbSet<InterviewType> InterviewTypes => Set<InterviewType>();
    public DbSet<AcquisitionSource> AcquisitionSources => Set<AcquisitionSource>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<CandidateProfile> CandidateProfiles => Set<CandidateProfile>();
    public DbSet<JobPosition> JobPositions => Set<JobPosition>();
    public DbSet<ClosureReason> ClosureReasons => Set<ClosureReason>();
    public DbSet<RecruitmentEvent> RecruitmentEvents => Set<RecruitmentEvent>();
    public DbSet<JobSkillDefinition> JobSkillDefinitions => Set<JobSkillDefinition>();
    public DbSet<JobCandidateLink> JobCandidateLinks => Set<JobCandidateLink>();
    public DbSet<ScreeningFeedback> ScreeningFeedbacks => Set<ScreeningFeedback>();
    public DbSet<InterviewSchedule> InterviewSchedules => Set<InterviewSchedule>();
    public DbSet<InterviewPanel> InterviewPanels => Set<InterviewPanel>();
    public DbSet<InterviewResult> InterviewResults => Set<InterviewResult>();
    public DbSet<SkillRating> SkillRatings => Set<SkillRating>();
    public DbSet<CandidateDocument> CandidateDocuments => Set<CandidateDocument>();
    public DbSet<DocVerifyStatus> DocVerifyStatuses => Set<DocVerifyStatus>();
    public DbSet<OfferLetter> OfferLetters => Set<OfferLetter>();
    public DbSet<EmployeeRecord> EmployeeRecords => Set<EmployeeRecord>();
    public DbSet<StatusChangeLog> StatusChangeLogs => Set<StatusChangeLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<CandidateSkill>()
            .HasKey(cs => new { cs.CandidateID, cs.SkillID });

        modelBuilder.Entity<JobSkillDefinition>()
            .HasKey(js => new { js.PositionID, js.SkillID });

        modelBuilder.Entity<InterviewPanel>()
            .HasKey(ip => new { ip.InterviewID, ip.InterviewerUserID });
        modelBuilder.Entity<OfferLetter>()
        .Property(o => o.CTC)
        .HasPrecision(18, 2);

        modelBuilder.Entity<UserRole>().HasData(
       new UserRole { RoleID = 1, RoleName = "Recruiter", Description = "Manages job openings, candidates, interviews" },
       new UserRole { RoleID = 2, RoleName = "HR", Description = "Culture fit, negotiations, documentation" },
       new UserRole { RoleID = 3, RoleName = "Interviewer", Description = "Provides interview feedback" },
       new UserRole { RoleID = 4, RoleName = "Reviewer", Description = "Screens CVs and shortlists candidates" },
       new UserRole { RoleID = 5, RoleName = "Admin", Description = "Manages users, roles, system configuration" },
       new UserRole { RoleID = 6, RoleName = "Candidate", Description = "Applies to jobs and uploads documents" },
       new UserRole { RoleID = 7, RoleName = "Viewer", Description = "Read-only access to system data" }
   );
    }

}

