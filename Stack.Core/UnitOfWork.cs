using Microsoft.EntityFrameworkCore;
using Stack.Core.Managers.Modules.Activities;
using Stack.Core.Managers.Modules.area;
using Stack.Core.Managers.Modules.Auth;
using Stack.Core.Managers.Modules.Common;
using Stack.Core.Managers.Modules.CR;
using Stack.Core.Managers.Modules.Interest;
using Stack.Core.Managers.Modules.Materials;
using Stack.Core.Managers.Modules.Pools;
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

        private ContactStatusManager contactStatusManager;
        public ContactStatusManager ContactStatusManager
        {
            get
            {
                if (contactStatusManager == null)
                {
                    contactStatusManager = new ContactStatusManager(context);
                }
                return contactStatusManager;
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

        private ContactCommentManager contactCommentManager;
        public ContactCommentManager ContactCommentManager
        {
            get
            {
                if (contactCommentManager == null)
                {
                    contactCommentManager = new ContactCommentManager(context);
                }
                return contactCommentManager;
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


        private SelectedOptionsManager selectedOptionsManager;
        public SelectedOptionsManager SelectedOptionsManager
        {
            get
            {
                if (selectedOptionsManager == null)
                {
                    selectedOptionsManager = new SelectedOptionsManager(context);
                }
                return selectedOptionsManager;
            }
        }

        private SubmissionDetailsManager submissionDetailsManager;
        public SubmissionDetailsManager SubmissionDetailsManager
        {
            get
            {
                if (submissionDetailsManager == null)
                {
                    submissionDetailsManager = new SubmissionDetailsManager(context);
                }
                return submissionDetailsManager;
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

        private LInterestManager lInterestManager;
        public LInterestManager LInterestManager
        {
            get
            {
                if (lInterestManager == null)
                {
                    lInterestManager = new LInterestManager(context);
                }
                return lInterestManager;
            }
        }


        private LInterest_LInterestInputManager lInterest_LInterestInputManager;
        public LInterest_LInterestInputManager LInterest_LInterestInputManager
        {
            get
            {
                if (lInterest_LInterestInputManager == null)
                {
                    lInterest_LInterestInputManager = new LInterest_LInterestInputManager(context);
                }
                return lInterest_LInterestInputManager;
            }
        }

        private LInterest_InterestAttributeManager lInterest_InterestAttributeManager;
        public LInterest_InterestAttributeManager LInterest_InterestAttributeManager
        {
            get
            {
                if (lInterest_InterestAttributeManager == null)
                {
                    lInterest_InterestAttributeManager = new LInterest_InterestAttributeManager(context);
                }
                return lInterest_InterestAttributeManager;
            }
        }

        private InterestAttributesManager interestAttributesManager;
        public InterestAttributesManager InterestAttributesManager
        {
            get
            {
                if (interestAttributesManager == null)
                {
                    interestAttributesManager = new InterestAttributesManager(context);
                }
                return interestAttributesManager;
            }
        }

        private LInterestInputManager lInterestInputManager;
        public LInterestInputManager LInterestInputManager
        {
            get
            {
                if (lInterestInputManager == null)
                {
                    lInterestInputManager = new LInterestInputManager(context);
                }
                return lInterestInputManager;
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

        private Location_PoolManager location_PoolManager;
        public Location_PoolManager Location_PoolManager
        {
            get
            {
                if (location_PoolManager == null)
                {
                    location_PoolManager = new Location_PoolManager(context);
                }
                return location_PoolManager;
            }
        }
        private LocationManager locationManager;
        public LocationManager LocationManager
        {
            get
            {
                if (locationManager == null)
                {
                    locationManager = new LocationManager(context);
                }
                return locationManager;
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

        private TagManager tagManager;
        public TagManager TagManager
        {
            get
            {
                if (tagManager == null)
                {
                    tagManager = new TagManager(context);
                }
                return tagManager;
            }
        }

        private ContactTagManager contactTagManager;
        public ContactTagManager ContactTagManager
        {
            get
            {
                if (contactTagManager == null)
                {
                    contactTagManager = new ContactTagManager(context);
                }
                return contactTagManager;
            }
        }

        private CustomerTagManager customerTagManager;
        public CustomerTagManager CustomerTagManager
        {
            get
            {
                if (customerTagManager == null)
                {
                    customerTagManager = new CustomerTagManager(context);
                }
                return customerTagManager;
            }
        }

        private CRTypesManager crTypesManager;
        public CRTypesManager CRTypesManager
        {
            get
            {
                if (crTypesManager == null)
                {
                    crTypesManager = new CRTypesManager(context);
                }
                return crTypesManager;
            }
        }

        private CustomerRequestsManager customerRequestsManager;
        public CustomerRequestsManager CustomerRequestsManager
        {
            get
            {
                if (customerRequestsManager == null)
                {
                    customerRequestsManager = new CustomerRequestsManager(context);
                }
                return customerRequestsManager;
            }
        }

        private CRSectionsManager crSectionsManager;
        public CRSectionsManager CRSectionsManager
        {
            get
            {
                if (crSectionsManager == null)
                {
                    crSectionsManager = new CRSectionsManager(context);
                }
                return crSectionsManager;
            }
        }

        private CR_SectionsManager cr_SectionsManager;
        public CR_SectionsManager CR_SectionsManager
        {
            get
            {
                if (cr_SectionsManager == null)
                {
                    cr_SectionsManager = new CR_SectionsManager(context);
                }
                return cr_SectionsManager;
            }
        }

        private CRSectionsQuestionsManager crSectionsQuestionsManager;
        public CRSectionsQuestionsManager CRSectionsQuestionsManager
        {
            get
            {
                if (crSectionsQuestionsManager == null)
                {
                    crSectionsQuestionsManager = new CRSectionsQuestionsManager(context);
                }
                return crSectionsQuestionsManager;
            }
        }

        private CRSectionQuestionOptionsManager crSectionQuestionOptionsManager;
        public CRSectionQuestionOptionsManager CRSectionQuestionOptionsManager
        {
            get
            {
                if (crSectionQuestionOptionsManager == null)
                {
                    crSectionQuestionOptionsManager = new CRSectionQuestionOptionsManager(context);
                }
                return crSectionQuestionOptionsManager;
            }
        }


        private CRSectionQuestionAnswersManager crSectionQuestionAnswersManager;
        public CRSectionQuestionAnswersManager CRSectionQuestionAnswersManager
        {
            get
            {
                if (crSectionQuestionAnswersManager == null)
                {
                    crSectionQuestionAnswersManager = new CRSectionQuestionAnswersManager(context);
                }
                return crSectionQuestionAnswersManager;
            }
        }

        private PoolConnectionIDsManager poolConnectionIDsManager;
        public PoolConnectionIDsManager PoolConnectionIDsManager
        {
            get
            {
                if (poolConnectionIDsManager == null)
                {
                    poolConnectionIDsManager = new PoolConnectionIDsManager(context);
                }
                return poolConnectionIDsManager;
            }
        }

        private SystemConfigurationManager systemConfigurationManager;
        public SystemConfigurationManager SystemConfigurationManager
        {
            get
            {
                if (systemConfigurationManager == null)
                {
                    systemConfigurationManager = new SystemConfigurationManager(context);
                }
                return systemConfigurationManager;
            }
        }

        private AuthorizationSectionsManager authorizationSectionsManager;
        public AuthorizationSectionsManager AuthorizationSectionsManager
        {
            get
            {
                if (authorizationSectionsManager == null)
                {
                    authorizationSectionsManager = new AuthorizationSectionsManager(context);
                }
                return authorizationSectionsManager;
            }
        }

        private SectionAuthorizationsManager sectionAuthorizationsManager;
        public SectionAuthorizationsManager SectionAuthorizationsManager
        {
            get
            {
                if (sectionAuthorizationsManager == null)
                {
                    sectionAuthorizationsManager = new SectionAuthorizationsManager(context);
                }
                return sectionAuthorizationsManager;
            }
        }



    }

}
