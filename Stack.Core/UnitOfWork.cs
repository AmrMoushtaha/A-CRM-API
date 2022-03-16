using Microsoft.EntityFrameworkCore;
using Stack.Core.Managers.Modules.Activities;
using Stack.Core.Managers.Modules.Auth;
using Stack.Core.Managers.Modules.Materials;
using Stack.Core.Managers.Modules.pool;
using Stack.DAL;
using System;
using System.Threading.Tasks;

namespace Stack.Core
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            this.context = context;
            UserManager = userManager;
            RoleManager = roleManager;
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // log message and enteries
            }
            catch (DbUpdateException ex)
            {
                // log message and enteries
            }
            catch (Exception ex)
            {
                // Log here.
            }
            return false;
        }

        public ApplicationUserManager UserManager { get; private set; } //Manager for application users table . 
        public ApplicationRoleManager RoleManager { get; private set; } //Manager for application users table . 



        private ContactManager contactManager;
        public ContactManager ContactManager
        {
            get
            {
                if (contactManager == null)
                {
                    contactManager = new ContactManager(context);
                }
                return contactManager;
            }
        }
        

        private ContactPhoneNumberManager contactPhoneNumberManager;
        public ContactPhoneNumberManager ContactPhoneNumberManager
        {
            get
            {
                if (contactPhoneNumberManager == null)
                {
                    contactPhoneNumberManager = new ContactPhoneNumberManager(context);
                }
                return contactPhoneNumberManager;
            }
        }

        private CustomerManager customerManager;
        public CustomerManager CustomerManager
        {
            get
            {
                if (customerManager == null)
                {
                    customerManager = new CustomerManager(context);
                }
                return customerManager;
            }
        }

        private DealManager dealManager;
        public DealManager DealManager
        {
            get
            {
                if (dealManager == null)
                {
                    dealManager = new DealManager(context);
                }
                return dealManager;
            }

        }

        private LeadManager leadManager;
        public LeadManager LeadManager
        {
            get
            {
                if (leadManager == null)
                {
                    leadManager = new LeadManager(context);
                }
                return leadManager;
            }
        }

        private LeadStatusManager leadStatusManager;
        public LeadStatusManager LeadStatusManager
        {
            get
            {
                if (leadStatusManager == null)
                {
                    leadStatusManager = new LeadStatusManager(context);
                }
                return leadStatusManager;
            }
        }


        private ProspectManager prospectManager;
        public ProspectManager ProspectManager
        {
            get
            {
                if (prospectManager == null)
                {
                    prospectManager = new ProspectManager(context);
                }
                return prospectManager;
            }
        }

        private ProspectStatusManager prospectStatusManager;
        public ProspectStatusManager ProspectStatusManager
        {
            get
            {
                if (prospectStatusManager == null)
                {
                    prospectStatusManager = new ProspectStatusManager(context);
                }
                return prospectStatusManager;
            }
        }


        private OpportunityManager opportunityManager;
        public OpportunityManager OpportunityManager
        {
            get
            {
                if (opportunityManager == null)
                {
                    opportunityManager = new OpportunityManager(context);
                }
                return opportunityManager;
            }
        }

        private OpportunityStatusManager opportunityStatusManager;
        public OpportunityStatusManager OpportunityStatusManager
        {
            get
            {
                if (opportunityStatusManager == null)
                {
                    opportunityStatusManager = new OpportunityStatusManager(context);
                }
                return opportunityStatusManager;
            }
        }

        private ProcessFlowsManager processFlowManager;
        public ProcessFlowsManager ProcessFlowsManager
        {
            get
            {
                if (processFlowManager == null)
                {
                    processFlowManager = new ProcessFlowsManager(context);
                }
                return processFlowManager;
            }
        }

        private ActivityTypesManager activityTypesManager;
        public ActivityTypesManager ActivityTypesManager
        {
            get
            {
                if (activityTypesManager == null)
                {
                    activityTypesManager = new ActivityTypesManager(context);
                }
                return activityTypesManager;
            }
        }

        private ActivitiesManager activitiesManager;
        public ActivitiesManager ActivitiesManager
        {
            get
            {
                if (activitiesManager == null)
                {
                    activitiesManager = new ActivitiesManager(context);
                }
                return activitiesManager;
            }
        }

        private SectionsManager sectionsManager;
        public SectionsManager SectionsManager
        {
            get
            {
                if (sectionsManager == null)
                {
                    sectionsManager = new SectionsManager(context);
                }
                return sectionsManager;
            }
        }

        private ActivitySectionsManager activitySectionsManager;
        public ActivitySectionsManager ActivitySectionsManager
        {
            get
            {
                if (activitySectionsManager == null)
                {
                    activitySectionsManager = new ActivitySectionsManager(context);
                }
                return activitySectionsManager;
            }
        }

        private SectionsQuestionsManager sectionQuestionsManager;
        public SectionsQuestionsManager SectionQuestionsManager
        {
            get
            {
                if (sectionQuestionsManager == null)
                {
                    sectionQuestionsManager = new SectionsQuestionsManager(context);
                }
                return sectionQuestionsManager;
            }
        }

        private SectionQuestionOptionsManager sectionQuestionsOptionsManager;
        public SectionQuestionOptionsManager SectionQuestionOptionsManager
        {
            get
            {
                if (sectionQuestionsOptionsManager == null)
                {
                    sectionQuestionsOptionsManager = new SectionQuestionOptionsManager(context);
                }
                return sectionQuestionsOptionsManager;
            }
        }


        private SectionQuestionAnswersManager sectionQuestionAnswersManager;
        public SectionQuestionAnswersManager SectionQuestionAnswersManager
        {
            get
            {
                if (sectionQuestionAnswersManager == null)
                {
                    sectionQuestionAnswersManager = new SectionQuestionAnswersManager(context);
                }
                return sectionQuestionAnswersManager;
            }
        }

        private PoolManager poolManager;
        public PoolManager PoolManager
        {
            get
            {
                if (poolManager == null)
                {
                    poolManager = new PoolManager(context);
                }
                return poolManager;
            }
        }

        private PoolUserManager poolUserManager;
        public PoolUserManager PoolUserManager
        {
            get
            {
                if (poolUserManager == null)
                {
                    poolUserManager = new PoolUserManager(context);
                }
                return poolUserManager;
            }
        }
        private PoolAdminManager poolAdminManager;
        public PoolAdminManager PoolAdminManager
        {
            get
            {
                if (poolAdminManager == null)
                {
                    poolAdminManager = new PoolAdminManager(context);
                }
                return poolAdminManager;
            }
        }
    }

}
