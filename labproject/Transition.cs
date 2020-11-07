using Newtonsoft.Json;
using System.Text;

namespace labproject
{
    class Transition
    {
        public string startState { get; set; }
        public string[] nextStates { get; set; }
        public char label { get; set; }

        public Transition(string startState, string[] nextStates, char label)
        {
            this.startState = startState;
            this.nextStates = nextStates;
            this.label = label;
        }

        public string[] IsMatch(char input)
        {
            if (!label.Equals(input))
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
