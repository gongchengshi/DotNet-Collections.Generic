using System;
using System.Collections.Generic;

namespace Gongchengshi.Collections.Generic
{
    /// <summary>
    /// This is basically a List<Tuple<T...>>.  The reason it exists is because a List<Tuple<>> does not
    /// have nice initialization (requires a new Tuple<> on every line).  Thus, you can, with a TupleList
    /// do:
    ///    ... = new TupleList<string, int>
    ///    {
    ///        { "One", 1 },
    ///        { "Two", 2 }
    ///    }
    /// </summary>
    public class TupleList<T1, T2> : List<Tuple<T1, T2>>
    {
        public void Add(T1 item1, T2 item2)
        {
            Add(new Tuple<T1, T2>(item1, item2));
        }
    }

    public class TupleList<T1, T2, T3> : List<Tuple<T1, T2, T3>>
    {
        public void Add(T1 item1, T2 item2, T3 item3)
        {
            Add(new Tuple<T1,T2,T3>(item1, item2, item3));
        }
    }
    
    public class TupleList<T1, T2, T3, T4> : List<Tuple<T1, T2, T3, T4>>
    {
        public void Add(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            Add(new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4));
        }
    }
}
