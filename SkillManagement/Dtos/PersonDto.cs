namespace SkillManagement.Dtos
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; } // ← Добавлен ?
        public List<SkillDto> Skills { get; set; } = new(); // ← Инициализация
    }

    public class SkillDto
    {
        public string Name { get; set; } = string.Empty; // ← Инициализация
        public byte Level { get; set; }
    }
}
