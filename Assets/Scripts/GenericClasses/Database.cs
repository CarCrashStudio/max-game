using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Database<T>
{
    public List<T> Db { get; } = new List<T>();

    public void Add (T add)
    {
        Db.Add(add);
    }
    public void Remove (T remove)
    {
        Db.Remove(remove);
    }

    public void Load (string location)
    {
        using (StreamReader reader = new StreamReader(location))
        {
            var json = reader.ReadToEnd().Replace("\n", "");
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            Db.AddRange(JsonConvert.DeserializeObject<List<T>>(json, settings));
            reader.Close();
        }
    }

    public T Find (T matchRecord)
    {
        return Db.Where(d => d.Equals(matchRecord)).FirstOrDefault();
    }
    //public T Find(string matchName)
    //{
    //    return Db.Where(d => ((dynamic)d).Name == matchName).FirstOrDefault();
    //}
    //public T Find(int matchID)
    //{
    //    return Db.Where(d => ((dynamic)d).ID == matchID).FirstOrDefault();
    //}
}
