using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public static class HeaderCellStyles
{
    public static ICellStyle ValueToCenter(HSSFWorkbook workbook)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        return cellStyle;
    }
    public static ICellStyle ValueToRight(HSSFWorkbook workbook)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        return cellStyle;
    }

    public static IFont WhiteBoldFont(HSSFWorkbook workbook)
    {
        IFont font = workbook.CreateFont();
        font.Color = IndexedColors.White.Index;
        font.IsBold = true;
        return font;
    }
    public static ICellStyle BrownWhiteBoldCenter(HSSFWorkbook workbook)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.FillPattern = FillPattern.SolidForeground;
        cellStyle.FillForegroundColor = IndexedColors.Brown.Index;
        cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        cellStyle.SetFont(WhiteBoldFont(workbook));
        return cellStyle;
    }

    public static ICellStyle BlueWhiteBoldCenter(HSSFWorkbook workbook)
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.FillPattern = FillPattern.SolidForeground;
        cellStyle.FillForegroundColor = IndexedColors.Blue.Index;
        cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = VerticalAlignment.Top;
        cellStyle.SetFont(WhiteBoldFont(workbook));
        return cellStyle;
    }
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
