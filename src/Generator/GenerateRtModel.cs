using MeshMakers.GenerateRtModel.Logic.Generator.Console_Output;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.Yaml;

namespace MeshMakers.GenerateRtModel.Logic.Generator
{
    public static class GenerateRtModel
    {
        private static void Main(string[] args)
        {
            var xmlManager = new XmlManager();
            
            //Process for reading and manage Zenon EQModel as xml data
            //and convert it to a linear relationship database in a Yaml file
            string programPart = "eqModel";
            
            var rootElementInEqModelXml = xmlManager.GetRootElement(programPart);
            var eqModelRepository = new EqModelRepository();
            
            if (rootElementInEqModelXml != null)
            {
                xmlManager.IterateOverEqModel(rootElementInEqModelXml, 0, eqModelRepository);
            }
            
            eqModelRepository.AssociateElementsFromChildToParent();
            eqModelRepository.GenerateCompleteGroupName();
            
            //Process for reading and manage Zenon Variables from xml data
            programPart = "variableModel";
            
            var rootElementInVariableXml = xmlManager.GetRootElement(programPart);
            VariableRepository variableRepository = new VariableRepository();
            
            if (rootElementInVariableXml != null)
            {
                xmlManager.IterateOverVariableModel(rootElementInVariableXml, variableRepository);
            }
            
            //Merge Variable Collection into eqModelRepository 
            var variableCollection = variableRepository.GetList();
            eqModelRepository.MergeVariablesIntoEqModelRepository(variableCollection);

            var eqModelList = eqModelRepository.GetElementList();
            new ConsoleOutput().WriteToConsole(eqModelList);
            
            
            //Generate RtModel
            var rtModelManager = new RtModelManager();
            rtModelManager.CreateModel(eqModelList);
            var rtModel = rtModelManager.GetRtModel().Root;
            
            //Generate Yaml File
            var yamlManager = new YamlManager();
            yamlManager.GenerateYamlFile(rtModel);
        }
    }  
}

