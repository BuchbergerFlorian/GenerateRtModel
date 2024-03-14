using MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing;

public class EqModelRepository
{
    private readonly List<XmlElementData>? _elementList;
    
    public EqModelRepository()
    {
        _elementList = new List<XmlElementData>();
    }

    public void AddElementToList(XmlElementData element)
    {
        _elementList?.Add(element);
    }

    public List<XmlElementData>? GetElementList()
    {
        return _elementList;
    }
    
    public void AssociateElementsFromChildToParent()
    {
        if (_elementList == null || _elementList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _elementList.Count - 1; i++)
        {
            var currentElement = _elementList[i];
            var nextElement = _elementList[i + 1];

            if (currentElement.Depth < nextElement.Depth)
            {
                nextElement.TargetId = currentElement.Id;
              
            }
            else if (currentElement.Depth == nextElement.Depth)
            {
                nextElement.TargetId = currentElement.TargetId;
            }
            else if (currentElement.Depth > nextElement.Depth)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    var item = _elementList[j];
                    if (item.Depth < nextElement.Depth)
                    {
                        nextElement.TargetId = item.Id;
                        break;
                    }
                }
            }
            
            
        }
    }

    public void GenerateCompleteGroupName()
    {
        if (_elementList == null || _elementList.Count == 0)
        {
            return;
        }
        
        _elementList[0].EqGroupName = _elementList[0].ElementName;
        
        for (int i = 0; i < _elementList.Count - 1; i++)
        {
            var currentElement = _elementList[i];
            var nextElement = _elementList[i + 1];
            
            if (currentElement.Depth < nextElement.Depth)
            {
                nextElement.EqGroupName = currentElement.EqGroupName + "." + nextElement.ElementName;
            }
            else if (currentElement.Depth >= nextElement.Depth)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    var item = _elementList[j];
                    if (item.Depth < nextElement.Depth)
                    {
                        nextElement.EqGroupName = item.EqGroupName + "." + nextElement.ElementName;
                        break;
                    }
                }
            }
        }
    }

    public void MergeVariablesIntoEqModelRepository(List<StructForVariableRepository> variableCollection)
    {
        foreach (var variable in variableCollection)
        {
            foreach (var eqModel in variable.EqModels)
            {
                if (_elementList != null)
                    foreach (var element in _elementList)
                    {
                        if (element.EqGroupName == eqModel)
                        {
                            element.AddVariable(variable.Name, variable.Description);
                        }
                    }
            }
        }
    }
}