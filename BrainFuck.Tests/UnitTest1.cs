using System.IO;
using System.Text;
using Xunit;

namespace BrainFuck.Tests
{
    //AAA Rule
    //arrange
    //act
    //assert

    public class Tests
    {
        [Fact]
        public void NextCharValueTest()
        {
            //arrange
            var repository = new Repository();
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            var num = 33;
            var expectedCurrent1 = num;
            var expectedCurrent2 = num;
            char newCurrent = (char)(num - 1);

            //act
            for (var i = 0; i < num; i++)
            {
                dataOperations.NextCharValue();
            }
            var actual1 = repository.Memory[repository.Current];

            repository.Memory[repository.Current] = newCurrent;
            dataOperations.NextCharValue();
            var actual2 = repository.Memory[repository.Current];

            //assert
            Assert.Equal(expectedCurrent1, actual1);
            Assert.Equal(expectedCurrent2, actual2);
        }

        [Fact]
        public void PreviousCharValueTest()
        {
            //arrange
            var repository = new Repository();
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            var expectedCurrent1 = '\0';

            var expectedCurrent2 = '!';
            char newCurrent2 = (char)34;

            //act
            dataOperations.PreviousCharValue();
            var actual1 = repository.Memory[repository.Current];

            repository.Memory[repository.Current] = newCurrent2;
            dataOperations.PreviousCharValue();
            var actual2 = repository.Memory[repository.Current];

            //assert
            Assert.Equal(expectedCurrent1, actual1);
            Assert.Equal(expectedCurrent2, actual2);
        }

        [Fact]
        public void NextCellTest()
        {
            //arrange
            var repository = new Repository();
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            var expectedCurrent = repository.Current + 1;
            var newCurrent = 69;
            var expectedCurrent2 = 70;

            //act
            dataOperations.NextCell();
            var actual1 = repository.Current;

            repository.Current = newCurrent;
            dataOperations.NextCell();
            var actual2 = repository.Current;

            //assert
            Assert.Equal(expectedCurrent, actual1);
            Assert.Equal(expectedCurrent2, actual2);
        }

        [Fact]
        public void PreviousCellTest()
        {
            //arrange
            var repository = new Repository();
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            var expectedCurrent = repository.Current;
            var newCurrent = 70;
            var expectedCurrent2 = 69;

            //act
            dataOperations.PreviusCell();
            var actual1 = repository.Current;

            repository.Current = newCurrent;
            dataOperations.PreviusCell();
            var actual2 = repository.Current;

            //assert
            Assert.Equal(expectedCurrent, actual1);
            Assert.Equal(expectedCurrent2, actual2);
        }

        [Fact]
        public void IfZeroNextTest()
        {
            //arrange
            var repository = new Repository();
            repository.Program = "++++[>++++++++++<-]>.";
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            var expectedCurrent1 = 18;

            //act
            int actual1 = 0;
            for (int i = 0; i < repository.Program.Length; i++)
            {
                if (repository.Program[i] == '[')
                {
                    actual1 = dataOperations.IfZeroNext(i, repository.Program);
                }
            }

            //assert
            Assert.Equal(expectedCurrent1, actual1);
        }

        [Fact]
        public void IfNoZeroBackTest()
        {
            //arrange
            var repository = new Repository();
            repository.Program = "++++[>++++++++++<-]>.";
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            var expectedCurrent1 = 4;

            //act
            int actual1 = 0;
            repository.Memory[0]++;
            for (int i = 0; i < repository.Program.Length; i++)
            {
                if (repository.Program[i] == ']')
                {
                    actual1 = dataOperations.IfNoZeroBack(i, repository.Program);
                }
            }
            //assert
            Assert.Equal(expectedCurrent1, actual1);
        }

        [Fact]
        public void DisplayCellValueTest()
        {
            //arrange
            var repository = new Repository();
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            repository.Memory[0] = 'H';
            var expectedValue1 = "H";

            //act
            dataOperations.DisplayCellValue();
            var actual1 = testTextWriter.OutputMem;

            //assert
            Assert.Equal(expectedValue1, actual1);


        }

        [Fact]
        public void InputValueInCellTests()
        {
            //arrange
            var repository = new Repository();
            var testTextReader = new TestTextReader("L");
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperations = new DataOperations(repository, inputOutput);

            repository.Memory[0] = 'H';
            var expectedValue1 = "L";

            //act

            dataOperations.InputValueInCell();
            var actual1 = testTextReader.InputMem;
            var actual2 = repository.Memory[0];

            //assert
            Assert.Equal(expectedValue1, actual1);
            Assert.Equal(expectedValue1[0], actual2);
        }

        [Fact]
        public void Enum—odeBrainFuckTests() 
        {
            //arrange
            var repository = new Repository();
            
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, testTextWriter);
            var dataOperationsTest = new DataOperationsTest(repository, inputOutput);

            var expectedValue = true;
            //act
            repository.Program = "+";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual1 = dataOperationsTest.Execution;

            repository.Program = "-";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual2 = dataOperationsTest.Execution;

            repository.Program = ".";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual3 = dataOperationsTest.Execution;

            repository.Program = ">";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual4 = dataOperationsTest.Execution;

            repository.Program = "<";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual5 = dataOperationsTest.Execution;

            repository.Program = ",";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual6 = dataOperationsTest.Execution;

            repository.Program = "[";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual7 = dataOperationsTest.Execution;

            repository.Program = "]";
            dataOperationsTest.Execution = false;
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual8 = dataOperationsTest.Execution;
            //assert
            Assert.Equal(expectedValue, actual1);
            Assert.Equal(expectedValue, actual2);
            Assert.Equal(expectedValue, actual3);
            Assert.Equal(expectedValue, actual4);
            Assert.Equal(expectedValue, actual5);
            Assert.Equal(expectedValue, actual6);
            Assert.Equal(expectedValue, actual7);
            Assert.Equal(expectedValue, actual8);
        }


        public class DataOperationsTest : DataOperations
        {
            private Repository _brainFuckCode;
            private InputOutput _inputOutput;

            public bool Execution { get; set; }

            public DataOperationsTest(Repository brainFuckCode, InputOutput inputOutput) : base(brainFuckCode, inputOutput)
            {
                _brainFuckCode = brainFuckCode;
                _inputOutput = inputOutput;
            }

            public override void NextCharValue() 
            {
                Execution = true;
            }

            public override void PreviousCharValue()
            {
                Execution = true;
            }

            public override void DisplayCellValue()
            {
                Execution = true;
            }
            public override void NextCell()
            {
                Execution = true;
            }

            public override void PreviusCell()
            {
                Execution = true;
            }

            public override void InputValueInCell()
            {
                Execution = true;
            }

            public override int IfZeroNext(int i, string s)
            {
                Execution = true;
                return 0;
            }

            public override int IfNoZeroBack(int i, string s)
            {
                Execution = true;
                return 0;
            }

        }
        public class TestTextReader : TextReader
        {
            private string _input;

            public string InputMem => _input;

            public TestTextReader(string input) 
            {
                _input = input;
            }
            public TestTextReader() 
            {
            }

            public override string ReadLine() 
            {
                return _input;
            }
        }

        public class TestTextWriter : TextWriter
        {
            private string _output;

            public string OutputMem => _output;


            public TestTextWriter(string output)
            {
                _output = output;
            }

            public TestTextWriter() 
            {
            }

            public override Encoding Encoding => Encoding.UTF8;

            public override void Write(string output)
            {
                _output = output;
            }
        }
    }
}