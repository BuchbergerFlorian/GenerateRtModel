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
        CreateApplyChangesNode();
        CreateSaveInTimeSeriesNode();
    }

    private void CreateSaveInTimeSeriesNode()
    {
        var createTimeSeriesNode = new SaveInTimeSeriesNodeConfiguration()
        {
            Description = "Save in time series database",
        };

        _meshPipelineDefinition.Transformations!.Add(createTimeSeriesNode);
    }

    private void CreateApplyChangesNode()
    {
        var applyChangesNode = new ApplyChangesNodeConfiguration
        {
            Description = "Apply changes to RtEntity",
            TargetPropertyName = "_UpdateItems"
        };

        _meshPipelineDefinition.Transformations!.Add(applyChangesNode);
    }

    private void CreateUpdateNodes(List<XmlElementData>? eqModelList)
    {
        if (eqModelList != null)
        {
            foreach (var element in eqModelList)
            {
                if (element.Variables.Count == 0)
                {
                    continue;
                }

                var node = CreateUpdateNode(element.Id, element.CkTypeId);


                foreach (var variable in element.Variables)
                {
                    node.AttributeUpdates!.Add(new AttributeUpdateConfiguration
                    {
                        AttributeName = variable.Description,
                        ValuePath = $"$.['{variable.Name}']",
                        AttributeValueType = AttributeValueTypesDto.Double
                    });
                }
                
                _meshPipelineDefinition.Transformations!.Add(node);
            }
        }
    }

    private CreateUpdateInfoNodeConfiguration CreateUpdateNode(OctoObjectId targetId, string ckTypeId)
    {
        return new CreateUpdateInfoNodeConfiguration
        {
            RtId = targetId,
            CkTypeId = ckTypeId,
            TargetPropertyName = "_UpdateItems",
            AttributeUpdates = [],
        };
    }

    private async Task<string> SerializeMeshPipelineValue()
    {
        PipelineSerializer pipelineSerializer = new PipelineSerializer();
        var serializer = pipelineSerializer.PipelineConfigurationSerializer;

        var meshPipelineDefinitionString = await serializer.SerializeAsync(_meshPipelineDefinition);
        return meshPipelineDefinitionString;
    }
}