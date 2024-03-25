using MeshMakers.GenerateRtModel.Generator.Console_Output;
using MeshMakers.GenerateRtModel.Generator.Data_Reading.Xml;
using MeshMakers.GenerateRtModel.Generator.Data_Storing;
using MeshMakers.GenerateRtModel.Generator.Data_Storing.RtModel;
using MeshMakers.GenerateRtModel.Generator.Data_Storing.Yaml;

namespace MeshMakers.GenerateRtModel.Generator
{
    public static class GenerateRtModel
    {
        private static void Main(string[] args)
        {
            var xmlManager = new XmlManager();
            
            //Process for reading and manage Zenon EQModel as xml data
            
            var rootElementInEqModelXml = xmlManager.GetRootElement(@"C:\dev\GenerateRtModel\src\Generator\Data_Reading\Xml\XML Files\EQModel_Basisproject.XML");
            var eqModelRepository = new EqModelRepository();
            
            if (rootElementInEqModelXml != null)
            {
                xmlManager.IterateOverEqModel(rootElementInEqModelXml, 0, eqModelRepository);
            }
            
            eqModelRepository.AssociateElementsFromChildToParent();
            eqModelRepository.GenerateCompleteGroupName();
            
            //Process for reading and manage Zenon Variables from xml data
            
            var rootElementInVariableXml = xmlManager.GetRootElement(@"C:\dev\GenerateRtModel\src\Generator\Data_Reading\Xml\XML Files\Variables_Basisproject.XML");
            VariableRepository variableRepository = new VariableRepository();
            
            if (rootElementInVariableXml != null)
            {
                xmlManager.IterateOverVariableModel(rootElementInVariableXml, variableRepository);
            }
            
            //Merge Variable Collection into eqModelRepository 
            var variables = variableRepository.GetList();
            eqModelRepository.MergeVariablesIntoEqModelRepository(variables);

            var eqModelList = eqModelRepository.GetElementList();
            new ConsoleOutput().WriteToConsole(eqModelList);
            
            
            //Generate RtModel
            var rtModelManager = new RtModelManager(eqModelList);
            var rtModel = rtModelManager.GetRtModel().Root;
            
            //Generate Yaml File
            var yamlManager = new YamlManager();
            yamlManager.GenerateYamlFile(rtModel);
        }
    }  
}

