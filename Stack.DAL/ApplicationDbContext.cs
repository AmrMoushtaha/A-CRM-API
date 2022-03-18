using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Region;
using System.Linq;
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

            modelBuilder.Entity<Area>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<LOneInterest>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<LOneInterestInput>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<LTwoInterest>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<LTwoInterestInput>()
             .Property<bool>("IsDeleted");

            modelBuilder.Entity<LThreeInterest>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<LThreeInterestInput>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<InterestAttribute>()
            .Property<bool>("IsDeleted");

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

            modelBuilder.Entity<Tag>()
            .Property<bool>("IsDeleted");

            //Soft delete query filters . 

            modelBuilder.Entity<Region>()
            .HasQueryFilter(Region => EF.Property<bool>(Region, "IsDeleted") == false);

            modelBuilder.Entity<Area>()
               .HasQueryFilter(Area => EF.Property<bool>(Area, "IsDeleted") == false);

            modelBuilder.Entity<LOneInterest>()
               .HasQueryFilter(LOneInterest => EF.Property<bool>(LOneInterest, "IsDeleted") == false);

            modelBuilder.Entity<LOneInterestInput>()
               .HasQueryFilter(LOneInterestInput => EF.Property<bool>(LOneInterestInput, "IsDeleted") == false);

            modelBuilder.Entity<LTwoInterest>()
               .HasQueryFilter(LTwoInterest => EF.Property<bool>(LTwoInterest, "IsDeleted") == false);

            modelBuilder.Entity<LTwoInterestInput>()
               .HasQueryFilter(LTwoInterestInput => EF.Property<bool>(LTwoInterestInput, "IsDeleted") == false);

            modelBuilder.Entity<LThreeInterest>()
               .HasQueryFilter(LThreeInterest => EF.Property<bool>(LThreeInterest, "IsDeleted") == false);

            modelBuilder.Entity<LThreeInterestInput>()
               .HasQueryFilter(LThreeInterestInput => EF.Property<bool>(LThreeInterestInput, "IsDeleted") == false);

            modelBuilder.Entity<InterestAttribute>()
               .HasQueryFilter(InterestAttribute => EF.Property<bool>(InterestAttribute, "IsDeleted") == false);

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

            modelBuilder.Entity<Tag>()
             .HasQueryFilter(Tag => EF.Property<bool>(Tag, "IsDeleted") == false);



            modelBuilder.Entity<Area_Pool>().HasKey(x => new { x.AreaID, x.PoolID });
            modelBuilder.Entity<Area_Pool>()
            .HasOne(pr => pr.Area)
            .WithMany(p => p.Pools)
            .HasForeignKey(pr => pr.AreaID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Area_Pool>()
            .HasOne(pr => pr.Pool)
            .WithMany(p => p.Area_Pools)
            .HasForeignKey(pr => pr.PoolID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Area_LOneInterest>().HasKey(x => new { x.AreaID, x.LOneInterestID });
            modelBuilder.Entity<Area_LOneInterest>()
            .HasOne(pr => pr.Area)
            .WithMany(p => p.Interests)
            .HasForeignKey(pr => pr.AreaID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Area_LOneInterest>()
            .HasOne(pr => pr.Interest)
            .WithMany(p => p.Area_LOneInterests)
            .HasForeignKey(pr => pr.LOneInterestID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LOneInterest_InterestAttributes>().HasKey(x => new { x.LOneInterestID, x.InterestAttributeID });
            modelBuilder.Entity<LOneInterest_InterestAttributes>()
            .HasOne(pr => pr.LOneInterest)
            .WithMany(p => p.Attributes)
            .HasForeignKey(pr => pr.LOneInterestID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LOneInterest_InterestAttributes>()
            .HasOne(pr => pr.InterestAttribute)
            .WithMany(p => p.LevelOne)
            .HasForeignKey(pr => pr.LOneInterestID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LOneInterest_LOneInterestInput>().HasKey(x => new { x.LOneInterestID, x.LOneInterestInputID });
            modelBuilder.Entity<LOneInterest_LOneInterestInput>()
            .HasOne(pr => pr.LOneInterest)
            .WithMany(p => p.Inputs)
            .HasForeignKey(pr => pr.LOneInterestID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LOneInterest_LOneInterestInput>()
            .HasOne(pr => pr.LOneInterestInput)
            .WithMany(p => p.LOneInterest_LOneInterestInputs)
            .HasForeignKey(pr => pr.LOneInterestInputID).OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<LTwoInterest_InterestAttributes>().HasKey(x => new { x.LTwoInterestID, x.InterestAttributeID });
            modelBuilder.Entity<LTwoInterest_InterestAttributes>()
            .HasOne(pr => pr.LTwoInterest)
            .WithMany(p => p.Attributes)
            .HasForeignKey(pr => pr.LTwoInterestID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LTwoInterest_InterestAttributes>()
            .HasOne(pr => pr.InterestAttribute)
            .WithMany(p => p.LevelTwo)
            .HasForeignKey(pr => pr.LTwoInterestID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LTwoInterest_LTwoInterestInput>().HasKey(x => new { x.LTwoInterestID, x.LTwoInterestInputID });
            modelBuilder.Entity<LTwoInterest_LTwoInterestInput>()
            .HasOne(pr => pr.LTwoInterest)
            .WithMany(p => p.Inputs)
            .HasForeignKey(pr => pr.LTwoInterestID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LTwoInterest_LTwoInterestInput>()
            .HasOne(pr => pr.LTwoInterestInput)
            .WithMany(p => p.LTwoInterest_LTwoInterestInputs)
            .HasForeignKey(pr => pr.LTwoInterestInputID).OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<LThreeInterest_InterestAttributes>().HasKey(x => new { x.LThreeInterestID, x.InterestAttributeID });
            modelBuilder.Entity<LThreeInterest_InterestAttributes>()
            .HasOne(pr => pr.LThreeInterest)
            .WithMany(p => p.Attributes)
            .HasForeignKey(pr => pr.LThreeInterestID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LThreeInterest_InterestAttributes>()
            .HasOne(pr => pr.InterestAttribute)
            .WithMany(p => p.LevelThree)
            .HasForeignKey(pr => pr.LThreeInterestID).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LThreeInterest_LThreeInterestInput>().HasKey(x => new { x.LThreeInterestID, x.LThreeInterestInputID });
            modelBuilder.Entity<LThreeInterest_LThreeInterestInput>()
            .HasOne(pr => pr.LThreeInterest)
            .WithMany(p => p.Inputs)
            .HasForeignKey(pr => pr.LThreeInterestID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LThreeInterest_LThreeInterestInput>()
            .HasOne(pr => pr.LThreeInterestInput)
            .WithMany(p => p.LThreeInterest_LThreeInterestInput)
            .HasForeignKey(pr => pr.LThreeInterestInputID).OnDelete(DeleteBehavior.NoAction);


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
            .HasForeignKey(pr => pr.ActivityID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ActivitySection>()
            .HasOne(pr => pr.Section)
            .WithMany(p => p.ActivitySections)
            .HasForeignKey(pr => pr.SectionID).OnDelete(DeleteBehavior.NoAction);


        }


        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Area_Pool> Area_Pools { get; set; }
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
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionQuestion> SectionQuestions { get; set; }
        public virtual DbSet<SectionQuestionAnswer> SectionQuestionAnswers { get; set; }
        public virtual DbSet<SectionQuestionOption> SectionQuestionOptions { get; set; }
        public virtual DbSet<ProcessFlow> ProcessFlows { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Area_LOneInterest> Area_LOneInterests { get; set; }
        public virtual DbSet<LOneInterest> LOneInterests { get; set; }
        public virtual DbSet<LOneInterestInput> LOneInterestInputs { get; set; }
        public virtual DbSet<LTwoInterest> LTwoInterests { get; set; }
        public virtual DbSet<LTwoInterestInput> LTwoInterestInputs { get; set; }
        public virtual DbSet<LTwoInterest_InterestAttributes> LTwoInterest_InterestAttributes { get; set; }
        public virtual DbSet<LTwoInterest_LTwoInterestInput> LTwoInterest_LTwoInterestInputs { get; set; }
        public virtual DbSet<LThreeInterest> LThreeInterests { get; set; }
        public virtual DbSet<LThreeInterestInput> LThreeInterestInputs { get; set; }
        public virtual DbSet<LThreeInterest_InterestAttributes> LThreeInterest_InterestAttributes { get; set; }
        public virtual DbSet<LThreeInterest_LThreeInterestInput> LThreeInterest_LThreeInterestInputs { get; set; }
        public virtual DbSet<InterestAttribute> InterestAttributes { get; set; }

    }

}
