using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS
{
    public class CollectionComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            if (x != y)
            {
                List<T> one = x.ToList();
                List<T> two = y.ToList();
                if (one.Count != two.Count)
                {
                    return false;
                }
                Dictionary<T, int> counts = new Dictionary<T, int>();
                one.ForEach(t =>
                    {
                        if (counts.ContainsKey(t))
                        {
                            counts[t]++;
                        }
                        else
                        {
                            counts.Add(t, 1);
                        }
                    });
                two.ForEach(t =>
                    {
                        if (counts.ContainsKey(t))
                        {
                            counts[t]--;
                        }
                        else
                        {
                            counts.Add(t, -1);
                        }
                    });
                return counts.All(kvp => kvp.Value == 0);
            }
            else
            {
                return x == y;
            }
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            return 1;
        }
    }
}
