using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OneAutomationFramework.Drivers.Selenium
{
    public interface ISeleniumConfiguration
    {
        Browser Browser { get; }
        string[]? Arguments { get; }
        float? DefaultTimeout { get; }
        bool? Headless { get; }
        bool? privateMode { get; }
        string? EnvUrl { get; }
    }

    public class SeleniumConfiguration : ISeleniumConfiguration
    {
        private class SpecFlowActionJson
        {
            [JsonInclude]
            public SeleniumSpecFlowJsonPart Selenium { get; private set; } = new SeleniumSpecFlowJsonPart();
        }

        private class SeleniumSpecFlowJsonPart
        {
            [JsonInclude]
            public Browser Browser { get; private set; }

            [JsonInclude]
            public string[]? Arguments { get; private set; }

            [JsonInclude]
            public float? DefaultTimeout { get; private set; }

            [JsonInclude]
            public bool? Headless { get; private set; }

            [JsonInclude]
            public bool? privateMode { get; private set; }

            [JsonInclude]
            public string? EnvUrl { get; private set; }

        }

        private readonly Lazy<SpecFlowActionJson> _specflowJsonPart;

        private SpecFlowActionJson LoadSpecFlowJson()
        {
            var json = LoadJson();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new SpecFlowActionJson();
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var specflowActionConfig = System.Text.Json.JsonSerializer.Deserialize<SpecFlowActionJson>(json, jsonSerializerOptions);

            return specflowActionConfig ?? new SpecFlowActionJson();
        }

        public SeleniumConfiguration()
        {
            _specflowJsonPart = new Lazy<SpecFlowActionJson>(LoadSpecFlowJson);
        }
        public Browser Browser => _specflowJsonPart.Value.Selenium.Browser;
        public string[]? Arguments => _specflowJsonPart.Value.Selenium.Arguments;
        public float? DefaultTimeout => _specflowJsonPart.Value.Selenium.DefaultTimeout;
        public bool? Headless => _specflowJsonPart.Value.Selenium.Headless;
        public bool? privateMode => _specflowJsonPart.Value.Selenium.privateMode;
        public string? EnvUrl => _specflowJsonPart.Value.Selenium.EnvUrl;


        public dynamic LoadJson()
        {
            var specFlowJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeleniumConfig.json");
            var content = File.ReadAllText(specFlowJsonFilePath);
            return content;

        }
    }

}