using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int[] solution(int n) {
        int[,] total = new int [n,n];//n * n 크기 2차원 배열 생성. 값이 없는 부분은 0으로 채워짐
        int num = 1;//채울 숫자
        int x = -1, y = 0; //행렬 초기 위치. x를 -1부터 시작해야 루프를 도는 과정에서 (0, 0) 위치부터 시작 가능

        for(int i=n;i >0; i-=3)//반시계 방향으로 채우기. 삼각형의 세 변이 아래->우->위로 채워질 때 마다 한 변씩 채워지는 것과 같으므로 한 사이클 끝나면 내부 삼각형 채우기 위해 -3해줌
        {
            for(int j = 0; j< i; j++)//아래 방향
            {
                //x++;//x좌표(행수)를 증가시키며 
                total[++x,y] = num++;//숫자 채워감
            }

            for(int j=0; j <i-1; j++)//우측 방향. 현재 크기에서 한 칸 줄인 크기로 우측 방향 채움
            {
                //y++;//y좌표(열수)를 증가시키며 
                total[x,++y] = num++;//숫자 채워감
            }

            for(int j=0; j<i-2; j++)// 대각선 위 . 현재 크기에서 두 칸 줄인 크기로 위측 방향 채움
            {
                //x--;//x좌표(행수) 감소
                //y--;//y좌표(열수) 감소
                total[--x,--y] = num++;//숫자 채워감
            }
        }

        List<int>result = new List<int>();//결과를 1차원 배열로 반환
        for(int i=0; i<n;i++)
        {
            for(int j=0; j<=i; j++)
            {
                result.Add(total[i,j]);
            }
        }
        return result.ToArray();
    }
    /*
        삼각형을 반시계로 채우는 사이클 : 아래 -> 우 -> 위
        n=4일때 : 아래->우->위 방향에서 4만큼 숫자 채움 -> 겉에 다 채움
                    -> 내부 삼각형 채워야 함
                    -> 한 사이클 끝날 때 마다 내부로 들어가니까 -3 필요
                    -> n=4이면 1 ~ 9까지 채워지고, 내부의 10만 채우면 됨

    */
    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        int n = 6;
        System.Console.WriteLine(String.Join(", ", solution.solution(n)));
    }
}