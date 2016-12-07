using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.ObjectModel;
using Microsoft.Office.Interop.Word;
using NPLab.Data.Migrations;
using NPLab.Data;
using NPLab.Models;
using System.Data.Linq;

namespace NPLab
{
    public static class SaveLoad
    {
        static Objects currObj = new Objects();
        public static void UpdateEl_1(EL_1 isol, DateTime date, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            //currObj = (from p in db.Object
            //           where p.Id == objId
            //           select p).FirstOrDefault();
            //EL_1 curr = (from el in db.EL_1
            //            where el.Date == date && el.ObjectsId == objId
            //            select el).FirstOrDefault();
            //EL_1 t = isol;
            //isol = new EL_1();
            //foreach (Cabel cab in t.ListOfCabels)
            //{
            //    Cabel tempC = new Cabel();
            //    tempC.CabelModel = cab.CabelModel;
            //    tempC.CabelType = cab.CabelType;
            //    tempC.ConductorsCount = cab.ConductorsCount;
            //    tempC.L1 = cab.L1;
            //    tempC.L2 = cab.L2;
            //    tempC.L3 = cab.L3;
            //    tempC.Measured = cab.Measured;
            //    tempC.N = cab.N;
            //    tempC.Name = cab.Name;
            //    tempC.PE = cab.PE;
            //    tempC.PEN = cab.PEN;
            //    tempC.Thickness = cab.Thickness;
            //    isol.ListOfCabels.Add(tempC);
            //}
            //isol.MaxMeasured = t.MaxMeasured;
            //isol.Min = t.Min;
            //isol.NameOfEngineer = t.NameOfEngineer;
            //isol.SourceVoltage = t.SourceVoltage;
            //isol.MinMeasured = t.MaxMeasured;
            //isol.Date = t.Date;
            ////currObj.El_1.Remove(curr);
            ////curr.ListOfCabels.Clear();
            //for (int i = 0; i < curr.ListOfCabels.Count; i++) { Cabel cab = curr.ListOfCabels.ElementAt(i);  db.Cabel.Remove(cab); db.Entry(cab).State = System.Data.Entity.EntityState.Deleted; }
            //curr.ListOfCabels.Clear();
            //db.Entry(curr).State = System.Data.Entity.EntityState.Deleted;
            //currObj.El_1.Add(isol);
            //.Entry(isol).State = System.Data.Entity.EntityState.Added;
            //foreach (Cabel cab in isol.ListOfCabels) db.Entry(cab).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            //db.Entry(isol).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public static void SaveEl_1(EL_1 e, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            //e.Object = currObj;
            //.ObjectsId = currObj.Id;
            currObj.El_1.Add(e);
            db.Entry(e).State = System.Data.Entity.EntityState.Added;
            foreach (Cabel cab in e.ListOfCabels) db.Entry(cab).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
            
        public static void UpdateEl_2(El_2 imp, DateTime date, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            var curr = (from el in currObj.El_2
                        where el.ControlDate == date
                        select el).FirstOrDefault();
            //curr = imp;
            //db.Entry(imp).State = System.Data.Entity.EntityState.Modified;
            //foreach (Sectors sec in imp.ListOfCabels) db.Entry(cab).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public static void SaveEl_2(El_2 e, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            //e.Object = currObj;
            //.ObjectsId = currObj.Id;
            currObj.El_2.Add(e);
            db.Entry(e).State = System.Data.Entity.EntityState.Added;
            foreach (Sectors sec in e.ListOfSectors)
            {
                db.Entry(sec).State = System.Data.Entity.EntityState.Added;
                foreach (Floors f in sec.ListOfFloors)
                {
                    db.Entry(f).State = System.Data.Entity.EntityState.Added;
                    foreach (Rooms r in f.ListOfRooms)
                    {
                        db.Entry(r).State = System.Data.Entity.EntityState.Added;
                        foreach (Installations inst in r.ListOfInstallations) db.Entry(inst).State = System.Data.Entity.EntityState.Added;
                    }
                }
            }
            db.SaveChanges();
        }

        public static void UpdateEl_3(EL_3 gr, DateTime date, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            var curr = (from el in currObj.El_3
                        where el.Date == date
                        select el).FirstOrDefault();
            //curr = gr;
            //db.Entry(gr).State = System.Data.Entity.EntityState.Modified;
            //foreach (Cabel cab in gr.ListOfCabels) db.Entry(cab).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public static void SaveEl_3(EL_3 e, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            //e.Object = currObj;
            //.ObjectsId = currObj.Id;
            currObj.El_3.Add(e);
            db.Entry(e).State = System.Data.Entity.EntityState.Added;
            foreach (Grounding gr in e.ListOfGroundings) db.Entry(gr).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

        public static void UpdateEl_3m(EL_3m liPro, DateTime date, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            var curr = (from el in currObj.El_3m
                        where el.Date == date
                        select el).FirstOrDefault();
            //curr = liPro;
            //db.Entry(liPro).State = System.Data.Entity.EntityState.Modified;
            //foreach (Cabel cab in liPro.ListOfCabels) db.Entry(cab).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public static void SaveEl_3m(EL_3m e, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            currObj.El_3m.Add(e);
            db.Entry(e).State = System.Data.Entity.EntityState.Added;
            foreach (LightningGrounding cab in e.ListOfGroundings_El_3m) db.Entry(cab).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
       
        public static void UpdateEl_4(EL_4 isol, DateTime date, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            var curr = (from el in currObj.El_4
                        where el.Date == date
                        select el).FirstOrDefault();
            //curr = isol;
            //db.Entry(isol).State = System.Data.Entity.EntityState.Modified;
            //foreach (Cabel cab in isol.ListOfCabels) db.Entry(cab).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public static void SaveEl_4(EL_4 e, int objId, NPLabDbContext db)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            //currObj.El_4.Add(e);
            //db.Entry(e).State = System.Data.Entity.EntityState.Added;
            //foreach (Sectors_El_4 sec in e.ListOfSectors)
            //{
            //    db.Entry(sec).State = System.Data.Entity.EntityState.Added;
            //    foreach (Floors_El_4 f in sec.ListOfFloors)
            //    {
            //        db.Entry(f).State = System.Data.Entity.EntityState.Added;
            //        foreach (Rooms_El_4 r in f.ListOfRooms)
            //        {
            //            db.Entry(r).State = System.Data.Entity.EntityState.Added;
            //            foreach (Installation_El_4 inst in r.ListOfInstallations) db.Entry(inst).State = System.Data.Entity.EntityState.Added;
            //        }
            //    }
            //}
            //foreach (Cabel cab in e.ListOfCabels) db.Entry(cab).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
    }
}