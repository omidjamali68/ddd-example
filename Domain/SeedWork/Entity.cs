namespace Domain.SeedWork
{
    public class Entity : IEntity
    {
        protected Entity() : base()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}