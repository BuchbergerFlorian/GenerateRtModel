using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;


namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class DataPipeline
{
    private readonly OctoObjectId _rtId;
    private RtEntityDto[] _dataPipeline = new RtEntityDto[3];
    private readonly RtEntityDto? _edgePipeline;
    private readonly List<XmlElementData> _eqModelList;
    
    public DataPipeline(List<XmlElementData> eqModelList)
    {
        _rtId = OctoObjectId.GenerateNewId();
        _eqModelList = eqModelList;
        _edgePipeline = new EdgePipeline(_rtId).GetEdgePipeline();
    }

    public RtEntityDto[] GetDataPipeline()
    {
        return _dataPipeline;
    }

    public async Task CreateDataPipeline()
    {
        var meshPipeline = new MeshPipeline(_rtId, _eqModelList);
        await meshPipeline.CreateMeshPipeline();
        var meshPipelineEntity = await meshPipeline.GetMeshPipeline();
        
        _dataPipeline =
        [
            new RtEntityDto
            {
                RtId = _rtId,
                CkTypeId = "System.Communication/DataPipeline",
                Attributes =
                [
                    new RtAttributeDto
                    {
                        Id = "System/Name",
                        Value = "Zenon Pipeline"
                    }
                ]
            },
            _edgePipeline,
            meshPipelineEntity
        ];
    }
}