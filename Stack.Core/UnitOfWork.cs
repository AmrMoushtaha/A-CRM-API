using Microsoft.EntityFrameworkCore;
using Stack.Core.Managers.Modules.Activities;
using Stack.Core.Managers.Modules.area;
using Stack.Core.Managers.Modules.Auth;
using Stack.Core.Managers.Modules.chat;
using Stack.Core.Managers.Modules.Common;
using Stack.Core.Managers.Modules.CR;
using Stack.Core.Managers.Modules.CustomerStage;
using Stack.Core.Managers.Modules.Hierarchy;
using Stack.Core.Managers.Modules.Interest;
using Stack.Core.Managers.Modules.Materials;
using Stack.Core.Managers.Modules.Pools;
using Stack.Core.Managers.Modules.Teams;
using Stack.DAL;
using Stack.Entities.Models.Modules.Chat;
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


        private DoneDealManager doneDealManager;
        public DoneDealManager DoneDealManager
        {
            get
            {
                if (doneDealManager == null)
                {
                    doneDealManager = new DoneDealManager(context);
                }
                return doneDealManager;
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


        private LevelManager levelManager;
        public LevelManager LevelManager
        {
            get
            {
                if (levelManager == null)
                {
                    levelManager = new LevelManager(context);
                }
                return levelManager;
            }
        }

        private SectionManager sectionManager;
        public SectionManager SectionManager
        {
            get
            {
                if (sectionManager == null)
                {
                    sectionManager = new SectionManager(context);
                }
                return sectionManager;
            }
        }

        private AttributesManager attributesManager;
        public AttributesManager AttributesManager
        {
            get
            {
                if (attributesManager == null)
                {
                    attributesManager = new AttributesManager(context);
                }
                return attributesManager;
            }
        }

        private LInputManager lInputManager;
        public LInputManager LInputManager
        {
            get
            {
                if (lInputManager == null)
                {
                    lInputManager = new LInputManager(context);
                }
                return lInputManager;
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

        private PoolRequestManager poolRequestManager;
        public PoolRequestManager PoolRequestManager
        {
            get
            {
                if (poolRequestManager == null)
                {
                    poolRequestManager = new PoolRequestManager(context);
                }
                return poolRequestManager;
            }
        }


        private CustomerCommentManager customerCommentManager;
        public CustomerCommentManager CustomerCommentManager
        {
            get
            {
                if (customerCommentManager == null)
                {
                    customerCommentManager = new CustomerCommentManager(context);
                }
                return customerCommentManager;
            }
        }

        private ChannelManager channelManager;
        public ChannelManager ChannelManager
        {
            get
            {
                if (channelManager == null)
                {
                    channelManager = new ChannelManager(context);
                }
                return channelManager;
            }
        }

        private LeadSourceNameManager leadSourceNameManager;
        public LeadSourceNameManager LeadSourceNameManager
        {
            get
            {
                if (leadSourceNameManager == null)
                {
                    leadSourceNameManager = new LeadSourceNameManager(context);
                }
                return leadSourceNameManager;
            }
        }

        private LeadSourceTypeManager leadSourceTypeManager;
        public LeadSourceTypeManager LeadSourceTypeManager
        {
            get
            {
                if (leadSourceTypeManager == null)
                {
                    leadSourceTypeManager = new LeadSourceTypeManager(context);
                }
                return leadSourceTypeManager;
            }
        }

        private ContactFavoriteManager contactFavoriteManager;
        public ContactFavoriteManager ContactFavoriteManager
        {
            get
            {
                if (contactFavoriteManager == null)
                {
                    contactFavoriteManager = new ContactFavoriteManager(context);
                }
                return contactFavoriteManager;
            }
        }

        private ProspectFavoriteManager prospectFavoriteManager;
        public ProspectFavoriteManager ProspectFavoriteManager
        {
            get
            {
                if (prospectFavoriteManager == null)
                {
                    prospectFavoriteManager = new ProspectFavoriteManager(context);
                }
                return prospectFavoriteManager;
            }
        }

        private LeadFavoriteManager leadFavoriteManager;
        public LeadFavoriteManager LeadFavoriteManager
        {
            get
            {
                if (leadFavoriteManager == null)
                {
                    leadFavoriteManager = new LeadFavoriteManager(context);
                }
                return leadFavoriteManager;
            }
        }

        private OpportunityFavoriteManager opportunityFavoriteManager;
        public OpportunityFavoriteManager OpportunityFavoriteManager
        {
            get
            {
                if (opportunityFavoriteManager == null)
                {
                    opportunityFavoriteManager = new OpportunityFavoriteManager(context);
                }
                return opportunityFavoriteManager;
            }
        }

        private DoneDealFavoriteManager doneDealFavoriteManager;
        public DoneDealFavoriteManager DoneDealFavoriteManager
        {
            get
            {
                if (doneDealFavoriteManager == null)
                {
                    doneDealFavoriteManager = new DoneDealFavoriteManager(context);
                }
                return doneDealFavoriteManager;
            }
        }


        private CustomerRequestTypeManager customerRequestTypeManager;
        public CustomerRequestTypeManager CustomerRequestTypeManager
        {
            get
            {
                if (customerRequestTypeManager == null)
                {
                    customerRequestTypeManager = new CustomerRequestTypeManager(context);
                }
                return customerRequestTypeManager;
            }
        }

        private CustomerRequestManager customerRequestManager;
        public CustomerRequestManager CustomerRequestManager
        {
            get
            {
                if (customerRequestManager == null)
                {
                    customerRequestManager = new CustomerRequestManager(context);
                }
                return customerRequestManager;
            }
        }

        private CRPhaseManager crPhaseManager;
        public CRPhaseManager CRPhaseManager
        {
            get
            {
                if (crPhaseManager == null)
                {
                    crPhaseManager = new CRPhaseManager(context);
                }
                return crPhaseManager;
            }
        }

        private CRPhaseInputManager crPhaseInputManager;
        public CRPhaseInputManager CRPhaseInputManager
        {
            get
            {
                if (crPhaseInputManager == null)
                {
                    crPhaseInputManager = new CRPhaseInputManager(context);
                }
                return crPhaseInputManager;
            }
        }

        private CRPhaseInputOptionManager crPhaseInputOptionManager;
        public CRPhaseInputOptionManager CRPhaseInputOptionManager
        {
            get
            {
                if (crPhaseInputOptionManager == null)
                {
                    crPhaseInputOptionManager = new CRPhaseInputOptionManager(context);
                }
                return crPhaseInputOptionManager;
            }
        }

        private CRPhaseTimelineManager crPhaseTimelineManager;
        public CRPhaseTimelineManager CRPhaseTimelineManager
        {
            get
            {
                if (crPhaseTimelineManager == null)
                {
                    crPhaseTimelineManager = new CRPhaseTimelineManager(context);
                }
                return crPhaseTimelineManager;
            }
        }


        private CRTimeline_PhaseManager crTimeline_PhaseManager;
        public CRTimeline_PhaseManager CRTimeline_PhaseManager
        {
            get
            {
                if (crTimeline_PhaseManager == null)
                {
                    crTimeline_PhaseManager = new CRTimeline_PhaseManager(context);
                }
                return crTimeline_PhaseManager;
            }
        }


        private CR_TimelineManager cr_TimelineManager;
        public CR_TimelineManager CR_TimelineManager
        {
            get
            {
                if (cr_TimelineManager == null)
                {
                    cr_TimelineManager = new CR_TimelineManager(context);
                }
                return cr_TimelineManager;
            }
        }

        private CR_Timeline_PhaseManager cr_Timeline_PhaseManager;
        public CR_Timeline_PhaseManager CR_Timeline_PhaseManager
        {
            get
            {
                if (cr_Timeline_PhaseManager == null)
                {
                    cr_Timeline_PhaseManager = new CR_Timeline_PhaseManager(context);
                }
                return cr_Timeline_PhaseManager;
            }
        }

        private CRPhaseInputAnswerManager crPhaseInputAnswerManager;
        public CRPhaseInputAnswerManager CRPhaseInputAnswerManager
        {
            get
            {
                if (crPhaseInputAnswerManager == null)
                {
                    crPhaseInputAnswerManager = new CRPhaseInputAnswerManager(context);
                }
                return crPhaseInputAnswerManager;
            }
        }

        private ConversationManager conversationManager;
        public ConversationManager ConversationManager
        {
            get
            {
                if (conversationManager == null)
                {
                    conversationManager = new ConversationManager(context);
                }
                return conversationManager;
            }
        }

        private MessageManager messageManager;
        public MessageManager MessageManager
        {
            get
            {
                if (messageManager == null)
                {
                    messageManager = new MessageManager(context);
                }
                return messageManager;
            }
        }

        private UsersConversationsManager usersConversationsManager;
        public UsersConversationsManager UsersConversationsManager
        {
            get
            {
                if (usersConversationsManager == null)
                {
                    usersConversationsManager = new UsersConversationsManager(context);
                }
                return usersConversationsManager;
            }
        }

        private TeamManager teamManager;
        public TeamManager TeamManager
        {
            get
            {
                if (teamManager == null)
                {
                    teamManager = new TeamManager(context);
                }
                return teamManager;
            }
        }

        private TeamUserManager teamUserManager;
        public TeamUserManager TeamUserManager
        {
            get
            {
                if (teamUserManager == null)
                {
                    teamUserManager = new TeamUserManager(context);
                }
                return teamUserManager;
            }
        }
    }

}
