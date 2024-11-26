using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int[] solution(string today, string[] terms, string[] privacies) {

        DateTime todayDate = DateTime.ParseExact(today, "yyyy.MM.dd", null);// c#의 데이트타임 구조체를 사용. ParseExact를 사용하여 string 날짜를 DateTime 객체로 바꾼다.

        List<int> answer = new List<int>();//파기할 개인정보 번호를 저장할 리스트

        //약관 종류와 유효기간을 딕셔너리에 매핑-> terms는 ["A 6"] 형태이므로 스플릿해서 A를 [0]에, 6을 [1]에 넣는다.
        Dictionary<string, int> termDict = terms.ToDictionary(
                        term=>term.Split()[0],
                        term=>int.Parse(term.Split()[1])
        );

        for(int i=0; i<privacies.Length; i++)//개인정보를 순회하면 각 개인정보 수집 날짜, 약관 종류를 분리한다.
        {
            string [] priInfo = privacies[i].Split();
            string priDate = priInfo[0];// 수집날짜
            string priTerm  =priInfo[1];//약관 종류

            DateTime collectDate = DateTime.ParseExact(priDate, "yyyy.MM.dd", null);// 수집한 날짜를 DateTime으로 변환
            int month = termDict[priTerm];// 약관종류, 유효기간을 매핑해놓은 딕셔너리에서 약관 종류에 따른 유효기간을 가져온다.
            DateTime expiration = collectDate.AddMonths(month).AddDays(-1);// Addmonth로 딕셔너리에서 가져온 유효기간을 추가하고, AddDays로 해당 월의 마지막 날을 계산한다.

            if(expiration < todayDate){answer.Add(i+1);}//만료 날짜 < 오늘 날짜 이면 파기 리스트에 넣는다.
        
        }
        return answer.OrderBy(x=>x).ToArray();//파기 리스트를 오름차순 정렬 후 배열로 리턴.
    }

    //today = 오늘 날짜 , 유효기간 배열 terms, 개인정보 배열 privacies
    //파기해야 할 개인정보 번호를 오름차순으로 배열에 담아 리턴
    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        string todays = "2020.01.01";
        string []terms = {"Z 3", "D 5"};
        string [] pri = {"2019.01.01 D", "2019.11.15 Z", "2019.08.02 D", "2019.07.01 D", "2018.12.28 Z"};

        System.Console.WriteLine(string.Join(",", solution.solution(todays, terms, pri)));
    }
}