﻿namespace Warehouse.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class HasPrecisionAttribute : Attribute
    {
        public HasPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }

        public byte Precision { get; set; }
        public byte Scale { get; set; }
    }
}