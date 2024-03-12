﻿using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;
using Meshmakers.Octo.Runtime.Contracts.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.Yaml
{
    public class YamlManager
    {
        private static IRtYamlSerializer _rtYamlSerializer;

        public YamlManager()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddRuntimeEngine();
            var builder = serviceCollection.BuildServiceProvider();
            _rtYamlSerializer = builder.GetRequiredService<IRtYamlSerializer>();
        }
        
        public async void GenerateYamlFile(RtModelRootDto root)
        {
            await using var fileStream = File.OpenWrite("C:\\dev\\GenerateRtModel\\Generator\\Data_Storing\\Yaml\\Yaml File\\EQModel.yaml");
            await using var streamWriter = new StreamWriter(fileStream);
            await _rtYamlSerializer.SerializeAsync(streamWriter, root);
        }
    }
}

