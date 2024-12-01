
using FitnessTrackerCore.Entities;

namespace FitnessTrackerCore.Interfaces
{
    public interface IRepository<TValue, TKey> where TValue : BaseEntity
    {
        public void Create(TValue item);

        public void Update(TValue item);

        public TValue Read(TKey key);

        public IQueryable<TValue> Read();

        public void Delete(TValue item);
    }
}
