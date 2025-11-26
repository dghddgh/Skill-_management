using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SkillManagement.Dtos;

namespace SkillManagement.Dtos
{
    public class UpdatePersonDto
    {
        [MaxLength(100, ErrorMessage = "Длина имени не может превышать 100 символов")]
        public string? Name { get; set; } // ← Можно обновлять имя

        [MaxLength(100, ErrorMessage = "Длина отображаемого имени не может превышать 100 символов")]
        public string? DisplayName { get; set; } // ← Можно обновлять DisplayName


        public List<CreateSkillDto>? Skills { get; set; }

        // Опциональная валидация на уровне DTO
        public bool IsValid()
        {
            if (Skills != null)
            {
                foreach (var skill in Skills)
                {
                    if (string.IsNullOrEmpty(skill.Name) || skill.Level < 1 || skill.Level > 10)
                        return false;
                }
            }
            return true;
        }
    }
}
