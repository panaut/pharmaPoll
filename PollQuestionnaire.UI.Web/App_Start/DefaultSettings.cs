using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PollQuestionnaire.UI.Web.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace PollQuestionnaire.UI.Web
{
    public class DefaultSettings
    {
        public static void CreateDefaultDbUser()
        {
            string sqlquery = @"INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'52e327d5-7016-4f41-8dea-4b6700954fa7', N'PQAdmin@patque.net', 0, N'AGalX6yXmRXqWoCG+eaWODibpDZbZbLlZHcIM5HWH0Z4yWGtWojGBSk+dpINzYWBsQ==', N'a7ac7ff6-27a4-47b3-aff6-6a848ac5bc72', NULL, 0, 0, NULL, 1, 0, N'PQAdmin@patque.net')";
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivan.bulut\Source\Repos\pharmaPoll\PollQuestionnaire.UI.Web\App_Data\aspnet-PollQuestionnaire.UI.Web-20170307121304.mdf;Initial Catalog=aspnet-PollQuestionnaire.UI.Web-20170307121304;Integrated Security=True; MultipleActiveResultSets=true");
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM AspNetUsers WHERE UserName = 'PQAdmin@patque.net';", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        SqlCommand command = new SqlCommand(sqlquery, conn);
                        command.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occre while creating table:" + e.Message + "\t" + e.GetType());
            }
        }
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public async void RegisterDefaultAdmin()
        //{
        //    // check if the default user exists
        //    string adminUser = "PQAdmin@patque.net";
        //    var resultCheckAdmin = await UserManager.FindByNameAsync(adminUser);
        //    if (resultCheckAdmin == null)
        //    {
        //        // if not create default user                
        //        string adminUserEmail = "PQAdmin@patque.net";
        //        string adminUserPassword = "Paki2804!";
        //        var user = new ApplicationUser { UserName = adminUser, Email = adminUserEmail };
        //        var resultRegister = await UserManager.CreateAsync(user, adminUserPassword);
        //        if (resultRegister.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //    }

        //}

        //internal static void RegisterADefaultAdmin()
        //{
        //    throw new NotImplementedException();
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}
    }
}