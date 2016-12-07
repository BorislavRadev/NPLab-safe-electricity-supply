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
using System.Reflection;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Word;
using NPLab.Data.Migrations;
using NPLab.Data;
using NPLab.Models;

namespace NPLab
{
    public partial class Home : Form
    {
        NPLabDbContext db = new NPLabDbContext();
            
        public Home()
        {
            InitializeComponent();
        }

        private void NewObject_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            Objects obj = new Objects();
            db.Object.Add(obj);
            db.SaveChanges();
            Main M = new Main(obj.Id, this);
            M.Show();
            M.Focus();
            this.Hide();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void OpenEx_Click(object sender, EventArgs e)
        {
            ChooseObj f = new ChooseObj(this);
            f.Show();
            f.Focus();
            this.Hide();
        }

        private void Help_Click(object sender, EventArgs e)
        {
            String strAppDir = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().GetName().CodeBase);
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Process proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = strAppDir + "/help.docx" ;
            proc.Start();
            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
