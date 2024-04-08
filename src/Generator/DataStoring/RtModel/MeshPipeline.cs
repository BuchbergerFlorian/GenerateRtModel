using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

public class MeshPipeline
{
    private readonly OctoObjectId _rtId;
    private RtEntityDto? _meshPipeline;
    private readonly MeshPipelineConfig _meshPipelineConfig;

    public MeshPipeline(OctoObjectId rtId, List<XmlElementData>? eqModelList)
    {
        _rtId = rtId;
        _meshPipelineConfig = new MeshPipelineConfig(eqModelList);
        SetMeshPipeline();
    }
    
    public RtEntityDto? GetMeshPipeline()
    {
        return _meshPipeline;
    }

    private void SetMeshPipeline()
    {
        _meshPipeline = new RtEntityDto
        {
           RtId = OctoObjectId.GenerateNewId(),
                CkTypeId = "System.Communication/MeshPipeline",
                Attributes = 
                [
                    new RtAttributeDto
                    {
                        Id = "System.Communication/PipelineDefinition",
                        Value = _meshPipelineConfig.GetMeshPipelineDefinition()
                    }
                ],
                Associations = 
                [
                    new()
                    {
                        RoleId = "System/ParentChild",
                        TargetRtId = _rtId,
                        TargetCkTypeId = "System.Communication/DataPipeline",
                    }
                ]
        };
    }

    
}