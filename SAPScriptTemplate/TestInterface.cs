using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPScriptTemplate
{
    interface TestInterface<T>
    {
        void Run(T data);
    }
}
