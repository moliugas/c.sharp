public class BaseIdModel : IIdModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}