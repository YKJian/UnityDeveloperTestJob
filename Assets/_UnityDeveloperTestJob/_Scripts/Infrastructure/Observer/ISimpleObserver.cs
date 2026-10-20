namespace Infrastructure.Observer
{
    public interface ISimpleObserver : IBaseObserver
    {
        public void OnNotify();
    }
}
