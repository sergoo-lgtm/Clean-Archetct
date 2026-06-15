namespace eTickets.DTO.ProducerDTOS;

public class ProducerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ProfilePictureURL { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}
