using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;


namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class DataPipeline
{
    private readonly OctoObjectId _rtId;
    private RtEntityDto[] _dataPipeline = new RtEntityDto[3];
    private readonly RtEntityDto? _edgePipeline;
    private readonly RtEntityDto? _meshPipeline;
    
    public DataPipeline(List<XmlElementData> eqModelList)
    {
        _rtId = OctoObjectId.GenerateNewId();
        _edgePipeline = new EdgePipeline(_rtId).GetEdgePipeline();
        _meshPipeline = new MeshPipeline(_rtId, eqModelList).GetMeshPipeline();
        SetDataPipeline();
    }

    public RtEntityDto[] GetDataPipeline()
    {
        return _dataPipeline;
    }

    private void SetDataPipeline()
    {
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
            _meshPipeline
        ];
    }
}