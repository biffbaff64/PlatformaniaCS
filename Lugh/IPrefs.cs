using System.Collections.Generic;

namespace Lugh;

public interface IPrefs
{
    void PutBoolean (string key, bool val);

    void PutInteger (string key, int val);

    void PutLong (string key, long val);

    void PutFloat (string key, float val);

    void PutString (string key, string val);

    void Put (Dictionary<string, object> vals);

    bool GetBoolean (string key);

    int GetInteger (string key);

    long GetLong (string key);

    float GetFloat (string key);

    string GetString (string key);

    bool GetBoolean (string key, bool defValue);

    int GetInteger (string key, int defValue);

    long GetLong (string key, long defValue);

    float GetFloat (string key, float defValue);

    string GetString (string key, string defValue);

    Dictionary<string, object> Get();

    bool Contains (string key);

    void Clear ();

    void Remove (string key);

    void Flush ();

}