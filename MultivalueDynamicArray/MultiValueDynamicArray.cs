using System.Globalization;

namespace MultivalueDynamicArray
{
    /// <summary>
    /// Defines a class for Multivalue Dynamic Array.
    /// </summary>
    public class MultiValueDynamicArray
        {
            /// <summary>
            /// The unformatted matrix.
            /// </summary>
            private string _arrayString;
    
            private const char Am = (char) 254;
            private const char Vm = (char) 253;
            private const char Sv = (char) 252;
    
            #region private methods:
    
            private string GetValue(int am)
            {
                var values = _arrayString.Split(Am);
    
                if (values.Length < am)
                    return string.Empty;
    
                return values[am - 1];
            }
    
            private string GetValue(int am, int vm)
            {
                var values = GetValue(am).Split(Vm);
                
                return values.Length < vm ? string.Empty : values[vm - 1];
            }
    
            private string GetValue(int am, int vm, int sv)
            {
                var values = GetValue(am, vm).Split(Sv);
    
                return values.Length < sv ? string.Empty : values[sv - 1];
            }
    
            /// <summary>
            /// Sets the value of the matrix
            /// </summary>
            /// <param name="attribute">Attribute number</param>
            /// <param name="value">value</param>
            private void SetValue(int attribute, string value)
            {
                var values = _arrayString.Split(Am);
                if (values.Length < attribute)
                {
                    var str = new string[attribute];
                    values.CopyTo(str, 0);
                    for (var i = values.Length; i < attribute; i++)
                    {
                        str[i] = string.Empty;
                    }
    
                    values = str;
                }
    
                values[attribute - 1] = value;
                _arrayString = string.Join(Am.ToString(CultureInfo.InvariantCulture), values);
            }
    
            private void SetValue(int attribute, int vm, string value)
            {
                var values = _arrayString.Split(Am);
                if (values.Length < attribute)
                {
                    var str = new string[attribute];
                    values.CopyTo(str, 0);
                    for (var i = values.Length; i < attribute; i++)
                    {
                        str[i] = string.Empty;
                    }
    
                    values = str;
                }

                var vmValues = values[attribute - 1].Split(Vm);
    
                vmValues = SetVm(vmValues, vm, value);
    
                values[attribute - 1] = string.Join(Vm.ToString(CultureInfo.CurrentCulture), vmValues);
                _arrayString = string.Join(Am.ToString(CultureInfo.InvariantCulture), values);
            }
    
            private void SetValue(int attribute, int vm, int sVm, string value)
            {
                var values = _arrayString.Split(Am);
    
                if (values.Length < attribute)
                {
                    var str = new string[attribute];
                    values.CopyTo(str, 0);
                    for (var i = values.Length; i < attribute; i++)
                    {
                        str[i] = string.Empty;
                    }
    
                    values = str;
                }
                
                var vmValues = values[attribute - 1].Split(Vm);
                vmValues = SetVm(vmValues, vm);
                
                var subValues = vmValues[vm - 1].Split(Sv);
                subValues = SetVm(subValues, sVm, value);
    
                vmValues[vm - 1] = string.Join(Sv.ToString(CultureInfo.CurrentCulture), subValues);
    
                values[attribute - 1] = string.Join(Vm.ToString(CultureInfo.CurrentCulture), vmValues);
                _arrayString = string.Join(Am.ToString(CultureInfo.InvariantCulture), values);
                
            }
    
            private static string[] SetVm(string[] vmValues, int vm)
            {
                if (vmValues.Length < vm)
                {
                    var str = new string[vm];
                    vmValues.CopyTo(str, 0);
                    for (var i = vmValues.Length; i < vm; i++)
                    {
                        str[i] = string.Empty;
                    }
    
                    vmValues = str;
                }
    
                return vmValues;
            }

            private static string[] SetVm(string[] vmValues, int vm, string value)
            {
                vmValues = SetVm(vmValues, vm);
                vmValues[vm - 1] = value;
                return vmValues;
            }
            #endregion
    
            #region public constructors
    
            /// <summary>
            /// Initializes a new instance of the <see cref="MultiValueDynamicArray"/> class.
            /// </summary>
            public MultiValueDynamicArray()
            {
                _arrayString = string.Empty;
            }
    
            /// <summary>
            /// /// Initializes a new instance of the <see cref="MultiValueDynamicArray"/> class.
            /// </summary>
            /// <param name="value"></param>
            public MultiValueDynamicArray(string value) : this()
            {
                _arrayString = ConvertFromD3Array(value);
            }
    
