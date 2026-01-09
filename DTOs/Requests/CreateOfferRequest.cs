namespace recruitment_process_portal_server.DTOs.Requests;

public class CreateOfferRequest
{
    public DateTime OfferDate { get; set; }
    public decimal CTC { get; set; }
    public DateTime? JoiningDate { get; set; }
}
