namespace Elecookies.Database {
    public interface Repository <T, ID> {
        void Save(T value);
        void Delete(ID id);
        T? FindById(ID id);
        List<T> All();
    }
}