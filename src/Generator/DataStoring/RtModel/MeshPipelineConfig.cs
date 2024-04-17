using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.ConstructionKit.Contracts.DataTransferObjects;
using Meshmakers.Octo.MeshNodes.Nodes;
using Meshmakers.Octo.Sdk.Common.EtlDataPipeline.Configuration;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class MeshPipelineConfig
{
    private string _meshPipelineDefinitionString;
    private readonly PipelineConfigurationRoot _meshPipelineDefinition;

    public MeshPipelineConfig(List<XmlElementData>? eqModelList)
    {
        _meshPipelineDefinitionString = string.Empty;
        _meshPipelineDefinition = new PipelineConfigurationRoot();
        SetMeshPipelineDefinition(eqModelList);
    }
    
    public async Task<string> GetMeshPipelineDefinition()
    {
        _meshPipelineDefinitionString = await SerializeMeshPipelineValue();
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
                    var targetId = element.Id;
                    string ckTypeId = element.CkTypeId;
                    string varName = variable.Name;
                    string varDescription = variable.Description;
                    CreateUpdateNode(targetId, ckTypeId,varName, varDescription);
                }
            } 
        }
    }
    
    private void CreateUpdateNode(OctoObjectId targetId,string ckTypeId, string varName, string varDescription)
    {
        var updateInfoNode = new CreateUpdateInfoNodeConfiguration
        {
            RtId = targetId, 
            CkTypeId = ckTypeId,
            TargetPropertyName = "_UpdateItems",
            AttributeUpdates = new List<AttributeUpdateConfiguration>
            {
                new AttributeUpdateConfiguration
                {
                    AttributeName = varDescription, 
                    AttributeValueType = AttributeValueTypesDto.Double,
                    ValuePath = varName
                }
            }
        };
            
        _meshPipelineDefinition.Transformations?.Add(updateInfoNode);
    }

    private async Task<string> SerializeMeshPipelineValue()
    {
        PipelineSerializer pipelineSerializer = new PipelineSerializer();
        var serializer = pipelineSerializer.PipelineConfigurationSerializer;
        
        var meshPipelineDefinitionString = await serializer.SerializeAsync(_meshPipelineDefinition);
        return meshPipelineDefinitionString;
    }
}