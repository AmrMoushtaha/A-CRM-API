using Microsoft.EntityFrameworkCore;
using Stack.Entities.Enums.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Hierarchy;

namespace Stack.DAL
{
    public static class ApplicationContextInitializer
    {

        public static void SeedInitializer(this ModelBuilder modelBuilder)
        {

            #region Initialize levels
            modelBuilder.Entity<Level>().HasData(
                CreateLevel(1,(int)LevelEnum.LevelOne, "Developer", "مطور"),
                CreateLevel(2,(int)LevelEnum.LevelTwo, "project", "مشروع"),
                CreateLevel(3,(int)LevelEnum.LevelThree, "building", "بناء"),
                CreateLevel(4,(int)LevelEnum.LevelFour, "Unit", "وحدة"));
            #endregion

            #region CreateLevel
            static Level CreateLevel(long ID,int Type, string LabelAR, string LabelEN)
            {
                return new Level
                {
                    ID=ID,
                    Type = Type,
                    LabelAR = LabelAR,
                    LabelEN = LabelEN,
                    IsDeleted=false
                };
            }
            #endregion


        }
    }
}