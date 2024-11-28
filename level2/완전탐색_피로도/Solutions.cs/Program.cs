using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {

    public int solution(int k, int[,] dungeons) {
        int n = dungeons.GetLength(0);//2차원배열의 행길이를 저장
        bool [] visited = new bool[n];//각 던전 별 방문 기록
        return DFS(k, dungeons, visited, 0);//재귀 호출을 진행하며 dungeons의 모든 경우의 수를 탐색
    }

    private int DFS(int currnetK, int[,] dungeons, bool[] visited, int count)
    {
        int maxCount = count;//최대 방문 횟수
        /*
            dungeon[i, 0] = 최소요구피로도
            dungeon[i, 1] = 소모 피로도
        */

        for(int i =0; i<dungeons.GetLength(0); i++)
        {
            if(!visited[i] && currnetK>=dungeons[i, 0])// 방문하지 않았고, 현재 피로도가 요구 피로도보다 큰 경우
            {
                visited[i] = true;//방문했음을 표시
                
                //재귀호출하면서 현재피로도-소모피로도 값을 넘겨주고 최대 방문횟수를 1씩 증가시킴. 재귀로 리턴된 이 값을 newCount에 저장
                int newCount = DFS(currnetK - dungeons[i,1], dungeons, visited, count+1);
                
                maxCount = Math.Max(maxCount, newCount);//재귀로 넘어온 값과 maxCount값 중 max값을 반환.
                visited[i] = false;// 다른 경로도 탐색해야 하므로 방문 표시를 제거
            }
        }return maxCount;
    }

    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        int k = 80;
        int[,] dun = {{80, 20}, {50, 40}, {30, 10}};
        System.Console.WriteLine(solution.solution(k ,dun));
    }
}