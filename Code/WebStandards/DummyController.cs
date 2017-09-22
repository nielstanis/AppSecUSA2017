using System;
using System.Collections.Generic;
using System.Text;

namespace WebStandards
{
    public class DummyController
    {
        public string Index(string input)
        {
            return $"Hello {input}";
        }
    }
}
