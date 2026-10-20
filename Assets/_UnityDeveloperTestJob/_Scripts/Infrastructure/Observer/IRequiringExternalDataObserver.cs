namespace Infrastructure.Observer
{
    public interface IRequiringExternalDataObserver : IBaseObserver
    {
        public void OnNotify<T>(T eventArgs);
    }
}
