using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class RtModelManager
{
    private readonly RtModelTemplate _rtModelTemplate;

    public RtModelManager(List<XmlElementData>? eqModelList)
    {
        _rtModelTemplate = new RtModelTemplate();
        CreateModelWithTemplate(eqModelList);
    }

    private void CreateModelWithTemplate(List<XmlElementData>? eqModelList)
    {
        if (eqModelList == null) return;
        foreach (var element in eqModelList)
        {
            if (element.ElementType == "Model")
                _rtModelTemplate.AddObjectToRoot(element.ElementName, element.Id);
            else
            {
                if (element.CkTypeId != "")
                {
                    _rtModelTemplate.AddObjectToRoot(element.ElementName, element.Id, element.TargetId, element.TargetCkType, element.CkTypeId);
                }
            }
        }

        _rtModelTemplate.AddEdgeAdapter(eqModelList);
        _rtModelTemplate.AddDataPipeLine(eqModelList);
    }
    
    public RtModelTemplate GetRtModel() => _rtModelTemplate;
}