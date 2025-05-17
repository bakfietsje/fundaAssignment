namespace FundaAssignment.Models;

public class Broker(int id,string name)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public int Count { get; set; } = 0;
}
