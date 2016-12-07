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
using NPLab.Data.Migrations;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using FileStream  = System.IO.FileStream;
namespace NPLab.ConvertFile
{
    public partial class ConvertFileExcel
    {
        public void ConvertLiProExcel(string URL, EL_3m currRep)
        {
  	        Excel._Worksheet LiProSheet;
            
  	        //Excel.Range Rng;
            int currRow;
            object misValue = System.Reflection.Missing.Value;
  	        try
  	        {
  		        //Start Excel and get Application object.
  		        AppXL.Visible = true;
  
  		        //Get a new workbook.
  		        ConvertXL = (Excel.Workbook)(AppXL.Workbooks.Add( Missing.Value ));
  		        LiProSheet = (Excel._Worksheet)ConvertXL.Worksheets.Add();
                LiProSheet.Name = "Мълниезащита";
  		        
                //Заглавия на колоните
  		        LiProSheet.Cells[1, 1] = "№ \nпо \nред";
  		        LiProSheet.Cells[1, 2] = "ЗАЗЕМИТЕЛИ";
  		        LiProSheet.get_Range("A1", "A2").Merge();
			    LiProSheet.get_Range("B1", "B2").Merge();
  		        LiProSheet.Cells[1, 3] = "ИЗМЕРВАТЕЛНИ\n ЕЛЕКТРОДИ";
  		        LiProSheet.Cells[1, 5] = "КОЕФ \n\n\n\n\n φ";
			    LiProSheet.get_Range("C1", "D1").Merge();
			    LiProSheet.get_Range("E1", "E2").Merge();
			    LiProSheet.Cells[1, 6] = "СЪПРОТИВЛЕНИЕ НА \nЗАЗЕМИТЕЛИТЕ";
			    LiProSheet.get_Range("F1", "H1").Merge();
                LiProSheet.Cells[2, 3] = "ПОМОЩЕН \nЗАЗЕ- \nMИТЕЛ \n\nRb [Ω]";
  		        LiProSheet.Cells[2, 4] = "СОНДА \n\n\n\nRb [Ω]";
  		        LiProSheet.Cells[2, 6] = "ИЗМЕРЕ-\n НО \n\n[Ω]";
                LiProSheet.Cells[2, 7] = "КОРИГИ-\nРАНО \n\n[Ω]";
                LiProSheet.Cells[2, 8] = "ИМ-\nПУЛСНО \n\n[Ω]";
                LiProSheet.Cells[2, 9] = "НОРМА \n\n\n[Ω]";
                LiProSheet.get_Range("A1", "H1").RowHeight = 30;
                LiProSheet.get_Range("A2", "H2").RowHeight = 63;
  		
  		        //Изграждане на 3-ти ред
  		        for(int i = 1; i <= 8; i++) LiProSheet.Cells[3, i] = i;
  		        currRow = 4; // редът, на който се въвежда в момента
  		
  		        //Записване на всеки сектор
  		        foreach(LightningGrounding grd in currRep.ListOfGroundings_El_3m) 
  		        {
                    double Fi;
                    if(currRep.WetSeason) Fi = 1.3;
                    else Fi = 1.15;
                    LiProSheet.Cells[currRow, 1] = (currRow - 3).ToString() + ". ";
                    LiProSheet.Cells[currRow, 2] = grd.Name;
  		            LiProSheet.Cells[currRow, 3] = grd.AuxiliaryGrounding;
                    LiProSheet.Cells[currRow, 4] = grd.Probe;
                    LiProSheet.Cells[currRow, 5] = Fi;
                    LiProSheet.Cells[currRow, 6] = grd.Measured;
                    LiProSheet.Cells[currRow, 7] = grd.Adjusted;
                    LiProSheet.Cells[currRow, 8] = grd.Impulsive;
                    LiProSheet.Cells[currRow, 9] = currRep.Max;
                    currRow++;
  		        }
  		
  		        //Формат на таблицата
  		        LiProSheet.get_Range("A1", "H" + currRow.ToString()).HorizontalAlignment = 
                    Excel.XlVAlign.xlVAlignCenter;
                LiProSheet.get_Range("A4", "H" + currRow.ToString()).VerticalAlignment = 
                    Excel.XlVAlign.xlVAlignTop;
  		        LiProSheet.get_Range("B4", "H" + currRow.ToString()).Font.Size = 11;
  		        LiProSheet.get_Range("A4", "A" + currRow.ToString()).Font.Size = 9;
  		        LiProSheet.get_Range("A1", "H1").Font.Size = 10;
  		        LiProSheet.get_Range("A2", "H2").Font.Size = 9;
                LiProSheet.get_Range("A1", "H2").VerticalAlignment = 
  			        Excel.XlVAlign.xlVAlignCenter;
                LiProSheet.get_Range("A1", "G" + currRow.ToString()).Font.Name = "Bookman Old Style";
                LiProSheet.get_Range("A1", "A" + currRow.ToString()).ColumnWidth = 4;
                LiProSheet.get_Range("B1", "B" + currRow.ToString()).ColumnWidth = 23;
                LiProSheet.get_Range("C1", "C" + currRow.ToString()).ColumnWidth = 11;
                LiProSheet.get_Range("A4", "H" + currRow.ToString()).WrapText = true;
            }

            catch
            {
                if (AppXL == null)
                {
                    MessageBox.Show("Excel не е инсталиран на компютъра!!");
                    return;
                }
                MessageBox.Show("Грешка при конвертирането на файла");
            }
            ConvertXL.SaveAs(URL);
        }
        static void Main()
        {

        }
    }  
}