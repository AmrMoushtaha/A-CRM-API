using Microsoft.EntityFrameworkCore;
using Stack.Core.Managers.Modules.Auth;
using Stack.Core.Managers.Modules.Employees;
using Stack.Core.Managers.Modules.Materials;
using Stack.Core.Managers.Modules.Organizations;
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

        private EmployeesManager employeesManager;
        public EmployeesManager EmployeesManager
        {
            get
            {
                if (employeesManager == null)
                {
                    employeesManager = new EmployeesManager(context);
                }
                return employeesManager;
            }
        }


        private ActionsManager actionsManager;
        public ActionsManager ActionsManager
        {
            get
            {
                if (actionsManager == null)
                {
                    actionsManager = new ActionsManager(context);
                }
                return actionsManager;
            }
        }

        private EmployeeActionsManager employeeActionsManager;
        public EmployeeActionsManager EmployeeActionsManager
        {
            get
            {
                if (employeeActionsManager == null)
                {
                    employeeActionsManager = new EmployeeActionsManager(context);
                }
                return employeeActionsManager;
            }
        }


        private EmployeeAddressesManager employeeAddressesManager;
        public EmployeeAddressesManager EmployeeAddressesManager
        {
            get
            {
                if (employeeAddressesManager == null)
                {
                    employeeAddressesManager = new EmployeeAddressesManager(context);
                }
                return employeeAddressesManager;
            }
        }


        private EmployeeGroupsManager employeeGroupsManager;
        public EmployeeGroupsManager EmployeeGroupsManager
        {
            get
            {
                if (employeeGroupsManager == null)
                {
                    employeeGroupsManager = new EmployeeGroupsManager(context);
                }
                return employeeGroupsManager;
            }
        }


        private EmployeePositionsManager employeePositionsManager;
        public EmployeePositionsManager EmployeePositionsManager
        {
            get
            {
                if (employeePositionsManager == null)
                {
                    employeePositionsManager = new EmployeePositionsManager(context);
                }
                return employeePositionsManager;
            }
        }


        private EmployeeSubGroupsManager employeeSubGroupsManager;
        public EmployeeSubGroupsManager EmployeeSubGroupsManager
        {
            get
            {
                if (employeeSubGroupsManager == null)
                {
                    employeeSubGroupsManager = new EmployeeSubGroupsManager(context);
                }
                return employeeSubGroupsManager;
            }
        }


        private PositionsManager positionsManager;
        public PositionsManager PositionsManager
        {
            get
            {
                if (positionsManager == null)
                {
                    positionsManager = new PositionsManager(context);
                }
                return positionsManager;
            }
        }


        private CompanyCodesManager companyCodesManager;
        public CompanyCodesManager CompanyCodesManager
        {
            get
            {
                if (companyCodesManager == null)
                {
                    companyCodesManager = new CompanyCodesManager(context);
                }
                return companyCodesManager;
            }
        }


        private CostCentersManager costCentersManager;
        public CostCentersManager CostCentersManager
        {
            get
            {
                if (costCentersManager == null)
                {
                    costCentersManager = new CostCentersManager(context);
                }
                return costCentersManager;
            }
        }


        private EmployeePurchasingGroupsManager employeePurchasingGroupsManager;
        public EmployeePurchasingGroupsManager EmployeePurchasingGroupsManager
        {
            get
            {
                if (employeePurchasingGroupsManager == null)
                {
                    employeePurchasingGroupsManager = new EmployeePurchasingGroupsManager(context);
                }
                return employeePurchasingGroupsManager;
            }
        }


        private ExchangeRatesManager exchangeRatesManager;
        public ExchangeRatesManager ExchangeRatesManager
        {
            get
            {
                if (exchangeRatesManager == null)
                {
                    exchangeRatesManager = new ExchangeRatesManager(context);
                }
                return exchangeRatesManager;
            }
        }


        private GLAccountsManager gLAccountsManager;
        public GLAccountsManager GLAccountsManager
        {
            get
            {
                if (gLAccountsManager == null)
                {
                    gLAccountsManager = new GLAccountsManager(context);
                }
                return gLAccountsManager;
            }
        }


        private OrgUnitsManager orgUnitsManager;
        public OrgUnitsManager OrgUnitsManager
        {
            get
            {
                if (orgUnitsManager == null)
                {
                    orgUnitsManager = new OrgUnitsManager(context);
                }
                return orgUnitsManager;
            }
        }


        private ProfitCentersManager profitCentersManager;
        public ProfitCentersManager ProfitCentersManager
        {
            get
            {
                if (profitCentersManager == null)
                {
                    profitCentersManager = new ProfitCentersManager(context);
                }
                return profitCentersManager;
            }
        }


        private PurchasingGroupsManager purchasingGroupsManager;
        public PurchasingGroupsManager PurchasingGroupsManager
        {
            get
            {
                if (purchasingGroupsManager == null)
                {
                    purchasingGroupsManager = new PurchasingGroupsManager(context);
                }
                return purchasingGroupsManager;
            }
        }

        private EmployeePhoneNumbersManager employeePhoneNumbersManager;
        public EmployeePhoneNumbersManager EmployeePhoneNumbersManager
        {
            get
            {
                if (employeePhoneNumbersManager == null)
                {
                    employeePhoneNumbersManager = new EmployeePhoneNumbersManager(context);
                }
                return employeePhoneNumbersManager;
            }
        }

        private PlantsManager plantsManager;
        public PlantsManager PlantsManager
        {
            get
            {
                if (plantsManager == null)
                {
                    plantsManager = new PlantsManager(context);
                }
                return plantsManager;
            }
        }


        private MaterialGroupsManager materialGroupsManager;
        public MaterialGroupsManager MaterialGroupsManager
        {
            get
            {
                if (materialGroupsManager == null)
                {
                    materialGroupsManager = new MaterialGroupsManager(context);
                }
                return materialGroupsManager;
            }
        }

        private UOMsManager uomsManager;
        public UOMsManager UOMsManager
        {
            get
            {
                if (uomsManager == null)
                {
                    uomsManager = new UOMsManager(context);
                }
                return uomsManager;
            }
        }


        private MaterialTypesManager materialTypesManager;
        public MaterialTypesManager MaterialTypesManager
        {
            get
            {
                if (materialTypesManager == null)
                {
                    materialTypesManager = new MaterialTypesManager(context);
                }
                return materialTypesManager;
            }
        }

        private MaterialsManager materialsManager;
        public MaterialsManager MaterialsManager
        {
            get
            {
                if (materialsManager == null)
                {
                    materialsManager = new MaterialsManager(context);
                }
                return materialsManager;
            }
        }


    }

}
