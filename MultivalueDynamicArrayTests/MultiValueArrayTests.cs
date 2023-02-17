using MultivalueDynamicArray;
using NUnit.Framework;

namespace MultivalueDynamicArrayTests
{
    [TestFixture]
    public class MultiValueArrayTests
    {
        private string _d3Data = "AAAA\\BBBB\\CCCC]BBBB]CCCC^BBBB";
        private MultiValueDynamicArray _dynamicArray;

        [SetUp]
        public void Setup()
        {
            _dynamicArray = new MultiValueDynamicArray();
        }

        [Test]
        public void should_return_dymanicArray_from_a_string()
        {
            _dynamicArray = _d3Data;
            Assert.AreEqual(_dynamicArray[1, 1, 1], "AAAA");
            Assert.AreEqual(_dynamicArray[1, 1, 2], "BBBB");
            Assert.AreEqual(_dynamicArray[1, 1, 3], "CCCC");
            Assert.AreEqual(_dynamicArray[1, 1], "AAAAüBBBBüCCCC");
            Assert.AreEqual(_dynamicArray[1, 2], "BBBB");
            Assert.AreEqual(_dynamicArray[1, 3], "CCCC");
            Assert.AreEqual(_dynamicArray[2,1], "BBBB");
        }
        
        [Test]
        public void should_return_dynamicArray_from_a_string_with_subvalues()
        {
            _dynamicArray = "AAAA\\BBBB\\CCCC]BBBB]CCCC^BBBB^^Test]Test2]Test3]Test4";
            
            Assert.AreEqual(_dynamicArray[1, 1, 1], "AAAA");
            Assert.AreEqual(_dynamicArray[1, 1, 2], "BBBB");
            Assert.AreEqual(_dynamicArray[1, 1, 3], "CCCC");
            Assert.AreEqual(_dynamicArray[1, 1], "AAAAüBBBBüCCCC");
            Assert.AreEqual(_dynamicArray[1, 2], "BBBB");
            Assert.AreEqual(_dynamicArray[1, 3], "CCCC");
            Assert.AreEqual(_dynamicArray[2,1], "BBBB");
            Assert.AreEqual(_dynamicArray[4,1], "Test");
            Assert.AreEqual(_dynamicArray[4,2], "Test2");
            Assert.AreEqual(_dynamicArray[4,3], "Test3");
            Assert.AreEqual(_dynamicArray[4,4], "Test4");
        }

        [Test]
        public void should_return_correctly_formatted_string()
        {
            _dynamicArray = _d3Data;
            string test = _dynamicArray;
            Assert.AreEqual(_dynamicArray.ToString(), _d3Data);
            Assert.AreEqual(test, _d3Data);
        }

        [Test]
        public void should_handle_attributes_correctly()
        {
            _dynamicArray = _d3Data;
            _dynamicArray[4] = "Test";
            Assert.AreEqual(_dynamicArray[4], "Test");
        }

        [Test]
        public void should_handle_values_correctly()
        {
            _dynamicArray = _d3Data;
            _dynamicArray[4, 1] = "Test";
            _dynamicArray[4, 2] = "Test2";
            _dynamicArray[4, 3] = "Test3";

            Assert.AreEqual(_dynamicArray[4, 1], "Test");
            Assert.AreEqual(_dynamicArray[4, 2], "Test2");
            Assert.AreEqual(_dynamicArray[4, 3], "Test3");
        }

        [Test]
        public void should_handle_subvalues_correctly()
        {
            _dynamicArray = _d3Data;
            _dynamicArray[4] = "Test";
            _dynamicArray[4, 1, 1] = "Test2";
            _dynamicArray[4, 2, 2] = "Test3";

            Assert.AreEqual(_dynamicArray[4, 1, 1], "Test2");
            Assert.AreEqual(_dynamicArray[4, 2, 2], "Test3");
        }

