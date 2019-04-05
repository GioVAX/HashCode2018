﻿using System.Collections.Generic;

namespace DancingLinks.UnitTests
{
    public class TestOption : IDlOption<int>
    {
        public TestOption(IEnumerable<int> items)
        {
            Items = items;
        }

        public IEnumerable<int> Items { get; }

        public int CompareTo(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}