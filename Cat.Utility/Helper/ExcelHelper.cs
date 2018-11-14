using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace Cat.Utility
{
    public class ExcelHelper
    {
        //public static string BasePath
        //{
        //    //get { return VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath); }
        //    get { return Cat.Foundation.CatContext.HostingEnvironment.WebRootPath; }
        //}

        /// <summary>
        /// 将DataTable导出到Excel，返回文件的web路径
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string ExportDataTableToExcel(DataTable dt, string fileName)
        {
            #region 表头

            var hssfworkbook = new HSSFWorkbook();
            HSSFSheet hssfSheet = hssfworkbook.CreateSheet(fileName);
            hssfSheet.DefaultColumnWidth = 20;
            hssfSheet.SetColumnWidth(0, 35 * 256);
            hssfSheet.SetColumnWidth(3, 20 * 256);
            // 表头
            HSSFRow tagRow = hssfSheet.CreateRow(0);
            tagRow.Height = 22 * 20;

            // 标题样式
            HSSFCellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
            cellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
            cellStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellStyle.BottomBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderLeft = HSSFCellStyle.BORDER_THIN;
            cellStyle.LeftBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderRight = HSSFCellStyle.BORDER_THIN;
            cellStyle.RightBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderTop = HSSFCellStyle.BORDER_THIN;
            cellStyle.TopBorderColor = HSSFColor.BLACK.index;
            //NPOI.SS.UserModel.Font font = hssfworkbook.CreateFont();
            //font.Boldweight = 30 * 20;
            //font.FontHeight = 12 * 20;
            //cellStyle.SetFont(font);

            int colIndex;
            for (colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
            {
                tagRow.CreateCell(colIndex).SetCellValue(dt.Columns[colIndex].ColumnName);
                tagRow.GetCell(colIndex).CellStyle = cellStyle;
            }

            #endregion

            #region 表数据

            // 表数据 
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                DataRow dr = dt.Rows[k];
                HSSFRow row = hssfSheet.CreateRow(k + 1);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dr[i].ToString());
                    row.GetCell(i).CellStyle = cellStyle;
                }
            }

            #endregion

            string path = Cat.Foundation.CatContext.HostingEnvironment.ContentRootPath + "/UploadFiles/Export";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (var file = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                hssfworkbook.Write(file);
                file.Close();
            }
            return string.Format("{0}UploadFiles/Export/{1}", Cat.Foundation.CatContext.HostingEnvironment.WebRootPath, fileName);
        }

        /// <summary>
        /// 将DataSet导出到Excel，返回文件的web路径
        /// </summary>
        /// <param name="set">DataTable</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string ExportDataTableToExcel(List<DataTable> set, string fileName)
        {
            var hssfworkbook = new HSSFWorkbook();

            // 标题样式
            HSSFCellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
            cellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
            cellStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellStyle.BottomBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderLeft = HSSFCellStyle.BORDER_THIN;
            cellStyle.LeftBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderRight = HSSFCellStyle.BORDER_THIN;
            cellStyle.RightBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderTop = HSSFCellStyle.BORDER_THIN;
            cellStyle.TopBorderColor = HSSFColor.BLACK.index;

            int index = 0;
            foreach (DataTable dt in set)
            {
                index++;
                HSSFSheet hssfSheet =
                    hssfworkbook.CreateSheet(string.IsNullOrEmpty(dt.TableName) ? "Sheet" + index : dt.TableName);

                #region 表头

                hssfSheet.DefaultColumnWidth = 20;
                hssfSheet.SetColumnWidth(0, 35 * 256);
                hssfSheet.SetColumnWidth(3, 20 * 256);
                // 表头
                HSSFRow tagRow = hssfSheet.CreateRow(0);
                tagRow.Height = 22 * 20;

                int colIndex;
                for (colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    tagRow.CreateCell(colIndex).SetCellValue(dt.Columns[colIndex].ColumnName);
                    tagRow.GetCell(colIndex).CellStyle = cellStyle;
                }

                #endregion

                #region 表数据

                // 表数据 
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    DataRow dr = dt.Rows[k];
                    HSSFRow row = hssfSheet.CreateRow(k + 1);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(dr[i].ToString());
                        row.GetCell(i).CellStyle = cellStyle;
                    }
                }

                #endregion
            }

            string path = Cat.Foundation.CatContext.HostingEnvironment.ContentRootPath + "/UploadFiles/Export";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (var file = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                hssfworkbook.Write(file);
                file.Close();
            }
            return string.Format("{0}UploadFiles/Export/{1}", Cat.Foundation.CatContext.HostingEnvironment.WebRootPath, fileName);
        }

        /// <summary>
        /// 将DataSet导出到Excel，返回文件的web路径
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string ExportDataTableToExcel(DataSet set, string fileName)
        {
            var hssfworkbook = new HSSFWorkbook();

            // 标题样式
            HSSFCellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
            cellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
            cellStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellStyle.BottomBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderLeft = HSSFCellStyle.BORDER_THIN;
            cellStyle.LeftBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderRight = HSSFCellStyle.BORDER_THIN;
            cellStyle.RightBorderColor = HSSFColor.BLACK.index;
            cellStyle.BorderTop = HSSFCellStyle.BORDER_THIN;
            cellStyle.TopBorderColor = HSSFColor.BLACK.index;

            int index = 0;
            foreach (DataTable dt in set.Tables)
            {
                index++;
                HSSFSheet hssfSheet =
                    hssfworkbook.CreateSheet(string.IsNullOrEmpty(dt.TableName) ? "Sheet" + index : dt.TableName);

                #region 表头

                hssfSheet.DefaultColumnWidth = 20;
                hssfSheet.SetColumnWidth(0, 35 * 256);
                hssfSheet.SetColumnWidth(3, 20 * 256);
                // 表头
                HSSFRow tagRow = hssfSheet.CreateRow(0);
                tagRow.Height = 22 * 20;

                int colIndex;
                for (colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    tagRow.CreateCell(colIndex).SetCellValue(dt.Columns[colIndex].ColumnName);
                    tagRow.GetCell(colIndex).CellStyle = cellStyle;
                }

                #endregion

                #region 表数据

                // 表数据 
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    DataRow dr = dt.Rows[k];
                    HSSFRow row = hssfSheet.CreateRow(k + 1);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(dr[i].ToString());
                        row.GetCell(i).CellStyle = cellStyle;
                    }
                }

                #endregion
            }

            string path = Cat.Foundation.CatContext.HostingEnvironment.ContentRootPath + "/UploadFiles/Export";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (var file = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                hssfworkbook.Write(file);
                file.Close();
            }
            return string.Format("{0}UploadFiles/Export/{1}", Cat.Foundation.CatContext.HostingEnvironment.WebRootPath, fileName);
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="fileName"></param> 
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName)
        {
            var data = new DataTable();
            try
            {
                var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fileStream);
                HSSFSheet sheet = workbook.GetSheetAt(0);

                if (sheet != null)
                {
                    HSSFRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        HSSFCell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                var column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    int startRow = sheet.FirstRowNum + 1;

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        HSSFRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}