        [Test]
        public void should_create_a_new_matrix_correctly()
        {
            var matrix = new MultiValueDynamicArray
            {
                [1] = "Test",
                [1, 1] = "Test2",
                [1, 2] = "Test32",
                [1, 2, 1] = "Test4",
                [1, 2, 2] = "Test5",
            };

            for (var i = 1; i <= 5; i++)
            {
                matrix[2, 1, i] = "Test" + i;
                matrix[2, 2, i] = "another test" + i;
                matrix[2, 3, i] = "subtest " + i;
            }


            Assert.AreEqual(matrix[1, 2, 1], "Test4");
            Assert.AreEqual(matrix[1, 2, 2], "Test5");
            Assert.AreEqual(matrix[2, 1, 1], "Test1");
            Assert.AreEqual(matrix[2, 1, 2], "Test2");
            Assert.AreEqual(matrix[2, 1, 3], "Test3");
            Assert.AreEqual(matrix[2, 1, 4], "Test4");
            Assert.AreEqual(matrix[2, 1, 5], "Test5");
            Assert.AreEqual(matrix[2, 2, 1], "another test1");
            Assert.AreEqual(matrix[2, 2, 2], "another test2");
            Assert.AreEqual(matrix[2, 2, 3], "another test3");
            Assert.AreEqual(matrix[2, 2, 4], "another test4");
            Assert.AreEqual(matrix[2, 2, 5], "another test5");
            Assert.AreEqual(matrix[2, 3, 1], "subtest 1");
            Assert.AreEqual(matrix[2, 3, 2], "subtest 2");
            Assert.AreEqual(matrix[2, 3, 3], "subtest 3");
            Assert.AreEqual(matrix[2, 3, 4], "subtest 4");
            Assert.AreEqual(matrix[2, 3, 5], "subtest 5");
        }

        [Test]
        public void should_be_able_to_resize()
        {
            var matrix = new MultiValueDynamicArray
            {
                [1] = "Test",
                [2] = "field 2",
                [3] = "field 3",
            };

            for (var i = 1; i <= 5; i++)
            {
                matrix[9, 1, i] = "Test" + i;
                matrix[9, 2, i] = "another test" + i;
                matrix[9, 3, i] = "subtest " + i;
            }

            Assert.That(matrix[9, 1, 1], Is.EqualTo("Test1"));
            Assert.That(matrix[9, 1, 2], Is.EqualTo("Test2"));
            Assert.That(matrix[9, 1, 3], Is.EqualTo("Test3"));
            Assert.That(matrix[9, 1, 4], Is.EqualTo("Test4"));
            Assert.That(matrix[9, 1, 5], Is.EqualTo("Test5"));
            Assert.That(matrix[9, 2, 1], Is.EqualTo("another test1"));
            Assert.That(matrix[9, 2, 2], Is.EqualTo("another test2"));
            Assert.That(matrix[9, 2, 3], Is.EqualTo("another test3"));
            Assert.That(matrix[9, 2, 4], Is.EqualTo("another test4"));
            Assert.That(matrix[9, 2, 5], Is.EqualTo("another test5"));
            Assert.That(matrix[9, 3, 1], Is.EqualTo("subtest 1"));
            Assert.That(matrix[9, 3, 2], Is.EqualTo("subtest 2"));
            Assert.That(matrix[9, 3, 3], Is.EqualTo("subtest 3"));
            Assert.That(matrix[9, 3, 4], Is.EqualTo("subtest 4"));
            Assert.That(matrix[9, 3, 5], Is.EqualTo("subtest 5"));
        }

        [Test]
        public void should_return_correct_number_of_attributes()
        {
            var matrix = new MultiValueDynamicArray
            {
                [1] = "Test",
                [1, 1] = "Test2",
                [1, 2] = "Test32",
                [1, 2, 1] = "Test4",
                [1, 2, 2] = "Test5",
            };
            
            _dynamicArray = _d3Data;

            Assert.That(_dynamicArray.GetAttributeCount(), Is.EqualTo(2));
            Assert.That(matrix.GetAttributeCount(), Is.EqualTo(1));
        }

        [Test]
        public void should_return_correct_number_of_values()
        {
            var matrix = new MultiValueDynamicArray
            {
                [1, 1] = "Test2",
                [1, 2] = "Test32",
                [1, 2, 1] = "Test4",
                [1, 2, 2] = "Test5",
            };
            
            Assert.That(matrix.GetValuesCount(1), Is.EqualTo(2));
        }
        
        
        [Test]
        public void should_return_correct_number_of_subvalues()
        {
            var matrix = new MultiValueDynamicArray
            {
                [1] = "Test",
                [1, 1] = "Test2",
                [1, 2] = "Test32",
                [1, 2, 1] = "Test4",
                [1, 2, 2] = "Test5",
            };
            
            Assert.That(matrix.GetSubValuesCount(1, 2), Is.EqualTo(2));
        }
    }
}