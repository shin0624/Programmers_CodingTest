using System;
using System.Collections.Generic;
using System.Linq;


public class Solution
{
    public int[] solution(string[,] clothes)
    {
        //의상 종류 별로 개수를 저장할 딕셔너리<의상, 개수>
        Dictionary<string, int> ClothesCount = new Dictionary<string, int>();

        //의상 종류별로 개수계산
        for (int i = 0; i < clothes.GetLength(0); i++)
        {
            string Category = clothes[i, 1];// 의상의 종류, 개수를 키로 지정
            if (ClothesCount.ContainsKey(Category))//키가 존재한다면
            {
                ClothesCount[Category]++;
            }
            else
            {
                ClothesCount[Category] = 1;
            }
        }
        //각 카테고리별로 (의상갯수+1)을 곱한 후 1을 빼야 함
        //+1 : 해당 종류의 의상을 안입을 경우
        //-1 : 아무것도 입지 않는경우 제외

        int result = ClothesCount.Values.Aggregate(1, (total, n) => total * (n + 1)) - 1;//Aggregate는 linq의 확장메서드로, 컬렉션의 모든 요소들을 하나의 값으로 누적하여 계산
        return new int[] { result };//반환타입은 인트형 배열 , 결과값을 배열에 담아서 반환함
    }


}