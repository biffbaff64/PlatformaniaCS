// ##################################################

// ##################################################

namespace Lugh.Utils;

public class StringObjectPair
{
    public string StringPart { get; set; }
    public object ObjectPart { get; set; }

    public StringObjectPair()
    {
    }
    
    public StringObjectPair( string s, object o )
    {
        this.StringPart = s;
        this.ObjectPart = o;
    }
}
