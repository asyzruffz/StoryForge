namespace StoryForge.Core.Storage.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    void Create(T obj);
    void Create(IEnumerable<T> objs);
    void Update(T obj);
    void Delete(T obj);
    //void Attach(T obj);
    //void Attach(IEnumerable<T> objs);
    //void LoadSingle<TResult>(T obj, Expression<Func<T, TResult?>> expression) where TResult : class;
    //void LoadCollection<TResult>(T obj, Expression<Func<T, IEnumerable<TResult>>> expression) where TResult : class;
}
