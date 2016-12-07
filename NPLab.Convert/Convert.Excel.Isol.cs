using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPLab.Data;
using NPLab.Models;
//using System.Data.Entity;
using NPLab.Data.Migrations;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using FileStream = System.IO.FileStream;
namespace NPLab.ConvertFile
{
    public partial class ConvertFileExcel
    {
        public void ConvertIsolExcel(string URL, EL_1 currRep, bool diffcond)
        {
            Excel._Worksheet IsolSheet;
            

            //Excel.Range Rng;
            int currRow, num;
            object misValue = System.Reflection.Missing.Value;
            //try
            //{
                //Start Excel and get Application object.
                AppXL.Visible = true;

                //Get a new workbook.
                ConvertXL = (Excel.Workbook)(AppXL.Workbooks.Add(Missing.Value));
                IsolSheet = (Excel._Worksheet)ConvertXL.Worksheets.Add();
                IsolSheet.Name = "Изолация";

                //Заглавия на колоните
                IsolSheet.Cells[1, 1] = "№ \nпо \nред";
                IsolSheet.Cells[1, 2] = "НАИМЕНОВАНИЕ НА \nУРЕДБАТА \n\n/СЪОРЪЖЕНИЕТО/";
                IsolSheet.Cells[1, 3] = "НОМИНАЛНО\n НАПРЕЖЕНИЕ НА \nМЕГАОММЕТЪРА \n\nV";
                
                IsolSheet.get_Range("A1", "A2").Merge();
                IsolSheet.get_Range("B1", "B2").Merge();
                IsolSheet.get_Range("C1", "C2").Merge();
                
                IsolSheet.Cells[1, 4] = "СЪПРОТИВЛЕНИЕ НА ИЗОЛАЦИЯТА";
                IsolSheet.get_Range("D1", "E1").Merge();
                IsolSheet.Cells[2, 4] = "ИЗМЕРЕНО \n\nМΩ";
                IsolSheet.Cells[2, 5] = "МИНИМАЛНО \nДОПУСТИМО \n\nМΩ";
                
                //Изграждане на 3-ти ред
                for (int i = 1; i <= 5; i++) IsolSheet.Cells[3, i] = i;
                currRow = 4; // редът, на който се въвежда в момента
                num = 0;
                Random r = new Random();

                //Записване на всеки сектор
                foreach (Cabel grd in currRep.ListOfCabels)
                {
                    IsolSheet.Cells[currRow, 1] = grd.Name;
                    IsolSheet.get_Range("A" + currRow.ToString(), "E" + currRow.ToString()).Merge();
                    currRow++;
                    string[] TypeStr = new string[6] { "L1", "L2", "L3", "PE", "N", "PEN" };
                    bool[] Cons = new bool[6] { grd.L1, grd.L2, grd.L3, grd.PE, grd.N, grd.PEN };
                    for (int i = 0; i < 6; i++)
                    {
                        if(!Cons[i]) continue;
                        for (int j = i; j < 6; j++)
                        {
                            IsolSheet.Cells[currRow, 1] = (num + 1).ToString() + ". ";
                            if (!Cons[j] || i == j) continue;
                            IsolSheet.Cells[currRow, 2] = TypeStr[i] + "-" + TypeStr[j];
                            IsolSheet.Cells[currRow, 3] = currRep.SourceVoltage;
                            if (!diffcond) IsolSheet.Cells[currRow, 4] = grd.Measured;
                            else IsolSheet.Cells[currRow, 4] = r.Next(currRep.MinMeasured, currRep.MaxMeasured);
                            IsolSheet.Cells[currRow, 5] = currRep.Min;
                            currRow++;
                            num++;
                        }
                    }
                }

                //Формат на таблицата
                IsolSheet.get_Range("A1", "E" + currRow.ToString()).HorizontalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;
                IsolSheet.get_Range("A1", "E" + currRow.ToString()).VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;
                IsolSheet.get_Range("B4", "E" + currRow.ToString()).Font.Size = 11;
                IsolSheet.get_Range("A4", "A" + currRow.ToString()).Font.Size = 8;
                IsolSheet.get_Range("C1", "E2").Font.Size = 10;
                IsolSheet.get_Range("A1", "A2").Font.Size = 9;
                IsolSheet.get_Range("B1", "B2").Font.Size = 11;
                IsolSheet.get_Range("A1", "A" + currRow.ToString()).ColumnWidth = 4;
                IsolSheet.get_Range("B1", "B" + currRow.ToString()).ColumnWidth = 30;
                IsolSheet.get_Range("C1", "C" + currRow.ToString()).ColumnWidth = 27;
                IsolSheet.get_Range("D1", "E" + currRow.ToString()).ColumnWidth = 23;
                IsolSheet.get_Range("A1", "E1").RowHeight = 30;
                IsolSheet.get_Range("B1", "E2").RowHeight = 54;
                IsolSheet.get_Range("C3", "E3").Font.Size = 9;
                IsolSheet.get_Range("A4", "H" + currRow.ToString()).WrapText = true;
                IsolSheet.get_Range("A1", "G" + currRow.ToString()).Font.Name = "Bookman Old Style";
                ConvertXL.SaveAs(URL);
            }
            //catch
            //{
            //    if (AppXL == null)
            //    {
            //        MessageBox.Show("Excel не е инсталиран правилно!");
            //        return;
            //    }
            //    MessageBox.Show("Грешка при конвертирането на файла");
            //}
            
        //}
    }
}