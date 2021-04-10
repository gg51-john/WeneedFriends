using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.ViewModel
{
    public class CLottoGen
    {
        public string getLotto()
        {
            Random rand = new Random();
            int count = 0;
            int[] numbers = new int[6];

            while (count < 6)
            {
                int temp = rand.Next(1, 50);

                      
                if (!flag(temp, numbers))
                {
                    numbers[count] = temp;
                    count++;
                }
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    if (numbers[j + 1] < numbers[j])
                    {
                        int big = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = big;

                    }
                }
            }

            string s = "";
            foreach (int i in numbers)
                s += i.ToString() + " ";


            return s;
        }
        private bool flag(int temp, int[] numbers)
        {

            foreach (int i in numbers)
            {
                if (i == temp)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
