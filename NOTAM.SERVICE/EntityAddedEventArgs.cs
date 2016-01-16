using System;
using NOTAM.Service;

namespace NOTAM.SERVICE
{
    public class EntityAddedEventArgs<T>: EventArgs where T : NotamBase
    {

        public EntityAddedEventArgs(T newEntity)
        {
            this.NewEntity = newEntity;
        }

        public T NewEntity { get; private set; }
    }
}
