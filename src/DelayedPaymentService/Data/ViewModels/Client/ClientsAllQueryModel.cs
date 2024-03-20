namespace DelayedPaymentService.Data.ViewModels.Client
{
    using static Common.ApplicationConstants;

    public class ClientsAllQueryModel
    {
        public ClientsAllQueryModel()
        {
            CurrentPage = DefaultPage;
            ClientsPerPage = DefaultItemsPerPage;

            Clients = new HashSet<ClientViewModel>();
        }
        public string? SearchTerm { get; set; }

        public int CurrentPage { get; set; } 

        public int ClientsPerPage { get; set; } 

        public int TotalPages { get; set; }

        public IEnumerable<ClientViewModel> Clients { get; set; }
    }
}
