// ##################################################

// ##################################################

namespace Lugh.Utils
{
    public class Entry< TKey, TValue >
    {
        public TKey   key;
        public TValue value;

        public new string ToString()
        {
            return key + "=" + value;
        }
    }
}

