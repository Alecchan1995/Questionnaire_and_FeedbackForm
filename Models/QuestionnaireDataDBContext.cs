#nullable disable warnings
using Microsoft.EntityFrameworkCore;

namespace Questionnaire_and_FeedbackForm.Models
{
    public class QuestionnaireDataDBContext : DbContext
    {
        public QuestionnaireDataDBContext() { }
        public QuestionnaireDataDBContext(DbContextOptions<QuestionnaireDataDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=BIOSReleaseNoteDB;Integrated Security=True");
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
        }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        //public DbSet<SystemName> SystemNames { get; set; }
        public DbSet<ModelOption> ModelOptions { get; set; }
        public DbSet<SystemFeedbackForm> SystemFeedbackForms { get; set; }
        public DbSet<RoleAndPermission> RoleAndPermissions{get;set;}
        public DbSet<OrderPrincipalDataModel> OrderPrincipalDataModels{get;set;}
    }
}