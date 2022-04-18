using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerRequest;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Entities.Models.Modules.Interest;
using System.Threading;
using System.Threading.Tasks;

namespace Stack.DAL
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {

            OnBeforeSaving();

            return base.SaveChanges(acceptAllChangesOnSuccess);

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {

            OnBeforeSaving();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        }

        private void OnBeforeSaving()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Contact>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<ContactPhoneNumber>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<ContactStatus>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<ContactComment>()
            .Property<bool>("IsDeleted");


            modelBuilder.Entity<Customer>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Lead>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<Opportunity>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<Prospect>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<LeadStatus>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<ProspectStatus>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<OpportunityStatus>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<Pool>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<Activity>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<ActivityType>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Section>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<SectionQuestion>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<SectionQuestionOption>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<ProcessFlow>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CustomerRequest>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRType>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRSection>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRSectionQuestion>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRSectionQuestionOption>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Tag>()
            .Property<bool>("IsDeleted");

            //Soft delete query filters . 


            modelBuilder.Entity<Contact>()
                .HasQueryFilter(Contact => EF.Property<bool>(Contact, "IsDeleted") == false);

            modelBuilder.Entity<ContactStatus>()
              .HasQueryFilter(ContactStatus => EF.Property<bool>(ContactStatus, "IsDeleted") == false);

            modelBuilder.Entity<ContactPhoneNumber>()
               .HasQueryFilter(ContactPhoneNumber => EF.Property<bool>(ContactPhoneNumber, "IsDeleted") == false);

            modelBuilder.Entity<ContactComment>()
               .HasQueryFilter(ContactComment => EF.Property<bool>(ContactComment, "IsDeleted") == false);

            modelBuilder.Entity<Customer>()
               .HasQueryFilter(Customer => EF.Property<bool>(Customer, "IsDeleted") == false);

            modelBuilder.Entity<CustomerPhoneNumber>()
               .HasQueryFilter(CustomerPhoneNumber => EF.Property<bool>(CustomerPhoneNumber, "IsDeleted") == false);

            modelBuilder.Entity<Deal>()
               .HasQueryFilter(Deal => EF.Property<bool>(Deal, "IsDeleted") == false);

            modelBuilder.Entity<Lead>()
               .HasQueryFilter(Lead => EF.Property<bool>(Lead, "IsDeleted") == false);

            modelBuilder.Entity<Prospect>()
               .HasQueryFilter(Prospect => EF.Property<bool>(Prospect, "IsDeleted") == false);

            modelBuilder.Entity<Opportunity>()
               .HasQueryFilter(Opportunity => EF.Property<bool>(Opportunity, "IsDeleted") == false);

            modelBuilder.Entity<LeadStatus>()
               .HasQueryFilter(LeadStatus => EF.Property<bool>(LeadStatus, "IsDeleted") == false);

            modelBuilder.Entity<OpportunityStatus>()
              .HasQueryFilter(OpportunityStatus => EF.Property<bool>(OpportunityStatus, "IsDeleted") == false);

            modelBuilder.Entity<ProspectStatus>()
              .HasQueryFilter(ProspectStatus => EF.Property<bool>(ProspectStatus, "IsDeleted") == false);

            modelBuilder.Entity<LeadStatus>()
              .HasQueryFilter(LeadStatus => EF.Property<bool>(LeadStatus, "IsDeleted") == false);

            modelBuilder.Entity<Pool>()
              .HasQueryFilter(Pool => EF.Property<bool>(Pool, "IsDeleted") == false);

            modelBuilder.Entity<LeadStatus>()
              .HasQueryFilter(LeadStatus => EF.Property<bool>(LeadStatus, "IsDeleted") == false);

            modelBuilder.Entity<Activity>()
              .HasQueryFilter(Activity => EF.Property<bool>(Activity, "IsDeleted") == false);

            modelBuilder.Entity<ActivityType>()
              .HasQueryFilter(ActivityType => EF.Property<bool>(ActivityType, "IsDeleted") == false);

            modelBuilder.Entity<Section>()
             .HasQueryFilter(Section => EF.Property<bool>(Section, "IsDeleted") == false);

            modelBuilder.Entity<SectionQuestion>()
             .HasQueryFilter(SectionQuestion => EF.Property<bool>(SectionQuestion, "IsDeleted") == false);

            modelBuilder.Entity<SectionQuestionAnswer>()
             .HasQueryFilter(SectionQuestionAnswer => EF.Property<bool>(SectionQuestionAnswer, "IsDeleted") == false);

            modelBuilder.Entity<SectionQuestionOption>()
             .HasQueryFilter(SectionQuestionOption => EF.Property<bool>(SectionQuestionOption, "IsDeleted") == false);

            modelBuilder.Entity<ProcessFlow>()
             .HasQueryFilter(ProcessFlow => EF.Property<bool>(ProcessFlow, "IsDeleted") == false);

            modelBuilder.Entity<CustomerRequest>()
              .HasQueryFilter(CustomerRequest => EF.Property<bool>(CustomerRequest, "IsDeleted") == false);

            modelBuilder.Entity<CRType>()
              .HasQueryFilter(CRType => EF.Property<bool>(CRType, "IsDeleted") == false);

            modelBuilder.Entity<CRSection>()
             .HasQueryFilter(CRSection => EF.Property<bool>(CRSection, "IsDeleted") == false);

            modelBuilder.Entity<CRSectionQuestion>()
             .HasQueryFilter(CRSectionQuestion => EF.Property<bool>(CRSectionQuestion, "IsDeleted") == false);

            modelBuilder.Entity<CRSectionQuestionAnswer>()
             .HasQueryFilter(CRSectionQuestionAnswer => EF.Property<bool>(CRSectionQuestionAnswer, "IsDeleted") == false);

            modelBuilder.Entity<CRSectionQuestionOption>()
             .HasQueryFilter(CRSectionQuestionOption => EF.Property<bool>(CRSectionQuestionOption, "IsDeleted") == false);


            modelBuilder.Entity<Tag>()
             .HasQueryFilter(Tag => EF.Property<bool>(Tag, "IsDeleted") == false);



            modelBuilder.Entity<Location_Pool>().HasKey(x => new { x.LocationID, x.PoolID });
            modelBuilder.Entity<Location_Pool>()
            .HasOne(pr => pr.Location)
            .WithMany(p => p.Pools)
            .HasForeignKey(pr => pr.LocationID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Location_Pool>()
            .HasOne(pr => pr.Pool)
            .WithMany(p => p.Location_Pools)
            .HasForeignKey(pr => pr.PoolID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Pool_Users>().HasKey(x => new { x.PoolID, x.UserID });
            modelBuilder.Entity<Pool_Users>()
            .HasOne(pr => pr.Pool)
            .WithMany(p => p.Pool_Users)
            .HasForeignKey(pr => pr.PoolID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Pool_Users>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.Pools)
            .HasForeignKey(pr => pr.UserID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Pool_Admin>().HasKey(x => new { x.PoolID, x.UserID });
            modelBuilder.Entity<Pool_Admin>()
            .HasOne(pr => pr.Pool)
            .WithMany(p => p.Pool_Admins)
            .HasForeignKey(pr => pr.PoolID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Pool_Admin>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.Pool_Admins)
            .HasForeignKey(pr => pr.UserID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Contact_Tag>().HasKey(x => new { x.ContactID, x.TagID });
            modelBuilder.Entity<Contact_Tag>()
            .HasOne(pr => pr.Contact)
            .WithMany(p => p.Tags)
            .HasForeignKey(pr => pr.ContactID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Contact_Tag>()
            .HasOne(pr => pr.Tag)
            .WithMany(p => p.ContactTags)
            .HasForeignKey(pr => pr.TagID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Customer_Tag>().HasKey(x => new { x.CustomerID, x.TagID });
            modelBuilder.Entity<Customer_Tag>()
            .HasOne(pr => pr.Customer)
            .WithMany(p => p.Tags)
            .HasForeignKey(pr => pr.CustomerID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Customer_Tag>()
            .HasOne(pr => pr.Tag)
            .WithMany(p => p.CustomerTags)
            .HasForeignKey(pr => pr.TagID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<ActivitySection>()
            .HasOne(pr => pr.Activity)
            .WithMany(p => p.ActivitySections)
            .HasForeignKey(pr => pr.ActivityID).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ActivitySection>()
            .HasOne(pr => pr.Section)
            .WithMany(p => p.ActivitySections)
            .HasForeignKey(pr => pr.SectionID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SectionQuestionAnswer>()
           .HasOne(pr => pr.ActivitySection)
           .WithMany(p => p.QuestionAnswers)
           .HasForeignKey(pr => pr.ActivitySectionID).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<SectionQuestionAnswer>()
            .HasOne(pr => pr.Question)
            .WithMany(p => p.QuestionAnswers)
            .HasForeignKey(pr => pr.QuestionID).OnDelete(DeleteBehavior.SetNull);



            modelBuilder.Entity<CR_Section>()
            .HasOne(pr => pr.CustomerRequest)
            .WithMany(p => p.RequestSections)
            .HasForeignKey(pr => pr.RequestID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CR_Section>()
            .HasOne(pr => pr.Section)
            .WithMany(p => p.RequestSections)
            .HasForeignKey(pr => pr.SectionID).OnDelete(DeleteBehavior.NoAction);


        }


        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Contact_Tag> Contact_Tags { get; set; }
        public virtual DbSet<Customer_Tag> Customer_Tags { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactStatus> ContactStatuses { get; set; }
        public virtual DbSet<ContactPhoneNumber> ContactPhoneNumbers { get; set; }
        public virtual DbSet<ContactComment> ContactComments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<LeadStatus> LeadStatuses { get; set; }
        public virtual DbSet<Opportunity> Opportunities { get; set; }
        public virtual DbSet<OpportunityStatus> OpportunityStatuses { get; set; }
        public virtual DbSet<Prospect> Prospects { get; set; }
        public virtual DbSet<ProspectStatus> ProspectStatuses { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Pool> Pools { get; set; }
        public virtual DbSet<Pool_Users> Pool_Users { get; set; }
        public virtual DbSet<Pool_Admin> Pool_Admins { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivitySection> ActivitySections { get; set; }
        public virtual DbSet<SubmissionDetails> SubmissionDetails { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionQuestion> SectionQuestions { get; set; }
        public virtual DbSet<SectionQuestionAnswer> SectionQuestionAnswers { get; set; }
        public virtual DbSet<SectionQuestionOption> SectionQuestionOptions { get; set; }
        public virtual DbSet<SelectedOption> SelectedOptions { get; set; }

        public virtual DbSet<CustomerRequest> CustomerRequests { get; set; }
        public virtual DbSet<CR_Section> CR_Sections { get; set; }
        public virtual DbSet<CRSubmissionDetails> CRSubmissionDetails { get; set; }
        public virtual DbSet<CRType> CRTypes { get; set; }
        public virtual DbSet<CRSection> CRSections { get; set; }
        public virtual DbSet<CRSectionQuestion> CRSectionQuestions { get; set; }
        public virtual DbSet<CRSectionQuestionAnswer> CRSectionQuestionAnswers { get; set; }
        public virtual DbSet<CRSectionQuestionOption> CRSectionQuestionOptions { get; set; }
        public virtual DbSet<CRSelectedOption> CRSelectedOptions { get; set; }

        public virtual DbSet<ProcessFlow> ProcessFlows { get; set; }
        public virtual DbSet<Location_Pool> Location_Pools { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<LSection> LSections { get; set; }
        public virtual DbSet<Input> Inputs { get; set; }
        public virtual DbSet<LInterestInput> LInterestInputs { get; set; }
        public virtual DbSet<LInterest> LInterests { get; set; }
        public virtual DbSet<LAttribute> LAttributes { get; set; }

    }

}
