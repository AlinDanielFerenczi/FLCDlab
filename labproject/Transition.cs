using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace labproject
{
    class Transition
    {
        public string startState { get; set; }
        public string[] nextStates { get; set; }
        public string label { get; set; }

        public Transition(string startState, string[] nextStates, string label)
        {
            this.startState = startState;
            this.nextStates = nextStates;
            this.label = label;
        }

        public string[] IsMatch(char input)
        {
            //if (!label.Equals(input))

            if (!Regex.IsMatch(input.ToString(), label))
                return new string[0];
            return nextStates;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(startState)
                .Append(",")
                .Append(label)
                .Append(")")
                .Append("=>")
                .Append(JsonConvert.SerializeObject(nextStates))
                .ToString();
        }
    }
}
