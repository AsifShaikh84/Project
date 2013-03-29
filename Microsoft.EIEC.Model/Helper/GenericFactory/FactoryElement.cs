namespace Microsoft.EIEC.Model.Helper.GenericFactory
{
    public class FactoryElement<T> : IFactoryElement where T : new()
    {
        #region IFactoryElement Members

        public object New()
        {
            return new T();
        }

        #endregion
    }
}
