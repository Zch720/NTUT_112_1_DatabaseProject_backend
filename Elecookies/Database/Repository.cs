namespace Elecookies.Database {
    public interface Repository <T, ID> {
        void Save(T value);
        void Delete(ID id);
        T? FindByID(ID id);
        List<T> All();
    }
}