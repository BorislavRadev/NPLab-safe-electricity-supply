
namespace TestNPLabDB
{
    using NPLab.Data;
    using NPLab.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using NPLab.Data.Migrations;

    public class StartUp
    {
        public static void Main()
        {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<NPLabDbContext,Configuration>()); 
            /*
            var db = new NPLabDbContext();

            var engineer = new Engineers
            {
                NameOfEngineer = "Malin Zhelev"
                Time = DateTime.Now
            };

            db.Engineers.Add(engineer);
            db.SaveChanges();

            Console.WriteLine(db.Engineers.Count());
             * */
        }
    }
}
