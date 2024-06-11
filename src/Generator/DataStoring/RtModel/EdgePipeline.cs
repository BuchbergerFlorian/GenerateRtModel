using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class EdgePipeline
{
    private readonly OctoObjectId _rtId;
    private readonly RtEntityDto _edgePipeline;

    public EdgePipeline(OctoObjectId rtId)
    {
        _rtId = rtId;
        _edgePipeline = SetEdgePipeline();
    }
    
    public RtEntityDto GetEdgePipeline()
    {
        return _edgePipeline;
    }

    private RtEntityDto SetEdgePipeline()
    {
        return new RtEntityDto
        {
            RtId = OctoObjectId.GenerateNewId(),
            CkTypeId = "System.Communication/EdgePipeline",
            Attributes =
            [
                new RtAttributeDto
                {
                    Id = "System.Communication/PipelineDefinition",
                    Value = CreateEdgePipelineValue()
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

    private string CreateEdgePipelineValue()
    {
        const string value = "transformations: \n  - type: PublishToDistributionEventHub@1\n    description: Load to event hub";
        return value;
    }
}