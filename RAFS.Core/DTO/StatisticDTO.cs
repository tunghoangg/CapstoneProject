using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class StatisticDTO
    {
        public int FarmId { get; set; }
        public string? FarmName { get; set; }
        public double FarmArea { get; set; }
        public string? FarmLogo { get; set; }
        //Tổng diện tích
        public double TotalArea { get; set; }
        //Tổng thu
        public double TotalIn { get; set; }
        //Tổng chi
        public double TotalOut { get; set; }
        //Tổng sản phẩm
        public int TotalMaterial { get; set; }
        //Tổng giá trị
        public double TotalMaterialValue { get; set; }
        //Tổng thu theo quý
        public double[]? CashFlowIn { get; set; }
        //Tổng chi theo quý
        public double[]? CashFlowOut { get; set; }
        //Chi tiết tổng thu của các loại 9-13 => %
        public double[]? CashFlowDetailIn { get; set; }
        //Chi tiết tổng chi của các loại 1-8 => %
        public double[]? CashFlowDetailOut { get; set; }
        //Giá trị
        public double[]? ItemValue { get; set; }
        //Top 3 name
        public string[]? TopThreeName { get; set; }
        //Phần trăm top 3 loại
        public double[]? TopThreeValue { get; set; }
    }

    public class StatisticCashFlowDTO
    {
        //Tổng thu
        public double TotalIn { get; set; }
        //Tổng chi
        public double TotalOut { get; set; }
        //Tổng sản phẩm
        //Tổng thu theo quý
        public double[]? CashFlowIn { get; set; }
        //Tổng chi theo quý
        public double[]? CashFlowOut { get; set; }
        //Chi tiết tổng thu của các loại 9-13 => %
        public double[]? CashFlowDetailIn { get; set; }
        //Chi tiết tổng chi của các loại 1-8 => %
        public double[]? CashFlowDetailOut { get; set; }
    }

    public class StatisticItemDTO
    {
        //Tổng sản phẩm
        public int TotalMaterial { get; set; }
        //Tổng giá trị
        public double TotalMaterialValue { get; set; }
        //Giá trị
        public double[]? ItemValue { get; set; }
        //Top 3 name
        public string[]? TopThreeName { get; set; }
        //Phần trăm top 3 loại
        public double[]? TopThreeValue { get; set; }
    }

}
