// using Microsoft.Extensions.DependencyInjection;
// using Meshmakers.Octo.MeshNodes.Configuration;
// using Meshmakers.Octo.Sdk.Common.EtlDataPipeline.Configuration.Serializer;
//
// namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;
//
// public class PipelineSerializer
// {
//     public IPipelineConfigurationSerializer PipelineConfigurationSerializer { get; }
//
//     public PipelineSerializer()
//     {
//         var serviceCollection = new ServiceCollection();
//         serviceCollection.AddDataPipelineSerializer()
//             .AddMeshDataPipelineNodes()
//             .RegisterNodeConfiguration<VariableNodeConfiguration>();
//         
//         var builder = serviceCollection.BuildServiceProvider();
//         
//         PipelineConfigurationSerializer = builder.GetRequiredService<IPipelineConfigurationSerializer>();
//     }
// }