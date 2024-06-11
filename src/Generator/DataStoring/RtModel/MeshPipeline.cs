using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

public class MeshPipeline
{
    private readonly OctoObjectId _rtId;
    private readonly RtEntityDto _meshPipeline;
    private readonly MeshPipelineConfig _meshPipelineConfig;

    public MeshPipeline(OctoObjectId rtId, List<XmlElementData>? eqModelList)
    {
        _rtId = rtId;
        _meshPipelineConfig = new MeshPipelineConfig(eqModelList);
        _meshPipeline = CreateMeshPipeline();
    }
    
    public Task<RtEntityDto> GetMeshPipeline()
    {
        return Task.FromResult(_meshPipeline);
    }

    private RtEntityDto CreateMeshPipeline()
    {
        return new RtEntityDto
        {
           RtId = OctoObjectId.GenerateNewId(),
                CkTypeId = "System.Communication/MeshPipeline",
                Attributes = 
                [
                    new RtAttributeDto
                    {
                        Id = "System.Communication/PipelineDefinition",
                        Value = _meshPipelineConfig.GetMeshPipelineDefinition().Result  
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