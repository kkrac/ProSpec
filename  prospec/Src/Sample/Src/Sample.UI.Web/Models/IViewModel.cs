namespace Sample.UI.Web.Models
{
    public interface IViewModel<T> where T : class
    {
        T ToModel();
    }
}
