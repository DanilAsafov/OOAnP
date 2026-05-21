namespace SpaceBattle.Lib.Contracts;

public interface IStrategy
{
    object Resolve(params object[] args);
}
