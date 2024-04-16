namespace WarehouseService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.EntityValidationConstants.Warehouse;
    using static Common.ApplicationConstants;

    public class WarehouseAllQueryModel
    {
        public WarehouseAllQueryModel()
        {
            CurrentPage = DefaultPage;
            WarehousesPerPage = DefaultItemsPerPage;

            Warehouses = new HashSet<WarehouseViewModel>();
        }

        public string? SearchTerm { get; set; }

        [Range(WarehouseSortingMinValue, WarehouseSortingMaxValue)]
        public WarehouseSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        [Range(PerPageMinValue, PerPageMaxValue)]
        public int WarehousesPerPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<WarehouseViewModel> Warehouses { get; set; }
    }
}
