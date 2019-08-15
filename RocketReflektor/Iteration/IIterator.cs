using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RocketReflektor.Iteration
{
    public interface IIterator<TOut>
    {
        int Count { get; }
        TOut Item(object index);
    }
}
