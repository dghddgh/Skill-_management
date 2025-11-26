using System.ComponentModel.DataAnnotations;

namespace SkillManagement.Dtos
{
    public class CreatePersonDto
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Длина имени не может превышать 100 символов")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Длина отображаемого имени не может превышать 100 символов")]
        public string? DisplayName { get; set; }

        public List<CreateSkillDto>? Skills { get; set; }
    }

    public class CreateSkillDto
    {
        [Required(ErrorMessage = "Название навыка обязательно")]
        [MaxLength(50, ErrorMessage = "Длина названия навыка не может превышать 50 символов")]
        public string Name { get; set; } = string.Empty; // ← Добавлено string.Empty

        [Range(1, 10, ErrorMessage = "Уровень навыка должен быть от 1 до 10")]
        public byte Level { get; set; }
    }
}
