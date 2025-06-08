# Big Man Computer - A Little Man Computer (LMC) Interpreter

![Language](https://img.shields.io/badge/language-VB.NET-blueviolet.svg)
![Platform](https://img.shields.io/badge/platform-Windows-0078D6.svg)
![Status](https://img.shields.io/badge/status-Completed-brightgreen.svg)

**Big Man Computer** is a feature-rich interpreter for the Little Man Computer (LMC) assembly language, developed as part of a computer science coursework project. It is designed to be a more intuitive and powerful educational tool than existing LMC simulators, helping students learn the fundamentals of low-level programming and the Von Neumann architecture.

The entire project, from conception and design to testing and evaluation, was developed in close consultation with a stakeholder to meet specific user needs.

## Key Features

This application builds upon the standard LMC model with several powerful enhancements:

- **Full LMC Interpreter:** Correctly interprets and executes any valid LMC program, including all standard opcodes and branching commands (`BRZ`, `BRP`, `BRA`).
- **CPU Visualizer:** An "Extended View" provides a real-time visualization of the Von Neumann architecture, showing how the RAM, Accumulator (ACC), Program Counter (PC), Current Instruction Register (CIR), Memory Address Register (MAR), and Memory Data Register (MDR) interact during execution.
- **Variable Execution Speed:** Users can run programs at full speed, at slower preset speeds, or use a "Step" button to execute the code line-by-line, making it easier to debug and understand the program flow.
- **Live Code Highlighting:** The current line of code being executed is highlighted in the editor, providing a clear visual trace of the program's progress.
- **Custom Commenting:** A unique feature that allows for inline comments using the `/` character, which is ignored by the interpreter.
- **Robust Error Handling:** The program provides clear, specific error messages to the user, identifying issues like syntax errors, missing `HLT` commands, undeclared variables, or overflow errors, often specifying the line number of the error.
- **Performance Optimization:** The interpreter uses static code analysis to pre-compile execution paths, reducing search times from O(n) to O(1) for repeated operations and loops, resulting in a significant performance increase.
- **Sample Programs:** Includes several pre-loaded LMC programs (e.g., Fibonacci sequence, Multiplication, Prime Number Finder) to help users learn the syntax and explore different algorithms.
- **Customizable UI:** The application's color scheme can be changed to suit user preference.

## Technologies Used

- **Language:** VB.NET
- **IDE:** Visual Studio 2017
- **Framework:** .NET Framework 4.5 (or higher)
- **Platform:** Windows

## System Requirements

### Minimum Hardware Requirements

- **Processor:** 1 GHz single-core CPU  
- **RAM:** 1 GB  
- **Storage:** 21 GB (for Windows) + 500 KB for the program  
- **Display:** 800x800 resolution monitor  

### Software Requirements

- **OS:** Windows 7 or higher (8, 8.1, 10)  
- **Framework:** .NET Framework 4.5 or higher  

## Installation and Usage

### Running the Executable (Recommended)

1. Go to the **Releases** section of this GitHub repository.
2. Download the latest `.zip` file.
3. Extract the files and run `BigManComputer.exe`.

### Running from Source Code

1. Clone this repository to your local machine.
2. Open the `.sln` file in Visual Studio 2017 or a newer version.
3. Ensure you have the .NET Framework 4.5 (or higher) development tools installed.
4. Build the solution (`Build > Build Solution`).
5. Run the project by pressing `F5` or the `Start` button.

## Code Overview

The program's logic is primarily contained within `Form1.vb`. The core procedures are:

- `Initialazation()`: The main entry point triggered by the "RUN" button. It reads the user's code into an array, validates it, and prepares the execution paths for the interpreter.
- `lexicalAnalisys()`: Parses each line of LMC code to identify the three key parts: the loop tag (if any), the command (opcode), and the operand (variable/data).
- `RamHandler()`: Manages the `RAMarray`, which stores declared variables and their values. It handles both populating the memory and searching for variables during execution.
- `Interpreter()`: The heart of the program. It contains a loop that executes the code line by line until a `HLT` or `COB` command is found. It uses a series of `If/ElseIf` statements to perform the action corresponding to the current command.
- `graphics()`: Updates the UI elements in the "Extended View" to reflect the current state of the accumulator, registers, and RAM after each step of execution.
- `Reset()`: A utility procedure that clears all variables, arrays, and UI fields, returning the program to its default state after a run is complete or an error occurs.


For a complete and in-depth understanding of the project's lifecycle, including problem analysis, iterative design, stakeholder feedback, testing logs, and evaluation, please see the full project report.


## License

This project is licensed under the MIT License. See the [LICENSE.md](LICENSE.md) file for details.
