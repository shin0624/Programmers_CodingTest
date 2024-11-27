using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
public class Solution {
    public int solution(string[,] book_time) {
        var timeList = new List<(DateTime start, DateTime end)>(); //2차원 문자열 배열을 List로 변환하고 DateTime 형식을 인자로 받음
        
        for(int i = 0; i<book_time.GetLength(0); i++)//2차원배열 길이는 행 수 -> Getlength사용
        {
            DateTime startTime = DateTime.ParseExact(book_time[i, 0], "HH:mm", CultureInfo.InvariantCulture);//대실 시작시간
            DateTime endTime = DateTime.ParseExact(book_time[i, 1], "HH:mm", CultureInfo.InvariantCulture).AddMinutes(10);//대실 종료시간. 여기에 10분을 더해야 청소시간까지 더해짐
            
            timeList.Add((startTime, endTime));//리스트에 대실시간을 삽입
        }

        var sortTime = timeList.OrderBy(x=>x.start).ToList();// 예약 시작 시간을 기준으로 정렬해서 시간순으로 예약 목록을 구조화->정렬된 상태에서 예약시간을 돌면서 객실 수 계산

        //고려해야하는것 -> [15:00, 17:00], [16:00, 18:00]처럼 예약시간이 겹칠때
        //--> foreach문으로 sortTime에서 예약시간을 뽑아와서, 뽑아온 시간과 startTime이 같은 예약은 지워야 함
        
        var rooms = new List<DateTime>();// 사용중인 객실의 종료 시간을 추적할 리스트
        int maxRooms = 0;

        foreach(var time in sortTime)
        {
            rooms.RemoveAll(x=> x<=time.start); // 각 객실 종료시간 x가 지금 foreach에서 처리중인 예약의 시작 시간보다 작은 것(즉, 13시 <15시 처럼 예약시간 이전이거나 같은것. 종료 시간이 현재 예약시간 이전인 것 = 사용종료된것)을 제거
            rooms.Add(time.end);// 새 객실을 추가한다.
            maxRooms = Math.Max(maxRooms, rooms.Count);// 겹치는 객실 수를 제거한 객실 수와 maxRoom을 비교하여 더 큰걸 리턴
        }
        return maxRooms;
    }
    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        string[,] a = {{"15:00", "17:00"}, {"16:40", "18:20"},{"14:20", "15:20"},{"14:10", "19:20"},{"18:20", "21:20"}};
        System.Console.WriteLine(solution.solution(a));
    }
}