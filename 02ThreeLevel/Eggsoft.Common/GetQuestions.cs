using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;

namespace Eggsoft.Common
{
    class GetQuestions
    {


    }

    class RandomNumberHelper
{
public static IList<int> RandomSelect(IList<int> sourceList, int selectCount)
{
if (selectCount > sourceList.Count)
throw new ArgumentOutOfRangeException("selectCount必需大于sourceList.Count");
IList<int> resultList = new List<int>();
for (int i = 0; i < selectCount; i++)
{
int nextIndex = GetRandomNumber(1, sourceList.Count);
int nextNumber = sourceList[nextIndex - 1];
sourceList.RemoveAt(nextIndex - 1);
resultList.Add(nextNumber);
}
return resultList;
}
public static int GetRandomNumber(int minValue, int maxValue)
{
return random.Next(minValue, maxValue + 1);
}
private static Random random = new Random();
}


}

