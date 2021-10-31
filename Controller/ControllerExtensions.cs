using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{

    static class ControllerExtensions {

        public static IEnumerable<T> ReverseEx<T>(this IEnumerable<T> coll)
        {
            var quick = coll as IList<T>;
            if (quick == null)
            {
                foreach (T item in coll.Reverse()) yield return item;
            }
            else
            {
                for (int ix = quick.Count - 1; ix >= 0; --ix)
                {
                    yield return quick[ix];
                }
            }
        }
    }
    }

