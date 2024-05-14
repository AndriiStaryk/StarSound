using System.ComponentModel.DataAnnotations;

namespace StarSound.CreationModels;

public class PerformerCreationModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field must not be empty")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public byte[]? Image { get; set; }
}
