﻿using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using MeshMakers.GenerateRtModel.Generator.DataStoring;
using MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;
using MeshMakers.GenerateRtModel.Generator.DataStoring.Yaml;

namespace MeshMakers.GenerateRtModel.Generator;

public class CreateRtModel
{
    private readonly string _filepathFromZenonXmlEqModel;
    private readonly string _filepathFromXmlZenonVariable;
    private XmlManager _xmlManager;
    public CreateRtModel(string filepathFromZenonXmlEqModel, string filepathFromXmlZenonVariable)
    {
        _filepathFromZenonXmlEqModel = filepathFromZenonXmlEqModel;
        _filepathFromXmlZenonVariable = filepathFromXmlZenonVariable;
        _xmlManager = new XmlManager();
    }

    public void Create()
    {
        //Process for reading and manage Zenon EQModel as xml data
        var rootElementInEqModelXml = _xmlManager.GetRootElement(_filepathFromZenonXmlEqModel);
        var eqModelRepository = new EqModelRepository();
            
        if (rootElementInEqModelXml != null)
        {
            _xmlManager.IterateOverEqModel(rootElementInEqModelXml, 0, eqModelRepository);
        }
            
        eqModelRepository.AssociateElementsFromChildToParent();
        eqModelRepository.GenerateCompleteGroupName();
            
        //Process for reading and manage Zenon Variables from xml data
            
        var rootElementInVariableXml = _xmlManager.GetRootElement(_filepathFromXmlZenonVariable);
        VariableRepository variableRepository = new VariableRepository();
            
        if (rootElementInVariableXml != null)
        {
            _xmlManager.IterateOverVariableModel(rootElementInVariableXml, variableRepository);
        }
            
        //Merge Variable Collection into eqModelRepository 
        var variables = variableRepository.GetList();
        eqModelRepository.MergeVariablesIntoEqModelRepository(variables);

        var eqModelList = eqModelRepository.GetElementList();
        ConsoleOutput.WriteToConsole(eqModelList);
            
            
        //Generate RtModel
        var rtModelManager = new RtModelManager(eqModelList);
        var rtModel = rtModelManager.GetRtModel().Root;
            
        //Generate Yaml File
        var yamlManager = new YamlManager();
        yamlManager.GenerateYamlFile(rtModel);
    }
}