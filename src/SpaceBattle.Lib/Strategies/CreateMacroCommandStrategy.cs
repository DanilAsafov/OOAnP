namespace SpaceBattle.Lib.Strategies;

public class CreateMacroCommandStrategy(string commandSpec) : IStrategy
{
    private readonly string _commandSpec = string.IsNullOrWhiteSpace(commandSpec)
        ? throw new ArgumentException("Спецификация команды не может быть пустой или null.", nameof(commandSpec))
        : commandSpec;

    public object Resolve(params object[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        var specKey = $"Specs.{_commandSpec}";

        IEnumerable<string> rawCommandNames;
        try
        {
            rawCommandNames = Ioc.Resolve<IEnumerable<string>>(specKey)
                ?? throw new InvalidOperationException($"Спецификация команды '{specKey}' разрешилась в null.");
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            throw new InvalidOperationException($"Спецификация команды '{specKey}' не содержит команд.", ex);
        }

        var commandNames = rawCommandNames.ToArray();

        if (commandNames.Length == 0)
        {
            throw new InvalidOperationException($"Спецификация команды '{specKey}' не содержит команд.");
        }

        ICommand[] commands = [.. commandNames.Select((name, index) =>
        {
            try
            {
                return Ioc.Resolve<ICommand>(name, args);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Не удалось разрешить команду '{name}' (индекс {index}) в спецификации '{_commandSpec}'.", ex);
            }
        })];

        return new MacroCommand(commands);
    }
}
