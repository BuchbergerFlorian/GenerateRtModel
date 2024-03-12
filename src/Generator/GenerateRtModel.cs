using MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.Yaml;

namespace MeshMakers.GenerateRtModel.Logic.Generator
{
    public static class GenerateRtModel
    {
        private static void Main(string[] args)
        {
            var xmlManager = new XmlManager();
            var yamlManager = new YamlManager();
            
            //Process for reading and manage Zenon EQModel as xml data
            //and convert it to a linear relationship database in a Yaml file
            string programPart = "eqModel";
            var rootElementInEqModelXml = xmlManager.GetRootElement(programPart);
            var rtModel = new RtModel();
            
            if (rootElementInEqModelXml != null)
            {
                xmlManager.IterateOverEqModel(rootElementInEqModelXml, 0, rtModel);
                yamlManager.GenerateYamlFile(rtModel.GetModelRoot());
            }
            
            //Process for reading and manage Zenon Variables as xml data
            //and convert it to a List in a Yaml file
            programPart = "variableModel";
            var rootElementInVariableXml = xmlManager.GetRootElement(programPart);
            if (rootElementInVariableXml != null)
            {
                VariableRepository variableList = new VariableRepository();
                xmlManager.IterateOverVariableModel(rootElementInVariableXml, variableList);

                var variableCollection = variableList.GetList();
                
                foreach (var variable in variableCollection)
                {
                    Console.WriteLine($"Name: {variable.Name}, Description: {variable.Description}");
                    foreach (var eqModel in variable.EqModel)
                    {
                        Console.WriteLine($"EQ-Model: {eqModel}");
                    }
                }
                
                // var yamlManager = new YamlManager();
                // yamlManager.GenerateYamlFile(variableCollection);
            }
            






        }
    }  
}

