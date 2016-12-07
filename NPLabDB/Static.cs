using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPLab;

namespace NPLab.Models
{
    public class StaticClass
    {
        public string[] TypesCabels = { "СВТ", "ПВВ-МБ1" };
        public string[] TypesProtectors = { "Merlin Gerin", "C25-C25" };
        public string[] Engineers = { "Малин Желев", "Димитър Дечев" };
        public List<InstallationItem> Items = new List<InstallationItem>();
        public double[] ProtsArr = new double[10] { 4, 6, 10, 16, 20, 25, 32, 40, 63, 80 };
        public double[] CabelSizesArr = new double[10] { 0.25, 0.5, 1, 2.5, 4, 5, 10, 16, 20, 25 };
        public List<double> CabelSizes = new List<double>();
        public double lambda = 230;
        public List<double> Prots = new List<double>();
        public List<Protectors> Protectors = new List<Protectors>();
        string conts = "Контакт монофазен,Контакт трифазен,Контакт двоен,Контакт троен,Контакт четворен,Контакт монофазен на ел. табло,Контакт трифазен на ел. табло";
        string lights = "Плафониер,Аплик,Осв. тяло тип 'луна',Осв. тяло на полилей";
        string pow = "Печка,Микровълнова фурна,Бойлер,Удължител,Компютър,Принтер";
        public StaticClass()
        {
            foreach (string el in conts.Split(','))
            {
                InstallationItem newIt = new InstallationItem
                {
                    Id = Items.Count + 1,
                    Name = el,
                    Type = (InstallationType)0
                };
                Items.Add(newIt);
            }
            foreach (string el in lights.Split(','))
            {
                InstallationItem newIt = new InstallationItem
                {
                    Id = Items.Count + 1,
                    Name = el,
                    Type = (InstallationType)1
                };
                Items.Add(newIt);
            }
            foreach (string el in pow.Split(','))
            {
                InstallationItem newIt = new InstallationItem
                {
                    Id = Items.Count + 1,
                    Name = el,
                    Type = (InstallationType)2
                };
                Items.Add(newIt);
            }
            Prots.AddRange(ProtsArr);
            CabelSizes.AddRange(CabelSizesArr);
        }
    }
}
