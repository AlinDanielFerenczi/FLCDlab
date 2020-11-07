using System;
using System.Linq;
using System.Collections.Generic;

namespace labproject
{
    class FiniteAutomata
    {
        public string initialState { get; set; }
        public string[] finalStates { get; set; }
        public Transition[] transitions { get; set; }
        public Dictionary<string, State> states { get; set; }

        public bool IsDeterministic()
        {
            return !(
                transitions.Any(transition => string.IsNullOrEmpty(transition.label.ToString())) || 
                transitions.Any(transition => transition.nextStates.Count() > 1)
            );
        }

        public bool CheckMatch(string input)
        {

            try
            {
                Transition(input, initialState);
                return true;
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Transition(string input, string currentState)
        {
            var state = states[currentState];
            var matched = false;

            if (input.Count() == 0 && finalStates.Contains(currentState))
                return;

            transitions.Where(transition => transition.startState == currentState).ToList().ForEach(transition =>
            {
                var nextStates = transition.IsMatch(input.First());

                if (nextStates.Count() > 0)
                    matched = true;

                foreach(var nextState in nextStates)
                    Transition(new string(input.Skip(1).ToArray()), nextState);
            });

            if (!matched && input.Count() != 0)
                throw new Exception("Not accepted");
            if (input.Count() == 0 && !finalStates.Contains(currentState))
                throw new Exception("Not accepted");
        }
    }
}
