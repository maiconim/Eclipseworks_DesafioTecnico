namespace DesafioTecnico.Business.Commons
{
    internal class ChangesInfo
    {
        public object? OldValue { get; }
        public object? NewValue { get; }

        public ChangesInfo(object? oldValue, object? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}