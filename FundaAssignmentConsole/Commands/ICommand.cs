namespace FundaAssignment.Commands;

public interface ICommand
{
    string Name { get; }
    Task ExecuteAsync();
}