using System;

namespace Common.Models
{
    public abstract class ModelBase<TKey>
    {
        public TKey Id { get; set; }

        public DateTime Created { get; set; }
    }
}
