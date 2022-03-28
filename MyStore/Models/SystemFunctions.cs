using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class SystemFunctions : Controller
    {
        public const string ManageAccount = "Manage_Account";
        public const string ManageProduct = "Manage_Product";
        public const string ManageCustomer = "Manage_Customer";
        public const string ManageOrderDetails = "Manage_OrderDetails";
        public const string ManageRoleMaster = "Manage_Role_Master";
        public const string ManageRolePermision = "Manage_Role_Permision";
    }
}
