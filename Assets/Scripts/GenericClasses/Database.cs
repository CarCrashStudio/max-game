using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Database<T>
{
    public List<T> Db { get; }

    public void Add (T add)
    {
        Db.Add(add);
    }
    public void Remove (T remove)
    {
        Db.Remove(remove);
    }

    public T Find (T matchRecord)
    {
        return Db.Where(d => d.Equals(matchRecord)).FirstOrDefault();
    }
    public T Find(string matchName)
    {
        return Db.Where(d => ((dynamic)d).Name == matchName).FirstOrDefault();
    }
    public T Find(int matchID)
    {
        return Db.Where(d => ((dynamic)d).ID == matchID).FirstOrDefault();
    }
}
