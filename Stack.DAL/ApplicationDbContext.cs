using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Employees;
using Stack.Entities.Models.Modules.Materials;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Entities.Models.Modules.PRS;
using Stack.Entities.Models.Modules.RFQS;
using Stack.Entities.Models.Modules.Vendors;
using System.Threading;
using System.Threading.Tasks;

namespace Stack.DAL
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

            foreach (var entry in ChangeTracker.Entries<Employee>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<PurchasingGroup>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<CompanyCode>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<CostCenter>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ProfitCenter>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<OrgUnit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Action>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Material>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<MaterialGroup>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<MaterialType>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Plant>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<StorageLocation>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<UOM>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<GLAccount>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Condition>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<PR>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<PRMainItem>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<PRSubItem>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<PRType>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<RFQ>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<RFQMainItem>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<RFQSubItem>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<RFQType>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ExchangeRate>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Vendor>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<VendorAddress>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<VendorPhoneNumber>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<VendorEmail>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Tax>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Bank>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Position>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<EmployeeGroup>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<EmployeeSubGroup>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Employee_PhoneNumber>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Adding IsDeleted property to models where soft delete is applied . 
            modelBuilder.Entity<Employee>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CompanyCode>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<CostCenter>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<ProfitCenter>()
             .Property<bool>("IsDeleted");

            modelBuilder.Entity<OrgUnit>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Action>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Material>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<MaterialGroup>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<MaterialType>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Plant>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<StorageLocation>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<UOM>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<GLAccount>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Condition>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<PR>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<PRMainItem>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<PRSubItem>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<PRType>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<RFQ>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<RFQMainItem>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<RFQSubItem>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<RFQType>()
           .Property<bool>("IsDeleted");

            modelBuilder.Entity<ExchangeRate>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Vendor>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<VendorEmail>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<VendorAddress>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<VendorPhoneNumber>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Tax>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Bank>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Position>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<EmployeeGroup>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<EmployeeSubGroup>()
            .Property<bool>("IsDeleted");

            modelBuilder.Entity<Employee_PhoneNumber>()
            .Property<bool>("IsDeleted");
            modelBuilder.Entity<PurchasingGroup>()
            .Property<bool>("IsDeleted");


            //Soft delete query filters . 
            modelBuilder.Entity<Vendor>()
               .HasQueryFilter(Vendor => EF.Property<bool>(Vendor, "IsDeleted") == false);

            modelBuilder.Entity<VendorAddress>()
               .HasQueryFilter(VendorAddress => EF.Property<bool>(VendorAddress, "IsDeleted") == false);

            modelBuilder.Entity<VendorEmail>()
               .HasQueryFilter(VendorEmail => EF.Property<bool>(VendorEmail, "IsDeleted") == false);

            modelBuilder.Entity<VendorPhoneNumber>()
               .HasQueryFilter(VendorPhoneNumber => EF.Property<bool>(VendorPhoneNumber, "IsDeleted") == false);

            modelBuilder.Entity<Tax>()
               .HasQueryFilter(Tax => EF.Property<bool>(Tax, "IsDeleted") == false);

            modelBuilder.Entity<Bank>()
               .HasQueryFilter(Bank => EF.Property<bool>(Bank, "IsDeleted") == false);


            modelBuilder.Entity<Employee>()
               .HasQueryFilter(Employee => EF.Property<bool>(Employee, "IsDeleted") == false);

            modelBuilder.Entity<Employee>()
                .HasQueryFilter(Employee => EF.Property<bool>(Employee, "IsDeleted") == false);

            modelBuilder.Entity<CompanyCode>()
                .HasQueryFilter(CompanyCode => EF.Property<bool>(CompanyCode, "IsDeleted") == false);

            modelBuilder.Entity<CostCenter>()
                .HasQueryFilter(CostCenter => EF.Property<bool>(CostCenter, "IsDeleted") == false);

            modelBuilder.Entity<ProfitCenter>()
               .HasQueryFilter(ProfitCenter => EF.Property<bool>(ProfitCenter, "IsDeleted") == false);

            modelBuilder.Entity<OrgUnit>()
                .HasQueryFilter(OrgUnit => EF.Property<bool>(OrgUnit, "IsDeleted") == false);

            modelBuilder.Entity<Action>()
                .HasQueryFilter(Action => EF.Property<bool>(Action, "IsDeleted") == false);

            modelBuilder.Entity<Material>()
                .HasQueryFilter(Material => EF.Property<bool>(Material, "IsDeleted") == false);

            modelBuilder.Entity<MaterialGroup>()
                .HasQueryFilter(MaterialGroup => EF.Property<bool>(MaterialGroup, "IsDeleted") == false);

            modelBuilder.Entity<MaterialType>()
                .HasQueryFilter(MaterialType => EF.Property<bool>(MaterialType, "IsDeleted") == false);

            modelBuilder.Entity<Plant>()
                .HasQueryFilter(Plant => EF.Property<bool>(Plant, "IsDeleted") == false);

            modelBuilder.Entity<StorageLocation>()
                .HasQueryFilter(StorageLocation => EF.Property<bool>(StorageLocation, "IsDeleted") == false);

            modelBuilder.Entity<UOM>()
                .HasQueryFilter(UOM => EF.Property<bool>(UOM, "IsDeleted") == false);

            modelBuilder.Entity<GLAccount>()
             .HasQueryFilter(GLAccount => EF.Property<bool>(GLAccount, "IsDeleted") == false);

            modelBuilder.Entity<PR>()
             .HasQueryFilter(PR => EF.Property<bool>(PR, "IsDeleted") == false);

            modelBuilder.Entity<PRMainItem>()
             .HasQueryFilter(PRMainItem => EF.Property<bool>(PRMainItem, "IsDeleted") == false);

            modelBuilder.Entity<PRSubItem>()
             .HasQueryFilter(PRSubItem => EF.Property<bool>(PRSubItem, "IsDeleted") == false);

            modelBuilder.Entity<PRType>()
             .HasQueryFilter(PRType => EF.Property<bool>(PRType, "IsDeleted") == false);

            modelBuilder.Entity<Condition>()
            .HasQueryFilter(Condition => EF.Property<bool>(Condition, "IsDeleted") == false);


            modelBuilder.Entity<RFQ>()
             .HasQueryFilter(RFQ => EF.Property<bool>(RFQ, "IsDeleted") == false);

            modelBuilder.Entity<RFQMainItem>()
             .HasQueryFilter(RFQMainItem => EF.Property<bool>(RFQMainItem, "IsDeleted") == false);

            modelBuilder.Entity<RFQSubItem>()
             .HasQueryFilter(RFQSubItem => EF.Property<bool>(RFQSubItem, "IsDeleted") == false);

            modelBuilder.Entity<RFQType>()
             .HasQueryFilter(RFQType => EF.Property<bool>(RFQType, "IsDeleted") == false);

            modelBuilder.Entity<ExchangeRate>()
            .HasQueryFilter(ExchangeRate => EF.Property<bool>(ExchangeRate, "IsDeleted") == false);

            modelBuilder.Entity<Position>()
            .HasQueryFilter(Position => EF.Property<bool>(Position, "IsDeleted") == false);

            modelBuilder.Entity<EmployeeSubGroup>()
             .HasQueryFilter(EmployeeSubGroup => EF.Property<bool>(EmployeeSubGroup, "IsDeleted") == false);

            modelBuilder.Entity<EmployeeGroup>()
            .HasQueryFilter(EmployeeGroup => EF.Property<bool>(EmployeeGroup, "IsDeleted") == false);

            modelBuilder.Entity<Employee_PhoneNumber>()
            .HasQueryFilter(Employee_PhoneNumber => EF.Property<bool>(Employee_PhoneNumber, "IsDeleted") == false);
            modelBuilder.Entity<PurchasingGroup>()
           .HasQueryFilter(PurchasingGroup => EF.Property<bool>(PurchasingGroup, "IsDeleted") == false);

            //Many to many relationships configuration . 
            modelBuilder.Entity<Employee_Action>().HasKey(x => new { x.EmployeeID, x.ActionID });
            modelBuilder.Entity<Employee_Action>()
            .HasOne(pr => pr.Employee)
            .WithMany(p => p.Employee_Actions)
            .HasForeignKey(pr => pr.EmployeeID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee_Action>()
            .HasOne(pr => pr.Action)
            .WithMany(p => p.Employee_Actions)
            .HasForeignKey(pr => pr.ActionID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee_PurchasingGroup>().HasKey(x => new { x.EmployeeID, x.PurchasingGroupID });
            modelBuilder.Entity<Employee_PurchasingGroup>()
            .HasOne(pr => pr.Employee)
            .WithMany(p => p.Employee_PurchasingGroups)
            .HasForeignKey(pr => pr.EmployeeID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee_PurchasingGroup>()
            .HasOne(pr => pr.PurchasingGroup)
            .WithMany(p => p.PurchasingGroup_Employees)
            .HasForeignKey(pr => pr.PurchasingGroupID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Material_UOM>().HasKey(x => new { x.MaterialID, x.UOMID });
            modelBuilder.Entity<Material_UOM>()
            .HasOne(pr => pr.Material)
            .WithMany(p => p.Material_UOMS)
            .HasForeignKey(pr => pr.MaterialID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Material_UOM>()
            .HasOne(pr => pr.UOM)
            .WithMany(p => p.UOM_Materials)
            .HasForeignKey(pr => pr.UOMID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Plant_Material>().HasKey(x => new { x.PlantID, x.MaterialID });
            modelBuilder.Entity<Plant_Material>()
            .HasOne(pr => pr.Plant)
            .WithMany(p => p.Plant_Materials)
            .HasForeignKey(pr => pr.PlantID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Plant_Material>()
            .HasOne(pr => pr.Material)
            .WithMany(p => p.Material_Plants)
            .HasForeignKey(pr => pr.MaterialID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PRMainItem_Condition>().HasKey(x => new { x.ConditionID, x.PRMainItemID });
            modelBuilder.Entity<PRMainItem_Condition>()
            .HasOne(pr => pr.PRMainItem)
            .WithMany(p => p.MainItem_Conditions)
            .HasForeignKey(pr => pr.PRMainItemID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PRMainItem_Condition>()
            .HasOne(pr => pr.Condition)
            .WithMany(p => p.Condition_PRMainItems)
            .HasForeignKey(pr => pr.ConditionID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RFQMainItem_Condition>().HasKey(x => new { x.RFQMainItemID, x.ConditionID });
            modelBuilder.Entity<RFQMainItem_Condition>()
            .HasOne(pr => pr.RFQMainItem)
            .WithMany(p => p.MainItem_Conditions)
            .HasForeignKey(pr => pr.RFQMainItemID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RFQMainItem_Condition>()
            .HasOne(pr => pr.Condition)
            .WithMany(p => p.Condition_RFQMainItems)
            .HasForeignKey(pr => pr.ConditionID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vendor_Bank>().HasKey(x => new { x.VendorID, x.BankID });
            modelBuilder.Entity<Vendor_Bank>()
            .HasOne(pr => pr.Vendor)
            .WithMany(p => p.Vendor_Banks)
            .HasForeignKey(pr => pr.VendorID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Vendor_Bank>()
            .HasOne(pr => pr.Bank)
            .WithMany(p => p.Bank_Vendors)
            .HasForeignKey(pr => pr.BankID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vendor_Tax>().HasKey(x => new { x.VendorID, x.TaxID });
            modelBuilder.Entity<Vendor_Tax>()
            .HasOne(pr => pr.Vendor)
            .WithMany(p => p.Vendor_Taxes)
            .HasForeignKey(pr => pr.VendorID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Vendor_Tax>()
            .HasOne(pr => pr.Tax)
            .WithMany(p => p.Tax_Vendors)
            .HasForeignKey(pr => pr.TaxID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vendor_CompanyCode>().HasKey(x => new { x.VendorID, x.CompanyCodeID });
            modelBuilder.Entity<Vendor_CompanyCode>()
            .HasOne(pr => pr.Vendor)
            .WithMany(p => p.Vendor_CompanyCodes)
            .HasForeignKey(pr => pr.VendorID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Vendor_CompanyCode>()
            .HasOne(pr => pr.CompanyCode)
            .WithMany(p => p.CompanyCode_Vendors)
            .HasForeignKey(pr => pr.CompanyCodeID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee_Position>().HasKey(x => new { x.EmployeeID, x.PositionID });
            modelBuilder.Entity<Employee_Position>()
            .HasOne(pr => pr.Employee)
            .WithMany(p => p.Employee_Positions)
            .HasForeignKey(pr => pr.EmployeeID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee_Position>()
            .HasOne(pr => pr.Position)
            .WithMany(p => p.Position_Employees)
            .HasForeignKey(pr => pr.PositionID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee_SubGroup>().HasKey(x => new { x.EmployeeID, x.SubGroupID });
            modelBuilder.Entity<Employee_SubGroup>()
            .HasOne(pr => pr.Employee)
            .WithMany(p => p.Employee_SubGroups)
            .HasForeignKey(pr => pr.EmployeeID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee_SubGroup>()
            .HasOne(pr => pr.SubGroup)
            .WithMany(p => p.SubGroup_Employees)
            .HasForeignKey(pr => pr.SubGroupID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee_PurchasingGroup>().HasKey(x => new { x.EmployeeID, x.PurchasingGroupID });
            modelBuilder.Entity<Employee_PurchasingGroup>()
            .HasOne(pr => pr.Employee)
            .WithMany(p => p.Employee_PurchasingGroups)
            .HasForeignKey(pr => pr.EmployeeID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee_PurchasingGroup>()
            .HasOne(pr => pr.PurchasingGroup)
            .WithMany(p => p.PurchasingGroup_Employees)
            .HasForeignKey(pr => pr.PurchasingGroupID).OnDelete(DeleteBehavior.NoAction);
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Employee_Action> Employee_Action { get; set; }
        public virtual DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public virtual DbSet<CompanyCode> CompanyCodes { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<ProfitCenter> ProfitCenters { get; set; }
        public virtual DbSet<OrgUnit> OrgUnits { get; set; }
        public virtual DbSet<PurchasingGroup> PurchasingGroups { get; set; }
        public virtual DbSet<Employee_PurchasingGroup> Employee_PurchasingGroup { get; set; }
        public virtual DbSet<GLAccount> GLAccounts { get; set; }
        public virtual DbSet<PRType> PRTypes { get; set; }
        public virtual DbSet<PR> PRS { get; set; }
        public virtual DbSet<PRMainItem> PRMainItems { get; set; }
        public virtual DbSet<PRSubItem> PRSubItem { get; set; }
        public virtual DbSet<Condition> Condition { get; set; }
        public virtual DbSet<PRMainItem_Condition> PRMainItem_Condition { get; set; }
        public virtual DbSet<RFQType> RFQTypes { get; set; }
        public virtual DbSet<RFQ> RFQS { get; set; }
        public virtual DbSet<RFQMainItem> RFQMainItems { get; set; }
        public virtual DbSet<RFQSubItem> RFQSubItems { get; set; }
        public virtual DbSet<RFQMainItem_Condition> RFQMainItem_Condition { get; set; }
        public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<VendorAddress> VendorAddresses { get; set; }
        public virtual DbSet<VendorPhoneNumber> VendorPhoneNumbers { get; set; }
        public virtual DbSet<VendorEmail> VendorEmails { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Vendor_Tax> Vendor_Tax { get; set; }
        public virtual DbSet<Vendor_Bank> Vendor_Bank { get; set; }
        public virtual DbSet<Vendor_CompanyCode> Vendor_CompanyCode { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Employee_Position> Employee_Position { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        public virtual DbSet<EmployeeSubGroup> EmployeeSubGroup { get; set; }
        public virtual DbSet<Employee_SubGroup> Employee_SubGroup { get; set; }
        public virtual DbSet<Employee_PhoneNumber> Employee_PhoneNumbers { get; set; }

    }

}
