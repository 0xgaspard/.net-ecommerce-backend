public class NestedCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<NestedCategoryDto> Subcategories { get; set; } = new List<NestedCategoryDto>();
}