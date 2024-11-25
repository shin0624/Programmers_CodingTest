using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int solution(int[] diffs, int[] times, long limit) {
        int mid = 0;
        int minlevel = int.MaxValue;//최소숙련도->제일큰거로초기화
        int left = 1;//난이도[0]은 무조건 1
        long right = diffs.Max();//난이도배열 최대값(탐색종료)
        
        //숙련도(level)가 어디까지 있는지 범위 탐색->이진탐색을 사용하여 최소~최대 중 중간값을 선택하여 비교
        while(left<=right)
        {           
            mid = (int)( (right + left) / 2); //숙련도 배열의 중간값(mid)을 찾아서 level로 넘긴다. 퍼즐 해결이 가능한지 판단
            if(SolveAll(mid, diffs, times, limit))
            {
                minlevel = Math.Min(minlevel, mid);
                right = mid-1;
                }//만약 중간값으로 퍼즐을 풀수 있다? -> 더낮은 값으로도 해결가능한지 본다.
            else{left = mid+1;}   //불가능? -> 더 높은 숙련도레벨 탐색
        }
        return Math.Min(minlevel, left);//찾은거중 제일 낮은 거 리턴
    }

    private bool SolveAll(int level, int[] diffs, int[] times, long limit)//숙련도에 대해 모든 퍼즐을 풀수 있는지 체크. 중간값(숙련도)를 기준으로 이전, 이후 난이도와 비교
    {
        //level은 solution에서 계산한 난이도배열의 중간값
        int n = diffs.Length;//난이도 배열 길이
        long totalTime = 0;//총소요시간

        for(int i=0; i<n;i++)
        {
            if(diffs[i] <=level){totalTime+=times[i];}//숙련도가 더 높을때
            else{
                //숙련도가 더 낮을 때 = diff - level
                int mis = diffs[i]- level;
                long timeForMis = i>0 ? (long)times[i]+times[i-1] : times[i];//틀릴때마다 현재풀이소요시간+이전풀이소요시간 ->틀린횟수만큼 반복해서 풀어야할 시간
                totalTime += timeForMis*mis + times[i];//총소요시간 : (삽질시간 + 현재소요시간)*틀린횟수
            }

            if(totalTime > limit){return false;}//limit 내에 다 못풀면 false전달
        }return true;
    }

    static void Main(string[] args)
    {
        Solution solution = new Solution();
        int[] dif = {1,4,4,2};
        int [] time = {6, 3, 8, 2};
        long limits = 59;
        Console.WriteLine(solution.solution(dif, time, limits));
    }
}