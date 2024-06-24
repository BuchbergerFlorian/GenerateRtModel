using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;
using Meshmakers.Octo.Runtime.Contracts.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.Yaml
{
    public class YamlManager
    {
        private static IRtYamlSerializer? _rtYamlSerializer;

        public YamlManager()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddRuntimeEngine();
            var builder = serviceCollection.BuildServiceProvider();
            _rtYamlSerializer = builder.GetRequiredService<IRtYamlSerializer>();
        }
        
        public async Task GenerateYamlFile(RtModelRootDto root, string filePathYamlRTModel)
        {
            if(File.Exists(filePathYamlRTModel))
            {
                File.Delete(filePathYamlRTModel);
            }
            using var fileStream = File.OpenWrite(filePathYamlRTModel);
            using var streamWriter = new StreamWriter(fileStream);
            if (_rtYamlSerializer != null)
            {
                await _rtYamlSerializer.SerializeAsync(streamWriter, root);
            }
        }
    }
}

