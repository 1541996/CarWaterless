using System;

namespace Core.Interface
{
    public interface IDel<T>
    {
        Nullable<bool> IsDeleted { get; set; }

    }
}
