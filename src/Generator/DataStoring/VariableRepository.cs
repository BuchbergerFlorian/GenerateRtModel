using System.Text.Json.Nodes;
using Hangfire.Common;
using Newtonsoft.Json.Linq;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring
{
    public class VariableRepository
    {
        private readonly List<StructForVariableRepository> _listOfVariables;

        public VariableRepository()
        {
            _listOfVariables = new List<StructForVariableRepository>();
        }

        public void AddVariableToList(string name, string description, string[] eqModels)
        {
            StructForVariableRepository newData;
            newData.Name = name;
            newData.EqModels = eqModels;

            if (!string.IsNullOrEmpty(description))
            {
                JObject data;
                try
                {
                    data = JObject.Parse(description);
                    var targetAttributeName = data.Value<string>("AttributeName");
                    if (targetAttributeName != null)
                    {
                        newData.Description = targetAttributeName;
                        _listOfVariables.Add(newData);
                    }
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }

        public List<StructForVariableRepository> GetList()
        {
            return _listOfVariables;
        }

        public int GetLengthOfList()
        {
            return _listOfVariables.Count;
        }

        public override string? ToString()
        {
            foreach (var variable in _listOfVariables)
            {
                Console.WriteLine();
                Console.WriteLine($"Name: {variable.Name}, Description: {variable.Description}");
                foreach (var eqModel in variable.EqModels)
                {
                    Console.WriteLine($"EQ-Model: {eqModel}");
                }
            }

            return null;
        }
    }
}