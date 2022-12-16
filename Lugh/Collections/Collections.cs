// ##################################################

// ##################################################

namespace Lugh.Collections;

public class Collections
{
    /// <summary>
    /// When true, the iterator for Array, ObjectMap, and other collections
    /// will allocate a new iterator for each invocation. When false, the
    /// iterator is reused and nested use will throw an exception.
    /// Default is false.
    /// </summary>
    public static bool AllocateIterators { get; set; }
}
