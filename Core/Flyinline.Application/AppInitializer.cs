using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application
{
    public static class AppInitializer
    {
        public static void Initialize()
        {
            Infrastructure.ExpressMapper.ExpressMapperInitializer.Initialize();
        }
    }
}
