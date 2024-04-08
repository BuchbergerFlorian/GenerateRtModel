using System.Text.Json;
using System.Text.Json.Serialization;
using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.ConstructionKit.Contracts.DataTransferObjects;
using Meshmakers.Octo.MeshNodes.Nodes;
using Meshmakers.Octo.Sdk.Common.EtlDataPipeline.Configuration;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class MeshPipelineConfig
{
    private Task<string> _meshPipelineDefinitionString;
    private readonly PipelineConfigurationRoot _meshPipelineDefinition;

    public MeshPipelineConfig(List<XmlElementData>? eqModelList)
    {
        _meshPipelineDefinitionString = Task.FromResult(string.Empty);
        _meshPipelineDefinition = new PipelineConfigurationRoot();
        SetMeshPipelineDefinition(eqModelList);
    }
    
    public Task<string> GetMeshPipelineDefinition()
    {
        _meshPipelineDefinitionString = SerializeVariables();
        return _meshPipelineDefinitionString;
    }

    private void SetMeshPipelineDefinition(List<XmlElementData>? eqModelList)
    {
        _meshPipelineDefinition.Transformations = new List<NodeConfiguration>()
        { 
            new RetrieveFromMessageNodeConfiguration
            { 
                Description = "Retrieve from distributed event hub message"
            }
        };

        CreateUpdateNodes(eqModelList);
    }

    private void CreateUpdateNodes(List<XmlElementData>? eqModelList)
    {
        if (eqModelList != null)
        {
            foreach (var element in eqModelList)
            { 
                foreach (var variable in element.Variables)
                {
                    string varName = variable.Name;
                    string varDescription = variable.Description;
                    CreateUpdateNode(varName, varDescription);
                }
            } 
        }
    }
    
    private void CreateUpdateNode(string varName, string varDescription)
    {
        var updateInfoNode = new CreateUpdateInfoNodeConfiguration
        {
            RtId = OctoObjectId.GenerateNewId(), //REPLACE
            CkTypeId = "Bla/Bla",
            TargetPropertyName = "_UpdateItems",
            AttributeUpdates = new List<AttributeUpdateConfiguration>
            {
                new AttributeUpdateConfiguration
                {
                    AttributeName = varDescription, //REPLACE
                    AttributeValueType = AttributeValueTypesDto.Double,
                    ValuePath = varName
                }
            }
        };
            
        _meshPipelineDefinition.Transformations.Add(updateInfoNode);
    }

    // private Task<string> SerializeMeshPipelineValue()
    // {
    //     PipelineSerializer pipelineSerializer = new PipelineSerializer();
    //     var serializer = pipelineSerializer.PipelineConfigurationSerializer;
    //     
    //     var meshPipelineDefinitionString = serializer.SerializeAsync(_meshPipelineDefinition);
    //     return meshPipelineDefinitionString;
    // }
    
    private Task<string> SerializeVariables()
    {
        var serializeVariables = JsonSerializer.Serialize(_meshPipelineDefinition, new JsonSerializerOptions
        { 
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        
        return Task.FromResult(serializeVariables);
    }
}