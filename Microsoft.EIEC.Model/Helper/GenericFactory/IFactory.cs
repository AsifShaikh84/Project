using System;

namespace Microsoft.EIEC.Model.Helper.GenericFactory
{
    public interface IFactory<K, T> where K : IComparable
    {
        T Create(K key);
    }
}
