using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Channel;
using Stack.Entities.Models.Modules.Channels;
using Stack.Entities.Models.Modules.Chat;
using Stack.Entities.Models.Modules.Common;
using Stack.Entities.Models.Modules.CR;
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
            //modelBuilder.SeedInitializer();

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

            modelBuilder.Entity<CustomerComment>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Deal>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<Lead>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<Opportunity>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<Prospect>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<DoneDeal>()
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

            modelBuilder.Entity<Tag>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Channel>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<LeadSourceName>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<LeadSourceType>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CustomerRequestType>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CustomerRequest>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRTimeline>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRPhase>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRPhaseInput>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRPhaseInputOption>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CR_Timeline>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRTimeline_Phase>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CR_Timeline_Phase>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CRPhaseInputAnswer>()
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


            modelBuilder.Entity<CustomerComment>()
               .HasQueryFilter(CustomerComment => EF.Property<bool>(CustomerComment, "IsDeleted") == false);

            modelBuilder.Entity<CustomerPhoneNumber>()
               .HasQueryFilter(CustomerPhoneNumber => EF.Property<bool>(CustomerPhoneNumber, "IsDeleted") == false);

            modelBuilder.Entity<Deal>()
               .HasQueryFilter(Deal => EF.Property<bool>(Deal, "IsDeleted") == false);


            modelBuilder.Entity<Lead>()
               .HasQueryFilter(Lead => EF.Property<bool>(Lead, "IsDeleted") == false);

            modelBuilder.Entity<Prospect>()
               .HasQueryFilter(Prospect => EF.Property<bool>(Prospect, "IsDeleted") == false);

            modelBuilder.Entity<DoneDeal>()
               .HasQueryFilter(DoneDeal => EF.Property<bool>(DoneDeal, "IsDeleted") == false);

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


            modelBuilder.Entity<SectionQuestionOption>()
             .HasQueryFilter(SectionQuestionOption => EF.Property<bool>(SectionQuestionOption, "IsDeleted") == false);

            modelBuilder.Entity<ProcessFlow>()
             .HasQueryFilter(ProcessFlow => EF.Property<bool>(ProcessFlow, "IsDeleted") == false);


            modelBuilder.Entity<Tag>()
             .HasQueryFilter(Tag => EF.Property<bool>(Tag, "IsDeleted") == false);

            modelBuilder.Entity<Channel>()
             .HasQueryFilter(Channel => EF.Property<bool>(Channel, "IsDeleted") == false);

            modelBuilder.Entity<LeadSourceName>()
             .HasQueryFilter(LeadSourceName => EF.Property<bool>(LeadSourceName, "IsDeleted") == false);

            modelBuilder.Entity<LeadSourceType>()
             .HasQueryFilter(LeadSourceType => EF.Property<bool>(LeadSourceType, "IsDeleted") == false);

            modelBuilder.Entity<CustomerRequestType>()
            .HasQueryFilter(CustomerRequestType => EF.Property<bool>(CustomerRequestType, "IsDeleted") == false);

            modelBuilder.Entity<CustomerRequest>()
            .HasQueryFilter(CustomerRequest => EF.Property<bool>(CustomerRequest, "IsDeleted") == false);

            modelBuilder.Entity<CRTimeline>()
            .HasQueryFilter(CRTimeline => EF.Property<bool>(CRTimeline, "IsDeleted") == false);

            modelBuilder.Entity<CRPhase>()
            .HasQueryFilter(CRPhase => EF.Property<bool>(CRPhase, "IsDeleted") == false);

            modelBuilder.Entity<CRPhaseInput>()
            .HasQueryFilter(CRPhaseInput => EF.Property<bool>(CRPhaseInput, "IsDeleted") == false);

            modelBuilder.Entity<CRPhaseInputOption>()
            .HasQueryFilter(CRPhaseInputOption => EF.Property<bool>(CRPhaseInputOption, "IsDeleted") == false);

            modelBuilder.Entity<CR_Timeline>()
            .HasQueryFilter(CR_Timeline => EF.Property<bool>(CR_Timeline, "IsDeleted") == false);

            modelBuilder.Entity<CRTimeline_Phase>()
            .HasQueryFilter(CRTimeline_Phase => EF.Property<bool>(CRTimeline_Phase, "IsDeleted") == false);

            modelBuilder.Entity<CR_Timeline_Phase>()
            .HasQueryFilter(CR_Timeline_Phase => EF.Property<bool>(CR_Timeline_Phase, "IsDeleted") == false);

            modelBuilder.Entity<CRPhaseInputAnswer>()
            .HasQueryFilter(CRPhaseInputAnswer => EF.Property<bool>(CRPhaseInputAnswer, "IsDeleted") == false);


            modelBuilder.Entity<Deal>()
               .HasOne(a => a.Customer)
               .WithMany(au => au.Deals)
               .HasForeignKey(a => a.CustomerID);

            //modelBuilder.Entity<CR_Timeline>()
            //    .HasKey(a => a.ID);

            modelBuilder.Entity<CR_Timeline>()
               .HasOne(a => a.Timeline)
               .WithMany(a => a.CustomerRequests)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CR_Timeline>()
               .HasOne(a => a.CustomerRequest)
               .WithMany(a => a.Timeline)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CRTimeline_Phase>()
               .HasOne(a => a.Timeline)
               .WithMany(a => a.Phases)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CRTimeline_Phase>()
               .HasOne(a => a.Phase)
               .WithMany(a => a.Timelines)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CR_Timeline_Phase>()
               .HasOne(a => a.Timeline_Phase)
               .WithMany(a => a.RequestTimelinePhaseDetails)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<CRPhaseInputAnswer>()
            .HasOne(a => a.RequestPhase)
            .WithMany(a => a.Answers)
            .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CRPhaseInputAnswer>()
            .HasOne(a => a.Input)
            .WithMany(a => a.Answers)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Prospect>()
               .HasOne(a => a.Deal)
               .WithMany(au => au.Prospects)
               .HasForeignKey(a => a.DealID);

            modelBuilder.Entity<Location_Pool>().HasKey(x => new { x.LocationID, x.PoolID });
            modelBuilder.Entity<Location_Pool>()
            .HasOne(pr => pr.Location)
            .WithMany(p => p.Pools)
            .HasForeignKey(pr => pr.LocationID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Location_Pool>()
            .HasOne(pr => pr.Pool)
            .WithMany(p => p.Location_Pools)
            .HasForeignKey(pr => pr.PoolID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Pool_User>().HasKey(x => new { x.PoolID, x.UserID });
            modelBuilder.Entity<Pool_User>()
            .HasOne(pr => pr.Pool)
            .WithMany(p => p.Pool_Users)
            .HasForeignKey(pr => pr.PoolID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Pool_User>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.Pools)
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

            modelBuilder.Entity<Contact_Favorite>().HasKey(x => new { x.ContactID, x.UserID });
            modelBuilder.Entity<Contact_Favorite>()
            .HasOne(pr => pr.Contact)
            .WithMany(p => p.Favorites)
            .HasForeignKey(pr => pr.ContactID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Contact_Favorite>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.Contact_Favorites)
            .HasForeignKey(pr => pr.UserID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Prospect_Favorite>().HasKey(x => new { x.RecordID, x.UserID });
            modelBuilder.Entity<Prospect_Favorite>()
            .HasOne(pr => pr.Record)
            .WithMany(p => p.Favorites)
            .HasForeignKey(pr => pr.RecordID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Prospect_Favorite>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.Prospect_Favorites)
            .HasForeignKey(pr => pr.UserID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Lead_Favorite>().HasKey(x => new { x.RecordID, x.UserID });
            modelBuilder.Entity<Lead_Favorite>()
            .HasOne(pr => pr.Record)
            .WithMany(p => p.Favorites)
            .HasForeignKey(pr => pr.RecordID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Lead_Favorite>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.Lead_Favorites)
            .HasForeignKey(pr => pr.UserID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Opportunity_Favorite>().HasKey(x => new { x.RecordID, x.UserID });
            modelBuilder.Entity<Opportunity_Favorite>()
            .HasOne(pr => pr.Record)
            .WithMany(p => p.Favorites)
            .HasForeignKey(pr => pr.RecordID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Opportunity_Favorite>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.Opportunity_Favorites)
            .HasForeignKey(pr => pr.UserID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoneDeal_Favorite>().HasKey(x => new { x.RecordID, x.UserID });
            modelBuilder.Entity<DoneDeal_Favorite>()
            .HasOne(pr => pr.Record)
            .WithMany(p => p.Favorites)
            .HasForeignKey(pr => pr.RecordID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DoneDeal_Favorite>()
            .HasOne(pr => pr.User)
            .WithMany(p => p.DoneDeal_Favorites)
            .HasForeignKey(pr => pr.UserID).OnDelete(DeleteBehavior.NoAction);

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


        }


        public virtual DbSet<Pool> Pools { get; set; }
        public virtual DbSet<Pool_User> Pool_Users { get; set; }
        public virtual DbSet<PoolConnectionID> PoolConnectionIDs { get; set; }
        public virtual DbSet<PoolRequest> PoolRequests { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Contact_Tag> Contact_Tags { get; set; }
        public virtual DbSet<Customer_Tag> Customer_Tags { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactStatus> ContactStatuses { get; set; }
        public virtual DbSet<ContactPhoneNumber> ContactPhoneNumbers { get; set; }
        public virtual DbSet<ContactComment> ContactComments { get; set; }
        public virtual DbSet<CustomerComment> CustomerComments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Prospect> Prospects { get; set; }
        public virtual DbSet<ProspectStatus> ProspectStatuses { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<LeadStatus> LeadStatuses { get; set; }
        public virtual DbSet<Opportunity> Opportunities { get; set; }
        public virtual DbSet<OpportunityStatus> OpportunityStatuses { get; set; }
        public virtual DbSet<DoneDeal> DoneDeals { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivitySection> ActivitySections { get; set; }
        public virtual DbSet<SubmissionDetails> SubmissionDetails { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionQuestion> SectionQuestions { get; set; }
        public virtual DbSet<SectionQuestionAnswer> SectionQuestionAnswers { get; set; }
        public virtual DbSet<SectionQuestionOption> SectionQuestionOptions { get; set; }
        public virtual DbSet<SelectedOption> SelectedOptions { get; set; }
        public virtual DbSet<ProcessFlow> ProcessFlows { get; set; }
        public virtual DbSet<Location_Pool> Location_Pools { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<LSection> LSections { get; set; }
        public virtual DbSet<Input> Inputs { get; set; }
        public virtual DbSet<LInterestInput> LInterestInputs { get; set; }
        public virtual DbSet<LInterest> LInterests { get; set; }
        public virtual DbSet<SystemConfiguration> SystemConfiguration { get; set; }
        public virtual DbSet<AuthorizationSection> AuthorizationSections { get; set; }
        public virtual DbSet<SectionAuthorization> SectionAuthorizations { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<LeadSourceName> LeadSourceNames { get; set; }
        public virtual DbSet<LeadSourceType> LeadSourceTypes { get; set; }
        public virtual DbSet<LAttribute> LAttributes { get; set; }
        //Favorites
        public virtual DbSet<Contact_Favorite> Contact_Favorites { get; set; }
        public virtual DbSet<Prospect_Favorite> Prospect_Favorites { get; set; }
        public virtual DbSet<Lead_Favorite> Lead_Favorites { get; set; }
        public virtual DbSet<Opportunity_Favorite> Opportunity_Favorites { get; set; }
        public virtual DbSet<DoneDeal_Favorite> DoneDeal_Favorites { get; set; }

        //Customer Request
        public virtual DbSet<CustomerRequestType> CustomerRequestTypes { get; set; }
        public virtual DbSet<CustomerRequest> CustomerRequests { get; set; }
        public virtual DbSet<CRTimeline> CRTimelines { get; set; }
        public virtual DbSet<CRPhase> CRPhases { get; set; }
        public virtual DbSet<CRPhaseInput> CRPhaseInputs { get; set; }
        public virtual DbSet<CRPhaseInputOption> CRPhaseInputOptions { get; set; }
        public virtual DbSet<CR_Timeline> CR_Timelines { get; set; }
        public virtual DbSet<CRTimeline_Phase> CRTimeline_Phases { get; set; }
        public virtual DbSet<CR_Timeline_Phase> CR_Timeline_Phases { get; set; }
        public virtual DbSet<CRPhaseInputAnswer> CRPhaseInputAnswers { get; set; }

        //chat
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<UsersConversations> UsersConversations { get; set; }




    }

}
