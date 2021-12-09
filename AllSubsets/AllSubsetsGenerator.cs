using System;
using System.Collections.Generic;
using System.Linq;

namespace AllSubsets
{
    public class AllSubsetsGenerator<T>
    {
        private List<int[]> codeGrayList;

        private void InitCodеGrey(int length, int capacity)
        {
            codeGrayList = new List<int[]>(capacity); //Инициализация массива кодов Грея

            var stack = new int[length + 1]; // Стек

            var grayVector = new int[length]; // Массив для текущего вектора кода Грея
            int i;

            for (i = 0; i < length; ++i) // Инициализация первого вектора кода Грея 
                grayVector[i] = 0;
            for (i = 0; i < length + 1; ++i) // и заполнение рабочего стека
                stack[i] = i + 1;

            i = stack[0];
            while (i < length + 1)
            {
                var resVector = new int[length];

                grayVector.CopyTo(resVector, 0);

                codeGrayList.Add(resVector);

                grayVector[length - i] = (grayVector[length - i] + 1) % 2; // Инвертируем значение переменной

                stack[0] = 1; // Заносим в стек индексы позиций 
                stack[i - 1] = stack[i]; // для дальнейших преобразований
                stack[i] = i + 1;

                i = stack[0]; // Выталкиваем из стека элемент
            }

            codeGrayList.Add(grayVector);
        }

        private static int ComputeCodeGrayWeight(IEnumerable<int> codeGray) 
        { 
            return codeGray.Count(bit => bit == 1);
        }

        public List<List<T>> GenerateAllSubsets(List<T> set)
        {
            if (set == null)
                throw new ArgumentOutOfRangeException(nameof(set));

            if (set.Count == 0)
                return new List<List<T>>(new[] { new List<T>() });

            var capacity = (int) Math.Pow(2, set.Count);

            var result = new List<List<T>>(capacity);

            InitCodеGrey(set.Count, capacity);

            for (var i = 0; i < capacity; i++)
            {
                var cap = ComputeCodeGrayWeight(codeGrayList[i]);

                result.Add(new List<T>(cap));

                for (var j = 0; j < set.Count; j++)
                    if (codeGrayList[i][j] == 1)
                        result[i].Add(set[j]);
            }

            return result;
        }
    }
}