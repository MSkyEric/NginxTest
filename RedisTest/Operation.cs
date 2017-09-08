using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    namespace CalculatorDemo
    {
        //【注意】类和方法都要用public修饰，不然没有“生成单元测试”的选项
        public class Operation
        {
            public int Add(int a, int b)
            {
                return a + b;
            }

            public int Minus(int a, int b)
            {
                return a - b;
            }

            public int Divide(int a, int b)
            {
                return a / b;
            }

            public int Multiply(int a, int b)
            {
                return a * b;
            }
        }
    }
}
