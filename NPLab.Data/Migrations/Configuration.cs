namespace NPLab.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using NPLab.Models;
    using System;

    public sealed class Configuration : DbMigrationsConfiguration<NPLabDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "NPLab.Data.NPLabDbContext";
        }

        protected override void Seed(NPLabDbContext context)
        {
            context.Engineers.AddOrUpdate(
                x => x.NameOfEngineer,
                new Engineers
                {
                    NameOfEngineer = "Malin Jelev",
                    Time = DateTime.Now
                });

           
        }
    }
}
