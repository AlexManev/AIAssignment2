using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Assignment2
{
    class TruthTablePostFix
    {
        private Stack<char> _input;
        private Stack<char> _operators;
        private Stack<char> _output;

        public TruthTablePostFix(string input)
        {
            _input = new Stack<char>();
            _operators = new Stack<char>();
            _output = new Stack<char>();

            char[] splitInput = input.ToCharArray();

            for (int i = splitInput.Length - 1; i >= 0; i--)
            {
                if (splitInput[i] != ' ')
                    _input.Push(Convert.ToChar(splitInput[i]));
            }

            char tempChar = '0';

            while (_input.Any())
            {
                tempChar = _input.Pop();

                if (IsOperator(tempChar))
                    AddOperator(tempChar);
                else
                    _output.Push(tempChar);
            }

            while (_operators.Any())
                _output.Push(_operators.Pop());
        }

        public static bool IsOperator(char input)
        {
            switch (input)
            {
                case '¬':
                case '&':
                case '|':
                case '>':
                case '=':
                    return true;
                default:
                    return false;
            }
        }

        public bool HigherPrecedence(char input)
        {
            if (Precedence(input) > Precedence(_operators.Peek()))
                return true;
            else
                return false;
        }

        public int Precedence(char input)
        {
            switch (input)
            {
                case '¬':
                    return 5;
                case '&':
                    return 4;
                case '|':
                    return 3;
                case '>':
                    return 2;
                case '=':
                    return 1;
                default:
                    return 0;
            }
        }
        public void AddOperator(char input)
        {

            if (_operators.Any() && !HigherPrecedence(input))
            {
                if (_operators.Peek() != '(')
                    _output.Push(_operators.Pop());
            }

            if (input == ')')
            {
                while (_operators.Any() && _operators.Peek() != '(')
                {
                    _output.Push(_operators.Pop());
                }

                _operators.Pop();

                return;
            }

            _operators.Push(input);
        }

        public char Solve()
        {
            while (_output.Count != 0)
            {
                _input.Push(_output.Pop());
            }

            _input.Reverse();
            _output.Clear();

            while (_input.Count > 0)
            {
                if (!IsOperator(_input.Peek()))
                    _output.Push(_input.Pop());
                else
                    _operators.Push(_input.Pop());

                if (_operators.Any())
                {
                    char rightInput = _output.Pop();
                    char leftInput = _output.Pop();
                    char result = 'F';

                    switch (_operators.Pop())
                    {
                        case '&':
                            if (leftInput == 'T' && rightInput == 'T')
                                result = 'T';
                            else
                                result = 'F';
                            break;
                        case '>':
                            if (leftInput == 'T' && rightInput == 'F')
                                result = 'F';
                            else
                                result = 'T';
                            break;
                        default:
                            break;
                    }
                    _input.Push(result);
                }
            }
            return _output.Pop();
        }
    }
}