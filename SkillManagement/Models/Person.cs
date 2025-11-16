using System.ComponentModel.DataAnnotations;

namespace SkillManagement.Models;

public class Person
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
