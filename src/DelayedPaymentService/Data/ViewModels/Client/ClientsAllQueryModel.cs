namespace DelayedPaymentService.Data.ViewModels.Client
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.ApplicationConstants;
    using static Common.EntityValidationConstants.Client;

    public class ClientsAllQueryModel
    {
        public ClientsAllQueryModel()
        {
            CurrentPage = DefaultPage;
            ClientsPerPage = DefaultItemsPerPage;

            Clients = new HashSet<ClientViewModel>();
        }
        public string? SearchTerm { get; set; }

        [Range(ClientSortingMinValue, ClientSortingMaxValue)]
        public ClientSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        [Range(PerPageMinValue, PerPageMaxValue)]
        public int ClientsPerPage { get; set; } 

        public int TotalPages { get; set; }

        public IEnumerable<ClientViewModel> Clients { get; set; }
    }
}
