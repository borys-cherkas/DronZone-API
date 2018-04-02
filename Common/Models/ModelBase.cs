namespace Common.Models
{
    public abstract class ModelBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
