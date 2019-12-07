namespace AdventOfCode2019.Utils
{
    using System.Collections.Generic;

    /// <summary>
    /// A computer capable of executing IntCode programs.
    /// </summary>
    public class IntCodeComputer
    {
        private readonly int[] _originalProgram;
        private readonly List<int> _input;
        private readonly List<int> _output;
        private int[] _registers;
        private int _instructionPointer;
        private int _inputPointer;

        /// <summary>
        /// Constructs a new <see cref="IntCodeComputer"/> with a given program.
        /// </summary>
        /// <param name="program">Program to use in this <see cref="IntCodeComputer"/>.</param>
        public IntCodeComputer(int[] program)
        {
            _originalProgram = program;
            _input = new List<int>();
            _output = new List<int>();
            _instructionPointer = 0;
            _inputPointer = 0;
            HasHalted = false;

            ResetProgram();
        }

        /// <summary>
        /// Gets the value of the first register.
        /// </summary>
        public int ZeroRegister => _registers[0];

        /// <summary>
        /// Whether or not this <see cref="IntCodeComputer"/> is waiting for new input.
        /// </summary>
        public bool WaitingForInput { get; private set; }

        /// <summary>
        /// Whether or not this <see cref="IntCodeComputer"/> has output ready to read.
        /// </summary>
        public bool HasOutput => _output.Count > 0;

        /// <summary>
        /// Whether or not this <see cref="IntCodeComputer"/> has halted its program or not.
        /// </summary>
        public bool HasHalted { get; private set; }

        /// <summary>
        /// Resets the computer to its original program state.
        /// </summary>
        public void ResetProgram()
        {
            _registers = (int[])_originalProgram.Clone();
            _output.Clear();
            _input.Clear();
            WaitingForInput = false;
            HasHalted = false;
            _inputPointer = 0;
            _instructionPointer = 0;
        }

        /// <summary>
        /// Sets the noun and verb values of this <see cref="IntCodeComputer"/>.
        /// </summary>
        /// <param name="noun">Value of the noun to set.</param>
        /// <param name="verb">Value of the verb to set.</param>
        public void SetNounAndVerb(int noun, int verb)
        {
            _registers[1] = noun;
            _registers[2] = verb;
        }

        /// <summary>
        /// Adds input to the input queue. Resets <see cref="WaitingForInput"/> property.
        /// </summary>
        /// <param name="input">Input to add.</param>
        public void AddInput(int input)
        {
            _input.Add(input);
            WaitingForInput = _input.Count == 0;
        }

        /// <summary>
        /// Adds input to the input queue. Resets <see cref="WaitingForInput"/> property.
        /// </summary>
        /// <param name="input">Input to add.</param>
        public void AddInput(IEnumerable<int> input)
        {
            _input.AddRange(input);
            WaitingForInput = _input.Count == _inputPointer-1;
        }

        public List<int> GetOutput()
        {
            var output = new List<int>(_output);
            _output.Clear();
            return output;
        }

        public void ContinueProgram(int[] input = null)
        {
            if (input != null)
            {
                AddInput(input);
            }

            while (true)
            {
                var opCodeString = _registers[_instructionPointer].ToString("D5");
                var opCode = (OpCode)int.Parse(opCodeString.Substring(3, 2));
                var parameterOneMode = (ParameterMode)int.Parse(opCodeString.Substring(2, 1));
                var parameterTwoMode = (ParameterMode)int.Parse(opCodeString.Substring(1, 1));
                var parameterThreeMode = (ParameterMode)int.Parse(opCodeString.Substring(0, 1));

                if (opCode == OpCode.Input)
                {
                    if (_inputPointer > _input.Count - 1)
                    {
                        WaitingForInput = true;
                        break;
                    }

                    _registers[_registers[_instructionPointer + 1]] = _input[_inputPointer];
                    _instructionPointer += 2;
                    _inputPointer++;
                }
                else if (opCode == OpCode.Output)
                {
                    _output.Add(_registers[_registers[_instructionPointer + 1]]);
                    _instructionPointer += 2;
                }
                else if (opCode == OpCode.Halt)
                {
                    HasHalted = true;
                    break;
                }
                else
                {
                    var targetRegister = _instructionPointer + 3;

                    var firstValue = parameterOneMode == ParameterMode.PositionMode
                        ? _registers[_registers[_instructionPointer + 1]]
                        : _registers[_instructionPointer + 1];

                    var secondValue = parameterTwoMode == ParameterMode.PositionMode
                        ? _registers[_registers[_instructionPointer + 2]]
                        : _registers[_instructionPointer + 2];

                    switch (opCode)
                    {
                        case OpCode.Add:
                            _registers[_registers[targetRegister]] = firstValue + secondValue;
                            _instructionPointer += 4;
                            break;
                        case OpCode.Multiply:
                            _registers[_registers[targetRegister]] = firstValue * secondValue;
                            _instructionPointer += 4;
                            break;
                        case OpCode.JumpIfTrue:
                            if (firstValue != 0)
                            {
                                _instructionPointer = secondValue;
                            }
                            else
                            {
                                _instructionPointer += 3;
                            }
                            break;
                        case OpCode.JumpIfFalse:
                            if (firstValue == 0)
                            {
                                _instructionPointer = secondValue;
                            }
                            else
                            {
                                _instructionPointer += 3;
                            }
                            break;
                        case OpCode.LessThan:
                            if (firstValue < secondValue)
                            {
                                _registers[_registers[_instructionPointer + 3]] = 1;
                            }
                            else
                            {
                                _registers[_registers[_instructionPointer + 3]] = 0;
                            }

                            _instructionPointer += 4;
                            break;
                        case OpCode.Equals:
                            if (firstValue == secondValue)
                            {
                                _registers[_registers[_instructionPointer + 3]] = 1;
                            }
                            else
                            {
                                _registers[_registers[_instructionPointer + 3]] = 0;
                            }

                            _instructionPointer += 4;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Executes this <see cref="IntCodeComputer"/>s program.
        /// </summary>
        /// <param name="input">Optional input, for programs using <see cref="OpCode.Input"/>.</param>
        public void RunProgram(int[] input = null)
        {
            _instructionPointer = 0;
            _inputPointer = 0;
            _input.Clear();

            ContinueProgram(input);
        }
    }

    /// <summary>
    /// Represents the mode of a parameter.
    /// </summary>
    public enum ParameterMode
    {
        /// <summary>
        /// The parameter references another register.
        /// </summary>
        PositionMode = 0,
        
        /// <summary>
        /// The parameter references the value.
        /// </summary>
        ImmediateMode = 1
    }
    
    /// <summary>
    /// Operational codes for the <see cref="IntCodeComputer"/>.
    /// </summary>
    public enum OpCode
    {
        /// <summary>
        /// Adds together numbers read from two positions and stores the result in a third position.
        /// </summary>
        Add = 1,

        /// <summary>
        /// Multiples together numbers read from two positions and stores the result in a third position.
        /// </summary>
        Multiply = 2,

        /// <summary>
        /// Takes a single integer as input and saves it to the position given by its only parameter.
        /// </summary>
        Input = 3,

        /// <summary>
        /// Outputs the value of its only parameter.
        /// </summary>
        Output = 4,

        /// <summary>
        /// If the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter.
        /// Otherwise it does nothing.
        /// </summary>
        JumpIfTrue = 5,

        /// <summary>
        /// If the first parameter is zero, it sets the instruction pointer to the value from the second parameter.
        /// Otherwise, it does nothing.
        /// </summary>
        JumpIfFalse = 6,

        /// <summary>
        /// If the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter.
        /// Otherwise, it stores 0.
        /// </summary>
        LessThan = 7,

        /// <summary>
        /// If the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.
        /// Otherwise, it stores 0.
        /// </summary>
        Equals = 8,
        
        /// <summary>
        /// Halts execution of the program.
        /// </summary>
        Halt = 99
    }
}
