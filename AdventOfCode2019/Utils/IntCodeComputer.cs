namespace AdventOfCode2019.Utils
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A computer capable of executing IntCode programs.
    /// </summary>
    public class IntCodeComputer
    {
        private readonly int[] _originalProgram;
        private int[] _registers;

        /// <summary>
        /// Constructs a new <see cref="IntCodeComputer"/> with a given program.
        /// </summary>
        /// <param name="program">Program to use in this <see cref="IntCodeComputer"/>.</param>
        public IntCodeComputer(int[] program)
        {
            _originalProgram = program;
            ResetProgram();
        }

        /// <summary>
        /// Gets the value of the first register.
        /// </summary>
        public int ZeroRegister => _registers[0];

        /// <summary>
        /// Gets the latest output from the program.
        /// </summary>
        public int Output { get; private set; }

        /// <summary>
        /// Resets the computer to its original program state.
        /// </summary>
        public void ResetProgram()
        {
            _registers = (int[])_originalProgram.Clone();
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
        /// Executes this <see cref="IntCodeComputer"/>s program.
        /// </summary>
        /// <param name="input">Optional input, for programs using <see cref="OpCode.Input"/>.</param>
        public void RunProgram(int[] input = null)
        {
            var instructionPointer = 0;
            var inputPointer = 0;

            while (true)
            {
                var opCodeString = _registers[instructionPointer].ToString("D5");
                var opCode = (OpCode)int.Parse(opCodeString.Substring(3,2));
                var parameterOneMode = (ParameterMode) int.Parse(opCodeString.Substring(2, 1));
                var parameterTwoMode = (ParameterMode)int.Parse(opCodeString.Substring(1, 1));
                var parameterThreeMode = (ParameterMode)int.Parse(opCodeString.Substring(0, 1));

                if (opCode == OpCode.Input)
                {
                    if (input == null)
                    {
                        throw new Exception("Something's very wrong.");
                    }

                    _registers[_registers[instructionPointer + 1]] = input[inputPointer++];
                    instructionPointer += 2;
                }
                else if (opCode == OpCode.Output)
                {
                    Output = _registers[_registers[instructionPointer + 1]];
                    instructionPointer += 2;
                }
                else if (opCode == OpCode.Halt)
                {
                    break;
                }
                else
                {
                    var targetRegister = instructionPointer + 3;

                    var firstValue = parameterOneMode == ParameterMode.PositionMode
                        ? _registers[_registers[instructionPointer + 1]]
                        : _registers[instructionPointer + 1];

                    var secondValue = parameterTwoMode == ParameterMode.PositionMode
                        ? _registers[_registers[instructionPointer + 2]]
                        : _registers[instructionPointer + 2];

                    switch (opCode)
                    {
                        case OpCode.Add:
                            _registers[_registers[targetRegister]] = firstValue + secondValue;
                            instructionPointer += 4;
                            break;
                        case OpCode.Multiply:
                            _registers[_registers[targetRegister]] = firstValue * secondValue;
                            instructionPointer += 4;
                            break;
                        case OpCode.JumpIfTrue:
                            if (firstValue != 0)
                            {
                                instructionPointer = secondValue;
                            }
                            else
                            {
                                instructionPointer += 3;
                            }
                            break;
                        case OpCode.JumpIfFalse:
                            if (firstValue == 0)
                            {
                                instructionPointer = secondValue;
                            }
                            else
                            {
                                instructionPointer += 3;
                            }
                            break;
                        case OpCode.LessThan:
                            if (firstValue < secondValue)
                            {
                                _registers[_registers[instructionPointer + 3]] = 1;
                            }
                            else
                            {
                                _registers[_registers[instructionPointer + 3]] = 0;
                            }

                            instructionPointer += 4;
                            break;
                        case OpCode.Equals:
                            if (firstValue == secondValue)
                            {
                                _registers[_registers[instructionPointer + 3]] = 1;
                            }
                            else
                            {
                                _registers[_registers[instructionPointer + 3]] = 0;
                            }

                            instructionPointer += 4;
                            break;
                    }
                }
            }
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
