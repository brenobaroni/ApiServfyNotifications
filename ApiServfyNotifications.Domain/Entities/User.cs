using System.ComponentModel.DataAnnotations;

namespace ApiServfyNotifications.Domain;

public partial class User
{
    [Key]
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Idpid { get; set; } = null!;

    public string PersonalIdNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public bool Active { get; set; }

    public string ContactNumber { get; set; } = null!;

    public string ContactNumberSecondary { get; set; } = null!;

    public string? Avatar { get; set; }
}
