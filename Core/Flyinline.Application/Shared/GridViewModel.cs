using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Shared
{
    public class GridViewModel<T>
    {
        public GridViewModel()
        {
            Data = new List<T>();
        }

        public List<T> Data { get; private set; }
    }
}
