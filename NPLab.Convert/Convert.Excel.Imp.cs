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
using FileStream  = System.IO.FileStream;
namespace NPLab.ConvertFile
{
    public partial class ConvertFileExcel
    {
        //Excel.Application AppXL = new Excel.Application();
				
		public void ConvertImpExcel(string URL, El_2 currRep)
		{
			Excel._Worksheet ImpSheet;
			//Excel.Range Rng;
			int instCount = 0, currRow;
			try
			{
				//Start Excel and get Application object.
				AppXL.Visible = true;
		  
				//Get a new workbook.
				ConvertXL = (Excel.Workbook)(AppXL.Workbooks.Add( Missing.Value ));
				ImpSheet = (Excel._Worksheet)ConvertXL.Worksheets.Add();
                ImpSheet.Name = "Импеданс";
		  
				//Заглавия на колоните
				ImpSheet.Cells[1, 1] = "№ \nпо \nред";
				ImpSheet.Cells[1, 2] = "НАИМЕНОВАНИЕ НА УРЕДБАТА \n(СЪОРЪЖЕНИЕТО)";
				ImpSheet.get_Range("C1", "E1").Merge();
				ImpSheet.Cells[1, 3] = "ВИД НА МАКСИМАЛНОТОКОВАТА \nЗАЩИТА";
				ImpSheet.Cells[1, 6] = "ИЗМЕРЕН\n ИМПЕ- \nДАНС \n[Ω}";
				ImpSheet.Cells[1, 7] = "МАКСИ-\nМАЛНО \nДОПУСТИМ \nИМПЕДАНС \n[Ω}";
				ImpSheet.get_Range("A1", "A2").Merge();
				ImpSheet.get_Range("B1", "B2").Merge();
				ImpSheet.Cells[2, 3] = "Стопяем\n предпа-\n зител \n Iн [A]";
				ImpSheet.Cells[2, 4] = "Автома-\nтичен \nпрекъсвач \nIн [A]";
				ImpSheet.Cells[2, 5] = "Коефи-\nциент на \nзадейст-\nване";
				ImpSheet.get_Range("F1", "F2").Merge();
				ImpSheet.get_Range("G1", "G2").Merge();
                ImpSheet.get_Range("A1", "H1").RowHeight = 30;
                ImpSheet.get_Range("A2", "H2").RowHeight = 60;
                ImpSheet.get_Range("A1", "H1").Font.Size = 10;
                ImpSheet.get_Range("A2", "H2").Font.Size = 9;
                ImpSheet.get_Range("A1", "H2").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ImpSheet.get_Range("A2", "H2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

				//Изграждане на 3-ти ред
				for(int i = 1; i <= 6; i++) ImpSheet.Cells[3, i] = i;
				currRow = 4; // редът, на който се въвежда в момента
			
				//Записване на всеки сектор
				foreach(Sectors sector in currRep.ListOfSectors) 
				{
                    if (sector.SectorName != "Единствен")
                    {
                        ImpSheet.Cells[currRow, 1] = sector.SectorName;
                        ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].Merge();
                        ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].Font.Bold = true;
                        ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        currRow++;
                    }
				  
                  //Записване на етажите
				  foreach(Floors floor in sector.ListOfFloors) 
				  {
                      if (floor.NameFloor != "Единствен")
                      {
                          ImpSheet.Cells[currRow, 2] = floor.NameFloor;
                          ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].Font.Bold = true;
                          ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].Font.Underline = true;
                          ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                          currRow++;
                      }

					  //Записване на помещенията 
					foreach(Rooms room in floor.ListOfRooms) 
					{
					  ImpSheet.Cells[currRow, 2] = room.RoomName;
					  ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].Font.Bold = true;
					  ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].Font.Underline = true;
					  ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 7]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
					  currRow++;
					  //Записване на съоръженията
					  foreach(Installations inst in room.ListOfInstallations) 
					  {
						 instCount++;
						 ImpSheet.Cells[currRow, 1] = instCount.ToString()+". ";
						 ImpSheet.Cells[currRow, 2] = inst.InstallationName;
						 if(!inst.isAutomaticProtector) ImpSheet.Cells[currRow, 3] = inst.Amperage;
						 else ImpSheet.Cells[currRow, 4] = inst.Amperage;
						 ImpSheet.Cells[currRow, 5] = inst.Coefficient;
						 ImpSheet.Cells[currRow, 6] = inst.Impedance;
                         ImpSheet.Cells[currRow, 7] = inst.Max.ToString();
						 ImpSheet.Range[ImpSheet.Cells[currRow, 3], ImpSheet.Cells[currRow, 7]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                         ImpSheet.Range[ImpSheet.Cells[currRow, 1], ImpSheet.Cells[currRow, 2]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                         currRow++;
						 //ImpSheet.Range[ImpSheet.Cells[currRow, 2]].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
					  }
					}
				  }
				}
			
				//Формат на таблицата
				ImpSheet.get_Range("A1", "G" + currRow.ToString()).Font.Name = "Bookman Old Style";
				ImpSheet.get_Range("A3", "G" + currRow.ToString()).Font.Size = 11;
				ImpSheet.get_Range("A2", "G2").Font.Size = 10;
				ImpSheet.get_Range("A1", "G1").Font.Size = 9;
                //ImpSheet.get_Range("B4", "B" + currRow.ToString()).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                //ImpSheet.get_Range("F4", "G" + currRow.ToString()).;
                //ImpSheet.get_Range("B4", "B" + currRow.ToString()).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                
                ImpSheet.get_Range("A1", "G" + currRow.ToString()).VerticalAlignment = 
					Excel.XlVAlign.xlVAlignCenter;
                ImpSheet.get_Range("A1", "A" + currRow.ToString()).ColumnWidth = 4;
                ImpSheet.get_Range("B1", "B" + currRow.ToString()).ColumnWidth = 35;
                ImpSheet.get_Range("C1", "C" + currRow.ToString()).ColumnWidth = 14; 
                ImpSheet.get_Range("D1", "D" + currRow.ToString()).ColumnWidth = 11;
                ImpSheet.get_Range("E1", "E" + currRow.ToString()).ColumnWidth = 11;
                ImpSheet.get_Range("F1", "F" + currRow.ToString()).ColumnWidth = 11;
                ImpSheet.get_Range("G1", "G" + currRow.ToString()).ColumnWidth = 12;
                ImpSheet.get_Range("A4", "G" + currRow.ToString()).WrapText = true;
                ConvertXL.SaveAs(URL);
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
        
      }
    }  
}