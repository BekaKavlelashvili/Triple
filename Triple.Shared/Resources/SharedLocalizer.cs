using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Triple.Shared.Resources
{
    public class SharedLocalizer : ISharedLocalizer
    {
        private IStringLocalizer _localizer;
        public SharedLocalizer(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("ApplicationStrings", "Triple.Shared");
        }

        LocalizedString ISharedLocalizer.this[string key] { get => _localizer[key]; }
    }

    public interface ISharedLocalizer
    {
        LocalizedString this[string key] { get; }
    }
}
