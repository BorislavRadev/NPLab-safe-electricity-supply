using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.ObjectModel;
using NPLab.Data.Migrations;
using NPLab.Data;
using NPLab.Models;

namespace NPLab
{
    public partial class ChooseObj : Form
    {
        public Home H;
        public ChooseObj(Home h)
        {
            this.H = h;
            InitializeComponent();
        }

        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            var db = new NPLabDbContext();
            string[] objNames = (from p in db.Object
                                 where p.ObjectName.IndexOf(SearchField.Text.ToLower()) != -1 && p.ObjectName != null
                                 select p.ObjectName).ToArray<string>();
            ListObjects.Items.Clear();
            ListObjects.Items.AddRange(objNames);
        }

        private void ChooseObj_Load(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            var db = new NPLabDbContext();
            string[] objNames = (from p in db.Object
                                 where p.ObjectName.Length > 0 && p.ObjectName != null
                                 select p.ObjectName).ToArray<string>();
            ListObjects.Items.AddRange(objNames);
        }

        private void Load_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            var db = new NPLabDbContext();
            if (ListObjects.SelectedIndex == -1) { MessageBox.Show("Не сте избрали обект"); return; }
            int toLoad;
            try
            {
                toLoad = (from p in db.Object
                          where p.ObjectName == ListObjects.SelectedItem.ToString()
                          select p.Id).First();
            }
            catch { MessageBox.Show("Грешка! Не съществува такъв обект"); return; }
            Main M = new Main(toLoad, H);
            M.Show();
            M.Focus();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
           H.Show();
           H.Focus();
           this.Close();
            }
    }
}
