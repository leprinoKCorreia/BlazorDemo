namespace BlazorBPAR.Services
{
    public class InputDataService
    {
        private Dictionary<string, object> inputValues = new Dictionary<string, object>();

        public event Action OnChange;

        public void SetValue(string key, object value)
        {
            if(inputValues.ContainsKey(key))
            {
                if (inputValues[key] != value)
                {
                    inputValues[key] = value;
                    NotifyDataChanged();
                }
            } else
            {
                inputValues.Add(key, value);
                NotifyDataChanged();
            }
        }

        public object GetValue(string key)
        {
            if (inputValues.ContainsKey(key))
            {
                return inputValues[key];
            }
            return null;
        }

        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}
