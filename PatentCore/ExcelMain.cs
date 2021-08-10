using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ExcelInterop
{
    public class ExcelMain
    {
        public string FilePath { get; set; }
        public ExcelMain(string filePath = null)
        {
            FilePath = filePath;
        }
        public string[,] GetExcelData()
        {
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            string[,] strArray = null;
            try
            {
                excelApp = new Excel.Application();

                wb = excelApp.Workbooks.Open(FilePath);

                ws = wb.Worksheets.get_Item(1);

                Excel.Range rng = ws.UsedRange;

                object[,] data = rng.Value;
                strArray = new string[data.GetLength(0), data.GetLength(1)];

                for (int r = 1; r <= data.GetLength(0); r++)
                {
                    for (int c = 1; c <= data.GetLength(1); c++)
                    {
                        if (data[r, c] == null) continue;
                        strArray[r - 1, c - 1] = data[r, c].ToString();
                    }
                }

                wb.Close(0);
                excelApp.Quit();
                Marshal.ReleaseComObject(rng);
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }

            return strArray;
            //return strArray;
        }

        //입력받은 str와 같은 셀을 찾는다
        //찾은셀의 같은 컬럼라인에서 끝날때 까지 list에 각 셀을 저장하고 리스트를 반환한다.
        public List<string> GetColByString(string str)
        {
            var arr = GetExcelData();

            int rLen = arr.GetLength(0);
            int cLen = arr.GetLength(1);

            var ret = new List<string>();
            for (int r = 0; r < rLen; r++)
            {
                //if (string.IsNullOrEmpty(arr[r, 0])) continue;
                for (int c = 0; c < cLen; c++)
                {
                    if (string.IsNullOrEmpty(arr[r, c])) continue;
                    if (arr[r, c].Equals(str))
                    {
                        r++;
                        while (r < rLen && !string.IsNullOrEmpty(arr[r, c]))
                        {
                            ret.Add(arr[r, c]);
                            r++;
                        }
                        return ret;
                    }
                }
            }

            return ret;
        }
        public void RunTest()
        {
            List<string> testData = new List<string>()
            { "Excel", "Access", "Word", "OneNote" };

            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Add();
                ws = wb.Worksheets.get_Item(1);

                int r = 1;
                foreach (var d in testData)
                {
                    ws.Cells[r, 1] = d;
                    r++;
                }

                wb.SaveAs(FilePath, Excel.XlFileFormat.xlWorkbookNormal);
                wb.Close(true);
                excelApp.Quit();
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }
        }

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
