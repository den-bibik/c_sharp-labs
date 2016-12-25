using System;

namespace lab_libary
{
    public interface IDeepCopy
    { object DeepCopy(); }

    public interface IDate
    { DateTime Date { get; set; } }

}
