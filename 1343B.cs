using System;
using System.IO;
 
namespace Issue
{
	public class Solver
	{
		private int[] _array;

		private int _minimal_unused; // odd
		private int _minimal_index;

		public void Calculate(int array_length) {
			_array = new int[array_length];

			var half_size = array_length / 2;

			_minimal_unused = 1;
			_minimal_index = half_size + 1;

			for (var i = 0; i < half_size; i += 2) {
				var first_even = (i + 1) * 2;
				var second_even = (i + 2) * 2;

				_array[i] = first_even;
				_array[i + 1] = second_even;
				
				var first_odd = _minimal_unused;
				var second_odd = first_even + second_even - first_odd;

				_array[half_size + i] = first_odd;;
				_array[half_size + i + 1] = second_odd;

				LookupNewMinimal( 
					bound: half_size + i + 1
				);
			}
		}

		private void LookupNewMinimal(int bound) {
			var candidate = _minimal_unused + 2;

			while (!IsNumberUnused(bound, candidate)) {
				candidate += 2;
			}

			_minimal_unused = candidate;
		}

		private bool IsNumberUnused(int bound, int candidate) {
		
			for (var i = _minimal_index; i <= bound; i += 2) {
				var item = _array[i];

				if (candidate > item) {
					_minimal_index = i + 2;
				}
				if (candidate == _array[i]) {
					return false;
				}
			}

			return true;
		}

		public void Solve(int array_length) {
			var half = array_length / 2;

			if ((half % 2) != 0) {
				Console.WriteLine("NO");
				return;
			}
			
			var odds_bound = _array.Length / 2;

			Console.WriteLine("YES");
			for (var i = 0; i < half; i++) {
				Console.Write(_array[i] + " ");
			}
			for (var i = 0; i < half - 1; i++) {
				Console.Write(_array[odds_bound + i] + " ");
			}

			Console.WriteLine(_array[odds_bound + half - 1]);
		}
	}

    public static class Program
    {
        public static void Main(string[] args) {
			var inputs_count = IO.Read();
			var inputs = new int[inputs_count];

			for (var i = 0; i < inputs_count; i++) {
				var array_length = IO.Read();

				inputs[i] = array_length; 
			}

			var solver = new Solver();
			
			var maximal_sequence = FindValidMaximal(inputs);
			if (maximal_sequence != -1) {
				solver.Calculate(maximal_sequence);
			}
			
			for (var i = 0; i < inputs_count; i++) {
				solver.Solve(inputs[i]);
			}
        }

		private static int FindValidMaximal(int[] array) {
			var maximal = -1;

			for (var i = 0; i < array.Length; i++) {
				var candidate = array[i];
				
				var isValid = candidate > maximal && (candidate % 4) == 0;

				if (isValid) {
					maximal = candidate;
				}
			}

			return maximal;
		}

		

		private static void WriteAnswer(int[] array) {
			if (array == null) {
				Console.WriteLine("NO");
				return;
			}
			
			var answer = string.Join(" ", array);
			
			Console.WriteLine("YES");
			Console.WriteLine(answer);
		}
    }
 
    public static class IO {
        public const int ZeroCode = (int) '0';
 
        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(),
                System.Text.Encoding.ASCII,
                false, 
                sizeof(int),
                false);
        }
 
        public static StreamReader Reader { 
            get; set;
        }
 
        public static string ReadLine() {
            return Reader.ReadLine();
        }
 
        public static int Read() {
            var reader = Reader;
            var symbol = reader.Read();
 
            while (symbol == ' ') {
                symbol = reader.Read();
            }
 
            var isNegative = false;
            if (symbol == '-') {
                isNegative = true;
                symbol = reader.Read();
            }
 
            int result = 0;
            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = reader.Read()
            ) {
                var digit = symbol - ZeroCode;
                
                if (digit < 10 && digit >= 0) {
                    result = (result << 1) + ((result << 1) << 2) + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        reader.Read();        // skip next \r symbol
                    break;
                }
            }
 
            return isNegative ? ~result + 1 : result;
        }

		public static long ReadLong() {
            var reader = Reader;
            var symbol = reader.Read();
 
            while (symbol == ' ') {
                symbol = reader.Read();
            }
 
            var isNegative = false;
            if (symbol == '-') {
                isNegative = true;
                symbol = reader.Read();
            }
 
            var result = 0L;
            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = reader.Read()
            ) {
                var digit = symbol - ZeroCode;
                
                if (digit < 10 && digit >= 0) {
                    result = (result << 1) + ((result << 1) << 2) + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        reader.Read();        // skip next \r symbol
                    break;
                }
            }
 
            return isNegative ? ~result + 1 : result;
        }
    }
 
    public static class Sort {
        public static void Heapsort<T>(this T[] array, bool isReversed = false)
            where T : IComparable<T>
        {
            Func<T[], int, int, int> comparer;
 
            if (isReversed) 
                comparer = Extensions.Min;
            else 
                comparer = Extensions.Max;
 
            // create Heap from unordered array
            BuildHeap(array, comparer);
 
            // sort
            for (var i = array.Length - 1; i >= 0; --i)
            {
                Swap(ref array[0], ref array[i]);
                FloatUp(array, 0, i, comparer);
            }
        }
 
        private static void BuildHeap<T>(T[] array, Func<T[], int, int, int> comparer)
            where T : IComparable<T>
        {
            var size = array.Length;
            var last_index = size - 1;
 
            for (var i = (last_index - 1) / 2; i >= 0; --i)
            {
                FloatUp(array, i, size, comparer);
            }
        }
 
        private static void FloatUp<T>(T[] array, int index, int size, Func<T[], int, int, int> comparer)
            where T : IComparable<T>
        {
            var left_node_index = 2 * (index + 1) - 1;
            var right_node_index = 2 * (index + 1);
 
            if (left_node_index >= size)
            {
                return;
            }
 
            var largest = comparer(array, left_node_index, index);
 
            if (right_node_index < size)
            {
                largest = comparer(array, right_node_index, largest);
            }
 
            if (largest != index)
            {
                Swap(ref array[index], ref array[largest]);
                FloatUp(array, largest, size, comparer);
            }
        }
 
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
 
    public static class Extensions {
        public static int Max<T>(this T[] array, int firstIndex, int secondIndex)
            where T : IComparable<T>
        {
            return (array[firstIndex].CompareTo(array[secondIndex]) > 0)
                ? firstIndex
                : secondIndex;
        }
 
        public static int Min<T>(this T[] array, int firstIndex, int secondIndex)
            where T : IComparable<T>
        {
            return (array[firstIndex].CompareTo(array[secondIndex]) <= 0)
                ? firstIndex
                : secondIndex;
        }

		public static int Maximum(this int[] array) {
			var maximum = array[0];

			for (var i = 1; i < array.Length; i++) {
				var element = array[i];
				if (element > maximum)
					maximum = element;
			}

			return maximum;
		}
    }
}
