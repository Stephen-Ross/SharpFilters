// Copyright © Stephen Ross 2016

using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace SharpFilters.Tests.TestsCommon
{
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoMoqDataAttribute())
        {
        }
    }
}