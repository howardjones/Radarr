using FluentAssertions;
using NUnit.Framework;
using NzbDrone.Core.Indexers.Torznab;
using NzbDrone.Core.Test.Framework;

namespace NzbDrone.Core.Test.IndexerTests.TorznabTests
{
    public class TorznabSettingFixture : CoreTest
    {
        [TestCase("http://localhost:9117/", "/api")]
        public void url_and_api_not_jackett_all(string baseUrl, string apiPath)
        {
            var setting = new TorznabSettings()
            {
                BaseUrl = baseUrl,
                ApiPath = apiPath
            };

            setting.Validate().IsValid.Should().BeTrue();
        }

        [TestCase("http://localhost:9117/torznab/all/api")]
        [TestCase("http://localhost:9117/api/v2.0/indexers/all/results/torznab")]
        public void jackett_all_url_should_not_validate(string baseUrl)
        {
            var setting = new TorznabSettings()
            {
                BaseUrl = baseUrl
            };

            setting.Validate().IsValid.Should().BeFalse();
        }

        [TestCase("/torznab/all/api")]
        [TestCase("/api/v2.0/indexers/all/results/torznab")]
        public void jackett_all_api_should_not_validate(string apiPath)
        {
            var setting = new TorznabSettings()
            {
                BaseUrl = "http://localhost:9117/",
                ApiPath = apiPath
            };

            setting.Validate().IsValid.Should().BeFalse();
        }
    }
}
