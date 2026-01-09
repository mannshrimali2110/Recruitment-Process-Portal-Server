namespace recruitment_process_portal_server.DTOs.Requests;

 public class UpdatePositionStatusRequest
{
     
         public int NewStatusId { get; set; }

     
         public string Reason { get; set; } = string.Empty;
}