            #endregion
    
            #region overriden methods:
    
            /// <summary>
            /// Returns the string that is inside the matrix with all the values and subvalues chars set.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return ConvertToD3Array(_arrayString);
            }
    
            private static string ToString(MultiValueDynamicArray matrix)
            {
                return matrix.ToString();
            }
    
            #endregion
    
            #region Helper Methods:
    
            private static string ConvertToD3Array(string array)
            {
                array = array.Replace((char) Am, '^');
                array = array.Replace((char) Vm, ']');
                array = array.Replace((char) Sv, '\\');
                return array;
            }
    
            private static string ConvertFromD3Array(string array)
            {
                array = array.Replace('^', Am);
                array = array.Replace(']', Vm);
                array = array.Replace('\\', Sv);
    
                return array;
            }
    
            #endregion
    
            #region overloaded operators:
    
            /// <summary>
            /// Implicit operator for string assignments.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static implicit operator string(MultiValueDynamicArray value)
            {
                return ToString(value);
            }
    
            /// <summary>
            /// Implicit operator for the MultiValueDynamicArray assignments.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static implicit operator MultiValueDynamicArray(string value)
            {
                return new MultiValueDynamicArray(value);
            }
    
            /// <summary>
            /// Implicit operator for integer assignments.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static implicit operator MultiValueDynamicArray(int value)
            {
                var result = new MultiValueDynamicArray
                {
                    [1] = value.ToString()
                };
                return result;
            }
    
            /// <summary>
            /// Implicit operator for double assignments.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static implicit operator MultiValueDynamicArray(double value)
            {
                var result = new MultiValueDynamicArray
                {
                    [1] = value.ToString(CultureInfo.CurrentCulture)
                };
                return result;
            }
    
            /// <summary>
            /// Implicit operator for decimal assignments.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static implicit operator MultiValueDynamicArray(decimal value)
            {
                var result = new MultiValueDynamicArray
                {
                    [1] = value.ToString(CultureInfo.InvariantCulture)
                };
                return result;
            }
    
            /// <summary>
            /// Implicit operator for float assignments.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static implicit operator MultiValueDynamicArray(float value)
            {
                var result = new MultiValueDynamicArray
                {
                    [1] = value.ToString(CultureInfo.InvariantCulture)
                };
                return result;
            }
    
            #endregion
            
            #region public methods:
            
            /// <summary>
            /// Retrieves the number of attributes in the matrix
            /// </summary>
            /// <returns></returns>
            public int GetAttributeCount()
            {
                return _arrayString.Split(Am).Length;
            }
            
            /// <summary>
            /// Retrieves the number of values in the matrix at a given attribute
            /// </summary>
            /// <param name="attribute">attribute to get the values from</param>
            /// <returns></returns>
            public int GetValuesCount(int attribute)
            {
                return this[attribute].Split(Vm).Length ;
            }
            
            /// <summary>
            /// Retrieves the number of subvalues in the matrix at a given attribute and value
            /// </summary>
            /// <param name="attribute"></param>
            /// <param name="mvValue"></param>
            /// <returns></returns>
            public int GetSubValuesCount(int attribute, int mvValue)
            {
                return _arrayString.Split(Am)[attribute - 1].Split(Vm)[mvValue -1].Split(Sv).Length;
            }
            
            #endregion 
    
            #region public properties:
    
            /// <summary>
            /// Gets or sets the value of the attribute of the matrix at the current index
            /// </summary>
            /// <param name="attribute">attribute's number</param>
            public string this[int attribute]
            {
                get => GetValue(attribute);
                set => SetValue(attribute, value);
            }
    
            /// <summary>
            /// Gets or sets the value of the attribute of the matrix at the current index
            /// </summary>
            /// <param name="attribute">attribute</param>
            /// <param name="mvValue">value</param>
            public string this[int attribute, int mvValue]
            {
                get => GetValue(attribute, mvValue);
                set => SetValue(attribute, mvValue, value);
            }
    
    
            /// <summary>
            /// Gets the number of attributes in the matrix.    
            /// </summary>
            /// <param name="attribute">attribute</param>
            /// <param name="mvValue">value</param>
            /// <param name="subValue">subvalue</param>
            public string this[int attribute, int mvValue, int subValue]
            {
                get => _arrayString.Split(Am)[attribute - 1].Split(Vm)[mvValue - 1].Split(Sv)[subValue - 1];
                set => SetValue(attribute, mvValue, subValue, value);
            }
    
            #endregion
        }
}