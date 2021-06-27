using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public static class HeaderCellStyles
{
    public static ICellStyle BrightGreen(HSSFWorkbook workbook)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.FillPattern = FillPattern.SolidForeground;
        cellStyle.FillForegroundColor = IndexedColors.BrightGreen.Index;
        cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        return cellStyle;
    }
    public static ICellStyle LightGreen(HSSFWorkbook workbook)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.FillPattern = FillPattern.SolidForeground;
        cellStyle.FillForegroundColor = IndexedColors.LightGreen.Index;
        cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        return cellStyle;
    }
    public static ICellStyle Yellow(HSSFWorkbook workbook)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.FillPattern = FillPattern.SolidForeground;
        cellStyle.FillForegroundColor = IndexedColors.Yellow.Index;
        cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        return cellStyle;
    }

    public static ICellStyle Custom(HSSFWorkbook workbook, short colorIndex)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.FillPattern = FillPattern.SolidForeground;
        cellStyle.FillForegroundColor = colorIndex;
        cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        return cellStyle;
    }




}
