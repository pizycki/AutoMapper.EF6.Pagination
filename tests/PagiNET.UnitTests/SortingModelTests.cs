﻿using System;

namespace PagiNET.UnitTests
{
    public class SortingModelTests
    {
        //[Test]
        //public void should_fail_when_orderBy_is_null() =>
        //    Should.Throw<ArgumentNullException>(() => Sorting<Foo, DateTime>.By(null));

        class Foo
        {
            public DateTime Time { get; set; }
        }
    }
}