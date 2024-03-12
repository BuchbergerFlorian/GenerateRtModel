namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel
{
    public class VariableRepository
    {
        private readonly List<StructForVariableRepository> _listOfVariables;

        public VariableRepository()
        {
            _listOfVariables = new List<StructForVariableRepository>();
        }
        
        public void AddVariableToList(string name, string description, string[] eqModel)
        {
            StructForVariableRepository newData;
            newData.Name = name;
            newData.Description = description;
            newData.EqModel = eqModel;
            
            _listOfVariables.Add(newData);
        }

        public List<StructForVariableRepository> GetList()
        {
            return _listOfVariables;
        }
        
        public int GetLengthOfList()
        {
            return _listOfVariables.Count;
        }

    }
}


