using CopyStart.Domain.Common;

namespace CopyStart.Domain.Modules.Procedures;

public class Procedure : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Title { get; private set; }
    public string Instructions { get; private set; }
    public bool IsActive { get; private set; }

    private Procedure()
    {
        Code = string.Empty;
        Title = string.Empty;
        Instructions = string.Empty;
    }

    public Procedure(string code, string title, string instructions)
    {
        Id = Guid.NewGuid();
        Code = string.IsNullOrWhiteSpace(code) ? throw new ArgumentException("Procedure code is required.", nameof(code)) : code;
        Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Procedure title is required.", nameof(title)) : title;
        Instructions = instructions ?? string.Empty;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
