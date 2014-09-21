using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public class NameGenerator
    {
        #region "Member variables"

        private string _PropertyName;

        #endregion

        #region "Properties"

        public string PropertyName { get; private set; }
        public List<int> IndexValues { get; private set; }
        public string ControlName { get; private set; }
        public string ControlID { get; private set; }

        #endregion

        public NameGenerator(string propertyName, List<int> indexValues)
        {
            this.PropertyName = propertyName;
            this.IndexValues = indexValues;
            _PropertyName = this.PropertyName;
            foreach(int item in indexValues)
            {
                _PropertyName = _PropertyName.Replace("[]", string.Format("[{0}]", item.ToString()));
            }
            this.ControlName = _PropertyName;
            this.ControlID = this.ControlName.Replace("[", "_").Replace("]", "_").Replace(".","_");
        }
        public NameGenerator(string propertyName)
        {
            this.PropertyName = propertyName;
            this.ControlName = propertyName;
            this.ControlID = this.ControlName.Replace("[", "_").Replace("]", "_").Replace(".", "_");
        }
    }    
}
