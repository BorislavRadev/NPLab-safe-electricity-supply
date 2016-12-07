using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPLab;
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
        public void ConvertDefExcel(string URL, EL_4 currRep)
        {
            Excel._Worksheet DefSheet;
            //Excel.Range Rng;
            int currRow;
            bool inPro = false;
            try
            {
                //Start Excel and get Application object.
                AppXL = new Excel.Application();
                AppXL.Visible = true;

                //Get a new workbook.
                ConvertXL = (Excel.Workbook)(AppXL.Workbooks.Add(Missing.Value));
                DefSheet = (Excel._Worksheet)ConvertXL.Worksheets.Add();
                DefSheet = (Excel._Worksheet)ConvertXL.Worksheets.Add();
                

                //Заглавия на колоните
                DefSheet.Cells[1, 1] = "№ \nпо \nред";
                DefSheet.Cells[1, 2] = "ОБЕКТ НА КОНТРОЛА";
                DefSheet.get_Range("C1", "D1").Merge();
                DefSheet.get_Range("E1", "F1").Merge();
                DefSheet.Cells[1, 3] = "ИЗМЕРЕНИ СТОЙНОСТИ";
                DefSheet.Cells[1, 5] = "МАКСИМАЛНО\n ДОПУСТИМИ\n ВЕЛИЧИНИ";
                DefSheet.get_Range("A1", "A2").Merge();
                DefSheet.get_Range("B1", "B2").Merge();
                DefSheet.Cells[2, 3] = "t изкл. \nms";
                DefSheet.Cells[2, 4] = "I ∆N \nmA";
                DefSheet.Cells[2, 5] = "t изкл. \nms";
                DefSheet.Cells[2, 6] = "I ∆N \nmA";
                DefSheet.get_Range("A1", "H1").RowHeight = 30;
                DefSheet.get_Range("A2", "H2").RowHeight = 60;
                DefSheet.get_Range("A1", "H1").Font.Size = 10;
                DefSheet.get_Range("A2", "H2").Font.Size = 9;
                DefSheet.get_Range("A1", "H2").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                DefSheet.get_Range("A2", "H2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                //Изграждане на 3-ти ред
                for (int i = 1; i <= 6; i++) DefSheet.Cells[3, i] = i;
                currRow = 4; // редът, на който се въвежда в момента

                //Записване на всеки сектор

                foreach (Protectors pro in Objects.Stats.Protectors)
                {
                    foreach (Sectors_El_4 sector in currRep.ListOfSectors.TakeWhile<Sectors_El_4>(sector => sector.ListOfFloors.Any<Floors_El_4>(fl => fl.ListOfRooms.Any<Rooms_El_4>(x => x.ListOfInstallations.Any<Installation_El_4>(y => y.TypeProtector == Objects.Stats.Protectors.IndexOf(pro))))))
                    {
                        if (!inPro && sector.ListOfFloors.All<Floors_El_4>(x => x.ListOfRooms.Any<Rooms_El_4>(room => room.ListOfInstallations.Any(y => y.TypeProtector == Objects.Stats.Protectors.IndexOf(pro))))) 
                        {
                            DefSheet.Cells[currRow, 2] = pro.Name;
                            DefSheet.Cells[currRow, 1] = Objects.toRoman(pro.Number) + ". ";
                            DefSheet.Cells[currRow, 3] = pro.DT;
                            DefSheet.Cells[currRow, 4] = pro.DN;
                            DefSheet.Cells[currRow, 5] = pro.MaxDT;
                            DefSheet.Cells[currRow, 6] = pro.MaxDN;
                            DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                            DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            currRow++;
                            DefSheet.Cells[currRow, 2] = "Защитаващ следните съоръжения:";
                            DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                            DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Merge();
                            DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            currRow++;
                            inPro = true;
                        }
                        DefSheet.Cells[currRow, 2] = sector.SectorName;
                        DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                        DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                        DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Merge();
                        currRow++;
                        //Записване на етажите
                        foreach (Floors_El_4 floor in sector.ListOfFloors.TakeWhile<Floors_El_4>(fl => fl.ListOfRooms.Any<Rooms_El_4>(x => x.ListOfInstallations.Any<Installation_El_4>(y => y.TypeProtector == Objects.Stats.Protectors.IndexOf(pro)))))
                        {
                            if (!inPro && floor.ListOfRooms.All<Rooms_El_4>(x => x.ListOfInstallations.Any<Installation_El_4>(y => y.TypeProtector == Objects.Stats.Protectors.IndexOf(pro))))
                            {
                                DefSheet.Cells[currRow, 2] = pro.Name;
                                DefSheet.Cells[currRow, 1] = Objects.toRoman(pro.Number) + ". ";
                                DefSheet.Cells[currRow, 3] = pro.DT;
                                DefSheet.Cells[currRow, 4] = pro.DN;
                                DefSheet.Cells[currRow, 5] = pro.MaxDT;
                                DefSheet.Cells[currRow, 6] = pro.MaxDN;
                                DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                                DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                currRow++;
                                DefSheet.Cells[currRow, 2] = "Защитаващ следните съоръжения:";
                                DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                                DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Merge();
                                DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                currRow++;
                                inPro = true;
                            }
                            DefSheet.Cells[currRow, 2] = floor.NameFloor;
                            DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                            DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                            DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            currRow++;
                            foreach (Rooms_El_4 room in floor.ListOfRooms.TakeWhile<Rooms_El_4>(r => r.ListOfInstallations.Any<Installation_El_4>(y => y.TypeProtector == Objects.Stats.Protectors.IndexOf(pro))))
                            {
                                if (!inPro)
                                {
                                    DefSheet.Cells[currRow, 2] = pro.Name;
                                    DefSheet.Cells[currRow, 1] = Objects.toRoman(pro.Number) + ". ";
                                    DefSheet.Cells[currRow, 3] = pro.DT;
                                    DefSheet.Cells[currRow, 4] = pro.DN;
                                    DefSheet.Cells[currRow, 5] = pro.MaxDT;
                                    DefSheet.Cells[currRow, 6] = pro.MaxDN;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    currRow++;
                                    DefSheet.Cells[currRow, 2] = "Защитаващ следните съоръжения:";
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Merge();
                                    DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    currRow++;
                                    DefSheet.Cells[currRow, 2] = room.RoomName;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    currRow++;
                                    inPro = true;
                                }
                                else
                                {
                                    DefSheet.Cells[currRow, 2] = room.RoomName;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    currRow++;
                                    inPro = true;
                                    DefSheet.Cells[currRow, 2] = pro.Name;
                                    DefSheet.Cells[currRow, 1] = Objects.toRoman(pro.Number) + ". ";
                                    DefSheet.Cells[currRow, 3] = pro.DT;
                                    DefSheet.Cells[currRow, 4] = pro.DN;
                                    DefSheet.Cells[currRow, 5] = pro.MaxDT;
                                    DefSheet.Cells[currRow, 6] = pro.MaxDN;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Bold = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    currRow++;
                                    DefSheet.Cells[currRow, 2] = "Защитаващ следните съоръжения:";
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Font.Italic = true;
                                    DefSheet.Range[DefSheet.Cells[currRow, 1], DefSheet.Cells[currRow, 6]].Merge();
                                    DefSheet.Range[DefSheet.Cells[currRow, 3], DefSheet.Cells[currRow, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    currRow++;
                                }
                                foreach (Installation_El_4 inst in room.ListOfInstallations.TakeWhile<Installation_El_4>(i => i.TypeProtector == Objects.Stats.Protectors.IndexOf(pro)))
                                {
                                    DefSheet.Cells[currRow, 2] = inst.InstallationName;
                                    DefSheet.Cells[currRow, 1] = inst.NumberIn.ToString() + ". ";
                                    DefSheet.Cells[currRow, 3] = inst.DT;
                                    DefSheet.Cells[currRow, 4] = inst.DN;
                                    DefSheet.Cells[currRow, 5] = inst.MaxDT;
                                    DefSheet.Cells[currRow, 6] = inst.MaxDN;
                                    currRow++;
                                }
                            }
                        }
                    }
                }

                //Формат на таблицата
                DefSheet.get_Range("A1", "G" + currRow.ToString()).Font.Name = "Bookman Old Style";
                DefSheet.get_Range("A3", "G" + currRow.ToString()).Font.Size = 11;
                DefSheet.get_Range("A2", "G2").Font.Size = 10;
                DefSheet.get_Range("A1", "G1").Font.Size = 9;
                DefSheet.get_Range("A1", "F" + currRow.ToString()).VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;
                DefSheet.get_Range("A1", "A" + currRow.ToString()).ColumnWidth = 4;
                DefSheet.get_Range("B1", "B" + currRow.ToString()).ColumnWidth = 35;
                DefSheet.get_Range("C1", "C" + currRow.ToString()).ColumnWidth = 11;
                DefSheet.get_Range("D1", "D" + currRow.ToString()).ColumnWidth = 12;
                DefSheet.get_Range("E1", "E" + currRow.ToString()).ColumnWidth = 11;
                DefSheet.get_Range("F1", "F" + currRow.ToString()).ColumnWidth = 12;
                DefSheet.get_Range("A4", "F" + currRow.ToString()).WrapText = true;
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
    }
}
      
