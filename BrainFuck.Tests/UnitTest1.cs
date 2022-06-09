using Moq;
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

        [Theory]
        [InlineData(33)]
        [InlineData(123)]
        [InlineData(155)]

        public void NextCharValueTest(int num)
        {
            //arrange
            var repository = new Repository();
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

            var expectedCurrent = num;

            //act
            for (var i = 0; i < num; i++)
            {
                dataOperations.NextCharValue();
            }
            var actual = repository.Memory[repository.Current];

            //assert
            Assert.Equal(expectedCurrent, actual);
        }

        [Theory]
        [InlineData(33)]
        [InlineData(123)]
        [InlineData(155)]
        public void NextCharValueMockTest(int num)
        {
            //arrange
            var mockIRepository = new Mock<IRepository>();
            mockIRepository.SetupProperty(x => x.Memory, new char[30000]);
            mockIRepository.SetupProperty(x => x.Current, 0);
            var mockIInputOutput = new Mock<IInputOutput>();

            var dataOperations = new DataOperations(mockIRepository.Object, mockIInputOutput.Object);

            var expectedCurrent = num;

            //act
            for (var i = 0; i < num; i++)
            {
                dataOperations.NextCharValue();
            }
            var actual = mockIRepository.Object.Memory[mockIRepository.Object.Current];

            //assert
            Assert.Equal(expectedCurrent, actual);
        }

        [Theory]
        [InlineData('"')]
        [InlineData(')')]
        [InlineData('!')]
        public void PreviousCharValueTest(char newChar)
        {
            //arrange
            var repository = new Repository();
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

            var expectedCurrent = newChar - 1;

            //act

            repository.Memory[repository.Current] = newChar;
            dataOperations.PreviousCharValue();
            var actual = repository.Memory[repository.Current];

            //assert

            Assert.Equal(expectedCurrent, actual);
        }

        [Theory]
        [InlineData(69)]
        [InlineData(123)]
        public void NextCellTest(int newCurrent)
        {
            //arrange
            var repository = new Repository();
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

            var expectedCurrent = newCurrent + 1;
            repository.Current = newCurrent;

            //act
            dataOperations.NextCell();
            var actual = repository.Current;

            //assert
            Assert.Equal(expectedCurrent, actual);

        }

        [Theory]
        [InlineData(70)]
        [InlineData(120)]
        [InlineData(1)]
        public void PreviousCellTest(int newCurrent)
        {
            //arrange
            var repository = new Repository();
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

            var expectedCurrent = newCurrent - 1;
            repository.Current = newCurrent;

            //act
            dataOperations.PreviusCell();
            var actual = repository.Current;

            repository.Current = newCurrent;
            dataOperations.PreviusCell();
            var actual2 = repository.Current;

            //assert
            Assert.Equal(expectedCurrent, actual);
        }

        [Fact]
        public void IfZeroNextTest()
        {
            //arrange
            var repository = new Repository();
            repository.Program = "++++[>++++++++++<-]>.";
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

            var expectedCurrent1 = 18;

            //act
            var actual1 = dataOperations.IfZeroNext(5, repository.Program);

            //assert
            Assert.Equal(expectedCurrent1, actual1);
        }

        [Theory]
        [InlineData("++++[>++++++++++<-]>.", 18)]
        [InlineData("++[>++++<-]>.", 10)]

        public void IfZeroNextTestTheory(string program, int expectedNum)
        {
            //arrange
            var repository = new Repository();
            repository.Program = program;
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

            var expectedCurrent = expectedNum;

            //act
            int actual = 0;
            for (int i = 0; i < repository.Program.Length; i++)
            {
                if (repository.Program[i] == '[')
                {
                    actual = dataOperations.IfZeroNext(i, repository.Program);
                }
            }

            //assert
            Assert.Equal(expectedCurrent, actual);
        }

        [Fact]
        public void IfNoZeroBackTest()
        {
            //arrange
            var repository = new Repository();
            repository.Program = "++++[>++++++++++<-]>.";
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

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

        [Theory]
        [InlineData("++++[>++++++++++<-]>.", 4)]
        [InlineData("++[>++++<-]>.", 2)]
        public void IfNoZeroBackTestTheory(string program, int expectedNum)
        {
            //arrange
            var repository = new Repository();
            repository.Program = program;
            var mockIInputOutput = new Mock<IInputOutput>();
            var dataOperations = new DataOperations(repository, mockIInputOutput.Object);

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
            Assert.Equal(expectedNum, actual1);
        }

        [Fact]
        public void DisplayCellValueTest()
        {
            //arrange
            var mockTextWrite = new Mock<TextWriter>();
            var called = false;

            mockTextWrite.Setup(x => x.Write("H")).Callback(() => called = true);

            var repository = new Repository();
            var testTextReader = new TestTextReader();
            var testTextWriter = new TestTextWriter();

            var inputOutput = new InputOutput(testTextReader, mockTextWrite.Object);
            var dataOperations = new DataOperations(repository, inputOutput);

            repository.Memory[0] = 'H';


            //act
            dataOperations.DisplayCellValue();


            //assert
            Assert.True(called);


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
            var mockIInputOutput = new Mock<InputOutput>(testTextReader, testTextWriter);
            var dataOperationsTest = new DataOperationsTest(repository, mockIInputOutput.Object);

            var expectedValue1 = nameof(dataOperationsTest.NextCharValue);
            var expectedValue2 = nameof(dataOperationsTest.PreviousCharValue);
            var expectedValue3 = nameof(dataOperationsTest.DisplayCellValue);
            var expectedValue4 = nameof(dataOperationsTest.NextCell);
            var expectedValue5 = nameof(dataOperationsTest.PreviusCell);
            var expectedValue6 = nameof(dataOperationsTest.InputValueInCell);
            var expectedValue7 = nameof(dataOperationsTest.IfZeroNext);
            var expectedValue8 = nameof(dataOperationsTest.IfNoZeroBack);

            //act
            repository.Program = "+";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual1 = dataOperationsTest.FuncName;

            repository.Program = "-";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual2 = dataOperationsTest.FuncName;

            repository.Program = ".";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual3 = dataOperationsTest.FuncName;

            repository.Program = ">";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual4 = dataOperationsTest.FuncName;

            repository.Program = "<";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual5 = dataOperationsTest.FuncName;

            repository.Program = ",";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual6 = dataOperationsTest.FuncName;

            repository.Program = "[";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual7 = dataOperationsTest.FuncName;

            repository.Program = "]";
            dataOperationsTest.Enum—odeBrainFuck(repository.Program);
            var actual8 = dataOperationsTest.FuncName;

            //assert
            Assert.Equal(expectedValue1, actual1);
            Assert.Equal(expectedValue2, actual2);
            Assert.Equal(expectedValue3, actual3);
            Assert.Equal(expectedValue4, actual4);
            Assert.Equal(expectedValue5, actual5);
            Assert.Equal(expectedValue6, actual6);
            Assert.Equal(expectedValue7, actual7);
            Assert.Equal(expectedValue8, actual8);
        }


        public class DataOperationsTest : DataOperations
        {
            private Repository _brainFuckCode;
            private InputOutput _inputOutput;

            public bool Execution { get; set; }
            public string FuncName { get; set; }

            public DataOperationsTest(Repository brainFuckCode, InputOutput inputOutput) : base(brainFuckCode, inputOutput)
            {
                _brainFuckCode = brainFuckCode;
                _inputOutput = inputOutput;
            }

            public override void NextCharValue()
            {
                Execution = true;
                FuncName = nameof(NextCharValue);
            }

            public override void PreviousCharValue()
            {
                Execution = true;
                FuncName = nameof(PreviousCharValue);
            }

            public override void DisplayCellValue()
            {
                Execution = true;
                FuncName = nameof(DisplayCellValue);
            }
            public override void NextCell()
            {
                Execution = true;
                FuncName = nameof(NextCell);
            }

            public override void PreviusCell()
            {
                Execution = true;
                FuncName = nameof(PreviusCell);
            }

            public override void InputValueInCell()
            {
                Execution = true;
                FuncName = nameof(InputValueInCell);
            }

            public override int IfZeroNext(int i, string s)
            {
                FuncName = nameof(IfZeroNext);
                Execution = true;
                return 0;
            }

            public override int IfNoZeroBack(int i, string s)
            {
                FuncName = nameof(IfNoZeroBack);
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