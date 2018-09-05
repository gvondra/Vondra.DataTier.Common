using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class LoaderFactory : ILoaderFactory
    {
        public ILoader CreateLoader()
        {
            return new Loader
            {
                Components = new List<ILoaderComponent>() {
                        new StringLoaderComponent(),
                        new IntegerLoaderComponent(),
                        new ShortLoaderComponent(),
                        new DecimalLoaderComponent(),
                        new DoubleLoaderComponent(),
                        new DateLoaderComponent(),
                        new BytesLoaderComponent(),
                        new BooleanLoaderComponent(),
                        new GuidLoaderComponent(),
                        new ByteLoaderComponent()}
            };
        }
    }
}
