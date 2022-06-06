using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AppSeedingInitilazer
    {
        public static void seed(IApplicationBuilder applicationBulider)
        {
            var scopeservice = applicationBulider.ApplicationServices.CreateScope();
            var context = scopeservice.ServiceProvider.GetService<SqlContext>();

            if (!context.Priorities.Any())
            {
                context.Priorities.AddRange(new List<Priority>() {
                    new Priority()
                    {
                        Name="Low",
                       
                    },new Priority()
                    {
                        Name="High",
                        
                    },new Priority()
                    {
                        Name="Critical",
                        
                    }

                   });

                context.SaveChanges();
            }

        }
    }
}
