using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class RtModelManager
{
    private readonly RtModelTemplate _rtModelTemplate;

    public RtModelManager()
    {
        _rtModelTemplate = new RtModelTemplate();
    }

    public async Task CreateModelWithTemplate(List<XmlElementData>? eqModelList)
    {
        if (eqModelList == null) return;
        foreach (var element in eqModelList)
        {
            if (element.ElementType == "Model")
            {
                _rtModelTemplate.AddObjectToRoot(element.ElementName, element.Id);
            }
            else if (element.CkTypeId != "")
            {
                _rtModelTemplate.AddObjectToRoot(element.ElementName, element.Id, element.TargetId,
                    element.TargetCkType, element.CkTypeId, element.AdditionalAttributes);
            }
        }

        _rtModelTemplate.AddEdgeAdapter(eqModelList);
        await _rtModelTemplate.AddDataPipeLine(eqModelList);
    }

    public RtModelTemplate GetRtModel()
    {
        _rtModelTemplate.Root.Dependencies =
            _rtModelTemplate.Root.Entities.Select(x => x.CkTypeId.ModelId).Distinct().ToList();
        return _rtModelTemplate;
    }
}