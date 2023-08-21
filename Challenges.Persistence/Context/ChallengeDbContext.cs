using Challenges.Domain.Entities;
using Challenges.Domain.Entities.Question;
using Challenges.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Context;

public class ChallengeDbContext : DbContext
{
    public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Survey> Surveys { get; set; }
    public DbSet<SurveyGenre> SurveyGenres { get; set; }
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<SurveyTag> SurveyTags { get; set; }
    public DbSet<SurveyType> SurveyTypes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionType> QuestionTypes { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<ChallengeRequest> ChallengeRequests { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationDetail> NotificationDetails { get; set; }
    public DbSet<NotificationType> NotificationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChallengeDbContext).Assembly);
        modelBuilder.HasDefaultSchema("challenge");
        base.OnModelCreating(modelBuilder);
    }
}